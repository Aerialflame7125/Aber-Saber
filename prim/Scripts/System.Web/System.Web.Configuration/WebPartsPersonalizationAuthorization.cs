using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Provides programmatic access to the <see langword="authorization" /> section contained in the <see langword="webParts" /> section of the configuration. This class cannot be inherited.</summary>
public sealed class WebPartsPersonalizationAuthorization : ConfigurationElement
{
	private static ConfigurationProperty Prop;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets a collection of rules used for personalization authorization related to Web Parts.</summary>
	/// <returns>An <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> object.</returns>
	[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
	public AuthorizationRuleCollection Rules => (AuthorizationRuleCollection)base[Prop];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static WebPartsPersonalizationAuthorization()
	{
		Prop = new ConfigurationProperty("", typeof(AuthorizationRuleCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
		properties = new ConfigurationPropertyCollection();
		properties.Add(Prop);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.WebPartsPersonalizationAuthorization" /> class.</summary>
	public WebPartsPersonalizationAuthorization()
	{
	}
}
