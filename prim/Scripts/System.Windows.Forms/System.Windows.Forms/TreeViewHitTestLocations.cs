using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Defines constants that represent areas of a <see cref="T:System.Windows.Forms.TreeView" /> or <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
[ComVisible(true)]
public enum TreeViewHitTestLocations
{
	/// <summary>A position in the client area of the <see cref="T:System.Windows.Forms.TreeView" /> control, but not on a node or a portion of a node.</summary>
	None = 1,
	/// <summary>A position within the bounds of an image contained on a <see cref="T:System.Windows.Forms.TreeView" /> or <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	Image = 2,
	/// <summary>A position on the text portion of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	Label = 4,
	/// <summary>A position in the indentation area for a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	Indent = 8,
	/// <summary>A position on the plus/minus area of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	PlusMinus = 0x10,
	/// <summary>A position to the right of the text area of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	RightOfLabel = 0x20,
	/// <summary>A position within the bounds of a state image for a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	StateImage = 0x40,
	/// <summary>A position above the client portion of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	AboveClientArea = 0x100,
	/// <summary>A position below the client portion of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	BelowClientArea = 0x200,
	/// <summary>A position to the right of the client area of the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	RightOfClientArea = 0x400,
	/// <summary>A position to the left of the client area of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	LeftOfClientArea = 0x800
}
