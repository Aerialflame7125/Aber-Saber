using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms;

/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.Padding" /> values to and from various other representations.</summary>
public class PaddingConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PaddingConverter" /> class. </summary>
	public PaddingConverter()
	{
	}

	/// <summary>Returns whether this converter can convert an object of one type to the type of this converter.</summary>
	/// <returns>true if this object can perform the conversion; otherwise, false.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from.</param>
	public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
	{
		if ((object)sourceType == typeof(string))
		{
			return true;
		}
		return false;
	}

	/// <param name="context"></param>
	/// <param name="destinationType"></param>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if ((object)destinationType == typeof(string))
		{
			return true;
		}
		if ((object)destinationType == typeof(InstanceDescriptor))
		{
			return true;
		}
		return false;
	}

	/// <param name="context"></param>
	/// <param name="culture"></param>
	/// <param name="value"></param>
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
		return new Padding(int.Parse(array[0].Trim()), int.Parse(array[1].Trim()), int.Parse(array[2].Trim()), int.Parse(array[3].Trim()));
	}

	/// <param name="context"></param>
	/// <param name="culture"></param>
	/// <param name="value"></param>
	/// <param name="destinationType"></param>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (value is Padding padding)
		{
			if ((object)destinationType == typeof(string))
			{
				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}
				return string.Format("{0}{4} {1}{4} {2}{4} {3}", padding.Left, padding.Top, padding.Right, padding.Bottom, culture.TextInfo.ListSeparator);
			}
			if ((object)destinationType == typeof(InstanceDescriptor))
			{
				Type[] types;
				object[] arguments;
				if (padding.All != -1)
				{
					types = new Type[1] { typeof(int) };
					arguments = new object[1] { padding.All };
				}
				else
				{
					types = new Type[4]
					{
						typeof(int),
						typeof(int),
						typeof(int),
						typeof(int)
					};
					arguments = new object[4] { padding.Left, padding.Top, padding.Right, padding.Bottom };
				}
				ConstructorInfo constructor = typeof(Padding).GetConstructor(types);
				return new InstanceDescriptor(constructor, arguments);
			}
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <param name="context"></param>
	/// <param name="propertyValues"></param>
	public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
	{
		if (propertyValues == null)
		{
			throw new ArgumentNullException("propertyValues");
		}
		if (context == null)
		{
			throw new ArgumentNullException("context");
		}
		if (((Padding)context.PropertyDescriptor.GetValue(context.Instance)).All == (int)propertyValues["All"])
		{
			return new Padding((int)propertyValues["Left"], (int)propertyValues["Top"], (int)propertyValues["Right"], (int)propertyValues["Bottom"]);
		}
		return new Padding((int)propertyValues["All"]);
	}

	/// <param name="context"></param>
	public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
	{
		return true;
	}

	/// <param name="context"></param>
	/// <param name="value"></param>
	/// <param name="attributes"></param>
	public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
	{
		return TypeDescriptor.GetProperties(typeof(Padding), attributes);
	}

	/// <param name="context"></param>
	public override bool GetPropertiesSupported(ITypeDescriptorContext context)
	{
		return true;
	}
}
