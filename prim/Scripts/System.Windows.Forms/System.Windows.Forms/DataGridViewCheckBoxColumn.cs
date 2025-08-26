using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

/// <summary>Hosts a collection of <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> objects.</summary>
/// <filterpriority>2</filterpriority>
[ToolboxBitmap("")]
public class DataGridViewCheckBoxColumn : DataGridViewColumn
{
	/// <summary>Gets or sets the template used to create new cells.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCell" /> that all other cells in the column are modeled after. The default value is a new <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> instance.</returns>
	/// <exception cref="T:System.InvalidCastException">The property is set to a value that is not of type <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" />. </exception>
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
			base.CellTemplate = value as DataGridViewCheckBoxCell;
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

	/// <summary>Gets or sets the underlying value corresponding to a cell value of false, which appears as an unchecked box.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing a value that the cells in this column will treat as a false value. The default is null.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(StringConverter))]
	[DefaultValue(null)]
	public object FalseValue
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewCheckBoxCell).FalseValue;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewCheckBoxCell).FalseValue = value;
		}
	}

	/// <summary>Gets or sets the flat style appearance of the check box cells.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.FlatStyle" /> value indicating the appearance of cells in the column. The default is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(FlatStyle.Standard)]
	public FlatStyle FlatStyle
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewCheckBoxCell).FlatStyle;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewCheckBoxCell).FlatStyle = value;
		}
	}

	/// <summary>Gets or sets the underlying value corresponding to an indeterminate or null cell value, which appears as a disabled checkbox.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing a value that the cells in this column will treat as an indeterminate value. The default is null.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is null. </exception>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(StringConverter))]
	[DefaultValue(null)]
	public object IndeterminateValue
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewCheckBoxCell).IndeterminateValue;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewCheckBoxCell).IndeterminateValue = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the hosted check box cells will allow three check states rather than two.</summary>
	/// <returns>true if the hosted <see cref="T:System.Windows.Forms.DataGridViewCheckBoxCell" /> objects are able to have a third, indeterminate, state; otherwise, false. The default is false.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is null.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool ThreeState
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewCheckBoxCell).ThreeState;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewCheckBoxCell).ThreeState = value;
		}
	}

	/// <summary>Gets or sets the underlying value corresponding to a cell value of true, which appears as a checked box.</summary>
	/// <returns>An <see cref="T:System.Object" /> representing a value that the cell will treat as a true value. The default is null.</returns>
	/// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCheckBoxColumn.CellTemplate" /> property is null.</exception>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(StringConverter))]
	[DefaultValue(null)]
	public object TrueValue
	{
		get
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			return (base.CellTemplate as DataGridViewCheckBoxCell).TrueValue;
		}
		set
		{
			if (base.CellTemplate == null)
			{
				throw new InvalidOperationException("CellTemplate is null.");
			}
			(base.CellTemplate as DataGridViewCheckBoxCell).TrueValue = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxColumn" /> and configures it to display check boxes with two or three states. </summary>
	/// <param name="threeState">true to display check boxes with three states; false to display check boxes with two states. </param>
	public DataGridViewCheckBoxColumn(bool threeState)
	{
		CellTemplate = new DataGridViewCheckBoxCell(threeState);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCheckBoxColumn" /> class to the default state.</summary>
	public DataGridViewCheckBoxColumn()
		: this(threeState: false)
	{
	}

	/// <returns>A <see cref="T:System.String" /> that describes the column.</returns>
	/// <filterpriority>1</filterpriority>
	public override string ToString()
	{
		return $"DataGridViewCheckBoxColumn {{ Name={base.Name}, Index={base.Index} }}";
	}
}
