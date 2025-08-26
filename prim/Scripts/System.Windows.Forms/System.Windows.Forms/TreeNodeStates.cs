namespace System.Windows.Forms;

/// <summary>Defines constants that represent the possible states of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
public enum TreeNodeStates
{
	/// <summary>The node is selected.</summary>
	Selected = 1,
	/// <summary>The node is disabled.</summary>
	Grayed = 2,
	/// <summary>The node is checked.</summary>
	Checked = 8,
	/// <summary>The node has focus.</summary>
	Focused = 0x10,
	/// <summary>The node is in its default state.</summary>
	Default = 0x20,
	/// <summary>The node is hot. This state occurs when the <see cref="P:System.Windows.Forms.TreeView.HotTracking" /> property is set to true and the mouse pointer is over the node.</summary>
	Hot = 0x40,
	/// <summary>The node is marked.</summary>
	Marked = 0x80,
	/// <summary>The node in an indeterminate state.</summary>
	Indeterminate = 0x100,
	/// <summary>The node should indicate a keyboard shortcut.</summary>
	ShowKeyboardCues = 0x200
}
