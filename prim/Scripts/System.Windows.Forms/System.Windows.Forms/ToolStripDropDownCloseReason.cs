namespace System.Windows.Forms;

/// <summary>Specifies the reason that a <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed.</summary>
public enum ToolStripDropDownCloseReason
{
	/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because another application has received the focus.</summary>
	AppFocusChange,
	/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because an application was launched.</summary>
	AppClicked,
	/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because one of its items was clicked.</summary>
	ItemClicked,
	/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because of keyboard activity, such as the ESC key being pressed.</summary>
	Keyboard,
	/// <summary>Specifies that the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control was closed because the <see cref="M:System.Windows.Forms.ToolStripDropDown.Close" /> method was called.</summary>
	CloseCalled
}
