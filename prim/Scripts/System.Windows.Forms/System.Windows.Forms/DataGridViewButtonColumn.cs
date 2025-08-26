using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewButtonCell" /> objects.</summary>
/// <filterpriority>2</filterpriority>
[ToolboxBitmap("")]
public class DataGridViewButtonColumn : DataGridViewColumn
{
	private FlatStyle flatStyle;

	private string text;

	/// <summary>Gets or sets the template used to create new cells.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after.</returns>
	/// <exception cref="T:System.InvalidCastException">The specified value when setting this property could not be cast to a <see cref="T:System.Windows.Forms.DataGridViewButtonCell" />. </exception>
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
			base.CellTemplate = value as DataGridViewButtonCell;
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

	/// <summary>Gets or sets the flat-style appearance of the button cells in the column.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.FlatStyle" /> value indicating the appearance of the buttons in the column. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is null. </exception>
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

	/// <summary>Gets or sets the default text displayed on the button cell.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the text. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">When setting this property, the value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(null)]
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			text = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> property value is displayed as the button text for cells in this column.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.Text" /> property value is displayed on buttons in the column; false if the <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValue" /> property value of each cell is displayed on its button. The default is false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is null.</exception>
	[DefaultValue(false)]
	public bool UseColumnTextForButtonValue
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null when getting this property.");
			}
			return (base.CellTemplate as DataGridViewButtonCell).UseColumnTextForButtonValue;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null when setting this property.");
			}
			(base.CellTemplate as DataGridViewButtonCell).UseColumnTextForButtonValue = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewButtonColumn" /> class to the default state.</summary>
	public DataGridViewButtonColumn()
	{
		base.CellTemplate = new DataGridViewButtonCell();
		flatStyle = FlatStyle.Standard;
		text = string.Empty;
	}

	/// <summary>Creates an exact copy of this column.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewButtonColumn" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewButtonColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override object Clone()
	{
		DataGridViewButtonColumn dataGridViewButtonColumn = (DataGridViewButtonColumn)base.Clone();
		dataGridViewButtonColumn.flatStyle = flatStyle;
		dataGridViewButtonColumn.text = text;
		return dataGridViewButtonColumn;
	}

	/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"DataGridViewButtonColumn {{ Name={base.Name}, Index={base.Index} }}";
	}
}
