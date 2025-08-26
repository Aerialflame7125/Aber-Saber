using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Displays editable text information in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewTextBoxCell : DataGridViewCell
{
	private int maxInputLength = 32767;

	private static DataGridViewTextBoxEditingControl editingControl;

	/// <summary>Gets the type of the formatted value associated with the cell.</summary>
	/// <returns>A <see cref="T:System.Type" /> representing the <see cref="T:System.String" /> type in all cases.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type FormattedValueType => typeof(string);

	/// <summary>Gets or sets the maximum number of characters that can be entered into the text box.</summary>
	/// <returns>The maximum number of characters that can be entered into the text box; the default value is 32767.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0.</exception>
	[DefaultValue(32767)]
	public virtual int MaxInputLength
	{
		get
		{
			return maxInputLength;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("MaxInputLength coudn't be less than 0.");
			}
			maxInputLength = value;
		}
	}

	/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type ValueType => base.ValueType;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" /> class.</summary>
	public DataGridViewTextBoxCell()
	{
		base.ValueType = typeof(object);
	}

	static DataGridViewTextBoxCell()
	{
		editingControl = new DataGridViewTextBoxEditingControl();
		editingControl.Multiline = false;
		editingControl.BorderStyle = BorderStyle.None;
	}

	/// <summary>Creates an exact copy of this cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object Clone()
	{
		DataGridViewTextBoxCell dataGridViewTextBoxCell = (DataGridViewTextBoxCell)base.Clone();
		dataGridViewTextBoxCell.maxInputLength = maxInputLength;
		return dataGridViewTextBoxCell;
	}

	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public override void DetachEditingControl()
	{
		if (base.DataGridView == null)
		{
			throw new InvalidOperationException("There is no associated DataGridView.");
		}
		base.DataGridView.EditingControlInternal = null;
	}

	/// <summary>Attaches and initializes the hosted editing control.</summary>
	/// <param name="rowIndex">The index of the row being edited.</param>
	/// <param name="initialFormattedValue">The initial value to be displayed in the control.</param>
	/// <param name="dataGridViewCellStyle">A cell style that is used to determine the appearance of the hosted control.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
	{
		if (base.DataGridView == null)
		{
			throw new InvalidOperationException("There is no associated DataGridView.");
		}
		base.DataGridView.EditingControlInternal = editingControl;
		editingControl.EditingControlDataGridView = base.DataGridView;
		editingControl.MaxLength = maxInputLength;
		if (initialFormattedValue == null || initialFormattedValue.ToString() == string.Empty)
		{
			editingControl.Text = string.Empty;
		}
		else
		{
			editingControl.Text = initialFormattedValue.ToString();
		}
		editingControl.ApplyCellStyleToEditingControl(dataGridViewCellStyle);
		editingControl.PrepareEditingControlForEdit(selectAll: true);
	}

	/// <summary>Determines if edit mode should be started based on the given key.</summary>
	/// <returns>true if edit mode should be started; otherwise, false. </returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that represents the key that was pressed.</param>
	/// <filterpriority>1</filterpriority>
	public override bool KeyEntersEditMode(KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Space)
		{
			return true;
		}
		if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.Z)
		{
			return true;
		}
		if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.Divide)
		{
			return true;
		}
		if (e.KeyCode == Keys.BrowserSearch || e.KeyCode == Keys.SelectMedia)
		{
			return true;
		}
		if (e.KeyCode >= Keys.OemSemicolon && e.KeyCode <= Keys.ProcessKey)
		{
			return true;
		}
		if (e.KeyCode == Keys.Attn || e.KeyCode == Keys.Packet)
		{
			return true;
		}
		if (e.KeyCode >= Keys.Exsel && e.KeyCode <= Keys.OemClear)
		{
			return true;
		}
		return false;
	}

	/// <param name="setLocation">true to have the control placed as specified by the other arguments; false to allow the control to place itself.</param>
	/// <param name="setSize">true to specify the size; false to allow the control to size itself. </param>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that defines the cell bounds. </param>
	/// <param name="cellClip">The area that will be used to paint the editing control.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell being edited.</param>
	/// <param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false.</param>
	/// <param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false.</param>
	/// <param name="isFirstDisplayedColumn">true if the hosting cell is in the first visible column; otherwise, false.</param>
	/// <param name="isFirstDisplayedRow">true if the hosting cell is in the first visible row; otherwise, false.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
	{
		cellBounds.Size = new Size(cellBounds.Width - 5, cellBounds.Height + 2);
		cellBounds.Location = new Point(cellBounds.X + 3, (cellBounds.Height - editingControl.Height) / 2 + cellBounds.Y - 1);
		base.PositionEditingControl(setLocation, setSize, cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
		editingControl.Invalidate();
	}

	/// <returns>A string that represents the current object.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"DataGridViewTextBoxCell {{ ColumnIndex={base.ColumnIndex}, RowIndex={base.RowIndex} }}";
	}

	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		if (base.DataGridView == null)
		{
			return Rectangle.Empty;
		}
		object formattedValue = base.FormattedValue;
		Size size = Size.Empty;
		if (formattedValue != null)
		{
			size = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
			size.Height += 2;
		}
		return new Rectangle(0, (base.OwningRow.Height - size.Height) / 2, size.Width, size.Height);
	}

	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty" />.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected override Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		if (base.DataGridView == null || string.IsNullOrEmpty(base.ErrorText))
		{
			return Rectangle.Empty;
		}
		Size size = new Size(12, 11);
		return new Rectangle(new Point(base.Size.Width - size.Width - 5, (base.Size.Height - size.Height) / 2), size);
	}

	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
	/// <param name="rowIndex">The zero-based row index of the cell.</param>
	/// <param name="constraintSize">The cell's maximum allowable size.</param>
	protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
	{
		object formattedValue = base.FormattedValue;
		if (formattedValue != null)
		{
			Size result = DataGridViewCell.MeasureTextSize(graphics, formattedValue.ToString(), cellStyle.Font, TextFormatFlags.Left);
			result.Height = Math.Max(result.Height, 20);
			result.Width += 2;
			return result;
		}
		return new Size(21, 20);
	}

	/// <summary>Called by <see cref="T:System.Windows.Forms.DataGridView" /> when the selection cursor moves onto a cell.</summary>
	/// <param name="rowIndex">The index of the row entered by the mouse.</param>
	/// <param name="throughMouseClick">true if the cell was entered as a result of a mouse click; otherwise, false.</param>
	protected override void OnEnter(int rowIndex, bool throughMouseClick)
	{
	}

	/// <summary>Called by the <see cref="T:System.Windows.Forms.DataGridView" /> when the mouse leaves a cell.</summary>
	/// <param name="rowIndex">The index of the row the mouse has left.</param>
	/// <param name="throughMouseClick">true if the cell was left as a result of a mouse click; otherwise, false.</param>
	protected override void OnLeave(int rowIndex, bool throughMouseClick)
	{
	}

	/// <summary>Called by <see cref="T:System.Windows.Forms.DataGridView" /> when the mouse leaves a cell.</summary>
	/// <param name="e">An <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
	protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
	{
	}

	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="cellState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values that specifies the state of the cell.</param>
	/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="errorText">An error message that is associated with the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
	/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground;
		dataGridViewPaintParts &= paintParts;
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, dataGridViewPaintParts);
		if (!base.IsInEditMode && (paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
		{
			Color foreColor = ((!Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
			TextFormatFlags textFormatFlags = TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
			textFormatFlags |= AlignmentToFlags(cellStyle.Alignment);
			Rectangle bounds = cellBounds;
			bounds.Height -= 2;
			bounds.Width -= 2;
			if ((cellStyle.Alignment & (DataGridViewContentAlignment)7) > DataGridViewContentAlignment.NotSet)
			{
				bounds.Offset(0, 2);
				bounds.Height -= 2;
			}
			if (formattedValue != null)
			{
				TextRenderer.DrawText(graphics, formattedValue.ToString(), cellStyle.Font, bounds, foreColor, textFormatFlags);
			}
		}
		DataGridViewPaintParts dataGridViewPaintParts2 = DataGridViewPaintParts.Border | DataGridViewPaintParts.ErrorIcon | DataGridViewPaintParts.Focus;
		dataGridViewPaintParts2 &= paintParts;
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, dataGridViewPaintParts2);
	}
}
