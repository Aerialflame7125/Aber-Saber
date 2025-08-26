namespace System.Windows.Forms;

/// <summary>Defines identifiers that indicate the current battery charge level or charging state information.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
public enum BatteryChargeStatus
{
	/// <summary>Indicates a high level of battery charge.</summary>
	High = 1,
	/// <summary>Indicates a low level of battery charge.</summary>
	Low = 2,
	/// <summary>Indicates a critically low level of battery charge.</summary>
	Critical = 4,
	/// <summary>Indicates a battery is charging.</summary>
	Charging = 8,
	/// <summary>Indicates that no battery is present.</summary>
	NoSystemBattery = 0x80,
	/// <summary>Indicates an unknown battery condition.</summary>
	Unknown = 0xFF
}
