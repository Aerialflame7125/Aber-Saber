using System.ComponentModel;
using System.Configuration;
using System.Web.SessionState;

namespace System.Web.Configuration;

/// <summary>Configures the session state for a Web application.</summary>
public sealed class SessionStateSection : ConfigurationSection
{
	internal static readonly string DefaultSqlConnectionString;

	private static ConfigurationProperty allowCustomSqlDatabaseProp;

	private static ConfigurationProperty cookielessProp;

	private static ConfigurationProperty cookieNameProp;

	private static ConfigurationProperty customProviderProp;

	private static ConfigurationProperty modeProp;

	private static ConfigurationProperty partitionResolverTypeProp;

	private static ConfigurationProperty providersProp;

	private static ConfigurationProperty regenerateExpiredSessionIdProp;

	private static ConfigurationProperty sessionIDManagerTypeProp;

	private static ConfigurationProperty sqlCommandTimeoutProp;

	private static ConfigurationProperty sqlConnectionStringProp;

	private static ConfigurationProperty stateConnectionStringProp;

	private static ConfigurationProperty stateNetworkTimeoutProp;

	private static ConfigurationProperty timeoutProp;

	private static ConfigurationProperty useHostingIdentityProp;

	private static ConfigurationProperty compressionEnabledProp;

	private static ConfigurationProperty sqlConnectionRetryIntervalProp;

	private static ConfigurationPropertyCollection properties;

	private static ConfigurationElementProperty elementProperty;

