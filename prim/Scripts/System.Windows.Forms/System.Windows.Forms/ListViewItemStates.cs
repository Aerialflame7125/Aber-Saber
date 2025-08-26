namespace System.Windows.Forms;

/// <summary>Defines constants that represent the possible states of a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
public enum ListViewItemStates
{
	/// <summary>The item is selected.</summary>
	Selected = 1,
	/// <summary>The item is disabled.</summary>
	Grayed = 2,
	/// <summary>The item is checked.</summary>
	Checked = 8,
	/// <summary>The item has focus.</summary>
	Focused = 0x10,
	/// <summary>The item is in its default state.</summary>
	Default = 0x20,
	/// <summary>The item is currently under the mouse pointer.</summary>
	Hot = 0x40,
	/// <summary>The item is marked.</summary>
	Marked = 0x80,
	/// <summary>The item is in an indeterminate state.</summary>
	Indeterminate = 0x100,
	/// <summary>The item should indicate a keyboard shortcut.</summary>
	ShowKeyboardCues = 0x200
}
