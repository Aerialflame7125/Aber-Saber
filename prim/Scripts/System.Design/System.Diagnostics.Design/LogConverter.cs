using System.ComponentModel;
using System.Globalization;

namespace System.Diagnostics.Design;

/// <summary>Provides the type converter for the <see cref="P:System.Diagnostics.EventLog.Log" /> property.</summary>
public class LogConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Design.LogConverter" /> class for the given type.</summary>
	public LogConverter()
	{
	}

	/// <summary>Indicates whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="sourceType">A T:System.Type  that represents the type you want to convert from.</param>
	/// <returns>
	///   <see langword="true" /> if the conversion can be performed; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>Converts the given object to a string, using the specified context and culture information.</summary>
	/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext  that provides a format context.</param>
	/// <param name="culture">The T:System.Globalization.CultureInfo  to use as the current culture.</param>
	/// <param name="value">The T:System.Object  to convert</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value is string)
		{
			return ((string)value).Trim();
		}
		return base.ConvertFrom(context, culture, value);
	}

	/// <summary>Gets a collection of standard values for the data type this validator is designed for.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or <see langword="null" /> if the data type does not support a standard set of values.</returns>
	[System.MonoTODO]
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Indicates whether this object supports a standard set of values that can be picked from a list using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>
	///   <see langword="true" /> because <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports. This method never returns <see langword="false" />.</returns>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return true;
	}
}
