using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellValidating" /> event of a <see cref="T:System.Windows.Forms.DataGridView" /> control. </summary>
public class DataGridViewCellValidatingEventArgs : CancelEventArgs
{
	private int columnIndex;

	private object formattedValue;

	private int rowIndex;

	/// <summary>Gets the column index of the cell that needs to be validated.</summary>
	/// <returns>A zero-based integer that specifies the column index of the cell that needs to be validated.</returns>
	public int ColumnIndex => columnIndex;

	/// <summary>Gets the formatted contents of the cell that needs to be validated.</summary>
	/// <returns>A reference to the formatted value.</returns>
	public object FormattedValue => formattedValue;

	/// <summary>Gets the row index of the cell that needs to be validated.</summary>
	/// <returns>A zero-based integer that specifies the row index of the cell that needs to be validated.</returns>
	public int RowIndex => rowIndex;

	internal DataGridViewCellValidatingEventArgs(int columnIndex, int rowIndex, object formattedValue)
	{
		this.columnIndex = columnIndex;
		this.rowIndex = rowIndex;
		this.formattedValue = formattedValue;
	}
}
