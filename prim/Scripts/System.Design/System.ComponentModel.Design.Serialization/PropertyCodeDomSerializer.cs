using System.CodeDom;

namespace System.ComponentModel.Design.Serialization;

internal class PropertyCodeDomSerializer : MemberCodeDomSerializer
{
	public override void Serialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor, CodeStatementCollection statements)
	{
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (descriptor == null)
		{
			throw new ArgumentNullException("descriptor");
		}
		if (statements == null)
		{
			throw new ArgumentNullException("statements");
		}
		PropertyDescriptor propertyDescriptor = (PropertyDescriptor)descriptor;
		if (propertyDescriptor.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content))
		{
			SerializeContentProperty(manager, value, propertyDescriptor, statements);
		}
		else
		{
			SerializeNormalProperty(manager, value, propertyDescriptor, statements);
		}
	}

	private void SerializeNormalProperty(IDesignerSerializationManager manager, object instance, PropertyDescriptor descriptor, CodeStatementCollection statements)
	{
		CodeAssignStatement codeAssignStatement = new CodeAssignStatement();
		CodeExpression codeExpression = null;
		ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
		RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
		codeExpression = ((expressionContext != null && expressionContext.PresetValue == instance && expressionContext.Expression != null) ? new CodePropertyReferenceExpression(expressionContext.Expression, descriptor.Name) : ((rootContext == null || rootContext.Value != instance) ? new CodePropertyReferenceExpression
		{
			PropertyName = descriptor.Name,
			TargetObject = SerializeToExpression(manager, instance)
		} : new CodePropertyReferenceExpression(rootContext.Expression, descriptor.Name)));
		CodeExpression codeExpression2 = null;
		MemberRelationship relationship = GetRelationship(manager, instance, descriptor);
		codeExpression2 = (relationship.IsEmpty ? SerializeToExpression(manager, descriptor.GetValue(instance)) : new CodePropertyReferenceExpression
		{
			PropertyName = relationship.Member.Name,
			TargetObject = SerializeToExpression(manager, relationship.Owner)
		});
		if (codeExpression2 == null || codeExpression == null)
		{
			ReportError(manager, "Cannot serialize " + ((IComponent)instance).Site.Name + "." + descriptor.Name, "Property Name: " + descriptor.Name + Environment.NewLine + "Property Type: " + descriptor.PropertyType.Name + Environment.NewLine);
		}
		else
		{
			codeAssignStatement.Left = codeExpression;
			codeAssignStatement.Right = codeExpression2;
			statements.Add(codeAssignStatement);
		}
	}

	private void SerializeContentProperty(IDesignerSerializationManager manager, object instance, PropertyDescriptor descriptor, CodeStatementCollection statements)
	{
		CodePropertyReferenceExpression codePropertyReferenceExpression = new CodePropertyReferenceExpression();
		codePropertyReferenceExpression.PropertyName = descriptor.Name;
		object value = descriptor.GetValue(instance);
		if (manager.Context[typeof(ExpressionContext)] is ExpressionContext expressionContext && expressionContext.PresetValue == instance)
		{
			codePropertyReferenceExpression.TargetObject = expressionContext.Expression;
		}
		else
		{
			codePropertyReferenceExpression.TargetObject = SerializeToExpression(manager, instance);
		}
		CodeDomSerializer codeDomSerializer = manager.GetSerializer(value.GetType(), typeof(CodeDomSerializer)) as CodeDomSerializer;
		if (codePropertyReferenceExpression.TargetObject != null && codeDomSerializer != null)
		{
			manager.Context.Push(new ExpressionContext(codePropertyReferenceExpression, codePropertyReferenceExpression.GetType(), null, value));
			object obj = codeDomSerializer.Serialize(manager, value);
			manager.Context.Pop();
			if (obj is CodeStatementCollection value2)
			{
				statements.AddRange(value2);
			}
			if (obj is CodeStatement value3)
			{
				statements.Add(value3);
			}
			if (obj is CodeExpression right)
			{
				statements.Add(new CodeAssignStatement(codePropertyReferenceExpression, right));
			}
		}
	}

	public override bool ShouldSerialize(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor)
	{
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (descriptor == null)
		{
			throw new ArgumentNullException("descriptor");
		}
		PropertyDescriptor propertyDescriptor = (PropertyDescriptor)descriptor;
		if (propertyDescriptor.Attributes.Contains(DesignOnlyAttribute.Yes))
		{
			return false;
		}
		if (manager.Context[typeof(SerializeAbsoluteContext)] is SerializeAbsoluteContext serializeAbsoluteContext && serializeAbsoluteContext.ShouldSerialize(descriptor))
		{
			return true;
		}
		bool flag = propertyDescriptor.ShouldSerializeValue(value);
		if (!flag && !GetRelationship(manager, value, descriptor).IsEmpty)
		{
			flag = true;
		}
		return flag;
	}

	private MemberRelationship GetRelationship(IDesignerSerializationManager manager, object value, MemberDescriptor descriptor)
	{
		if (manager.GetService(typeof(MemberRelationshipService)) is MemberRelationshipService memberRelationshipService)
		{
			return memberRelationshipService[value, descriptor];
		}
		return MemberRelationship.Empty;
	}
}
