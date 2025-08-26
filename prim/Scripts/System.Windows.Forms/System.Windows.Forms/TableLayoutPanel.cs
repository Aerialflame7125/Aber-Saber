using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Represents a panel that dynamically lays out its contents in a grid composed of rows and columns.</summary>
/// <filterpriority>1</filterpriority>
[ProvideProperty("RowSpan", typeof(Control))]
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[Designer("System.Windows.Forms.Design.TableLayoutPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultProperty("ColumnCount")]
[ComVisible(true)]
[ProvideProperty("Row", typeof(Control))]
[ProvideProperty("ColumnSpan", typeof(Control))]
[ProvideProperty("CellPosition", typeof(Control))]
[DesignerSerializer("System.Windows.Forms.Design.TableLayoutPanelCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ProvideProperty("Column", typeof(Control))]
[Docking(DockingBehavior.Never)]
public class TableLayoutPanel : Panel, IExtenderProvider
{
	private TableLayoutSettings settings;

	private static TableLayout layout_engine = new TableLayout();

	private TableLayoutPanelCellBorderStyle cell_border_style;

	internal Control[,] actual_positions;

	internal int[] column_widths;

	internal int[] row_heights;

	private static object CellPaintEvent;

	/// <summary>Gets or sets the border style for the panel.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values describing the style of the border of the panel. The default is <see cref="F:System.Windows.Forms.BorderStyle.None" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Localizable(true)]
	public new BorderStyle BorderStyle
	{
		get
		{
			return base.BorderStyle;
		}
		set
		{
			base.BorderStyle = value;
		}
	}

	/// <summary>Gets or sets the style of the cell borders.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.TableLayoutPanelCellBorderStyle" /> values describing the style of all the cell borders in the table. The default is <see cref="F:System.Windows.Forms.TableLayoutPanelCellBorderStyle.None" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(TableLayoutPanelCellBorderStyle.None)]
	[Localizable(true)]
	public TableLayoutPanelCellBorderStyle CellBorderStyle
	{
		get
		{
			return cell_border_style;
		}
		set
		{
			if (cell_border_style != value)
			{
				cell_border_style = value;
				PerformLayout(this, "CellBorderStyle");
				Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the number of columns in the table.</summary>
	/// <returns>The number of columns in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control. The default is 0.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(0)]
	[Localizable(true)]
	public int ColumnCount
	{
		get
		{
			return settings.ColumnCount;
		}
		set
		{
			settings.ColumnCount = value;
		}
	}

	/// <summary>Gets a collection of column styles for the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" /> containing a <see cref="T:System.Windows.Forms.ColumnStyle" /> for each column in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[MergableProperty(false)]
	[DisplayName("Columns")]
	[Browsable(false)]
	public TableLayoutColumnStyleCollection ColumnStyles => settings.ColumnStyles;

	/// <summary>Gets the collection of controls contained within the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutControlCollection" /> containing the controls associated with the current <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Browsable(false)]
	public new TableLayoutControlCollection Controls => (TableLayoutControlCollection)base.Controls;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control should expand to accommodate new cells when all existing cells are occupied.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelGrowStyle" /> indicating the growth scheme. The default is <see cref="F:System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows" />.</returns>
	/// <exception cref="T:System.ArgumentException">The property value is invalid for the <see cref="T:System.Windows.Forms.TableLayoutPanelGrowStyle" /> enumeration.</exception>
	[DefaultValue(TableLayoutPanelGrowStyle.AddRows)]
	public TableLayoutPanelGrowStyle GrowStyle
	{
		get
		{
			return settings.GrowStyle;
		}
		set
		{
			settings.GrowStyle = value;
		}
	}

	/// <summary>Gets a cached instance of the panel's layout engine.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the panel's contents.</returns>
	/// <filterpriority>1</filterpriority>
	public override LayoutEngine LayoutEngine => layout_engine;

	/// <summary>Gets or sets a value representing the table layout settings.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutSettings" /> containing the table layout settings.</returns>
	/// <exception cref="T:System.NotSupportedException">The property value is null, or an attempt was made to set <see cref="T:System.Windows.Forms.TableLayoutSettings" />  directly, which is not supported; instead, set individual properties.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public TableLayoutSettings LayoutSettings
	{
		get
		{
			return settings;
		}
		set
		{
			if (value.isSerialized)
			{
				value.ColumnCount = value.ColumnStyles.Count;
				value.RowCount = value.RowStyles.Count;
				value.panel = this;
				settings = value;
				value.isSerialized = false;
				return;
			}
			throw new NotSupportedException("LayoutSettings value cannot be set directly.");
		}
	}

	/// <summary>Gets or sets the number of rows in the table.</summary>
	/// <returns>The number of rows in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control. The default is 0.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(0)]
	[Localizable(true)]
	public int RowCount
	{
		get
		{
			return settings.RowCount;
		}
		set
		{
			settings.RowCount = value;
		}
	}

	/// <summary>Gets a collection of row styles for the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" /> containing a <see cref="T:System.Windows.Forms.RowStyle" /> for each row in the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> control.</returns>
	/// <filterpriority>1</filterpriority>
	[DisplayName("Rows")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[MergableProperty(false)]
	[Browsable(false)]
	public TableLayoutRowStyleCollection RowStyles => settings.RowStyles;

	/// <summary>Occurs when the cell is redrawn.</summary>
	public event TableLayoutCellPaintEventHandler CellPaint
	{
		add
		{
			base.Events.AddHandler(CellPaintEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(CellPaintEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutPanel" /> class.</summary>
	public TableLayoutPanel()
	{
		settings = new TableLayoutSettings(this);
		cell_border_style = TableLayoutPanelCellBorderStyle.None;
		column_widths = new int[0];
		row_heights = new int[0];
	}

	static TableLayoutPanel()
	{
		CellPaint = new object();
	}

	/// <summary>For a description of this member, see <see cref="M:System.ComponentModel.IExtenderProvider.CanExtend(System.Object)" />.</summary>
	/// <returns>true if this object can provide extender properties to the specified object; otherwise, false.</returns>
	/// <param name="obj">The <see cref="T:System.Object" /> to receive the extender properties.</param>
	bool IExtenderProvider.CanExtend(object obj)
	{
		if (obj is Control && (obj as Control).Parent == this)
		{
			return true;
		}
		return false;
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the cell position.</returns>
	/// <param name="control">A control contained within a cell.</param>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DefaultValue(-1)]
	[DisplayName("Cell")]
	public TableLayoutPanelCellPosition GetCellPosition(Control control)
	{
		return settings.GetCellPosition(control);
	}

	/// <summary>Returns the column position of the specified child control.</summary>
	/// <returns>The column position of the specified child control, or -1 if the position of <paramref name="control" /> is determined by <see cref="P:System.Windows.Forms.TableLayoutPanel.LayoutEngine" />.</returns>
	/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="control" /> is not a type that can be arranged by this <see cref="T:System.Windows.Forms.Layout.LayoutEngine" />.</exception>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DisplayName("Column")]
	[DefaultValue(-1)]
	public int GetColumn(Control control)
	{
		return settings.GetColumn(control);
	}

	/// <summary>Returns the number of columns spanned by the specified child control.</summary>
	/// <returns>The number of columns spanned by the child control. The default is 1.</returns>
	/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(1)]
	[DisplayName("ColumnSpan")]
	public int GetColumnSpan(Control control)
	{
		return settings.GetColumnSpan(control);
	}

	/// <summary>Returns an array representing the widths, in pixels, of the columns in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
	/// <returns>An array of type <see cref="T:System.Int32" /> that contains the widths, in pixels, of the columns in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public int[] GetColumnWidths()
	{
		return column_widths;
	}

	/// <summary>Returns the child control occupying the specified position.</summary>
	/// <returns>The child control occupying the specified cell; otherwise, null if no control exists at the specified column and row, or if the control has its <see cref="P:System.Windows.Forms.Control.Visible" /> property set to false.</returns>
	/// <param name="column">The column position of the control to retrieve.</param>
	/// <param name="row">The row position of the control to retrieve.</param>
	/// <exception cref="T:System.ArgumentException">Either <paramref name="column" /> or <paramref name="row" /> (or both) is less than 0.</exception>
	/// <filterpriority>1</filterpriority>
	public Control GetControlFromPosition(int column, int row)
	{
		if (column < 0 || row < 0)
		{
			throw new ArgumentException();
		}
		TableLayoutPanelCellPosition tableLayoutPanelCellPosition = new TableLayoutPanelCellPosition(column, row);
		foreach (Control control in Controls)
		{
			if (settings.GetCellPosition(control) == tableLayoutPanelCellPosition)
			{
				return control;
			}
		}
		return null;
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell that contains the control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the cell position.</returns>
	/// <param name="control">A control contained within a cell.</param>
	public TableLayoutPanelCellPosition GetPositionFromControl(Control control)
	{
		for (int i = 0; i < actual_positions.GetLength(0); i++)
		{
			for (int j = 0; j < actual_positions.GetLength(1); j++)
			{
				if (actual_positions[i, j] == control)
				{
					return new TableLayoutPanelCellPosition(i, j);
				}
			}
		}
		return new TableLayoutPanelCellPosition(-1, -1);
	}

	/// <summary>Returns the row position of the specified child control.</summary>
	/// <returns>The row position of <paramref name="control" />, or -1 if the position of <paramref name="control" /> is determined by <see cref="P:System.Windows.Forms.TableLayoutPanel.LayoutEngine" />.</returns>
	/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="control" /> is not a type that can be arranged by this <see cref="T:System.Windows.Forms.Layout.LayoutEngine" />.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue("-1")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[DisplayName("Row")]
	public int GetRow(Control control)
	{
		return settings.GetRow(control);
	}

	/// <summary>Returns an array representing the heights, in pixels, of the rows in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</summary>
	/// <returns>An array of type <see cref="T:System.Int32" /> that contains the heights, in pixels, of the rows in the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Browsable(false)]
	public int[] GetRowHeights()
	{
		return row_heights;
	}

	/// <summary>Returns the number of rows spanned by the specified child control.</summary>
	/// <returns>The number of rows spanned by the child control. The default is 1.</returns>
	/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
	/// <filterpriority>1</filterpriority>
	[DisplayName("RowSpan")]
	[DefaultValue(1)]
	public int GetRowSpan(Control control)
	{
		return settings.GetRowSpan(control);
	}

	/// <summary>Sets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
	/// <param name="control">A control contained within a cell.</param>
	/// <param name="position">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</param>
	public void SetCellPosition(Control control, TableLayoutPanelCellPosition position)
	{
		settings.SetCellPosition(control, position);
	}

	/// <summary>Sets the column position of the specified child control.</summary>
	/// <param name="control">The control to move to another column.</param>
	/// <param name="column">The column to which <paramref name="control" /> will be moved.</param>
	/// <filterpriority>1</filterpriority>
	public void SetColumn(Control control, int column)
	{
		settings.SetColumn(control, column);
	}

	/// <summary>Sets the number of columns spanned by the child control.</summary>
	/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
	/// <param name="value">The number of columns to span.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is less than 1.</exception>
	/// <filterpriority>1</filterpriority>
	public void SetColumnSpan(Control control, int value)
	{
		settings.SetColumnSpan(control, value);
	}

	/// <summary>Sets the row position of the specified child control.</summary>
	/// <param name="control">The control to move to another row.</param>
	/// <param name="row">The row to which <paramref name="control" /> will be moved.</param>
	/// <filterpriority>1</filterpriority>
	public void SetRow(Control control, int row)
	{
		settings.SetRow(control, row);
	}

	/// <summary>Sets the number of rows spanned by the child control.</summary>
	/// <param name="control">A child control of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />.</param>
	/// <param name="value">The number of rows to span.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is less than 1.</exception>
	/// <filterpriority>1</filterpriority>
	public void SetRowSpan(Control control, int value)
	{
		settings.SetRowSpan(control, value);
	}

	/// <summary>Creates a new instance of the control collection for the control.</summary>
	/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override ControlCollection CreateControlsInstance()
	{
		return new TableLayoutControlCollection(this);
	}

	/// <summary>Receives a call when the cell should be refreshed.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.TableLayoutCellPaintEventArgs" /> that provides data for the event.</param>
	protected virtual void OnCellPaint(TableLayoutCellPaintEventArgs e)
	{
		((TableLayoutCellPaintEventHandler)base.Events[CellPaint])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected override void OnLayout(LayoutEventArgs levent)
	{
		base.OnLayout(levent);
		Invalidate();
	}

	/// <summary>Paints the background of the panel.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" />  that contains information about the panel to paint.</param>
	protected override void OnPaintBackground(PaintEventArgs e)
	{
		base.OnPaintBackground(e);
		DrawCellBorders(e);
		int cellBorderWidth = GetCellBorderWidth(CellBorderStyle);
		int num = cellBorderWidth;
		int num2 = cellBorderWidth;
		for (int i = 0; i < column_widths.Length; i++)
		{
			for (int j = 0; j < row_heights.Length; j++)
			{
				OnCellPaint(new TableLayoutCellPaintEventArgs(e.Graphics, e.ClipRectangle, new Rectangle(num, num2, column_widths[i] + cellBorderWidth, row_heights[j] + cellBorderWidth), i, j));
				num2 += row_heights[j] + cellBorderWidth;
			}
			num += column_widths[i] + cellBorderWidth;
			num2 = cellBorderWidth;
		}
	}

	/// <summary>Scales a control's location, size, padding and margin.</summary>
	/// <param name="factor">The height and width of the control's bounds.</param>
	/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" />  that specifies the bounds of the control to use when defining its size and position.</param>
	protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
	{
		base.ScaleControl(factor, specified);
	}

	/// <summary>Performs the work of scaling the entire panel and any child controls.</summary>
	/// <param name="dx">The ratio by which to scale the control horizontally.</param>
	/// <param name="dy">The ratio by which to scale the control vertically</param>
	[EditorBrowsable(EditorBrowsableState.Never)]
	protected override void ScaleCore(float dx, float dy)
	{
		base.ScaleCore(dx, dy);
	}

	internal static int GetCellBorderWidth(TableLayoutPanelCellBorderStyle style)
	{
		switch (style)
		{
		case TableLayoutPanelCellBorderStyle.Single:
			return 1;
		case TableLayoutPanelCellBorderStyle.Inset:
		case TableLayoutPanelCellBorderStyle.Outset:
			return 2;
		case TableLayoutPanelCellBorderStyle.InsetDouble:
		case TableLayoutPanelCellBorderStyle.OutsetDouble:
		case TableLayoutPanelCellBorderStyle.OutsetPartial:
			return 3;
		default:
			return 0;
		}
	}

	private void DrawCellBorders(PaintEventArgs e)
	{
		Rectangle rect = new Rectangle(Point.Empty, base.Size);
		switch (CellBorderStyle)
		{
		case TableLayoutPanelCellBorderStyle.Single:
			DrawSingleBorder(e.Graphics, rect);
			break;
		case TableLayoutPanelCellBorderStyle.Inset:
			DrawInsetBorder(e.Graphics, rect);
			break;
		case TableLayoutPanelCellBorderStyle.InsetDouble:
			DrawInsetDoubleBorder(e.Graphics, rect);
			break;
		case TableLayoutPanelCellBorderStyle.Outset:
			DrawOutsetBorder(e.Graphics, rect);
			break;
		case TableLayoutPanelCellBorderStyle.OutsetDouble:
		case TableLayoutPanelCellBorderStyle.OutsetPartial:
			DrawOutsetDoubleBorder(e.Graphics, rect);
			break;
		}
	}

	private void DrawSingleBorder(Graphics g, Rectangle rect)
	{
		ControlPaint.DrawBorder(g, rect, SystemColors.ControlDark, ButtonBorderStyle.Solid);
		int num = DisplayRectangle.X;
		int num2 = DisplayRectangle.Y;
		for (int i = 0; i < column_widths.Length - 1; i++)
		{
			num += column_widths[i] + 1;
			g.DrawLine(SystemPens.ControlDark, new Point(num, 1), new Point(num, base.Bottom - 2));
		}
		for (int j = 0; j < row_heights.Length - 1; j++)
		{
			num2 += row_heights[j] + 1;
			g.DrawLine(SystemPens.ControlDark, new Point(1, num2), new Point(base.Right - 2, num2));
		}
	}

	private void DrawInsetBorder(Graphics g, Rectangle rect)
	{
		ControlPaint.DrawBorder3D(g, rect, Border3DStyle.Etched);
		int num = DisplayRectangle.X;
		int num2 = DisplayRectangle.Y;
		for (int i = 0; i < column_widths.Length - 1; i++)
		{
			num += column_widths[i] + 2;
			g.DrawLine(SystemPens.ControlDark, new Point(num, 1), new Point(num, base.Bottom - 3));
			g.DrawLine(Pens.White, new Point(num + 1, 1), new Point(num + 1, base.Bottom - 3));
		}
		for (int j = 0; j < row_heights.Length - 1; j++)
		{
			num2 += row_heights[j] + 2;
			g.DrawLine(SystemPens.ControlDark, new Point(1, num2), new Point(base.Right - 3, num2));
			g.DrawLine(Pens.White, new Point(1, num2 + 1), new Point(base.Right - 3, num2 + 1));
		}
	}

	private void DrawOutsetBorder(Graphics g, Rectangle rect)
	{
		g.DrawRectangle(SystemPens.ControlDark, new Rectangle(rect.Left + 1, rect.Top + 1, rect.Width - 2, rect.Height - 2));
		g.DrawRectangle(Pens.White, new Rectangle(rect.Left, rect.Top, rect.Width - 2, rect.Height - 2));
		int num = DisplayRectangle.X;
		int num2 = DisplayRectangle.Y;
		for (int i = 0; i < column_widths.Length - 1; i++)
		{
			num += column_widths[i] + 2;
			g.DrawLine(Pens.White, new Point(num, 1), new Point(num, base.Bottom - 3));
			g.DrawLine(SystemPens.ControlDark, new Point(num + 1, 1), new Point(num + 1, base.Bottom - 3));
		}
		for (int j = 0; j < row_heights.Length - 1; j++)
		{
			num2 += row_heights[j] + 2;
			g.DrawLine(Pens.White, new Point(1, num2), new Point(base.Right - 3, num2));
			g.DrawLine(SystemPens.ControlDark, new Point(1, num2 + 1), new Point(base.Right - 3, num2 + 1));
		}
	}

	private void DrawOutsetDoubleBorder(Graphics g, Rectangle rect)
	{
		rect.Width--;
		rect.Height--;
		g.DrawRectangle(SystemPens.ControlDark, new Rectangle(rect.Left + 2, rect.Top + 2, rect.Width - 2, rect.Height - 2));
		g.DrawRectangle(Pens.White, new Rectangle(rect.Left, rect.Top, rect.Width - 2, rect.Height - 2));
		int num = DisplayRectangle.X;
		int num2 = DisplayRectangle.Y;
		for (int i = 0; i < column_widths.Length - 1; i++)
		{
			num += column_widths[i] + 3;
			g.DrawLine(Pens.White, new Point(num, 3), new Point(num, base.Bottom - 5));
			g.DrawLine(SystemPens.ControlDark, new Point(num + 2, 3), new Point(num + 2, base.Bottom - 5));
		}
		for (int j = 0; j < row_heights.Length - 1; j++)
		{
			num2 += row_heights[j] + 3;
			g.DrawLine(Pens.White, new Point(3, num2), new Point(base.Right - 4, num2));
			g.DrawLine(SystemPens.ControlDark, new Point(3, num2 + 2), new Point(base.Right - 4, num2 + 2));
		}
		num = DisplayRectangle.X;
		num2 = DisplayRectangle.Y;
		for (int k = 0; k < column_widths.Length - 1; k++)
		{
			num += column_widths[k] + 3;
			g.DrawLine(ThemeEngine.Current.ResPool.GetPen(BackColor), new Point(num + 1, 3), new Point(num + 1, base.Bottom - 5));
		}
		for (int l = 0; l < row_heights.Length - 1; l++)
		{
			num2 += row_heights[l] + 3;
			g.DrawLine(ThemeEngine.Current.ResPool.GetPen(BackColor), new Point(3, num2 + 1), new Point(base.Right - 4, num2 + 1));
		}
	}

	private void DrawInsetDoubleBorder(Graphics g, Rectangle rect)
	{
		rect.Width--;
		rect.Height--;
		g.DrawRectangle(Pens.White, new Rectangle(rect.Left + 2, rect.Top + 2, rect.Width - 2, rect.Height - 2));
		g.DrawRectangle(SystemPens.ControlDark, new Rectangle(rect.Left, rect.Top, rect.Width - 2, rect.Height - 2));
		int num = DisplayRectangle.X;
		int num2 = DisplayRectangle.Y;
		for (int i = 0; i < column_widths.Length - 1; i++)
		{
			num += column_widths[i] + 3;
			g.DrawLine(SystemPens.ControlDark, new Point(num, 3), new Point(num, base.Bottom - 5));
			g.DrawLine(Pens.White, new Point(num + 2, 3), new Point(num + 2, base.Bottom - 5));
		}
		for (int j = 0; j < row_heights.Length - 1; j++)
		{
			num2 += row_heights[j] + 3;
			g.DrawLine(SystemPens.ControlDark, new Point(3, num2), new Point(base.Right - 4, num2));
			g.DrawLine(Pens.White, new Point(3, num2 + 2), new Point(base.Right - 4, num2 + 2));
		}
		num = DisplayRectangle.X;
		num2 = DisplayRectangle.Y;
		for (int k = 0; k < column_widths.Length - 1; k++)
		{
			num += column_widths[k] + 3;
			g.DrawLine(ThemeEngine.Current.ResPool.GetPen(BackColor), new Point(num + 1, 3), new Point(num + 1, base.Bottom - 5));
		}
		for (int l = 0; l < row_heights.Length - 1; l++)
		{
			num2 += row_heights[l] + 3;
			g.DrawLine(ThemeEngine.Current.ResPool.GetPen(BackColor), new Point(3, num2 + 1), new Point(base.Right - 4, num2 + 1));
		}
	}

	internal override Size GetPreferredSizeCore(Size proposedSize)
	{
		actual_positions = (LayoutEngine as TableLayout).CalculateControlPositions(this, Math.Max(ColumnCount, 1), Math.Max(RowCount, 1));
		int length = actual_positions.GetLength(0);
		int length2 = actual_positions.GetLength(1);
		int[] array = new int[length];
		float num = 0f;
		for (int i = 0; i < length; i++)
		{
			if (i < ColumnStyles.Count && ColumnStyles[i].SizeType == SizeType.Percent)
			{
				num += ColumnStyles[i].Width;
			}
			int num2 = 0;
			for (int j = 0; j < length2; j++)
			{
				Control control = actual_positions[i, j];
				if (control != null)
				{
					num2 = (control.AutoSize ? Math.Max(num2, control.PreferredSize.Width + control.Margin.Horizontal + base.Padding.Horizontal) : Math.Max(num2, control.ExplicitBounds.Width + control.Margin.Horizontal + base.Padding.Horizontal));
				}
			}
			array[i] = num2;
		}
		int num3 = 0;
		int num4 = 0;
		for (int k = 0; k < length; k++)
		{
			if (k < ColumnStyles.Count && ColumnStyles[k].SizeType == SizeType.Percent)
			{
				num4 = Math.Max(num4, (int)((float)array[k] / (ColumnStyles[k].Width / num)));
			}
			else
			{
				num3 += array[k];
			}
		}
		int[] array2 = new int[length2];
		float num5 = 0f;
		for (int l = 0; l < length2; l++)
		{
			if (l < RowStyles.Count && RowStyles[l].SizeType == SizeType.Percent)
			{
				num5 += RowStyles[l].Height;
			}
			int num6 = 0;
			for (int m = 0; m < length; m++)
			{
				Control control2 = actual_positions[m, l];
				if (control2 != null)
				{
					num6 = (control2.AutoSize ? Math.Max(num6, control2.PreferredSize.Height + control2.Margin.Vertical + base.Padding.Vertical) : Math.Max(num6, control2.ExplicitBounds.Height + control2.Margin.Vertical + base.Padding.Vertical));
				}
			}
			array2[l] = num6;
		}
		int num7 = 0;
		int num8 = 0;
		for (int n = 0; n < length2; n++)
		{
			if (n < RowStyles.Count && RowStyles[n].SizeType == SizeType.Percent)
			{
				num8 = Math.Max(num8, (int)((float)array2[n] / (RowStyles[n].Height / num5)));
			}
			else
			{
				num7 += array2[n];
			}
		}
		int cellBorderWidth = GetCellBorderWidth(CellBorderStyle);
		return new Size(num3 + num4 + cellBorderWidth * (length + 1), num7 + num8 + cellBorderWidth * (length2 + 1));
	}
}
