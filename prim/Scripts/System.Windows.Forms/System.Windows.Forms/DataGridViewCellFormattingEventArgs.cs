namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellFormatting" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewCellFormattingEventArgs : ConvertEventArgs
{
	private int columnIndex;

	private DataGridViewCellStyle cellStyle;

	private bool formattingApplied;

	private int rowIndex;

	/// <summary>Gets or sets the style of the cell that is being formatted.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the display style of the cell being formatted. The default is the value of the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" /> property. </returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewCellStyle CellStyle
	{
		get
		{
			return cellStyle;
		}
		set
		{
			cellStyle = value;
		}
	}

	/// <summary>Gets the column index of the cell that is being formatted.</summary>
	/// <returns>The column index of the cell that is being formatted.</returns>
	/// <filterpriority>1</filterpriority>
	public int ColumnIndex => columnIndex;

	/// <summary>Gets or sets a value indicating whether the cell value has been successfully formatted.</summary>
	/// <returns>true if the formatting for the cell value has been handled; otherwise, false. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool FormattingApplied
	{
		get
		{
			return formattingApplied;
		}
		set
		{
			formattingApplied = value;
		}
	}

	/// <summary>Gets the row index of the cell that is being formatted.</summary>
	/// <returns>The row index of the cell that is being formatted.</returns>
	/// <filterpriority>1</filterpriority>
	public int RowIndex => rowIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs" /> class.</summary>
	/// <param name="columnIndex">The column index of the cell that caused the event.</param>
	/// <param name="rowIndex">The row index of the cell that caused the event.</param>
	/// <param name="value">The cell's contents.</param>
	/// <param name="desiredType">The type to convert <paramref name="value" /> to. </param>
	/// <param name="cellStyle">The style of the cell that caused the event.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="columnIndex" /> is less than -1-or-<paramref name="rowIndex" /> is less than -1.</exception>
	public DataGridViewCellFormattingEventArgs(int columnIndex, int rowIndex, object value, Type desiredType, DataGridViewCellStyle cellStyle)
		: base(value, desiredType)
	{
		this.columnIndex = columnIndex;
		this.rowIndex = rowIndex;
		this.cellStyle = cellStyle;
	}
}
