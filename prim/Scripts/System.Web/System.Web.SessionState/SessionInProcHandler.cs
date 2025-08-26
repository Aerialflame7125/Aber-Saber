using System.Collections.Specialized;
using System.Threading;
using System.Web.Caching;

namespace System.Web.SessionState;

internal class SessionInProcHandler : SessionStateStoreProviderBase
{
	private const string CachePrefix = "@@@InProc@";

	private const int CachePrefixLength = 10;

	private const int lockAcquireTimeout = 30000;

	private CacheItemRemovedCallback removedCB;

	private SessionStateItemExpireCallback expireCallback;

	private HttpStaticObjectsCollection staticObjects;

	public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
	{
		return new SessionStateStoreData(new SessionStateItemCollection(), staticObjects, timeout);
	}

	private void InsertSessionItem(InProcSessionItem item, int timeout, string id)
	{
		if (item != null && !string.IsNullOrEmpty(id))
		{
			HttpRuntime.InternalCache.Insert(id, item, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(timeout), CacheItemPriority.AboveNormal, removedCB);
		}
	}

	private void UpdateSessionItemTimeout(int timeout, string id)
	{
		if (!string.IsNullOrEmpty(id))
		{
			HttpRuntime.InternalCache.SetItemTimeout(id, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(timeout), doLock: true);
		}
	}

	public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
	{
		EnsureGoodId(id, throwOnNull: true);
		InProcSessionItem inProcSessionItem = new InProcSessionItem();
		inProcSessionItem.expiresAt = DateTime.UtcNow.AddMinutes(timeout);
		inProcSessionItem.timeout = timeout;
		InsertSessionItem(inProcSessionItem, timeout, "@@@InProc@" + id);
	}

	public override void Dispose()
	{
	}

	public override void EndRequest(HttpContext context)
	{
		if (staticObjects != null)
		{
			staticObjects.GetObjects().Clear();
			staticObjects = null;
		}
	}

	private SessionStateStoreData GetItemInternal(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions, bool exclusive)
	{
		locked = false;
		lockAge = TimeSpan.MinValue;
		lockId = int.MinValue;
		actions = SessionStateActions.None;
		if (id == null)
		{
			return null;
		}
		Cache internalCache = HttpRuntime.InternalCache;
		string key = "@@@InProc@" + id;
		if (!(internalCache[key] is InProcSessionItem inProcSessionItem))
		{
			return null;
		}
		bool flag = false;
		bool flag2 = false;
		try
		{
			if (inProcSessionItem.rwlock.TryEnterUpgradeableReadLock(30000))
			{
				flag = true;
				if (inProcSessionItem.locked)
				{
					locked = true;
					lockAge = DateTime.UtcNow.Subtract(inProcSessionItem.lockedTime);
					lockId = inProcSessionItem.lockId;
					return null;
				}
				if (exclusive)
				{
					if (!inProcSessionItem.rwlock.TryEnterWriteLock(30000))
					{
						throw new ApplicationException("Failed to acquire lock");
					}
					flag2 = true;
					inProcSessionItem.locked = true;
					inProcSessionItem.lockedTime = DateTime.UtcNow;
					inProcSessionItem.lockId++;
					lockId = inProcSessionItem.lockId;
				}
				if (inProcSessionItem.items == null)
				{
					actions = SessionStateActions.InitializeItem;
					inProcSessionItem.items = new SessionStateItemCollection();
				}
				if (inProcSessionItem.staticItems == null)
				{
					inProcSessionItem.staticItems = staticObjects;
				}
				return new SessionStateStoreData(inProcSessionItem.items, inProcSessionItem.staticItems, inProcSessionItem.timeout);
			}
			throw new ApplicationException("Failed to acquire lock");
		}
		catch
		{
			throw;
		}
		finally
		{
			if (flag2)
			{
				inProcSessionItem.rwlock.ExitWriteLock();
			}
			if (flag)
			{
				inProcSessionItem.rwlock.ExitUpgradeableReadLock();
			}
		}
	}

