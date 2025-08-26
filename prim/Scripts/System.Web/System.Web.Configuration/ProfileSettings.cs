using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the ASP.NET event profiles. This class cannot be inherited.</summary>
public sealed class ProfileSettings : ConfigurationElement
{
	private static ConfigurationProperty customProp;

	private static ConfigurationProperty maxLimitProp;

	private static ConfigurationProperty minInstancesProp;

	private static ConfigurationProperty minIntervalProp;

	private static ConfigurationProperty nameProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the fully qualified type of a custom class that implements the <see cref="T:System.Web.Management.IWebEventCustomEvaluator" /> interface.</summary>
	/// <returns>The fully qualified type of a custom class that implements the <see cref="T:System.Web.Management.IWebEventCustomEvaluator" /> interface. The default is an empty string ("").</returns>
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

	/// <summary>Gets or sets the maximum number of times events of the same type are raised.</summary>
	/// <returns>The maximum number of times events of the same type are raised. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
	[TypeConverter(typeof(InfiniteIntConverter))]
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("maxLimit", DefaultValue = int.MaxValue)]
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

	/// <summary>Gets or sets the minimum number of event occurrences before the event is raised to the provider.</summary>
	/// <returns>The minimum number of event occurrences before the event is fired to the provider. The default is <see langword="1" />.</returns>
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

	/// <summary>Gets or sets the minimum interval between two events of the same type.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> that specifies the minimum interval between two events of the same type. The default is <see cref="F:System.TimeSpan.Zero" />.</returns>
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

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object.</summary>
	/// <returns>The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object. The default is an empty string("").</returns>
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

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ProfileSettings()
	{
		customProp = new ConfigurationProperty("custom", typeof(string), "");
		maxLimitProp = new ConfigurationProperty("maxLimit", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		minInstancesProp = new ConfigurationProperty("minInstances", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);
		minIntervalProp = new ConfigurationProperty("minInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		properties = new ConfigurationPropertyCollection();
		properties.Add(customProp);
		properties.Add(maxLimitProp);
		properties.Add(minInstancesProp);
		properties.Add(minIntervalProp);
		properties.Add(nameProp);
	}

	internal ProfileSettings()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileSettings" /> class. using the specified name for the new instance of the class.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object to create.</param>
	public ProfileSettings(string name)
	{
		Name = name;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class, using the specified settings for the new instance of the class.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object to create.</param>
	/// <param name="minInstances">The minimum number of event occurrences before the event is raised to the provider. </param>
	/// <param name="maxLimit">The maximum number of times events of the same type are raised.</param>
	/// <param name="minInterval">A <see cref="T:System.TimeSpan" /> that specifies the minimum interval between two events of the same type.</param>
	/// <param name="custom">The fully qualified type of a custom class that implements <see cref="T:System.Web.Management.IWebEventCustomEvaluator" />.</param>
	public ProfileSettings(string name, int minInstances, int maxLimit, TimeSpan minInterval, string custom)
	{
		Name = name;
		MinInstances = minInstances;
		MaxLimit = maxLimit;
		MinInterval = MinInterval;
		Custom = custom;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProfileSettings" /> class, using specified settings for the new instance of the class.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.ProfileSettings" /> object to create.</param>
	/// <param name="minInstances">The minimum number of event occurrences before the event is raised to the provider. </param>
	/// <param name="maxLimit">The maximum number of times events of the same type are raised.</param>
	/// <param name="minInterval">A <see cref="T:System.TimeSpan" /> that specifies the minimum length of the interval between the times when two events of the same type are raised.</param>
	public ProfileSettings(string name, int minInstances, int maxLimit, TimeSpan minInterval)
	{
		Name = name;
		MinInstances = minInstances;
		MaxLimit = maxLimit;
		MinInterval = MinInterval;
	}
}
