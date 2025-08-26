using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Represents an area within a <see cref="T:System.Windows.Forms.LinkLabel" /> control that represents a hyperlink within the control.</summary>
/// <filterpriority>2</filterpriority>
[Serializable]
[TypeConverter(typeof(LinkAreaConverter))]
public struct LinkArea
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.LinkArea.LinkAreaConverter" /> objects to and from various other representations.</summary>
	public class LinkAreaConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkArea.LinkAreaConverter" /> class. </summary>
		public LinkAreaConverter()
		{
		}

		/// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
		/// <returns>True if this object can perform the conversion.</returns>
		/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="sourceType">The type you wish to convert from. </param>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if ((object)sourceType == typeof(string))
			{
				return true;
			}
			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if ((object)destinationType == typeof(string))
			{
				return true;
			}
			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given object to the converter's native type.</summary>
		/// <returns>The converted object. This will throw an exception if the conversion could not be performed.</returns>
		/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="culture">An optional culture info. If not supplied, the current culture is assumed. </param>
		/// <param name="value">The object to convert. </param>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			string[] array = ((string)value).Split(culture.TextInfo.ListSeparator.ToCharArray());
			int start = int.Parse(array[0].Trim());
			int length = int.Parse(array[1].Trim());
			return new LinkArea(start, length);
		}

		/// <summary>Converts the given object to another type. The most common types to convert are to and from a string object. The default implementation will make a call to <see cref="M:System.Windows.Forms.LinkArea.ToString" /> on the object if the object is valid and if the destination type is string. If this cannot convert to the destination type, this will throw a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>The converted object.</returns>
		/// <param name="context">A formatter context. This object can be used to extract additional information about the environment this converter is being invoked from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
		/// <param name="culture">An optional culture info. If not supplied the current culture is assumed. </param>
		/// <param name="value">The object to convert. </param>
		/// <param name="destinationType">The type to convert the object to. </param>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || !(value is LinkArea) || (object)destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			LinkArea linkArea = (LinkArea)value;
			return linkArea.Start + culture.TextInfo.ListSeparator + linkArea.Length;
		}

		/// <summary>Creates an instance of this type, given a set of property values for the object. This is useful for objects that are immutable, but still want to provide changeable properties.</summary>
		/// <returns>The newly created object, or null if the object could not be created. The default implementation returns null.</returns>
		/// <param name="context">A type descriptor through which additional context may be provided. </param>
		/// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" />. </param>
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			return new LinkArea((int)propertyValues["Start"], (int)propertyValues["Length"]);
		}

		/// <summary>Determines if changing a value on this object should require a call to <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> to create a new value.</summary>
		/// <returns>Returns true if <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> should be called when a change is made to one or more properties of this object.</returns>
		/// <param name="context">A type descriptor through which additional context may be provided. </param>
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Retrieves the set of properties for this type. </summary>
		/// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this might return null. The default implementation always returns null.</returns>
		/// <param name="context">A type descriptor through which additional context may be provided. </param>
		/// <param name="value">The value of the object to get the properties for. </param>
		/// <param name="attributes">The attributes of the object to get the properties for. </param>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(typeof(LinkArea), attributes);
		}

		/// <summary>Determines if this object supports properties. By default, this is false.</summary>
		/// <returns>Returns true if <see cref="M:System.Windows.Forms.LinkArea.LinkAreaConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object.</returns>
		/// <param name="context">A type descriptor through which additional context may be provided. </param>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}

	private int start;

	private int length;

	/// <summary>Gets or sets the starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
	/// <returns>The location within the text of the <see cref="T:System.Windows.Forms.LinkLabel" /> control where the link starts.</returns>
	/// <filterpriority>1</filterpriority>
	public int Start
	{
		get
		{
			return start;
		}
		set
		{
			start = value;
		}
	}

	/// <summary>Gets or sets the number of characters in the link area.</summary>
	/// <returns>The number of characters, including spaces, in the link area.</returns>
	/// <filterpriority>1</filterpriority>
	public int Length
	{
		get
		{
			return length;
		}
		set
		{
			length = value;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.LinkArea" /> is empty.</summary>
	/// <returns>true if the specified start and length return an empty link area; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public bool IsEmpty
	{
		get
		{
			if (start == 0 && length == 0)
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkArea" /> class.</summary>
	/// <param name="start">The zero-based starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />. </param>
	/// <param name="length">The number of characters, after the starting character, to include in the link area. </param>
	public LinkArea(int start, int length)
	{
		this.start = start;
		this.length = length;
	}

	/// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.</returns>
	/// <param name="o"></param>
	/// <filterpriority>1</filterpriority>
	public override bool Equals(object o)
	{
		if (!(o is LinkArea linkArea))
		{
			return false;
		}
		return linkArea.Start == start && linkArea.Length == length;
	}

	/// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
	/// <filterpriority>1</filterpriority>
	public override int GetHashCode()
	{
		return (start << 4) | length;
	}

	public override string ToString()
	{
		return $"{{Start={start.ToString()}, Length={length.ToString()}}}";
	}

	/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are equal.</summary>
	/// <returns>true if two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are equal; otherwise, false.</returns>
	/// <param name="linkArea1">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
	/// <param name="linkArea2">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
	public static bool operator ==(LinkArea linkArea1, LinkArea linkArea2)
	{
		return linkArea1.Length == linkArea2.Length && linkArea1.Start == linkArea2.Start;
	}

	/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are not equal.</summary>
	/// <returns>true if two instances of the <see cref="T:System.Windows.Forms.LinkArea" /> class are not equal; otherwise, false.</returns>
	/// <param name="linkArea1">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
	/// <param name="linkArea2">A <see cref="T:System.Windows.Forms.LinkArea" /> to compare.</param>
	public static bool operator !=(LinkArea linkArea1, LinkArea linkArea2)
	{
		return !(linkArea1 == linkArea2);
	}
}
