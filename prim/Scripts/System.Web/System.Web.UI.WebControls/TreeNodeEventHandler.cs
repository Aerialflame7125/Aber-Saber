namespace System.Web.UI.WebControls;

/// <summary>Represents the method that will handle the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCollapsed" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeDataBound" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" />, or <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodePopulate" /> event of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
/// <param name="sender">The source of the event.</param>
/// <param name="e">A <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> that contains the event data.</param>
public delegate void TreeNodeEventHandler(object sender, TreeNodeEventArgs e);
