using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System.Windows.Forms;

/// <summary>Hosts a <see cref="T:System.Windows.Forms.TextBox" /> control in a cell of a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> for editing strings.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridTextBoxColumn : DataGridColumnStyle
{
	private string format;

	private IFormatProvider format_provider;

	private StringFormat string_format = new StringFormat();

	private DataGridTextBox textbox;

	private static readonly int offset_x = 2;

	private static readonly int offset_y = 2;

	/// <summary>Gets or sets the character(s) that specify how text is formatted.</summary>
	/// <returns>The character or characters that specify how text is formatted.</returns>
	/// <filterpriority>1</filterpriority>
	[Editor("System.Windows.Forms.Design.DataGridColumnStyleFormatEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[DefaultValue(null)]
	public string Format
	{
		get
		{
			return format;
		}
		set
		{
			if (value != format)
			{
				format = value;
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the culture specific information used to determine how values are formatted.</summary>
	/// <returns>An object that implements the <see cref="T:System.IFormatProvider" /> interface, such as the <see cref="T:System.Globalization.CultureInfo" /> class.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public IFormatProvider FormatInfo
	{
		get
		{
			return format_provider;
		}
		set
		{
			if (value != format_provider)
			{
				format_provider = value;
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that formats the values displayed in the column.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	public override PropertyDescriptor PropertyDescriptor
	{
		set
		{
			base.PropertyDescriptor = value;
		}
	}

	/// <summary>Sets a value indicating whether the text box column is read-only.</summary>
	/// <returns>true if the text box column is read-only; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
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

	/// <summary>Gets the hosted <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TextBox" /> control hosted by the column.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual TextBox TextBox => textbox;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> class.</summary>
	public DataGridTextBoxColumn()
		: this(null, string.Empty, isDefault: false)
	{
	}

	/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> with a specified <see cref="T:System.ComponentModel.PropertyDescriptor" />.</summary>
	/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the column with which the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> will be associated. </param>
	public DataGridTextBoxColumn(PropertyDescriptor prop)
		: this(prop, string.Empty, isDefault: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> class using the specified <see cref="T:System.ComponentModel.PropertyDescriptor" />. Specifies whether the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> is a default column.</summary>
	/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to be associated with the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />. </param>
	/// <param name="isDefault">Specifies whether the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> is a default column. </param>
	public DataGridTextBoxColumn(PropertyDescriptor prop, bool isDefault)
		: this(prop, string.Empty, isDefault)
	{
	}

	/// <summary>Initializes a new instance of a <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> with the specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> and format.</summary>
	/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the column with which the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> will be associated. </param>
	/// <param name="format">The format used to format the column values. </param>
	public DataGridTextBoxColumn(PropertyDescriptor prop, string format)
		: this(prop, format, isDefault: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> class with a specified <see cref="T:System.ComponentModel.PropertyDescriptor" /> and format. Specifies whether the column is the default column.</summary>
	/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> to be associated with the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" />. </param>
	/// <param name="format">The format used. </param>
	/// <param name="isDefault">Specifies whether the <see cref="T:System.Windows.Forms.DataGridTextBoxColumn" /> is the default column. </param>
	public DataGridTextBoxColumn(PropertyDescriptor prop, string format, bool isDefault)
		: base(prop)
	{
		Format = format;
		is_default = isDefault;
		textbox = new DataGridTextBox();
		textbox.Multiline = true;
		textbox.WordWrap = false;
		textbox.BorderStyle = BorderStyle.None;
		textbox.Visible = false;
	}

	/// <summary>Initiates a request to interrupt an edit procedure.</summary>
	/// <param name="rowNum">The number of the row in which an edit operation is being interrupted. </param>
	protected internal override void Abort(int rowNum)
	{
		EndEdit();
	}

	/// <summary>Inititates a request to complete an editing procedure.</summary>
	/// <returns>true if the value was successfully committed; otherwise, false.</returns>
	/// <param name="dataSource">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to. </param>
	/// <param name="rowNum">The number of the edited row. </param>
	protected internal override bool Commit(CurrencyManager dataSource, int rowNum)
	{
		textbox.Bounds = Rectangle.Empty;
		if (textbox.IsInEditOrNavigateMode)
		{
			return true;
		}
		try
		{
			string formattedValue = GetFormattedValue(dataSource, rowNum);
			if (formattedValue != textbox.Text)
			{
				if (textbox.Text == NullText)
				{
					SetColumnValueAtRow(dataSource, rowNum, DBNull.Value);
				}
				else
				{
					object value = textbox.Text;
					TypeConverter converter = TypeDescriptor.GetConverter(PropertyDescriptor.PropertyType);
					if (converter != null && converter.CanConvertFrom(typeof(string)))
					{
						value = converter.ConvertFrom(null, CultureInfo.CurrentCulture, textbox.Text);
						if (converter.CanConvertTo(typeof(string)))
						{
							textbox.Text = (string)converter.ConvertTo(null, CultureInfo.CurrentCulture, value, typeof(string));
						}
					}
					SetColumnValueAtRow(dataSource, rowNum, value);
				}
			}
		}
		catch
		{
			return false;
		}
		EndEdit();
		return true;
	}

	/// <summary>Informs the column that the focus is being conceded.</summary>
	protected internal override void ConcedeFocus()
	{
		HideEditBox();
	}

	/// <summary>Prepares a cell for editing.</summary>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> control the column belongs to. </param>
	/// <param name="rowNum">The row number in this column being edited. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> in which the control is to be sited. </param>
	/// <param name="readOnly">A value indicating whether the column is a read-only. true if the value is read-only; otherwise, false. </param>
	/// <param name="displayText">The text to display in the control. </param>
	/// <param name="cellIsVisible">A value indicating whether the cell is visible. true if the cell is visible; otherwise, false. </param>
	protected internal override void Edit(CurrencyManager source, int rowNum, Rectangle bounds, bool readOnly, string displayText, bool cellIsVisible)
	{
		grid.SuspendLayout();
		textbox.TextChanged -= textbox_TextChanged;
		textbox.TextAlign = alignment;
		bool flag = false;
		flag = base.TableStyleReadOnly || ReadOnly || readOnly;
		if (!flag && displayText != null)
		{
			textbox.Text = displayText;
			textbox.IsInEditOrNavigateMode = false;
		}
		else
		{
			textbox.Text = GetFormattedValue(source, rowNum);
		}
		textbox.TextChanged += textbox_TextChanged;
		textbox.ReadOnly = flag;
		textbox.Bounds = new Rectangle(new Point(bounds.X + offset_x, bounds.Y + offset_y), new Size(bounds.Width - offset_x - 1, bounds.Height - offset_y - 1));
		textbox.Visible = cellIsVisible;
		textbox.SelectAll();
		textbox.Focus();
		grid.ResumeLayout(performLayout: false);
	}

	private void textbox_TextChanged(object o, EventArgs e)
	{
		textbox.IsInEditOrNavigateMode = false;
		grid.EditRowChanged(this);
	}

	/// <summary>Ends an edit operation on the <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
	protected void EndEdit()
	{
		textbox.TextChanged -= textbox_TextChanged;
		HideEditBox();
	}

	/// <summary>Enters a <see cref="F:System.DBNull.Value" /> in the column.</summary>
	protected internal override void EnterNullValue()
	{
		textbox.Text = NullText;
	}

	/// <summary>Gets the height of a cell in a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</summary>
	/// <returns>The height of a cell.</returns>
	protected internal override int GetMinimumHeight()
	{
		return base.FontHeight + 3;
	}

	/// <summary>Gets the height to be used in for automatically resizing columns.</summary>
	/// <returns>The height the cells automatically resize to.</returns>
	/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw shapes on the screen. </param>
	/// <param name="value">The value to draw. </param>
	protected internal override int GetPreferredHeight(Graphics g, object value)
	{
		string formattedValue = GetFormattedValue(value);
		Regex regex = new Regex("/\r\n/");
		int count = regex.Matches(formattedValue).Count;
		return DataGridTableStyle.DataGrid.Font.Height * (count + 1) + 1;
	}

	/// <summary>Returns the optimum width and height of the cell in a specified row relative to the specified value.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that contains the dimensions of the cell.</returns>
	/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw shapes on the screen. </param>
	/// <param name="value">The value to draw. </param>
	protected internal override Size GetPreferredSize(Graphics g, object value)
	{
		string formattedValue = GetFormattedValue(value);
		Size result = Size.Ceiling(g.MeasureString(formattedValue, DataGridTableStyle.DataGrid.Font));
		result.Width += 4;
		return result;
	}

	/// <summary>Hides the <see cref="T:System.Windows.Forms.DataGridTextBox" /> control and moves the focus to the <see cref="T:System.Windows.Forms.DataGrid" /> control.</summary>
	protected void HideEditBox()
	{
		if (textbox.Visible)
		{
			grid.SuspendLayout();
			textbox.Bounds = Rectangle.Empty;
			textbox.Visible = false;
			textbox.IsInEditOrNavigateMode = true;
			grid.ResumeLayout(performLayout: false);
		}
	}

	/// <summary>Paints the a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, and row number.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object to draw to. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> the that contains the column. </param>
	/// <param name="rowNum">The number of the row in the underlying data table. </param>
	protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum)
	{
		Paint(g, bounds, source, rowNum, alignToRight: false);
	}

	/// <summary>Paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, and alignment.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object to draw to. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> the that contains the column. </param>
	/// <param name="rowNum">The number of the row in the underlying data table. </param>
	/// <param name="alignToRight">A value indicating whether to align the column's content to the right. true if the content should be aligned to the right; otherwise, false. </param>
	protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, bool alignToRight)
	{
		Paint(g, bounds, source, rowNum, ThemeEngine.Current.ResPool.GetSolidBrush(DataGridTableStyle.BackColor), ThemeEngine.Current.ResPool.GetSolidBrush(DataGridTableStyle.ForeColor), alignToRight);
	}

	/// <summary>Paints a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> with the specified <see cref="T:System.Drawing.Graphics" />, <see cref="T:System.Drawing.Rectangle" />, <see cref="T:System.Windows.Forms.CurrencyManager" />, row number, <see cref="T:System.Drawing.Brush" />, and foreground color.</summary>
	/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object to draw to. </param>
	/// <param name="bounds">The bounding <see cref="T:System.Drawing.Rectangle" /> to paint into. </param>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> of the <see cref="T:System.Windows.Forms.DataGrid" /> the that contains the column. </param>
	/// <param name="rowNum">The number of the row in the underlying data table. </param>
	/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> that paints the background. </param>
	/// <param name="foreBrush">A <see cref="T:System.Drawing.Brush" /> that paints the foreground color. </param>
	/// <param name="alignToRight">A value indicating whether to align the column's content to the right. true if the content should be aligned to the right; otherwise, false. </param>
	protected internal override void Paint(Graphics g, Rectangle bounds, CurrencyManager source, int rowNum, Brush backBrush, Brush foreBrush, bool alignToRight)
	{
		PaintText(g, bounds, GetFormattedValue(source, rowNum), backBrush, foreBrush, alignToRight);
	}

	/// <summary>Draws the text and rectangle at the given location with the specified alignment.</summary>
	/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw the string. </param>
	/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> which contains the boundary data of the rectangle. </param>
	/// <param name="text">The string to be drawn to the screen. </param>
	/// <param name="alignToRight">A value indicating whether the text is right-aligned. </param>
	protected void PaintText(Graphics g, Rectangle bounds, string text, bool alignToRight)
	{
		PaintText(g, bounds, text, ThemeEngine.Current.ResPool.GetSolidBrush(DataGridTableStyle.BackColor), ThemeEngine.Current.ResPool.GetSolidBrush(DataGridTableStyle.ForeColor), alignToRight);
	}

	/// <summary>Draws the text and rectangle at the specified location with the specified colors and alignment.</summary>
	/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> object used to draw the string. </param>
	/// <param name="textBounds">A <see cref="T:System.Drawing.Rectangle" /> which contains the boundary data of the rectangle. </param>
	/// <param name="text">The string to be drawn to the screen. </param>
	/// <param name="backBrush">A <see cref="T:System.Drawing.Brush" /> that determines the rectangle's background color </param>
	/// <param name="foreBrush">A <see cref="T:System.Drawing.Brush" /> that determines the rectangles foreground color. </param>
	/// <param name="alignToRight">A value indicating whether the text is right-aligned. </param>
	protected void PaintText(Graphics g, Rectangle textBounds, string text, Brush backBrush, Brush foreBrush, bool alignToRight)
	{
		if (alignToRight)
		{
			string_format.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
		}
		else
		{
			string_format.FormatFlags &= ~StringFormatFlags.DirectionRightToLeft;
		}
		switch (alignment)
		{
		case HorizontalAlignment.Center:
			string_format.Alignment = StringAlignment.Center;
			break;
		case HorizontalAlignment.Right:
			string_format.Alignment = StringAlignment.Far;
			break;
		default:
			string_format.Alignment = StringAlignment.Near;
			break;
		}
		g.FillRectangle(backBrush, textBounds);
		PaintGridLine(g, textBounds);
		textBounds.X += offset_x;
		textBounds.Width -= offset_x;
		textBounds.Y += offset_y;
		textBounds.Height -= offset_y;
		string_format.FormatFlags |= StringFormatFlags.NoWrap;
		g.DrawString(text, DataGridTableStyle.DataGrid.Font, foreBrush, textBounds, string_format);
	}

	/// <summary>Removes the reference that the <see cref="T:System.Windows.Forms.DataGrid" /> holds to the control used to edit data.</summary>
	protected internal override void ReleaseHostedControl()
	{
		if (textbox != null)
		{
			grid.SuspendLayout();
			grid.Controls.Remove(textbox);
			grid.Invalidate(new Rectangle(textbox.Location, textbox.Size));
			textbox.Dispose();
			textbox = null;
			grid.ResumeLayout(performLayout: false);
		}
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.TextBox" /> control to the <see cref="T:System.Windows.Forms.DataGrid" /> control's <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.DataGrid" /> control the <see cref="T:System.Windows.Forms.TextBox" /> control is added to. </param>
	protected override void SetDataGridInColumn(DataGrid value)
	{
		base.SetDataGridInColumn(value);
		if (value != null)
		{
			textbox.SetDataGrid(grid);
			grid.SuspendLayout();
			grid.Controls.Add(textbox);
			grid.ResumeLayout(performLayout: false);
		}
	}

	/// <summary>Updates the user interface.</summary>
	/// <param name="source">The <see cref="T:System.Windows.Forms.CurrencyManager" /> that supplies the data. </param>
	/// <param name="rowNum">The index of the row to update. </param>
	/// <param name="displayText">The text that will be displayed in the cell. </param>
	protected internal override void UpdateUI(CurrencyManager source, int rowNum, string displayText)
	{
		if (textbox.Visible && textbox.IsInEditOrNavigateMode)
		{
			textbox.Text = GetFormattedValue(source, rowNum);
		}
		else
		{
			textbox.Text = displayText;
		}
	}

	private string GetFormattedValue(CurrencyManager source, int rowNum)
	{
		object columnValueAtRow = GetColumnValueAtRow(source, rowNum);
		return GetFormattedValue(columnValueAtRow);
	}

	private string GetFormattedValue(object obj)
	{
		if (DBNull.Value.Equals(obj) || obj == null)
		{
			return NullText;
		}
		if (format != null && format != string.Empty && obj is IFormattable)
		{
			return ((IFormattable)obj).ToString(format, format_provider);
		}
		TypeConverter converter = TypeDescriptor.GetConverter(PropertyDescriptor.PropertyType);
		if (converter != null && converter.CanConvertTo(typeof(string)))
		{
			return (string)converter.ConvertTo(null, CultureInfo.CurrentCulture, obj, typeof(string));
		}
		return obj.ToString();
	}
}
