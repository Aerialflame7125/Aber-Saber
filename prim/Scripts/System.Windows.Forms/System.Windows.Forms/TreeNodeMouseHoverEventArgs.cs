using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.NodeMouseHover" /> event. </summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class TreeNodeMouseHoverEventArgs : EventArgs
{
	private TreeNode node;

	/// <summary>Gets the node the mouse pointer is currently resting on.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> the mouse pointer is currently resting on.</returns>
	/// <filterpriority>1</filterpriority>
	public TreeNode Node => node;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNodeMouseHoverEventArgs" /> class. </summary>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> the mouse pointer is currently resting on.</param>
	public TreeNodeMouseHoverEventArgs(TreeNode node)
	{
		this.node = node;
	}
}
