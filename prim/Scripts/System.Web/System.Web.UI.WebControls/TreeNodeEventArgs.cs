namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCheckChanged" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCollapsed" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeDataBound" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" />, and <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodePopulate" /> events of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. This class cannot be inherited.</summary>
public sealed class TreeNodeEventArgs : EventArgs
{
	private TreeNode _node;

	/// <summary>Gets the node that raised the event.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the node that raised the event.</returns>
	public TreeNode Node => _node;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> class using the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</summary>
	/// <param name="node">A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the current node when the event is raised. </param>
	public TreeNodeEventArgs(TreeNode node)
	{
		_node = node;
	}
}
