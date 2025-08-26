using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Provides a type converter to convert data for an image key to and from another data type.</summary>
/// <filterpriority>2</filterpriority>
public class ImageKeyConverter : StringConverter
{
	/// <summary>Gets or sets a value indicating whether null is valid in the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> collection.</summary>
	/// <returns>true in all cases, indicating null is valid in the standard values collection.</returns>
	protected virtual bool IncludeNoneAsStandardValue => true;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ImageKeyConverter" /> class. </summary>
	public ImageKeyConverter()
	{
	}

	/// <summary>Returns whether this converter can convert an object of the given type to a string using the specified context.</summary>
	/// <returns>true to indicate the specified conversion can be performed; otherwise, false. </returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="sourceType">A <see cref="T:System.Type" /> that specifies the type you want to convert from.</param>
	/// <filterpriority>1</filterpriority>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if ((object)sourceType == typeof(string))
		{
			return true;
		}
		return false;
	}

	/// <summary>Converts from the specified object to a string.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> to provide locale information. </param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
	/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
	/// <filterpriority>1</filterpriority>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value != null && value is string)
		{
			return (string)value;
		}
		return base.ConvertFrom(context, culture, value);
	}

	/// <summary>Converts the given object to the specified type.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that provides locale information. </param>
	/// <param name="value">The object to convert, typically an image key.</param>
	/// <param name="destinationType">The type to convert the object to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="destinationType" /> is null. </exception>
	/// <exception cref="T:System.NotSupportedException">The specified <paramref name="value" /> could not be converted to the specified <paramref name="destinationType" />.</exception>
	/// <filterpriority>1</filterpriority>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (value == null)
		{
			return "(none)";
		}
		if ((object)destinationType == typeof(string))
		{
			if (value is string && (string)value == string.Empty)
			{
				return "(none)";
			}
			return value.ToString();
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>Returns a collection of standard image keys for the image list associated with the specified context. </summary>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that contains the standard set of image key values. </returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null.</param>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		string[] values = new string[1] { string.Empty };
		return new StandardValuesCollection(values);
	}

	/// <summary>Determines whether the list of standard values for the <see cref="T:System.Windows.Forms.ImageKeyConverter" /> is exclusive (that is, whether it allows values other than those returned by <see cref="Overload:System.Windows.Forms.ImageKeyConverter.GetStandardValues" />).</summary>
	/// <returns>true to indicate the list does not allow additional values; otherwise, false. Always returns true. </returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null.</param>
	/// <filterpriority>1</filterpriority>
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		return true;
	}

	/// <summary>Determines whether this type converter supports a standard set of values that can be picked from a list.</summary>
	/// <returns>true to indicate a list of standard values is supported; otherwise, false. Always returns true.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context, which can be used to extract additional information about the environment this type converter is being invoked from. This parameter or properties of this parameter can be null.</param>
	/// <filterpriority>1</filterpriority>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return true;
	}
}
