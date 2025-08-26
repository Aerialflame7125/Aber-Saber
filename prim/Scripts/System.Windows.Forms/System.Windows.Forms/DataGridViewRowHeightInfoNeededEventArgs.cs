namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowHeightInfoNeeded" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
public class DataGridViewRowHeightInfoNeededEventArgs : EventArgs
{
	private int height;

	private int minimumHeight;

	private int rowIndex;

	/// <summary>Gets or sets the height of the row the event occurred for.</summary>
	/// <returns>The row height. </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is greater than 65,536. </exception>
	public int Height
	{
		get
		{
			return height;
		}
		set
		{
			height = value;
		}
	}

	/// <summary>Gets or sets the minimum height of the row the event occurred for. </summary>
	/// <returns>The minimum row height.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 2.</exception>
	public int MinimumHeight
	{
		get
		{
			return minimumHeight;
		}
		set
		{
			minimumHeight = value;
		}
	}

	/// <summary>Gets the index of the row associated with this <see cref="T:System.Windows.Forms.DataGridViewRowHeightInfoNeededEventArgs" />.</summary>
	/// <returns>The zero-based index of the row the event occurred for.</returns>
	public int RowIndex => rowIndex;

	internal DataGridViewRowHeightInfoNeededEventArgs(int rowIndex, int height, int minimumHeight)
	{
		this.rowIndex = rowIndex;
		this.height = height;
		this.minimumHeight = minimumHeight;
	}
}
