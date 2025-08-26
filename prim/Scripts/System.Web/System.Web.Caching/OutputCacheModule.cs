using System.Collections;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Text;
using System.Web.Compilation;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Caching;

internal sealed class OutputCacheModule : IHttpModule
{
	private CacheItemRemovedCallback response_removed;

	private static object keysCacheLock = new object();

	private Dictionary<string, string> keysCache;

	private Dictionary<string, string> entriesToInvalidate;

	private OutputCacheProvider FindCacheProvider(HttpApplication app)
	{
		HttpContext current = HttpContext.Current;
		if (app == null)
		{
			app = current?.ApplicationInstance;
			if (app == null)
			{
				throw new InvalidOperationException("Unable to find output cache provider.");
			}
		}
		string outputCacheProviderName = app.GetOutputCacheProviderName(current);
		if (string.IsNullOrEmpty(outputCacheProviderName))
		{
			throw new ProviderException("Invalid OutputCacheProvider name. Name must not be null or an empty string.");
		}
		return OutputCache.GetProvider(outputCacheProviderName) ?? throw new ProviderException($"OutputCacheProvider named '{outputCacheProviderName}' cannot be found.");
	}

	public void Dispose()
	{
	}

	public void Init(HttpApplication context)
	{
		context.ResolveRequestCache += OnResolveRequestCache;
		context.UpdateRequestCache += OnUpdateRequestCache;
		response_removed = OnRawResponseRemoved;
	}

	private void OnBuildManagerRemoveEntry(BuildManagerRemoveEntryEventArgs args)
	{
		string entryName = args.EntryName;
		HttpContext context = args.Context;
		string value;
		lock (keysCacheLock)
		{
			if (!keysCache.TryGetValue(entryName, out value))
			{
				return;
			}
			keysCache.Remove(entryName);
			if (context == null)
			{
				if (entriesToInvalidate == null)
				{
					entriesToInvalidate = new Dictionary<string, string>(StringComparer.Ordinal);
					entriesToInvalidate.Add(entryName, value);
					return;
				}
				if (!entriesToInvalidate.ContainsKey(entryName))
				{
					entriesToInvalidate.Add(entryName, value);
					return;
				}
			}
		}
		OutputCacheProvider outputCacheProvider = FindCacheProvider(context?.ApplicationInstance);
		outputCacheProvider.Remove(entryName);
		if (!string.IsNullOrEmpty(value))
		{
			outputCacheProvider.Remove(value);
		}
	}

	private void OnResolveRequestCache(object o, EventArgs args)
	{
		HttpApplication httpApplication = o as HttpApplication;
		HttpContext httpContext = httpApplication?.Context;
		if (httpContext == null)
		{
			return;
		}
		OutputCacheProvider outputCacheProvider = FindCacheProvider(httpApplication);
		string filePath = httpContext.Request.FilePath;
		if (!(outputCacheProvider.Get(filePath) is CachedVaryBy cachedVaryBy))
		{
			return;
		}
		string text = cachedVaryBy.CreateKey(filePath, httpContext);
		if (!(outputCacheProvider.Get(text) is CachedRawResponse cachedRawResponse))
		{
			return;
		}
		lock (keysCacheLock)
		{
			if (entriesToInvalidate != null && entriesToInvalidate.TryGetValue(filePath, out var value) && string.Compare(value, text, StringComparison.Ordinal) == 0)
			{
				outputCacheProvider.Remove(filePath);
				outputCacheProvider.Remove(text);
				entriesToInvalidate.Remove(filePath);
				return;
			}
		}
		ArrayList validationCallbacks = cachedRawResponse.Policy.ValidationCallbacks;
		if (validationCallbacks != null && validationCallbacks.Count > 0)
		{
			bool flag = true;
			bool flag2 = false;
			foreach (Pair item in validationCallbacks)
			{
				HttpCacheValidateHandler httpCacheValidateHandler = (HttpCacheValidateHandler)item.First;
				object second = item.Second;
				HttpValidationStatus validationStatus = HttpValidationStatus.Valid;
				try
				{
					httpCacheValidateHandler(httpContext, second, ref validationStatus);
				}
				catch
				{
					flag = false;
					break;
				}
				switch (validationStatus)
				{
				case HttpValidationStatus.Invalid:
					flag = false;
					goto end_IL_0140;
				case HttpValidationStatus.IgnoreThisRequest:
					flag2 = true;
					break;
				}
				continue;
				end_IL_0140:
				break;
			}
			if (!flag)
			{
				OnRawResponseRemoved(text, cachedRawResponse, CacheItemRemovedReason.Removed);
				return;
			}
			if (flag2)
			{
				return;
			}
		}
		HttpResponse response = httpContext.Response;
		response.ClearContent();
		IList data = cachedRawResponse.GetData();
		if (data != null)
		{
			Encoding responseEncoding = WebEncoding.ResponseEncoding;
			foreach (CachedRawResponse.DataItem item2 in data)
			{
				if (item2.Length > 0)
				{
					response.BinaryWrite(item2.Buffer, 0, (int)item2.Length);
				}
				else if (item2.Callback != null)
				{
					string text2 = item2.Callback(httpContext);
					if (text2 != null && text2.Length != 0)
					{
						byte[] bytes = responseEncoding.GetBytes(text2);
						response.BinaryWrite(bytes, 0, bytes.Length);
					}
				}
			}
		}
		response.ClearHeaders();
		response.SetCachedHeaders(cachedRawResponse.Headers);
		response.StatusCode = cachedRawResponse.StatusCode;
		response.StatusDescription = cachedRawResponse.StatusDescription;
		httpApplication.CompleteRequest();
	}

