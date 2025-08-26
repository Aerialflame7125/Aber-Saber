namespace System.Windows.Forms;

/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeRowsModeChanged" /> or <see cref="E:System.Windows.Forms.DataGridView.RowHeadersWidthSizeModeChanged" /> events of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
/// <param name="sender">The source of the event. </param>
/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs" /> that contains the event data.</param>
/// <filterpriority>2</filterpriority>
public delegate void DataGridViewAutoSizeModeEventHandler(object sender, DataGridViewAutoSizeModeEventArgs e);
