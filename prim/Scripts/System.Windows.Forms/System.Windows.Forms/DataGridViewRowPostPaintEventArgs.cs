using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.RowPostPaint" /> event. </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewRowPostPaintEventArgs : EventArgs
{
	private DataGridView dataGridView;

	private Graphics graphics;

	private Rectangle clipBounds;

	private Rectangle rowBounds;

	private int rowIndex;

	private DataGridViewElementStates rowState;

	private string errorText;

	private DataGridViewCellStyle inheritedRowStyle;

	private bool isFirstDisplayedRow;

	private bool isLastVisibleRow;

	/// <summary>Gets or sets the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle ClipBounds
	{
		get
		{
			return clipBounds;
		}
		set
		{
			clipBounds = value;
		}
	}

	/// <summary>Gets a string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
	/// <returns>A string that represents an error message for the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
	/// <filterpriority>1</filterpriority>
	public string ErrorText => errorText;

	/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Graphics Graphics => graphics;

	/// <summary>Gets the cell style applied to the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains the cell style applied to the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
	public DataGridViewCellStyle InheritedRowStyle => inheritedRowStyle;

	/// <summary>Gets a value indicating whether the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	/// <returns>true if the row being painted is currently the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool IsFirstDisplayedRow => isFirstDisplayedRow;

	/// <summary>Gets a value indicating whether the current row is the last visible row displayed in the <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	/// <returns>true if the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public bool IsLastVisibleRow => isLastVisibleRow;

	/// <summary>Get the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
	/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Rectangle RowBounds => rowBounds;

	/// <summary>Gets the index of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
	/// <returns>The index of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</returns>
	/// <filterpriority>1</filterpriority>
	public int RowIndex => rowIndex;

	/// <summary>Gets the state of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewElementStates State => rowState;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowPostPaintEventArgs" /> class. </summary>
	/// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView" /> that owns the row that is being painted.</param>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be painted.</param>
	/// <param name="rowBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the row.</param>
	/// <param name="errorText">An error message that is associated with the row.</param>
	/// <param name="inheritedRowStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the row.</param>
	/// <param name="isFirstDisplayedRow">true to indicate whether the current row is the first row currently displayed in the <see cref="T:System.Windows.Forms.DataGridView" />; otherwise, false.</param>
	/// <param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView" /> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible" /> property set to true; otherwise, false.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridView" /> is null.-or-<paramref name="graphics" /> is null.-or-<paramref name="inheritedRowStyle" /> is null.</exception>
	public DataGridViewRowPostPaintEventArgs(DataGridView dataGridView, Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, string errorText, DataGridViewCellStyle inheritedRowStyle, bool isFirstDisplayedRow, bool isLastVisibleRow)
	{
		this.dataGridView = dataGridView;
		this.graphics = graphics;
		this.clipBounds = clipBounds;
		this.rowBounds = rowBounds;
		this.rowIndex = rowIndex;
		this.rowState = rowState;
		this.errorText = errorText;
		this.inheritedRowStyle = inheritedRowStyle;
		this.isFirstDisplayedRow = isFirstDisplayedRow;
		this.isLastVisibleRow = isLastVisibleRow;
	}

	/// <summary>Draws the focus rectangle around the specified bounds.</summary>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the focus area.</param>
	/// <param name="cellsPaintSelectionBackground">true to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" /> property to determine the color of the focus rectangle; false to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" /> property.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void DrawFocus(Rectangle bounds, bool cellsPaintSelectionBackground)
	{
		if (rowIndex < 0 || rowIndex >= dataGridView.Rows.Count)
		{
			throw new InvalidOperationException("Invalid RowIndex.");
		}
		DataGridViewRow rowInternal = dataGridView.GetRowInternal(rowIndex);
		rowInternal.PaintCells(graphics, clipBounds, bounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, DataGridViewPaintParts.Focus);
	}

	/// <summary>Paints the specified cell parts for the area in the specified bounds.</summary>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
	/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
	public void PaintCells(Rectangle clipBounds, DataGridViewPaintParts paintParts)
	{
		if (rowIndex < 0 || rowIndex >= dataGridView.Rows.Count)
		{
			throw new InvalidOperationException("Invalid RowIndex.");
		}
		DataGridViewRow rowInternal = dataGridView.GetRowInternal(rowIndex);
		rowInternal.PaintCells(graphics, clipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);
	}

	/// <summary>Paints the cell backgrounds for the area in the specified bounds.</summary>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
	/// <param name="cellsPaintSelectionBackground">true to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />; false to paint the background of the specified bounds with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
	public void PaintCellsBackground(Rectangle clipBounds, bool cellsPaintSelectionBackground)
	{
		if (cellsPaintSelectionBackground)
		{
			PaintCells(clipBounds, DataGridViewPaintParts.All);
		}
		else
		{
			PaintCells(clipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.Focus);
		}
	}

	/// <summary>Paints the cell contents for the area in the specified bounds.</summary>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area of the <see cref="T:System.Windows.Forms.DataGridView" /> to be painted.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
	public void PaintCellsContent(Rectangle clipBounds)
	{
		PaintCells(clipBounds, DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground);
	}

	/// <summary>Paints the entire row header of the current <see cref="T:System.Windows.Forms.DataGridViewRow" />.</summary>
	/// <param name="paintSelectionBackground">true to paint the row header with the color of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor" /> property of the <see cref="P:System.Windows.Forms.DataGridViewRow.InheritedStyle" />; false to paint the row header with the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor" /> of the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersDefaultCellStyle" /> property.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void PaintHeader(bool paintSelectionBackground)
	{
		if (paintSelectionBackground)
		{
			PaintHeader(DataGridViewPaintParts.All);
		}
		else
		{
			PaintHeader(DataGridViewPaintParts.Background | DataGridViewPaintParts.Border | DataGridViewPaintParts.ContentBackground | DataGridViewPaintParts.ContentForeground | DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.Focus);
		}
	}

	/// <summary>Paints the specified parts of the row header of the current row.</summary>
	/// <param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values specifying the parts to paint.</param>
	/// <exception cref="T:System.InvalidOperationException">
	///   <see cref="P:System.Windows.Forms.DataGridViewRowPostPaintEventArgs.RowIndex" /> is less than zero or greater than the number of rows in the <see cref="T:System.Windows.Forms.DataGridView" /> control minus one.</exception>
	public void PaintHeader(DataGridViewPaintParts paintParts)
	{
		if (rowIndex < 0 || rowIndex >= dataGridView.Rows.Count)
		{
			throw new InvalidOperationException("Invalid RowIndex.");
		}
		DataGridViewRow rowInternal = dataGridView.GetRowInternal(rowIndex);
		rowInternal.PaintHeader(graphics, clipBounds, rowBounds, rowIndex, rowState, isFirstDisplayedRow, isLastVisibleRow, paintParts);
	}
}
