using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Represents a column of <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> objects.</summary>
/// <filterpriority>2</filterpriority>
[Designer("System.Windows.Forms.Design.DataGridViewComboBoxColumnDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxBitmap("")]
public class DataGridViewComboBoxColumn : DataGridViewColumn
{
	private bool autoComplete;

	private DataGridViewComboBoxDisplayStyle displayStyle;

	private bool displayStyleForCurrentCellOnly;

	private FlatStyle flatStyle;

	/// <summary>Gets or sets a value indicating whether cells in the column will match the characters being entered in the cell with one from the possible selections. </summary>
	/// <returns>true if auto completion is activated; otherwise, false. The default is true.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null.</exception>
	/// <filterpriority>1</filterpriority>
	[Browsable(true)]
	[DefaultValue(true)]
	public bool AutoComplete
	{
		get
		{
			return autoComplete;
		}
		set
		{
			autoComplete = value;
		}
	}

	/// <summary>Gets or sets the template used to create cells.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default value is a new <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</returns>
	/// <exception cref="T:System.InvalidCastException">When setting this property to a value that is not of type <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />. </exception>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public override DataGridViewCell CellTemplate
	{
		get
		{
			return base.CellTemplate;
		}
		set
		{
			if (!(value is DataGridViewComboBoxCell dataGridViewComboBoxCell))
			{
				throw new InvalidCastException("Invalid cell tempalte type.");
			}
			dataGridViewComboBoxCell.OwningColumnTemplate = this;
			base.CellTemplate = dataGridViewComboBoxCell;
		}
	}

	/// <summary>Gets or sets the data source that populates the selections for the combo boxes.</summary>
	/// <returns>An object that represents a data source. The default is null.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[AttributeProvider(typeof(IListSource))]
	[DefaultValue(null)]
	[RefreshProperties(RefreshProperties.Repaint)]
	public object DataSource
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewComboBoxCell).DataSource;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewComboBoxCell).DataSource = value;
		}
	}

	/// <summary>Gets or sets a string that specifies the property or column from which to retrieve strings for display in the combo boxes.</summary>
	/// <returns>A <see cref="T:System.String" /> that specifies the name of a property or column in the data source specified in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.DataSource" /> property. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string DisplayMember
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewComboBoxCell).DisplayMember;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewComboBoxCell).DisplayMember = value;
		}
	}

	/// <summary>Gets or sets a value that determines how the combo box is displayed when not editing.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewComboBoxDisplayStyle" /> value indicating the combo box appearance. The default is <see cref="F:System.Windows.Forms.DataGridViewComboBoxDisplayStyle.DropDownButton" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null.</exception>
	[DefaultValue(DataGridViewComboBoxDisplayStyle.DropDownButton)]
	public DataGridViewComboBoxDisplayStyle DisplayStyle
	{
		get
		{
			return displayStyle;
		}
		set
		{
			displayStyle = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.DisplayStyle" /> property value applies only to the current cell in the <see cref="T:System.Windows.Forms.DataGridView" /> control when the current cell is in this column.</summary>
	/// <returns>true if the display style applies only to the current cell; otherwise false. The default is false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null.</exception>
	[DefaultValue(false)]
	public bool DisplayStyleForCurrentCellOnly
	{
		get
		{
			return displayStyleForCurrentCellOnly;
		}
		set
		{
			displayStyleForCurrentCellOnly = value;
		}
	}

	/// <summary>Gets or sets the width of the drop-down lists of the combo boxes.</summary>
	/// <returns>The width, in pixels, of the drop-down lists. The default is 1.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(1)]
	public int DropDownWidth
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewComboBoxCell).DropDownWidth;
		}
		set
		{
			if (value < 1)
			{
				throw new ArgumentException("Value is less than 1.");
			}
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewComboBoxCell).DropDownWidth = value;
		}
	}

	/// <summary>Gets or sets the flat style appearance of the column's cells.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.FlatStyle" /> value indicating the cell appearance. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null.</exception>
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
			flatStyle = value;
		}
	}

	/// <summary>Gets the collection of objects used as selections in the combo boxes.</summary>
	/// <returns>An <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell.ObjectCollection" /> that represents the selections in the combo boxes. </returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public DataGridViewComboBoxCell.ObjectCollection Items
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewComboBoxCell).Items;
		}
	}

	/// <summary>Gets or sets the maximum number of items in the drop-down list of the cells in the column.</summary>
	/// <returns>The maximum number of drop-down list items, from 1 to 100. The default is 8.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(8)]
	public int MaxDropDownItems
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewComboBoxCell).MaxDropDownItems;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewComboBoxCell).MaxDropDownItems = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the items in the combo box are sorted.</summary>
	/// <returns>true if the combo box is sorted; otherwise, false. The default is false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(false)]
	public bool Sorted
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewComboBoxCell).Sorted;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewComboBoxCell).Sorted = value;
		}
	}

	/// <summary>Gets or sets a string that specifies the property or column from which to get values that correspond to the selections in the drop-down list.</summary>
	/// <returns>A <see cref="T:System.String" /> that specifies the name of a property or column used in the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.DataSource" /> property. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewComboBoxColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	public string ValueMember
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewComboBoxCell).ValueMember;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewComboBoxCell).ValueMember = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxColumn" /> class to the default state.</summary>
	public DataGridViewComboBoxColumn()
	{
		CellTemplate = new DataGridViewComboBoxCell();
		((DataGridViewComboBoxCell)CellTemplate).OwningColumnTemplate = this;
		base.SortMode = DataGridViewColumnSortMode.NotSortable;
		autoComplete = true;
		displayStyle = DataGridViewComboBoxDisplayStyle.DropDownButton;
		displayStyleForCurrentCellOnly = false;
	}

	internal void SyncItems(IList items)
	{
		if (DataSource != null || base.DataGridView == null)
		{
			return;
		}
		for (int i = 0; i < base.DataGridView.RowCount; i++)
		{
			if (base.DataGridView.Rows[i].Cells[base.Index] is DataGridViewComboBoxCell dataGridViewComboBoxCell)
			{
				dataGridViewComboBoxCell.Items.ClearInternal();
				dataGridViewComboBoxCell.Items.AddRangeInternal(Items);
			}
		}
	}

	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object Clone()
	{
		DataGridViewComboBoxColumn dataGridViewComboBoxColumn = (DataGridViewComboBoxColumn)base.Clone();
		dataGridViewComboBoxColumn.autoComplete = autoComplete;
		dataGridViewComboBoxColumn.displayStyle = displayStyle;
		dataGridViewComboBoxColumn.displayStyleForCurrentCellOnly = displayStyleForCurrentCellOnly;
		dataGridViewComboBoxColumn.flatStyle = flatStyle;
		dataGridViewComboBoxColumn.CellTemplate = (DataGridViewComboBoxCell)CellTemplate.Clone();
		return dataGridViewComboBoxColumn;
	}

	/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return GetType().Name;
	}
}
