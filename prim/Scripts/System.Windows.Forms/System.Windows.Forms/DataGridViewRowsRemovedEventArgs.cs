namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowsRemoved" /> event.</summary>
public class DataGridViewRowsRemovedEventArgs : EventArgs
{
	private int rowIndex;

	private int rowCount;

	/// <summary>Gets the number of rows that were deleted.</summary>
	/// <returns>The number of deleted rows.</returns>
	public int RowCount => rowCount;

	/// <summary>Gets the zero-based index of the row deleted, or the first deleted row if multiple rows were deleted.</summary>
	/// <returns>The zero-based index of the row that was deleted, or the first deleted row if multiple rows were deleted.</returns>
	public int RowIndex => rowIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowsRemovedEventArgs" /> class.</summary>
	/// <param name="rowIndex">The zero-based index of the row that was deleted, or the first deleted row if multiple rows were deleted. </param>
	/// <param name="rowCount">The number of rows that were deleted.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than 0.-or-<paramref name="rowCount" /> is less than 1.</exception>
	public DataGridViewRowsRemovedEventArgs(int rowIndex, int rowCount)
	{
		this.rowIndex = rowIndex;
		this.rowCount = rowCount;
	}
}
