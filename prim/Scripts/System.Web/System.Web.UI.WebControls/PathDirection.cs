namespace System.Web.UI.WebControls;

/// <summary>Indicates the hierarchical order in which navigation nodes are rendered for site-navigation controls.</summary>
public enum PathDirection
{
	/// <summary>Nodes are rendered in a hierarchical order from the top-most node to the current node, from left to right.</summary>
	RootToCurrent,
	/// <summary>Nodes are rendered in a hierarchical order from the current node to the top-most node, from left to right.</summary>
	CurrentToRoot
}
