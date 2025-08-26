namespace System.Windows.Forms;

/// <summary>Specifies the reason that a form was closed.</summary>
/// <filterpriority>2</filterpriority>
public enum CloseReason
{
	/// <summary>The cause of the closure was not defined or could not be determined.</summary>
	None,
	/// <summary>The operating system is closing all applications before shutting down.</summary>
	WindowsShutDown,
	/// <summary>The parent form of this multiple document interface (MDI) form is closing.</summary>
	MdiFormClosing,
	/// <summary>The user is closing the form through the user interface (UI), for example by clicking the Close button on the form window, selecting Close from the window's control menu, or pressing ALT+F4.</summary>
	UserClosing,
	/// <summary>The form is  closing because the user clicked End Task in Microsoft Windows Task Manager.  Note that if the user ends a process by clicking End Process, the form closes without raising the <see cref="E:System.Windows.Forms.Form.FormClosing" /> or <see cref="E:System.Windows.Forms.Form.FormClosed" /> event.</summary>
	TaskManagerClosing,
	/// <summary>The owner form is closing.</summary>
	FormOwnerClosing,
	/// <summary>The <see cref="M:System.Windows.Forms.Application.Exit" /> method of the <see cref="T:System.Windows.Forms.Application" /> class was invoked. </summary>
	ApplicationExitCall
}
