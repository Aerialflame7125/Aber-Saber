using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the ASP.NET event-buffering settings for event providers. This class cannot be inherited.</summary>
public sealed class BufferModeSettings : ConfigurationElement
{
	private static ConfigurationProperty maxBufferSizeProp;

	private static ConfigurationProperty maxBufferThreadsProp;

	private static ConfigurationProperty maxFlushSizeProp;

	private static ConfigurationProperty nameProp;

	private static ConfigurationProperty regularFlushIntervalProp;

	private static ConfigurationProperty urgentFlushIntervalProp;

	private static ConfigurationProperty urgentFlushThresholdProp;

	private static ConfigurationPropertyCollection properties;

	private static ConfigurationElementProperty elementProperty;

	protected override ConfigurationElementProperty ElementProperty
	{
		protected internal get
		{
			return elementProperty;
		}
	}

	/// <summary>Gets or sets the maximum number of events that can be buffered at one time.</summary>
	/// <returns>The maximum number of events that can be buffered at one time.</returns>
	[TypeConverter(typeof(InfiniteIntConverter))]
	[IntegerValidator(MinValue = 1, MaxValue = int.MaxValue)]
	[ConfigurationProperty("maxBufferSize", DefaultValue = "2147483647", Options = ConfigurationPropertyOptions.IsRequired)]
	public int MaxBufferSize
	{
		get
		{
			return (int)base[maxBufferSizeProp];
		}
		set
		{
			base[maxBufferSizeProp] = value;
		}
	}

	/// <summary>Gets or sets the maximum number of flushing threads that can be active at one time.</summary>
	/// <returns>The maximum number of flushing threads that can be active at one time. The default is 1.</returns>
	[TypeConverter(typeof(InfiniteIntConverter))]
	[IntegerValidator(MinValue = 1, MaxValue = int.MaxValue)]
	[ConfigurationProperty("maxBufferThreads", DefaultValue = "1")]
	public int MaxBufferThreads
	{
		get
		{
			return (int)base[maxBufferThreadsProp];
		}
		set
		{
			base[maxBufferThreadsProp] = value;
		}
	}

