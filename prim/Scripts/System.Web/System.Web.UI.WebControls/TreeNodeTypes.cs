namespace System.Web.UI.WebControls;

/// <summary>Represents the different node types (leaf, parent, and root) in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
[Flags]
public enum TreeNodeTypes
{
	/// <summary>No nodes.</summary>
	None = 0,
	/// <summary>A node with no parent node and one or more child nodes.</summary>
	Root = 1,
	/// <summary>A node with a parent node and one or more child nodes.</summary>
	Parent = 2,
	/// <summary>A node with no child nodes.</summary>
	Leaf = 4,
	/// <summary>All nodes.</summary>
	All = 7
}
