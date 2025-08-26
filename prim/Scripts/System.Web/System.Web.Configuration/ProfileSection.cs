using System.Configuration;

namespace System.Web.Configuration;

/// <summary>The <see cref="T:System.Web.Configuration.ProfileSection" /> class provides a way to programmatically access and modify the <see langword="profile" /> section of a configuration file. This class cannot be inherited.</summary>
public sealed class ProfileSection : ConfigurationSection
{
	private static ConfigurationProperty automaticSaveEnabledProp;

	private static ConfigurationProperty defaultProviderProp;

	private static ConfigurationProperty enabledProp;

	private static ConfigurationProperty inheritsProp;

	private static ConfigurationProperty propertySettingsProp;

	private static ConfigurationProperty providersProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value that determines whether changes to user-profile information are automatically saved on page exit.</summary>
	/// <returns>
	///     <see langword="true" /> if profile information is automatically saved on page exit; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("automaticSaveEnabled", DefaultValue = true)]
	public bool AutomaticSaveEnabled
	{
		get
		{
			return (bool)base[automaticSaveEnabledProp];
		}
		set
		{
			base[automaticSaveEnabledProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the default profile provider. </summary>
	/// <returns>The name of a provider in the <see cref="P:System.Web.Configuration.ProfileSection.Providers" /> collection, or an empty string (""). The default is "AspNetSqlProfileProvider."</returns>
	[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetSqlProfileProvider")]
	[StringValidator(MinLength = 1)]
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

	/// <summary>Gets or sets a value indicating whether the ASP.NET profile feature is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the ASP.NET compilation system should generate a <see langword="ProfileCommon" /> class that can be used to access information about individual user profiles; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("enabled", DefaultValue = true)]
	public bool Enabled
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

	/// <summary>Gets or sets a type reference for a custom type derived from <see cref="T:System.Web.Profile.ProfileBase" />.</summary>
	/// <returns>A valid type reference, or an empty string (""). The default is an empty string.</returns>
	[ConfigurationProperty("inherits", DefaultValue = "")]
	public string Inherits
	{
		get
		{
			return (string)base[inheritsProp];
		}
		set
		{
			base[inheritsProp] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> collection of <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object that contains all the properties defined within the <see langword="properties" /> subsection of the <see langword="profile" /> section of the configuration file.</returns>
	[ConfigurationProperty("properties")]
	public RootProfilePropertySettingsCollection PropertySettings => (RootProfilePropertySettingsCollection)base[propertySettingsProp];

	/// <summary>Gets a collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
	/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> that contains the providers defined within the <see langword="providers" /> subsection of the <see langword="profile" /> section of the configuration file.</returns>
	[ConfigurationProperty("providers")]
	public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base[providersProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ProfileSection()
	{
		automaticSaveEnabledProp = new ConfigurationProperty("automaticSaveEnabled", typeof(bool), true);
		defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetSqlProfileProvider");
		enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);
		inheritsProp = new ConfigurationProperty("inherits", typeof(string), "");
		propertySettingsProp = new ConfigurationProperty("properties", typeof(RootProfilePropertySettingsCollection));
		providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));
		properties = new ConfigurationPropertyCollection();
		properties.Add(automaticSaveEnabledProp);
		properties.Add(defaultProviderProp);
		properties.Add(enabledProp);
		properties.Add(inheritsProp);
		properties.Add(propertySettingsProp);
		properties.Add(providersProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileSection" /> class using default settings.</summary>
	public ProfileSection()
	{
	}
}
