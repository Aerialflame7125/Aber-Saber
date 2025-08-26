namespace System.Windows.Forms;

/// <summary>Specifies the action that raised a <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> event.</summary>
/// <filterpriority>2</filterpriority>
public enum TreeViewAction
{
	/// <summary>The action that caused the event is unknown.</summary>
	Unknown,
	/// <summary>The event was caused by a keystroke.</summary>
	ByKeyboard,
	/// <summary>The event was caused by a mouse operation.</summary>
	ByMouse,
	/// <summary>The event was caused by the <see cref="T:System.Windows.Forms.TreeNode" /> collapsing.</summary>
	Collapse,
	/// <summary>The event was caused by the <see cref="T:System.Windows.Forms.TreeNode" /> expanding.</summary>
	Expand
}
