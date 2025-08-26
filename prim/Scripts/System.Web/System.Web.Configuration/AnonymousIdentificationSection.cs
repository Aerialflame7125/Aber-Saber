using System.ComponentModel;
using System.Configuration;
using System.Web.Security;

namespace System.Web.Configuration;

/// <summary>Configures anonymous identification for users that are not authenticated. This class cannot be inherited.</summary>
public sealed class AnonymousIdentificationSection : ConfigurationSection
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty enabledProp;

	private static ConfigurationProperty cookielessProp;

	private static ConfigurationProperty cookieNameProp;

	private static ConfigurationProperty cookieTimeoutProp;

	private static ConfigurationProperty cookiePathProp;

	private static ConfigurationProperty cookieRequireSSLProp;

	private static ConfigurationProperty cookieSlidingExpirationProp;

	private static ConfigurationProperty cookieProtectionProp;

	private static ConfigurationProperty domainProp;

	/// <summary>Gets or sets a value indicating whether to use cookies.</summary>
	/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values. The default value is <see cref="F:System.Web.HttpCookieMode.UseDeviceProfile" />. </returns>
	[ConfigurationProperty("cookieless", DefaultValue = "UseCookies")]
	public HttpCookieMode Cookieless
	{
		get
		{
			return (HttpCookieMode)base[cookielessProp];
		}
		set
		{
			base[cookielessProp] = value;
		}
	}

	/// <summary>Gets or sets the cookie name.</summary>
	/// <returns>The name of the cookie. The default value is ".ASPXANONYMOUS".</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("cookieName", DefaultValue = ".ASPXANONYMOUS")]
	public string CookieName
	{
		get
		{
			return (string)base[cookieNameProp];
		}
		set
		{
			base[cookieNameProp] = value;
		}
	}

	/// <summary>Gets or sets the path where the cookie is stored.</summary>
	/// <returns>The path of the HTTP cookie to use for the user's anonymous identification. The default value is a slash (/), which represents the Web application root.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("cookiePath", DefaultValue = "/")]
	public string CookiePath
	{
		get
		{
			return (string)base[cookiePathProp];
		}
		set
		{
			base[cookiePathProp] = value;
		}
	}

	/// <summary>Gets or sets the encryption type used to encrypt the cookie.</summary>
	/// <returns>One of the <see cref="T:System.Web.Security.CookieProtection" /> values. The default value is <see cref="F:System.Web.Security.CookieProtection.All" />.</returns>
	[ConfigurationProperty("cookieProtection", DefaultValue = "Validation")]
	public CookieProtection CookieProtection
	{
		get
		{
			return (CookieProtection)base[cookieProtectionProp];
		}
		set
		{
			base[cookieProtectionProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a Secure Sockets Layer (SSL) connection is required when transmitting authentication information.</summary>
	/// <returns>
	///     <see langword="true" /> if an SSL connection is required; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("cookieRequireSSL", DefaultValue = "False")]
	public bool CookieRequireSSL
	{
		get
		{
			return (bool)base[cookieRequireSSLProp];
		}
		set
		{
			base[cookieRequireSSLProp] = value;
		}
	}

	/// <summary>Gets or sets whether the cookie time-out value is reset on each request.</summary>
	/// <returns>
	///     <see langword="true" /> if the sliding expiration is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("cookieSlidingExpiration", DefaultValue = "True")]
	public bool CookieSlidingExpiration
	{
		get
		{
			return (bool)base[cookieSlidingExpirationProp];
		}
		set
		{
			base[cookieSlidingExpirationProp] = value;
		}
	}

	/// <summary>Gets or sets the amount of time, in minutes, after which the authentication expires.</summary>
	/// <returns>The amount of time, in minutes, after which the authentication expires. The default value is 100000.</returns>
	[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
	[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
	[ConfigurationProperty("cookieTimeout", DefaultValue = "69.10:40:00")]
	public TimeSpan CookieTimeout
	{
		get
		{
			return (TimeSpan)base[cookieTimeoutProp];
		}
		set
		{
			base[cookieTimeoutProp] = value;
		}
	}

	/// <summary>Gets or sets the cookie domain.</summary>
	/// <returns>The name of the cookie domain. The default is an empty string ("").</returns>
	[ConfigurationProperty("domain")]
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

	/// <summary>Gets or sets a value indicating whether anonymous identification is enabled. </summary>
	/// <returns>
	///     <see langword="true" /> if anonymous identification is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("enabled", DefaultValue = "False")]
	public bool Enabled
	{
		get
		{
			return (bool)base[enabledProp];
		}
		set
		{
			base[enabledProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static AnonymousIdentificationSection()
	{
		enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);
		cookielessProp = new ConfigurationProperty("cookieless", typeof(HttpCookieMode), HttpCookieMode.UseCookies, new GenericEnumConverter(typeof(HttpCookieMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		cookieNameProp = new ConfigurationProperty("cookieName", typeof(string), ".ASPXANONYMOUS", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		cookieTimeoutProp = new ConfigurationProperty("cookieTimeout", typeof(TimeSpan), new TimeSpan(69, 10, 40, 0), new TimeSpanMinutesOrInfiniteConverter(), PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		cookiePathProp = new ConfigurationProperty("cookiePath", typeof(string), "/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		cookieRequireSSLProp = new ConfigurationProperty("cookieRequireSSL", typeof(bool), false);
		cookieSlidingExpirationProp = new ConfigurationProperty("cookieSlidingExpiration", typeof(bool), true);
		cookieProtectionProp = new ConfigurationProperty("cookieProtection", typeof(CookieProtection), CookieProtection.Validation, new GenericEnumConverter(typeof(CookieProtection)), null, ConfigurationPropertyOptions.None);
		domainProp = new ConfigurationProperty("domain", typeof(string), null);
		properties = new ConfigurationPropertyCollection();
		properties.Add(enabledProp);
		properties.Add(cookielessProp);
		properties.Add(cookieNameProp);
		properties.Add(cookieTimeoutProp);
		properties.Add(cookiePathProp);
		properties.Add(cookieRequireSSLProp);
		properties.Add(cookieSlidingExpirationProp);
		properties.Add(cookieProtectionProp);
		properties.Add(domainProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.AnonymousIdentificationSection" /> class.</summary>
	public AnonymousIdentificationSection()
	{
	}
}
