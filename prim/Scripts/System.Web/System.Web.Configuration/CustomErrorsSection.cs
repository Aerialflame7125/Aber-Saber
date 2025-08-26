using System.Configuration;
using System.Xml;

namespace System.Web.Configuration;

/// <summary>Configures the ASP.NET custom errors. This class cannot be inherited.</summary>
public sealed class CustomErrorsSection : ConfigurationSection
{
	private static ConfigurationProperty defaultRedirectProp;

	private static ConfigurationProperty errorsProp;

	private static ConfigurationProperty modeProp;

	private static ConfigurationProperty redirectModeProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the default URL for redirection.</summary>
	/// <returns>The default URL to which the application is redirected when an error occurs.</returns>
	/// <exception cref="T:System.NullReferenceException">The <see cref="P:System.Web.Configuration.CustomErrorsSection.DefaultRedirect" /> property is <see langword="null" />. This is the default.</exception>
	[ConfigurationProperty("defaultRedirect")]
	public string DefaultRedirect
	{
		get
		{
			return (string)base[defaultRedirectProp];
		}
		set
		{
			base[defaultRedirectProp] = value;
		}
	}

	/// <summary>Gets the collection of the <see cref="T:System.Web.Configuration.CustomError" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.CustomErrorCollection" /> that contains the custom errors.</returns>
	[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
	public CustomErrorCollection Errors => (CustomErrorCollection)base[errorsProp];

	/// <summary>Gets or sets the error display modality.</summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.CustomErrorsMode" /> values. The default is <see cref="F:System.Web.Configuration.CustomErrorsMode.RemoteOnly" />.</returns>
	[ConfigurationProperty("mode", DefaultValue = "RemoteOnly")]
	public CustomErrorsMode Mode
	{
		get
		{
			return (CustomErrorsMode)base[modeProp];
		}
		set
		{
			base[modeProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the URL of the request should be changed when the user is redirected to a custom error page.</summary>
	/// <returns>A value that indicates whether the URL is changed when the user is redirected to the custom error page. The default value is <see cref="F:System.Web.Configuration.CustomErrorsRedirectMode.ResponseRedirect" />.</returns>
	[ConfigurationProperty("redirectMode", DefaultValue = CustomErrorsRedirectMode.ResponseRedirect)]
	public CustomErrorsRedirectMode RedirectMode
	{
		get
		{
			return (CustomErrorsRedirectMode)base[redirectModeProp];
		}
		set
		{
			base[redirectModeProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static CustomErrorsSection()
	{
		defaultRedirectProp = new ConfigurationProperty("defaultRedirect", typeof(string), null);
		errorsProp = new ConfigurationProperty(string.Empty, typeof(CustomErrorCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
		modeProp = new ConfigurationProperty("mode", typeof(CustomErrorsMode), CustomErrorsMode.RemoteOnly, new GenericEnumConverter(typeof(CustomErrorsMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		redirectModeProp = new ConfigurationProperty("redirectMode", typeof(CustomErrorsRedirectMode), CustomErrorsRedirectMode.ResponseRedirect, new GenericEnumConverter(typeof(CustomErrorsRedirectMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(defaultRedirectProp);
		properties.Add(errorsProp);
		properties.Add(modeProp);
		properties.Add(redirectModeProp);
	}

	protected internal override void DeserializeSection(XmlReader reader)
	{
		base.DeserializeSection(reader);
	}

	protected internal override void Reset(ConfigurationElement parentElement)
	{
		base.Reset(parentElement);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CustomErrorsSection" /> class using default settings.</summary>
	public CustomErrorsSection()
	{
	}
}
