using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewImageCell" /> objects.</summary>
/// <filterpriority>2</filterpriority>
[ToolboxBitmap("")]
public class DataGridViewImageColumn : DataGridViewColumn
{
	private Icon icon;

	private Image image;

	private bool valuesAreIcons;

	/// <summary>Gets or sets the template used to create new cells.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after.</returns>
	/// <exception cref="T:System.InvalidCastException">The set type is not compatible with type <see cref="T:System.Windows.Forms.DataGridViewImageCell" />. </exception>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public override DataGridViewCell CellTemplate
	{
		get
		{
			return base.CellTemplate;
		}
		set
		{
			base.CellTemplate = value as DataGridViewImageCell;
		}
	}

	/// <summary>Gets or sets the column's default cell style.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to be applied as the default style.</returns>
	[Browsable(true)]
	public override DataGridViewCellStyle DefaultCellStyle
	{
		get
		{
			return base.DefaultCellStyle;
		}
		set
		{
			base.DefaultCellStyle = value;
		}
	}

	/// <summary>Gets or sets a string that describes the column's image. </summary>
	/// <returns>The textual description of the column image. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewImageColumn.CellTemplate" /> property is null.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue("")]
	[Browsable(true)]
	public string Description
	{
		get
		{
			return (base.CellTemplate as DataGridViewImageCell).Description;
		}
		set
		{
			(base.CellTemplate as DataGridViewImageCell).Description = value;
		}
	}

	/// <summary>Gets or sets the icon displayed in the cells of this column when the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property is not set and the cell's <see cref="P:System.Windows.Forms.DataGridViewImageCell.ValueIsIcon" /> property is set to true.</summary>
	/// <returns>The <see cref="T:System.Drawing.Icon" /> to display. The default is null.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public Icon Icon
	{
		get
		{
			return icon;
		}
		set
		{
			icon = value;
		}
	}

	/// <summary>Gets or sets the image displayed in the cells of this column when the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property is not set and the cell's <see cref="P:System.Windows.Forms.DataGridViewImageCell.ValueIsIcon" /> property is set to false.</summary>
	/// <returns>The <see cref="T:System.Drawing.Image" /> to display. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	public Image Image
	{
		get
		{
			return image;
		}
		set
		{
			image = value;
		}
	}

	/// <summary>Gets or sets the image layout in the cells for this column.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewImageCellLayout" /> that specifies the cell layout. The default is <see cref="F:System.Windows.Forms.DataGridViewImageCellLayout.Normal" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewImageColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(DataGridViewImageCellLayout.Normal)]
	public DataGridViewImageCellLayout ImageLayout
	{
		get
		{
			return (base.CellTemplate as DataGridViewImageCell).ImageLayout;
		}
		set
		{
			(base.CellTemplate as DataGridViewImageCell).ImageLayout = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether cells in this column display <see cref="T:System.Drawing.Icon" /> values.</summary>
	/// <returns>true if cells display values of type <see cref="T:System.Drawing.Icon" />; false if cells display values of type <see cref="T:System.Drawing.Image" />. The default is false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewImageColumn.CellTemplate" /> property is null.</exception>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool ValuesAreIcons
	{
		get
		{
			return valuesAreIcons;
		}
		set
		{
			valuesAreIcons = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageColumn" /> class, configuring it for use with cell values of type <see cref="T:System.Drawing.Image" />.</summary>
	public DataGridViewImageColumn()
		: this(valuesAreIcons: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewImageColumn" /> class, optionally configuring it for use with <see cref="T:System.Drawing.Icon" /> cell values.</summary>
	/// <param name="valuesAreIcons">true to indicate that the <see cref="P:System.Windows.Forms.DataGridViewCell.Value" /> property of cells in this column will be set to values of type <see cref="T:System.Drawing.Icon" />; false to indicate that they will be set to values of type <see cref="T:System.Drawing.Image" />.</param>
	public DataGridViewImageColumn(bool valuesAreIcons)
	{
		this.valuesAreIcons = valuesAreIcons;
		base.CellTemplate = new DataGridViewImageCell(valuesAreIcons);
		(base.CellTemplate as DataGridViewImageCell).ImageLayout = DataGridViewImageCellLayout.Normal;
		DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
		icon = null;
		image = null;
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
		DataGridViewImageColumn dataGridViewImageColumn = (DataGridViewImageColumn)base.Clone();
		dataGridViewImageColumn.icon = icon;
		dataGridViewImageColumn.image = image;
		return dataGridViewImageColumn;
	}

	/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return GetType().Name;
	}
}
