namespace System.Windows.Forms;

/// <summary>Specifies the date and time format the <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays.</summary>
/// <filterpriority>2</filterpriority>
public enum DateTimePickerFormat
{
	/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in a custom format. For more information, see <see cref="P:System.Windows.Forms.DateTimePicker.CustomFormat" />.</summary>
	Custom = 8,
	/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in the long date format set by the user's operating system.</summary>
	Long = 1,
	/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in the short date format set by the user's operating system.</summary>
	Short = 2,
	/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in the time format set by the user's operating system.</summary>
	Time = 4
}
