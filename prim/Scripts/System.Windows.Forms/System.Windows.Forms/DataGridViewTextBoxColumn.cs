using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" /> cells.</summary>
/// <filterpriority>2</filterpriority>
[ToolboxBitmap("")]
public class DataGridViewTextBoxColumn : DataGridViewColumn
{
	private int maxInputLength;

	/// <summary>Gets or sets the template used to model cell appearance.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after.</returns>
	/// <exception cref="T:System.InvalidCastException">The set type is not compatible with type <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" />. </exception>
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
			base.CellTemplate = value as DataGridViewTextBoxCell;
		}
	}

	/// <summary>Gets or sets the maximum number of characters that can be entered into the text box.</summary>
	/// <returns>The maximum number of characters that can be entered into the text box; the default value is 32767.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewTextBoxColumn.CellTemplate" /> property is null.</exception>
	[DefaultValue(32767)]
	public int MaxInputLength
	{
		get
		{
			return maxInputLength;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException("Value is less than 0.");
			}
			maxInputLength = value;
		}
	}

	/// <summary>Gets or sets the sort mode for the column.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewColumnSortMode" /> that specifies the criteria used to order the rows based on the cell values in a column.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	[DefaultValue(DataGridViewColumnSortMode.Automatic)]
	public new DataGridViewColumnSortMode SortMode
	{
		get
		{
			return base.SortMode;
		}
		set
		{
			base.SortMode = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxColumn" /> class to the default state.</summary>
	public DataGridViewTextBoxColumn()
	{
		base.CellTemplate = new DataGridViewTextBoxCell();
		maxInputLength = 32767;
		base.SortMode = DataGridViewColumnSortMode.Automatic;
	}

	/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"DataGridViewTextBoxColumn {{ Name={base.Name}, Index={base.Index} }}";
	}
}
