using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;

namespace System.Windows.Forms;

/// <summary>Represents the table drawn by the <see cref="T:System.Windows.Forms.DataGrid" /> control at run time.</summary>
/// <filterpriority>2</filterpriority>
[ToolboxItem(false)]
[DesignTimeVisible(false)]
public class DataGridTableStyle : Component, IDataGridEditingService
{
	/// <summary>Gets the default table style.</summary>
	/// <filterpriority>1</filterpriority>
	public static readonly DataGridTableStyle DefaultTableStyle = new DataGridTableStyle(isDefaultTableStyle: true);

	private static readonly Color def_alternating_backcolor = ThemeEngine.Current.DataGridAlternatingBackColor;

	private static readonly Color def_backcolor = ThemeEngine.Current.DataGridBackColor;

	private static readonly Color def_forecolor = SystemColors.WindowText;

	private static readonly Color def_gridline_color = ThemeEngine.Current.DataGridGridLineColor;

	private static readonly Color def_header_backcolor = ThemeEngine.Current.DataGridHeaderBackColor;

	private static readonly Font def_header_font = ThemeEngine.Current.DefaultFont;

	private static readonly Color def_header_forecolor = ThemeEngine.Current.DataGridHeaderForeColor;

	private static readonly Color def_link_color = ThemeEngine.Current.DataGridLinkColor;

	private static readonly Color def_link_hovercolor = ThemeEngine.Current.DataGridLinkHoverColor;

	private static readonly Color def_selection_backcolor = ThemeEngine.Current.DataGridSelectionBackColor;

	private static readonly Color def_selection_forecolor = ThemeEngine.Current.DataGridSelectionForeColor;

	private static readonly int def_preferredrow_height = ThemeEngine.Current.DefaultFont.Height + 3;

	private bool allow_sorting;

	private DataGrid datagrid;

	private Color header_forecolor;

	private string mapping_name;

	private Color alternating_backcolor;

	private bool columnheaders_visible;

	private GridColumnStylesCollection column_styles;

	private Color gridline_color;

	private DataGridLineStyle gridline_style;

	private Color header_backcolor;

	private Font header_font;

	private Color link_color;

	private Color link_hovercolor;

	private int preferredcolumn_width;

	private int preferredrow_height;

	private bool _readonly;

	private bool rowheaders_visible;

	private Color selection_backcolor;

	private Color selection_forecolor;

	private int rowheaders_width;

	private Color backcolor;

	private Color forecolor;

	private bool is_default;

	internal ArrayList table_relations;

	private CurrencyManager manager;

	private static object AllowSortingChangedEvent;

	private static object AlternatingBackColorChangedEvent;

	private static object BackColorChangedEvent;

	private static object ColumnHeadersVisibleChangedEvent;

	private static object ForeColorChangedEvent;

	private static object GridLineColorChangedEvent;

	private static object GridLineStyleChangedEvent;

	private static object HeaderBackColorChangedEvent;

	private static object HeaderFontChangedEvent;

	private static object HeaderForeColorChangedEvent;

	private static object LinkColorChangedEvent;

	private static object LinkHoverColorChangedEvent;

	private static object MappingNameChangedEvent;

	private static object PreferredColumnWidthChangedEvent;

	private static object PreferredRowHeightChangedEvent;

	private static object ReadOnlyChangedEvent;

	private static object RowHeadersVisibleChangedEvent;

	private static object RowHeaderWidthChangedEvent;

	private static object SelectionBackColorChangedEvent;

	private static object SelectionForeColorChangedEvent;