	private void OnUpdateRequestCache(object o, EventArgs args)
	{
		HttpApplication httpApplication = o as HttpApplication;
		HttpContext httpContext = httpApplication?.Context;
		HttpResponse httpResponse = httpContext?.Response;
		if (httpResponse != null && httpResponse.IsCached && httpResponse.StatusCode == 200 && !httpContext.Trace.IsEnabled)
		{
			DoCacheInsert(httpContext, httpApplication, httpResponse);
		}
	}

	private void DoCacheInsert(HttpContext context, HttpApplication app, HttpResponse response)
	{
		string filePath = context.Request.FilePath;
		OutputCacheProvider outputCacheProvider = FindCacheProvider(app);
		CachedVaryBy cachedVaryBy = outputCacheProvider.Get(filePath) as CachedVaryBy;
		CachedRawResponse cachedRawResponse = null;
		bool flag = true;
		string text = null;
		string value = null;
		HttpCachePolicy cache = response.Cache;
		if (cachedVaryBy == null)
		{
			cachedVaryBy = new CachedVaryBy(cache, filePath);
			outputCacheProvider.Add(filePath, cachedVaryBy, Cache.NoAbsoluteExpiration);
			flag = false;
			text = filePath;
		}
		string text2 = cachedVaryBy.CreateKey(filePath, context);
		if (flag)
		{
			cachedRawResponse = outputCacheProvider.Get(text2) as CachedRawResponse;
		}
		if (cachedRawResponse == null)
		{
			CachedRawResponse cachedResponse = response.GetCachedResponse();
			if (cachedResponse != null)
			{
				string[] cachekeys = new string[1] { filePath };
				cachedResponse.VaryBy = cachedVaryBy;
				cachedVaryBy.ItemList.Add(text2);
				TimeSpan timeSpan;
				DateTime absoluteExpiration;
				DateTime utcExpiry;
				if (cache.Sliding)
				{
					timeSpan = TimeSpan.FromSeconds(cache.Duration);
					absoluteExpiration = Cache.NoAbsoluteExpiration;
					utcExpiry = DateTime.UtcNow + timeSpan;
				}
				else
				{
					timeSpan = Cache.NoSlidingExpiration;
					absoluteExpiration = cache.Expires;
					utcExpiry = absoluteExpiration.ToUniversalTime();
				}
				outputCacheProvider.Set(text2, cachedResponse, utcExpiry);
				HttpRuntime.InternalCache.Insert(text2, cachedResponse, new CacheDependency(null, cachekeys), absoluteExpiration, timeSpan, CacheItemPriority.Normal, response_removed);
				value = text2;
			}
		}
		if (text == null)
		{
			return;
		}
		lock (keysCacheLock)
		{
			if (keysCache == null)
			{
				BuildManager.RemoveEntry += OnBuildManagerRemoveEntry;
				keysCache = new Dictionary<string, string>(StringComparer.Ordinal);
				keysCache.Add(text, value);
			}
			else if (!keysCache.ContainsKey(text))
			{
				keysCache.Add(text, value);
			}
		}
	}

	private void OnRawResponseRemoved(string key, object value, CacheItemRemovedReason reason)
	{
		CachedVaryBy cachedVaryBy = ((value is CachedRawResponse cachedRawResponse) ? cachedRawResponse.VaryBy : null);
		if (cachedVaryBy != null)
		{
			List<string> itemList = cachedVaryBy.ItemList;
			OutputCacheProvider outputCacheProvider = FindCacheProvider(null);
			itemList.Remove(key);
			outputCacheProvider.Remove(key);
			if (itemList.Count == 0)
			{
				outputCacheProvider.Remove(cachedVaryBy.Key);
			}
		}
	}
}
