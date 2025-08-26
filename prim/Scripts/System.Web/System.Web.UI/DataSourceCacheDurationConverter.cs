using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Provides a type converter to convert 32-bit signed integer objects to and from data source control cache duration representations.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class DataSourceCacheDurationConverter : Int32Converter
{
	private static readonly List<int> standardValues = new List<int> { 0 };

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> class. </summary>
	public DataSourceCacheDurationConverter()
	{
	}

	/// <summary>Determines whether the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> can convert an object in the given source type to an <see cref="T:System.Int32" /> object.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="sourceType">A <see cref="T:System.Type" /> that represents a type that the converter converts.</param>
	/// <returns>
	///     <see langword="true" /> if this converter can perform the operation; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>Returns a value indicating whether the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> instance can convert an object to the given destination type.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="destinationType">A <see cref="T:System.Type" />  that represents the type to which you want to convert.</param>
	/// <returns>
	///     <see langword="true" /> if this converter can perform the operation; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	/// <summary>Converts the specified object to an <see cref="T:System.Int32" /> object.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture in which to represent the number.</param>
	/// <param name="value">The object to convert.</param>
	/// <returns>An object that represents the converted value. If <see langword="null" /> is passed in the value parameter, <see langword="null" /> is returned.</returns>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value == null)
		{
			return null;
		}
		if (value is string text && (text.Length == 0 || string.Compare("infinite", text, StringComparison.OrdinalIgnoreCase) == 0))
		{
			return 0;
		}
		return base.ConvertFrom(context, culture, value);
	}

	/// <summary>Converts the specified object to another type.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture in which to represent the number.</param>
	/// <param name="value">The object to convert.</param>
	/// <param name="destinationType">The type to convert the object to.</param>
	/// <returns>An object that represents the converted value.</returns>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			if (value == null)
			{
				return string.Empty;
			}
			if (value is int && (int)value == 0)
			{
				return "Infinite";
			}
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>Returns a collection of standard values for the data type the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> instance is designed for.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that contains 0 for cache duration, which represents "Infinite".</returns>
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		return new StandardValuesCollection(standardValues);
	}

	/// <summary>Specifies whether the collection of standard values returned from the <see cref="Overload:System.Web.UI.DataSourceCacheDurationConverter.GetStandardValues" /> method is an exclusive list, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return false;
	}

	/// <summary>Specifies whether the <see cref="T:System.Web.UI.DataSourceCacheDurationConverter" /> object supports a standard set of values that can be picked from a list, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
	/// <returns>
	///     <see langword="true" /> in all cases.</returns>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return true;
	}
}
