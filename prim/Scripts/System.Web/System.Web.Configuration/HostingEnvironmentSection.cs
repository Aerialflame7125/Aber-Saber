using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Defines configuration settings that control the behavior of the application hosting environment. This class cannot be inherited.</summary>
public sealed class HostingEnvironmentSection : ConfigurationSection
{
	private static ConfigurationProperty idleTimeoutProp;

	private static ConfigurationProperty shadowCopyBinAssembliesProp;

	private static ConfigurationProperty shutdownTimeoutProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the amount of time, in minutes, before unloading an inactive application.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> that is the specified number of minutes before unloading an inactive application. </returns>
	[TypeConverter(typeof(TimeSpanMinutesOrInfiniteConverter))]
	[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
	[ConfigurationProperty("idleTimeout", DefaultValue = "10675199.02:48:05.4775807")]
	public TimeSpan IdleTimeout
	{
		get
		{
			return (TimeSpan)base[idleTimeoutProp];
		}
		set
		{
			base[idleTimeoutProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the assemblies of an application in Bin are shadow copied to the application's ASP.NET Temporary Files directory. </summary>
	/// <returns>
	///     <see langword="true" /> if the assemblies of an application in Bin are shadow copied to the application's ASP.NET Temporary Files directory; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("shadowCopyBinAssemblies", DefaultValue = "True")]
	public bool ShadowCopyBinAssemblies
	{
		get
		{
			return (bool)base[shadowCopyBinAssembliesProp];
		}
		set
		{
			base[shadowCopyBinAssembliesProp] = value;
		}
	}

	/// <summary>Gets or sets the amount of time, in seconds, to gracefully shut down the application.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> that is the specified number of seconds to gracefully shut down the application. The default is 30 seconds.</returns>
	[TypeConverter(typeof(TimeSpanSecondsConverter))]
	[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
	[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:00:30")]
	public TimeSpan ShutdownTimeout
	{
		get
		{
			return (TimeSpan)base[shutdownTimeoutProp];
		}
		set
		{
			base[shutdownTimeoutProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static HostingEnvironmentSection()
	{
		idleTimeoutProp = new ConfigurationProperty("idleTimeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.TimeSpanMinutesOrInfiniteConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		shadowCopyBinAssembliesProp = new ConfigurationProperty("shadowCopyBinAssemblies", typeof(bool), true);
		shutdownTimeoutProp = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(30.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(idleTimeoutProp);
		properties.Add(shadowCopyBinAssembliesProp);
		properties.Add(shutdownTimeoutProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HostingEnvironmentSection" /> class by using default settings.</summary>
	public HostingEnvironmentSection()
	{
	}
}
