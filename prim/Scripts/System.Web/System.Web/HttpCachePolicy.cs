using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.UI;
using System.Web.Util;

namespace System.Web;

/// <summary>Contains methods for setting cache-specific HTTP headers and for controlling the ASP.NET page output cache.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpCachePolicy
{
	private HttpCacheVaryByContentEncodings vary_by_content_encodings = new HttpCacheVaryByContentEncodings();

	private HttpCacheVaryByHeaders vary_by_headers = new HttpCacheVaryByHeaders();

	private HttpCacheVaryByParams vary_by_params = new HttpCacheVaryByParams();

	private ArrayList validation_callbacks;

	private StringBuilder cache_extension;

	internal HttpCacheability Cacheability;

	private string etag;

	private bool etag_from_file_dependencies;

	private bool last_modified_from_file_dependencies;

	internal bool have_expire_date;

	internal DateTime expire_date;

	internal bool have_last_modified;

	internal DateTime last_modified;

	private HttpCacheRevalidation revalidation;

	private string vary_by_custom;

	private bool HaveMaxAge;

	private TimeSpan MaxAge;

	private bool HaveProxyMaxAge;

	private TimeSpan ProxyMaxAge;

	private ArrayList fields;

	private bool sliding_expiration;

	private int duration;

	private bool allow_response_in_browser_history;

	private bool allow_server_caching = true;

	private bool set_no_store;

	private bool set_no_transform;

	private bool valid_until_expires;

	private bool omit_vary_star;

	/// <summary>Gets the list of <see langword="Content-Encoding" /> headers that will be used to vary the output cache.</summary>
	/// <returns>An object that specifies which <see langword="Content-Encoding" /> headers are used to select the cached response.</returns>
	public HttpCacheVaryByContentEncodings VaryByContentEncodings => vary_by_content_encodings;

	/// <summary>Gets the list of all HTTP headers that will be used to vary cache output.</summary>
	/// <returns>An <see cref="T:System.Web.HttpCacheVaryByHeaders" /> that specifies which HTTP headers are used to select the cached response.</returns>
	public HttpCacheVaryByHeaders VaryByHeaders => vary_by_headers;

	/// <summary>Gets the list of parameters received by an HTTP <see langword="GET" /> or HTTP <see langword="POST" /> that affect caching.</summary>
	/// <returns>An <see cref="T:System.Web.HttpCacheVaryByParams" /> that specifies which cache-control headers are used to select the cached response.</returns>
	public HttpCacheVaryByParams VaryByParams => vary_by_params;

	internal bool AllowServerCaching => allow_server_caching;

	internal int Duration
	{
		get
		{
			return duration;
		}
		set
		{
			duration = value;
		}
	}

	internal bool Sliding => sliding_expiration;

	internal DateTime Expires => expire_date;

	internal ArrayList ValidationCallbacks => validation_callbacks;

	internal bool OmitVaryStar => omit_vary_star;

	internal bool ValidUntilExpires => valid_until_expires;

	internal HttpCachePolicy()
	{
	}

	internal int ExpireMinutes()
	{
		if (!have_expire_date)
		{
			return 0;
		}
		return (expire_date - DateTime.Now).Minutes;
	}

	/// <summary>Registers a validation callback for the current response.</summary>
	/// <param name="handler">The <see cref="T:System.Web.HttpCacheValidateHandler" /> value. </param>
	/// <param name="data">The arbitrary user-supplied data that is passed back to the <see cref="M:System.Web.HttpCachePolicy.AddValidationCallback(System.Web.HttpCacheValidateHandler,System.Object)" /> delegate. </param>
	/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="handler" /> is <see langword="null" />. </exception>
	public void AddValidationCallback(HttpCacheValidateHandler handler, object data)
	{
		if (handler == null)
		{
			throw new ArgumentNullException("handler");
		}
		if (validation_callbacks == null)
		{
			validation_callbacks = new ArrayList();
		}
		validation_callbacks.Add(new Pair(handler, data));
	}

	/// <summary>Appends the specified text to the <see langword="Cache-Control" /> HTTP header.</summary>
	/// <param name="extension">The text to append to the <see langword="Cache-Control" /> header. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="extension" /> is <see langword="null" />. </exception>
	public void AppendCacheExtension(string extension)
	{
		if (extension == null)
		{
			throw new ArgumentNullException("extension");
		}
		if (cache_extension == null)
		{
			cache_extension = new StringBuilder(extension);
		}
		else
		{
			cache_extension.Append(", " + extension);
		}
	}

	/// <summary>Sets the <see langword="Cache-Control" /> header to one of the values of <see cref="T:System.Web.HttpCacheability" />.</summary>
	/// <param name="cacheability">An <see cref="T:System.Web.HttpCacheability" /> enumeration value. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="cacheability" /> is not one of the enumeration values. </exception>
	public void SetCacheability(HttpCacheability cacheability)
	{
		if (cacheability < HttpCacheability.NoCache || cacheability > HttpCacheability.ServerAndPrivate)
		{
			throw new ArgumentOutOfRangeException("cacheability");
		}
		if (Cacheability <= (HttpCacheability)0 || cacheability <= Cacheability)
		{
			Cacheability = cacheability;
		}
	}

	/// <summary>Sets the <see langword="Cache-Control" /> header to one of the values of <see cref="T:System.Web.HttpCacheability" /> and appends an extension to the directive.</summary>
	/// <param name="cacheability">The <see cref="T:System.Web.HttpCacheability" /> enumeration value to set the header to. </param>
	/// <param name="field">The cache control extension to add to the header. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="field" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="cacheability" /> is not <see cref="F:System.Web.HttpCacheability.Private" /> or <see cref="F:System.Web.HttpCacheability.NoCache" />. </exception>
	public void SetCacheability(HttpCacheability cacheability, string field)
	{
		if (field == null)
		{
			throw new ArgumentNullException("field");
		}
		if (cacheability != HttpCacheability.NoCache && cacheability != HttpCacheability.Private)
		{
			throw new ArgumentException("Must be NoCache or Private", "cacheability");
		}
		if (fields == null)
		{
			fields = new ArrayList();
		}
		fields.Add(new Pair(cacheability, field));
	}

	/// <summary>Sets the <see langword="ETag" /> HTTP header to the specified string.</summary>
	/// <param name="etag">The text to use for the <see langword="ETag" /> header. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="etag" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <see langword="ETag" /> header has already been set. - or -The <see cref="M:System.Web.HttpCachePolicy.SetETagFromFileDependencies" /> has already been called.</exception>
	public void SetETag(string etag)
	{
		if (etag == null)
		{
			throw new ArgumentNullException("etag");
		}
		if (this.etag != null)
		{
			throw new InvalidOperationException("The ETag header has already been set");
		}
		if (etag_from_file_dependencies)
		{
			throw new InvalidOperationException("SetEtagFromFileDependencies has already been called");
		}
		this.etag = etag;
	}

	/// <summary>Sets the <see langword="ETag" /> HTTP header based on the time stamps of the handler's file dependencies.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see langword="ETag" /> header has already been set. </exception>
	public void SetETagFromFileDependencies()
	{
		if (etag != null)
		{
			throw new InvalidOperationException("The ETag header has already been set");
		}
		etag_from_file_dependencies = true;
	}

	/// <summary>Sets the <see langword="Expires" /> HTTP header to an absolute date and time.</summary>
	/// <param name="date">The absolute <see cref="T:System.DateTime" /> value to set the <see langword="Expires" /> header to. </param>
	public void SetExpires(DateTime date)
	{
		if (!have_expire_date || !(date > expire_date))
		{
			have_expire_date = true;
			expire_date = date;
		}
	}

	/// <summary>Sets the <see langword="Last-Modified" /> HTTP header to the <see cref="T:System.DateTime" /> value supplied.</summary>
	/// <param name="date">The new <see cref="T:System.DateTime" /> value for the <see langword="Last-Modified" /> header. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="date" /> is later than the current <see langword="DateTime" />. </exception>
	public void SetLastModified(DateTime date)
	{
		if (date > DateTime.Now)
		{
			throw new ArgumentOutOfRangeException("date");
		}
		if (!have_last_modified || !(date < last_modified))
		{
			have_last_modified = true;
			last_modified = date;
		}
	}

	/// <summary>Sets the <see langword="Last-Modified" /> HTTP header based on the time stamps of the handler's file dependencies.</summary>
	public void SetLastModifiedFromFileDependencies()
	{
		last_modified_from_file_dependencies = true;
	}

	/// <summary>Sets the <see langword="Cache-Control: max-age" /> HTTP header based on the specified time span.</summary>
	/// <param name="delta">The time span used to set the <see langword="Cache" /> - <see langword="Control: max-age" /> header. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="delta" /> is less than 0 or greater than one year. </exception>
	public void SetMaxAge(TimeSpan delta)
	{
		if (delta < TimeSpan.Zero)
		{
			throw new ArgumentOutOfRangeException("delta");
		}
		if (!HaveMaxAge || !(MaxAge < delta))
		{
			MaxAge = delta;
			HaveMaxAge = true;
		}
	}

	/// <summary>Stops all origin-server caching for the current response.</summary>
	public void SetNoServerCaching()
	{
		allow_server_caching = false;
	}

	/// <summary>Sets the <see langword="Cache-Control: no-store" /> HTTP header.</summary>
	public void SetNoStore()
	{
		set_no_store = true;
	}

	/// <summary>Sets the <see langword="Cache-Control: no-transform" /> HTTP header.</summary>
	public void SetNoTransforms()
	{
		set_no_transform = true;
	}

	/// <summary>Sets the <see langword="Cache-Control: s-maxage" /> HTTP header based on the specified time span.</summary>
	/// <param name="delta">The time span used to set the <see langword="Cache-Control: s-maxage" /> header. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="delta" /> is less than 0. </exception>
	public void SetProxyMaxAge(TimeSpan delta)
	{
		if (delta < TimeSpan.Zero)
		{
			throw new ArgumentOutOfRangeException("delta");
		}
		if (!HaveProxyMaxAge || !(ProxyMaxAge < delta))
		{
			ProxyMaxAge = delta;
			HaveProxyMaxAge = true;
		}
	}

	/// <summary>Sets the <see langword="Cache-Control" /> HTTP header to either the <see langword="must-revalidate" /> or the <see langword="proxy-revalidate" /> directives based on the supplied enumeration value.</summary>
	/// <param name="revalidation">The <see cref="T:System.Web.HttpCacheRevalidation" /> enumeration value to set the <see langword="Cache-Control" /> header to. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="revalidation" /> is not one of the enumeration values. </exception>
	public void SetRevalidation(HttpCacheRevalidation revalidation)
	{
		if (revalidation < HttpCacheRevalidation.AllCaches || revalidation > HttpCacheRevalidation.None)
		{
			throw new ArgumentOutOfRangeException("revalidation");
		}
		if (this.revalidation > revalidation)
		{
			this.revalidation = revalidation;
		}
	}

	/// <summary>Sets cache expiration to from absolute to sliding.</summary>
	/// <param name="slide">
	///       <see langword="true" /> or <see langword="false" />. </param>
	public void SetSlidingExpiration(bool slide)
	{
		sliding_expiration = slide;
	}

	/// <summary>Specifies whether the ASP.NET cache should ignore HTTP <see langword="Cache-Control" /> headers sent by the client that invalidate the cache.</summary>
	/// <param name="validUntilExpires">
	///       <see langword="true" /> if the cache ignores <see langword="Cache-Control" /> invalidation headers; otherwise, <see langword="false" />. </param>
	public void SetValidUntilExpires(bool validUntilExpires)
	{
		valid_until_expires = validUntilExpires;
	}

	/// <summary>Specifies a custom text string to vary cached output responses by.</summary>
	/// <param name="custom">The text string to vary cached output by. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="custom" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.HttpCachePolicy.SetVaryByCustom(System.String)" /> method has already been called. </exception>
	public void SetVaryByCustom(string custom)
	{
		if (custom == null)
		{
			throw new ArgumentNullException("custom");
		}
		if (vary_by_custom != null)
		{
			throw new InvalidOperationException("VaryByCustom has already been set.");
		}
		vary_by_custom = custom;
	}

	/// <summary>Gets the custom string that is used to vary the HTTP caching.</summary>
	/// <returns>The custom string for varying the HTTP caching.</returns>
	internal string GetVaryByCustom()
	{
		return vary_by_custom;
	}

	/// <summary>Makes the response is available in the client browser History cache, regardless of the <see cref="T:System.Web.HttpCacheability" /> setting made on the server, when the <paramref name="allow" /> parameter is <see langword="true" />.</summary>
	/// <param name="allow">
	///       <see langword="true" /> to direct the client browser to store responses in the History folder; otherwise <see langword="false" />. The default is <see langword="false" />. </param>
	public void SetAllowResponseInBrowserHistory(bool allow)
	{
		if (Cacheability == HttpCacheability.NoCache || Cacheability == HttpCacheability.Server)
		{
			allow_response_in_browser_history = allow;
		}
	}

	internal void SetHeaders(HttpResponse response, NameValueCollection headers)
	{
		bool flag = false;
		string text = null;
		switch (Cacheability)
		{
		case HttpCacheability.Public:
			text = "public";
			break;
		case HttpCacheability.NoCache:
		case HttpCacheability.Server:
			flag = true;
			text = "no-cache";
			break;
		default:
			text = "private";
			break;
		}
		if (flag)
		{
			response.CacheControl = text;
			if (!allow_response_in_browser_history)
			{
				headers.Add("Expires", "-1");
				headers.Add("Pragma", "no-cache");
			}
		}
		else
		{
			if (HaveMaxAge)
			{
				text = text + ", max-age=" + (long)MaxAge.TotalSeconds;
			}
			if (have_expire_date)
			{
				string value = TimeUtil.ToUtcTimeString(expire_date);
				headers.Add("Expires", value);
			}
		}
		if (set_no_store)
		{
			text += ", no-store";
		}
		if (set_no_transform)
		{
			text += ", no-transform";
		}
		if (cache_extension != null && cache_extension.Length > 0)
		{
			if (!string.IsNullOrEmpty(text))
			{
				text += ", ";
			}
			text += cache_extension.ToString();
		}
		headers.Add("Cache-Control", text);
		if (last_modified_from_file_dependencies || etag_from_file_dependencies)
		{
			HeadersFromFileDependencies(response);
		}
		if (etag != null)
		{
			headers.Add("ETag", etag);
		}
		if (have_last_modified)
		{
			headers.Add("Last-Modified", TimeUtil.ToUtcTimeString(last_modified));
		}
		if (!vary_by_params.IgnoreParams)
		{
			string responseHeaderValue = vary_by_params.GetResponseHeaderValue();
			if (responseHeaderValue != null)
			{
				headers.Add("Vary", responseHeaderValue);
			}
		}
	}

	private void HeadersFromFileDependencies(HttpResponse response)
	{
		string[] fileDependencies = response.FileDependencies;
		if (fileDependencies == null || fileDependencies.Length == 0)
		{
			return;
		}
		bool flag = etag != null && etag_from_file_dependencies;
		if (!flag && !last_modified_from_file_dependencies)
		{
			return;
		}
		DateTime dateTime = DateTime.MinValue;
		StringBuilder stringBuilder = new StringBuilder();
		string[] array = fileDependencies;
		foreach (string path in array)
		{
			if (File.Exists(path))
			{
				DateTime lastWriteTime;
				try
				{
					lastWriteTime = File.GetLastWriteTime(path);
				}
				catch
				{
					continue;
				}
				if (last_modified_from_file_dependencies && lastWriteTime > dateTime)
				{
					dateTime = lastWriteTime;
				}
				if (flag)
				{
					stringBuilder.AppendFormat("{0}", lastWriteTime.Ticks.ToString("x"));
				}
			}
		}
		if (last_modified_from_file_dependencies && dateTime > DateTime.MinValue)
		{
			last_modified = dateTime;
			have_last_modified = true;
		}
		if (flag && stringBuilder.Length > 0)
		{
			etag = stringBuilder.ToString();
		}
	}

	/// <summary>Specifies whether the response should contain the <see langword="vary:*" /> header when varying by parameters.</summary>
	/// <param name="omit">
	///       <see langword="true" /> to direct the <see cref="T:System.Web.HttpCachePolicy" /> to not use the * value for its <see cref="P:System.Web.HttpCachePolicy.VaryByHeaders" /> property; otherwise, <see langword="false" />.</param>
	public void SetOmitVaryStar(bool omit)
	{
		omit_vary_star = omit;
	}
}
