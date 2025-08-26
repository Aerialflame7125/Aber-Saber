namespace System.Web.Caching;

/// <summary>Specifies the reason an item was removed from the <see cref="T:System.Web.Caching.Cache" />.</summary>
public enum CacheItemRemovedReason
{
	/// <summary>The item is removed from the cache by a <see cref="M:System.Web.Caching.Cache.Remove(System.String)" /> method call or by an <see cref="M:System.Web.Caching.Cache.Insert(System.String,System.Object)" /> method call that specified the same key.</summary>
	Removed = 1,
	/// <summary>The item is removed from the cache because it expired.</summary>
	Expired,
	/// <summary>The item is removed from the cache because the system removed it to free memory.</summary>
	Underused,
	/// <summary>The item is removed from the cache because the cache dependency associated with it changed.</summary>
	DependencyChanged
}
