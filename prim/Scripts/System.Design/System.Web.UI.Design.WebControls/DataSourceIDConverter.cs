using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design.WebControls;

/// <summary>Creates a user-selectable list of data source names.</summary>
public class DataSourceIDConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.DataSourceIDConverter" /> class.</summary>
	public DataSourceIDConverter()
	{
	}

	/// <summary>Gets a value indicating whether this converter can convert an object in the specified source type to the native type of the converter.</summary>
	/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
	/// <param name="sourceType">The <see cref="T:System.Type" /> of the object for which conversion is being requested.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="sourceType" /> is a string; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		return sourceType == typeof(string);
	}

	/// <summary>Converts the specified object to the native type of the converter.</summary>
	/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that specifies the culture of the <paramref name="value" /> parameter.</param>
	/// <param name="value">The object to convert.</param>
	/// <returns>The <paramref name="value" /> parameter is returned as a string.</returns>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="value" /> is other than a string or <see langword="null" />.</exception>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value == null)
		{
			return string.Empty;
		}
		if (value is string)
		{
			return (string)value;
		}
		throw GetConvertFromException(value);
	}

	/// <summary>Returns a list of the available data source names.</summary>
	/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> containing the names of the controls that implement the <see cref="T:System.Web.UI.IDataSource" /> interface and are available for use in the given context.</returns>
	[System.MonoTODO]
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a value indicating whether the returned data source names are an exclusive list of possible values.</summary>
	/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
	/// <returns>Always <see langword="false" />.</returns>
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return false;
	}

	/// <summary>Gets a value indicating whether this object returns a standard set of data source names that can be picked from a list.</summary>
	/// <param name="context">An object implementing the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about a context to a type converter so that the type converter can perform a conversion.</param>
	/// <returns>Always <see langword="true" />.</returns>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return true;
	}

	/// <summary>Gets a value that indicates whether the specified component is a valid data source.</summary>
	/// <param name="component">An object that implements the <see cref="T:System.ComponentModel.IComponent" /> interface.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="component" /> is a valid data source; otherwise, <see langword="false" />.</returns>
	protected virtual bool IsValidDataSource(IComponent component)
	{
		if (component == null)
		{
			return false;
		}
		return component is IDataSource;
	}
}
