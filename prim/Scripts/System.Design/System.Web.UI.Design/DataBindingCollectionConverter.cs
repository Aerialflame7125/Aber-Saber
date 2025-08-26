using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.Design;

/// <summary>Provides a type converter for <see cref="T:System.Web.UI.DataBindingCollection" /> objects.</summary>
[Obsolete("This class is not supposed to be in use anymore as DesignerActionList is supposed to be used for editing DataBinding")]
public class DataBindingCollectionConverter : TypeConverter
{
	/// <summary>Initializes an instance of the <see cref="T:System.Web.UI.Design.DataBindingCollectionConverter" /> class.</summary>
	public DataBindingCollectionConverter()
	{
	}

	/// <summary>Converts a data binding collection to the specified type.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the component or control to which the data binding collection belongs.</param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that can be used to provide additional culture information.</param>
	/// <param name="value">The object to convert.</param>
	/// <param name="destinationType">The type to convert to.</param>
	/// <returns>The object produced by the type conversion. If the <paramref name="destinationType" /> parameter is of type <see cref="T:System.String" />, this method returns an empty string ("").</returns>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		if (destinationType == typeof(string))
		{
			return string.Empty;
		}
		return base.ConvertTo(context, culture, value, destinationType);
	}
}
