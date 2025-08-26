using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Provides a type converter to convert data for an image index to and from a string.</summary>
/// <filterpriority>2</filterpriority>
public class ImageIndexConverter : Int32Converter
{
	/// <summary>Gets or sets a value indicating whether a none or null value is valid in the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> collection.</summary>
	/// <returns>true if a none or null value is valid in the standard values collection; otherwise, false.</returns>
	protected virtual bool IncludeNoneAsStandardValue => true;

	/// <summary>Initializes and instance of the <see cref="T:System.Windows.Forms.ImageIndexConverter" /> class.</summary>
	public ImageIndexConverter()
	{
	}

	/// <summary>Converts the specified value object to a 32-bit signed integer object.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to provide locale information. </param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
	/// <exception cref="T:System.Exception">The conversion could not be performed. </exception>
	/// <filterpriority>1</filterpriority>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value != null && value is string)
		{
			string text = (string)value;
			if (text == "(none)")
			{
				return -1;
			}
			return int.Parse(text);
		}
		return base.ConvertFrom(context, culture, value);
	}

	/// <summary>Converts the specified object to the specified destination type.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides locale information. </param>
	/// <param name="value">The object to convert, typically an index represented as an <see cref="T:System.Int32" />.</param>
	/// <param name="destinationType">The type to convert the object to, often a <see cref="T:System.String" />.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="destinationType" /> is null. </exception>
	/// <exception cref="T:System.NotSupportedException">The specified <paramref name="value" /> could not be converted to the specified <paramref name="destinationType" />.</exception>
	/// <filterpriority>1</filterpriority>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (value != null && (object)destinationType == typeof(string))
		{
			if (value is int && (int)value == -1)
			{
				return "(none)";
			}
			return value.ToString();
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>Returns a collection of standard index values for the image list associated with the specified format context.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid index values. If no image list is found, this collection will contain a single object with a value of -1.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null. </param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		int[] values = new int[1] { -1 };
		return new StandardValuesCollection(values);
	}

	/// <summary>Determines if the list of standard values returned from the <see cref="Overload:System.Windows.Forms.ImageIndexConverter.GetStandardValues" /> method is an exclusive list. </summary>
	/// <returns>true if the <see cref="Overload:System.Windows.Forms.ImageIndexConverter.GetStandardValues" /> method returns an exclusive list of valid values; otherwise, false. This implementation always returns false.</returns>
	/// <param name="context">A formatter context. </param>
	/// <filterpriority>1</filterpriority>
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return false;
	}

	/// <summary>Determines if the type converter supports a standard set of values that can be picked from a list.</summary>
	/// <returns>true if the <see cref="Overload:System.Windows.Forms.ImageIndexConverter.GetStandardValues" /> method returns a standard set of values; otherwise, false. Always returns true.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null. </param>
	/// <filterpriority>1</filterpriority>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return true;
	}
}
