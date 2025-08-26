using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI;

internal class MinimizableAttributeTypeConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (value is string && destinationType == typeof(bool))
		{
			return value != null;
		}
		if (value is bool && destinationType == typeof(string))
		{
			return ((bool)value).ToString(culture);
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		string arg;
		if (value != null)
		{
			Type type = value.GetType();
			if (type == typeof(string))
			{
				string text = value as string;
				if (string.IsNullOrEmpty(text) || string.Compare(text, "false", StringComparison.OrdinalIgnoreCase) == 0)
				{
					return false;
				}
				return true;
			}
			arg = type.FullName;
		}
		else
		{
			arg = "null";
		}
		throw new NotSupportedException($"MinimizableAttributeTypeConverter cannot convert from {arg}");
	}
}
