using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Represents the formatting and style information applied to individual cells within a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
[Editor("System.Windows.Forms.Design.DataGridViewCellStyleEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[TypeConverter(typeof(DataGridViewCellStyleConverter))]
public class DataGridViewCellStyle : ICloneable
{
	private DataGridViewContentAlignment alignment;

	private Color backColor;

	private object dataSourceNullValue;

	private Font font;

	private Color foreColor;

	private string format;

	private IFormatProvider formatProvider;

	private object nullValue;

	private Padding padding;

	private Color selectionBackColor;

	private Color selectionForeColor;

	private object tag;

	private DataGridViewTriState wrapMode;

	/// <summary>Gets or sets a value indicating the position of the cell content within a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewContentAlignment" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewContentAlignment.NotSet" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.DataGridViewContentAlignment" /> value. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(DataGridViewContentAlignment.NotSet)]
	public DataGridViewContentAlignment Alignment
	{
		get
		{
			return alignment;
		}
		set
		{
			if (!Enum.IsDefined(typeof(DataGridViewContentAlignment), value))
			{
				throw new InvalidEnumArgumentException("Value is not valid DataGridViewContentAlignment.");
			}
			if (alignment != value)
			{
				alignment = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets the background color of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of a cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public Color BackColor
	{
		get
		{
			return backColor;
		}
		set
		{
			if (backColor != value)
			{
				backColor = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets the value saved to the data source when the user enters a null value into a cell.</summary>
	/// <returns>The value saved to the data source when the user specifies a null cell value. The default is <see cref="F:System.DBNull.Value" />.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public object DataSourceNullValue
	{
		get
		{
			return dataSourceNullValue;
		}
		set
		{
			if (dataSourceNullValue != value)
			{
				dataSourceNullValue = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets the font applied to the textual content of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> applied to the cell text. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	public Font Font
	{
		get
		{
			return font;
		}
		set
		{
			if (font != value)
			{
				font = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets the foreground color of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of a cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Color ForeColor
	{
		get
		{
			return foreColor;
		}
		set
		{
			if (foreColor != value)
			{
				foreColor = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets the format string applied to the textual content of a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
	/// <returns>A string that indicates the format of the cell value. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <filterpriority>1</filterpriority>
	[Editor("System.Windows.Forms.Design.FormatStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultValue("")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public string Format
	{
		get
		{
			return format;
		}
		set
		{
			if (format != value)
			{
				format = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets the object used to provide culture-specific formatting of <see cref="T:System.Windows.Forms.DataGridView" /> cell values.</summary>
	/// <returns>An <see cref="T:System.IFormatProvider" /> used for cell formatting. The default is <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" />.</returns>
	/// <filterpriority>1</filterpriority>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public IFormatProvider FormatProvider
	{
		get
		{
			if (formatProvider == null)
			{
				return CultureInfo.CurrentCulture;
			}
			return formatProvider;
		}
		set
		{
			if (formatProvider != value)
			{
				formatProvider = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.DataSourceNullValue" /> property has been set.</summary>
	/// <returns>true if the value of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.DataSourceNullValue" /> property is the default value; otherwise, false.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public bool IsDataSourceNullValueDefault => dataSourceNullValue != null;

	/// <summary>Gets a value that indicates whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.FormatProvider" /> property has been set.</summary>
	/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.FormatProvider" /> property is the default value; otherwise, false.</returns>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	[Browsable(false)]
	public bool IsFormatProviderDefault => formatProvider == null;

	/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.NullValue" /> property has been set.</summary>
	/// <returns>true if the value of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.NullValue" /> property is the default value; otherwise, false.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public bool IsNullValueDefault
	{
		get
		{
			if (nullValue is string)
			{
				return (string)nullValue == string.Empty;
			}
			return false;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> cell display value corresponding to a cell value of <see cref="F:System.DBNull.Value" /> or null.</summary>
	/// <returns>The object used to indicate a null value in a cell. The default is <see cref="F:System.String.Empty" />.</returns>
	/// <filterpriority>1</filterpriority>
	[TypeConverter(typeof(StringConverter))]
	[DefaultValue("")]
	public object NullValue
	{
		get
		{
			return nullValue;
		}
		set
		{
			if (nullValue != value)
			{
				nullValue = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets the space between the edge of a <see cref="T:System.Windows.Forms.DataGridViewCell" /> and its content.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the space between the edge of a <see cref="T:System.Windows.Forms.DataGridViewCell" /> and its content.</returns>
	/// <filterpriority>1</filterpriority>
	public Padding Padding
	{
		get
		{
			return padding;
		}
		set
		{
			if (padding != value)
			{
				padding = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets the background color used by a <see cref="T:System.Windows.Forms.DataGridView" /> cell when it is selected.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of a selected cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Color SelectionBackColor
	{
		get
		{
			return selectionBackColor;
		}
		set
		{
			if (selectionBackColor != value)
			{
				selectionBackColor = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets the foreground color used by a <see cref="T:System.Windows.Forms.DataGridView" /> cell when it is selected.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of a selected cell. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Color SelectionForeColor
	{
		get
		{
			return selectionForeColor;
		}
		set
		{
			if (selectionForeColor != value)
			{
				selectionForeColor = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets an object that contains additional data related to the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
	/// <returns>An object that contains additional data. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public object Tag
	{
		get
		{
			return tag;
		}
		set
		{
			if (tag != value)
			{
				tag = value;
				OnStyleChanged();
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether textual content in a <see cref="T:System.Windows.Forms.DataGridView" /> cell is wrapped to subsequent lines or truncated when it is too long to fit on a single line.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.DataGridViewTriState" /> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.NotSet" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.DataGridViewTriState" /> value. </exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(DataGridViewTriState.NotSet)]
	public DataGridViewTriState WrapMode
	{
		get
		{
			return wrapMode;
		}
		set
		{
			if (!Enum.IsDefined(typeof(DataGridViewTriState), value))
			{
				throw new InvalidEnumArgumentException("Value is not valid DataGridViewTriState.");
			}
			if (wrapMode != value)
			{
				wrapMode = value;
				OnStyleChanged();
			}
		}
	}

	internal event EventHandler StyleChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> class using default property values.</summary>
	public DataGridViewCellStyle()
	{
		alignment = DataGridViewContentAlignment.NotSet;
		backColor = Color.Empty;
		dataSourceNullValue = DBNull.Value;
		font = null;
		foreColor = Color.Empty;
		format = string.Empty;
		nullValue = string.Empty;
		padding = Padding.Empty;
		selectionBackColor = Color.Empty;
		selectionForeColor = Color.Empty;
		tag = null;
		wrapMode = DataGridViewTriState.NotSet;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> class using the property values of the specified <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
	/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> used as a template to provide initial property values. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewCellStyle" /> is null.</exception>
	public DataGridViewCellStyle(DataGridViewCellStyle dataGridViewCellStyle)
	{
		alignment = dataGridViewCellStyle.alignment;
		backColor = dataGridViewCellStyle.backColor;
		dataSourceNullValue = dataGridViewCellStyle.dataSourceNullValue;
		font = dataGridViewCellStyle.font;
		foreColor = dataGridViewCellStyle.foreColor;
		format = dataGridViewCellStyle.format;
		formatProvider = dataGridViewCellStyle.formatProvider;
		nullValue = dataGridViewCellStyle.nullValue;
		padding = dataGridViewCellStyle.padding;
		selectionBackColor = dataGridViewCellStyle.selectionBackColor;
		selectionForeColor = dataGridViewCellStyle.selectionForeColor;
		tag = dataGridViewCellStyle.tag;
		wrapMode = dataGridViewCellStyle.wrapMode;
	}

	/// <summary>Creates an exact copy of this <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents an exact copy of this cell style.</returns>
	object ICloneable.Clone()
	{
		return Clone();
	}

	/// <summary>Applies the specified <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to the current <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
	/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to apply to the current <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="dataGridViewCellStyle" /> is null.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
	///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public virtual void ApplyStyle(DataGridViewCellStyle dataGridViewCellStyle)
	{
		if (dataGridViewCellStyle.alignment != 0)
		{
			alignment = dataGridViewCellStyle.alignment;
		}
		if (dataGridViewCellStyle.backColor != Color.Empty)
		{
			backColor = dataGridViewCellStyle.backColor;
		}
		if (dataGridViewCellStyle.dataSourceNullValue != DBNull.Value)
		{
			dataSourceNullValue = dataGridViewCellStyle.dataSourceNullValue;
		}
		if (dataGridViewCellStyle.font != null)
		{
			font = dataGridViewCellStyle.font;
		}
		if (dataGridViewCellStyle.foreColor != Color.Empty)
		{
			foreColor = dataGridViewCellStyle.foreColor;
		}
		if (dataGridViewCellStyle.format != string.Empty)
		{
			format = dataGridViewCellStyle.format;
		}
		if (dataGridViewCellStyle.formatProvider != null)
		{
			formatProvider = dataGridViewCellStyle.formatProvider;
		}
		if (dataGridViewCellStyle.nullValue != null)
		{
			nullValue = dataGridViewCellStyle.nullValue;
		}
		if (dataGridViewCellStyle.padding != Padding.Empty)
		{
			padding = dataGridViewCellStyle.padding;
		}
		if (dataGridViewCellStyle.selectionBackColor != Color.Empty)
		{
			selectionBackColor = dataGridViewCellStyle.selectionBackColor;
		}
		if (dataGridViewCellStyle.selectionForeColor != Color.Empty)
		{
			selectionForeColor = dataGridViewCellStyle.selectionForeColor;
		}
		if (dataGridViewCellStyle.tag != null)
		{
			tag = dataGridViewCellStyle.tag;
		}
		if (dataGridViewCellStyle.wrapMode != 0)
		{
			wrapMode = dataGridViewCellStyle.wrapMode;
		}
	}

	/// <summary>Creates an exact copy of this <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> that represents an exact copy of this cell style.</returns>
	public virtual DataGridViewCellStyle Clone()
	{
		return new DataGridViewCellStyle(this);
	}

	/// <summary>Returns a value indicating whether this instance is equivalent to the specified object.</summary>
	/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> and has the same property values as this instance; otherwise, false.</returns>
	/// <param name="o">An object to compare with this instance, or null. </param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object o)
	{
		if (o is DataGridViewCellStyle)
		{
			DataGridViewCellStyle dataGridViewCellStyle = (DataGridViewCellStyle)o;
			return alignment == dataGridViewCellStyle.alignment && backColor == dataGridViewCellStyle.backColor && dataSourceNullValue == dataGridViewCellStyle.dataSourceNullValue && font == dataGridViewCellStyle.font && foreColor == dataGridViewCellStyle.foreColor && format == dataGridViewCellStyle.format && formatProvider == dataGridViewCellStyle.formatProvider && nullValue == dataGridViewCellStyle.nullValue && padding == dataGridViewCellStyle.padding && selectionBackColor == dataGridViewCellStyle.selectionBackColor && selectionForeColor == dataGridViewCellStyle.selectionForeColor && tag == dataGridViewCellStyle.tag && wrapMode == dataGridViewCellStyle.wrapMode;
		}
		return false;
	}

	/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>Returns a string indicating the current property settings of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</summary>
	/// <returns>A string indicating the current property settings of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />.</returns>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override string ToString()
	{
		return string.Empty;
	}

	internal void OnStyleChanged()
	{
		if (this.StyleChanged != null)
		{
			this.StyleChanged(this, EventArgs.Empty);
		}
	}

	internal StringFormat SetAlignment(StringFormat format)
	{
		switch (Alignment)
		{
		case DataGridViewContentAlignment.BottomLeft:
		case DataGridViewContentAlignment.BottomCenter:
		case DataGridViewContentAlignment.BottomRight:
			format.LineAlignment = StringAlignment.Near;
			break;
		case DataGridViewContentAlignment.MiddleLeft:
		case DataGridViewContentAlignment.MiddleCenter:
		case DataGridViewContentAlignment.MiddleRight:
			format.LineAlignment = StringAlignment.Center;
			break;
		case DataGridViewContentAlignment.TopLeft:
		case DataGridViewContentAlignment.TopCenter:
		case DataGridViewContentAlignment.TopRight:
			format.LineAlignment = StringAlignment.Far;
			break;
		}
		switch (Alignment)
		{
		case DataGridViewContentAlignment.TopCenter:
		case DataGridViewContentAlignment.MiddleCenter:
		case DataGridViewContentAlignment.BottomCenter:
			format.Alignment = StringAlignment.Center;
			break;
		case DataGridViewContentAlignment.TopLeft:
		case DataGridViewContentAlignment.MiddleLeft:
		case DataGridViewContentAlignment.BottomLeft:
			format.Alignment = StringAlignment.Near;
			break;
		case DataGridViewContentAlignment.TopRight:
		case DataGridViewContentAlignment.MiddleRight:
		case DataGridViewContentAlignment.BottomRight:
			format.Alignment = StringAlignment.Far;
			break;
		}
		return format;
	}
}
