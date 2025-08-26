using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures an ASP.NET application to use the <see cref="T:System.Web.Configuration.AuthenticationMode" /> forms modality. </summary>
public sealed class FormsAuthenticationConfiguration : ConfigurationElement
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty cookielessProp;

	private static ConfigurationProperty credentialsProp;

	private static ConfigurationProperty defaultUrlProp;

	private static ConfigurationProperty domainProp;

	private static ConfigurationProperty enableCrossAppRedirectsProp;

	private static ConfigurationProperty loginUrlProp;

	private static ConfigurationProperty nameProp;

	private static ConfigurationProperty pathProp;

	private static ConfigurationProperty protectionProp;

	private static ConfigurationProperty requireSSLProp;

	private static ConfigurationProperty slidingExpirationProp;

	private static ConfigurationProperty timeoutProp;

	private static ConfigurationElementProperty elementProperty;

	protected override ConfigurationElementProperty ElementProperty
	{
		protected internal get
		{
			return elementProperty;
		}
	}

	/// <summary>Gets or sets a value indicating whether forms-based authentication should use cookies.</summary>
	/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values. The default value is <see cref="F:System.Web.HttpCookieMode.UseDeviceProfile" />.</returns>
	[ConfigurationProperty("cookieless", DefaultValue = "UseDeviceProfile")]
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

	/// <summary>Gets the <see cref="T:System.Web.Configuration.FormsAuthenticationCredentials" /> collection of user names and passwords.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationCredentials" /> collection that contains the user names and passwords.</returns>
	[ConfigurationProperty("credentials")]
	public FormsAuthenticationCredentials Credentials => (FormsAuthenticationCredentials)base[credentialsProp];

	/// <summary>Gets or sets the default URL.</summary>
	/// <returns>The URL to which to redirect the request after authentication. The default value is default.aspx.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("defaultUrl", DefaultValue = "default.aspx")]
	public string DefaultUrl
	{
		get
		{
			return (string)base[defaultUrlProp];
		}
		set
		{
			base[defaultUrlProp] = value;
		}
	}

	/// <summary>Gets or sets the domain name to be sent with forms authentication cookies.</summary>
	/// <returns>The name of the domain for the outgoing forms authentication cookies. Default is an empty string.</returns>
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

	/// <summary>Gets or sets a value indicating whether authenticated users can be redirected to URLS in other applications.</summary>
	/// <returns>true if authenticated users can be redirected to URLs in other applications; otherwise false. The default is false.</returns>
	[ConfigurationProperty("enableCrossAppRedirects", DefaultValue = "False")]
	public bool EnableCrossAppRedirects
	{
		get
		{
			return (bool)base[enableCrossAppRedirectsProp];
		}
		set
		{
			base[enableCrossAppRedirectsProp] = value;
		}
	}

	/// <summary>Gets or sets the redirection URL for the request.</summary>
	/// <returns>The URL the request is redirected to when the user is not authenticated. The default value is login.aspx.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("loginUrl", DefaultValue = "login.aspx")]
	public string LoginUrl
	{
		get
		{
			return (string)base[loginUrlProp];
		}
		set
		{
			base[loginUrlProp] = value;
		}
	}

	/// <summary>Gets or sets the cookie name.</summary>
	/// <returns>The name of the HTTP cookie to use for request authentication.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("name", DefaultValue = ".ASPXAUTH")]
	public string Name
	{
		get
		{
			return (string)base[nameProp];
		}
		set
		{
			base[nameProp] = value;
		}
	}

	/// <summary>Gets or sets the cookie path.</summary>
	/// <returns>The path of the HTTP cookie to use for authentication. The default value is a slash (/), which represents the Web-application root.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("path", DefaultValue = "/")]
	public string Path
	{
		get
		{
			return (string)base[pathProp];
		}
		set
		{
			base[pathProp] = value;
		}
	}

	/// <summary>Gets or sets the encryption type used to encrypt the cookie.</summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.FormsProtectionEnum" /> enumeration values. The default value is <see langword="All" />.
	///     Note   Be sure to use the default value for this property if you want both data validation and encryption to help protect the cookie. This option uses the configured data-validation algorithm based on the <see langword="machineKey" />. Triple-DES (3DES) is used for encryption, if available and if the key is long enough (48 bytes or more). To improve the protection of your cookie, you may also want to set the <see cref="P:System.Web.Configuration.FormsAuthenticationConfiguration.RequireSSL" /> to <see langword="true" />.</returns>
	[ConfigurationProperty("protection", DefaultValue = "All")]
	public FormsProtectionEnum Protection
	{
		get
		{
			return (FormsProtectionEnum)base[protectionProp];
		}
		set
		{
			base[protectionProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a Secure Sockets Layer (SSL) connection is required when transmitting authentication information.</summary>
	/// <returns>
	///     <see langword="true" /> if an SSL connection is required; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
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

	/// <summary>Gets or sets the authentication sliding expiration.</summary>
	/// <returns>
	///     <see langword="true" /> if the sliding expiration is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("slidingExpiration", DefaultValue = "True")]
	public bool SlidingExpiration
	{
		get
		{
			return (bool)base[slidingExpirationProp];
		}
		set
		{
			base[slidingExpirationProp] = value;
		}
	}

	/// <summary>Gets or sets the authentication time-out.</summary>
	/// <returns>The amount of time in minutes after which the authentication expires. The default value is 30 minutes.</returns>
	[TypeConverter(typeof(TimeSpanMinutesConverter))]
	[TimeSpanValidator(MinValueString = "00:01:00")]
	[ConfigurationProperty("timeout", DefaultValue = "00:30:00")]
	public TimeSpan Timeout
	{
		get
		{
			return (TimeSpan)base[timeoutProp];
		}
		set
		{
			base[timeoutProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static FormsAuthenticationConfiguration()
	{
		cookielessProp = new ConfigurationProperty("cookieless", typeof(HttpCookieMode), HttpCookieMode.UseDeviceProfile, new GenericEnumConverter(typeof(HttpCookieMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		credentialsProp = new ConfigurationProperty("credentials", typeof(FormsAuthenticationCredentials), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		defaultUrlProp = new ConfigurationProperty("defaultUrl", typeof(string), "default.aspx", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		domainProp = new ConfigurationProperty("domain", typeof(string), "");
		enableCrossAppRedirectsProp = new ConfigurationProperty("enableCrossAppRedirects", typeof(bool), false);
		loginUrlProp = new ConfigurationProperty("loginUrl", typeof(string), "login.aspx", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		nameProp = new ConfigurationProperty("name", typeof(string), ".ASPXAUTH", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		pathProp = new ConfigurationProperty("path", typeof(string), "/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		protectionProp = new ConfigurationProperty("protection", typeof(FormsProtectionEnum), FormsProtectionEnum.All, new GenericEnumConverter(typeof(FormsProtectionEnum)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		requireSSLProp = new ConfigurationProperty("requireSSL", typeof(bool), false);
		slidingExpirationProp = new ConfigurationProperty("slidingExpiration", typeof(bool), true);
		timeoutProp = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.FromMinutes(30.0), PropertyHelper.TimeSpanMinutesConverter, new TimeSpanValidator(new TimeSpan(0, 1, 0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(cookielessProp);
		properties.Add(credentialsProp);
		properties.Add(defaultUrlProp);
		properties.Add(domainProp);
		properties.Add(enableCrossAppRedirectsProp);
		properties.Add(loginUrlProp);
		properties.Add(nameProp);
		properties.Add(pathProp);
		properties.Add(protectionProp);
		properties.Add(requireSSLProp);
		properties.Add(slidingExpirationProp);
		properties.Add(timeoutProp);
		elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(FormsAuthenticationConfiguration), ValidateElement));
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FormsAuthenticationConfiguration" /> class.</summary>
	public FormsAuthenticationConfiguration()
	{
	}

	private static void ValidateElement(object o)
	{
	}
}
