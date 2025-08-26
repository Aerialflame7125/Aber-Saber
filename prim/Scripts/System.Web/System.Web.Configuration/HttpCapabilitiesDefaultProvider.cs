using System.Collections;

namespace System.Web.Configuration;

/// <summary>The default extension of the <see cref="T:System.Web.Configuration.HttpCapabilitiesProvider" /> class that is included with ASP.NET.</summary>
public class HttpCapabilitiesDefaultProvider : HttpCapabilitiesProvider
{
	/// <summary>Gets or sets the length of time in seconds to retain the <see cref="T:System.Web.HttpBrowserCapabilities" /> object in the cache.</summary>
	/// <returns>The length of time in seconds to retain the <see cref="T:System.Web.HttpBrowserCapabilities" /> object in the cache.</returns>
	public TimeSpan CacheTime { get; set; }

	/// <summary>Gets or sets the type of the class that is used to hold the results from parsing the <see langword="browserCap" /> element of the Web.config file.</summary>
	/// <returns>The type of the class that is used to hold the results from parsing the <see langword="browserCaps" /> element of the Web.config file.</returns>
	public Type ResultType { get; set; }

	/// <summary>Gets or sets the number of characters from the user agent string to use for caching of the <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</summary>
	/// <returns>The number of characters from the supplied user agent string to use for caching of the <see cref="T:System.Web.HttpBrowserCapabilities" /> object.</returns>
	public int UserAgentCacheKeyLength { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpCapabilitiesDefaultProvider" /> class.</summary>
	public HttpCapabilitiesDefaultProvider()
	{
		UserAgentCacheKeyLength = 64;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpCapabilitiesDefaultProvider" /> class with the values of the specified instance.</summary>
	/// <param name="parent">The <see cref="T:System.Web.Configuration.HttpCapabilitiesDefaultProvider" /> instance to use for initializing a new instance.</param>
	public HttpCapabilitiesDefaultProvider(HttpCapabilitiesDefaultProvider parent)
	{
		CacheTime = parent.CacheTime;
		ResultType = parent.ResultType;
		UserAgentCacheKeyLength = parent.UserAgentCacheKeyLength;
	}

	/// <summary>Adds an HTTP request string to use to parse browser capability information.</summary>
	/// <param name="variable">The string to use to parse browser capability information.</param>
	public void AddDependency(string variable)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds a search string that modifies a browser definition.</summary>
	/// <param name="ruleList">The search string that modifies a browser definition.</param>
	public virtual void AddRuleList(ArrayList ruleList)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the <see cref="T:System.Web.HttpBrowserCapabilities" /> object for the specified <see cref="T:System.Web.HttpRequest" /> object.</summary>
	/// <param name="request">The <see cref="T:System.Web.HttpRequest" /> object.</param>
	/// <returns>The <see cref="T:System.Web.HttpBrowserCapabilities" /> object for the <see cref="T:System.Web.HttpRequest" /> object that was passed in.</returns>
	public override HttpBrowserCapabilities GetBrowserCapabilities(HttpRequest request)
	{
		return new HttpBrowserCapabilities
		{
			capabilities = HttpCapabilitiesBase.GetConfigCapabilities(null, request).Capabilities
		};
	}
}
