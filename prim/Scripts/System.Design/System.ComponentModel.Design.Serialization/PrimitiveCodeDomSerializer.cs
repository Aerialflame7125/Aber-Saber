using System.CodeDom;

namespace System.ComponentModel.Design.Serialization;

internal class PrimitiveCodeDomSerializer : CodeDomSerializer
{
	public override object Serialize(IDesignerSerializationManager manager, object value)
	{
		return new CodePrimitiveExpression(value);
	}
}
