namespace System.Windows.Forms;

/// <summary>Defines constants that represent areas in a <see cref="T:System.Windows.Forms.ListView" /> or <see cref="T:System.Windows.Forms.ListViewItem" />. </summary>
/// <filterpriority>2</filterpriority>
[Flags]
public enum ListViewHitTestLocations
{
	/// <summary>A position outside the bounds of a <see cref="T:System.Windows.Forms.ListViewItem" /></summary>
	None = 1,
	/// <summary>A position within the bounds of an image contained in a <see cref="T:System.Windows.Forms.ListView" /> or <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	Image = 2,
	/// <summary>A position within the bounds of a text area contained in a <see cref="T:System.Windows.Forms.ListView" /> or <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	Label = 4,
	/// <summary>A position below the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	BelowClientArea = 0x10,
	/// <summary>A position to the right of the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	RightOfClientArea = 0x20,
	/// <summary>A position to the left of the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	LeftOfClientArea = 0x40,
	/// <summary>A position above the client portion of a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	AboveClientArea = 0x100,
	/// <summary>A position within the bounds of an image associated with a <see cref="T:System.Windows.Forms.ListViewItem" /> that indicates the state of the item.</summary>
	StateImage = 0x200
}
