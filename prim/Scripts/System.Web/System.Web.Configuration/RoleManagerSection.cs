using System.ComponentModel;
using System.Configuration;
using System.Web.Security;

namespace System.Web.Configuration;

/// <summary>Defines configuration settings that are used to support the role management infrastructure of Web applications. This class cannot be inherited.</summary>
public sealed class RoleManagerSection : ConfigurationSection
{
	private static ConfigurationProperty cacheRolesInCookieProp;

	private static ConfigurationProperty cookieNameProp;

	private static ConfigurationProperty cookiePathProp;

	private static ConfigurationProperty cookieProtectionProp;

	private static ConfigurationProperty cookieRequireSSLProp;

	private static ConfigurationProperty cookieSlidingExpirationProp;

	private static ConfigurationProperty cookieTimeoutProp;

	private static ConfigurationProperty createPersistentCookieProp;

	private static ConfigurationProperty defaultProviderProp;

	private static ConfigurationProperty domainProp;

	private static ConfigurationProperty enabledProp;

	private static ConfigurationProperty maxCachedResultsProp;

	private static ConfigurationProperty providersProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value indicating whether the current user's roles are cached in a cookie.</summary>
	/// <returns>
	///     <see langword="true" /> if the current user's roles are cached in a cookie; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("cacheRolesInCookie", DefaultValue = false)]
	public bool CacheRolesInCookie
	{
		get
		{
			return (bool)base[cacheRolesInCookieProp];
		}
		set
		{
			base[cacheRolesInCookieProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the cookie that is used to cache role names.</summary>
	/// <returns>The name of the cookie used to cache role names. The default is ".ASPXROLES".</returns>
	[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("cookieName", DefaultValue = ".ASPXROLES")]
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

	/// <summary>Gets or sets the virtual path of the cookie that is used to cache role names.</summary>
	/// <returns>The path of the cookie used to store role names. The default is "/".</returns>
	[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
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

	/// <summary>Gets or sets the type of security that is used to protect the cookie that caches role names.</summary>
	/// <returns>The type of security protection used within the cookie where role names are cached. The default is <see langword="All" />.</returns>
	[ConfigurationProperty("cookieProtection", DefaultValue = "All")]
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

	/// <summary>Gets or sets a value indicating whether the cookie that is used to cache role names requires a Secure Sockets Layer (SSL) connection in order to be returned to the server.</summary>
	/// <returns>
	///     <see langword="true" /> if an SSL connection is needed in order to return to the server the cookie where role names are cached; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("cookieRequireSSL", DefaultValue = false)]
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

	/// <summary>Gets or sets a value indicating whether the cookie that is used to cache role names will be reset periodically. </summary>
	/// <returns>
	///     <see langword="true" /> if the role names cookie expiration date and time will be reset periodically; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("cookieSlidingExpiration", DefaultValue = true)]
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

	/// <summary>Gets or sets the number of minutes before the cookie that is used to cache role names expires.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> that is the number of minutes before the cookie used to cache role names expires. The default is 30, in minutes.</returns>
	[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
	[ConfigurationProperty("cookieTimeout", DefaultValue = "00:30:00")]
	[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
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

	/// <summary>Indicates whether a session-based cookie or a persistent cookie is used to cache role names. </summary>
	/// <returns>
	///     <see langword="true" /> to make the role names cookie persistent across browser sessions; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("createPersistentCookie", DefaultValue = false)]
	public bool CreatePersistentCookie
	{
		get
		{
			return (bool)base[createPersistentCookieProp];
		}
		set
		{
			base[createPersistentCookieProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the default provider that is used to manage roles. </summary>
	/// <returns>The name of a provider in the <see cref="P:System.Web.Configuration.RoleManagerSection.Providers" />. The default is "AspNetSqlRoleProvider".</returns>
	[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlRoleProvider")]
	public string DefaultProvider
	{
		get
		{
			return (string)base[defaultProviderProp];
		}
		set
		{
			base[defaultProviderProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the domain that is associated with the cookie that is used to cache role names. </summary>
	/// <returns>The <see cref="P:System.Web.HttpCookie.Domain" /> of the cookie used to cache role names. The default is an empty string (<see langword="&quot;&quot;" />).</returns>
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

	/// <summary>Gets or sets a value indicating whether the ASP.NET role management feature is enabled. </summary>
	/// <returns>
	///     <see langword="true" /> if the ASP.NET role management feature is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("enabled", DefaultValue = false)]
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

	/// <summary>Gets or sets the maximum number of roles that ASP.NET caches in the role cookie. </summary>
	/// <returns>A value indicating the maximum number of roles ASP.NET caches in the role cookie. The default is 25.</returns>
	[ConfigurationProperty("maxCachedResults", DefaultValue = 25)]
	public int MaxCachedResults
	{
		get
		{
			return (int)base[maxCachedResultsProp];
		}
		set
		{
			base[maxCachedResultsProp] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> object of <see cref="T:System.Configuration.ProviderSettings" /> elements.</summary>
	/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the providers settings defined within the <see langword="providers" /> subsection of the <see langword="roleManager" /> section of the configuration file.</returns>
	[ConfigurationProperty("providers")]
	public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base[providersProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static RoleManagerSection()
	{
		cacheRolesInCookieProp = new ConfigurationProperty("cacheRolesInCookie", typeof(bool), false);
		cookieNameProp = new ConfigurationProperty("cookieName", typeof(string), ".ASPXROLES");
		cookiePathProp = new ConfigurationProperty("cookiePath", typeof(string), "/");
		cookieProtectionProp = new ConfigurationProperty("cookieProtection", typeof(CookieProtection), CookieProtection.All);
		cookieRequireSSLProp = new ConfigurationProperty("cookieRequireSSL", typeof(bool), false);
		cookieSlidingExpirationProp = new ConfigurationProperty("cookieSlidingExpiration", typeof(bool), true);
		cookieTimeoutProp = new ConfigurationProperty("cookieTimeout", typeof(TimeSpan), TimeSpan.FromMinutes(30.0), PropertyHelper.TimeSpanMinutesOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		createPersistentCookieProp = new ConfigurationProperty("createPersistentCookie", typeof(bool), false);
		defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlRoleProvider");
		domainProp = new ConfigurationProperty("domain", typeof(string), "");
		enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);
		maxCachedResultsProp = new ConfigurationProperty("maxCachedResults", typeof(int), 25);
		providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));
		properties = new ConfigurationPropertyCollection();
		properties.Add(cacheRolesInCookieProp);
		properties.Add(cookieNameProp);
		properties.Add(cookiePathProp);
		properties.Add(cookieProtectionProp);
		properties.Add(cookieRequireSSLProp);
		properties.Add(cookieSlidingExpirationProp);
		properties.Add(cookieTimeoutProp);
		properties.Add(createPersistentCookieProp);
		properties.Add(defaultProviderProp);
		properties.Add(domainProp);
		properties.Add(enabledProp);
		properties.Add(maxCachedResultsProp);
		properties.Add(providersProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RoleManagerSection" /> class by using default settings.</summary>
	public RoleManagerSection()
	{
	}
}
