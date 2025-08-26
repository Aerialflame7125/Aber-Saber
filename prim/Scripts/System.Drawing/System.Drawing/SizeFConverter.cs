using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Drawing;

/// <summary>Converts <see cref="T:System.Drawing.SizeF" /> objects from one type to another.</summary>
public class SizeFConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.SizeFConverter" /> class.</summary>
	public SizeFConverter()
	{
	}

	/// <summary>Returns a value indicating whether the converter can convert from the type specified to the <see cref="T:System.Drawing.SizeF" /> type, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
	/// <param name="sourceType">A <see cref="T:System.Type" /> the represents the type you wish to convert from.</param>
	/// <returns>
	///   <see langword="true" /> to indicate the conversion can be performed; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == typeof(string))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>Returns a value indicating whether the <see cref="T:System.Drawing.SizeFConverter" /> can convert a <see cref="T:System.Drawing.SizeF" /> to the specified type.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
	/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
	/// <returns>
	///   <see langword="true" /> if this converter can perform the conversion otherwise, <see langword="false" />.</returns>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			return true;
		}
		if (destinationType == typeof(InstanceDescriptor))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (!(value is string text))
		{
			return base.ConvertFrom(context, culture, value);
		}
		if (culture == null)
		{
			culture = CultureInfo.CurrentCulture;
		}
		string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
		SingleConverter singleConverter = new SingleConverter();
		float[] array2 = new float[array.Length];
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i] = (float)singleConverter.ConvertFromString(context, culture, array[i]);
		}
		if (array.Length != 2)
		{
			throw new ArgumentException("Failed to parse Text(" + text + ") expected text in the format \"Width,Height.\"");
		}
		return new SizeF(array2[0], array2[1]);
	}

	/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (value is SizeF sizeF)
		{
			if (destinationType == typeof(string))
			{
				return sizeF.Width.ToString(culture) + culture.TextInfo.ListSeparator + " " + sizeF.Height.ToString(culture);
			}
			if (destinationType == typeof(InstanceDescriptor))
			{
				return new InstanceDescriptor(typeof(SizeF).GetConstructor(new Type[2]
				{
					typeof(float),
					typeof(float)
				}), new object[2] { sizeF.Width, sizeF.Height });
			}
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>Creates an instance of a <see cref="T:System.Drawing.SizeF" /> with the specified property values using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
	/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> containing property names and values.</param>
	/// <returns>An <see cref="T:System.Object" /> representing the new <see cref="T:System.Drawing.SizeF" />, or <see langword="null" /> if the object cannot be created.</returns>
	public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
	{
		float width = (float)propertyValues["Width"];
		float height = (float)propertyValues["Height"];
		return new SizeF(width, height);
	}

	/// <summary>Returns a value indicating whether changing a value on this object requires a call to the <see cref="Overload:System.Drawing.SizeFConverter.CreateInstance" /> method to create a new value.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. This may be <see langword="null" />.</param>
	/// <returns>Always returns <see langword="true" />.</returns>
	public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
	{
		return true;
	}

	/// <summary>Retrieves a set of properties for the <see cref="T:System.Drawing.SizeF" /> type using the specified context and attributes.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to return properties for.</param>
	/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties.</param>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the properties.</returns>
	public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
	{
		if (value is SizeF)
		{
			return TypeDescriptor.GetProperties(value, attributes);
		}
		return base.GetProperties(context, value, attributes);
	}

	/// <summary>Returns whether the <see cref="T:System.Drawing.SizeF" /> type supports properties.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be supplied.</param>
	/// <returns>Always returns <see langword="true" />.</returns>
	public override bool GetPropertiesSupported(ITypeDescriptorContext context)
	{
		return true;
	}
}
