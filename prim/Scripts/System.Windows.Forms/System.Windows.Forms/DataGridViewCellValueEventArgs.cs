namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellValueNeeded" /> and <see cref="E:System.Windows.Forms.DataGridView.CellValuePushed" /> events of the <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewCellValueEventArgs : EventArgs
{
	private int columnIndex;

	private int rowIndex;

	private object cellValue;

	/// <summary>Gets a value indicating the column index of the cell that the event occurs for.</summary>
	/// <returns>The index of the column containing the cell that the event occurs for.</returns>
	/// <filterpriority>1</filterpriority>
	public int ColumnIndex => columnIndex;

	/// <summary>Gets a value indicating the row index of the cell that the event occurs for.</summary>
	/// <returns>The index of the row containing the cell that the event occurs for.</returns>
	/// <filterpriority>1</filterpriority>
	public int RowIndex => rowIndex;

	/// <summary>Gets or sets the value of the cell that the event occurs for.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the cell's value.</returns>
	/// <filterpriority>1</filterpriority>
	public object Value
	{
		get
		{
			return cellValue;
		}
		set
		{
			cellValue = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellValueEventArgs" /> class. </summary>
	/// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
	/// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="columnIndex" /> is less than 0.-or-<paramref name="rowIndex" /> is less than 0.</exception>
	public DataGridViewCellValueEventArgs(int columnIndex, int rowIndex)
	{
		this.columnIndex = columnIndex;
		this.rowIndex = rowIndex;
	}
}
