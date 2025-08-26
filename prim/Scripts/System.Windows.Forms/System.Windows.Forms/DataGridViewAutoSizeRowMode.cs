namespace System.Windows.Forms;

/// <summary>Defines values for specifying how the height of a row is adjusted. </summary>
public enum DataGridViewAutoSizeRowMode
{
	/// <summary>The row height adjusts to fit the contents of the row header. </summary>
	RowHeader = 1,
	/// <summary>The row height adjusts to fit the contents of all cells in the row, excluding the header cell. </summary>
	AllCellsExceptHeader,
	/// <summary>The row height adjusts to fit the contents of all cells in the row, including the header cell. </summary>
	AllCells
}
