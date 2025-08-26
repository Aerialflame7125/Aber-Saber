using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Provides a type converter for a property representing the field name of a bound column field in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class DataColumnSelectionConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DataColumnSelectionConverter" /> class.</summary>
	public DataColumnSelectionConverter()
	{
	}

	/// <summary>Indicates whether the specified source type can be converted to the type of the associated control property.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> implementation that can be used to gain additional context information.</param>
	/// <param name="sourceType">The type to convert from.</param>
	/// <returns>
	///   <see langword="true" /> if the converter can perform the conversion; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Converts the specified object to the type of the associated control property.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> implementation that can be used to gain additional context information.</param>
	/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> object that can be used to support localization features.</param>
	/// <param name="value">The object to convert.</param>
	/// <returns>An <see cref="T:System.Object" /> instance that represents the converted object.</returns>
	/// <exception cref="T:System.NotSupportedException">The conversion could not be performed.</exception>
	[System.MonoTODO]
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a list of available values that can be assigned to the associated control property.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> implementation that can be used to gain additional context information.</param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> instance containing available values for assignment to the associated control property.</returns>
	[System.MonoTODO]
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Indicates whether this converter returns a list containing all possible values that can be assigned to the associated control property.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> implementation that can be used to gain additional context information.</param>
	/// <returns>
	///   <see langword="true" /> if this converter returns a list containing all possible values that can be assigned to the associated control property; otherwise <see langword="false" />.</returns>
	[System.MonoTODO]
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Indicates whether this converter returns a set of available values for assignment to a control property, within the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> implementation that can be used to gain additional context information.</param>
	/// <returns>
	///   <see langword="true" /> if this converter returns a standard set of available values for assignment to the associated control property; otherwise <see langword="false" />.</returns>
	[System.MonoTODO]
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}
}
