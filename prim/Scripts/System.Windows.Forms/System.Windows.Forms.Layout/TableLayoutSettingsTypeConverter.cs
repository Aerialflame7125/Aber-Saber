using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml;

namespace System.Windows.Forms.Layout;

/// <summary>Provides a unified way of converting types of values to other types, as well as for accessing standard values and subproperties.</summary>
public class TableLayoutSettingsTypeConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Layout.TableLayoutSettingsTypeConverter" /> class.</summary>
	public TableLayoutSettingsTypeConverter()
	{
	}

	/// <summary>Returns a value indicating whether this converter can convert an object to the given destination type by using the context.</summary>
	/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if ((object)destinationType == typeof(string))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	/// <summary>Determines whether this converter can convert an object in the given source type to the native type of this converter.</summary>
	/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if ((object)sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>Converts the given value object to the specified type by using the specified context and culture information.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="destinationType" /> is null.</exception>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (!(value is TableLayoutSettings) || (object)destinationType != typeof(string))
		{
			return base.ConvertTo(context, culture, value, destinationType);
		}
		TableLayoutSettings tableLayoutSettings = value as TableLayoutSettings;
		StringWriter stringWriter = new StringWriter();
		XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
		xmlTextWriter.WriteStartDocument();
		List<ControlInfo> controls = tableLayoutSettings.GetControls();
		xmlTextWriter.WriteStartElement("TableLayoutSettings");
		xmlTextWriter.WriteStartElement("Controls");
		foreach (ControlInfo item in controls)
		{
			xmlTextWriter.WriteStartElement("Control");
			xmlTextWriter.WriteAttributeString("Name", item.Control.ToString());
			xmlTextWriter.WriteAttributeString("Row", item.Row.ToString());
			xmlTextWriter.WriteAttributeString("RowSpan", item.RowSpan.ToString());
			xmlTextWriter.WriteAttributeString("Column", item.Col.ToString());
			xmlTextWriter.WriteAttributeString("ColumnSpan", item.ColSpan.ToString());
			xmlTextWriter.WriteEndElement();
		}
		xmlTextWriter.WriteEndElement();
		List<string> list = new List<string>();
		foreach (ColumnStyle item2 in (IEnumerable)tableLayoutSettings.ColumnStyles)
		{
			list.Add(item2.SizeType.ToString());
			list.Add(item2.Width.ToString(CultureInfo.InvariantCulture));
		}
		xmlTextWriter.WriteStartElement("Columns");
		xmlTextWriter.WriteAttributeString("Styles", string.Join(",", list.ToArray()));
		xmlTextWriter.WriteEndElement();
		list.Clear();
		foreach (RowStyle item3 in (IEnumerable)tableLayoutSettings.RowStyles)
		{
			list.Add(item3.SizeType.ToString());
			list.Add(item3.Height.ToString(CultureInfo.InvariantCulture));
		}
		xmlTextWriter.WriteStartElement("Rows");
		xmlTextWriter.WriteAttributeString("Styles", string.Join(",", list.ToArray()));
		xmlTextWriter.WriteEndElement();
		xmlTextWriter.WriteEndElement();
		xmlTextWriter.WriteEndDocument();
		xmlTextWriter.Close();
		return stringWriter.ToString();
	}

	/// <summary>Converts the given object to the type of this converter by using the specified context and culture information.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (!(value is string))
		{
			return base.ConvertFrom(context, culture, value);
		}
		XmlDocument xmlDocument = new XmlDocument();
		xmlDocument.LoadXml(value as string);
		TableLayoutSettings tableLayoutSettings = new TableLayoutSettings(null);
		int rowCount = ParseControl(xmlDocument, tableLayoutSettings);
		ParseColumnStyle(xmlDocument, tableLayoutSettings);
		ParseRowStyle(xmlDocument, tableLayoutSettings);
		tableLayoutSettings.RowCount = rowCount;
		return tableLayoutSettings;
	}

	private int ParseControl(XmlDocument xmldoc, TableLayoutSettings settings)
	{
		int num = 0;
		foreach (XmlNode item in xmldoc.GetElementsByTagName("Control"))
		{
			if (item.Attributes["Name"] != null && !string.IsNullOrEmpty(item.Attributes["Name"].Value))
			{
				if (item.Attributes["Row"] != null)
				{
					settings.SetRow(item.Attributes["Name"].Value, GetValue(item.Attributes["Row"].Value));
					num++;
				}
				if (item.Attributes["RowSpan"] != null)
				{
					settings.SetRowSpan(item.Attributes["Name"].Value, GetValue(item.Attributes["RowSpan"].Value));
				}
				if (item.Attributes["Column"] != null)
				{
					settings.SetColumn(item.Attributes["Name"].Value, GetValue(item.Attributes["Column"].Value));
				}
				if (item.Attributes["ColumnSpan"] != null)
				{
					settings.SetColumnSpan(item.Attributes["Name"].Value, GetValue(item.Attributes["ColumnSpan"].Value));
				}
			}
		}
		return num;
	}

	private void ParseColumnStyle(XmlDocument xmldoc, TableLayoutSettings settings)
	{
		foreach (XmlNode item in xmldoc.GetElementsByTagName("Columns"))
		{
			if (item.Attributes["Styles"] == null)
			{
				continue;
			}
			string value = item.Attributes["Styles"].Value;
			if (!string.IsNullOrEmpty(value))
			{
				string[] array = BuggySplit(value);
				for (int i = 0; i < array.Length; i += 2)
				{
					float result = 0f;
					SizeType sizeType = (SizeType)(int)Enum.Parse(typeof(SizeType), array[i]);
					float.TryParse(array[i + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out result);
					settings.ColumnStyles.Add(new ColumnStyle(sizeType, result));
				}
			}
		}
	}

	private void ParseRowStyle(XmlDocument xmldoc, TableLayoutSettings settings)
	{
		foreach (XmlNode item in xmldoc.GetElementsByTagName("Rows"))
		{
			if (item.Attributes["Styles"] == null)
			{
				continue;
			}
			string value = item.Attributes["Styles"].Value;
			if (!string.IsNullOrEmpty(value))
			{
				string[] array = BuggySplit(value);
				for (int i = 0; i < array.Length; i += 2)
				{
					float result = 0f;
					SizeType sizeType = (SizeType)(int)Enum.Parse(typeof(SizeType), array[i]);
					float.TryParse(array[i + 1], NumberStyles.Float, CultureInfo.InvariantCulture, out result);
					settings.RowStyles.Add(new RowStyle(sizeType, result));
				}
			}
		}
	}

	private int GetValue(string attValue)
	{
		int result = -1;
		if (!string.IsNullOrEmpty(attValue))
		{
			int.TryParse(attValue, out result);
		}
		return result;
	}

	private string[] BuggySplit(string s)
	{
		List<string> list = new List<string>();
		string[] array = s.Split(',');
		for (int i = 0; i < array.Length; i++)
		{
			switch (array[i].ToLowerInvariant())
			{
			case "autosize":
			case "absolute":
			case "percent":
				list.Add(array[i]);
				continue;
			}
			if (i + 1 < array.Length)
			{
				if (float.TryParse(array[i + 1], out var _))
				{
					list.Add($"{array[i]}.{array[i + 1]}");
					i++;
				}
				else
				{
					list.Add(array[i]);
				}
			}
			else
			{
				list.Add(array[i]);
			}
		}
		return list.ToArray();
	}
}
