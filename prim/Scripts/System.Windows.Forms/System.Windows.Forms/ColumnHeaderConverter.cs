using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms;

/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.ColumnHeader" /> objects from one type to another.</summary>
/// <filterpriority>2</filterpriority>
public class ColumnHeaderConverter : ExpandableObjectConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeaderConverter" /> class. </summary>
	public ColumnHeaderConverter()
	{
	}

	/// <summary>Converts the specified object to the specified type, using the specified context and culture information.</summary>
	/// <returns>The <see cref="T:System.Object" /> that is the result of the conversion.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that represents information about a culture, such as language and calendar system. Can be null.</param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
	/// <param name="destinationType">The <see cref="T:System.Type" /> to convert to.</param>
	/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed<paramref name="." /></exception>
	/// <filterpriority>1</filterpriority>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if ((object)destinationType == typeof(InstanceDescriptor) && value is ColumnHeader)
		{
			ColumnHeader columnHeader = (ColumnHeader)value;
			if (columnHeader.ImageIndex != -1)
			{
				Type[] types = new Type[1] { typeof(int) };
				ConstructorInfo constructor = typeof(ColumnHeader).GetConstructor(types);
				if ((object)constructor != null)
				{
					object[] arguments = new object[1] { columnHeader.ImageIndex };
					return new InstanceDescriptor(constructor, arguments, isComplete: false);
				}
			}
			else if (string.IsNullOrEmpty(columnHeader.ImageKey))
			{
				Type[] types = new Type[1] { typeof(string) };
				ConstructorInfo constructor = typeof(ColumnHeader).GetConstructor(types);
				if ((object)constructor != null)
				{
					object[] arguments2 = new object[1] { columnHeader.ImageKey };
					return new InstanceDescriptor(constructor, arguments2, isComplete: false);
				}
			}
			else
			{
				Type[] types = Type.EmptyTypes;
				ConstructorInfo constructor = typeof(ColumnHeader).GetConstructor(types);
				if ((object)constructor != null)
				{
					object[] arguments3 = new object[0];
					return new InstanceDescriptor(constructor, arguments3, isComplete: false);
				}
			}
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}

	/// <summary>Returns a value indicating whether the <see cref="T:System.Windows.Forms.ColumnHeaderConverter" /> can convert a <see cref="T:System.Windows.Forms.ColumnHeader" /> to the specified type, using the specified context.</summary>
	/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
	/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
	/// <param name="destinationType">A type representing the type to convert to.</param>
	/// <filterpriority>1</filterpriority>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if ((object)destinationType == typeof(InstanceDescriptor))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}
}
