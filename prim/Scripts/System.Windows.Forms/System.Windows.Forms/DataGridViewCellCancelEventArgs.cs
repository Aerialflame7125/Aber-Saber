using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for <see cref="E:System.Windows.Forms.DataGridView.CellBeginEdit" /> and <see cref="E:System.Windows.Forms.DataGridView.RowValidating" /> events.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewCellCancelEventArgs : CancelEventArgs
{
	private int columnIndex;

	private int rowIndex;

	/// <summary>Gets the column index of the cell that the event occurs for.</summary>
	/// <returns>The column index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that the event occurs for.</returns>
	/// <filterpriority>1</filterpriority>
	public int ColumnIndex => columnIndex;

	/// <summary>Gets the row index of the cell that the event occurs for.</summary>
	/// <returns>The row index of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that the event occurs for.</returns>
	/// <filterpriority>1</filterpriority>
	public int RowIndex => rowIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs" /> class. </summary>
	/// <param name="columnIndex">The index of the column containing the cell that the event occurs for.</param>
	/// <param name="rowIndex">The index of the row containing the cell that the event occurs for.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="columnIndex" /> is less than -1.-or-<paramref name="rowIndex" /> is less than -1.</exception>
	public DataGridViewCellCancelEventArgs(int columnIndex, int rowIndex)
	{
		this.columnIndex = columnIndex;
		this.rowIndex = rowIndex;
	}
}
