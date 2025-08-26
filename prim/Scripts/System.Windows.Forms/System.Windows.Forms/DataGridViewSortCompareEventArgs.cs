using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.SortCompare" /> event.</summary>
public class DataGridViewSortCompareEventArgs : HandledEventArgs
{
	private DataGridViewColumn dataGridViewColumn;

	private object cellValue1;

	private object cellValue2;

	private int rowIndex1;

	private int rowIndex2;

	private int sortResult;

	/// <summary>Gets the value of the first cell to compare.</summary>
	/// <returns>The value of the first cell.</returns>
	public object CellValue1 => cellValue1;

	/// <summary>Gets the value of the second cell to compare.</summary>
	/// <returns>The value of the second cell.</returns>
	public object CellValue2 => cellValue2;

	/// <summary>Gets the column being sorted. </summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewColumn" /> to sort.</returns>
	public DataGridViewColumn Column => dataGridViewColumn;

	/// <summary>Gets the index of the row containing the first cell to compare.</summary>
	/// <returns>The index of the row containing the second cell.</returns>
	public int RowIndex1 => rowIndex1;

	/// <summary>Gets the index of the row containing the second cell to compare.</summary>
	/// <returns>The index of the row containing the second cell.</returns>
	public int RowIndex2 => rowIndex2;

	/// <summary>Gets or sets a value indicating the order in which the compared cells will be sorted.</summary>
	/// <returns>Less than zero if the first cell will be sorted before the second cell; zero if the first cell and second cell have equivalent values; greater than zero if the second cell will be sorted before the first cell.</returns>
	public int SortResult
	{
		get
		{
			return sortResult;
		}
		set
		{
			sortResult = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewSortCompareEventArgs" /> class.</summary>
	/// <param name="dataGridViewColumn">The column to sort.</param>
	/// <param name="cellValue1">The value of the first cell to compare.</param>
	/// <param name="cellValue2">The value of the second cell to compare.</param>
	/// <param name="rowIndex1">The index of the row containing the first cell.</param>
	/// <param name="rowIndex2">The index of the row containing the second cell.</param>
	public DataGridViewSortCompareEventArgs(DataGridViewColumn dataGridViewColumn, object cellValue1, object cellValue2, int rowIndex1, int rowIndex2)
	{
		this.dataGridViewColumn = dataGridViewColumn;
		this.cellValue1 = cellValue1;
		this.cellValue2 = cellValue2;
		this.rowIndex1 = rowIndex1;
		this.rowIndex2 = rowIndex2;
	}
}
