using System.ComponentModel;
using System.Globalization;

namespace System.Web.Services.Configuration;

internal class TypeTypeConverter : TypeAndNameConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string)
		{
			return ((TypeAndName)base.ConvertFrom(context, culture, value)).type;
		}
		return base.ConvertFrom(context, culture, value);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			TypeAndName value2 = new TypeAndName((Type)value);
			return base.ConvertTo(context, culture, (object)value2, destinationType);
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
