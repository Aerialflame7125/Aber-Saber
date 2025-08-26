using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms;

/// <summary>Displays a button-like user interface (UI) for use in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewButtonCell : DataGridViewCell
{
	/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> to accessibility client applications.</summary>
	protected class DataGridViewButtonCellAccessibleObject : DataGridViewCellAccessibleObject
	{
		/// <summary>Gets a <see cref="T:System.String" /> that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" />.</summary>
		/// <returns>The <see cref="T:System.String" /> "Press" if the <see cref="P:System.Windows.Forms.DataGridViewCell.ReadOnly" /> property is set to false; otherwise, an empty <see cref="T:System.String" /> ("").</returns>
		public override string DefaultAction
		{
			get
			{
				if (base.Owner.ReadOnly)
				{
					return "Press";
				}
				return string.Empty;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" /> class. </summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" />.</param>
		public DataGridViewButtonCellAccessibleObject(DataGridViewCell owner)
			: base(owner)
		{
		}

		/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" /></summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property does not belong to a <see cref="T:System.Windows.Forms.DataGridView" /> control.-or-The <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property belongs to a shared row.</exception>
		public override void DoDefaultAction()
		{
		}

		/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" />.</summary>
		/// <returns>The value –1.</returns>
		public override int GetChildCount()
		{
			return -1;
		}
	}

	private FlatStyle flatStyle;

	private bool useColumnTextForButtonValue;

	private PushButtonState button_state;

	/// <summary>Gets the type of the cell's hosted editing control.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the underlying editing control.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type EditType => null;

	/// <summary>Gets or sets the style determining the button's appearance.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.FlatStyle" /> value. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(FlatStyle.Standard)]
	public FlatStyle FlatStyle
	{
		get
		{
			return flatStyle;
		}
		set
		{
			if (!Enum.IsDefined(typeof(FlatStyle), value))
			{
				throw new InvalidEnumArgumentException("Value is not valid FlatStyle.");
			}
			if (value == FlatStyle.Popup)
			{
				throw new Exception("FlatStyle cannot be set to Popup in this control.");
			}
		}
	}

	/// <returns>A <see cref="T:System.Type" /> representing the type of the cell's formatted value.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type FormattedValueType => typeof(string);

	/// <summary>Gets or sets a value indicating whether the owning column's text will appear on the button displayed by the cell.</summary>
	/// <returns>true if the value of the <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property will automatically match the value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> property of the owning column; otherwise, false. The default is false.</returns>
	[DefaultValue(false)]
	public bool UseColumnTextForButtonValue
	{
		get
		{
			return useColumnTextForButtonValue;
		}
		set
		{
			useColumnTextForButtonValue = value;
		}
	}

	/// <returns>A <see cref="T:System.Type" /> representing the data type of the value in the cell.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type ValueType => ((object)base.ValueType != null) ? base.ValueType : typeof(object);

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> class.</summary>
	public DataGridViewButtonCell()
	{
		useColumnTextForButtonValue = false;
		button_state = PushButtonState.Normal;
	}

	/// <summary>Creates an exact copy of this cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object Clone()
	{
		DataGridViewButtonCell dataGridViewButtonCell = (DataGridViewButtonCell)base.Clone();
		dataGridViewButtonCell.flatStyle = flatStyle;
		dataGridViewButtonCell.useColumnTextForButtonValue = useColumnTextForButtonValue;
		return dataGridViewButtonCell;
	}

	/// <summary>Returns the string representation of the cell.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the current cell.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return GetType().Name + ": RowIndex: " + base.RowIndex + "; ColumnIndex: " + base.ColumnIndex + ";";
	}

	/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />. </summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewButtonCell.DataGridViewButtonCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />. </returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new DataGridViewButtonCellAccessibleObject(this);
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
		Rectangle empty = Rectangle.Empty;
		empty.Height = base.OwningRow.Height - 1;
		empty.Width = base.OwningColumn.Width - 1;
		return empty;
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
			result.Height = Math.Max(result.Height, 21);
			result.Width += 10;
			return result;
		}
		return new Size(21, 21);
	}

	/// <summary>Retrieves the text associated with the button.</summary>
	/// <returns>The value of the <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> or the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> value of the owning column if <see cref="P:System.Windows.Forms.DataGridViewButtonCell.UseColumnTextForButtonValue" /> is true. </returns>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	protected override object GetValue(int rowIndex)
	{
		if (useColumnTextForButtonValue)
		{
			return (base.OwningColumn as DataGridViewButtonColumn).Text;
		}
		return base.GetValue(rowIndex);
	}

	/// <summary>Indicates whether a row is unshared if a key is pressed while the focus is on a cell in the row.</summary>
	/// <returns>true if the user pressed the SPACE key without modifier keys; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
	{
		return e.KeyData == Keys.Space;
	}

	/// <summary>Indicates whether a row is unshared when a key is released while the focus is on a cell in the row.</summary>
	/// <returns>true if the user released the SPACE key; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
	{
		return e.KeyData == Keys.Space;
	}

	/// <summary>Indicates whether a row will be unshared when the mouse button is held down while the pointer is on a cell in the row.</summary>
	/// <returns>true if the user pressed the left mouse button; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
	protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return e.Button == MouseButtons.Left;
	}

	/// <summary>Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.</summary>
	/// <returns>true if the cell was the last cell receiving a mouse click; otherwise, false.</returns>
	/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
	protected override bool MouseEnterUnsharesRow(int rowIndex)
	{
		return false;
	}

	/// <summary>Indicates whether a row will be unshared when the mouse pointer leaves the row.</summary>
	/// <returns>true if the button displayed by the cell is in the pressed state; otherwise, false.</returns>
	/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
	protected override bool MouseLeaveUnsharesRow(int rowIndex)
	{
		return button_state == PushButtonState.Pressed;
	}

	/// <summary>Indicates whether a row will be unshared when the mouse button is released while the pointer is on a cell in the row. </summary>
	/// <returns>true if the left mouse button was released; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
	protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return e.Button == MouseButtons.Left;
	}

	/// <summary>Called when a character key is pressed while the focus is on the cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
	/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
	protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
	{
		if ((e.KeyData & Keys.Space) == Keys.Space)
		{
			button_state = PushButtonState.Pressed;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when a character key is released while the focus is on the cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data</param>
	/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
	protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
	{
		if ((e.KeyData & Keys.Space) == Keys.Space)
		{
			button_state = PushButtonState.Normal;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when the focus moves from the cell.</summary>
	/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
	/// <param name="throughMouseClick">true if focus left the cell as a result of user mouse click; false if focus left due to a programmatic cell change.</param>
	protected override void OnLeave(int rowIndex, bool throughMouseClick)
	{
		if (button_state != PushButtonState.Normal)
		{
			button_state = PushButtonState.Normal;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when the mouse button is held down while the pointer is on the cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
	protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
	{
		if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
		{
			button_state = PushButtonState.Pressed;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when the mouse pointer moves out of the cell.</summary>
	/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
	protected override void OnMouseLeave(int rowIndex)
	{
		if (button_state != PushButtonState.Normal)
		{
			button_state = PushButtonState.Normal;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when the mouse pointer moves while it is over the cell. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
	protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
	{
		if (button_state != PushButtonState.Normal && button_state != PushButtonState.Hot)
		{
			button_state = PushButtonState.Hot;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when the mouse button is released while the pointer is on the cell. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
	protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
	{
		if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
		{
			button_state = PushButtonState.Normal;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell" />.</param>
	/// <param name="clipBounds">A <see cref="T:System.Drawing.Rectangle" /> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView" /> that needs to be repainted.</param>
	/// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle" /> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="rowIndex">The row index of the cell that is being painted.</param>
	/// <param name="elementState"></param>
	/// <param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being painted.</param>
	/// <param name="errorText">An error message that is associated with the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that contains formatting and style information about the cell.</param>
	/// <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle" /> that contains border styles for the cell that is being painted.</param>
	/// <param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts" /> values that specifies which parts of the cell need to be painted.</param>
	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
	}

	internal override void PaintPartBackground(Graphics graphics, Rectangle cellBounds, DataGridViewCellStyle style)
	{
		ButtonRenderer.DrawButton(graphics, cellBounds, button_state);
	}

	internal override void PaintPartSelectionBackground(Graphics graphics, Rectangle cellBounds, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle)
	{
		cellBounds.Inflate(-2, -2);
		base.PaintPartSelectionBackground(graphics, cellBounds, cellState, cellStyle);
	}

	internal override void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
	{
		Color foreColor = ((!Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
		TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
		cellBounds.Height -= 2;
		cellBounds.Width -= 2;
		if (formattedValue != null)
		{
			TextRenderer.DrawText(graphics, formattedValue.ToString(), cellStyle.Font, cellBounds, foreColor, flags);
		}
	}
}
