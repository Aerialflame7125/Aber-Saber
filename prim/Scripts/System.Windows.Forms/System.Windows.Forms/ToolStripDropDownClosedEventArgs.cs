namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closed" /> event. </summary>
public class ToolStripDropDownClosedEventArgs : EventArgs
{
	private ToolStripDropDownCloseReason close_reason;

	/// <summary>Gets the reason that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> closed.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</returns>
	public ToolStripDropDownCloseReason CloseReason => close_reason;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs" /> class. </summary>
	/// <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</param>
	public ToolStripDropDownClosedEventArgs(ToolStripDropDownCloseReason reason)
	{
		close_reason = reason;
	}
}
