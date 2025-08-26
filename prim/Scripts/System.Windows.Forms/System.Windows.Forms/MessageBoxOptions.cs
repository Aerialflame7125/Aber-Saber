namespace System.Windows.Forms;

/// <summary>Specifies options on a <see cref="T:System.Windows.Forms.MessageBox" />.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
public enum MessageBoxOptions
{
	/// <summary>The message box is displayed on the active desktop.</summary>
	DefaultDesktopOnly = 0x20000,
	/// <summary>The message box text is right-aligned.</summary>
	RightAlign = 0x80000,
	/// <summary>Specifies that the message box text is displayed with right to left reading order.</summary>
	RtlReading = 0x100000,
	/// <summary>The message box is displayed on the active desktop.</summary>
	ServiceNotification = 0x200000
}
