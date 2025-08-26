using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents a column header in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewColumnHeaderCell : DataGridViewHeaderCell
{
	/// <summary>Provides information about a <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> to accessibility client applications.</summary>
	protected class DataGridViewColumnHeaderCellAccessibleObject : DataGridViewCellAccessibleObject
	{
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the accessible object.</returns>
		public override Rectangle Bounds => base.Bounds;

		/// <summary>Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
		/// <returns>A string that describes the default action of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" /></returns>
		public override string DefaultAction => base.DefaultAction;

		/// <summary>Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
		/// <returns>The name of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</returns>
		public override string Name => base.Name;

		/// <returns>The parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject" />.</returns>
		public override AccessibleObject Parent => base.Parent;

		/// <summary>Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
		/// <returns>The <see cref="F:System.Windows.Forms.AccessibleRole.RowHeader" /> value.</returns>
		public override AccessibleRole Role => base.Role;

		/// <summary>Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates" /> values. The default is <see cref="F:System.Windows.Forms.AccessibleStates.Selectable" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner" /> property is null.</exception>
		public override AccessibleStates State => base.State;

		/// <summary>Gets the value of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
		/// <returns>The value of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</returns>
		public override string Value => base.Value;

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> that owns the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</param>
		public DataGridViewColumnHeaderCellAccessibleObject(DataGridViewColumnHeaderCell owner)
			: base(owner)
		{
		}

		/// <summary>Performs the default action associated with the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell.DataGridViewColumnHeaderCellAccessibleObject" />.</summary>
		public override void DoDefaultAction()
		{
			base.DoDefaultAction();
		}

		/// <summary>Navigates to another accessible object.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AccessibleObject" /> that represents an object in the specified direction.</returns>
		/// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation" /> values.</param>
		public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
		{
			return base.Navigate(navigationDirection);
		}

		/// <summary>Modifies the column selection depending on the selection mode.</summary>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.Windows.Forms.AccessibleSelection" /> values. </param>
		public override void Select(AccessibleSelection flags)
		{
			base.Select(flags);
		}
	}

	private SortOrder sortGlyphDirection;

	private object header_text;

	/// <summary>Gets or sets a value indicating which sort glyph is displayed.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.SortOrder" /> value representing the current glyph. The default is <see cref="F:System.Windows.Forms.SortOrder.None" />. </returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.SortOrder" /> value.</exception>
	/// <exception cref="T:System.InvalidOperationException">When setting this property, the value of either the <see cref="P:System.Windows.Forms.DataGridViewCell.OwningColumn" /> property or the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> property of the cell is null.-or-When changing the value of this property, the specified value is not <see cref="F:System.Windows.Forms.SortOrder.None" /> and the value of the <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode" /> property of the owning column is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.NotSortable" />.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public SortOrder SortGlyphDirection
	{
		get
		{
			return sortGlyphDirection;
		}
		set
		{
			sortGlyphDirection = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> class.</summary>
	public DataGridViewColumnHeaderCell()
	{
	}

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
		return MemberwiseClone();
	}

	/// <summary>Retrieves the inherited shortcut menu for the specified row.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> of the column headers if one exists; otherwise, the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> inherited from <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
	/// <param name="rowIndex">The index of the row to get the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> of. The index must be -1 to indicate the row of column headers.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is not -1.</exception>
	public override ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
	{
		if (rowIndex != -1)
		{
			throw new ArgumentOutOfRangeException("RowIndex is not -1");
		}
		if (base.ContextMenuStrip != null)
		{
			return base.ContextMenuStrip;
		}
		return base.GetInheritedContextMenuStrip(rowIndex);
	}

	/// <summary>Gets the style applied to the cell. </summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that includes the style settings of the cell inherited from the cell's parent row, column, and <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
	/// <param name="inheritedCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be populated with the inherited cell style. </param>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="includeColors">true to include inherited colors in the returned cell style; otherwise, false. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is not -1.</exception>
	public override DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex, bool includeColors)
	{
		DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle(base.DataGridView.DefaultCellStyle);
		dataGridViewCellStyle.ApplyStyle(base.DataGridView.ColumnHeadersDefaultCellStyle);
		if (base.HasStyle)
		{
			dataGridViewCellStyle.ApplyStyle(base.Style);
		}
		return dataGridViewCellStyle;
	}

	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return GetType().Name;
	}

	/// <summary>Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" />. </summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" />. </returns>
	protected override AccessibleObject CreateAccessibilityInstance()
	{
		return new DataGridViewColumnHeaderCellAccessibleObject(this);
	}

	/// <summary>Retrieves the formatted value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</summary>
	/// <returns>A <see cref="T:System.Object" /> that represents the value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard" />.</returns>
	/// <param name="rowIndex">The zero-based index of the row containing the cell.</param>
	/// <param name="firstCell">true to indicate that the cell is in the first column of the region defined by the selected cells; otherwise, false.</param>
	/// <param name="lastCell">true to indicate that the cell is the last column of the region defined by the selected cells; otherwise, false.</param>
	/// <param name="inFirstRow">true to indicate that the cell is in the first row of the region defined by the selected cells; otherwise, false.</param>
	/// <param name="inLastRow">true to indicate that the cell is in the last row of the region defined by the selected cells; otherwise, false.</param>
	/// <param name="format">The current format string of the cell.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is not -1.</exception>
	protected override object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
	{
		if (rowIndex != -1)
		{
			throw new ArgumentOutOfRangeException("rowIndex");
		}
		string text = GetValue(rowIndex) as string;
		string text2 = string.Empty;
		string text3 = string.Empty;
		string text4 = string.Empty;
		string text5 = string.Empty;
		string text6 = string.Empty;
		string text7 = string.Empty;
		if (format == DataFormats.UnicodeText || format == DataFormats.Text)
		{
			if (lastCell && !inLastRow)
			{
				text6 = Environment.NewLine;
			}
			else if (!lastCell)
			{
				text6 = "\t";
			}
		}
		else if (format == DataFormats.CommaSeparatedValue)
		{
			if (lastCell && !inLastRow)
			{
				text6 = Environment.NewLine;
			}
			else if (!lastCell)
			{
				text6 = ",";
			}
		}
		else
		{
			if (!(format == DataFormats.Html))
			{
				return text;
			}
			if (firstCell)
			{
				text2 = "<TABLE>";
				text4 = "<THEAD>";
			}
			text3 = "<TH>";
			text6 = "</TH>";
			if (lastCell)
			{
				text7 = "</THEAD>";
				if (inLastRow)
				{
					text5 = "</TABLE>";
				}
			}
			if (text == null)
			{
				text = "&nbsp;";
			}
		}
		if (text == null)
		{
			text = string.Empty;
		}
		return text2 + text4 + text3 + text + text6 + text7 + text5;
	}

	/// <summary>Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics" /> object and cell style.</summary>
	/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that bounds the cell's contents.</returns>
	/// <param name="graphics">The graphics context for the cell.</param>
	/// <param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied to the cell.</param>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is not -1.</exception>
	protected override Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
	{
		if (base.DataGridView == null)
		{
			return Rectangle.Empty;
		}
		object value = GetValue(-1);
		if (value == null || value.ToString() == string.Empty)
		{
			return Rectangle.Empty;
		}
		Size size = Size.Empty;
		if (value != null)
		{
			size = DataGridViewCell.MeasureTextSize(graphics, value.ToString(), cellStyle.Font, TextFormatFlags.Left);
		}
		return new Rectangle(3, (base.DataGridView.ColumnHeadersHeight - size.Height) / 2, size.Width, size.Height);
	}

	/// <summary>Calculates the preferred size, in pixels, of the cell.</summary>
	/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the preferred size, in pixels, of the cell.</returns>
	/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> used to draw the cell.</param>
	/// <param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the style of the cell.</param>
	/// <param name="rowIndex">The zero-based row index of the cell.</param>
	/// <param name="constraintSize">The cell's maximum allowable size.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is not -1.</exception>
	protected override Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
	{
		object obj = header_text;
		if (obj != null)
		{
			Size result = DataGridViewCell.MeasureTextSize(graphics, obj.ToString(), cellStyle.Font, TextFormatFlags.Left);
			result.Height = Math.Max(result.Height, 18);
			result.Width += 25;
			return result;
		}
		return new Size(19, 12);
	}

	/// <summary>Gets the value of the cell. </summary>
	/// <returns>The value contained in the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" />.</returns>
	/// <param name="rowIndex">The index of the cell's parent row.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is not -1.</exception>
	protected override object GetValue(int rowIndex)
	{
		if (header_text != null)
		{
			return header_text;
		}
		if (base.OwningColumn != null && !base.OwningColumn.HeaderTextSet)
		{
			return base.OwningColumn.Name;
		}
		return null;
	}

	protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates dataGridViewElementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
	{
		DataGridViewPaintParts dataGridViewPaintParts = DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground;
		dataGridViewPaintParts &= paintParts;
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, dataGridViewPaintParts);
		if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
		{
			Color color = ((!Selected) ? cellStyle.ForeColor : cellStyle.SelectionForeColor);
			TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.TextBoxControl | TextFormatFlags.EndEllipsis;
			Rectangle bounds = cellBounds;
			bounds.Height -= 2;
			bounds.Width -= 2;
			if (formattedValue != null)
			{
				TextRenderer.DrawText(graphics, formattedValue.ToString(), cellStyle.Font, bounds, color, flags);
			}
			Point point = new Point(cellBounds.Right - 14, cellBounds.Y + (cellBounds.Height - 4) / 2);
			if (sortGlyphDirection == SortOrder.Ascending)
			{
				using Pen pen = new Pen(color);
				graphics.DrawLine(pen, point.X + 4, point.Y + 1, point.X + 4, point.Y + 2);
				graphics.DrawLine(pen, point.X + 3, point.Y + 2, point.X + 5, point.Y + 2);
				graphics.DrawLine(pen, point.X + 2, point.Y + 3, point.X + 6, point.Y + 3);
				graphics.DrawLine(pen, point.X + 1, point.Y + 4, point.X + 7, point.Y + 4);
				graphics.DrawLine(pen, point.X, point.Y + 5, point.X + 8, point.Y + 5);
			}
			else if (sortGlyphDirection == SortOrder.Descending)
			{
				using Pen pen2 = new Pen(color);
				graphics.DrawLine(pen2, point.X + 4, point.Y + 5, point.X + 4, point.Y + 4);
				graphics.DrawLine(pen2, point.X + 3, point.Y + 4, point.X + 5, point.Y + 4);
				graphics.DrawLine(pen2, point.X + 2, point.Y + 3, point.X + 6, point.Y + 3);
				graphics.DrawLine(pen2, point.X + 1, point.Y + 2, point.X + 7, point.Y + 2);
				graphics.DrawLine(pen2, point.X, point.Y + 1, point.X + 8, point.Y + 1);
			}
		}
		DataGridViewPaintParts dataGridViewPaintParts2 = DataGridViewPaintParts.Border;
		if (this is DataGridViewTopLeftHeaderCell)
		{
			dataGridViewPaintParts2 |= DataGridViewPaintParts.ErrorIcon;
		}
		dataGridViewPaintParts2 &= paintParts;
		base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, dataGridViewPaintParts2);
	}

	protected override void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
	{
		if (ThemeEngine.Current.DataGridViewColumnHeaderCellDrawBorder(this, graphics, cellBounds))
		{
			return;
		}
		Pen borderPen = GetBorderPen();
		if (base.ColumnIndex == -1)
		{
			graphics.DrawLine(borderPen, cellBounds.Left, cellBounds.Top, cellBounds.Left, cellBounds.Bottom - 1);
			graphics.DrawLine(borderPen, cellBounds.Right - 1, cellBounds.Top, cellBounds.Right - 1, cellBounds.Bottom - 1);
			graphics.DrawLine(borderPen, cellBounds.Left, cellBounds.Bottom - 1, cellBounds.Right - 1, cellBounds.Bottom - 1);
			graphics.DrawLine(borderPen, cellBounds.Left, cellBounds.Top, cellBounds.Right - 1, cellBounds.Top);
			return;
		}
		graphics.DrawLine(borderPen, cellBounds.Left, cellBounds.Bottom - 1, cellBounds.Right - 1, cellBounds.Bottom - 1);
		graphics.DrawLine(borderPen, cellBounds.Left, cellBounds.Top, cellBounds.Right - 1, cellBounds.Top);
		if (base.ColumnIndex == base.DataGridView.Columns.Count - 1 || base.ColumnIndex == -1)
		{
			graphics.DrawLine(borderPen, cellBounds.Right - 1, cellBounds.Top, cellBounds.Right - 1, cellBounds.Bottom - 1);
		}
		else
		{
			graphics.DrawLine(borderPen, cellBounds.Right - 1, cellBounds.Top + 3, cellBounds.Right - 1, cellBounds.Bottom - 3);
		}
	}

	internal override void PaintPartBackground(Graphics graphics, Rectangle cellBounds, DataGridViewCellStyle style)
	{
		if (!ThemeEngine.Current.DataGridViewColumnHeaderCellDrawBackground(this, graphics, cellBounds))
		{
			base.PaintPartBackground(graphics, cellBounds, style);
		}
	}

	/// <summary>Sets the value of the cell. </summary>
	/// <returns>true if the value has been set; otherwise false.</returns>
	/// <param name="rowIndex">The index of the cell's parent row. </param>
	/// <param name="value">The cell value to set. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="rowIndex" /> is not -1.</exception>
	protected override bool SetValue(int rowIndex, object value)
	{
		header_text = value;
		return true;
	}
}
