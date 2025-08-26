using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Provides programmatic access to the <see langword="urlMappings" /> configuration section. This class cannot be inherited.</summary>
public sealed class UrlMappingsSection : ConfigurationSection
{
	private static ConfigurationProperty enabledProp;

	private static ConfigurationProperty urlMappingsProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value indicating whether the mapping is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the mapping is enabled; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[ConfigurationProperty("enabled", DefaultValue = "True")]
	public bool IsEnabled
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

	/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.UrlMapping" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.UrlMappingCollection" /> that contains <see cref="T:System.Web.Configuration.UrlMapping" /> objects.</returns>
	[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
	public UrlMappingCollection UrlMappings => (UrlMappingCollection)base[urlMappingsProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static UrlMappingsSection()
	{
		enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);
		urlMappingsProp = new ConfigurationProperty("", typeof(UrlMappingCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);
		properties = new ConfigurationPropertyCollection();
		properties.Add(enabledProp);
		properties.Add(urlMappingsProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.UrlMappingsSection" /> class.</summary>
	public UrlMappingsSection()
	{
	}
}
