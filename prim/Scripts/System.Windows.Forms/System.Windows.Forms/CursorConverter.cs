using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

namespace System.Windows.Forms;

/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.Cursor" /> objects to and from various other representations. </summary>
/// <filterpriority>2</filterpriority>
public class CursorConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CursorConverter" /> class. </summary>
	public CursorConverter()
	{
	}

	/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
	/// <returns>true if this object can perform the conversion.</returns>
	/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
	/// <param name="sourceType">The type you wish to convert from. </param>
	/// <filterpriority>1</filterpriority>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if ((object)sourceType == typeof(byte[]))
		{
			return true;
		}
		return base.CanConvertFrom(context, sourceType);
	}

	/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
	/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
	/// <filterpriority>1</filterpriority>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if ((object)destinationType == typeof(byte[]) || (object)destinationType == typeof(InstanceDescriptor))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
	/// <filterpriority>1</filterpriority>
	public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
	{
		if (!(value is byte[] buffer))
		{
			return base.ConvertFrom(context, culture, value);
		}
		using MemoryStream stream = new MemoryStream(buffer);
		return new Cursor(stream);
	}

	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
	/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
	/// <filterpriority>1</filterpriority>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if ((object)destinationType == null)
		{
			throw new ArgumentNullException("destinationType");
		}
		if (value == null && (object)destinationType == typeof(string))
		{
			return "(none)";
		}
		if (!(value is Cursor))
		{
			throw new ArgumentException("object must be of class Cursor", "value");
		}
		if ((object)destinationType == typeof(byte[]))
		{
			if (value == null)
			{
				return new byte[0];
			}
			Cursor cursor = (Cursor)value;
			SerializationInfo serializationInfo = new SerializationInfo(typeof(Cursor), new FormatterConverter());
			((ISerializable)cursor).GetObjectData(serializationInfo, new StreamingContext(StreamingContextStates.Remoting));
			return (byte[])serializationInfo.GetValue("CursorData", typeof(byte[]));
		}
		if ((object)destinationType == typeof(InstanceDescriptor))
		{
			PropertyInfo[] properties = typeof(Cursors).GetProperties();
			PropertyInfo[] array = properties;
			foreach (PropertyInfo propertyInfo in array)
			{
				if (propertyInfo.GetValue(null, null) == value)
				{
					return new InstanceDescriptor(propertyInfo, null);
				}
			}
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>Retrieves a collection containing a set of standard values for the data type this validator is designed for. This will return null if the data type does not support a standard set of values.</summary>
	/// <returns>A collection containing a standard set of valid values, or null. The default implementation always returns null.</returns>
	/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
	/// <filterpriority>1</filterpriority>
	public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
	{
		PropertyInfo[] properties = typeof(Cursors).GetProperties();
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < properties.Length; i++)
		{
			arrayList.Add(properties[i].GetValue(null, null));
		}
		return new StandardValuesCollection(arrayList);
	}

	/// <summary>Determines if this object supports a standard set of values that can be picked from a list.</summary>
	/// <returns>Returns true if GetStandardValues should be called to find a common set of values the object supports.</returns>
	/// <param name="context">A type descriptor through which additional context may be provided. </param>
	/// <filterpriority>1</filterpriority>
	public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
	{
		return true;
	}
}
