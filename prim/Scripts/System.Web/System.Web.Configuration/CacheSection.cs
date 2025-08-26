using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the global cache settings for an ASP.NET application. This class cannot be inherited.</summary>
public sealed class CacheSection : ConfigurationSection
{
	private static ConfigurationProperty disableExpirationProp;

	private static ConfigurationProperty disableMemoryCollectionProp;

	private static ConfigurationProperty percentagePhysicalMemoryUsedLimitProp;

	private static ConfigurationProperty privateBytesLimitProp;

	private static ConfigurationProperty privateBytesPollTimeProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value indicating whether the cache expiration is disabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the cache expiration is disabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("disableExpiration", DefaultValue = "False")]
	public bool DisableExpiration
	{
		get
		{
			return (bool)base[disableExpirationProp];
		}
		set
		{
			base[disableExpirationProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the cache memory collection is disabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the cache memory collection is disabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("disableMemoryCollection", DefaultValue = "False")]
	public bool DisableMemoryCollection
	{
		get
		{
			return (bool)base[disableMemoryCollectionProp];
		}
		set
		{
			base[disableMemoryCollectionProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the maximum percentage of virtual memory usage.</summary>
	/// <returns>The maximum percentage of virtual memory usage. The default value is 90%.</returns>
	[IntegerValidator(MinValue = 0, MaxValue = 100)]
	[ConfigurationProperty("percentagePhysicalMemoryUsedLimit", DefaultValue = "0")]
	public int PercentagePhysicalMemoryUsedLimit
	{
		get
		{
			return (int)base[percentagePhysicalMemoryUsedLimitProp];
		}
		set
		{
			base[percentagePhysicalMemoryUsedLimitProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the maximum size of the working-process private space.</summary>
	/// <returns>The maximum number, in bytes, of the private space allocated to the working process. The default value is 0.</returns>
	[LongValidator(MinValue = 0L, MaxValue = long.MaxValue)]
	[ConfigurationProperty("privateBytesLimit", DefaultValue = "0")]
	public long PrivateBytesLimit
	{
		get
		{
			return (long)base[privateBytesLimitProp];
		}
		set
		{
			base[privateBytesLimitProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the time interval between polling for the worker-process memory usage.</summary>
	/// <returns>The time interval between polling for the worker process memory usage. The default value is 2 minutes.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[ConfigurationProperty("privateBytesPollTime", DefaultValue = "00:02:00")]
	public TimeSpan PrivateBytesPollTime
	{
		get
		{
			return (TimeSpan)base[privateBytesPollTimeProp];
		}
		set
		{
			base[privateBytesPollTimeProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static CacheSection()
	{
		disableExpirationProp = new ConfigurationProperty("disableExpiration", typeof(bool), false);
		disableMemoryCollectionProp = new ConfigurationProperty("disableMemoryCollection", typeof(bool), false);
		percentagePhysicalMemoryUsedLimitProp = new ConfigurationProperty("percentagePhysicalMemoryUsedLimit", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		privateBytesLimitProp = new ConfigurationProperty("privateBytesLimit", typeof(long), 0L, TypeDescriptor.GetConverter(typeof(long)), new LongValidator(0L, long.MaxValue), ConfigurationPropertyOptions.None);
		privateBytesPollTimeProp = new ConfigurationProperty("privateBytesPollTime", typeof(TimeSpan), TimeSpan.FromMinutes(2.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(disableExpirationProp);
		properties.Add(disableMemoryCollectionProp);
		properties.Add(percentagePhysicalMemoryUsedLimitProp);
		properties.Add(privateBytesLimitProp);
		properties.Add(privateBytesPollTimeProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.CacheSection" /> class.</summary>
	public CacheSection()
	{
	}
}
