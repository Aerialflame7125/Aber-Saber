namespace System.Web.Caching;

/// <summary>Defines a callback method for notifying applications before a cached item is removed from the cache.</summary>
/// <param name="key">The identifier of the item that is being removed from the cache.</param>
/// <param name="reason">The reason that the item is being removed from the cache.</param>
/// <param name="expensiveObject">When this method returns, contains the cached item object that contains the updates.</param>
/// <param name="dependency">When this method returns, contains the object that defines the dependency between the item object and a file, a cache key, an array of either, or another <see cref="T:System.Web.Caching.CacheDependency" /> object.</param>
/// <param name="absoluteExpiration">When this method returns, contains the time at which the object expired.</param>
/// <param name="slidingExpiration">When this method returns, contains the interval between the time that the object was last accessed and the time at which the object expired.</param>
public delegate void CacheItemUpdateCallback(string key, CacheItemUpdateReason reason, out object expensiveObject, out CacheDependency dependency, out DateTime absoluteExpiration, out TimeSpan slidingExpiration);
