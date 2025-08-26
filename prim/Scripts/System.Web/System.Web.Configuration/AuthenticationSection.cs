using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the authentication for a Web application. This class cannot be inherited.</summary>
public sealed class AuthenticationSection : ConfigurationSection
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty formsProp;

	private static ConfigurationProperty passportProp;

	private static ConfigurationProperty modeProp;

	/// <summary>Gets the <see cref="P:System.Web.Configuration.AuthenticationSection.Forms" /> element property.</summary>
	/// <returns>A <see cref="P:System.Web.Configuration.AuthenticationSection.Forms" /> element property that contains information used during forms-based authentication.</returns>
	[ConfigurationProperty("forms")]
	public FormsAuthenticationConfiguration Forms => (FormsAuthenticationConfiguration)base[formsProp];

	/// <summary>Gets the <see cref="P:System.Web.Configuration.AuthenticationSection.Passport" /> element property.</summary>
	/// <returns>A <see cref="P:System.Web.Configuration.AuthenticationSection.Passport" /> element property that contains information used during passport-based authentication.</returns>
	[ConfigurationProperty("passport")]
	[Obsolete("This property is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public PassportAuthentication Passport => (PassportAuthentication)base[passportProp];

	/// <summary>Gets or sets the authentication modality.</summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.AuthenticationMode" /> values.</returns>
	[ConfigurationProperty("mode", DefaultValue = "Windows")]
	public AuthenticationMode Mode
	{
		get
		{
			return (AuthenticationMode)base[modeProp];
		}
		set
		{
			base[modeProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static AuthenticationSection()
	{
		formsProp = new ConfigurationProperty("forms", typeof(FormsAuthenticationConfiguration), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		passportProp = new ConfigurationProperty("passport", typeof(PassportAuthentication), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		modeProp = new ConfigurationProperty("mode", typeof(AuthenticationMode), AuthenticationMode.Windows, new GenericEnumConverter(typeof(AuthenticationMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(formsProp);
		properties.Add(passportProp);
		properties.Add(modeProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.AuthenticationSection" /> class.</summary>
	public AuthenticationSection()
	{
	}

	protected internal override void Reset(ConfigurationElement parentElement)
	{
		base.Reset(parentElement);
	}
}
