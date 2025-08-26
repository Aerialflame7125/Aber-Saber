namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.BeforeLabelEdit" /> and <see cref="E:System.Windows.Forms.TreeView.AfterLabelEdit" /> events.</summary>
/// <filterpriority>2</filterpriority>
public class NodeLabelEditEventArgs : EventArgs
{
	private TreeNode node;

	private string label;

	private bool cancel;

	/// <summary>Gets or sets a value indicating whether the edit has been canceled.</summary>
	/// <returns>true if the edit has been canceled; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool CancelEdit
	{
		get
		{
			return cancel;
		}
		set
		{
			cancel = value;
			if (cancel)
			{
				node.EndEdit(cancel: true);
			}
		}
	}

	/// <summary>Gets the tree node containing the text to edit.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the tree node containing the text to edit.</returns>
	/// <filterpriority>1</filterpriority>
	public TreeNode Node => node;

	/// <summary>Gets the new text to associate with the tree node.</summary>
	/// <returns>The string value that represents the new <see cref="T:System.Windows.Forms.TreeNode" /> label or null if the user cancels the edit. </returns>
	/// <filterpriority>1</filterpriority>
	public string Label => label;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <param name="node">The tree node containing the text to edit. </param>
	public NodeLabelEditEventArgs(TreeNode node)
	{
		this.node = node;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.TreeNode" /> and the specified text with which to update the tree node label.</summary>
	/// <param name="node">The tree node containing the text to edit. </param>
	/// <param name="label">The new text to associate with the tree node. </param>
	public NodeLabelEditEventArgs(TreeNode node, string label)
		: this(node)
	{
		this.label = label;
	}

	internal void SetLabel(string label)
	{
		this.label = label;
	}
}
