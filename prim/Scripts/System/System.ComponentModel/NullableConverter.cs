using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel;

/// <summary>Provides automatic conversion between a nullable type and its underlying primitive type.</summary>
[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
public class NullableConverter : TypeConverter
{
	private Type nullableType;

	private Type simpleType;

	private TypeConverter simpleTypeConverter;

	/// <summary>Gets the nullable type.</summary>
	/// <returns>A <see cref="T:System.Type" /> that represents the nullable type.</returns>
	public Type NullableType => nullableType;

	/// <summary>Gets the underlying type.</summary>
	/// <returns>A <see cref="T:System.Type" /> that represents the underlying type.</returns>
	public Type UnderlyingType => simpleType;

	/// <summary>Gets the underlying type converter.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that represents the underlying type converter.</returns>
	public TypeConverter UnderlyingTypeConverter => simpleTypeConverter;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NullableConverter" /> class.</summary>
	/// <param name="type">The specified nullable type.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="type" /> is not a nullable type.</exception>
	public NullableConverter(Type type)
	{
		nullableType = type;
		simpleType = Nullable.GetUnderlyingType(type);
		if (simpleType == null)
		{
			throw new ArgumentException(global::SR.GetString("The specified type is not a nullable type."), "type");
		}
		simpleTypeConverter = TypeDescriptor.GetConverter(simpleType);
	}

	/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
	/// <returns>
	///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if (sourceType == simpleType)
		{
			return true;
		}
		if (simpleTypeConverter != null)
		{
			return simpleTypeConverter.CanConvertFrom(context, sourceType);
		}
		return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (value == null || value.GetType() == simpleType)
		{
			return value;
		}
		if (value is string && string.IsNullOrEmpty(value as string))
		{
			return null;
		}
		if (simpleTypeConverter != null)
		{
			return simpleTypeConverter.ConvertFrom(context, culture, value);
		}
		return base.ConvertFrom(context, culture, value);
	}

	/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
	/// <returns>
	///   <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if (destinationType == simpleType)
		{
			return true;
		}
		if (destinationType == typeof(InstanceDescriptor))
		{
			return true;
		}
		if (simpleTypeConverter != null)
		{
			return simpleTypeConverter.CanConvertTo(context, destinationType);
		}
		return base.CanConvertTo(context, destinationType);
	}

	/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="destinationType" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (destinationType == null)
		{
			throw new ArgumentNullException("destinationType");
		}
		if (destinationType == simpleType && nullableType.IsInstanceOfType(value))
		{
			return value;
		}
		if (destinationType == typeof(InstanceDescriptor))
		{
			return new InstanceDescriptor(nullableType.GetConstructor(new Type[1] { simpleType }), new object[1] { value }, isComplete: true);
		}
		if (value == null)
		{
			if (destinationType == typeof(string))
			{
				return string.Empty;
			}
		}
		else if (simpleTypeConverter != null)
		{
			return simpleTypeConverter.ConvertTo(context, culture, value, destinationType);
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>Creates an instance of the type that this <see cref="T:System.ComponentModel.TypeConverter" /> is associated with, using the specified context, given a set of property values for the object.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values.</param>
	/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or <see langword="null" /> if the object cannot be created. This method always returns <see langword="null" />.</returns>
	public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
	{
		if (simpleTypeConverter != null)
		{
			return simpleTypeConverter.CreateInstance(context, propertyValues);
		}
		return base.CreateInstance(context, propertyValues);
	}

	/// <summary>Returns whether changing a value on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>
	///   <see langword="true" /> if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, <see langword="false" />.</returns>
	public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
	{
		if (simpleTypeConverter != null)
		{
			return simpleTypeConverter.GetCreateInstanceSupported(context);
		}
		return base.GetCreateInstanceSupported(context);
	}

	/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties.</param>
	/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
	/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or <see langword="null" /> if there are no properties.</returns>
	public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
	{
		if (simpleTypeConverter != null)
		{
			return simpleTypeConverter.GetProperties(context, value, attributes);
		}
		return base.GetProperties(context, value, attributes);
	}

	/// <summary>Returns whether this object supports properties, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>
	///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, <see langword="false" />.</returns>
	public override bool GetPropertiesSupported(ITypeDescriptorContext context)
	{
		if (simpleTypeConverter != null)
		{
			return simpleTypeConverter.GetPropertiesSupported(context);
		}
		return base.GetPropertiesSupported(context);
	}

	/// <summary>Returns a collection of standard values for the data type this type converter is designed for when provided with a format context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be <see langword="null" />.</param>
	/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or <see langword="null" /> if the data type does not support a standard set of values.</returns>
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		if (simpleTypeConverter != null)
		{
			StandardValuesCollection standardValues = simpleTypeConverter.GetStandardValues(context);
			if (GetStandardValuesSupported(context) && standardValues != null)
			{
				object[] array = new object[standardValues.Count + 1];
				int num = 0;
				array[num++] = null;
				foreach (object item in standardValues)
				{
					array[num++] = item;
				}
				return new StandardValuesCollection(array);
			}
		}
		return base.GetStandardValues(context);
	}

	/// <summary>Returns whether the collection of standard values returned from <see cref="Overload:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exclusive list of possible values, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; <see langword="false" /> if other values are possible.</returns>
	public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
	{
		if (simpleTypeConverter != null)
		{
			return simpleTypeConverter.GetStandardValuesExclusive(context);
		}
		return base.GetStandardValuesExclusive(context);
	}

	/// <summary>Returns whether this object supports a standard set of values that can be picked from a list, using the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <returns>
	///   <see langword="true" /> if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, <see langword="false" />.</returns>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		if (simpleTypeConverter != null)
		{
			return simpleTypeConverter.GetStandardValuesSupported(context);
		}
		return base.GetStandardValuesSupported(context);
	}

	/// <summary>Returns whether the given value object is valid for this type and for the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to test for validity.</param>
	/// <returns>
	///   <see langword="true" /> if the specified value is valid for this object; otherwise, <see langword="false" />.</returns>
	public override bool IsValid(ITypeDescriptorContext context, object value)
	{
		if (simpleTypeConverter != null)
		{
			if (value == null)
			{
				return true;
			}
			return simpleTypeConverter.IsValid(context, value);
		}
		return base.IsValid(context, value);
	}
}
