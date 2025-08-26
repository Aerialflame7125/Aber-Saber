using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms;

/// <summary>Displays a check box user interface (UI) to use in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewCheckBoxCell : DataGridViewCell, IDataGridViewEditingCell
{
	/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> to accessibility client applications.</summary>
	protected class DataGridViewCheckBoxCellAccessibleObject : DataGridViewCellAccessibleObject
	{
		/// <summary>Gets a string that represents the default action of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</summary>
		/// <returns>A description of the default action.</returns>
		public override string DefaultAction
		{
			get
			{
				if (base.Owner.ReadOnly)
				{
					return string.Empty;
				}
				throw new NotImplementedException();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" /> class. </summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</param>
		public DataGridViewCheckBoxCellAccessibleObject(DataGridViewCell owner)
			: base(owner)
		{
		}

		/// <summary>Performs the default action of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property does not belong to a DataGridView control.-or-The <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property belongs to a shared row.</exception>
		public override void DoDefaultAction()
		{
		}

		/// <summary>Gets the number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" />.</summary>
		/// <returns>The value –1.</returns>
		public override int GetChildCount()
		{
			return -1;
		}
	}

	private object editingCellFormattedValue;

	private bool editingCellValueChanged;

	private object falseValue;

	private FlatStyle flatStyle;

	private object indeterminateValue;

	private bool threeState;

	private object trueValue;

	private PushButtonState check_state;

	/// <summary>Gets or sets the formatted value of the control hosted by the cell when it is in edit mode.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the cell's value.</returns>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property value is null.-or-The assigned value is null or is not of the type indicated by the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property.-or- The assigned value is not of type <see cref="T:System.Boolean" /> nor of type <see cref="T:System.Windows.Forms.CheckState" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property value is null.</exception>
	public virtual object EditingCellFormattedValue
	{
		get
		{
			return editingCellFormattedValue;
		}
		set
		{
			if ((object)FormattedValueType == null || value == null || (object)value.GetType() != FormattedValueType || !(value is bool) || !(value is CheckState))
			{
				throw new ArgumentException("Cannot set this property.");
			}
			editingCellFormattedValue = value;
		}
	}

	/// <summary>Gets or sets a flag indicating that the value has been changed for this cell.</summary>
	/// <returns>true if the cell's value has changed; otherwise, false.</returns>
	public virtual bool EditingCellValueChanged
	{
		get
		{
			return editingCellValueChanged;
		}
		set
		{
			editingCellValueChanged = value;
		}
	}

	/// <summary>Gets the type of the cell's hosted editing control.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the underlying editing control.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type EditType => null;

	/// <summary>Gets or sets the underlying value corresponding to a cell value of false.</summary>
	/// <returns>An <see cref="T:System.Object" /> corresponding to a cell value of false. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	public object FalseValue
	{
		get
		{
			return falseValue;
		}
		set
		{
			falseValue = value;
		}
	}

	/// <summary>Gets or sets the flat style appearance of the check box user interface (UI).</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.FlatStyle" /> value.</exception>
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

	/// <summary>Gets the type of the cell display value. </summary>
	/// <returns>A <see cref="T:System.Type" /> representing the display type of the cell.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type FormattedValueType
	{
		get
		{
			if (ThreeState)
			{
				return typeof(CheckState);
			}
			return typeof(bool);
		}
	}

	/// <summary>Gets or sets the underlying value corresponding to an indeterminate or null cell value.</summary>
	/// <returns>An <see cref="T:System.Object" /> corresponding to an indeterminate or null cell value. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	public object IndeterminateValue
	{
		get
		{
			return indeterminateValue;
		}
		set
		{
			indeterminateValue = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether ternary mode has been enabled for the hosted check box control.</summary>
	/// <returns>true if ternary mode is enabled; false if binary mode is enabled. The default is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool ThreeState
	{
		get
		{
			return threeState;
		}
		set
		{
			threeState = value;
		}
	}

	/// <summary>Gets or sets the underlying value corresponding to a cell value of true.</summary>
	/// <returns>An <see cref="T:System.Object" /> corresponding to a cell value of true. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	public object TrueValue
	{
		get
		{
			return trueValue;
		}
		set
		{
			trueValue = value;
		}
	}

	/// <summary>Gets the data type of the values in the cell.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the underlying value of the cell.</returns>
	/// <filterpriority>1</filterpriority>
	public override Type ValueType
	{
		get
		{
			if ((object)base.ValueType == null)
			{
				if (base.OwningColumn != null && (object)base.OwningColumn.ValueType != null)
				{
					return base.OwningColumn.ValueType;
				}
				if (ThreeState)
				{
					return typeof(CheckState);
				}
				return typeof(bool);
			}
			return base.ValueType;
		}
		set
		{
			base.ValueType = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> class to its default state.</summary>
	public DataGridViewCheckBoxCell()
	{
		check_state = PushButtonState.Normal;
		editingCellFormattedValue = false;
		editingCellValueChanged = false;
		falseValue = null;
		flatStyle = FlatStyle.Standard;
		indeterminateValue = null;
		threeState = false;
		trueValue = null;
		ValueType = null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> class, enabling binary or ternary state.</summary>
	/// <param name="threeState">true to enable ternary state; false to enable binary state.</param>
	public DataGridViewCheckBoxCell(bool threeState)
		: this()
	{
		this.threeState = threeState;
		editingCellFormattedValue = CheckState.Unchecked;
	}

	/// <summary>Creates an exact copy of this cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object Clone()
	{
		DataGridViewCheckBoxCell dataGridViewCheckBoxCell = (DataGridViewCheckBoxCell)base.Clone();
		dataGridViewCheckBoxCell.editingCellValueChanged = editingCellValueChanged;
		dataGridViewCheckBoxCell.editingCellFormattedValue = editingCellFormattedValue;
		dataGridViewCheckBoxCell.falseValue = falseValue;
		dataGridViewCheckBoxCell.flatStyle = flatStyle;
		dataGridViewCheckBoxCell.indeterminateValue = indeterminateValue;
		dataGridViewCheckBoxCell.threeState = threeState;
		dataGridViewCheckBoxCell.trueValue = trueValue;
		dataGridViewCheckBoxCell.ValueType = ValueType;
		return dataGridViewCheckBoxCell;
	}

	/// <summary>Gets the formatted value of the cell while it is in edit mode.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing the formatted value of the editing cell. </returns>
	/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that describes the context in which any formatting error occurs. </param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridViewCheckBoxCell.FormattedValueType" /> property value is null.</exception>
	public virtual object GetEditingCellFormattedValue(DataGridViewDataErrorContexts context)
	{
		if ((object)FormattedValueType == null)
		{
			throw new InvalidOperationException("FormattedValueType is null.");
		}
		if ((context & DataGridViewDataErrorContexts.ClipboardContent) != 0)
		{
			return Convert.ToString(base.Value);
		}
		if (editingCellFormattedValue == null)
		{
			if (threeState)
			{
				return CheckState.Indeterminate;
			}
			return false;
		}
		return editingCellFormattedValue;
	}

	/// <summary>Converts a value formatted for display to an actual cell value.</summary>
	/// <returns>The cell value.</returns>
	/// <param name="formattedValue">The display value of the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
	/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the display value type, or null to use the default converter.</param>
	/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> for the cell value type, or null to use the default converter.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="cellStyle" /> is null.</exception>
	/// <exception cref="T:System.FormatException">The <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property value is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="formattedValue" /> is null.- or -The type of <paramref name="formattedValue" /> does not match the type indicated by the <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType" /> property. </exception>
	public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
	{
		if (cellStyle == null)
		{
			throw new ArgumentNullException("CellStyle is null");
		}
		if ((object)FormattedValueType == null)
		{
			throw new FormatException("FormattedValueType is null.");
		}
		if (formattedValue == null || (object)formattedValue.GetType() != FormattedValueType)
		{
			throw new ArgumentException("FormattedValue is null or is not instance of FormattedValueType.");
		}
		return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
	}

	/// <summary>This method is not meaningful for this type.</summary>
	/// <param name="selectAll">This parameter is ignored.</param>
	public virtual void PrepareEditingCellForEdit(bool selectAll)
	{
		editingCellFormattedValue = GetCurrentValue();
	}

	/// <summary>Returns the string representation of the cell.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the current cell.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"DataGridViewCheckBoxCell {{ ColumnIndex={base.ColumnIndex}, RowIndex={base.RowIndex} }}";
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when the cell content is clicked.</summary>
	/// <returns>true if the cell is in edit mode; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains data about the mouse click.</param>
	protected override bool ContentClickUnsharesRow(DataGridViewCellEventArgs e)
	{
		return base.IsInEditMode;
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when the cell content is double-clicked.</summary>
	/// <returns>true if the cell is in edit mode; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains data about the double-click.</param>
	protected override bool ContentDoubleClickUnsharesRow(DataGridViewCellEventArgs e)
	{
		return base.IsInEditMode;
	}

	/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />. </summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell.DataGridViewCheckBoxCellAccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />. </returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new DataGridViewCheckBoxCellAccessibleObject(this);
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
		return new Rectangle((base.Size.Width - 13) / 2, (base.Size.Height - 13) / 2, 13, 13);
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

	/// <summary>Gets the formatted value of the cell's data. </summary>
	/// <returns>The value of the cell's data after formatting has been applied or null if the cell is not part of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</returns>
	/// <param name="value">The value to be formatted. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> in effect for the cell.</param>
	/// <param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param>
	/// <param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter" /> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param>
	/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values describing the context in which the formatted value is needed.</param>
	protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
	{
		if (base.DataGridView == null || value == null)
		{
			if (threeState)
			{
				return CheckState.Indeterminate;
			}
			return false;
		}
		return value;
	}

	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
	/// <param name="rowIndex">The zero-based row index of the cell.</param>
	/// <param name="constraintSize">The cell's maximum allowable size.</param>
	protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
	{
		return new Size(21, 20);
	}

	/// <summary>Indicates whether the row containing the cell is unshared when a key is pressed while the cell has focus.</summary>
	/// <returns>true if the SPACE key is pressed and the CTRL, ALT, and SHIFT keys are all not pressed; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key press. </param>
	/// <param name="rowIndex">The index of the row containing the cell. </param>
	protected override bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
	{
		return e.KeyData == Keys.Space;
	}

	/// <summary>Indicates whether the row containing the cell is unshared when a key is released while the cell has focus.</summary>
	/// <returns>true if the SPACE key is released; otherwise, false.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains data about the key press. </param>
	/// <param name="rowIndex">The index of the row containing the cell. </param>
	protected override bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
	{
		return e.KeyData == Keys.Space;
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is pressed while the pointer is over the cell.</summary>
	/// <returns>Always true.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
	protected override bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return e.Button == MouseButtons.Left;
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer moves over the cell.</summary>
	/// <returns>true if the cell was the last cell receiving a mouse click; otherwise, false.</returns>
	/// <param name="rowIndex">The index of the row containing the cell.</param>
	protected override bool MouseEnterUnsharesRow(int rowIndex)
	{
		return false;
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when the mouse pointer leaves the cell.</summary>
	/// <returns>true if the button is not in the normal state; false if the button is in the pressed state.</returns>
	/// <param name="rowIndex">The index of the row containing the cell.</param>
	protected override bool MouseLeaveUnsharesRow(int rowIndex)
	{
		return check_state == PushButtonState.Pressed;
	}

	/// <summary>Indicates whether the row containing the cell will be unshared when the mouse button is released while the pointer is over the cell.</summary>
	/// <returns>Always true.</returns>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains data about the mouse click.</param>
	protected override bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)
	{
		return e.Button == MouseButtons.Left;
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
	protected override void OnContentClick(DataGridViewCellEventArgs e)
	{
		if (ReadOnly)
		{
			return;
		}
		if (!base.IsInEditMode)
		{
			base.DataGridView.BeginEdit(selectAll: false);
		}
		CheckState currentValue = GetCurrentValue();
		if (threeState)
		{
			switch (currentValue)
			{
			case CheckState.Indeterminate:
				editingCellFormattedValue = CheckState.Unchecked;
				break;
			case CheckState.Checked:
				editingCellFormattedValue = CheckState.Indeterminate;
				break;
			default:
				editingCellFormattedValue = CheckState.Checked;
				break;
			}
		}
		else if (currentValue == CheckState.Checked)
		{
			editingCellFormattedValue = false;
		}
		else
		{
			editingCellFormattedValue = true;
		}
		editingCellValueChanged = true;
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
	protected override void OnContentDoubleClick(DataGridViewCellEventArgs e)
	{
	}

	/// <summary>Called when a character key is pressed while the focus is on a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data</param>
	/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
	protected override void OnKeyDown(KeyEventArgs e, int rowIndex)
	{
		if (!ReadOnly && (e.KeyData & Keys.Space) == Keys.Space)
		{
			check_state = PushButtonState.Pressed;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when a character key is released while the focus is on a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data</param>
	/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
	protected override void OnKeyUp(KeyEventArgs e, int rowIndex)
	{
		if (!ReadOnly && (e.KeyData & Keys.Space) == Keys.Space)
		{
			check_state = PushButtonState.Normal;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when the focus moves from a cell.</summary>
	/// <param name="rowIndex">The row index of the current cell, or -1 if the cell is not owned by a row.</param>
	/// <param name="throughMouseClick">true if the cell was left as a result of user mouse click rather than a programmatic cell change; otherwise, false.</param>
	protected override void OnLeave(int rowIndex, bool throughMouseClick)
	{
		if (!ReadOnly && check_state != PushButtonState.Normal)
		{
			check_state = PushButtonState.Normal;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when the mouse button is held down while the pointer is on a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
	protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
	{
		if (!ReadOnly && (e.Button & MouseButtons.Left) == MouseButtons.Left)
		{
			check_state = PushButtonState.Pressed;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when the mouse pointer moves from a cell.</summary>
	/// <param name="rowIndex">The row index of the current cell or -1 if the cell is not owned by a row.</param>
	protected override void OnMouseLeave(int rowIndex)
	{
		if (!ReadOnly && check_state != PushButtonState.Normal)
		{
			check_state = PushButtonState.Normal;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when the mouse pointer moves within a cell.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
	protected override void OnMouseMove(DataGridViewCellMouseEventArgs e)
	{
		if (!ReadOnly && check_state != PushButtonState.Normal && check_state != PushButtonState.Hot)
		{
			check_state = PushButtonState.Hot;
			base.DataGridView.InvalidateCell(this);
		}
	}

	/// <summary>Called when the mouse button is released while the pointer is on a cell. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs" /> that contains the event data.</param>
	protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
	{
		if (!ReadOnly && (e.Button & MouseButtons.Left) == MouseButtons.Left)
		{
			check_state = PushButtonState.Normal;
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

	internal override void PaintPartContent(Graphics graphics, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, DataGridViewCellStyle cellStyle, object formattedValue)
	{
		CheckBoxState checkBoxState = GetCurrentValue() switch
		{
			CheckState.Unchecked => (CheckBoxState)check_state, 
			CheckState.Checked => (CheckBoxState)(check_state + 4), 
			_ => (CheckBoxState)((!threeState) ? check_state : (check_state + 8)), 
		};
		Point glyphLocation = new Point(cellBounds.X + (base.Size.Width - 13) / 2, cellBounds.Y + (base.Size.Height - 13) / 2);
		CheckBoxRenderer.DrawCheckBox(graphics, glyphLocation, checkBoxState);
	}

	private CheckState GetCurrentValue()
	{
		CheckState result = CheckState.Indeterminate;
		object obj = ((!editingCellValueChanged) ? base.Value : editingCellFormattedValue);
		if (obj == null)
		{
			result = CheckState.Indeterminate;
		}
		else if (obj is bool)
		{
			if ((bool)obj)
			{
				result = CheckState.Checked;
			}
			else if (!(bool)obj)
			{
				result = CheckState.Unchecked;
			}
		}
		else if (obj is CheckState)
		{
			result = (CheckState)(int)obj;
		}
		return result;
	}
}
