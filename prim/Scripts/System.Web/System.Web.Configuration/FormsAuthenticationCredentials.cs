using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures user credentials for ASP.NET applications that use form-based authentication.</summary>
public sealed class FormsAuthenticationCredentials : ConfigurationElement
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty passwordFormatProp;

	private static ConfigurationProperty usersProp;

	/// <summary>Gets or sets the password format.</summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.FormsAuthPasswordFormat" /> values.</returns>
	[ConfigurationProperty("passwordFormat", DefaultValue = "SHA1")]
	public FormsAuthPasswordFormat PasswordFormat
	{
		get
		{
			return (FormsAuthPasswordFormat)base[passwordFormatProp];
		}
		set
		{
			base[passwordFormatProp] = value;
		}
	}

	/// <summary>Gets the users' names and password credentials.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.FormsAuthenticationUserCollection" /> that contains the users' names and password credentials.</returns>
	[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
	public FormsAuthenticationUserCollection Users => (FormsAuthenticationUserCollection)base[usersProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static FormsAuthenticationCredentials()
	{
		passwordFormatProp = new ConfigurationProperty("passwordFormat", typeof(FormsAuthPasswordFormat), FormsAuthPasswordFormat.SHA1, new GenericEnumConverter(typeof(FormsAuthPasswordFormat)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		usersProp = new ConfigurationProperty("", typeof(FormsAuthenticationUserCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
		properties = new ConfigurationPropertyCollection();
		properties.Add(passwordFormatProp);
		properties.Add(usersProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.FormsAuthenticationCredentials" /> class.</summary>
	public FormsAuthenticationCredentials()
	{
	}
}
