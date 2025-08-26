using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the user's credentials for Web applications that use forms-based authentication. </summary>
public sealed class FormsAuthenticationUser : ConfigurationElement
{
	private static ConfigurationProperty nameProp;

	private static ConfigurationProperty passwordProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the logon user name.</summary>
	/// <returns>The logon user name required by the application.</returns>
	[StringValidator]
	[TypeConverter(typeof(LowerCaseStringConverter))]
	[ConfigurationProperty("name", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
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

	/// <summary>Gets or sets the user's password.</summary>
	/// <returns>The user's password required by the application.</returns>
	[StringValidator]
	[ConfigurationProperty("password", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
	public string Password
	{
		get
		{
			return (string)base[passwordProp];
		}
		set
		{
			base[passwordProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static FormsAuthenticationUser()
	{
		nameProp = new ConfigurationProperty("name", typeof(string), "", new LowerCaseStringConverter(), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		passwordProp = new ConfigurationProperty("password", typeof(string), "", ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(nameProp);
		properties.Add(passwordProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FormsAuthenticationUser" /> class using the passed parameters.</summary>
	/// <param name="name">User's name.</param>
	/// <param name="password">User's password.</param>
	public FormsAuthenticationUser(string name, string password)
	{
		Name = name;
		Password = password;
	}
}
