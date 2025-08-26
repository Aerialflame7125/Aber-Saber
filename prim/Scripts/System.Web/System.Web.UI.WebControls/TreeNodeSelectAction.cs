namespace System.Web.UI.WebControls;

/// <summary>Represents the event or events to raise when a node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control is selected.</summary>
public enum TreeNodeSelectAction
{
	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.SelectedNodeChanged" /> event when a node is selected.</summary>
	Select,
	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" /> event when a node is selected.</summary>
	Expand,
	/// <summary>Raises both the <see cref="E:System.Web.UI.WebControls.TreeView.SelectedNodeChanged" /> and <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" /> events when a node is selected.</summary>
	SelectExpand,
	/// <summary>Raises no events when a node is selected.</summary>
	None
}
