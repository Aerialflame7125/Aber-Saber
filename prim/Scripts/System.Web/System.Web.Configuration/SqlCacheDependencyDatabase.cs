using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the SQL cache dependencies databases for an ASP.NET application. This class cannot be inherited. </summary>
public sealed class SqlCacheDependencyDatabase : ConfigurationElement
{
	private static ConfigurationProperty connectionStringNameProp;

	private static ConfigurationProperty nameProp;

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

	/// <summary>Gets or sets the connection name for the database.</summary>
	/// <returns>A string that specifies the name of a database connection string within the <see langword="connectionStrings" /> configuration section.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("connectionStringName", Options = ConfigurationPropertyOptions.IsRequired)]
	public string ConnectionStringName
	{
		get
		{
			return (string)base[connectionStringNameProp];
		}
		set
		{
			base[connectionStringNameProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the database. </summary>
	/// <returns>A string that specifies the name used by <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> to identify the database.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("name", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
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

	/// <summary>Gets or sets the frequency with which the <see cref="T:System.Web.Caching.SqlCacheDependency" /> polls the database table for changes.</summary>
	/// <returns>The database polling time, in milliseconds. </returns>
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

	static SqlCacheDependencyDatabase()
	{
		connectionStringNameProp = new ConfigurationProperty("connectionStringName", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
		nameProp = new ConfigurationProperty("name", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		pollTimeProp = new ConfigurationProperty("pollTime", typeof(int), 60000);
		properties = new ConfigurationPropertyCollection();
		properties.Add(connectionStringNameProp);
		properties.Add(nameProp);
		properties.Add(pollTimeProp);
		elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(SqlCacheDependencyDatabase), ValidateElement));
	}

	internal SqlCacheDependencyDatabase()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> class with the passed parameters.</summary>
	/// <param name="name">A string that specifies the name used by <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> to identify the database.</param>
	/// <param name="connectionStringName">A string that specifies the name of the connection string in the <see langword="connectionStrings" /> section to use to connect to this database.</param>
	public SqlCacheDependencyDatabase(string name, string connectionStringName)
	{
		Name = name;
		ConnectionStringName = name;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> class. </summary>
	/// <param name="name">A string that specifies the name used by <see cref="T:System.Web.Configuration.SqlCacheDependencyDatabase" /> to identify the database.</param>
	/// <param name="connectionStringName">A string that specifies the name of the connection string in the <see langword="connectionStrings" /> section to use to connect to this database.</param>
	/// <param name="pollTime">The database polling time, in milliseconds. </param>
	public SqlCacheDependencyDatabase(string name, string connectionStringName, int pollTime)
	{
		Name = name;
		ConnectionStringName = name;
		PollTime = pollTime;
	}

	private static void ValidateElement(object o)
	{
	}
}
