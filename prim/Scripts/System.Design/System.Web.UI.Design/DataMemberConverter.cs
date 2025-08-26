using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design;

/// <summary>Provides a type converter that can retrieve a list of data members from the current component's selected data source.</summary>
public class DataMemberConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DataFieldConverter" /> class.</summary>
	public DataMemberConverter()
	{
	}

	/// <summary>Gets a value indicating whether the converter can convert an object of the specified source type to the native type of the converter.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information.</param>
	/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from.</param>
	/// <returns>
	///   <see langword="true" /> if the converter can perform the conversion; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		return sourceType == typeof(string);
	}

	/// <summary>Converts the specified object to the native type of the converter.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that can be used to support localization features.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the specified object after conversion.</returns>
	/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value == null)
		{
			return string.Empty;
		}
		if (value.GetType() == typeof(string))
		{
			return (string)value;
		}
		throw GetConvertFromException(value);
	}

	/// <summary>Gets the data members present within the selected data source, if information about them is available.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object indicating the component or control to get values for.</param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> listing the data members of the data source selected for the component.</returns>
	[System.MonoTODO]
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a value indicating whether the collection of standard values returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is a list of all possible values.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list of all possible values that are valid; <see langword="false" /> if other values are possible.  
	/// As implemented in this class, this method always returns <see langword="false" />.</returns>
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return false;
	}

	/// <summary>Gets a value indicating whether the converter supports a standard set of values that can be picked from a list.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information.</param>
	/// <returns>
	///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, <see langword="false" />. This implementation always returns <see langword="true" />.</returns>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return context.Instance is IComponent;
	}
}
