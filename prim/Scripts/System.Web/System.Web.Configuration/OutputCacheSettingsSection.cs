using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the output cache settings for application pages . This class cannot be inherited.</summary>
public sealed class OutputCacheSettingsSection : ConfigurationSection
{
	private static ConfigurationProperty outputCacheProfilesProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets a <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" />.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.OutputCacheProfileCollection" /> of <see cref="T:System.Web.Configuration.OutputCacheProfile" /> objects</returns>
	[ConfigurationProperty("outputCacheProfiles")]
	public OutputCacheProfileCollection OutputCacheProfiles => (OutputCacheProfileCollection)base[outputCacheProfilesProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static OutputCacheSettingsSection()
	{
		outputCacheProfilesProp = new ConfigurationProperty("outputCacheProfiles", typeof(OutputCacheProfileCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(outputCacheProfilesProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.OutputCacheSettingsSection" /> class.</summary>
	public OutputCacheSettingsSection()
	{
	}
}
