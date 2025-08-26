namespace System.Windows.Forms;

/// <summary>Specifies the state of an item that is being drawn.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
public enum DrawItemState
{
	/// <summary>The item currently has no state.</summary>
	None = 0,
	/// <summary>The item is selected.</summary>
	Selected = 1,
	/// <summary>The item is grayed. Only menu controls use this value.</summary>
	Grayed = 2,
	/// <summary>The item is unavailable.</summary>
	Disabled = 4,
	/// <summary>The item is checked. Only menu controls use this value.</summary>
	Checked = 8,
	/// <summary>The item has focus.</summary>
	Focus = 0x10,
	/// <summary>The item is in its default visual state.</summary>
	Default = 0x20,
	/// <summary>The item is being hot-tracked, that is, the item is highlighted as the mouse pointer passes over it.</summary>
	HotLight = 0x40,
	/// <summary>The item is inactive.</summary>
	Inactive = 0x80,
	/// <summary>The item displays without a keyboard accelerator.</summary>
	NoAccelerator = 0x100,
	/// <summary>The item displays without the visual cue that indicates it has focus.</summary>
	NoFocusRect = 0x200,
	/// <summary>The item is the editing portion of a <see cref="T:System.Windows.Forms.ComboBox" />.</summary>
	ComboBoxEdit = 0x1000
}
