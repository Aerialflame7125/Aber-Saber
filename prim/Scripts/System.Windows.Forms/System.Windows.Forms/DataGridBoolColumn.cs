using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Specifies a column in which each cell contains a check box for representing a Boolean value.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridBoolColumn : DataGridColumnStyle
{
	[Flags]
	private enum CheckState
	{
		Checked = 1,
		UnChecked = 2,
		Null = 4,
		Selected = 8
	}

	private bool allow_null;

	private object false_value;

	private object null_value;

	private object true_value;

	private int editing_row;

	private CheckState editing_state;

	private CheckState model_state;

	private Size checkbox_size;

	private static object AllowNullChangedEvent;

	private static object FalseValueChangedEvent;

	private static object TrueValueChangedEvent;

	/// <summary>Gets or sets a value indicating whether null values are allowed.</summary>
	/// <returns>true if null values are allowed, otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool AllowNull
	{
		get
		{
			return allow_null;
		}
		set
		{
			if (value != allow_null)
			{
				allow_null = value;
				((EventHandler)base.Events[AllowNullChanged])?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the actual value used when setting the value of the column to false.</summary>
	/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	[TypeConverter(typeof(StringConverter))]
	public object FalseValue
	{
		get
		{
			return false_value;
		}
		set
		{
			if (value != false_value)
			{
				false_value = value;
				((EventHandler)base.Events[FalseValueChanged])?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the actual value used when setting the value of the column to <see cref="F:System.DBNull.Value" />.</summary>
	/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(StringConverter))]
	public object NullValue
	{
		get
		{
			return null_value;
		}
		set
		{
			if (value != null_value)
			{
				null_value = value;
			}
		}
	}

	/// <summary>Gets or sets the actual value used when setting the value of the column to true.</summary>
	/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	[TypeConverter(typeof(StringConverter))]
	public object TrueValue
	{
		get
		{
			return true_value;
		}
		set
		{
			if (value != true_value)
			{
				true_value = value;
				((EventHandler)base.Events[TrueValueChanged])?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridBoolColumn.AllowNull" /> property is changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler AllowNullChanged
	{
		add
		{
			base.Events.AddHandler(AllowNullChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AllowNullChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridBoolColumn.FalseValue" /> property is changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler FalseValueChanged
	{
		add
		{
			base.Events.AddHandler(FalseValueChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(FalseValueChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridBoolColumn.TrueValue" /> property value is changed.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler TrueValueChanged
	{
		add
		{
			base.Events.AddHandler(TrueValueChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(TrueValueChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> class.</summary>
	public DataGridBoolColumn()
		: this(null, isDefault: false)
	{
	}

	/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
	/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the column. </param>
	public DataGridBoolColumn(PropertyDescriptor prop)
		: this(prop, isDefault: false)
	{
	}

	/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />, and specifying whether the column style is a default column.</summary>
	/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with the column. </param>
	/// <param name="isDefault">true to specify the column as the default; otherwise, false. </param>
	public DataGridBoolColumn(PropertyDescriptor prop, bool isDefault)
		: base(prop)
	{
		false_value = false;
		null_value = null;
		true_value = true;
		allow_null = true;
		is_default = isDefault;
		checkbox_size = new Size(ThemeEngine.Current.DataGridMinimumColumnCheckBoxWidth, ThemeEngine.Current.DataGridMinimumColumnCheckBoxHeight);
	}

	static DataGridBoolColumn()
	{
		AllowNullChanged = new object();
		FalseValueChanged = new object();
		TrueValueChanged = new object();
	}

	/// <summary>Initiates a request to interrupt an edit procedure.</summary>
	/// <param name="rowNum">The number of the row in which an operation is being interrupted. </param>
	protected internal override void Abort(int rowNum)
	{
		if (rowNum == editing_row)
		{
			grid.Invalidate(grid.GetCurrentCellBounds());
			editing_row = -1;
		}
	}

	/// <summary>Initiates a request to complete an editing procedure.</summary>
	/// <returns>true if the editing procedure committed successfully; otherwise, false.</returns>
	/// <param name="dataSource">The <see cref="T:System.Data.DataView" /> of the edited column. </param>
	/// <param name="rowNum">The number of the edited row. </param>
	protected internal override bool Commit(CurrencyManager dataSource, int rowNum)
	{
		if (rowNum == editing_row)
		{
			SetColumnValueAtRow(dataSource, rowNum, FromStateToValue(editing_state));
			grid.Invalidate(grid.GetCurrentCellBounds());
			editing_row = -1;
		}
		return true;
	}

	[System.MonoTODO("Stub, does nothing")]
	protected internal override void ConcedeFocus()
	{
		base.ConcedeFocus();
	}

	/// <summary>Prepares the cell for editing a value.</summary>
	/// <param name="source">The <see cref="T:System.Data.DataView" /> of the edited cell. </param>
	/// <param name="rowNum">The row number of the edited cell. </param>
	/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited. </param>
	/// <param name="readOnly">true if the value is read only; otherwise, false. </param>
	/// <param name="displayText">The text to display in the cell. </param>
	/// <param name="cellIsVisible">true to show the cell; otherwise, false. </param>
	protected internal override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible)
	{
		editing_row = rowNum;
		model_state = FromValueToState(GetColumnValueAtRow(source, rowNum));
		editing_state = model_state | CheckState.Selected;
		grid.Invalidate(grid.GetCurrentCellBounds());
	}

	/// <summary>Enters a <see cref="F:System.DBNull.Value" /> into the column.</summary>
	/// <exception cref="T:System.Exception">The <see cref="P:System.Windows.Forms.DataGridBoolColumn.AllowNull" /> property is set to false. </exception>
	[System.MonoTODO("Stub, does nothing")]
	protected internal override void EnterNullValue()
	{
		base.EnterNullValue();
	}

	private bool ValueEquals(object value, object obj)
	{
		return value?.Equals(obj) ?? (obj == null);
	}

	/// <summary>Gets the value at the specified row.</summary>
	/// <returns>The value, typed as <see cref="T:System.Object" />.</returns>
	/// <param name="lm">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the column. </param>
	/// <param name="row">The row number. </param>
	protected internal override object GetColumnValueAtRow(CurrencyManager lm, int row)
	{
		object columnValueAtRow = base.GetColumnValueAtRow(lm, row);
		if (ValueEquals(DBNull.Value, columnValueAtRow))
		{
			return null_value;
		}
		if (ValueEquals(true, columnValueAtRow))
		{
			return true_value;
		}
		return false_value;
	}

	/// <summary>Gets the height of a cell in a column.</summary>
	/// <returns>The height of the column. The default is 16.</returns>
	protected internal override int GetMinimumHeight()
	{
		return checkbox_size.Height;
	}

	/// <summary>Gets the height used when resizing columns.</summary>
	/// <returns>The height used to automatically resize cells in a column.</returns>
	/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that draws on the screen. </param>
	/// <param name="value">An <see cref="T:System.Object" /> that contains the value to be drawn to the screen. </param>
	protected internal override int GetPreferredHeight(Graphics g, object value)
	{
		return checkbox_size.Height;
	}

	/// <summary>Gets the optimum width and height of a cell given a specific value to contain.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the drawing information for the cell.</returns>
	/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> that draws the cell. </param>
	/// <param name="value">The value that must fit in the cell. </param>
	protected internal override Size GetPreferredSize(Graphics g, object value)
	{
		return checkbox_size;
	}

	/// <summary>Draws the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the given <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" /> and row number.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the column. </param>
	/// <param name="rowNum">The number of the row referred to in the underlying data. </param>
	protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
	{
		Paint(g, bounds, source, rowNum, alignToRight: false);
	}

	/// <summary>Draws the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the given <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, row number, and alignment settings.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the column. </param>
	/// <param name="rowNum">The number of the row in the underlying data table being referred to. </param>
	/// <param name="alignToRight">A value indicating whether to align the content to the right. true if the content is aligned to the right, otherwise, false. </param>
	protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
	{
		Paint(g, bounds, source, rowNum, ThemeEngine.Current.ResPool.GetSolidBrush(DataGridTableStyle.BackColor), ThemeEngine.Current.ResPool.GetSolidBrush(DataGridTableStyle.ForeColor), alignToRight);
	}

	/// <summary>Draws the <see cref="T:System.Windows.Forms.DataGridBoolColumn" /> with the given <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, row number, <see cref="T:System.Drawing.Brush" />, and <see cref="T:System.Drawing.Color" />.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to draw to. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the column. </param>
	/// <param name="rowNum">The number of the row in the underlying data table being referred to. </param>
	/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> used to paint the background color. </param>
	/// <param name="foreBrush">A <see cref="T:System.Drawing.Color" /> used to paint the foreground color. </param>
	/// <param name="alignToRight">A value indicating whether to align the content to the right. true if the content is aligned to the right, otherwise, false. </param>
	protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
	{
		Rectangle rectangle = default(Rectangle);
		CheckState checkState = ((rowNum != editing_row) ? FromValueToState(GetColumnValueAtRow(source, rowNum)) : editing_state);
		rectangle.X = bounds.X + (bounds.Width - checkbox_size.Width - 2) / 2;
		rectangle.Y = bounds.Y + (bounds.Height - checkbox_size.Height - 2) / 2;
		rectangle.Width = checkbox_size.Width - 2;
		rectangle.Height = checkbox_size.Height - 2;
		if ((checkState & CheckState.Selected) == CheckState.Selected)
		{
			backBrush = ThemeEngine.Current.ResPool.GetSolidBrush(grid.SelectionBackColor);
			checkState &= ~CheckState.Selected;
		}
		g.FillRectangle(backBrush, bounds);
		ThemeEngine.Current.CPDrawCheckBox(g, rectangle, checkState switch
		{
			CheckState.Checked => ButtonState.Checked, 
			CheckState.Null => ButtonState.Inactive | ButtonState.Checked, 
			_ => ButtonState.Normal, 
		});
		PaintGridLine(g, bounds);
	}

	/// <summary>Sets the value of a specified row.</summary>
	/// <param name="lm">The <see cref="T:System.Windows.Forms.CurrencyManager" /> for the column. </param>
	/// <param name="row">The row number. </param>
	/// <param name="value">The value to set, typed as <see cref="T:System.Object" />. </param>
	protected internal override void SetColumnValueAtRow(CurrencyManager lm, int row, object value)
	{
		object value2 = null;
		if (ValueEquals(null_value, value))
		{
			value2 = DBNull.Value;
		}
		else if (ValueEquals(true_value, value))
		{
			value2 = true;
		}
		else if (ValueEquals(false_value, value))
		{
			value2 = false;
		}
		base.SetColumnValueAtRow(lm, row, value2);
	}

	private object FromStateToValue(CheckState state)
	{
		if ((state & CheckState.Checked) == CheckState.Checked)
		{
			return true_value;
		}
		if ((state & CheckState.Null) == CheckState.Null)
		{
			return null_value;
		}
		return false_value;
	}

	private CheckState FromValueToState(object obj)
	{
		if (ValueEquals(true_value, obj))
		{
			return CheckState.Checked;
		}
		if (ValueEquals(null_value, obj))
		{
			return CheckState.Null;
		}
		return CheckState.UnChecked;
	}

	private CheckState GetNextState(CheckState state)
	{
		return (CheckState)(((state & ~CheckState.Selected) switch
		{
			CheckState.Checked => (!AllowNull) ? 2 : 4, 
			CheckState.Null => 2, 
			_ => 1, 
		}) | (int)(state & CheckState.Selected));
	}

	internal override void OnKeyDown(KeyEventArgs ke, int row, int column)
	{
		Keys keyCode = ke.KeyCode;
		if (keyCode == Keys.Space)
		{
			NextState(row, column);
		}
	}

	internal override void OnMouseDown(MouseEventArgs e, int row, int column)
	{
		NextState(row, column);
	}

	private void NextState(int row, int column)
	{
		grid.ColumnStartedEditing(default(Rectangle));
		editing_state = GetNextState(editing_state);
		grid.Invalidate(grid.GetCellBounds(row, column));
	}
}
