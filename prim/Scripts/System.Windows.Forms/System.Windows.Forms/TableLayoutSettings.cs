using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Collects the characteristics associated with table layouts.</summary>
/// <filterpriority>2</filterpriority>
[Serializable]
[TypeConverter(typeof(TableLayoutSettingsTypeConverter))]
public sealed class TableLayoutSettings : LayoutSettings, ISerializable
{
	internal class StyleConverter : TypeConverter
	{
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			return Enum.Parse(typeof(StyleConverter), (string)value, ignoreCase: true);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || !(value is StyleConverter) || (object)destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			return ((StyleConverter)value).ToString();
		}
	}

	private TableLayoutColumnStyleCollection column_styles;

	private TableLayoutRowStyleCollection row_styles;

	private TableLayoutPanelGrowStyle grow_style;

	private int column_count;

	private int row_count;

	private Dictionary<object, int> columns;

	private Dictionary<object, int> column_spans;

	private Dictionary<object, int> rows;

	private Dictionary<object, int> row_spans;

	internal TableLayoutPanel panel;

	internal bool isSerialized;

	/// <summary>Gets or sets the maximum number of columns allowed in the table layout.</summary>
	/// <returns>The maximum number of columns allowed in the table layout. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The property value is less than 0.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(0)]
	public int ColumnCount
	{
		get
		{
			return column_count;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (column_count != value)
			{
				column_count = value;
				if (panel != null)
				{
					panel.PerformLayout(panel, "ColumnCount");
				}
			}
		}
	}

	/// <summary>Gets the collection of styles used to determine the look and feel of the table layout columns. </summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutColumnStyleCollection" /> that contains the column styles for the layout table. </returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TableLayoutColumnStyleCollection ColumnStyles => column_styles;

	/// <summary>Gets or sets a value indicating how the table layout should expand to accommodate new cells when all existing cells are occupied.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.TableLayoutPanelGrowStyle" /> values. The default is <see cref="F:System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows" />.</returns>
	/// <exception cref="T:System.ArgumentException">The property value is not valid for the enumeration type.</exception>
	[DefaultValue(TableLayoutPanelGrowStyle.AddRows)]
	public TableLayoutPanelGrowStyle GrowStyle
	{
		get
		{
			return grow_style;
		}
		set
		{
			if (!Enum.IsDefined(typeof(TableLayoutPanelGrowStyle), value))
			{
				throw new ArgumentException();
			}
			if (grow_style != value)
			{
				grow_style = value;
				if (panel != null)
				{
					panel.PerformLayout(panel, "GrowStyle");
				}
			}
		}
	}

	/// <summary>Gets the current table layout engine.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> currently being used. </returns>
	/// <filterpriority>1</filterpriority>
	public override LayoutEngine LayoutEngine
	{
		get
		{
			if (panel != null)
			{
				return panel.LayoutEngine;
			}
			return base.LayoutEngine;
		}
	}

	/// <summary>Gets or sets the maximum number of rows allowed in the table layout.</summary>
	/// <returns>The maximum number of rows allowed in the table layout. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The property value is less than 0.</exception>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(0)]
	public int RowCount
	{
		get
		{
			return row_count;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (row_count != value)
			{
				row_count = value;
				if (panel != null)
				{
					panel.PerformLayout();
				}
			}
		}
	}

	/// <summary>Gets the collection of styles used to determine the look and feel of the table layout rows.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutRowStyleCollection" /> that contains the row styles for the layout table.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public TableLayoutRowStyleCollection RowStyles => row_styles;

	internal TableLayoutSettings(TableLayoutPanel panel)
	{
		column_styles = new TableLayoutColumnStyleCollection(panel);
		row_styles = new TableLayoutRowStyleCollection(panel);
		grow_style = TableLayoutPanelGrowStyle.AddRows;
		column_count = 0;
		row_count = 0;
		columns = new Dictionary<object, int>();
		column_spans = new Dictionary<object, int>();
		rows = new Dictionary<object, int>();
		row_spans = new Dictionary<object, int>();
		this.panel = panel;
	}

	private TableLayoutSettings(SerializationInfo serializationInfo, StreamingContext context)
	{
		TypeConverter converter = TypeDescriptor.GetConverter(this);
		string @string = serializationInfo.GetString("SerializedString");
		if (!string.IsNullOrEmpty(@string) && converter != null)
		{
			TableLayoutSettings tableLayoutSettings = converter.ConvertFromInvariantString(@string) as TableLayoutSettings;
			column_styles = tableLayoutSettings.column_styles;
			row_styles = tableLayoutSettings.row_styles;
			grow_style = tableLayoutSettings.grow_style;
			column_count = tableLayoutSettings.column_count;
			row_count = tableLayoutSettings.row_count;
			columns = tableLayoutSettings.columns;
			column_spans = tableLayoutSettings.column_spans;
			rows = tableLayoutSettings.rows;
			row_spans = tableLayoutSettings.row_spans;
			panel = tableLayoutSettings.panel;
			isSerialized = true;
		}
	}

	/// <summary>For a description of this member, see <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" />.</summary>
	/// <param name="si">The object to be populated with serialization information. </param>
	/// <param name="context">The destination context of the serialization.</param>
	void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
	{
		TableLayoutSettingsTypeConverter tableLayoutSettingsTypeConverter = new TableLayoutSettingsTypeConverter();
		string value = tableLayoutSettingsTypeConverter.ConvertToInvariantString(this);
		si.AddValue("SerializedString", value);
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the cell position.</returns>
	/// <param name="control">A control contained within a cell.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	[DefaultValue(-1)]
	public TableLayoutPanelCellPosition GetCellPosition(object control)
	{
		if (control == null)
		{
			throw new ArgumentNullException();
		}
		if (!columns.TryGetValue(control, out var value))
		{
			value = -1;
		}
		if (!rows.TryGetValue(control, out var value2))
		{
			value2 = -1;
		}
		return new TableLayoutPanelCellPosition(value, value2);
	}

	/// <summary>Gets the column position of the specified child control.</summary>
	/// <returns>The column position of the specified child control.</returns>
	/// <param name="control">A control contained within a cell.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	[DefaultValue(-1)]
	public int GetColumn(object control)
	{
		if (control == null)
		{
			throw new ArgumentNullException();
		}
		if (columns.TryGetValue(control, out var value))
		{
			return value;
		}
		return -1;
	}

	/// <summary>Gets the number of columns that the cell containing the child control spans.</summary>
	/// <returns>The number of columns that the cell containing the child control spans.</returns>
	/// <param name="control">A control contained within a cell.</param>
	public int GetColumnSpan(object control)
	{
		if (control == null)
		{
			throw new ArgumentNullException();
		}
		if (column_spans.TryGetValue(control, out var value))
		{
			return value;
		}
		return 1;
	}

	/// <summary>Gets the row position of the specified child control.</summary>
	/// <returns>The row position of the specified child control.</returns>
	/// <param name="control">A control contained within a cell.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	[DefaultValue(-1)]
	public int GetRow(object control)
	{
		if (control == null)
		{
			throw new ArgumentNullException();
		}
		if (rows.TryGetValue(control, out var value))
		{
			return value;
		}
		return -1;
	}

	/// <summary>Gets the number of rows that the cell containing the child control spans.</summary>
	/// <returns>The number of rows that the cell containing the child control spans.</returns>
	/// <param name="control">A control contained within a cell.</param>
	public int GetRowSpan(object control)
	{
		if (control == null)
		{
			throw new ArgumentNullException();
		}
		if (row_spans.TryGetValue(control, out var value))
		{
			return value;
		}
		return 1;
	}

	/// <summary>Sets the <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" /> that represents the row and the column of the cell.</summary>
	/// <param name="control">A control contained within a cell.</param>
	/// <param name="cellPosition">A <see cref="T:System.Windows.Forms.TableLayoutPanelCellPosition" />  that represents the cell position.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	[DefaultValue(-1)]
	public void SetCellPosition(object control, TableLayoutPanelCellPosition cellPosition)
	{
		if (control == null)
		{
			throw new ArgumentNullException();
		}
		columns[control] = cellPosition.Column;
		rows[control] = cellPosition.Row;
		if (panel != null)
		{
			panel.PerformLayout();
		}
	}

	/// <summary>Sets the column position for the specified child control.</summary>
	/// <param name="control">A control contained within a cell.</param>
	/// <param name="column">The column position for the specified child control.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="column" /> is less than -1.</exception>
	public void SetColumn(object control, int column)
	{
		if (control == null)
		{
			throw new ArgumentNullException();
		}
		if (column < -1)
		{
			throw new ArgumentException();
		}
		columns[control] = column;
		if (panel != null)
		{
			panel.PerformLayout();
		}
	}

	/// <summary>Sets the number of columns that the cell containing the child control spans.</summary>
	/// <param name="control">A control contained within a cell.</param>
	/// <param name="value">The number of columns that the cell containing the child control spans.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is less than 1.</exception>
	public void SetColumnSpan(object control, int value)
	{
		if (control == null)
		{
			throw new ArgumentNullException();
		}
		if (value < -1)
		{
			throw new ArgumentException();
		}
		column_spans[control] = value;
		if (panel != null)
		{
			panel.PerformLayout();
		}
	}

	/// <summary>Sets the row position of the specified child control.</summary>
	/// <param name="control">A control contained within a cell.</param>
	/// <param name="row">The row position of the specified child control.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="row" /> is less than -1.</exception>
	public void SetRow(object control, int row)
	{
		if (control == null)
		{
			throw new ArgumentNullException();
		}
		if (row < -1)
		{
			throw new ArgumentException();
		}
		rows[control] = row;
		if (panel != null)
		{
			panel.PerformLayout();
		}
	}

	/// <summary>Sets the number of rows that the cell containing the child control spans.</summary>
	/// <param name="control">A control contained within a cell.</param>
	/// <param name="value">The number of rows that the cell containing the child control spans.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="control" /> is null.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="value" /> is less than 1.</exception>
	public void SetRowSpan(object control, int value)
	{
		if (control == null)
		{
			throw new ArgumentNullException();
		}
		if (value < -1)
		{
			throw new ArgumentException();
		}
		row_spans[control] = value;
		if (panel != null)
		{
			panel.PerformLayout();
		}
	}

	internal List<ControlInfo> GetControls()
	{
		List<ControlInfo> list = new List<ControlInfo>();
		foreach (KeyValuePair<object, int> column in columns)
		{
			ControlInfo item = default(ControlInfo);
			item.Control = column.Key;
			item.Col = GetColumn(column.Key);
			item.ColSpan = GetColumnSpan(column.Key);
			item.Row = GetRow(column.Key);
			item.RowSpan = GetRowSpan(column.Key);
			list.Add(item);
		}
		return list;
	}
}
