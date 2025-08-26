using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class FormClosingEventArgs : CancelEventArgs
{
	private CloseReason close_reason;

	/// <summary>Gets a value that indicates why the form is being closed.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.CloseReason" /> enumerated values. </returns>
	/// <filterpriority>1</filterpriority>
	public CloseReason CloseReason => close_reason;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> class.</summary>
	/// <param name="closeReason">A <see cref="T:System.Windows.Forms.CloseReason" /> value that represents the reason why the form is being closed.</param>
	/// <param name="cancel">true to cancel the event; otherwise, false.</param>
	public FormClosingEventArgs(CloseReason closeReason, bool cancel)
		: base(cancel)
	{
		close_reason = closeReason;
	}
}
