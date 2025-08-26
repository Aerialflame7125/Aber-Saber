namespace System.Web.Caching;

internal sealed class InMemoryOutputCacheProvider : OutputCacheProvider
{
	private const string CACHE_PREFIX = "@InMemoryOCP_";

	public override object Add(string key, object entry, DateTime utcExpiry)
	{
		return HttpRuntime.InternalCache.Add("@InMemoryOCP_" + key, entry, null, utcExpiry.ToLocalTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
	}

	public override object Get(string key)
	{
		return HttpRuntime.InternalCache.Get("@InMemoryOCP_" + key);
	}

	public override void Remove(string key)
	{
		HttpRuntime.InternalCache.Remove("@InMemoryOCP_" + key);
	}

	public override void Set(string key, object entry, DateTime utcExpiry)
	{
		Cache internalCache = HttpRuntime.InternalCache;
		string key2 = "@InMemoryOCP_" + key;
		if (internalCache.Get(key2) != null)
		{
			internalCache.Remove(key2);
		}
		internalCache.Add(key2, entry, null, utcExpiry.ToLocalTime(), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
	}
}
