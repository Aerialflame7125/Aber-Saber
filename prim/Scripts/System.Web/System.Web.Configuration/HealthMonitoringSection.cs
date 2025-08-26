using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures ASP.NET profiles that determine how health-monitoring events are sent to event providers. This class cannot be inherited.</summary>
public sealed class HealthMonitoringSection : ConfigurationSection
{
	private static ConfigurationProperty bufferModesProp;

	private static ConfigurationProperty enabledProp;

	private static ConfigurationProperty eventMappingsProp;

	private static ConfigurationProperty heartbeatIntervalProp;

	private static ConfigurationProperty profilesProp;

	private static ConfigurationProperty providersProp;

	private static ConfigurationProperty rulesProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets a collection of objects that specify the settings for the buffer modes.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.BufferModesCollection" /> collection of <see cref="T:System.Web.Configuration.BufferModeSettings" /> objects.</returns>
	[ConfigurationProperty("bufferModes")]
	public BufferModesCollection BufferModes => (BufferModesCollection)base[bufferModesProp];

	/// <summary>Gets or sets a value indicating whether health monitoring is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if health monitoring is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("enabled", DefaultValue = "True")]
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

	/// <summary>Gets a <see cref="T:System.Web.Configuration.EventMappingSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.EventMappingSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.EventMappingSettings" /> objects. The default is an empty <see cref="T:System.Web.Configuration.EventMappingSettingsCollection" /> collection.</returns>
	[ConfigurationProperty("eventMappings")]
	public EventMappingSettingsCollection EventMappings => (EventMappingSettingsCollection)base[eventMappingsProp];

	/// <summary>Gets or sets the interval used by the application domain when it raises the <see cref="T:System.Web.Management.WebHeartbeatEvent" /> event.</summary>
	/// <returns>The interval used by the application domain when it raises the <see cref="T:System.Web.Management.WebHeartbeatEvent" /> event.</returns>
	[TypeConverter(typeof(TimeSpanSecondsConverter))]
	[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "24.20:31:23")]
	[ConfigurationProperty("heartbeatInterval", DefaultValue = "00:00:00")]
	public TimeSpan HeartbeatInterval
	{
		get
		{
			return (TimeSpan)base[heartbeatIntervalProp];
		}
		set
		{
			base[heartbeatIntervalProp] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.Configuration.ProfileSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.ProfileSettings" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.ProfileSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.ProfileSettings" /> objects. The default is an empty <see cref="T:System.Web.Configuration.ProfileSettingsCollection" /> collection.</returns>
	[ConfigurationProperty("profiles")]
	public ProfileSettingsCollection Profiles => (ProfileSettingsCollection)base[profilesProp];

	/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of <see cref="T:System.Configuration.ProviderSettings" /> objects.</summary>
	/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection. The default is an empty <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection.</returns>
	[ConfigurationProperty("providers")]
	public ProviderSettingsCollection Providers => (ProviderSettingsCollection)base[providersProp];

	/// <summary>Gets a <see cref="T:System.Web.Configuration.RuleSettingsCollection" /> collection of <see cref="T:System.Web.Configuration.RuleSettings" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.RuleSettingsCollection" /> collection. The default is an empty <see cref="T:System.Web.Configuration.RuleSettingsCollection" /> collection</returns>
	[ConfigurationProperty("rules")]
	public RuleSettingsCollection Rules => (RuleSettingsCollection)base[rulesProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static HealthMonitoringSection()
	{
		bufferModesProp = new ConfigurationProperty("bufferModes", typeof(BufferModesCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);
		eventMappingsProp = new ConfigurationProperty("eventMappings", typeof(EventMappingSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		heartbeatIntervalProp = new ConfigurationProperty("heartbeatInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.TimeSpanSecondsConverter, new TimeSpanValidator(TimeSpan.Zero, new TimeSpan(24, 30, 31, 23)), ConfigurationPropertyOptions.None);
		profilesProp = new ConfigurationProperty("profiles", typeof(ProfileSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		rulesProp = new ConfigurationProperty("rules", typeof(RuleSettingsCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(bufferModesProp);
		properties.Add(enabledProp);
		properties.Add(eventMappingsProp);
		properties.Add(heartbeatIntervalProp);
		properties.Add(profilesProp);
		properties.Add(providersProp);
		properties.Add(rulesProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HealthMonitoringSection" /> class using default settings.</summary>
	public HealthMonitoringSection()
	{
	}
}
