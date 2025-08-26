using System.ComponentModel;
using System.Globalization;

namespace System.Web.Services.Configuration;

internal class TypeAndNameConverter : TypeConverter
{
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string)
		{
			return new TypeAndName((string)value);
		}
		return base.ConvertFrom(context, culture, value);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			TypeAndName typeAndName = (TypeAndName)value;
			if (typeAndName.name != null)
			{
				return typeAndName.name;
			}
			return typeAndName.type.AssemblyQualifiedName;
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
