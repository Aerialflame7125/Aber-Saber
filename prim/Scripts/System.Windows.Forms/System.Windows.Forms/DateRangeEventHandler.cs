namespace System.Windows.Forms;

/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.MonthCalendar.DateChanged" /> or <see cref="E:System.Windows.Forms.MonthCalendar.DateSelected" /> event of a <see cref="T:System.Windows.Forms.MonthCalendar" />.</summary>
/// <param name="sender">The source of the event. </param>
/// <param name="e">A <see cref="T:System.Windows.Forms.DateRangeEventArgs" /> that contains the event data. </param>
/// <filterpriority>2</filterpriority>
public delegate void DateRangeEventHandler(object sender, DateRangeEventArgs e);
