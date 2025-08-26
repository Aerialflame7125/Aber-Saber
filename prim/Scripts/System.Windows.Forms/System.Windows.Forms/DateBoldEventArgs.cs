namespace System.Windows.Forms;

/// <summary>Provides data for events that are internal to the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class DateBoldEventArgs : EventArgs
{
	private int size;

	private DateTime start;

	private int[] days_to_bold;

	/// <summary>Gets or sets dates that are bold.</summary>
	/// <returns>The dates that are bold.</returns>
	/// <filterpriority>1</filterpriority>
	public int[] DaysToBold
	{
		get
		{
			return days_to_bold;
		}
		set
		{
			days_to_bold = value;
		}
	}

	/// <summary>Gets the number of dates that are bold.</summary>
	/// <returns>The number of dates that are bold.</returns>
	/// <filterpriority>1</filterpriority>
	public int Size => size;

	/// <summary>Gets the first date that is bold.</summary>
	/// <returns>The first date that is bold.</returns>
	/// <filterpriority>1</filterpriority>
	public DateTime StartDate => start;

	private DateBoldEventArgs(DateTime start, int size, int[] daysToBold)
	{
		this.start = start;
		this.size = size;
		days_to_bold = daysToBold;
	}
}
