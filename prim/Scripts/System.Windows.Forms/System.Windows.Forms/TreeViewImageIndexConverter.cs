using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Provides a type converter to convert data for an image index to and from one data type to another for use by the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class TreeViewImageIndexConverter : ImageIndexConverter
{
	/// <summary>Gets a value indicating null is valid in the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> collection.</summary>
	/// <returns>true if null is valid in the standard values collection; otherwise, false.</returns>
	protected override bool IncludeNoneAsStandardValue => false;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewImageIndexConverter" /> class.</summary>
	public TreeViewImageIndexConverter()
	{
	}

	/// <param name="context"></param>
	/// <param name="culture"></param>
	/// <param name="value"></param>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value != null && value is string)
		{
			string text = (string)value;
			if (text.Equals("(default)", StringComparison.InvariantCultureIgnoreCase))
			{
				return -1;
			}
			if (text.Equals("(none)", StringComparison.InvariantCultureIgnoreCase))
			{
				return -2;
			}
			return int.Parse(text);
		}
		return base.ConvertFrom(context, culture, value);
	}

	/// <param name="context"></param>
	/// <param name="culture"></param>
	/// <param name="value"></param>
	/// <param name="destinationType"></param>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if ((object)destinationType == typeof(string))
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (value is int && (int)value == -1)
			{
				return "(default)";
			}
			if (value is int && (int)value == -2)
			{
				return "(none)";
			}
			if (value is string && ((string)value).Length == 0)
			{
				return string.Empty;
			}
			return value.ToString();
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <param name="context"></param>
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		int[] values = new int[2] { -1, -2 };
		return new StandardValuesCollection(values);
	}
}
