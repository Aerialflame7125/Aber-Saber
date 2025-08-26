using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the ASP.NET event rules. This class cannot be inherited.</summary>
public sealed class RuleSettings : ConfigurationElement
{
	private static ConfigurationProperty customProp;

	private static ConfigurationProperty eventNameProp;

	private static ConfigurationProperty maxLimitProp;

	private static ConfigurationProperty minInstancesProp;

	private static ConfigurationProperty minIntervalProp;

	private static ConfigurationProperty nameProp;

	private static ConfigurationProperty profileProp;

	private static ConfigurationProperty providerProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</summary>
	/// <returns>The fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</returns>
	[ConfigurationProperty("custom", DefaultValue = "")]
	public string Custom
	{
		get
		{
			return (string)base[customProp];
		}
		set
		{
			base[customProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</summary>
	/// <returns>The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</returns>
	[ConfigurationProperty("eventName", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
	public string EventName
	{
		get
		{
			return (string)base[eventNameProp];
		}
		set
		{
			base[eventNameProp] = value;
		}
	}

	/// <summary>Gets or sets the maximum number of times events of the same type are raised.</summary>
	/// <returns>The maximum number of times events of the same type are raised. The default value is <see cref="F:System.Int32.MaxValue" />.</returns>
	[TypeConverter(typeof(InfiniteIntConverter))]
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("maxLimit", DefaultValue = "2147483647")]
	public int MaxLimit
	{
		get
		{
			return (int)base[maxLimitProp];
		}
		set
		{
			base[maxLimitProp] = value;
		}
	}

	/// <summary>Gets or sets the minimum number of occurrences of the same type of event before the event is raised to the provider.</summary>
	/// <returns>The minimum number of occurrences of the same type of event before the event is raised to the provider. The default value is 1.</returns>
	[IntegerValidator(MinValue = 1, MaxValue = int.MaxValue)]
	[ConfigurationProperty("minInstances", DefaultValue = "1")]
	public int MinInstances
	{
		get
		{
			return (int)base[minInstancesProp];
		}
		set
		{
			base[minInstancesProp] = value;
		}
	}

	/// <summary>Gets or sets the minimum time interval between two events of the same type.</summary>
	/// <returns>The minimum time interval between two events of the same type. The default value is 0 ticks.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[ConfigurationProperty("minInterval", DefaultValue = "00:00:00")]
	public TimeSpan MinInterval
	{
		get
		{
			return (TimeSpan)base[minIntervalProp];
		}
		set
		{
			base[minIntervalProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object.</summary>
	/// <returns>The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object. The default value is an empty string ("").</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("name", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string Name
	{
		get
		{
			return (string)base[nameProp];
		}
		set
		{
			base[nameProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</summary>
	/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</returns>
	[ConfigurationProperty("profile", DefaultValue = "")]
	public string Profile
	{
		get
		{
			return (string)base[profileProp];
		}
		set
		{
			base[profileProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</summary>
	/// <returns>The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</returns>
	[ConfigurationProperty("provider", DefaultValue = "")]
	public string Provider
	{
		get
		{
			return (string)base[providerProp];
		}
		set
		{
			base[providerProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static RuleSettings()
	{
		customProp = new ConfigurationProperty("custom", typeof(string), "");
		eventNameProp = new ConfigurationProperty("eventName", typeof(string), "", ConfigurationPropertyOptions.IsRequired);
		maxLimitProp = new ConfigurationProperty("maxLimit", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		minInstancesProp = new ConfigurationProperty("minInstances", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);
		minIntervalProp = new ConfigurationProperty("minInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.None);
		nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		profileProp = new ConfigurationProperty("profile", typeof(string), "");
		providerProp = new ConfigurationProperty("provider", typeof(string), "");
		properties = new ConfigurationPropertyCollection();
		properties.Add(customProp);
		properties.Add(eventNameProp);
		properties.Add(maxLimitProp);
		properties.Add(minInstancesProp);
		properties.Add(minIntervalProp);
		properties.Add(nameProp);
		properties.Add(profileProp);
		properties.Add(providerProp);
	}

	internal RuleSettings()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class where all values are specified.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object to create.</param>
	/// <param name="eventName">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</param>
	/// <param name="provider">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</param>
	/// <param name="profile">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</param>
	/// <param name="minInstances">The minimum number of occurrences of an event of the same type before the event is fired to the provider. </param>
	/// <param name="maxLimit">The maximum number of times events of the same type are fired.</param>
	/// <param name="minInterval">The minimum time interval between two events of the same type.</param>
	/// <param name="custom">The fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</param>
	public RuleSettings(string name, string eventName, string provider, string profile, int minInstances, int maxLimit, TimeSpan minInterval, string custom)
	{
		Name = name;
		EventName = eventName;
		Provider = provider;
		Profile = profile;
		MinInstances = minInstances;
		MaxLimit = maxLimit;
		MinInterval = minInterval;
		Custom = custom;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RuleSettings" /> class where all values except those of the <see cref="P:System.Web.Configuration.RuleSettings.Custom" /> class are specified.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object to create.</param>
	/// <param name="eventName">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</param>
	/// <param name="provider">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</param>
	/// <param name="profile">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object this rule applies to.</param>
	/// <param name="minInstances">The minimum number of occurrences of the same type of event that can occur before the event is raised to the provider. </param>
	/// <param name="maxLimit">The maximum number of times events of the same type can be raised.</param>
	/// <param name="minInterval">The minimum time interval between two events of the same type.</param>
	public RuleSettings(string name, string eventName, string provider, string profile, int minInstances, int maxLimit, TimeSpan minInterval)
	{
		Name = name;
		EventName = eventName;
		Provider = provider;
		Profile = profile;
		MinInstances = minInstances;
		MaxLimit = maxLimit;
		MinInterval = minInterval;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RuleSettings" /> class using default settings; however, the name, event name, and provider are specified.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.RuleSettings" /> object to create.</param>
	/// <param name="eventName">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object this rule applies to.</param>
	/// <param name="provider">The name of the <see cref="T:System.Configuration.ProviderSettings" /> object this rule applies to.</param>
	public RuleSettings(string name, string eventName, string provider)
	{
		Name = name;
		EventName = eventName;
		Provider = provider;
	}
}
