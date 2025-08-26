namespace System.Windows.Forms;

/// <summary>Provides data for <see cref="T:System.Windows.Forms.DataGridView" /> events related to cell and row operations.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewCellEventArgs : EventArgs
{
	private int columnIndex;

	private int rowIndex;

	/// <summary>Gets a value indicating the column index of the cell that the event occurs for.</summary>
	/// <returns>The index of the column containing the cell that the event occurs for.</returns>
	/// <filterpriority>1</filterpriority>
	public int ColumnIndex => columnIndex;

	/// <summary>Gets a value indicating the row index of the cell that the event occurs for.</summary>
	/// <returns>The index of the row containing the cell that the event occurs for.</returns>
	/// <filterpriority>1</filterpriority>
	public int RowIndex => rowIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> class. </summary>
	/// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
	/// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="columnIndex" /> is less than -1.-or-<paramref name="rowIndex" /> is less than -1.</exception>
	public DataGridViewCellEventArgs(int columnIndex, int rowIndex)
	{
		this.columnIndex = columnIndex;
		this.rowIndex = rowIndex;
	}
}
