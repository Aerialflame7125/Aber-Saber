using System.ComponentModel;

namespace System.Diagnostics;

/// <summary>Defines the counter type, name, and Help string for a custom counter.</summary>
[Serializable]
[TypeConverter("System.Diagnostics.Design.CounterCreationDataConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public class CounterCreationData
{
	private string help = string.Empty;

	private string name;

	private PerformanceCounterType type;

	/// <summary>Gets or sets the custom counter's description.</summary>
	/// <returns>The text that describes the counter's behavior.</returns>
	/// <exception cref="T:System.ArgumentNullException">The specified value is <see langword="null" />.</exception>
	[DefaultValue("")]
	[MonitoringDescription("Description of this counter.")]
	public string CounterHelp
	{
		get
		{
			return help;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			help = value;
		}
	}

	/// <summary>Gets or sets the name of the custom counter.</summary>
	/// <returns>A name for the counter, which is unique in its category.</returns>
	/// <exception cref="T:System.ArgumentNullException">The specified value is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The specified value is not between 1 and 80 characters long or contains double quotes, control characters or leading or trailing spaces.</exception>
	[DefaultValue("")]
	[MonitoringDescription("Name of this counter.")]
	[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string CounterName
	{
		get
		{
			return name;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value == "")
			{
				throw new ArgumentException("value");
			}
			name = value;
		}
	}

	/// <summary>Gets or sets the performance counter type of the custom counter.</summary>
	/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that defines the behavior of the performance counter.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">You have specified a type that is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterType" /> enumeration.</exception>
	[DefaultValue(typeof(PerformanceCounterType), "NumberOfItems32")]
	[MonitoringDescription("Type of this counter.")]
	public PerformanceCounterType CounterType
	{
		get
		{
			return type;
		}
		set
		{
			if (!Enum.IsDefined(typeof(PerformanceCounterType), value))
			{
				throw new InvalidEnumArgumentException();
			}
			type = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class, to a counter of type <see langword="NumberOfItems32" />, and with empty name and help strings.</summary>
	public CounterCreationData()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class, to a counter of the specified type, using the specified counter name and Help strings.</summary>
	/// <param name="counterName">The name of the counter, which must be unique within its category.</param>
	/// <param name="counterHelp">The text that describes the counter's behavior.</param>
	/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that identifies the counter's behavior.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">You have specified a value for <paramref name="counterType" /> that is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterType" /> enumeration.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="counterHelp" /> is <see langword="null" />.</exception>
	public CounterCreationData(string counterName, string counterHelp, PerformanceCounterType counterType)
	{
		CounterName = counterName;
		CounterHelp = counterHelp;
		CounterType = counterType;
	}
}
