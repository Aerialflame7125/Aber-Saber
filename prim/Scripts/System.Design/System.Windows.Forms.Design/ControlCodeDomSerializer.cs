using System.CodeDom;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms.Design;

internal class ControlCodeDomSerializer : ComponentCodeDomSerializer
{
	public override object Serialize(IDesignerSerializationManager manager, object value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (!(value is Control))
		{
			throw new InvalidOperationException("value is not a Control");
		}
		object obj = base.Serialize(manager, value);
		if (obj is CodeStatementCollection codeStatementCollection && (TypeDescriptor.GetProperties(value)["Controls"].GetValue(value) as ICollection).Count > 0)
		{
			CodeExpression expression = GetExpression(manager, value);
			CodeStatement codeStatement = new CodeExpressionStatement(new CodeMethodInvokeExpression(expression, "SuspendLayout"));
			codeStatement.UserData["statement-order"] = "begin";
			codeStatementCollection.Add(codeStatement);
			codeStatement = new CodeExpressionStatement(new CodeMethodInvokeExpression(expression, "ResumeLayout", new CodePrimitiveExpression(false)));
			codeStatement.UserData["statement-order"] = "end";
			codeStatementCollection.Add(codeStatement);
			obj = codeStatementCollection;
		}
		return obj;
	}
}
