namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnModeChanged" /> event. </summary>
public class DataGridViewAutoSizeColumnModeEventArgs : EventArgs
{
	private DataGridViewColumn dataGridViewColumn;

	private DataGridViewAutoSizeColumnMode previousMode;

	/// <summary>Gets the column with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property that changed.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property that changed.</returns>
	public DataGridViewColumn Column => dataGridViewColumn;

	/// <summary>Gets the previous value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property of the column.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value representing the previous value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property of the <see cref="P:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs.Column" />.</returns>
	public DataGridViewAutoSizeColumnMode PreviousMode => previousMode;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs" /> class. </summary>
	/// <param name="dataGridViewColumn">The column with the <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property that changed.</param>
	/// <param name="previousMode">The previous <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value of the column's <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode" /> property. </param>
	public DataGridViewAutoSizeColumnModeEventArgs(DataGridViewColumn dataGridViewColumn, DataGridViewAutoSizeColumnMode previousMode)
	{
		this.dataGridViewColumn = dataGridViewColumn;
		this.previousMode = previousMode;
	}
}
