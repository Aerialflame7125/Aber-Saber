namespace System.Windows.Forms;

/// <summary>Indicates current system power status information.</summary>
/// <filterpriority>2</filterpriority>
public class PowerStatus
{
	private BatteryChargeStatus battery_charge_status;

	private int battery_full_lifetime;

	private float battery_life_percent;

	private int battery_life_remaining;

	private PowerLineStatus power_line_status;

	/// <summary>Gets the current battery charge status.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BatteryChargeStatus" /> values indicating the current battery charge level or charging status.</returns>
	/// <filterpriority>1</filterpriority>
	public BatteryChargeStatus BatteryChargeStatus => battery_charge_status;

	/// <summary>Gets the reported full charge lifetime of the primary battery power source in seconds.</summary>
	/// <returns>The reported number of seconds of battery life available when the battery is fully charged, or -1 if the battery life is unknown.</returns>
	/// <filterpriority>1</filterpriority>
	public int BatteryFullLifetime => battery_full_lifetime;

	/// <summary>Gets the approximate amount of full battery charge remaining.</summary>
	/// <returns>The approximate amount, from 0.0 to 1.0, of full battery charge remaining.</returns>
	/// <filterpriority>1</filterpriority>
	public float BatteryLifePercent => battery_life_percent;

	/// <summary>Gets the approximate number of seconds of battery time remaining.</summary>
	/// <returns>The approximate number of seconds of battery life remaining, or –1 if the approximate remaining battery life is unknown.</returns>
	/// <filterpriority>1</filterpriority>
	public int BatteryLifeRemaining => battery_life_remaining;

	/// <summary>Gets the current system power status.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.PowerLineStatus" /> values indicating the current system power status.</returns>
	public PowerLineStatus PowerLineStatus => power_line_status;

	internal PowerStatus(BatteryChargeStatus batteryChargeStatus, int batteryFullLifetime, float batteryLifePercent, int batteryLifeRemaining, PowerLineStatus powerLineStatus)
	{
		battery_charge_status = batteryChargeStatus;
		battery_full_lifetime = batteryFullLifetime;
		battery_life_percent = batteryLifePercent;
		battery_life_remaining = batteryLifeRemaining;
		power_line_status = powerLineStatus;
	}
}
