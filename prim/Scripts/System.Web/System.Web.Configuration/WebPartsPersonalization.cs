using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Allows you to specify the personalization provider and set personalization authorizations. This class cannot be inherited.</summary>
public sealed class WebPartsPersonalization : ConfigurationElement
{
	private static ConfigurationProperty authorizationProp;

	private static ConfigurationProperty defaultProviderProp;

	private static ConfigurationProperty providersProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets an <see cref="T:System.Web.Configuration.AuthorizationSection" /> object containing the Web Parts personalization authorizations for the current Web application.</summary>
	/// <returns>An <see cref="T:System.Web.Configuration.AuthorizationSection" /> object containing the Web Parts personalization authorizations for the current Web application.</returns>
	[ConfigurationProperty("authorization")]
	public WebPartsPersonalizationAuthorization Authorization => (WebPartsPersonalizationAuthorization)base[authorizationProp];

	/// <summary>Gets or sets the name of the default Web Parts personalization provider.</summary>
	/// <returns>The name of the default Web Parts personalization provider.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlPersonalizationProvider")]
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

	/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection that contains the Web Parts personalization providers for the current Web application.</summary>
	/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection that contains the Web Parts personalization providers for the current Web application.</returns>
	[ConfigurationProperty("providers")]
	public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base[providersProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static WebPartsPersonalization()
	{
		authorizationProp = new ConfigurationProperty("authorization", typeof(WebPartsPersonalizationAuthorization), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlPersonalizationProvider", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(authorizationProp);
		properties.Add(defaultProviderProp);
		properties.Add(providersProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.WebPartsPersonalization" /> class using default settings.</summary>
	public WebPartsPersonalization()
	{
	}
}
