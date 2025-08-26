using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Converts from a <see cref="T:System.Web.UI.WebControls.Unit" /> object to an object of another data type and from another type to a <see cref="T:System.Web.UI.WebControls.Unit" /> object.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class UnitConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.UnitConverter" /> class.</summary>
	public UnitConverter()
	{
	}

	/// <summary>Returns a value indicating whether the unit converter can convert from the specified source type.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> instance that specifies the context of the object to convert. </param>
	/// <param name="sourceType">The type of the source. </param>
	/// <returns>
	///     <see langword="true" /> if the source type can be converted from; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>Returns a value indicating whether the converter can convert a <see cref="T:System.Web.UI.WebControls.Unit" /> object to the specified type.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context of the object to convert.</param>
	/// <param name="destinationType">The <see cref="T:System.Type" /> that represents the data type to convert to.</param>
	/// <returns>
	///     <see langword="true" /> if the converter supports converting a <see cref="T:System.Web.UI.WebControls.Unit" /> object to the destination type; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	/// <summary>Performs type conversion to the specified destination type given the specified context, object and argument list.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> instance that indicates the context of the object to convert. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass <see langword="null" /> for this parameter. </param>
	/// <param name="value">The object to convert. </param>
	/// <param name="destinationType">The type to convert to. </param>
	/// <returns>The object resulting from conversion.</returns>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (value is Unit && destinationType == typeof(string))
		{
			return ((Unit)value).ToString(culture);
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>Performs type conversion from the specified context, object, and argument list.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> instance that indicates the context of the object to convert. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass <see langword="null" /> for this parameter. </param>
	/// <param name="value">The object to convert. </param>
	/// <returns>The object resulting from conversion.</returns>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value == null)
		{
			return null;
		}
		if (value.GetType() == typeof(string))
		{
			return new Unit((string)value, culture);
		}
		return base.ConvertFrom(context, culture, value);
	}
}
