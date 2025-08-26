namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Form.FormClosed" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class FormClosedEventArgs : EventArgs
{
	private CloseReason close_reason;

	/// <summary>Gets a value that indicates why the form was closed. </summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.CloseReason" /> enumerated values. </returns>
	/// <filterpriority>1</filterpriority>
	public CloseReason CloseReason => close_reason;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FormClosedEventArgs" /> class.</summary>
	/// <param name="closeReason">A <see cref="T:System.Windows.Forms.CloseReason" /> value that represents the reason why the form was closed.</param>
	public FormClosedEventArgs(CloseReason closeReason)
	{
		close_reason = closeReason;
	}
}