	public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
	{
		EnsureGoodId(id, throwOnNull: false);
		return GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, exclusive: false);
	}

	public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
	{
		EnsureGoodId(id, throwOnNull: false);
		return GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, exclusive: true);
	}

	public override void Initialize(string name, NameValueCollection config)
	{
		if (string.IsNullOrEmpty(name))
		{
			name = "Session InProc handler";
		}
		removedCB = OnSessionRemoved;
		base.Initialize(name, config);
	}

	public override void InitializeRequest(HttpContext context)
	{
		staticObjects = HttpApplicationFactory.ApplicationState.SessionObjects.Clone();
	}

	public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
	{
		EnsureGoodId(id, throwOnNull: true);
		string key = "@@@InProc@" + id;
		if (!(HttpRuntime.InternalCache[key] is InProcSessionItem inProcSessionItem) || lockId == null || lockId.GetType() != typeof(int) || inProcSessionItem.lockId != (int)lockId)
		{
			return;
		}
		bool flag = false;
		ReaderWriterLockSlim readerWriterLockSlim = null;
		try
		{
			readerWriterLockSlim = inProcSessionItem.rwlock;
			if (readerWriterLockSlim != null && readerWriterLockSlim.TryEnterWriteLock(30000))
			{
				flag = true;
				inProcSessionItem.locked = false;
				return;
			}
			throw new ApplicationException("Failed to acquire lock");
		}
		catch
		{
			throw;
		}
		finally
		{
			if (flag)
			{
				readerWriterLockSlim?.ExitWriteLock();
			}
		}
	}

	public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
	{
		EnsureGoodId(id, throwOnNull: true);
		string key = "@@@InProc@" + id;
		Cache internalCache = HttpRuntime.InternalCache;
		if (!(internalCache[key] is InProcSessionItem inProcSessionItem) || lockId == null || lockId.GetType() != typeof(int) || inProcSessionItem.lockId != (int)lockId)
		{
			return;
		}
		bool flag = false;
		ReaderWriterLockSlim readerWriterLockSlim = null;
		try
		{
			readerWriterLockSlim = inProcSessionItem.rwlock;
			if (readerWriterLockSlim != null && readerWriterLockSlim.TryEnterWriteLock(30000))
			{
				flag = true;
				internalCache.Remove(key);
				return;
			}
			throw new ApplicationException("Failed to acquire lock after");
		}
		catch
		{
			throw;
		}
		finally
		{
			if (flag)
			{
				readerWriterLockSlim.ExitWriteLock();
			}
		}
	}

	public override void ResetItemTimeout(HttpContext context, string id)
	{
		EnsureGoodId(id, throwOnNull: true);
		string text = "@@@InProc@" + id;
		if (!(HttpRuntime.InternalCache[text] is InProcSessionItem inProcSessionItem))
		{
			return;
		}
		bool flag = false;
		ReaderWriterLockSlim readerWriterLockSlim = null;
		try
		{
			readerWriterLockSlim = inProcSessionItem.rwlock;
			if (readerWriterLockSlim != null && readerWriterLockSlim.TryEnterWriteLock(30000))
			{
				flag = true;
				inProcSessionItem.resettingTimeout = true;
				UpdateSessionItemTimeout(inProcSessionItem.timeout, text);
				return;
			}
			throw new ApplicationException("Failed to acquire lock after");
		}
		catch
		{
			throw;
		}
		finally
		{
			if (flag)
			{
				readerWriterLockSlim?.ExitWriteLock();
			}
		}
	}

	public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
	{
		EnsureGoodId(id, throwOnNull: true);
		string text = "@@@InProc@" + id;
		Cache internalCache = HttpRuntime.InternalCache;
		InProcSessionItem inProcSessionItem = internalCache[text] as InProcSessionItem;
		ISessionStateItemCollection items = null;
		int num = 20;
		HttpStaticObjectsCollection staticItems = null;
		if (item != null)
		{
			items = item.Items;
			num = item.Timeout;
			staticItems = item.StaticObjects;
		}
		if (newItem || inProcSessionItem == null)
		{
			inProcSessionItem = new InProcSessionItem();
			inProcSessionItem.timeout = num;
			inProcSessionItem.expiresAt = DateTime.UtcNow.AddMinutes(num);
			if (lockId.GetType() == typeof(int))
			{
				inProcSessionItem.lockId = (int)lockId;
			}
		}
		else
		{
			if (lockId == null || lockId.GetType() != typeof(int) || inProcSessionItem.lockId != (int)lockId)
			{
				return;
			}
			inProcSessionItem.resettingTimeout = true;
			internalCache.Remove(text);
		}
		bool flag = false;
		ReaderWriterLockSlim readerWriterLockSlim = null;
		try
		{
			readerWriterLockSlim = inProcSessionItem.rwlock;
			if (readerWriterLockSlim != null && readerWriterLockSlim.TryEnterWriteLock(30000))
			{
				flag = true;
			}
			else if (readerWriterLockSlim != null)
			{
				throw new ApplicationException("Failed to acquire lock");
			}
			if (inProcSessionItem.resettingTimeout)
			{
				UpdateSessionItemTimeout(num, text);
				return;
			}
			inProcSessionItem.locked = false;
			inProcSessionItem.items = items;
			inProcSessionItem.staticItems = staticItems;
			InsertSessionItem(inProcSessionItem, num, text);
		}
		catch
		{
			throw;
		}
		finally
		{
			if (flag)
			{
				readerWriterLockSlim?.ExitWriteLock();
			}
		}
	}

	public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
	{
		this.expireCallback = expireCallback;
		return true;
	}

	private void EnsureGoodId(string id, bool throwOnNull)
	{
		if (id == null)
		{
			if (throwOnNull)
			{
				throw new HttpException("Session ID is invalid");
			}
		}
		else if (id.Length > SessionIDManager.SessionIDMaxLength)
		{
			throw new HttpException("Session ID too long");
		}
	}

	private void OnSessionRemoved(string key, object value, CacheItemRemovedReason reason)
	{
		if (expireCallback != null)
		{
			if (key.StartsWith("@@@InProc@", StringComparison.OrdinalIgnoreCase))
			{
				key = key.Substring(10);
			}
			if (value is SessionStateStoreData)
			{
				expireCallback(key, (SessionStateStoreData)value);
			}
			else if (value is InProcSessionItem)
			{
				InProcSessionItem inProcSessionItem = (InProcSessionItem)value;
				if (inProcSessionItem.resettingTimeout)
				{
					inProcSessionItem.resettingTimeout = false;
					return;
				}
				expireCallback(key, new SessionStateStoreData(inProcSessionItem.items, inProcSessionItem.staticItems, inProcSessionItem.timeout));
				inProcSessionItem.Dispose();
			}
			else
			{
				expireCallback(key, null);
			}
		}
		else if (value is InProcSessionItem)
		{
			InProcSessionItem inProcSessionItem2 = (InProcSessionItem)value;
			if (inProcSessionItem2.resettingTimeout)
			{
				inProcSessionItem2.resettingTimeout = false;
			}
			else
			{
				inProcSessionItem2.Dispose();
			}
		}
	}
}
