using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls;

/// <summary>Provides a type converter to convert a string of comma-separated values to and from an array of strings.</summary>
public class StringArrayConverter : TypeConverter
{
	/// <summary>Determines whether the <see cref="T:System.Web.UI.WebControls.StringArrayConverter" /> can convert the specified source type to an array of strings.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />.</param>
	/// <param name="sourceType">The <see cref="T:System.Type" /> to convert.</param>
	/// <returns>
	///     <see langword="true" /> if the converter can perform the operation; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>Converts the specified comma-separated string into an array of strings.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object. If <see langword="null" />, the current culture is used.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
	/// <exception cref="M:System.ComponentModel.TypeConverter.GetConvertFromException(System.Object)">The conversion cannot be performed because <paramref name="value" /> is not a string.</exception>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value == null)
		{
			return null;
		}
		if (value is string)
		{
			return ((string)value).Split(',');
		}
		return base.ConvertFrom(context, culture, value);
	}

	/// <summary>Converts an array of strings into a string of values separated by commas.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object. If <see langword="null" />, the current culture is used.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <param name="destinationType">The <see cref="T:System.Type" /> to convert <paramref name="value" /> to.</param>
	/// <returns>An <see cref="T:System.Object" /> instance that represents the converted <paramref name="value" />.</returns>
	/// <exception cref="M:System.ComponentModel.TypeConverter.GetConvertToException(System.Object,System.Type)">
	///         <paramref name="destinationType" /> is not of type <see cref="T:System.String" />.</exception>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (value is string[] && destinationType == typeof(string))
		{
			return string.Join(",", (string[])value);
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.StringArrayConverter" /> class.</summary>
	public StringArrayConverter()
	{
	}
}
