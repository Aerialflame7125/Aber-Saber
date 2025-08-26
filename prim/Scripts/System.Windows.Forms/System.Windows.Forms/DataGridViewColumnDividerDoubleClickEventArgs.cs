namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.ColumnDividerDoubleClick" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
public class DataGridViewColumnDividerDoubleClickEventArgs : HandledMouseEventArgs
{
	private int columnIndex;

	/// <summary>The index of the column next to the column divider that was double-clicked.</summary>
	/// <returns>The index of the column next to the divider. </returns>
	public int ColumnIndex => columnIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnDividerDoubleClickEventArgs" /> class. </summary>
	/// <param name="columnIndex">The index of the column next to the column divider that was double-clicked. </param>
	/// <param name="e">A new <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> containing the inherited event data. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="columnIndex" /> is less than -1.</exception>
	public DataGridViewColumnDividerDoubleClickEventArgs(int columnIndex, HandledMouseEventArgs e)
		: base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
	{
		this.columnIndex = columnIndex;
	}
}
