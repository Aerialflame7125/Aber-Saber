namespace System.Windows.Forms;

/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.TreeView.AfterCheck" />, <see cref="E:System.Windows.Forms.TreeView.AfterCollapse" />, <see cref="E:System.Windows.Forms.TreeView.AfterExpand" />, or <see cref="E:System.Windows.Forms.TreeView.AfterSelect" /> event of a <see cref="T:System.Windows.Forms.TreeView" />.</summary>
/// <param name="sender">The source of the event. </param>
/// <param name="e">A <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> that contains the event data. </param>
/// <filterpriority>2</filterpriority>
public delegate void TreeViewEventHandler(object sender, TreeViewEventArgs e);
