namespace System.Windows.Forms;

/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.DataGridView.CellValueNeeded" /> event or <see cref="E:System.Windows.Forms.DataGridView.CellValuePushed" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
/// <param name="sender">The source of the event. </param>
/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellValueEventArgs" /> that contains the event data.</param>
/// <filterpriority>2</filterpriority>
public delegate void DataGridViewCellValueEventHandler(object sender, DataGridViewCellValueEventArgs e);
