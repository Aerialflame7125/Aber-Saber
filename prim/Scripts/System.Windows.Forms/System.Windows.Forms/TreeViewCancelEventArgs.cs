using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.BeforeCheck" />, <see cref="E:System.Windows.Forms.TreeView.BeforeCollapse" />, <see cref="E:System.Windows.Forms.TreeView.BeforeExpand" />, and <see cref="E:System.Windows.Forms.TreeView.BeforeSelect" /> events of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class TreeViewCancelEventArgs : CancelEventArgs
{
	private TreeNode node;

	private TreeViewAction action;

	/// <summary>Gets the tree node to be checked, expanded, collapsed, or selected.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> to be checked, expanded, collapsed, or selected.</returns>
	/// <filterpriority>1</filterpriority>
	public TreeNode Node => node;

	/// <summary>Gets the type of <see cref="T:System.Windows.Forms.TreeView" /> action that raised the event.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.TreeViewAction" /> values.</returns>
	/// <filterpriority>1</filterpriority>
	public TreeViewAction Action => action;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> class with the specified tree node, a value specifying whether the event is to be canceled, and the type of tree view action that raised the event.</summary>
	/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> that the event is responding to. </param>
	/// <param name="cancel">true to cancel the event; otherwise, false. </param>
	/// <param name="action">One of the <see cref="T:System.Windows.Forms.TreeViewAction" /> values indicating the type of action that raised the event. </param>
	public TreeViewCancelEventArgs(TreeNode node, bool cancel, TreeViewAction action)
		: base(cancel)
	{
		this.node = node;
		this.action = action;
	}
}
