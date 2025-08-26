using System.CodeDom;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Serializes an object graph to a series of CodeDOM statements. This class provides an abstract base class for a serializer.</summary>
public class CodeDomSerializer : CodeDomSerializerBase
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> class.</summary>
	public CodeDomSerializer()
	{
	}

	/// <summary>Serializes the given object, accounting for default values.</summary>
	/// <param name="manager">The serialization manager to use for serialization.</param>
	/// <param name="value">The object to serialize.</param>
	/// <returns>A CodeDom object representing <paramref name="value" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	public virtual object SerializeAbsolute(IDesignerSerializationManager manager, object value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		SerializeAbsoluteContext context = new SerializeAbsoluteContext();
		manager.Context.Push(context);
		object result = Serialize(manager, value);
		manager.Context.Pop();
		return result;
	}

	/// <summary>Serializes the specified object into a CodeDOM object.</summary>
	/// <param name="manager">The serialization manager to use during serialization.</param>
	/// <param name="value">The object to serialize.</param>
	/// <returns>A CodeDOM object representing the object that has been serialized.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	public virtual object Serialize(IDesignerSerializationManager manager, object value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		object result = null;
		bool isComplete = false;
		CodeExpression codeExpression = SerializeCreationExpression(manager, value, out isComplete);
		if (codeExpression != null)
		{
			if (isComplete)
			{
				result = codeExpression;
			}
			else
			{
				CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
				SerializeProperties(manager, codeStatementCollection, value, new Attribute[0]);
				SerializeEvents(manager, codeStatementCollection, value);
				result = codeStatementCollection;
			}
			SetExpression(manager, value, codeExpression);
		}
		return result;
	}

	/// <summary>Serializes the specified value to a CodeDOM expression.</summary>
	/// <param name="manager">The serialization manager to use during serialization.</param>
	/// <param name="value">The object to serialize.</param>
	/// <returns>The serialized value. This returns <see langword="null" /> if no reference expression can be obtained for the specified value, or the value cannot be serialized.</returns>
	[Obsolete("This method has been deprecated. Use SerializeToExpression or GetExpression instead.")]
	protected CodeExpression SerializeToReferenceExpression(IDesignerSerializationManager manager, object value)
	{
		return SerializeToExpression(manager, value);
	}

	/// <summary>Determines which statement group the given statement should belong to.</summary>
	/// <param name="statement">The <see cref="T:System.CodeDom.CodeStatement" /> for which to determine the group.</param>
	/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that <paramref name="statement" /> has been reduced to.</param>
	/// <param name="targetType">The <see cref="T:System.Type" /> of <paramref name="statement" />.</param>
	/// <returns>The name of the component with which <paramref name="statement" /> should be grouped.</returns>
	public virtual string GetTargetComponentName(CodeStatement statement, CodeExpression expression, Type targetType)
	{
		if (expression is CodeFieldReferenceExpression)
		{
			return ((CodeFieldReferenceExpression)expression).FieldName;
		}
		if (expression is CodeVariableReferenceExpression)
		{
			return ((CodeVariableReferenceExpression)expression).VariableName;
		}
		return null;
	}

	/// <summary>Serializes the given member on the given object.</summary>
	/// <param name="manager">The serialization manager to use for serialization.</param>
	/// <param name="owningObject">The object to which is <paramref name="member" /> attached.</param>
	/// <param name="member">The member to serialize.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> representing the serialized state of <paramref name="member" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" />, <paramref name="owningObject" />, or <paramref name="member" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="member" /> is not a serializable type.</exception>
	public virtual CodeStatementCollection SerializeMember(IDesignerSerializationManager manager, object owningObject, MemberDescriptor member)
	{
		if (member == null)
		{
			throw new ArgumentNullException("member");
		}
		if (owningObject == null)
		{
			throw new ArgumentNullException("owningObject");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
		CodeExpression expression = GetExpression(manager, owningObject);
		if (expression == null)
		{
			string text = manager.GetName(owningObject);
			if (text == null)
			{
				text = GetUniqueName(manager, owningObject);
			}
			expression = new CodeVariableReferenceExpression(text);
			SetExpression(manager, owningObject, expression);
		}
		if (member is PropertyDescriptor)
		{
			SerializeProperty(manager, codeStatementCollection, owningObject, (PropertyDescriptor)member);
		}
		if (member is EventDescriptor)
		{
			SerializeEvent(manager, codeStatementCollection, owningObject, (EventDescriptor)member);
		}
		return codeStatementCollection;
	}

	/// <summary>Serializes the given member, accounting for default values.</summary>
	/// <param name="manager">The serialization manager to use for serialization.</param>
	/// <param name="owningObject">The object to which is <paramref name="member" /> attached.</param>
	/// <param name="member">The member to serialize.</param>
	/// <returns>A CodeDom object representing <paramref name="member" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" />, <paramref name="owningObject" />, or <paramref name="member" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="member" /> is not a serializable type.</exception>
	public virtual CodeStatementCollection SerializeMemberAbsolute(IDesignerSerializationManager manager, object owningObject, MemberDescriptor member)
	{
		if (member == null)
		{
			throw new ArgumentNullException("member");
		}
		if (owningObject == null)
		{
			throw new ArgumentNullException("owningObject");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		SerializeAbsoluteContext context = new SerializeAbsoluteContext(member);
		manager.Context.Push(context);
		CodeStatementCollection result = SerializeMember(manager, owningObject, member);
		manager.Context.Pop();
		return result;
	}

	/// <summary>Deserializes the specified serialized CodeDOM object into an object.</summary>
	/// <param name="manager">A serialization manager interface that is used during the deserialization process.</param>
	/// <param name="codeObject">A serialized CodeDOM object to deserialize.</param>
	/// <returns>The deserialized CodeDOM object.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="codeObject" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="codeObject" /> is an unsupported code element.</exception>
	public virtual object Deserialize(IDesignerSerializationManager manager, object codeObject)
	{
		object obj = null;
		if (codeObject is CodeExpression expression)
		{
			obj = DeserializeExpression(manager, null, expression);
		}
		if (codeObject is CodeStatement statement)
		{
			obj = DeserializeStatementToInstance(manager, statement);
		}
		if (codeObject is CodeStatementCollection codeStatementCollection)
		{
			foreach (CodeStatement item in codeStatementCollection)
			{
				if (obj == null)
				{
					obj = DeserializeStatementToInstance(manager, item);
				}
				else
				{
					DeserializeStatement(manager, item);
				}
			}
		}
		return obj;
	}

	/// <summary>Deserializes a single statement.</summary>
	/// <param name="manager">The serialization manager to use for serialization.</param>
	/// <param name="statement">The statement to deserialize.</param>
	/// <returns>An object instance resulting from deserializing <paramref name="statement" />.</returns>
	protected object DeserializeStatementToInstance(IDesignerSerializationManager manager, CodeStatement statement)
	{
		if (statement is CodeAssignStatement { Left: CodeFieldReferenceExpression left } codeAssignStatement)
		{
			return DeserializeExpression(manager, left.FieldName, codeAssignStatement.Right);
		}
		DeserializeStatement(manager, statement);
		return null;
	}
}
