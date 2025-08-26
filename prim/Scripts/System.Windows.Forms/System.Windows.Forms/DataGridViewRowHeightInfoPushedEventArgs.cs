using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowHeightInfoPushed" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
public class DataGridViewRowHeightInfoPushedEventArgs : HandledEventArgs
{
	private int height;

	private int minimumHeight;

	private int rowIndex;

	/// <summary>Gets the height of the row the event occurred for.</summary>
	/// <returns>The row height, in pixels.</returns>
	public int Height => height;

	/// <summary>Gets the minimum height of the row the event occurred for.</summary>
	/// <returns>The minimum row height, in pixels.</returns>
	public int MinimumHeight => minimumHeight;

	/// <summary>Gets the index of the row the event occurred for.</summary>
	/// <returns>The zero-based index of the row.</returns>
	public int RowIndex => rowIndex;

	internal DataGridViewRowHeightInfoPushedEventArgs(int rowIndex, int height, int minimumHeight)
	{
		this.rowIndex = rowIndex;
		this.height = height;
		this.minimumHeight = minimumHeight;
	}
}
