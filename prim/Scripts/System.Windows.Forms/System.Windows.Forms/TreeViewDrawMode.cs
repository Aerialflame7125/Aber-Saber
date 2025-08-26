namespace System.Windows.Forms;

/// <summary>Defines constants that represent the ways a <see cref="T:System.Windows.Forms.TreeView" /> can be drawn.</summary>
/// <filterpriority>2</filterpriority>
public enum TreeViewDrawMode
{
	/// <summary>The <see cref="T:System.Windows.Forms.TreeView" /> is drawn by the operating system.</summary>
	Normal,
	/// <summary>The label portion of the <see cref="T:System.Windows.Forms.TreeView" /> nodes are drawn manually. Other node elements are drawn by the operating system, including icons, checkboxes, plus and minus signs, and lines connecting the nodes.</summary>
	OwnerDrawText,
	/// <summary>All elements of a <see cref="T:System.Windows.Forms.TreeView" /> node are drawn manually, including icons, checkboxes, plus and minus signs, and lines connecting the nodes.</summary>
	OwnerDrawAll
}
