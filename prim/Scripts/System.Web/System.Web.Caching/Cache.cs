using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Caching;

/// <summary>Implements the cache for a Web application. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class Cache : IEnumerable
{
	private const int LOW_WATER_MARK = 10000;

	private const int HIGH_WATER_MARK = 15000;

	/// <summary>Used in the <paramref name="absoluteExpiration" /> parameter in an <see cref="M:System.Web.Caching.Cache.Insert(System.String,System.Object)" /> method call to indicate the item should never expire. This field is read-only.</summary>
	public static readonly DateTime NoAbsoluteExpiration = DateTime.MaxValue;

	/// <summary>Used as the <paramref name="slidingExpiration" /> parameter in an <see cref="M:System.Web.Caching.Cache.Insert(System.String,System.Object)" /> or <see cref="M:System.Web.Caching.Cache.Add(System.String,System.Object,System.Web.Caching.CacheDependency,System.DateTime,System.TimeSpan,System.Web.Caching.CacheItemPriority,System.Web.Caching.CacheItemRemovedCallback)" /> method call to disable sliding expirations. This field is read-only.</summary>
	public static readonly TimeSpan NoSlidingExpiration = TimeSpan.Zero;

	private ReaderWriterLockSlim cacheLock;

	private CacheItemLRU cache;

	private CacheItemPriorityQueue timedItems;

	private Timer expirationTimer;

	private long expirationTimerPeriod;

	private Cache dependencyCache;

	private bool? disableExpiration;

	private long privateBytesLimit = -1L;

	private long percentagePhysicalMemoryLimit = -1L;

	private bool DisableExpiration
	{
		get
		{
			if (!disableExpiration.HasValue)
			{
				if (!(WebConfigurationManager.GetWebApplicationSection("system.web/caching/cache") is CacheSection cacheSection))
				{
					disableExpiration = false;
				}
				else
				{
					disableExpiration = cacheSection.DisableExpiration;
				}
			}
			return disableExpiration.Value;
		}
	}

	/// <summary>Gets the number of bytes available for the cache.</summary>
	/// <returns>The number of bytes available for the cache.</returns>
	public long EffectivePrivateBytesLimit
	{
		get
		{
			if (privateBytesLimit == -1)
			{
				if (!(WebConfigurationManager.GetWebApplicationSection("system.web/caching/cache") is CacheSection cacheSection))
				{
					privateBytesLimit = 0L;
				}
				else
				{
					privateBytesLimit = cacheSection.PrivateBytesLimit;
				}
				if (privateBytesLimit == 0L)
				{
					privateBytesLimit = 734003200L;
				}
			}
			return privateBytesLimit;
		}
	}

	/// <summary>Gets the percentage of physical memory that can be consumed by an application before ASP.NET starts removing items from the cache.</summary>
	/// <returns>The percentage of physical memory available to the application.</returns>
	public long EffectivePercentagePhysicalMemoryLimit
	{
		get
		{
			if (percentagePhysicalMemoryLimit == -1)
			{
				if (!(WebConfigurationManager.GetWebApplicationSection("system.web/caching/cache") is CacheSection cacheSection))
				{
					percentagePhysicalMemoryLimit = 0L;
				}
				else
				{
					percentagePhysicalMemoryLimit = cacheSection.PercentagePhysicalMemoryUsedLimit;
				}
				if (percentagePhysicalMemoryLimit == 0L)
				{
					percentagePhysicalMemoryLimit = 97L;
				}
			}
			return percentagePhysicalMemoryLimit;
		}
	}

	/// <summary>Gets the number of items stored in the cache.</summary>
	/// <returns>The number of items stored in the cache.</returns>
	public int Count => cache.Count;

	/// <summary>Gets or sets the cache item at the specified key.</summary>
	/// <param name="key">A <see cref="T:System.String" /> object that represents the key for the cache item.</param>
	/// <returns>The specified cache item.</returns>
	public object this[string key]
	{
		get
		{
			return Get(key);
		}
		set
		{
			Insert(key, value);
		}
	}

	internal Cache DependencyCache
	{
		get
		{
			if (dependencyCache == null)
			{
				return this;
			}
			return dependencyCache;
		}
		set
		{
			dependencyCache = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.Cache" /> class.</summary>
	public Cache()
	{
		cacheLock = new ReaderWriterLockSlim();
		cache = new CacheItemLRU(this, 15000, 10000);
	}

	private CacheItem RemoveCacheItem(string key)
	{
		if (key == null)
		{
			return null;
		}
		CacheItem cacheItem = cache[key];
		if (cacheItem == null)
		{
			return null;
		}
		_ = timedItems;
		cacheItem.Disabled = true;
		cache.Remove(key);
		return cacheItem;
	}

	/// <summary>Adds the specified item to the <see cref="T:System.Web.Caching.Cache" /> object with dependencies, expiration and priority policies, and a delegate you can use to notify your application when the inserted item is removed from the <see langword="Cache" />.</summary>
	/// <param name="key">The cache key used to reference the item. </param>
	/// <param name="value">The item to be added to the cache. </param>
	/// <param name="dependencies">The file or cache key dependencies for the item. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains <see langword="null" />.</param>
	/// <param name="absoluteExpiration">The time at which the added object expires and is removed from the cache. If you are using sliding expiration, the <paramref name="absoluteExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration" />.</param>
	/// <param name="slidingExpiration">The interval between the time the added object was last accessed and the time at which that object expires. If this value is the equivalent of 20 minutes, the object expires and is removed from the cache 20 minutes after it is last accessed. If you are using absolute expiration, the <paramref name="slidingExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration" />.</param>
	/// <param name="priority">The relative cost of the object, as expressed by the <see cref="T:System.Web.Caching.CacheItemPriority" /> enumeration. The cache uses this value when it evicts objects; objects with a lower cost are removed from the cache before objects with a higher cost. </param>
	/// <param name="onRemoveCallback">A delegate that, if provided, is called when an object is removed from the cache. You can use this to notify applications when their objects are deleted from the cache.</param>
	/// <returns>An object that represents the item that was added if the item was previously stored in the cache; otherwise, <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="value" /> parameter is set to <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="slidingExpiration" /> parameter is set to less than <see langword="TimeSpan.Zero" /> or more than one year.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="absoluteExpiration" /> and <paramref name="slidingExpiration" /> parameters are both set for the item you are trying to add to the <see langword="Cache" />.</exception>
	public object Add(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		try
		{
			cacheLock.EnterWriteLock();
			CacheItem cacheItem = cache[key];
			if (cacheItem != null)
			{
				return cacheItem.Value;
			}
			Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback, null, doLock: false);
		}
		finally
		{
			cacheLock.ExitWriteLock();
		}
		return null;
	}

	/// <summary>Retrieves the specified item from the <see cref="T:System.Web.Caching.Cache" /> object.</summary>
	/// <param name="key">The identifier for the cache item to retrieve.</param>
	/// <returns>The retrieved cache item, or <see langword="null" /> if the key is not found.</returns>
	public object Get(string key)
	{
		try
		{
			cacheLock.EnterUpgradeableReadLock();
			CacheItem cacheItem = cache[key];
			if (cacheItem == null)
			{
				return null;
			}
			if (cacheItem.Dependency != null && cacheItem.Dependency.HasChanged)
			{
				try
				{
					cacheLock.EnterWriteLock();
					if (!NeedsUpdate(cacheItem, CacheItemUpdateReason.DependencyChanged, needLock: false))
					{
						Remove(cacheItem.Key, CacheItemRemovedReason.DependencyChanged, doLock: false, invokeCallback: true);
					}
				}
				finally
				{
					cacheLock.ExitWriteLock();
				}
				return null;
			}
			if (!DisableExpiration)
			{
				if (cacheItem.SlidingExpiration != NoSlidingExpiration)
				{
					cacheItem.AbsoluteExpiration = DateTime.Now + cacheItem.SlidingExpiration;
					long num = (long)cacheItem.SlidingExpiration.TotalMilliseconds;
					cacheItem.ExpiresAt = cacheItem.AbsoluteExpiration.Ticks;
					if (expirationTimer != null && (expirationTimerPeriod == 0L || expirationTimerPeriod > num))
					{
						expirationTimerPeriod = num;
						expirationTimer.Change(expirationTimerPeriod, expirationTimerPeriod);
					}
				}
				else if (DateTime.Now >= cacheItem.AbsoluteExpiration)
				{
					try
					{
						cacheLock.EnterWriteLock();
						if (!NeedsUpdate(cacheItem, CacheItemUpdateReason.Expired, needLock: false))
						{
							Remove(key, CacheItemRemovedReason.Expired, doLock: false, invokeCallback: true);
						}
					}
					finally
					{
						cacheLock.ExitWriteLock();
					}
					return null;
				}
			}
			return cacheItem.Value;
		}
		finally
		{
			cacheLock.ExitUpgradeableReadLock();
		}
	}

	/// <summary>Inserts an item into the <see cref="T:System.Web.Caching.Cache" /> object with a cache key to reference its location, using default values provided by the <see cref="T:System.Web.Caching.CacheItemPriority" /> enumeration.</summary>
	/// <param name="key">The cache key used to reference the item. </param>
	/// <param name="value">The object to be inserted into the cache.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="value" /> parameter is <see langword="null" />.</exception>
	public void Insert(string key, object value)
	{
		Insert(key, value, null, NoAbsoluteExpiration, NoSlidingExpiration, CacheItemPriority.Normal, null, null, doLock: true);
	}

	/// <summary>Inserts an object into the <see cref="T:System.Web.Caching.Cache" /> that has file or key dependencies.</summary>
	/// <param name="key">The cache key used to identify the item.</param>
	/// <param name="value">The object to be inserted in the cache.</param>
	/// <param name="dependencies">The file or cache key dependencies for the inserted object. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains <see langword="null" />.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="value" /> parameter is <see langword="null" />.</exception>
	public void Insert(string key, object value, CacheDependency dependencies)
	{
		Insert(key, value, dependencies, NoAbsoluteExpiration, NoSlidingExpiration, CacheItemPriority.Normal, null, null, doLock: true);
	}

	/// <summary>Inserts an object into the <see cref="T:System.Web.Caching.Cache" /> with dependencies and expiration policies.</summary>
	/// <param name="key">The cache key used to reference the object. </param>
	/// <param name="value">The object to be inserted in the cache. </param>
	/// <param name="dependencies">The file or cache key dependencies for the inserted object. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains <see langword="null" />.</param>
	/// <param name="absoluteExpiration">The time at which the inserted object expires and is removed from the cache. To avoid possible issues with local time such as changes from standard time to daylight saving time, use <see cref="P:System.DateTime.UtcNow" /> rather than <see cref="P:System.DateTime.Now" /> for this parameter value. If you are using absolute expiration, the <paramref name="slidingExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration" />.</param>
	/// <param name="slidingExpiration">The interval between the time the inserted object is last accessed and the time at which that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. If you are using sliding expiration, the <paramref name="absoluteExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration" />.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="value" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">You set the <paramref name="slidingExpiration" /> parameter to less than <see langword="TimeSpan.Zero" /> or the equivalent of more than one year.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="absoluteExpiration" /> and <paramref name="slidingExpiration" /> parameters are both set for the item you are trying to add to the <see langword="Cache" />.</exception>
	public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration)
	{
		Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, CacheItemPriority.Normal, null, null, doLock: true);
	}

	/// <summary>Inserts an object into the <see cref="T:System.Web.Caching.Cache" /> object together with dependencies, expiration policies, and a delegate that you can use to notify the application before the item is removed from the cache.</summary>
	/// <param name="key">The cache key that is used to reference the object.</param>
	/// <param name="value">The object to insert into the cache.</param>
	/// <param name="dependencies">The file or cache key dependencies for the item. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains <see langword="null" />.</param>
	/// <param name="absoluteExpiration">The time at which the inserted object expires and is removed from the cache. To avoid possible issues with local time such as changes from standard time to daylight saving time, use <see cref="P:System.DateTime.UtcNow" /> instead of <see cref="P:System.DateTime.Now" /> for this parameter value. If you are using absolute expiration, the <paramref name="slidingExpiration" /> parameter must be set to <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration" />.</param>
	/// <param name="slidingExpiration">The interval between the time that the cached object was last accessed and the time at which that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. If you are using sliding expiration, the <paramref name="absoluteExpiration" /> parameter must be set to <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration" />.</param>
	/// <param name="onUpdateCallback">A delegate that will be called before the object is removed from the cache. You can use this to update the cached item and ensure that it is not removed from the cache.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" />, <paramref name="value" />, or <paramref name="onUpdateCallback" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">You set the <paramref name="slidingExpiration" /> parameter to less than <see langword="TimeSpan.Zero" /> or the equivalent of more than one year.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="absoluteExpiration" /> and <paramref name="slidingExpiration" /> parameters are both set for the item you are trying to add to the <see langword="Cache" />.-or-The <paramref name="dependencies" /> parameter is <see langword="null" />, and the <paramref name="absoluteExpiration" /> parameter is set to <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration" />, and the <paramref name="slidingExpiration" /> parameter is set to <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration" />.</exception>
	public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemUpdateCallback onUpdateCallback)
	{
		Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, CacheItemPriority.Normal, null, onUpdateCallback, doLock: true);
	}

	/// <summary>Inserts an object into the <see cref="T:System.Web.Caching.Cache" /> object with dependencies, expiration and priority policies, and a delegate you can use to notify your application when the inserted item is removed from the <see langword="Cache" />.</summary>
	/// <param name="key">The cache key used to reference the object.</param>
	/// <param name="value">The object to be inserted in the cache.</param>
	/// <param name="dependencies">The file or cache key dependencies for the item. When any dependency changes, the object becomes invalid and is removed from the cache. If there are no dependencies, this parameter contains <see langword="null" />.</param>
	/// <param name="absoluteExpiration">The time at which the inserted object expires and is removed from the cache. To avoid possible issues with local time such as changes from standard time to daylight saving time, use <see cref="P:System.DateTime.UtcNow" /> rather than <see cref="P:System.DateTime.Now" /> for this parameter value. If you are using absolute expiration, the <paramref name="slidingExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoSlidingExpiration" />.</param>
	/// <param name="slidingExpiration">The interval between the time the inserted object was last accessed and the time at which that object expires. If this value is the equivalent of 20 minutes, the object will expire and be removed from the cache 20 minutes after it was last accessed. If you are using sliding expiration, the <paramref name="absoluteExpiration" /> parameter must be <see cref="F:System.Web.Caching.Cache.NoAbsoluteExpiration" />.</param>
	/// <param name="priority">The cost of the object relative to other items stored in the cache, as expressed by the <see cref="T:System.Web.Caching.CacheItemPriority" /> enumeration. This value is used by the cache when it evicts objects; objects with a lower cost are removed from the cache before objects with a higher cost.</param>
	/// <param name="onRemoveCallback">A delegate that, if provided, will be called when an object is removed from the cache. You can use this to notify applications when their objects are deleted from the cache.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="key" /> or <paramref name="value" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">You set the <paramref name="slidingExpiration" /> parameter to less than <see langword="TimeSpan.Zero" /> or the equivalent of more than one year.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="absoluteExpiration" /> and <paramref name="slidingExpiration" /> parameters are both set for the item you are trying to add to the <see langword="Cache" />.</exception>
	public void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback)
	{
		Insert(key, value, dependencies, absoluteExpiration, slidingExpiration, priority, onRemoveCallback, null, doLock: true);
	}

	private void Insert(string key, object value, CacheDependency dependencies, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemPriority priority, CacheItemRemovedCallback onRemoveCallback, CacheItemUpdateCallback onUpdateCallback, bool doLock)
	{
		if (key == null)
		{
			throw new ArgumentNullException("key");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (slidingExpiration < TimeSpan.Zero || slidingExpiration > TimeSpan.FromDays(365.0))
		{
			throw new ArgumentNullException("slidingExpiration");
		}
		if (absoluteExpiration != NoAbsoluteExpiration && slidingExpiration != NoSlidingExpiration)
		{
			throw new ArgumentException("Both absoluteExpiration and slidingExpiration are specified");
		}
		CacheItem cacheItem = new CacheItem();
		cacheItem.Value = value;
		cacheItem.Key = key;
		if (dependencies != null)
		{
			cacheItem.Dependency = dependencies;
			dependencies.DependencyChanged += OnDependencyChanged;
			dependencies.SetCache(DependencyCache);
		}
		cacheItem.Priority = priority;
		SetItemTimeout(cacheItem, absoluteExpiration, slidingExpiration, onRemoveCallback, onUpdateCallback, key, doLock);
	}

	internal void SetItemTimeout(string key, DateTime absoluteExpiration, TimeSpan slidingExpiration, bool doLock)
	{
		CacheItem cacheItem = null;
		try
		{
			if (doLock)
			{
				cacheLock.EnterWriteLock();
			}
			cacheItem = cache[key];
			if (cacheItem != null)
			{
				SetItemTimeout(cacheItem, absoluteExpiration, slidingExpiration, cacheItem.OnRemoveCallback, null, key, doLock: false);
			}
		}
		finally
		{
			if (doLock)
			{
				cacheLock.ExitWriteLock();
			}
		}
	}

	private void SetItemTimeout(CacheItem ci, DateTime absoluteExpiration, TimeSpan slidingExpiration, CacheItemRemovedCallback onRemoveCallback, CacheItemUpdateCallback onUpdateCallback, string key, bool doLock)
	{
		bool flag = DisableExpiration;
		if (!flag)
		{
			ci.SlidingExpiration = slidingExpiration;
			if (slidingExpiration != NoSlidingExpiration)
			{
				ci.AbsoluteExpiration = DateTime.Now + slidingExpiration;
			}
			else
			{
				ci.AbsoluteExpiration = absoluteExpiration;
			}
		}
		ci.OnRemoveCallback = onRemoveCallback;
		ci.OnUpdateCallback = onUpdateCallback;
		try
		{
			if (doLock)
			{
				cacheLock.EnterWriteLock();
			}
			if (key != null)
			{
				cache[key] = ci;
				cache.EvictIfNecessary();
			}
			ci.LastChange = DateTime.Now;
			if (flag || !(ci.AbsoluteExpiration != NoAbsoluteExpiration))
			{
				return;
			}
			bool flag2;
			if (ci.IsTimedItem)
			{
				flag2 = UpdateTimedItem(ci);
				if (!flag2)
				{
					UpdateTimerPeriod(ci);
				}
			}
			else
			{
				flag2 = true;
			}
			if (flag2)
			{
				ci.IsTimedItem = true;
				EnqueueTimedItem(ci);
			}
		}
		finally
		{
			if (doLock)
			{
				cacheLock.ExitWriteLock();
			}
		}
	}

	private bool UpdateTimedItem(CacheItem item)
	{
		if (timedItems == null)
		{
			return true;
		}
		item.ExpiresAt = item.AbsoluteExpiration.Ticks;
		return !timedItems.Update(item);
	}

	private void UpdateTimerPeriod(CacheItem item)
	{
		if (timedItems == null)
		{
			timedItems = new CacheItemPriorityQueue();
		}
		long num = Math.Max(0L, (long)(item.AbsoluteExpiration - DateTime.Now).TotalMilliseconds);
		item.ExpiresAt = item.AbsoluteExpiration.Ticks;
		if (num > 4294967294u)
		{
			num = 4294967294L;
		}
		if (expirationTimer == null || expirationTimerPeriod > num)
		{
			expirationTimerPeriod = num;
			if (expirationTimer == null)
			{
				expirationTimer = new Timer(ExpireItems, null, expirationTimerPeriod, expirationTimerPeriod);
			}
			else
			{
				expirationTimer.Change(expirationTimerPeriod, expirationTimerPeriod);
			}
		}
	}

	private void EnqueueTimedItem(CacheItem item)
	{
		UpdateTimerPeriod(item);
		timedItems.Enqueue(item);
	}

	/// <summary>Removes the specified item from the application's <see cref="T:System.Web.Caching.Cache" /> object.</summary>
	/// <param name="key">A <see cref="T:System.String" /> identifier for the cache item to remove.</param>
	/// <returns>The item removed from the <see langword="Cache" />. If the value in the key parameter is not found, returns <see langword="null" />.</returns>
	public object Remove(string key)
	{
		return Remove(key, CacheItemRemovedReason.Removed, doLock: true, invokeCallback: true);
	}

	internal object Remove(string key, CacheItemRemovedReason reason, bool doLock, bool invokeCallback)
	{
		CacheItem cacheItem = null;
		try
		{
			if (doLock)
			{
				cacheLock.EnterWriteLock();
			}
			cacheItem = RemoveCacheItem(key);
		}
		finally
		{
			if (doLock)
			{
				cacheLock.ExitWriteLock();
			}
		}
		object result = null;
		if (cacheItem != null)
		{
			if (cacheItem.Dependency != null)
			{
				cacheItem.Dependency.SetCache(null);
				cacheItem.Dependency.DependencyChanged -= OnDependencyChanged;
				cacheItem.Dependency.Dispose();
			}
			if (invokeCallback && cacheItem.OnRemoveCallback != null)
			{
				try
				{
					cacheItem.OnRemoveCallback(key, cacheItem.Value, reason);
				}
				catch
				{
				}
			}
			result = cacheItem.Value;
			cacheItem.Value = null;
			cacheItem.Key = null;
			cacheItem.Dependency = null;
			cacheItem.OnRemoveCallback = null;
			cacheItem.OnUpdateCallback = null;
			cacheItem = null;
		}
		return result;
	}

	internal void InvokePrivateCallbacks()
	{
		try
		{
			cacheLock.EnterReadLock();
			cache.InvokePrivateCallbacks();
		}
		finally
		{
			cacheLock.ExitReadLock();
		}
	}

	/// <summary>Retrieves a dictionary enumerator used to iterate through the key settings and their values contained in the cache.</summary>
	/// <returns>An enumerator to iterate through the <see cref="T:System.Web.Caching.Cache" /> object.</returns>
	public IDictionaryEnumerator GetEnumerator()
	{
		List<CacheItem> list = null;
		try
		{
			cacheLock.EnterReadLock();
			list = cache.ToList();
		}
		finally
		{
			cacheLock.ExitReadLock();
		}
		return new CacheItemEnumerator(list);
	}

	/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Web.Caching.Cache" /> object collection.</summary>
	/// <returns>An enumerator that can iterate through the <see cref="T:System.Web.Caching.Cache" /> object collection.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	private void OnDependencyChanged(object o, EventArgs a)
	{
		CheckDependencies();
	}

	private bool NeedsUpdate(CacheItem item, CacheItemUpdateReason reason, bool needLock)
	{
		try
		{
			if (needLock)
			{
				cacheLock.EnterWriteLock();
			}
			if (item == null || item.OnUpdateCallback == null)
			{
				return false;
			}
			string key = item.Key;
			CacheItemUpdateCallback onUpdateCallback = item.OnUpdateCallback;
			onUpdateCallback(key, reason, out var expensiveObject, out var dependency, out var absoluteExpiration, out var slidingExpiration);
			if (expensiveObject == null)
			{
				return false;
			}
			CacheItemPriority priority = item.Priority;
			CacheItemRemovedCallback onRemoveCallback = item.OnRemoveCallback;
			Remove(key, reason switch
			{
				CacheItemUpdateReason.Expired => CacheItemRemovedReason.Expired, 
				CacheItemUpdateReason.DependencyChanged => CacheItemRemovedReason.DependencyChanged, 
				_ => CacheItemRemovedReason.Removed, 
			}, doLock: false, invokeCallback: false);
			Insert(key, expensiveObject, dependency, absoluteExpiration, slidingExpiration, priority, onRemoveCallback, onUpdateCallback, doLock: false);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
		finally
		{
			if (needLock)
			{
				cacheLock.ExitWriteLock();
			}
		}
	}

	private void ExpireItems(object data)
	{
		DateTime now = DateTime.Now;
		CacheItem cacheItem = null;
		expirationTimer.Change(-1, -1);
		try
		{
			cacheLock.EnterWriteLock();
			while (true)
			{
				cacheItem = timedItems.Peek();
				if (cacheItem == null)
				{
					if (timedItems.Count == 0)
					{
						break;
					}
					timedItems.Dequeue();
					continue;
				}
				if (!cacheItem.Disabled && cacheItem.ExpiresAt > now.Ticks)
				{
					break;
				}
				if (cacheItem.Disabled)
				{
					cacheItem = timedItems.Dequeue();
					continue;
				}
				cacheItem = timedItems.Dequeue();
				if (cacheItem != null && !NeedsUpdate(cacheItem, CacheItemUpdateReason.Expired, needLock: false))
				{
					Remove(cacheItem.Key, CacheItemRemovedReason.Expired, doLock: false, invokeCallback: true);
				}
			}
		}
		finally
		{
			cacheLock.ExitWriteLock();
		}
		if (cacheItem != null)
		{
			long num = Math.Max(0L, (long)(cacheItem.AbsoluteExpiration - now).TotalMilliseconds);
			if (num > 0 && (expirationTimerPeriod == 0L || expirationTimerPeriod > num))
			{
				expirationTimerPeriod = num;
				expirationTimer.Change(expirationTimerPeriod, expirationTimerPeriod);
				return;
			}
			if (expirationTimerPeriod > 0)
			{
				return;
			}
		}
		expirationTimer.Change(-1, -1);
		expirationTimerPeriod = 0L;
	}

	internal void CheckDependencies()
	{
		try
		{
			cacheLock.EnterWriteLock();
			List<CacheItem> list = cache.SelectItems(delegate(CacheItem it)
			{
				if (it == null)
				{
					return false;
				}
				return (it.Dependency != null && it.Dependency.HasChanged && !NeedsUpdate(it, CacheItemUpdateReason.DependencyChanged, needLock: false)) ? true : false;
			});
			foreach (CacheItem item in list)
			{
				Remove(item.Key, CacheItemRemovedReason.DependencyChanged, doLock: false, invokeCallback: true);
			}
			list.Clear();
			list.TrimExcess();
		}
		finally
		{
			cacheLock.ExitWriteLock();
		}
	}

	internal DateTime GetKeyLastChange(string key)
	{
		try
		{
			cacheLock.EnterReadLock();
			return cache[key]?.LastChange ?? DateTime.MaxValue;
		}
		finally
		{
			cacheLock.ExitReadLock();
		}
	}
}
