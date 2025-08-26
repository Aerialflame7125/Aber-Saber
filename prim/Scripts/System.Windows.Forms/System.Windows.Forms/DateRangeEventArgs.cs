namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.MonthCalendar.DateChanged" /> or <see cref="E:System.Windows.Forms.MonthCalendar.DateSelected" /> events of the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class DateRangeEventArgs : EventArgs
{
	private DateTime end;

	private DateTime start;

	/// <summary>Gets the last date/time value in the range that the user has selected.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> that represents the last date in the date range that the user has selected.</returns>
	/// <filterpriority>1</filterpriority>
	public DateTime End => end;

	/// <summary>Gets the first date/time value in the range that the user has selected.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> that represents the first date in the date range that the user has selected.</returns>
	/// <filterpriority>1</filterpriority>
	public DateTime Start => start;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DateRangeEventArgs" /> class.</summary>
	/// <param name="start">The first date/time value in the range that the user has selected. </param>
	/// <param name="end">The last date/time value in the range that the user has selected. </param>
	public DateRangeEventArgs(DateTime start, DateTime end)
	{
		this.start = start;
		this.end = end;
	}
}
