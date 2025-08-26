namespace System.Web.UI.WebControls;

/// <summary>The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" /> enumeration is used by the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control to identify the type of a <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> node within a node hierarchy.</summary>
public enum SiteMapNodeItemType
{
	/// <summary>The top node of the site navigation hierarchy. There can be only one root node.</summary>
	Root,
	/// <summary>A parent node of the currently viewed page in the site navigation path. A parent node is any node that is found between the root node and the current node in the navigation hierarchy.</summary>
	Parent,
	/// <summary>The currently viewed page in the site navigation path.</summary>
	Current,
	/// <summary>A site map navigation path separator. The default separator for the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control is the "&gt;" character.</summary>
	PathSeparator
}
