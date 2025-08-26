namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.ColumnStateChanged" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewColumnStateChangedEventArgs : EventArgs
{
	private DataGridViewColumn dataGridViewColumn;

	private DataGridViewElementStates stateChanged;

	/// <summary>Gets the column whose state changed.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> whose state changed.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewColumn Column => dataGridViewColumn;

	/// <summary>Gets the new column state.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</returns>
	public DataGridViewElementStates StateChanged => stateChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnStateChangedEventArgs" /> class. </summary>
	/// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> whose state has changed.</param>
	/// <param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values.</param>
	public DataGridViewColumnStateChangedEventArgs(DataGridViewColumn dataGridViewColumn, DataGridViewElementStates stateChanged)
	{
		this.dataGridViewColumn = dataGridViewColumn;
		this.stateChanged = stateChanged;
	}
}
