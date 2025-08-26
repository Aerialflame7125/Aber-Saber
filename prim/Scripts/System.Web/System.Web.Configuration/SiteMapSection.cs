using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines configuration settings that are used to support the infrastructure for configuring, storing, and rendering site navigation. This class cannot be inherited.</summary>
public sealed class SiteMapSection : ConfigurationSection
{
	private static ConfigurationProperty defaultProviderProp;

	private static ConfigurationProperty enabledProp;

	private static ConfigurationProperty providersProp;

	private static ConfigurationPropertyCollection properties;

	private SiteMapProviderCollection providers;

	/// <summary>Gets or sets the name of the default navigation provider. </summary>
	/// <returns>The name of a provider in the <see cref="P:System.Web.Configuration.SiteMapSection.Providers" /> property or a <see cref="F:System.String.Empty" /> field. The default is <see langword="&quot;AspNetXmlSiteMapProvider&quot;" />.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetXmlSiteMapProvider")]
	public string DefaultProvider
	{
		get
		{
			return (string)base["defaultProvider"];
		}
		set
		{
			base["defaultProvider"] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the ASP.NET site map feature is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the ASP.NET site map feature is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("enabled", DefaultValue = "True")]
	public bool Enabled
	{
		get
		{
			return (bool)base["enabled"];
		}
		set
		{
			base["enabled"] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
	/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the providers settings defined within the <see langword="providers" /> subsection of the <see langword="siteMap" /> section of the configuration file.</returns>
	[ConfigurationProperty("providers")]
	public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base["providers"];

	internal SiteMapProviderCollection ProvidersInternal
	{
		get
		{
			if (providers == null)
			{
				SiteMapProviderCollection siteMapProviderCollection = new SiteMapProviderCollection();
				ProvidersHelper.InstantiateProviders(Providers, siteMapProviderCollection, typeof(SiteMapProvider));
				providers = siteMapProviderCollection;
			}
			return providers;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static SiteMapSection()
	{
		defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetXmlSiteMapProvider");
		enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);
		providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));
		properties = new ConfigurationPropertyCollection();
		properties.Add(defaultProviderProp);
		properties.Add(enabledProp);
		properties.Add(providersProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.SiteMapSection" /> class by using default settings.</summary>
	public SiteMapSection()
	{
	}
}
