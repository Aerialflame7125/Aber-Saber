namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowStateChanged" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewRowStateChangedEventArgs : EventArgs
{
	private DataGridViewRow dataGridViewRow;

	private DataGridViewElementStates stateChanged;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has a changed state.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has a changed state.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewRow Row => dataGridViewRow;

	/// <summary>Gets the state that has changed on the row.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the row.</returns>
	public DataGridViewElementStates StateChanged => stateChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowStateChangedEventArgs" /> class. </summary>
	/// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow" /> that has a changed state.</param>
	/// <param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the row.</param>
	public DataGridViewRowStateChangedEventArgs(DataGridViewRow dataGridViewRow, DataGridViewElementStates stateChanged)
	{
		this.dataGridViewRow = dataGridViewRow;
		this.stateChanged = stateChanged;
	}
}