	/// <summary>Gets or sets a value indicating whether the user can specify the initial catalog value in the <see cref="P:System.Web.Configuration.SessionStateSection.SqlConnectionString" /> property.</summary>
	/// <returns>
	///     <see langword="true" /> if the user is allowed to specify the catalog; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[ConfigurationProperty("allowCustomSqlDatabase", DefaultValue = "False")]
	public bool AllowCustomSqlDatabase
	{
		get
		{
			return (bool)base[allowCustomSqlDatabaseProp];
		}
		set
		{
			base[allowCustomSqlDatabaseProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether cookies are used to identify client sessions.</summary>
	/// <returns>
	///     <see langword="true" /> if all requests are treated as cookieless, or <see langword="false" /> if no requests are treated as cookieless, or one of the <see cref="T:System.Web.HttpCookieMode" /> values. The default value in ASP.NET version 2.0 is <see cref="F:System.Web.HttpCookieMode.AutoDetect" />. In earlier versions, the default value was <see langword="false" />.</returns>
	[ConfigurationProperty("cookieless")]
	public HttpCookieMode Cookieless
	{
		get
		{
			return ParseCookieMode((string)base[cookielessProp]);
		}
		set
		{
			base[cookielessProp] = value.ToString();
		}
	}

	/// <summary>Gets or sets the cookie name.</summary>
	/// <returns>The name of the HTTP cookie to use for session identification.</returns>
	[ConfigurationProperty("cookieName", DefaultValue = "ASP.NET_SessionId")]
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

	/// <summary>Gets or sets the name of the custom provider from the <see cref="P:System.Web.Configuration.SessionStateSection.Providers" /> collection.</summary>
	/// <returns>The custom provider name.</returns>
	[ConfigurationProperty("customProvider", DefaultValue = "")]
	public string CustomProvider
	{
		get
		{
			return (string)base[customProviderProp];
		}
		set
		{
			base[customProviderProp] = value;
		}
	}

	/// <summary>Gets or sets a value specifying where to store the session state.</summary>
	/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values. The default value is <see cref="F:System.Web.SessionState.SessionStateMode.InProc" />.</returns>
	[ConfigurationProperty("mode", DefaultValue = "InProc")]
	public SessionStateMode Mode
	{
		get
		{
			return (SessionStateMode)base[modeProp];
		}
		set
		{
			base[modeProp] = value;
		}
	}

	/// <summary>Gets or sets a value specifying where to store the session state.</summary>
	/// <returns>A value specifying where to store the session state, or an empty string ("").</returns>
	[ConfigurationProperty("partitionResolverType", DefaultValue = "")]
	public string PartitionResolverType
	{
		get
		{
			return (string)base[partitionResolverTypeProp];
		}
		set
		{
			base[partitionResolverTypeProp] = value;
		}
	}

	/// <summary>Gets the current <see cref="T:System.Configuration.ProviderSettingsCollection" /> providers.</summary>
	/// <returns>The collection containing the <see cref="T:System.Web.Configuration.SessionStateSection" /> providers.</returns>
	[ConfigurationProperty("providers")]
	public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base[providersProp];

	/// <summary>Gets or sets a value indicating whether the session Id will be re-issued when an expired session ID is specified by the client.</summary>
	/// <returns>
	///     <see langword="true" /> if the session ID must be regenerated; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[ConfigurationProperty("regenerateExpiredSessionId", DefaultValue = "True")]
	public bool RegenerateExpiredSessionId
	{
		get
		{
			return (bool)base[regenerateExpiredSessionIdProp];
		}
		set
		{
			base[regenerateExpiredSessionIdProp] = value;
		}
	}

	/// <summary>Gets or sets a value specifying the fully qualified type of session ID Manager.</summary>
	/// <returns>A fully qualified type of session ID Manager.</returns>
	[ConfigurationProperty("sessionIDManagerType", DefaultValue = "")]
	public string SessionIDManagerType
	{
		get
		{
			return (string)base[sessionIDManagerTypeProp];
		}
		set
		{
			base[sessionIDManagerTypeProp] = value;
		}
	}

	/// <summary>Gets or sets the duration time-out for the SQL commands using the SQL Server session state mode.</summary>
	/// <returns>The amount of time, in seconds, after which a SQL command will time out. The default is 30 seconds.</returns>
	[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
	[ConfigurationProperty("sqlCommandTimeout", DefaultValue = "00:00:30")]
	public TimeSpan SqlCommandTimeout
	{
		get
		{
			return (TimeSpan)base[sqlCommandTimeoutProp];
		}
		set
		{
			base[sqlCommandTimeoutProp] = value;
		}
	}

	/// <summary>Gets or sets the SQL connection string.</summary>
	/// <returns>The SQL connection string. Its default value is the generic string: "data source=127.0.0.1;Integrated Security=SSPI"</returns>
	[ConfigurationProperty("sqlConnectionString", DefaultValue = "data source=localhost;Integrated Security=SSPI")]
	public string SqlConnectionString
	{
		get
		{
			return (string)base[sqlConnectionStringProp];
		}
		set
		{
			base[sqlConnectionStringProp] = value;
		}
	}

	/// <summary>Gets or sets the state server connection string.</summary>
	/// <returns>The state server connection string.</returns>
	[ConfigurationProperty("stateConnectionString", DefaultValue = "tcpip=loopback:42424")]
	public string StateConnectionString
	{
		get
		{
			return (string)base[stateConnectionStringProp];
		}
		set
		{
			base[stateConnectionStringProp] = value;
		}
	}

	/// <summary>Gets or sets the amount of time the network connection between the Web server and the state server can remain idle. </summary>
	/// <returns>The time, in seconds, that the network connection between the Web server and the state server can remain idle before the session is abandoned. The default value is 10 seconds.</returns>
	[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
	[ConfigurationProperty("stateNetworkTimeout", DefaultValue = "00:00:10")]
	public TimeSpan StateNetworkTimeout
	{
		get
		{
			return (TimeSpan)base[stateNetworkTimeoutProp];
		}
		set
		{
			base[stateNetworkTimeoutProp] = value;
		}
	}

	/// <summary>Gets or sets the session time-out</summary>
	/// <returns>The session time-out, in minutes. The default value is 20 minutes.</returns>
	[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
	[TimeSpanValidator(MinValueString = "00:01:00", MaxValueString = "10675199.02:48:05.4775807")]
	[ConfigurationProperty("timeout", DefaultValue = "00:20:00")]
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

	/// <summary>Gets or sets a value specifying the whether the session state will use client impersonation when available, or will always revert to the hosting identity.</summary>
	/// <returns>
	///     <see langword="true" /> if Web application should revert to hosting identity; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[ConfigurationProperty("useHostingIdentity", DefaultValue = "True")]
	public bool UseHostingIdentity
	{
		get
		{
			return (bool)base[useHostingIdentityProp];
		}
		set
		{
			base[useHostingIdentityProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether compression is enabled for session-state data.</summary>
	/// <returns>
	///     <see langword="true" /> if compression is enabled; otherwise <see langword="false" />. The default is <see langword="false" />. </returns>
	[ConfigurationProperty("compressionEnabled", DefaultValue = false)]
	public bool CompressionEnabled
	{
		get
		{
			return (bool)base[compressionEnabledProp];
		}
		set
		{
			base[compressionEnabledProp] = value;
		}
	}

	/// <summary>Gets or sets the time interval that should elapse before ASP.NET reconnects to the database.</summary>
	/// <returns>The time interval that should elapse before ASP.NET reconnects to the database.</returns>
	[TypeConverter(typeof(TimeSpanSecondsOrInfiniteConverter))]
	[ConfigurationProperty("sqlConnectionRetryInterval", DefaultValue = "00:00:00")]
	public TimeSpan SqlConnectionRetryInterval
	{
		get
		{
			return (TimeSpan)base[sqlConnectionRetryIntervalProp];
		}
		set
		{
			base[sqlConnectionRetryIntervalProp] = value;
		}
	}

	protected override ConfigurationElementProperty ElementProperty
	{
		protected internal get
		{
			return elementProperty;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	internal bool CookieLess
	{
		get
		{
			return Cookieless != HttpCookieMode.UseCookies;
		}
		set
		{
			Cookieless = ((!value) ? HttpCookieMode.UseCookies : HttpCookieMode.UseUri);
		}
	}

	static SessionStateSection()
	{
		DefaultSqlConnectionString = "data source=localhost;Integrated Security=SSPI";
		allowCustomSqlDatabaseProp = new ConfigurationProperty("allowCustomSqlDatabase", typeof(bool), false);
		cookielessProp = new ConfigurationProperty("cookieless", typeof(string), null);
		cookieNameProp = new ConfigurationProperty("cookieName", typeof(string), "ASP.NET_SessionId");
		customProviderProp = new ConfigurationProperty("customProvider", typeof(string), "");
		modeProp = new ConfigurationProperty("mode", typeof(SessionStateMode), SessionStateMode.InProc, new GenericEnumConverter(typeof(SessionStateMode)), null, ConfigurationPropertyOptions.None);
		partitionResolverTypeProp = new ConfigurationProperty("partitionResolverType", typeof(string), "");
		providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, null, ConfigurationPropertyOptions.None);
		regenerateExpiredSessionIdProp = new ConfigurationProperty("regenerateExpiredSessionId", typeof(bool), true);
		sessionIDManagerTypeProp = new ConfigurationProperty("sessionIDManagerType", typeof(string), "");
		sqlCommandTimeoutProp = new ConfigurationProperty("sqlCommandTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(30.0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, null, ConfigurationPropertyOptions.None);
		sqlConnectionStringProp = new ConfigurationProperty("sqlConnectionString", typeof(string), DefaultSqlConnectionString);
		stateConnectionStringProp = new ConfigurationProperty("stateConnectionString", typeof(string), "tcpip=loopback:42424");
		stateNetworkTimeoutProp = new ConfigurationProperty("stateNetworkTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(10.0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		timeoutProp = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.FromMinutes(20.0), PropertyHelper.TimeSpanMinutesOrInfiniteConverter, new TimeSpanValidator(new TimeSpan(0, 1, 0), TimeSpan.MaxValue), ConfigurationPropertyOptions.None);
		useHostingIdentityProp = new ConfigurationProperty("useHostingIdentity", typeof(bool), true);
		compressionEnabledProp = new ConfigurationProperty("compressionEnabled", typeof(bool), false);
		sqlConnectionRetryIntervalProp = new ConfigurationProperty("sqlConnectionRetryIntervalProp", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.TimeSpanSecondsOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(allowCustomSqlDatabaseProp);
		properties.Add(cookielessProp);
		properties.Add(cookieNameProp);
		properties.Add(customProviderProp);
		properties.Add(modeProp);
		properties.Add(partitionResolverTypeProp);
		properties.Add(providersProp);
		properties.Add(regenerateExpiredSessionIdProp);
		properties.Add(sessionIDManagerTypeProp);
		properties.Add(sqlCommandTimeoutProp);
		properties.Add(sqlConnectionStringProp);
		properties.Add(stateConnectionStringProp);
		properties.Add(stateNetworkTimeoutProp);
		properties.Add(timeoutProp);
		properties.Add(useHostingIdentityProp);
		properties.Add(compressionEnabledProp);
		properties.Add(sqlConnectionRetryIntervalProp);
		elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SessionStateSection), ValidateElement));
	}

	protected override void PostDeserialize()
	{
		base.PostDeserialize();
	}

	private static void ValidateElement(object o)
	{
	}

	private HttpCookieMode ParseCookieMode(string s)
	{
		if (s == "true")
		{
			return HttpCookieMode.UseUri;
		}
		if (s == "false" || s == null)
		{
			return HttpCookieMode.UseCookies;
		}
		try
		{
			return (HttpCookieMode)Enum.Parse(typeof(HttpCookieMode), s);
		}
		catch
		{
			return HttpCookieMode.UseCookies;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SessionStateSection" /> class.</summary>
	public SessionStateSection()
	{
	}
}
