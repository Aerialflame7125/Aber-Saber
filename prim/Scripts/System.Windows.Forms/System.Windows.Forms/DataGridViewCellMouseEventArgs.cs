namespace System.Windows.Forms;

/// <summary>Provides data for mouse events raised by a <see cref="T:System.Windows.Forms.DataGridView" /> whenever the mouse is moved within a <see cref="T:System.Windows.Forms.DataGridViewCell" />. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewCellMouseEventArgs : MouseEventArgs
{
	private int columnIndex;

	private int rowIndex;

	/// <summary>Gets the zero-based column index of the cell.</summary>
	/// <returns>An integer specifying the column index.</returns>
	/// <filterpriority>1</filterpriority>
	public int ColumnIndex => columnIndex;

	/// <summary>Gets the zero-based row index of the cell.</summary>
	/// <returns>An integer specifying the row index.</returns>
	/// <filterpriority>1</filterpriority>
	public int RowIndex => rowIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> class.</summary>
	/// <param name="columnIndex">The cell's zero-based column index.</param>
	/// <param name="rowIndex">The cell's zero-based row index.</param>
	/// <param name="localX">The x-coordinate of the mouse, in pixels.</param>
	/// <param name="localY">The y-coordinate of the mouse, in pixels.</param>
	/// <param name="e">The originating <see cref="T:System.Windows.Forms.MouseEventArgs" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="columnIndex" /> is less than -1.-or-<paramref name="rowIndex" /> is less than -1.</exception>
	public DataGridViewCellMouseEventArgs(int columnIndex, int rowIndex, int localX, int localY, MouseEventArgs e)
		: base(e.Button, e.Clicks, localX, localY, e.Delta)
	{
		this.columnIndex = columnIndex;
		this.rowIndex = rowIndex;
	}
}
