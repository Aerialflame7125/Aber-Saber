using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the SQL cache dependencies for an ASP.NET application. This class cannot be inherited. </summary>
public sealed class SqlCacheDependencySection : ConfigurationSection
{
	private static ConfigurationProperty databasesProp;

	private static ConfigurationProperty enabledProp;

	private static ConfigurationProperty pollTimeProp;

	private static ConfigurationPropertyCollection properties;

	private static ConfigurationElementProperty elementProperty;

	protected override ConfigurationElementProperty ElementProperty
	{
		protected internal get
		{
			return elementProperty;
		}
	}

	/// <summary>Gets the collection of <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects stored within the <see cref="T:System.Web.Configuration.SqlCacheDependencySection" />.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabaseCollection" /> of <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> objects</returns>
	[ConfigurationProperty("databases")]
	public SqlCacheDependencyDatabaseCollection Databases => (SqlCacheDependencyDatabaseCollection)base[databasesProp];

	/// <summary>Gets or sets a value indicating whether the database table should be monitored for changes.</summary>
	/// <returns>
	///     <see langword="true" /> if SQL cache monitoring is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("enabled", DefaultValue = "True")]
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

	/// <summary>Gets or sets the frequency with which the <see cref="T:System.Web.Caching.SqlCacheDependency" /> polls the database table for changes.</summary>
	/// <returns>The SQL cache dependency polling time, in milliseconds. The default is 500.</returns>
	[ConfigurationProperty("pollTime", DefaultValue = "60000")]
	public int PollTime
	{
		get
		{
			return (int)base[pollTimeProp];
		}
		set
		{
			base[pollTimeProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static SqlCacheDependencySection()
	{
		databasesProp = new ConfigurationProperty("databases", typeof(SqlCacheDependencyDatabaseCollection), null, null, null, ConfigurationPropertyOptions.None);
		enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);
		pollTimeProp = new ConfigurationProperty("pollTime", typeof(int), 60000);
		properties = new ConfigurationPropertyCollection();
		properties.Add(databasesProp);
		properties.Add(enabledProp);
		properties.Add(pollTimeProp);
		elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SqlCacheDependencySection), ValidateElement));
	}

	private static void ValidateElement(object o)
	{
	}

	protected override void PostDeserialize()
	{
		base.PostDeserialize();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SqlCacheDependencySection" /> class.</summary>
	public SqlCacheDependencySection()
	{
	}
}