	/// <summary>Gets or sets the maximum number of events per flush.</summary>
	/// <returns>The maximum number of events per flush.</returns>
	[TypeConverter(typeof(InfiniteIntConverter))]
	[IntegerValidator(MinValue = 1, MaxValue = int.MaxValue)]
	[ConfigurationProperty("maxFlushSize", DefaultValue = "2147483647", Options = ConfigurationPropertyOptions.IsRequired)]
	public int MaxFlushSize
	{
		get
		{
			return (int)base[maxFlushSizeProp];
		}
		set
		{
			base[maxFlushSizeProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object.</summary>
	/// <returns>The name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object. The default value is an empty string.</returns>
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

	/// <summary>Gets or sets the amount of time between buffer flushes.</summary>
	/// <returns>The regular amount of time between buffer flushes.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
	[ConfigurationProperty("regularFlushInterval", DefaultValue = "00:00:01", Options = ConfigurationPropertyOptions.IsRequired)]
	public TimeSpan RegularFlushInterval
	{
		get
		{
			return (TimeSpan)base[regularFlushIntervalProp];
		}
		set
		{
			base[regularFlushIntervalProp] = value;
		}
	}

	/// <summary>Gets or sets the minimum amount of time that can pass between buffer flushes. </summary>
	/// <returns>The minimum amount of time that can pass between buffer flushes.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[ConfigurationProperty("urgentFlushInterval", DefaultValue = "00:00:00", Options = ConfigurationPropertyOptions.IsRequired)]
	public TimeSpan UrgentFlushInterval
	{
		get
		{
			return (TimeSpan)base[urgentFlushIntervalProp];
		}
		set
		{
			base[urgentFlushIntervalProp] = value;
		}
	}

	/// <summary>Gets or sets the number of events that can be buffered before a flush is triggered.</summary>
	/// <returns>The number of events that can be buffered before a flush is triggered.</returns>
	[TypeConverter(typeof(InfiniteIntConverter))]
	[IntegerValidator(MinValue = 1, MaxValue = int.MaxValue)]
	[ConfigurationProperty("urgentFlushThreshold", DefaultValue = "2147483647", Options = ConfigurationPropertyOptions.IsRequired)]
	public int UrgentFlushThreshold
	{
		get
		{
			return (int)base[urgentFlushThresholdProp];
		}
		set
		{
			base[urgentFlushThresholdProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static BufferModeSettings()
	{
		IntegerValidator validator = new IntegerValidator(1, int.MaxValue);
		maxBufferSizeProp = new ConfigurationProperty("maxBufferSize", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, validator, ConfigurationPropertyOptions.IsRequired);
		maxBufferThreadsProp = new ConfigurationProperty("maxBufferThreads", typeof(int), 1, PropertyHelper.InfiniteIntConverter, validator, ConfigurationPropertyOptions.None);
		maxFlushSizeProp = new ConfigurationProperty("maxFlushSize", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, validator, ConfigurationPropertyOptions.IsRequired);
		nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		regularFlushIntervalProp = new ConfigurationProperty("regularFlushInterval", typeof(TimeSpan), TimeSpan.FromSeconds(1.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.IsRequired);
		urgentFlushIntervalProp = new ConfigurationProperty("urgentFlushInterval", typeof(TimeSpan), TimeSpan.FromSeconds(0.0), PropertyHelper.InfiniteTimeSpanConverter, null, ConfigurationPropertyOptions.IsRequired);
		urgentFlushThresholdProp = new ConfigurationProperty("urgentFlushThreshold", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, validator, ConfigurationPropertyOptions.IsRequired);
		properties = new ConfigurationPropertyCollection();
		properties.Add(nameProp);
		properties.Add(maxBufferSizeProp);
		properties.Add(maxBufferThreadsProp);
		properties.Add(maxFlushSizeProp);
		properties.Add(regularFlushIntervalProp);
		properties.Add(urgentFlushIntervalProp);
		properties.Add(urgentFlushThresholdProp);
		elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(BufferModeSettings), ValidateElement));
	}

	internal BufferModeSettings()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class using specified settings.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> object being created.</param>
	/// <param name="maxBufferSize">The maximum number of events buffered at one time. The value must be greater than zero.</param>
	/// <param name="maxFlushSize">The maximum number of events per buffer flush. Must be greater than zero.</param>
	/// <param name="urgentFlushThreshold">The number of events buffered before a buffer flush is triggered. The value must be greater than zero and less than or equal to <paramref name="maxBufferSize" />.</param>
	/// <param name="regularFlushInterval">The standard amount of time between buffer flushes. The value can be made infinite by setting it to <see cref="F:System.Int32.MaxValue" /> ticks.</param>
	/// <param name="urgentFlushInterval">The minimum length of time that can pass between buffer flushes. The value must be less than or equal to <paramref name="regularFlushInterval" />.</param>
	/// <param name="maxBufferThreads">The maximum number of buffer-flushing threads that can be active at one time.</param>
	public BufferModeSettings(string name, int maxBufferSize, int maxFlushSize, int urgentFlushThreshold, TimeSpan regularFlushInterval, TimeSpan urgentFlushInterval, int maxBufferThreads)
	{
		Name = name;
		MaxBufferSize = maxBufferSize;
		MaxFlushSize = maxFlushSize;
		UrgentFlushThreshold = urgentFlushThreshold;
		RegularFlushInterval = regularFlushInterval;
		UrgentFlushInterval = urgentFlushInterval;
		MaxBufferThreads = maxBufferThreads;
	}

	[MonoTODO("Should do some validation here")]
	private static void ValidateElement(object o)
	{
	}
}
