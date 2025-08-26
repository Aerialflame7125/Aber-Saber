namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowContextMenuStripNeeded" /> event. </summary>
public class DataGridViewRowContextMenuStripNeededEventArgs : EventArgs
{
	private int rowIndex;

	private ContextMenuStrip contextMenuStrip;

	/// <summary>Gets or sets the shortcut menu for the row that raised the <see cref="E:System.Windows.Forms.DataGridView.RowContextMenuStripNeeded" /> event.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> in use.</returns>
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

	/// <summary>Gets the index of the row that is requesting a shortcut menu.</summary>
	/// <returns>The zero-based index of the row that is requesting a shortcut menu.</returns>
	public int RowIndex => rowIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs" /> class. </summary>
	/// <param name="rowIndex">The index of the row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is less than -1.</exception>
	public DataGridViewRowContextMenuStripNeededEventArgs(int rowIndex)
	{
		this.rowIndex = rowIndex;
	}
}
