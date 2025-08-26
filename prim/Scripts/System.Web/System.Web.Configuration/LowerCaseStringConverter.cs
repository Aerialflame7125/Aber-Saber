using System.ComponentModel;
using System.Globalization;

namespace System.Web.Configuration;

/// <summary>Provides support to convert an object to a lowercase string. This class cannot be inherited.</summary>
public sealed class LowerCaseStringConverter : TypeConverter
{
	/// <summary>Determines whether an object can be converted to a lowercase string based on the specified parameters.</summary>
	/// <param name="ctx">An object that implements the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface.</param>
	/// <param name="type">The type of object to convert.</param>
	/// <returns>
	///     <see langword="true" /> if the parameters describe an object that can be converted to a lowercase string object; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertFrom(ITypeDescriptorContext ctx, Type type)
	{
		return type == typeof(string);
	}

	/// <summary>Determines whether an object can be converted to a lowercase string based on the specified parameters.</summary>
	/// <param name="ctx">An object that implements the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface.</param>
	/// <param name="type">The type of object to convert.</param>
	/// <returns>
	///     <see langword="true" /> if the parameters describe an object that can be converted to a lowercase string object; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertTo(ITypeDescriptorContext ctx, Type type)
	{
		return type == typeof(string);
	}

	/// <summary>Converts an object from its original value to a lowercase string based on the specified parameters.</summary>
	/// <param name="ctx">An object that implements the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface.</param>
	/// <param name="ci">An object that implements the <see cref="T:System.Globalization.CultureInfo" /> class.</param>
	/// <param name="data">The object to convert.</param>
	/// <returns>A lowercase string object.</returns>
	public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
	{
		return ((string)data).ToLowerInvariant();
	}

	/// <summary>Converts an object to a lowercase string based on the specified parameters.</summary>
	/// <param name="ctx">An object that implements the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface.</param>
	/// <param name="ci">An object that implements the <see cref="T:System.Globalization.CultureInfo" /> class.</param>
	/// <param name="value">The object to convert.</param>
	/// <param name="type">The type of object to convert.</param>
	/// <returns>A lowercase string object.</returns>
	public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
	{
		if (value == null)
		{
			return "";
		}
		if (!(value is string))
		{
			throw new ArgumentException("value");
		}
		return ((string)value).ToLowerInvariant();
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Configuration.LowerCaseStringConverter" /> class.</summary>
	public LowerCaseStringConverter()
	{
	}
}
