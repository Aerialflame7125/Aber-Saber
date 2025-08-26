using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design;

/// <summary>Provides a list of valid skin IDs for a control at design time, based on the currently applicable theme.</summary>
public class SkinIDTypeConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.SkinIDTypeConverter" /> class.</summary>
	[System.MonoTODO]
	public SkinIDTypeConverter()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a value indicating whether this converter can convert a <see cref="P:System.Web.UI.Control.SkinID" /> object to a string using the provided format context and type.</summary>
	/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext that provides a format context for the control being designed.</param>
	/// <param name="sourceType">A <see cref="T:System.Type" />.</param>
	/// <returns>
	///   <see langword="true" />, if the conversion can be made; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a value indicating whether this converter can convert a <see cref="P:System.Web.UI.Control.SkinID" /> object to the specified type, using the specified context.</summary>
	/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext that provides a format context for the control being designed.</param>
	/// <param name="destType">A T:System.Type that represents the type to convert to.</param>
	/// <returns>
	///   <see langword="true" />, if a conversion can be made; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Converts the given string to a <see cref="P:System.Web.UI.Control.SkinID" /> object using the specified context and culture information.</summary>
	/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext that provides a format context that represents the control being designed.</param>
	/// <param name="culture">A T:System.Globalization.CultureInfo. If <see langword="null" /> is passed, the current culture is assumed.</param>
	/// <param name="value">The string to convert.</param>
	/// <returns>An object that can be cast as a <see cref="P:System.Web.UI.DataSourceControl.SkinID" /> object, if the conversion can be made; otherwise, <see langword="null" />.</returns>
	[System.MonoTODO]
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Converts the given <see cref="P:System.Web.UI.Control.SkinID" /> object to a string using the specified context and culture information.</summary>
	/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext that provides a format context that represents the control being designed.</param>
	/// <param name="culture">A T:System.Globalization.CultureInfo. If <see langword="null" />, the current culture is assumed.</param>
	/// <param name="value">The <see cref="P:System.Web.UI.Control.SkinID" /> object to convert.</param>
	/// <param name="destinationType">The T:System.Type to convert <paramref name="value" /> to (must be a <see cref="T:System.String" />).</param>
	/// <returns>An object that represents the converted value.</returns>
	[System.MonoTODO]
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a set of <see cref="P:System.Web.UI.Control.SkinID" /> objects that can be applied to the control that is represented by the given format context.</summary>
	/// <param name="context">An T:System.ComponentModel.ITypeDescriptorContext that provides a format context that represents the control being designed. <paramref name="context" /> or properties of <paramref name="context" /> can be <see langword="null" />.</param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a set of <see cref="P:System.Web.UI.Control.SkinID" /> objects; otherwise, <see langword="null" />, if the control does not support skins.</returns>
	[System.MonoTODO]
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a value indicating whether the control that is represented by the given context supports a set of <see cref="P:System.Web.UI.Control.SkinID" /> objects that can be picked from a list.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> for the control being designed.</param>
	/// <returns>
	///   <see langword="true" />, if <see cref="Overload:System.Web.UI.Design.SkinIDTypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}
}