	/// <summary>Indicates whether sorting is allowed on the grid table when this <see cref="T:System.Windows.Forms.DataGridTableStyle" /> is used.</summary>
	/// <returns>true if sorting is allowed; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool AllowSorting
	{
		get
		{
			return allow_sorting;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (allow_sorting != value)
			{
				allow_sorting = value;
				OnAllowSortingChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the background color of odd-numbered rows of the grid.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of odd-numbered rows. The default is <see cref="P:System.Drawing.SystemBrushes.Window" /></returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color AlternatingBackColor
	{
		get
		{
			return alternating_backcolor;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (alternating_backcolor != value)
			{
				alternating_backcolor = value;
				OnAlternatingBackColorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the background color of even-numbered rows of the grid.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of odd-numbered rows.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color BackColor
	{
		get
		{
			return backcolor;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (backcolor != value)
			{
				backcolor = value;
				OnForeColorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether column headers are visible.</summary>
	/// <returns>true if column headers are visible; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool ColumnHeadersVisible
	{
		get
		{
			return columnheaders_visible;
		}
		set
		{
			if (columnheaders_visible != value)
			{
				columnheaders_visible = value;
				OnColumnHeadersVisibleChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGrid" /> control for the drawn table.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGrid" /> control that displays the table.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	public virtual DataGrid DataGrid
	{
		get
		{
			return datagrid;
		}
		set
		{
			if (datagrid != value)
			{
				datagrid = value;
				for (int i = 0; i < column_styles.Count; i++)
				{
					column_styles[i].SetDataGridInternal(datagrid);
				}
			}
		}
	}

	/// <summary>Gets or sets the foreground color of the grid table.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the grid table.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color ForeColor
	{
		get
		{
			return forecolor;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (forecolor != value)
			{
				forecolor = value;
				OnBackColorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the collection of columns drawn for this table.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.GridColumnStylesCollection" /> that contains all <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> objects for the table.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public virtual GridColumnStylesCollection GridColumnStyles => column_styles;

	/// <summary>Gets or sets the color of grid lines.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the grid line color.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color GridLineColor
	{
		get
		{
			return gridline_color;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (gridline_color != value)
			{
				gridline_color = value;
				OnGridLineColorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the style of grid lines.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridLineStyle" /> values. The default is DataGridLineStyle.Solid.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(DataGridLineStyle.Solid)]
	public DataGridLineStyle GridLineStyle
	{
		get
		{
			return gridline_style;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (gridline_style != value)
			{
				gridline_style = value;
				OnGridLineStyleChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the background color of headers.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of headers.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color HeaderBackColor
	{
		get
		{
			return header_backcolor;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (value == Color.Empty)
			{
				throw new ArgumentNullException("Color.Empty value is invalid.");
			}
			if (header_backcolor != value)
			{
				header_backcolor = value;
				OnHeaderBackColorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the font used for header captions.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> used for captions.</returns>
	/// <filterpriority>1</filterpriority>
	[AmbientValue(null)]
	[Localizable(true)]
	public Font HeaderFont
	{
		get
		{
			if (header_font != null)
			{
				return header_font;
			}
			if (DataGrid != null)
			{
				return DataGrid.Font;
			}
			return def_header_font;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (header_font != value)
			{
				header_font = value;
				OnHeaderFontChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the foreground color of headers.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of headers.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color HeaderForeColor
	{
		get
		{
			return header_forecolor;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (header_forecolor != value)
			{
				header_forecolor = value;
				OnHeaderForeColorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the color of link text.</summary>
	/// <returns>The <see cref="T:System.Drawing.Color" /> of link text.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color LinkColor
	{
		get
		{
			return link_color;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (link_color != value)
			{
				link_color = value;
				OnLinkColorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the color displayed when hovering over link text.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the hover color.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public Color LinkHoverColor
	{
		get
		{
			return link_hovercolor;
		}
		set
		{
			if (link_hovercolor != value)
			{
				link_hovercolor = value;
			}
		}
	}

	/// <summary>Gets or sets the name used to map this table to a specific data source.</summary>
	/// <returns>The name used to map this grid to a specific data source.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue("")]
	[Editor("System.Windows.Forms.Design.DataGridTableStyleMappingNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public string MappingName
	{
		get
		{
			return mapping_name;
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (mapping_name != value)
			{
				mapping_name = value;
				OnMappingNameChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the width used to create columns when a new grid is displayed.</summary>
	/// <returns>The width used to create columns when a new grid is displayed.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(75)]
	[Localizable(true)]
	[TypeConverter(typeof(DataGridPreferredColumnWidthTypeConverter))]
	public int PreferredColumnWidth
	{
		get
		{
			return preferredcolumn_width;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (value < 0)
			{
				throw new ArgumentException("PreferredColumnWidth is less than 0");
			}
			if (preferredcolumn_width != value)
			{
				preferredcolumn_width = value;
				OnPreferredColumnWidthChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the height used to create a row when a new grid is displayed.</summary>
	/// <returns>The height of a row, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	public int PreferredRowHeight
	{
		get
		{
			return preferredrow_height;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (preferredrow_height != value)
			{
				preferredrow_height = value;
				OnPreferredRowHeightChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether columns can be edited.</summary>
	/// <returns>true, if columns cannot be edited; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public virtual bool ReadOnly
	{
		get
		{
			return _readonly;
		}
		set
		{
			if (_readonly != value)
			{
				_readonly = value;
				OnReadOnlyChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether row headers are visible.</summary>
	/// <returns>true if row headers are visible; otherwise, false. The default is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool RowHeadersVisible
	{
		get
		{
			return rowheaders_visible;
		}
		set
		{
			if (rowheaders_visible != value)
			{
				rowheaders_visible = value;
				OnRowHeadersVisibleChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the width of row headers.</summary>
	/// <returns>The width of row headers, in pixels.</returns>
	/// <filterpriority>1</filterpriority>
	[Localizable(true)]
	[DefaultValue(35)]
	public int RowHeaderWidth
	{
		get
		{
			return rowheaders_width;
		}
		set
		{
			if (rowheaders_width != value)
			{
				rowheaders_width = value;
				OnRowHeaderWidthChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the background color of selected cells.</summary>
	/// <returns>The <see cref="T:System.Drawing.Color" /> that represents the background color of selected cells.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color SelectionBackColor
	{
		get
		{
			return selection_backcolor;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (selection_backcolor != value)
			{
				selection_backcolor = value;
				OnSelectionBackColorChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the foreground color of selected cells.</summary>
	/// <returns>The <see cref="T:System.Drawing.Color" /> that represents the foreground color of selected cells.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Description("The foreground color for the current data grid row")]
	public Color SelectionForeColor
	{
		get
		{
			return selection_forecolor;
		}
		set
		{
			if (is_default)
			{
				throw new ArgumentException("Cannot change the value of this property on the default DataGridTableStyle.");
			}
			if (selection_forecolor != value)
			{
				selection_forecolor = value;
				OnSelectionForeColorChanged(EventArgs.Empty);
			}
		}
	}

	internal DataGridLineStyle CurrentGridLineStyle
	{
		get
		{
			if (is_default && datagrid != null)
			{
				return datagrid.GridLineStyle;
			}
			return gridline_style;
		}
	}

	internal Color CurrentGridLineColor
	{
		get
		{
			if (is_default && datagrid != null)
			{
				return datagrid.GridLineColor;
			}
			return gridline_color;
		}
	}

	internal Color CurrentHeaderBackColor
	{
		get
		{
			if (is_default && datagrid != null)
			{
				return datagrid.HeaderBackColor;
			}
			return header_backcolor;
		}
	}

	internal Color CurrentHeaderForeColor
	{
		get
		{
			if (is_default && datagrid != null)
			{
				return datagrid.HeaderForeColor;
			}
			return header_forecolor;
		}
	}

	internal int CurrentPreferredColumnWidth
	{
		get
		{
			if (is_default && datagrid != null)
			{
				return datagrid.PreferredColumnWidth;
			}
			return preferredcolumn_width;
		}
	}

	internal int CurrentPreferredRowHeight
	{
		get
		{
			if (is_default && datagrid != null)
			{
				return datagrid.PreferredRowHeight;
			}
			return preferredrow_height;
		}
	}

	internal bool CurrentRowHeadersVisible
	{
		get
		{
			if (is_default && datagrid != null)
			{
				return datagrid.RowHeadersVisible;
			}
			return rowheaders_visible;
		}
	}

	internal bool HasRelations => table_relations.Count > 0;

	internal string[] Relations
	{
		get
		{
			string[] array = new string[table_relations.Count];
			table_relations.CopyTo(array, 0);
			return array;
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.AllowSorting" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler AllowSortingChanged
	{
		add
		{
			base.Events.AddHandler(AllowSortingChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AllowSortingChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.AlternatingBackColor" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler AlternatingBackColorChanged
	{
		add
		{
			base.Events.AddHandler(AlternatingBackColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(AlternatingBackColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.BackColor" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler BackColorChanged
	{
		add
		{
			base.Events.AddHandler(BackColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(BackColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.ColumnHeadersVisible" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ColumnHeadersVisibleChanged
	{
		add
		{
			base.Events.AddHandler(ColumnHeadersVisibleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ColumnHeadersVisibleChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.ForeColor" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ForeColorChanged
	{
		add
		{
			base.Events.AddHandler(ForeColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ForeColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineColor" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler GridLineColorChanged
	{
		add
		{
			base.Events.AddHandler(GridLineColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(GridLineColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineStyle" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler GridLineStyleChanged
	{
		add
		{
			base.Events.AddHandler(GridLineStyleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(GridLineStyleChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderBackColor" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler HeaderBackColorChanged
	{
		add
		{
			base.Events.AddHandler(HeaderBackColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(HeaderBackColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderFont" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler HeaderFontChanged
	{
		add
		{
			base.Events.AddHandler(HeaderFontChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(HeaderFontChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderForeColor" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler HeaderForeColorChanged
	{
		add
		{
			base.Events.AddHandler(HeaderForeColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(HeaderForeColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkColor" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler LinkColorChanged
	{
		add
		{
			base.Events.AddHandler(LinkColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LinkColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkHoverColor" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler LinkHoverColorChanged
	{
		add
		{
			base.Events.AddHandler(LinkHoverColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(LinkHoverColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.MappingName" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler MappingNameChanged
	{
		add
		{
			base.Events.AddHandler(MappingNameChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(MappingNameChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.PreferredColumnWidth" /> property value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler PreferredColumnWidthChanged
	{
		add
		{
			base.Events.AddHandler(PreferredColumnWidthChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PreferredColumnWidthChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.PreferredRowHeight" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler PreferredRowHeightChanged
	{
		add
		{
			base.Events.AddHandler(PreferredRowHeightChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(PreferredRowHeightChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.ReadOnly" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler ReadOnlyChanged
	{
		add
		{
			base.Events.AddHandler(ReadOnlyChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ReadOnlyChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.RowHeadersVisible" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler RowHeadersVisibleChanged
	{
		add
		{
			base.Events.AddHandler(RowHeadersVisibleChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RowHeadersVisibleChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.RowHeaderWidth" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler RowHeaderWidthChanged
	{
		add
		{
			base.Events.AddHandler(RowHeaderWidthChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(RowHeaderWidthChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionBackColor" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler SelectionBackColorChanged
	{
		add
		{
			base.Events.AddHandler(SelectionBackColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectionBackColorChangedEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionForeColor" /> value changes.</summary>
	/// <filterpriority>1</filterpriority>
	public event EventHandler SelectionForeColorChanged
	{
		add
		{
			base.Events.AddHandler(SelectionForeColorChangedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectionForeColorChangedEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> class.</summary>
	public DataGridTableStyle()
		: this(isDefaultTableStyle: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> class using the specified value to determine whether the grid table is the default style.</summary>
	/// <param name="isDefaultTableStyle">true to specify the table as the default; otherwise, false. </param>
	public DataGridTableStyle(bool isDefaultTableStyle)
	{
		is_default = isDefaultTableStyle;
		allow_sorting = true;
		datagrid = null;
		header_forecolor = def_header_forecolor;
		mapping_name = string.Empty;
		table_relations = new ArrayList();
		column_styles = new GridColumnStylesCollection(this);
		alternating_backcolor = def_alternating_backcolor;
		columnheaders_visible = true;
		gridline_color = def_gridline_color;
		gridline_style = DataGridLineStyle.Solid;
		header_backcolor = def_header_backcolor;
		header_font = null;
		link_color = def_link_color;
		link_hovercolor = def_link_hovercolor;
		preferredcolumn_width = ThemeEngine.Current.DataGridPreferredColumnWidth;
		preferredrow_height = ThemeEngine.Current.DefaultFont.Height + 3;
		_readonly = false;
		rowheaders_visible = true;
		selection_backcolor = def_selection_backcolor;
		selection_forecolor = def_selection_forecolor;
		rowheaders_width = 35;
		backcolor = def_backcolor;
		forecolor = def_forecolor;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridTableStyle" /> class with the specified <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
	/// <param name="listManager">The <see cref="T:System.Windows.Forms.CurrencyManager" /> to use. </param>
	public DataGridTableStyle(CurrencyManager listManager)
		: this(isDefaultTableStyle: false)
	{
		manager = listManager;
	}

	static DataGridTableStyle()
	{
		AllowSortingChanged = new object();
		AlternatingBackColorChanged = new object();
		BackColorChanged = new object();
		ColumnHeadersVisibleChanged = new object();
		ForeColorChanged = new object();
		GridLineColorChanged = new object();
		GridLineStyleChanged = new object();
		HeaderBackColorChanged = new object();
		HeaderFontChanged = new object();
		HeaderForeColorChanged = new object();
		LinkColorChanged = new object();
		LinkHoverColorChanged = new object();
		MappingNameChanged = new object();
		PreferredColumnWidthChanged = new object();
		PreferredRowHeightChanged = new object();
		ReadOnlyChanged = new object();
		RowHeadersVisibleChanged = new object();
		RowHeaderWidthChanged = new object();
		SelectionBackColorChanged = new object();
		SelectionForeColorChanged = new object();
	}

	/// <summary>Requests an edit operation.</summary>
	/// <returns>true, if the operation succeeds; otherwise, false.</returns>
	/// <param name="gridColumn">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to edit. </param>
	/// <param name="rowNumber">The number of the edited row. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("Not implemented, will throw NotImplementedException")]
	public bool BeginEdit(DataGridColumnStyle gridColumn, int rowNumber)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a <see cref="T:System.Windows.Forms.DataGridColumnStyle" />, using the specified property descriptor.</summary>
	/// <returns>The newly created <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
	/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used to create the column style object. </param>
	protected internal virtual DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop)
	{
		return CreateGridColumn(prop, isDefault: false);
	}

	/// <summary>Creates a <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> using the specified property descriptor. Specifies whether the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> is a default column style.</summary>
	/// <returns>The newly created <see cref="T:System.Windows.Forms.DataGridColumnStyle" />.</returns>
	/// <param name="prop">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> used to create the column style object. </param>
	/// <param name="isDefault">Specifies whether the <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> is a default column style. This parameter is read-only. </param>
	protected internal virtual DataGridColumnStyle CreateGridColumn(PropertyDescriptor prop, bool isDefault)
	{
		if ((object)prop.PropertyType == typeof(bool))
		{
			return new DataGridBoolColumn(prop, isDefault);
		}
		if (prop.PropertyType.Equals(typeof(DateTime)))
		{
			return new DataGridTextBoxColumn(prop, "d", isDefault);
		}
		if (prop.PropertyType.Equals(typeof(int)) || prop.PropertyType.Equals(typeof(short)))
		{
			return new DataGridTextBoxColumn(prop, "G", isDefault);
		}
		return new DataGridTextBoxColumn(prop, isDefault);
	}

	/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.DataGridTableStyle" />.</summary>
	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);
	}

	/// <summary>Requests an end to an edit operation.</summary>
	/// <returns>true if the edit operation ends successfully; otherwise, false.</returns>
	/// <param name="gridColumn">The <see cref="T:System.Windows.Forms.DataGridColumnStyle" /> to edit. </param>
	/// <param name="rowNumber">The number of the edited row. </param>
	/// <param name="shouldAbort">A value indicating whether the operation should be stopped; true if it should stop; otherwise, false. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[System.MonoTODO("Not implemented, will throw NotImplementedException")]
	public bool EndEdit(DataGridColumnStyle gridColumn, int rowNumber, bool shouldAbort)
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.AllowSortingChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnAllowSortingChanged(EventArgs e)
	{
		((EventHandler)base.Events[AllowSortingChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.AlternatingBackColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnAlternatingBackColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[AlternatingBackColorChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.BackColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnBackColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[BackColorChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.ColumnHeadersVisibleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnColumnHeadersVisibleChanged(EventArgs e)
	{
		((EventHandler)base.Events[ColumnHeadersVisibleChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.ForeColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnForeColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[ForeColorChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.GridLineColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnGridLineColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[GridLineColorChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.GridLineStyleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnGridLineStyleChanged(EventArgs e)
	{
		((EventHandler)base.Events[GridLineStyleChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.HeaderBackColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnHeaderBackColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[HeaderBackColorChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.HeaderFontChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnHeaderFontChanged(EventArgs e)
	{
		((EventHandler)base.Events[HeaderFontChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.HeaderForeColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnHeaderForeColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[HeaderForeColorChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.LinkColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnLinkColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[LinkColorChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the LinkHoverColorChanged event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnLinkHoverColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[LinkHoverColorChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.MappingNameChanged" /> event </summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnMappingNameChanged(EventArgs e)
	{
		((EventHandler)base.Events[MappingNameChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.PreferredColumnWidthChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnPreferredColumnWidthChanged(EventArgs e)
	{
		((EventHandler)base.Events[PreferredColumnWidthChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.PreferredRowHeightChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnPreferredRowHeightChanged(EventArgs e)
	{
		((EventHandler)base.Events[PreferredRowHeightChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.ReadOnlyChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnReadOnlyChanged(EventArgs e)
	{
		((EventHandler)base.Events[ReadOnlyChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.RowHeadersVisibleChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnRowHeadersVisibleChanged(EventArgs e)
	{
		((EventHandler)base.Events[RowHeadersVisibleChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.RowHeaderWidthChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnRowHeaderWidthChanged(EventArgs e)
	{
		((EventHandler)base.Events[RowHeaderWidthChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.SelectionBackColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnSelectionBackColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[SelectionBackColorChanged])?.Invoke(this, e);
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridTableStyle.SelectionForeColorChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected virtual void OnSelectionForeColorChanged(EventArgs e)
	{
		((EventHandler)base.Events[SelectionForeColorChanged])?.Invoke(this, e);
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.AlternatingBackColor" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ResetAlternatingBackColor()
	{
		AlternatingBackColor = def_alternating_backcolor;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.BackColor" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ResetBackColor()
	{
		BackColor = def_backcolor;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.ForeColor" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ResetForeColor()
	{
		ForeColor = def_forecolor;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineColor" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ResetGridLineColor()
	{
		GridLineColor = def_gridline_color;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderBackColor" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ResetHeaderBackColor()
	{
		HeaderBackColor = def_header_backcolor;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderFont" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	public void ResetHeaderFont()
	{
		HeaderFont = def_header_font;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderForeColor" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ResetHeaderForeColor()
	{
		HeaderForeColor = def_header_forecolor;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkColor" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ResetLinkColor()
	{
		LinkColor = def_link_color;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkHoverColor" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	public void ResetLinkHoverColor()
	{
		LinkHoverColor = def_link_hovercolor;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionBackColor" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ResetSelectionBackColor()
	{
		SelectionBackColor = def_selection_backcolor;
	}

	/// <summary>Resets the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionForeColor" /> property to its default value.</summary>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public void ResetSelectionForeColor()
	{
		SelectionForeColor = def_selection_forecolor;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.AlternatingBackColor" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected virtual bool ShouldSerializeAlternatingBackColor()
	{
		return alternating_backcolor != def_alternating_backcolor;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.BackColor" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected bool ShouldSerializeBackColor()
	{
		return backcolor != def_backcolor;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.ForeColor" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected bool ShouldSerializeForeColor()
	{
		return forecolor != def_forecolor;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.GridLineColor" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected virtual bool ShouldSerializeGridLineColor()
	{
		return gridline_color != def_gridline_color;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderBackColor" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected virtual bool ShouldSerializeHeaderBackColor()
	{
		return header_backcolor != def_header_backcolor;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.HeaderForeColor" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected virtual bool ShouldSerializeHeaderForeColor()
	{
		return header_forecolor != def_header_forecolor;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkColor" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected virtual bool ShouldSerializeLinkColor()
	{
		return link_color != def_link_color;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.LinkHoverColor" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected virtual bool ShouldSerializeLinkHoverColor()
	{
		return link_hovercolor != def_link_hovercolor;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.PreferredRowHeight" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected bool ShouldSerializePreferredRowHeight()
	{
		return preferredrow_height != def_preferredrow_height;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionBackColor" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected bool ShouldSerializeSelectionBackColor()
	{
		return selection_backcolor != def_selection_backcolor;
	}

	/// <summary>Indicates whether the <see cref="P:System.Windows.Forms.DataGridTableStyle.SelectionForeColor" /> property should be persisted.</summary>
	/// <returns>true if the property value has changed from its default; otherwise, false.</returns>
	protected virtual bool ShouldSerializeSelectionForeColor()
	{
		return selection_forecolor != def_selection_forecolor;
	}

	internal void CreateColumnsForTable(bool onlyBind)
	{
		CurrencyManager listManager = manager;
		if (listManager == null)
		{
			listManager = datagrid.ListManager;
			if (listManager == null)
			{
				return;
			}
		}
		for (int i = 0; i < column_styles.Count; i++)
		{
			column_styles[i].bound = false;
		}
		table_relations.Clear();
		PropertyDescriptorCollection itemProperties = listManager.GetItemProperties();
		for (int j = 0; j < itemProperties.Count; j++)
		{
			DataGridColumnStyle dataGridColumnStyle = column_styles[itemProperties[j].Name];
			if (dataGridColumnStyle != null)
			{
				if (dataGridColumnStyle.Width == -1)
				{
					dataGridColumnStyle.Width = CurrentPreferredColumnWidth;
				}
				dataGridColumnStyle.PropertyDescriptor = itemProperties[j];
				dataGridColumnStyle.bound = true;
			}
			else if (!onlyBind)
			{
				if (typeof(IBindingList).IsAssignableFrom(itemProperties[j].PropertyType))
				{
					table_relations.Add(itemProperties[j].Name);
					continue;
				}
				dataGridColumnStyle = CreateGridColumn(itemProperties[j], isDefault: true);
				dataGridColumnStyle.bound = true;
				dataGridColumnStyle.grid = datagrid;
				dataGridColumnStyle.MappingName = itemProperties[j].Name;
				dataGridColumnStyle.HeaderText = itemProperties[j].Name;
				dataGridColumnStyle.Width = CurrentPreferredColumnWidth;
				column_styles.Add(dataGridColumnStyle);
			}
		}
	}
}
