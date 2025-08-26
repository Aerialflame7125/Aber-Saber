namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellStateChanged" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewCellStateChangedEventArgs : EventArgs
{
	private DataGridViewCell dataGridViewCell;

	private DataGridViewElementStates stateChanged;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that has a changed state.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCell" /> whose state has changed.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewCell Cell => dataGridViewCell;

	/// <summary>Gets the state that has changed on the cell.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the cell.</returns>
	public DataGridViewElementStates StateChanged => stateChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStateChangedEventArgs" /> class. </summary>
	/// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that has a changed state.</param>
	/// <param name="stateChanged">One of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values indicating the state that has changed on the cell.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewCell" /> is null.</exception>
	public DataGridViewCellStateChangedEventArgs(DataGridViewCell dataGridViewCell, DataGridViewElementStates stateChanged)
	{
		this.dataGridViewCell = dataGridViewCell;
		this.stateChanged = stateChanged;
	}
}
