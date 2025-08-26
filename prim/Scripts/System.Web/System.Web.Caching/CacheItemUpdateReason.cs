namespace System.Web.Caching;

/// <summary>Specifies the reason that a cached item is being removed from the <see cref="T:System.Web.Caching.Cache" /> object.</summary>
public enum CacheItemUpdateReason
{
	/// <summary>Specifies that the item is being removed from the cache because the absolute or sliding expiration interval expired.</summary>
	Expired = 1,
	/// <summary>Specifies that the item is being removed from the cache because the associated <see cref="T:System.Web.Caching.CacheDependency" /> object changed.</summary>
	DependencyChanged
}
