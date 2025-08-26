using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

/// <summary>Converts <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> objects to and from other data types.  </summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewCellStyleConverter : TypeConverter
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStyleConverter" /> class. </summary>
	public DataGridViewCellStyleConverter()
	{
	}

	/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
	/// <filterpriority>1</filterpriority>
	public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
	{
		throw new NotImplementedException();
	}

	/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
	/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is assumed. </param>
	/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
	/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to. </param>
	/// <filterpriority>1</filterpriority>
	public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
	{
		throw new NotImplementedException();
	}
}
