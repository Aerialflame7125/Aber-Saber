namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellContextMenuStripNeeded" /> event. </summary>
public class DataGridViewCellContextMenuStripNeededEventArgs : DataGridViewCellEventArgs
{
	private ContextMenuStrip contextMenuStrip;

	/// <summary>Gets or sets the shortcut menu for the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.CellContextMenuStripNeeded" /> event.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> for the cell. </returns>
	public ContextMenuStrip ContextMenuStrip
	{
		get
		{
			return contextMenuStrip;
		}
		set
		{
			contextMenuStrip = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventArgs" /> class. </summary>
	/// <param name="columnIndex">The column index of cell that the event occurred for.</param>
	/// <param name="rowIndex">The row index of the cell that the event occurred for.</param>
	public DataGridViewCellContextMenuStripNeededEventArgs(int columnIndex, int rowIndex)
		: base(columnIndex, rowIndex)
	{
	}
}
