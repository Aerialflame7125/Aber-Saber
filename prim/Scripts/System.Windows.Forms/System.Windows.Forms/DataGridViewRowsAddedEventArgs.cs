namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowsAdded" /> event. </summary>
public class DataGridViewRowsAddedEventArgs : EventArgs
{
	private int rowIndex;

	private int rowCount;

	/// <summary>Gets the number of rows that have been added.</summary>
	/// <returns>The number of rows that have been added.</returns>
	public int RowCount => rowCount;

	/// <summary>Gets the index of the first added row.</summary>
	/// <returns>The index of the first added row.</returns>
	public int RowIndex => rowIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowsAddedEventArgs" /> class. </summary>
	/// <param name="rowIndex">The index of the first added row.</param>
	/// <param name="rowCount">The number of rows that have been added.</param>
	public DataGridViewRowsAddedEventArgs(int rowIndex, int rowCount)
	{
		this.rowIndex = rowIndex;
		this.rowCount = rowCount;
	}
}
