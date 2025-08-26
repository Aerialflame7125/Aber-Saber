using System.Text;
using System.Web.Caching;

namespace System.Web.UI.WebControls;

internal class DataSourceCacheManager
{
	private readonly int cacheDuration;

	private readonly string cacheKeyDependency;

	private readonly string controlID;

	private readonly DataSourceCacheExpiry cacheExpirationPolicy;

	private readonly Control owner;

	private readonly HttpContext context;

	private static Cache DataCache
	{
		get
		{
			if (HttpContext.Current != null)
			{
				return HttpContext.Current.InternalCache;
			}
			throw new InvalidOperationException("HttpContext.Current is null.");
		}
	}

	internal DataSourceCacheManager(int cacheDuration, string cacheKeyDependency, DataSourceCacheExpiry cacheExpirationPolicy, Control owner, HttpContext context)
	{
		this.cacheDuration = cacheDuration;
		this.cacheKeyDependency = cacheKeyDependency;
		this.cacheExpirationPolicy = cacheExpirationPolicy;
		controlID = owner.UniqueID;
		this.owner = owner;
		this.context = context;
		if (DataCache[controlID] == null)
		{
			DataCache[controlID] = new object();
		}
	}

	internal void Expire()
	{
		DataCache[controlID] = new object();
	}

	internal object GetCachedObject(string methodName, ParameterCollection parameters)
	{
		return DataCache[GetKeyFromParameters(methodName, parameters)];
	}

	internal void SetCachedObject(string methodName, ParameterCollection parameters, object o)
	{
		if (o == null)
		{
			return;
		}
		string keyFromParameters = GetKeyFromParameters(methodName, parameters);
		if (DataCache[keyFromParameters] != null)
		{
			DataCache.Remove(keyFromParameters);
		}
		DateTime absoluteExpiration = Cache.NoAbsoluteExpiration;
		TimeSpan slidingExpiration = Cache.NoSlidingExpiration;
		if (cacheDuration > 0)
		{
			if (cacheExpirationPolicy == DataSourceCacheExpiry.Absolute)
			{
				absoluteExpiration = DateTime.Now.AddSeconds(cacheDuration);
			}
			else
			{
				slidingExpiration = new TimeSpan(0, 0, cacheDuration);
			}
		}
		string[] cachekeys = ((cacheKeyDependency.Length <= 0) ? new string[0] : new string[1] { cacheKeyDependency });
		DataCache.Add(keyFromParameters, o, new CacheDependency(new string[0], cachekeys), absoluteExpiration, slidingExpiration, CacheItemPriority.Normal, null);
	}

	private string GetKeyFromParameters(string methodName, ParameterCollection parameters)
	{
		StringBuilder stringBuilder = new StringBuilder(methodName);
		if (owner != null)
		{
			stringBuilder.Append(owner.ID);
		}
		for (int i = 0; i < parameters.Count; i++)
		{
			stringBuilder.Append(parameters[i].Name);
			stringBuilder.Append(parameters[i].GetValue(context, owner));
		}
		return stringBuilder.ToString();
	}
}
