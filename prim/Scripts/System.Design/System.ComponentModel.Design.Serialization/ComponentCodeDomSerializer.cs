using System.CodeDom;

namespace System.ComponentModel.Design.Serialization;

internal class ComponentCodeDomSerializer : CodeDomSerializer
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
		RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
		if (rootContext != null && rootContext.Value == value)
		{
			return rootContext.Expression;
		}
		CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
		if (((IComponent)value).Site == null)
		{
			ReportError(manager, "Component of type '" + value.GetType().Name + "' not sited");
			return codeStatementCollection;
		}
		string name = manager.GetName(value);
		CodeExpression codeExpression = null;
		codeExpression = ((rootContext == null) ? new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), name) : new CodeFieldReferenceExpression(rootContext.Expression, name));
		SetExpression(manager, value, codeExpression);
		if (!(manager.Context[typeof(ExpressionContext)] is ExpressionContext expressionContext) || expressionContext.PresetValue != value || (expressionContext.PresetValue == value && (expressionContext.Expression is CodeFieldReferenceExpression || expressionContext.Expression is CodePropertyReferenceExpression)))
		{
			bool isComplete = true;
			codeStatementCollection.Add(new CodeCommentStatement(string.Empty));
			codeStatementCollection.Add(new CodeCommentStatement(name));
			codeStatementCollection.Add(new CodeCommentStatement(string.Empty));
			if (!(((IComponent)value).Site is INestedSite))
			{
				CodeStatement codeStatement = new CodeAssignStatement(codeExpression, SerializeCreationExpression(manager, value, out isComplete));
				codeStatement.UserData["statement-order"] = "initializer";
				codeStatementCollection.Add(codeStatement);
			}
			SerializeProperties(manager, codeStatementCollection, value, new Attribute[0]);
			SerializeEvents(manager, codeStatementCollection, value);
		}
		return codeStatementCollection;
	}
}
