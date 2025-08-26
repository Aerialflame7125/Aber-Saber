using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls;

internal class HorizontalAlignConverter : EnumConverter
{
	public HorizontalAlignConverter()
		: base(typeof(HorizontalAlign))
	{
	}

	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		return base.CanConvertFrom(context, sourceType);
	}

	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		return base.ConvertFrom(context, culture, value);
	}

	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		return base.CanConvertTo(context, destinationType);
	}

	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
