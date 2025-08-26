using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.CellPainting" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewCellPaintingEventArgs : HandledEventArgs
{
	private DataGridView dataGridView;

	private Graphics graphics;

	private Rectangle clipBounds;

	private Rectangle cellBounds;

	private int rowIndex;

	private int columnIndex;

	private DataGridViewElementStates cellState;

	private object cellValue;

	private object formattedValue;

	private string errorText;

	private DataGridViewCellStyle cellStyle;

	private DataGridViewAdvancedBorderStyle advancedBorderStyle;

	private DataGridViewPaintParts paintParts;

	/// <summary>Gets the border style of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that represents the border style of the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewAdvancedBorderStyle AdvancedBorderStyle => advancedBorderStyle;

	/// <summary>Get the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle CellBounds => cellBounds;

	/// <summary>Gets the cell style of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains the cell style of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewCellStyle CellStyle => cellStyle;

	/// <summary>Gets the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle ClipBounds => clipBounds;

	/// <summary>Gets the column index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>The column index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	public int ColumnIndex => columnIndex;

	/// <summary>Gets a string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>A string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	public string ErrorText => errorText;

	/// <summary>Gets the formatted value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>The formatted value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	public object FormattedValue => formattedValue;

	/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>The cell parts that are to be painted.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to be painted.</returns>
	public DataGridViewPaintParts PaintParts => paintParts;

	/// <summary>Gets the row index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>The row index of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	public int RowIndex => rowIndex;

	/// <summary>Gets the state of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewElementStates State => cellState;

	/// <summary>Gets the value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</summary>
	/// <returns>The value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	public object Value => cellValue;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellPaintingEventArgs" /> class. </summary>
	/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that contains the cell to be painted.</param>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="columnIndex">The column index of the cell that is being painted.</param>
	/// <param name="cellState">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
	/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="errorText">An error message that is associated with the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
	/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridView" /> is null.-or-<paramref name="graphics" /> is null.-or-<paramref name="cellStyle" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="paintParts" /> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values.</exception>
	public DataGridViewCellPaintingEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, int columnIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		this.dataGridView = dataGridView;
		this.graphics = graphics;
		this.clipBounds = clipBounds;
		this.cellBounds = cellBounds;
		this.rowIndex = rowIndex;
		this.columnIndex = columnIndex;
		this.cellState = cellState;
		cellValue = value;
		this.formattedValue = formattedValue;
		this.errorText = errorText;
		this.cellStyle = cellStyle;
		this.advancedBorderStyle = advancedBorderStyle;
		this.paintParts = paintParts;
	}

	/// <summary>Paints the specified parts of the cell for the area in the specified bounds.</summary>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
	/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	public void Paint(Rectangle clipBounds, DataGridViewPaintParts paintParts)
	{
		if (rowIndex < -1 || rowIndex >= dataGridView.Rows.Count)
		{
			throw new InvalidOperationException("Invalid \"RowIndex.\"");
		}
		if (columnIndex < -1 || columnIndex >= dataGridView.Columns.Count)
		{
			throw new InvalidOperationException("Invalid \"ColumnIndex.\"");
		}
		DataGridViewCell dataGridViewCell = ((rowIndex == -1 && columnIndex == -1) ? dataGridView.TopLeftHeaderCell : ((rowIndex == -1) ? dataGridView.Columns[columnIndex].HeaderCell : ((columnIndex != -1) ? dataGridView.Rows[rowIndex].Cells[columnIndex] : dataGridView.Rows[rowIndex].HeaderCell)));
		dataGridViewCell.PaintInternal(graphics, clipBounds, cellBounds, rowIndex, cellState, Value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
	}

	/// <summary>Paints the cell background for the area in the specified bounds.</summary>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
	/// <param name="cellsPaintSelectionBackground">true to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" />; false to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewCell.InheritedStyle" />.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	public void PaintBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
	{
		Paint(clipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);
	}

	/// <summary>Paints the cell content for the area in the specified bounds.</summary>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex" /> is less than -1 or greater than or equal to the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-<see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex" /> is less than -1 or greater than or equal to the number of columns in the <see cref="T:System.Windows.Forms.DataGridView" /> control.</exception>
	[System.MonoInternalNote("Needs row header cell edit pencil glyph")]
	public void PaintContent(Rectangle clipBounds)
	{
		Paint(clipBounds, DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground);
	}
}
