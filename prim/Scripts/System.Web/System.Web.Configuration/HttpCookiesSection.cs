using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures properties for cookies used by a Web application.</summary>
public sealed class HttpCookiesSection : ConfigurationSection
{
	private static ConfigurationProperty domainProp;

	private static ConfigurationProperty httpOnlyCookiesProp;

	private static ConfigurationProperty requireSSLProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the cookie domain name.</summary>
	/// <returns>The cookie domain name. </returns>
	[ConfigurationProperty("domain", DefaultValue = "")]
	public string Domain
	{
		get
		{
			return (string)base[domainProp];
		}
		set
		{
			base[domainProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the support for the browser's <see langword="HttpOnly" /> cookie is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if support for the <see langword="HttpOnly" /> cookie is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("httpOnlyCookies", DefaultValue = "False")]
	public bool HttpOnlyCookies
	{
		get
		{
			return (bool)base[httpOnlyCookiesProp];
		}
		set
		{
			base[httpOnlyCookiesProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether Secure Sockets Layer (SSL) communication is required.</summary>
	/// <returns>
	///     <see langword="true" /> if SSL is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("requireSSL", DefaultValue = "False")]
	public bool RequireSSL
	{
		get
		{
			return (bool)base[requireSSLProp];
		}
		set
		{
			base[requireSSLProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static HttpCookiesSection()
	{
		domainProp = new ConfigurationProperty("domain", typeof(string), "");
		httpOnlyCookiesProp = new ConfigurationProperty("httpOnlyCookies", typeof(bool), false);
		requireSSLProp = new ConfigurationProperty("requireSSL", typeof(bool), false);
		properties = new ConfigurationPropertyCollection();
		properties.Add(domainProp);
		properties.Add(httpOnlyCookiesProp);
		properties.Add(requireSSLProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpCookiesSection" /> class.</summary>
	public HttpCookiesSection()
	{
	}
}
