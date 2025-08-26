namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowDividerDoubleClick" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
public class DataGridViewRowDividerDoubleClickEventArgs : HandledMouseEventArgs
{
	private int rowIndex;

	/// <summary>The index of the row above the row divider that was double-clicked.</summary>
	/// <returns>The index of the row above the divider.</returns>
	public int RowIndex => rowIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowDividerDoubleClickEventArgs" /> class. </summary>
	/// <param name="rowIndex">The index of the row above the row divider that was double-clicked.</param>
	/// <param name="e">A new <see cref="T:System.Windows.Forms.HandledMouseEventArgs" /> containing the inherited event data.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than -1.</exception>
	public DataGridViewRowDividerDoubleClickEventArgs(int rowIndex, HandledMouseEventArgs e)
		: base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
	{
		this.rowIndex = rowIndex;
	}
}
