using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Contains functionality common to row header cells and column header cells.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewHeaderCell : DataGridViewCell
{
	private ButtonState buttonState;

	/// <returns>true if the cell is on-screen or partially on-screen; otherwise, false.</returns>
	[Browsable(false)]
	public override bool Displayed => base.Displayed;

	/// <summary>Gets the type of the formatted value of the cell.</summary>
	/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.String" /> type.</returns>
	public override Type FormattedValueType => typeof(string);

	/// <summary>Gets a value indicating whether the cell is frozen. </summary>
	/// <returns>true if the cell is frozen; otherwise, false. The default is false if the cell is detached from a <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public override bool Frozen => base.Frozen;

	/// <summary>Gets a value indicating whether the header cell is read-only.</summary>
	/// <returns>true in all cases.</returns>
	/// <exception cref="T:System.InvalidOperationException">An operation tries to set this property.</exception>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override bool ReadOnly
	{
		get
		{
			return base.ReadOnly;
		}
		set
		{
			base.ReadOnly = value;
		}
	}

	/// <summary>Gets a value indicating whether the cell is resizable.</summary>
	/// <returns>true if this cell can be resized; otherwise, false. The default is false if the cell is not attached to a <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public override bool Resizable => base.Resizable;

	/// <summary>Gets or sets a value indicating whether the cell is selected.</summary>
	/// <returns>false in all cases.</returns>
	/// <exception cref="T:System.InvalidOperationException">This property is being set.</exception>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public override bool Selected
	{
		get
		{
			return base.Selected;
		}
		set
		{
			base.Selected = value;
		}
	}

	/// <summary>Gets the type of the value stored in the cell.</summary>
	/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.String" /> type.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type ValueType
	{
		get
		{
			return base.ValueType;
		}
		set
		{
			base.ValueType = value;
		}
	}

	/// <summary>Gets a value indicating whether or not the cell is visible.</summary>
	/// <returns>true if the cell is visible; otherwise, false. The default is false if the cell is detached from a <see cref="T:System.Windows.Forms.DataGridView" /></returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public override bool Visible => base.Visible;

	/// <summary>Gets the buttonlike visual state of the header cell.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.ButtonState" /> values; the default is <see cref="F:System.Windows.Forms.ButtonState.Normal" />.</returns>
	protected ButtonState ButtonState => buttonState;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> class.</summary>
	public DataGridViewHeaderCell()
	{
		buttonState = ButtonState.Normal;
	}

	/// <summary>Creates an exact copy of this cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object Clone()
	{
		return new DataGridViewHeaderCell();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> and optionally releases the managed resources. </summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
	}

	/// <summary>Gets the shortcut menu of the header cell.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" /> if the <see cref="T:System.Windows.Forms.DataGridViewHeaderCell" /> or <see cref="T:System.Windows.Forms.DataGridView" /> has a shortcut menu assigned; otherwise, null.</returns>
	/// <param name="rowIndex">Ignored by this implementation.</param>
	public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
	{
		if (base.DataGridView == null)
		{
			return null;
		}
		if (ContextMenuStrip != null)
		{
			return ContextMenuStrip;
		}
		if (base.DataGridView.ContextMenuStrip != null)
		{
			return base.DataGridView.ContextMenuStrip;
		}
		return null;
	}

	/// <summary>Returns a value indicating the current state of the cell as inherited from the state of its row or column.</summary>
	/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the current state of the cell.</returns>
	/// <param name="rowIndex">The index of the row containing the cell or -1 if the cell is not a row header cell or is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control.</param>
	/// <exception cref="T:System.ArgumentException">The cell is a row header cell, the cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control, and <paramref name="rowIndex" /> is not -1.- or -The cell is a row header cell, the cell is contained within a <see cref="T:System.Windows.Forms.DataGridView" /> control, and <paramref name="rowIndex" /> is outside the valid range of 0 to the number of rows in the control minus 1.- or -The cell is a row header cell and <paramref name="rowIndex" /> is not the index of the row containing this cell.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The cell is a column header cell or the control's <see cref="P:System.Windows.Forms.DataGridView.TopLeftHeaderCell" />  and <paramref name="rowIndex" /> is not -1.</exception>
	public override DataGridViewElementStates GetInheritedState(int rowIndex)
	{
		return DataGridViewElementStates.ResizableSet | State;
	}

	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"DataGridViewHeaderCell {{ ColumnIndex={base.ColumnIndex}, RowIndex={base.RowIndex} }}";
	}

	/// <summary>Gets the size of the cell.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the header cell.</returns>
	/// <param name="rowIndex">The row index of the header cell.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property for this cell is null and <paramref name="rowIndex" /> does not equal -1. -or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningColumn" /> property for this cell is not null and <paramref name="rowIndex" /> does not equal -1. -or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningRow" /> property for this cell is not null and <paramref name="rowIndex" /> is less than zero or greater than or equal to the number of rows in the control.-or-The values of the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningColumn" /> and <see cref="P:System.Windows.Forms.DataGridViewCell.OwningRow" /> properties of this cell are both null and <paramref name="rowIndex" /> does not equal -1.</exception>
	/// <exception cref="T:System.ArgumentException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningRow" /> property for this cell is not null and <paramref name="rowIndex" /> indicates a row other than the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningRow" />.</exception>
	protected override Size GetSize(int rowIndex)
	{
		if (base.DataGridView == null && rowIndex != -1)
		{
			throw new ArgumentOutOfRangeException("rowIndex");
		}
		if (base.OwningColumn != null && rowIndex != -1)
		{
			throw new ArgumentOutOfRangeException("rowIndex");
		}
		if (base.OwningRow != null && (rowIndex < 0 || rowIndex >= base.DataGridView.Rows.Count))
		{
			throw new ArgumentOutOfRangeException("rowIndex");
		}
		if (base.OwningColumn == null && base.OwningRow == null && rowIndex != -1)
		{
			throw new ArgumentOutOfRangeException("rowIndex");
		}
		if (base.OwningRow != null && base.OwningRow.Index != rowIndex)
		{
			throw new ArgumentException("rowIndex");
		}
		if (base.DataGridView == null)
		{
			return new Size(-1, -1);
		}
		if (this is DataGridViewTopLeftHeaderCell)
		{
			return new Size(base.DataGridView.RowHeadersWidth, base.DataGridView.ColumnHeadersHeight);
		}
		if (this is DataGridViewColumnHeaderCell)
		{
			return new Size(100, base.DataGridView.ColumnHeadersHeight);
		}
		if (this is DataGridViewRowHeaderCell)
		{
			return new Size(base.DataGridView.RowHeadersWidth, 22);
		}
		return Size.Empty;
	}

	/// <summary>Gets the value of the cell. </summary>
	/// <returns>The value of the current <see cref="T:System.Windows.Forms.DataGridViewCell" />.</returns>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is not -1.</exception>
	protected override object GetValue(int rowIndex)
	{
		return base.GetValue(rowIndex);
	}

	/// <summary>Indicates whether a row will be unshared when the mouse button is held down while the pointer is on a cell in the row.</summary>
	/// <returns>true if the user clicks with the left mouse button, visual styles are enabled, and the <see cref="P:System.Windows.Forms.DataGridView.EnableHeadersVisualStyles" /> property is true; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the mouse position.</param>
	protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		if (base.DataGridView == null)
		{
			return false;
		}
		if (e.Button == MouseButtons.Left && Application.RenderWithVisualStyles && base.DataGridView.EnableHeadersVisualStyles)
		{
			return true;
		}
		return false;
	}

	/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
	/// <returns>true if visual styles are enabled, and the <see cref="P:System.Windows.Forms.DataGridView.EnableHeadersVisualStyles" /> property is true; otherwise, false.</returns>
	/// <param name="rowIndex">The index of the row that the mouse pointer entered.</param>
	protected override bool MouseEnterUnsharesRow(int rowIndex)
	{
		if (base.DataGridView == null)
		{
			return false;
		}
		if (Application.RenderWithVisualStyles && base.DataGridView.EnableHeadersVisualStyles)
		{
			return true;
		}
		return false;
	}

	/// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewHeaderCell.ButtonState" /> property value is not <see cref="F:System.Windows.Forms.ButtonState.Normal" />, visual styles are enabled, and the <see cref="P:System.Windows.Forms.DataGridView.EnableHeadersVisualStyles" /> property is true; otherwise, false.</returns>
	/// <param name="rowIndex">The index of the row that the mouse pointer left.</param>
	protected override bool MouseLeaveUnsharesRow(int rowIndex)
	{
		if (base.DataGridView == null)
		{
			return false;
		}
		if (ButtonState != 0 && Application.RenderWithVisualStyles && base.DataGridView.EnableHeadersVisualStyles)
		{
			return true;
		}
		return false;
	}

	/// <summary>Indicates whether a row will be unshared when the mouse button is released while the pointer is on a cell in the row.</summary>
	/// <returns>true if the left mouse button was released, visual styles are enabled, and the <see cref="P:System.Windows.Forms.DataGridView.EnableHeadersVisualStyles" /> property is true; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the mouse position.</param>
	protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		if (base.DataGridView == null)
		{
			return false;
		}
		if (e.Button == MouseButtons.Left && Application.RenderWithVisualStyles && base.DataGridView.EnableHeadersVisualStyles)
		{
			return true;
		}
		return false;
	}

	/// <summary>Called when the mouse button is held down while the pointer is on a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the mouse position.</param>
	protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
	{
		base.OnMouseDown(e);
	}

	/// <summary>Called when the mouse pointer enters the cell.</summary>
	/// <param name="rowIndex">The index of the row containing the cell.</param>
	protected override void OnMouseEnter(int rowIndex)
	{
		base.OnMouseEnter(rowIndex);
	}

	/// <summary>Called when the mouse pointer leaves the cell.</summary>
	/// <param name="rowIndex">The index of the row containing the cell.</param>
	protected override void OnMouseLeave(int rowIndex)
	{
		base.OnMouseLeave(rowIndex);
	}

	/// <summary>Called when the mouse button is released while the pointer is over the cell. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains information about the mouse position.</param>
	protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
	{
		base.OnMouseUp(e);
	}

	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="dataGridViewElementState"></param>
	/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="errorText">An error message that is associated with the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
	/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
	}
}
