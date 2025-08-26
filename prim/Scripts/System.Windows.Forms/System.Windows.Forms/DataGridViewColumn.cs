using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms;

/// <summary>Represents a column in a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
[DesignTimeVisible(false)]
[ToolboxItem("")]
[TypeConverter(typeof(DataGridViewColumnConverter))]
[Designer("System.Windows.Forms.Design.DataGridViewColumnDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public class DataGridViewColumn : DataGridViewBand, IDisposable, IComponent
{
	private bool auto_generated;

	private DataGridViewAutoSizeColumnMode autoSizeMode;

	private DataGridViewCell cellTemplate;

	private ContextMenuStrip contextMenuStrip;

	private string dataPropertyName;

	private int displayIndex;

	private int dividerWidth;

	private float fillWeight;

	private bool frozen;

	private DataGridViewColumnHeaderCell headerCell;

	private bool isDataBound;

	private int minimumWidth = 5;

	private string name = string.Empty;

	private bool readOnly;

	private ISite site;

	private DataGridViewColumnSortMode sortMode;

	private string toolTipText;

	private Type valueType;

	private bool visible = true;

	private int width = 100;

	private int dataColumnIndex;

	private bool headerTextSet;

	/// <summary>Gets or sets the mode by which the column automatically adjusts its width.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value that determines whether the column will automatically adjust its width and how it will determine its preferred width. The default is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is a <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> that is not valid. </exception>
	/// <exception cref="T:System.InvalidOperationException">The specified value when setting this property results in an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader" /> for a visible column when column headers are hidden.-or-The specified value when setting this property results in an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode" /> value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" /> for a visible column that is frozen.</exception>
	[DefaultValue(DataGridViewAutoSizeColumnMode.NotSet)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public DataGridViewAutoSizeColumnMode AutoSizeMode
	{
		get
		{
			return autoSizeMode;
		}
		set
		{
			if (autoSizeMode != value)
			{
				DataGridViewAutoSizeColumnMode previousMode = autoSizeMode;
				autoSizeMode = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnAutoSizeColumnModeChanged(new DataGridViewAutoSizeColumnModeEventArgs(this, previousMode));
					base.DataGridView.AutoResizeColumnsInternal();
				}
			}
		}
	}

	/// <summary>Gets or sets the template used to create new cells.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual DataGridViewCell CellTemplate
	{
		get
		{
			return cellTemplate;
		}
		set
		{
			cellTemplate = value;
		}
	}

	/// <summary>Gets the run-time type of the cell template.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Windows.Forms.DataGridViewCell" /> used as a template for this column. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public Type CellType
	{
		get
		{
			if (cellTemplate == null)
			{
				return null;
			}
			return cellTemplate.GetType();
		}
	}

	/// <summary>Gets or sets the shortcut menu for the column.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.ContextMenuStrip" /> associated with the current <see cref="T:System.Windows.Forms.DataGridViewColumn" />. The default is null.</returns>
	[DefaultValue(null)]
	public override ContextMenuStrip ContextMenuStrip
	{
		get
		{
			return contextMenuStrip;
		}
		set
		{
			if (contextMenuStrip != value)
			{
				contextMenuStrip = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnContextMenuStripChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets the name of the data source property or database column to which the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> is bound.</summary>
	/// <returns>The case-insensitive name of the property or database column associated with the <see cref="T:System.Windows.Forms.DataGridViewColumn" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue("")]
	[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Editor("System.Windows.Forms.Design.DataGridViewColumnDataPropertyNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Browsable(true)]
	public string DataPropertyName
	{
		get
		{
			return dataPropertyName;
		}
		set
		{
			if (dataPropertyName != value)
			{
				dataPropertyName = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnDataPropertyNameChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets the column's default cell style.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the default style of the cells in the column.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(true)]
	public override DataGridViewCellStyle DefaultCellStyle
	{
		get
		{
			return base.DefaultCellStyle;
		}
		set
		{
			if (DefaultCellStyle != value)
			{
				base.DefaultCellStyle = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnDefaultCellStyleChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets the display order of the column relative to the currently displayed columns.</summary>
	/// <returns>The zero-based position of the column as it is displayed in the associated <see cref="T:System.Windows.Forms.DataGridView" />, or -1 if the band is not contained within a control. </returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> is not null and the specified value when setting this property is less than 0 or greater than or equal to the number of columns in the control.-or-<see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView" /> is null and the specified value when setting this property is less than -1.-or-The specified value when setting this property is equal to <see cref="F:System.Int32.MaxValue" />. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public int DisplayIndex
	{
		get
		{
			if (displayIndex < 0)
			{
				return base.Index;
			}
			return displayIndex;
		}
		set
		{
			if (displayIndex != value)
			{
				if (value < 0 || value > int.MaxValue)
				{
					throw new ArgumentOutOfRangeException("DisplayIndex is out of range");
				}
				displayIndex = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.Columns.RegenerateSortedList();
					base.DataGridView.OnColumnDisplayIndexChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	internal int DataColumnIndex
	{
		get
		{
			return dataColumnIndex;
		}
		set
		{
			dataColumnIndex = value;
		}
	}

	/// <summary>Gets or sets the width, in pixels, of the column divider.</summary>
	/// <returns>The thickness, in pixels, of the divider (the column's right margin). </returns>
	[DefaultValue(0)]
	public int DividerWidth
	{
		get
		{
			return dividerWidth;
		}
		set
		{
			if (dividerWidth != value)
			{
				dividerWidth = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnDividerWidthChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets a value that represents the width of the column when it is in fill mode relative to the widths of other fill-mode columns in the control.</summary>
	/// <returns>A <see cref="T:System.Single" /> representing the width of the column when it is in fill mode relative to the widths of other fill-mode columns. The default is 100.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than or equal to 0. </exception>
	[DefaultValue(100)]
	public float FillWeight
	{
		get
		{
			return fillWeight;
		}
		set
		{
			fillWeight = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a column will move when a user scrolls the <see cref="T:System.Windows.Forms.DataGridView" /> control horizontally.</summary>
	/// <returns>true to freeze the column; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.All)]
	[DefaultValue(false)]
	public override bool Frozen
	{
		get
		{
			return frozen;
		}
		set
		{
			frozen = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> that represents the column header.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell" /> that represents the header cell for the column.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public DataGridViewColumnHeaderCell HeaderCell
	{
		get
		{
			return headerCell;
		}
		set
		{
			if (headerCell != value)
			{
				headerCell = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnHeaderCellChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets the caption text on the column's header cell.</summary>
	/// <returns>A <see cref="T:System.String" /> with the desired text. The default is an empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Localizable(true)]
	public string HeaderText
	{
		get
		{
			if (headerCell.Value == null)
			{
				return string.Empty;
			}
			return (string)headerCell.Value;
		}
		set
		{
			headerCell.Value = value;
			headerTextSet = true;
		}
	}

	internal bool AutoGenerated
	{
		get
		{
			return auto_generated;
		}
		set
		{
			auto_generated = value;
		}
	}

	internal bool HeaderTextSet => headerTextSet;

	/// <summary>Gets the sizing mode in effect for the column.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value in effect for the column.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public DataGridViewAutoSizeColumnMode InheritedAutoSizeMode
	{
		get
		{
			if (base.DataGridView == null)
			{
				return autoSizeMode;
			}
			if (autoSizeMode != 0)
			{
				return autoSizeMode;
			}
			return base.DataGridView.AutoSizeColumnsMode switch
			{
				DataGridViewAutoSizeColumnsMode.AllCells => DataGridViewAutoSizeColumnMode.AllCells, 
				DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader => DataGridViewAutoSizeColumnMode.AllCellsExceptHeader, 
				DataGridViewAutoSizeColumnsMode.ColumnHeader => DataGridViewAutoSizeColumnMode.ColumnHeader, 
				DataGridViewAutoSizeColumnsMode.DisplayedCells => DataGridViewAutoSizeColumnMode.DisplayedCells, 
				DataGridViewAutoSizeColumnsMode.DisplayedCellsExceptHeader => DataGridViewAutoSizeColumnMode.DisplayedCellsExceptHeader, 
				DataGridViewAutoSizeColumnsMode.Fill => DataGridViewAutoSizeColumnMode.Fill, 
				_ => DataGridViewAutoSizeColumnMode.None, 
			};
		}
	}

	/// <summary>Gets the cell style currently applied to the column.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents the cell style used to display the column.</returns>
	[Browsable(false)]
	public override DataGridViewCellStyle InheritedStyle
	{
		get
		{
			if (base.DataGridView == null)
			{
				return base.DefaultCellStyle;
			}
			if (base.DefaultCellStyle == null)
			{
				return base.DataGridView.DefaultCellStyle;
			}
			return base.DefaultCellStyle.Clone();
		}
	}

	/// <summary>Gets a value indicating whether the column is bound to a data source.</summary>
	/// <returns>true if the column is connected to a data source; otherwise, false.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool IsDataBound => isDataBound;

	/// <summary>Gets or sets the minimum width, in pixels, of the column.</summary>
	/// <returns>The number of pixels, from 2 to <see cref="F:System.Int32.MaxValue" />, that specifies the minimum width of the column. The default is 5.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 2 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(5)]
	[Localizable(true)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public int MinimumWidth
	{
		get
		{
			return minimumWidth;
		}
		set
		{
			if (minimumWidth != value)
			{
				if (value < 2 || value > int.MaxValue)
				{
					throw new ArgumentOutOfRangeException("MinimumWidth is out of range");
				}
				minimumWidth = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnMinimumWidthChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets the name of the column.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the name of the column. The default is an empty string ("").</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Browsable(false)]
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			if (name != value)
			{
				if (value == null)
				{
					name = string.Empty;
				}
				else
				{
					name = value;
				}
				if (!headerTextSet)
				{
					headerCell.Value = name;
				}
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnNameChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the user can edit the column's cells.</summary>
	/// <returns>true if the user cannot edit the column's cells; otherwise, false.</returns>
	/// <exception cref="T:System.InvalidOperationException">This property is set to false for a column that is bound to a read-only data source. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override bool ReadOnly
	{
		get
		{
			if (base.DataGridView != null && base.DataGridView.ReadOnly)
			{
				return true;
			}
			return readOnly;
		}
		set
		{
			readOnly = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the column is resizable.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewTriState" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.True" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override DataGridViewTriState Resizable
	{
		get
		{
			return base.Resizable;
		}
		set
		{
			base.Resizable = value;
		}
	}

	/// <summary>Gets or sets the site of the column.</summary>
	/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the column, if any.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public ISite Site
	{
		get
		{
			return site;
		}
		set
		{
			site = value;
		}
	}

	/// <summary>Gets or sets the sort mode for the column.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewColumnSortMode" /> that specifies the criteria used to order the rows based on the cell values in a column.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value assigned to the property conflicts with <see cref="P:System.Windows.Forms.DataGridView.SelectionMode" />. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(DataGridViewColumnSortMode.NotSortable)]
	public DataGridViewColumnSortMode SortMode
	{
		get
		{
			return sortMode;
		}
		set
		{
			if (base.DataGridView != null && value == DataGridViewColumnSortMode.Automatic && (base.DataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect || base.DataGridView.SelectionMode == DataGridViewSelectionMode.ColumnHeaderSelect))
			{
				throw new InvalidOperationException("Column's SortMode cannot be set to Automatic while the DataGridView control's SelectionMode is set to FullColumnSelect or ColumnHeaderSelect.");
			}
			if (sortMode != value)
			{
				sortMode = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnSortModeChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets the text used for ToolTips.</summary>
	/// <returns>The text to display as a ToolTip for the column.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue("")]
	[Localizable(true)]
	public string ToolTipText
	{
		get
		{
			if (toolTipText == null)
			{
				return string.Empty;
			}
			return toolTipText;
		}
		set
		{
			if (toolTipText != value)
			{
				toolTipText = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.OnColumnToolTipTextChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	/// <summary>Gets or sets the data type of the values in the column's cells.</summary>
	/// <returns>A <see cref="T:System.Type" /> that describes the run-time class of the values stored in the column's cells.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Type ValueType
	{
		get
		{
			return valueType;
		}
		set
		{
			valueType = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the column is visible.</summary>
	/// <returns>true if the column is visible; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(true)]
	[Localizable(true)]
	public override bool Visible
	{
		get
		{
			return visible;
		}
		set
		{
			visible = value;
			if (base.DataGridView != null)
			{
				base.DataGridView.Invalidate();
			}
		}
	}

	/// <summary>Gets or sets the current width of the column.</summary>
	/// <returns>The width, in pixels, of the column. The default is 100.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is greater than 65536.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[RefreshProperties(RefreshProperties.Repaint)]
	[Localizable(true)]
	public int Width
	{
		get
		{
			return width;
		}
		set
		{
			if (width != value)
			{
				if (value < minimumWidth)
				{
					throw new ArgumentOutOfRangeException("Width is less than MinimumWidth");
				}
				width = value;
				if (base.DataGridView != null)
				{
					base.DataGridView.Invalidate();
					base.DataGridView.OnColumnWidthChanged(new DataGridViewColumnEventArgs(this));
				}
			}
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> is disposed.</summary>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public event EventHandler Disposed;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> class to the default state.</summary>
	public DataGridViewColumn()
	{
		cellTemplate = null;
		base.DefaultCellStyle = new DataGridViewCellStyle();
		readOnly = false;
		headerCell = new DataGridViewColumnHeaderCell();
		headerCell.SetColumnIndex(base.Index);
		headerCell.Value = string.Empty;
		displayIndex = -1;
		dataColumnIndex = -1;
		dataPropertyName = string.Empty;
		fillWeight = 100f;
		sortMode = DataGridViewColumnSortMode.NotSortable;
		SetState(DataGridViewElementStates.Visible);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumn" /> class using an existing <see cref="T:System.Windows.Forms.DataGridViewCell" /> as a template.</summary>
	/// <param name="cellTemplate">An existing <see cref="T:System.Windows.Forms.DataGridViewCell" /> to use as a template. </param>
	public DataGridViewColumn(DataGridViewCell cellTemplate)
		: this()
	{
		this.cellTemplate = (DataGridViewCell)cellTemplate.Clone();
	}

	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewBand" />.</returns>
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

	/// <summary>Calculates the ideal width of the column based on the specified criteria.</summary>
	/// <returns>The ideal width, in pixels, of the column.</returns>
	/// <param name="autoSizeColumnMode">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value that specifies an automatic sizing mode. </param>
	/// <param name="fixedHeight">true to calculate the width of the column based on the current row heights; false to calculate the width with the expectation that the row heights will be adjusted.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="autoSizeColumnMode" /> is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet" />, <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.None" />, or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill" />. </exception>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
	///   <paramref name="autoSizeColumnMode" /> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode" /> value. </exception>
	public virtual int GetPreferredWidth(DataGridViewAutoSizeColumnMode autoSizeColumnMode, bool fixedHeight)
	{
		if (autoSizeColumnMode == DataGridViewAutoSizeColumnMode.NotSet || autoSizeColumnMode == DataGridViewAutoSizeColumnMode.None || autoSizeColumnMode == DataGridViewAutoSizeColumnMode.Fill)
		{
			throw new ArgumentException("AutoSizeColumnMode is invalid");
		}
		if (fixedHeight)
		{
			return 0;
		}
		return 0;
	}

	/// <summary>Gets a string that describes the column.</summary>
	/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return Name + ", Index: " + base.Index + ".";
	}

	/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
		if (!disposing)
		{
		}
	}

	internal override void SetDataGridView(DataGridView dataGridView)
	{
		if (sortMode == DataGridViewColumnSortMode.Automatic && dataGridView != null && dataGridView.SelectionMode == DataGridViewSelectionMode.FullColumnSelect)
		{
			throw new InvalidOperationException("Column's SortMode cannot be set to Automatic while the DataGridView control's SelectionMode is set to FullColumnSelect.");
		}
		base.SetDataGridView(dataGridView);
		headerCell.SetDataGridView(dataGridView);
	}

	internal override void SetIndex(int index)
	{
		base.SetIndex(index);
		headerCell.SetColumnIndex(base.Index);
	}

	internal void SetIsDataBound(bool value)
	{
		isDataBound = value;
	}

	internal override void SetState(DataGridViewElementStates state)
	{
		if (State != state)
		{
			base.SetState(state);
			if (base.DataGridView != null)
			{
				base.DataGridView.OnColumnStateChanged(new DataGridViewColumnStateChangedEventArgs(this, state));
			}
		}
	}
}
