using System.CodeDom;

namespace System.ComponentModel.Design.Serialization;

internal class EnumCodeDomSerializer : CodeDomSerializer
{
	public override object Serialize(IDesignerSerializationManager manager, object value)
	{
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		Enum[] array = null;
		TypeConverter converter = TypeDescriptor.GetConverter(value);
		array = ((!converter.CanConvertTo(typeof(Enum[]))) ? new Enum[1] { (Enum)value } : ((Enum[])converter.ConvertTo(value, typeof(Enum[]))));
		CodeExpression codeExpression = null;
		CodeExpression codeExpression2 = null;
		Enum[] array2 = array;
		foreach (Enum e in array2)
		{
			codeExpression2 = GetEnumExpression(e);
			codeExpression = ((codeExpression != null) ? new CodeBinaryOperatorExpression(codeExpression, CodeBinaryOperatorType.BitwiseOr, codeExpression2) : codeExpression2);
		}
		return codeExpression;
	}

	private CodeExpression GetEnumExpression(Enum e)
	{
		TypeConverter converter = TypeDescriptor.GetConverter(e);
		if (converter != null && converter.CanConvertTo(typeof(string)))
		{
			return new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(e.GetType().FullName), (string)converter.ConvertTo(e, typeof(string)));
		}
		return null;
	}
}
