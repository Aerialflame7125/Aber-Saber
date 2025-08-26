namespace System.Windows.Forms;

/// <summary>Provides information specifying how acceleration should be performed on a spin box (also known as an up-down control) when the up or down button is pressed for specified time period.</summary>
public class NumericUpDownAcceleration
{
	private decimal increment;

	private int seconds;

	/// <summary>Gets or sets the quantity to increment or decrement the displayed value during acceleration.</summary>
	/// <returns>The quantity to increment or decrement the displayed value during acceleration.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 0.</exception>
	public decimal Increment
	{
		get
		{
			return increment;
		}
		set
		{
			increment = value;
		}
	}

	/// <summary>Gets or sets the number of seconds the up or down button must be pressed before the acceleration starts.</summary>
	/// <returns>Gets or sets the number of seconds the up or down button must be pressed before the acceleration starts.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than 0.</exception>
	public int Seconds
	{
		get
		{
			return seconds;
		}
		set
		{
			seconds = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> class.</summary>
	/// <param name="seconds">The number of seconds the up or down button is pressed before the acceleration starts. </param>
	/// <param name="increment">The quantity the value displayed in the control should be incremented or decremented during acceleration.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="seconds" /> or <paramref name="increment" /> is less than 0.</exception>
	public NumericUpDownAcceleration(int seconds, decimal increment)
	{
		if (seconds < 0)
		{
			throw new ArgumentOutOfRangeException("Invalid seconds value. The seconds value must be equal or greater than zero.");
		}
		if (increment < 0m)
		{
			throw new ArgumentOutOfRangeException("Invalid increment value. The increment value must be equal or greater than zero.");
		}
		this.increment = increment;
		this.seconds = seconds;
	}
}
