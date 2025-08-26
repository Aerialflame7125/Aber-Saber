using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Provides a type converter to convert <see cref="T:System.Windows.Forms.TreeNode" /> objects to and from various other representations.</summary>
/// <filterpriority>2</filterpriority>
public class TreeNodeConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNodeConverter" /> class.</summary>
	public TreeNodeConverter()
	{
	}

	/// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
	/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you wish to convert to. </param>
	/// <filterpriority>1</filterpriority>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		if ((object)destinationType == typeof(string))
		{
			return true;
		}
		return base.CanConvertTo(context, destinationType);
	}

	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
	/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
	/// <filterpriority>1</filterpriority>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if ((object)destinationType == typeof(string) && value != null)
		{
			return value.ToString();
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
