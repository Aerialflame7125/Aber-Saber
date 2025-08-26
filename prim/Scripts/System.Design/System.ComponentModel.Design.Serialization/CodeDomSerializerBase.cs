using System.CodeDom;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Provides a base class for <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> classes.</summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public abstract class CodeDomSerializerBase
{
	private sealed class DeserializationErrorMarker : CodeExpression
	{
		public override bool Equals(object o)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}

	private class ExpressionTable : Hashtable
	{
	}

	private static readonly DeserializationErrorMarker _errorMarker = new DeserializationErrorMarker();

	internal CodeDomSerializerBase()
	{
	}

	/// <summary>Serializes the given object into an expression.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object to serialize. Can be <see langword="null" />.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> object if <paramref name="value" /> can be serialized; otherwise, <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> is <see langword="null" />.</exception>
	protected CodeExpression SerializeToExpression(IDesignerSerializationManager manager, object value)
	{
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		CodeExpression codeExpression = null;
		if (value != null)
		{
			codeExpression = GetExpression(manager, value);
		}
		if (codeExpression == null)
		{
			CodeDomSerializer serializer = GetSerializer(manager, value);
			if (serializer != null)
			{
				object obj = serializer.Serialize(manager, value);
				codeExpression = obj as CodeExpression;
				if (codeExpression == null)
				{
					CodeStatement codeStatement = obj as CodeStatement;
					CodeStatementCollection codeStatementCollection = obj as CodeStatementCollection;
					if (codeStatement != null || codeStatementCollection != null)
					{
						CodeStatementCollection codeStatementCollection2 = null;
						if (manager.Context[typeof(StatementContext)] is StatementContext statementContext && value != null)
						{
							codeStatementCollection2 = statementContext.StatementCollection[value];
						}
						if (codeStatementCollection2 == null)
						{
							codeStatementCollection2 = manager.Context[typeof(CodeStatementCollection)] as CodeStatementCollection;
						}
						if (codeStatementCollection2 != null)
						{
							if (codeStatementCollection != null)
							{
								codeStatementCollection2.AddRange(codeStatementCollection);
							}
							else
							{
								codeStatementCollection2.Add(codeStatement);
							}
						}
					}
				}
				if (codeExpression == null && value != null)
				{
					codeExpression = GetExpression(manager, value);
				}
			}
			else
			{
				ReportError(manager, "No serializer found for type '" + value.GetType().Name + "'");
			}
		}
		return codeExpression;
	}

	/// <summary>Locates a serializer for the given object value.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object specifying the serializer to retrieve.</param>
	/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> that is appropriate for <paramref name="value" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	protected CodeDomSerializer GetSerializer(IDesignerSerializationManager manager, object value)
	{
		DesignerSerializerAttribute designerSerializerAttribute;
		DesignerSerializerAttribute designerSerializerAttribute2 = (designerSerializerAttribute = null);
		CodeDomSerializer codeDomSerializer = null;
		if (value == null)
		{
			return GetSerializer(manager, null);
		}
		foreach (Attribute attribute in TypeDescriptor.GetAttributes(value))
		{
			if (attribute is DesignerSerializerAttribute designerSerializerAttribute3 && manager.GetType(designerSerializerAttribute3.SerializerBaseTypeName) == typeof(CodeDomSerializer))
			{
				designerSerializerAttribute = designerSerializerAttribute3;
				break;
			}
		}
		foreach (Attribute attribute2 in TypeDescriptor.GetAttributes(value.GetType()))
		{
			if (attribute2 is DesignerSerializerAttribute designerSerializerAttribute4 && manager.GetType(designerSerializerAttribute4.SerializerBaseTypeName) == typeof(CodeDomSerializer))
			{
				designerSerializerAttribute2 = designerSerializerAttribute4;
				break;
			}
		}
		if (designerSerializerAttribute2 != null && designerSerializerAttribute != null && designerSerializerAttribute2.SerializerTypeName != designerSerializerAttribute.SerializerTypeName)
		{
			return Activator.CreateInstance(manager.GetType(designerSerializerAttribute.SerializerTypeName)) as CodeDomSerializer;
		}
		return GetSerializer(manager, value.GetType());
	}

	/// <summary>Locates a serializer for the given type.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="valueType">The <see cref="T:System.Type" /> specifying the serializer to retrieve.</param>
	/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> that is appropriate for <paramref name="valueType" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="valueType" /> is <see langword="null" />.</exception>
	protected CodeDomSerializer GetSerializer(IDesignerSerializationManager manager, Type valueType)
	{
		return manager.GetSerializer(valueType, typeof(CodeDomSerializer)) as CodeDomSerializer;
	}

	/// <summary>Returns an expression for the given object.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object for which to get an expression.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> representing <paramref name="value" />, or <see langword="null" /> if there is no existing expression for <paramref name="value" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> is <see langword="null" />.</exception>
	protected CodeExpression GetExpression(IDesignerSerializationManager manager, object value)
	{
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		CodeExpression codeExpression = null;
		if (manager.Context[typeof(ExpressionTable)] is ExpressionTable expressionTable)
		{
			codeExpression = expressionTable[value] as CodeExpression;
		}
		if (codeExpression == null && manager.Context[typeof(RootContext)] is RootContext rootContext && rootContext.Value == value)
		{
			codeExpression = rootContext.Expression;
		}
		if (codeExpression == null)
		{
			string name = manager.GetName(value);
			if ((name == null || name.IndexOf(".") == -1) && manager.GetService(typeof(IReferenceService)) is IReferenceService referenceService)
			{
				name = referenceService.GetName(value);
				if (name != null && name.IndexOf(".") != -1)
				{
					string[] array = name.Split(',');
					value = manager.GetInstance(array[0]);
					if (value != null)
					{
						codeExpression = SerializeToExpression(manager, value);
						if (codeExpression != null)
						{
							for (int i = 1; i < array.Length; i++)
							{
								codeExpression = new CodePropertyReferenceExpression(codeExpression, array[i]);
							}
						}
					}
				}
			}
		}
		return codeExpression;
	}

	/// <summary>Associates an object with an expression.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object to serialize.</param>
	/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> with which to associate <paramref name="value" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" />, <paramref name="value" />, or <paramref name="expression" /> is <see langword="null" />.</exception>
	protected void SetExpression(IDesignerSerializationManager manager, object value, CodeExpression expression)
	{
		SetExpression(manager, value, expression, isPreset: false);
	}

	/// <summary>Associates an object with an expression, optionally specifying a preset expression.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object to serialize.</param>
	/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> with which to associate <paramref name="value" />.</param>
	/// <param name="isPreset">
	///   <see langword="true" /> to specify a preset expression; otherwise, <see langword="false" />.</param>
	protected void SetExpression(IDesignerSerializationManager manager, object value, CodeExpression expression, bool isPreset)
	{
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (expression == null)
		{
			throw new ArgumentNullException("expression");
		}
		ExpressionTable expressionTable = manager.Context[typeof(ExpressionTable)] as ExpressionTable;
		if (expressionTable == null)
		{
			expressionTable = new ExpressionTable();
			manager.Context.Append(expressionTable);
		}
		expressionTable[value] = expression;
	}

	/// <summary>Returns a value indicating whether the given object has been serialized.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object to test for previous serialization.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> has been serialized; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	protected bool IsSerialized(IDesignerSerializationManager manager, object value)
	{
		return IsSerialized(manager, value, honorPreset: false);
	}

	/// <summary>Returns a value indicating whether the given object has been serialized, optionally considering preset expressions.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object to test for previous serialization.</param>
	/// <param name="honorPreset">
	///   <see langword="true" /> to include preset expressions; otherwise, <see langword="false" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="value" /> has been serialized; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	protected bool IsSerialized(IDesignerSerializationManager manager, object value, bool honorPreset)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (GetExpression(manager, value) != null)
		{
			return true;
		}
		return false;
	}

	/// <summary>Returns an expression representing the creation of the given object.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object to serialize.</param>
	/// <param name="isComplete">
	///   <see langword="true" /> if <paramref name="value" /> was fully serialized; otherwise, <see langword="false" />.</param>
	/// <returns>An expression representing the creation of <paramref name="value" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	protected CodeExpression SerializeCreationExpression(IDesignerSerializationManager manager, object value, out bool isComplete)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		CodeExpression result = null;
		TypeConverter converter = TypeDescriptor.GetConverter(value);
		if (converter != null && converter.CanConvertTo(typeof(InstanceDescriptor)))
		{
			InstanceDescriptor instanceDescriptor = converter.ConvertTo(value, typeof(InstanceDescriptor)) as InstanceDescriptor;
			isComplete = instanceDescriptor.IsComplete;
			if (instanceDescriptor != null && instanceDescriptor.MemberInfo != null)
			{
				result = SerializeInstanceDescriptor(manager, instanceDescriptor);
			}
			else
			{
				ReportError(manager, "Unable to serialize to InstanceDescriptor", "Value Type: " + value.GetType().Name + Environment.NewLine + "Value (ToString): " + value.ToString());
			}
		}
		else
		{
			if (value.GetType().GetConstructor(Type.EmptyTypes) != null)
			{
				result = new CodeObjectCreateExpression(value.GetType().FullName);
			}
			isComplete = false;
		}
		return result;
	}

	private CodeExpression SerializeInstanceDescriptor(IDesignerSerializationManager manager, InstanceDescriptor descriptor)
	{
		CodeExpression result = null;
		MemberInfo memberInfo = descriptor.MemberInfo;
		CodeExpression targetObject = new CodeTypeReferenceExpression(memberInfo.DeclaringType);
		if (memberInfo is PropertyInfo)
		{
			result = new CodePropertyReferenceExpression(targetObject, memberInfo.Name);
		}
		else if (memberInfo is FieldInfo)
		{
			result = new CodeFieldReferenceExpression(targetObject, memberInfo.Name);
		}
		else if (memberInfo is MethodInfo)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression(targetObject, memberInfo.Name);
			if (descriptor.Arguments != null && descriptor.Arguments.Count > 0)
			{
				codeMethodInvokeExpression.Parameters.AddRange(SerializeParameters(manager, descriptor.Arguments));
			}
			result = codeMethodInvokeExpression;
		}
		else if (memberInfo is ConstructorInfo)
		{
			CodeObjectCreateExpression codeObjectCreateExpression = new CodeObjectCreateExpression(memberInfo.DeclaringType);
			if (descriptor.Arguments != null && descriptor.Arguments.Count > 0)
			{
				codeObjectCreateExpression.Parameters.AddRange(SerializeParameters(manager, descriptor.Arguments));
			}
			result = codeObjectCreateExpression;
		}
		return result;
	}

	private CodeExpression[] SerializeParameters(IDesignerSerializationManager manager, ICollection parameters)
	{
		CodeExpression[] array = null;
		if (parameters != null && parameters.Count > 0)
		{
			array = new CodeExpression[parameters.Count];
			int num = 0;
			foreach (object parameter in parameters)
			{
				array[num] = SerializeToExpression(manager, parameter);
				num++;
			}
		}
		return array;
	}

	/// <summary>Serializes the given event into the given statement collection.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="statements">The <see cref="T:System.CodeDom.CodeStatementCollection" /> into which the event will be serialized.</param>
	/// <param name="value">The object to which <paramref name="descriptor" /> is bound.</param>
	/// <param name="descriptor">An <see cref="T:System.ComponentModel.EventDescriptor" /> specifying the event to serialize.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" />, <paramref name="value" />, <paramref name="statements" />, or <paramref name="descriptor" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializerException">
	///   <see cref="T:System.ComponentModel.Design.IEventBindingService" /> is not available.</exception>
	protected void SerializeEvent(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, EventDescriptor descriptor)
	{
		if (descriptor == null)
		{
			throw new ArgumentNullException("descriptor");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (statements == null)
		{
			throw new ArgumentNullException("statements");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (manager.GetSerializer(descriptor.GetType(), typeof(MemberCodeDomSerializer)) is MemberCodeDomSerializer memberCodeDomSerializer && memberCodeDomSerializer.ShouldSerialize(manager, value, descriptor))
		{
			memberCodeDomSerializer.Serialize(manager, value, descriptor, statements);
		}
	}

	/// <summary>Serializes the specified events into the given statement collection.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="statements">The <see cref="T:System.CodeDom.CodeStatementCollection" /> into which the event will be serialized.</param>
	/// <param name="value">The object on which events will be serialized.</param>
	/// <param name="filter">An <see cref="T:System.Attribute" /> array that filters which events will be serialized.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" />, <paramref name="value" />, or <paramref name="statements" /> is <see langword="null" />.</exception>
	protected void SerializeEvents(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, params Attribute[] filter)
	{
		if (filter == null)
		{
			throw new ArgumentNullException("filter");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (statements == null)
		{
			throw new ArgumentNullException("statements");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		foreach (EventDescriptor @event in TypeDescriptor.GetEvents(value, filter))
		{
			SerializeEvent(manager, statements, value, @event);
		}
	}

	/// <summary>Serializes a property on the given object.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="statements">The <see cref="T:System.CodeDom.CodeStatementCollection" /> into which the property will be serialized.</param>
	/// <param name="value">The object on which the property will be serialized.</param>
	/// <param name="propertyToSerialize">The property to serialize.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" />, <paramref name="value" />, <paramref name="statements" />, or <paramref name="propertyToSerialize" /> is <see langword="null" />.</exception>
	protected void SerializeProperty(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, PropertyDescriptor propertyToSerialize)
	{
		if (propertyToSerialize == null)
		{
			throw new ArgumentNullException("propertyToSerialize");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (statements == null)
		{
			throw new ArgumentNullException("statements");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (manager.GetSerializer(propertyToSerialize.GetType(), typeof(MemberCodeDomSerializer)) is MemberCodeDomSerializer memberCodeDomSerializer && memberCodeDomSerializer.ShouldSerialize(manager, value, propertyToSerialize))
		{
			memberCodeDomSerializer.Serialize(manager, value, propertyToSerialize, statements);
		}
	}

	/// <summary>Serializes the properties on the given object into the given statement collection.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="statements">The <see cref="T:System.CodeDom.CodeStatementCollection" /> into which the properties will be serialized.</param>
	/// <param name="value">The object on which the properties will be serialized.</param>
	/// <param name="filter">An <see cref="T:System.Attribute" /> array that filters which properties will be serialized.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" />, <paramref name="value" />, or <paramref name="statements" /> is <see langword="null" />.</exception>
	protected void SerializeProperties(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, Attribute[] filter)
	{
		if (filter == null)
		{
			throw new ArgumentNullException("filter");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (statements == null)
		{
			throw new ArgumentNullException("statements");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(value, filter))
		{
			if (!property.Attributes.Contains(DesignerSerializationVisibilityAttribute.Hidden))
			{
				SerializeProperty(manager, statements, value, property);
			}
		}
	}

	/// <summary>Returns an instance of the given type.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="type">The <see cref="T:System.Type" /> of the instance to return.</param>
	/// <param name="parameters">The parameters to pass to the constructor for <paramref name="type" />.</param>
	/// <param name="name">The name of the deserialized object.</param>
	/// <param name="addToContainer">
	///   <see langword="true" /> to add this object to the design container; otherwise, <see langword="false" />. The object must implement <see cref="T:System.ComponentModel.IComponent" /> for this to have any effect.</param>
	/// <returns>An instance of <paramref name="type" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="type" /> is <see langword="null" />.</exception>
	protected virtual object DeserializeInstance(IDesignerSerializationManager manager, Type type, object[] parameters, string name, bool addToContainer)
	{
		if (type == null)
		{
			throw new ArgumentNullException("type");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		return manager.CreateInstance(type, parameters, name, addToContainer);
	}

	/// <summary>Returns a unique name for the given object.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object for which the name will be retrieved.</param>
	/// <returns>A unique name for <paramref name="value" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	protected string GetUniqueName(IDesignerSerializationManager manager, object value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		string text = manager.GetName(value);
		if (text == null)
		{
			text = (manager.GetService(typeof(INameCreationService)) as INameCreationService).CreateName(null, value.GetType());
			if (text == null)
			{
				text = value.GetType().Name.ToLower();
			}
			manager.SetName(value, text);
		}
		return text;
	}

	/// <summary>Deserializes the given expression into an in-memory object.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="name">The name of the object that results from the expression. Can be <see langword="null" /> if there is no need to name the object.</param>
	/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to interpret.</param>
	/// <returns>An object resulting from interpretation of <paramref name="expression" />.</returns>
	protected object DeserializeExpression(IDesignerSerializationManager manager, string name, CodeExpression expression)
	{
		if (expression == null)
		{
			throw new ArgumentNullException("expression");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		bool flag = false;
		object obj = null;
		if (expression is CodeThisReferenceExpression)
		{
			if (manager.Context[typeof(RootContext)] is RootContext rootContext)
			{
				obj = rootContext.Value;
			}
			else if (manager.GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
			{
				obj = designerHost.RootComponent;
			}
		}
		CodeVariableReferenceExpression codeVariableReferenceExpression = expression as CodeVariableReferenceExpression;
		if (obj == null && codeVariableReferenceExpression != null)
		{
			obj = manager.GetInstance(codeVariableReferenceExpression.VariableName);
			if (obj == null)
			{
				ReportError(manager, "Variable '" + codeVariableReferenceExpression.VariableName + "' not initialized prior to reference");
				flag = true;
			}
		}
		CodeFieldReferenceExpression codeFieldReferenceExpression = expression as CodeFieldReferenceExpression;
		if (obj == null && codeFieldReferenceExpression != null)
		{
			obj = manager.GetInstance(codeFieldReferenceExpression.FieldName);
			if (obj == null)
			{
				object obj2 = DeserializeExpression(manager, null, codeFieldReferenceExpression.TargetObject);
				FieldInfo fieldInfo = null;
				fieldInfo = ((!(obj2 is Type)) ? obj2.GetType().GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField) : ((Type)obj2).GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField));
				if (fieldInfo != null)
				{
					obj = fieldInfo.GetValue(obj2);
				}
			}
			if (obj == null)
			{
				ReportError(manager, "Field '" + codeFieldReferenceExpression.FieldName + "' not initialized prior to reference");
			}
		}
		CodePrimitiveExpression codePrimitiveExpression = expression as CodePrimitiveExpression;
		if (obj == null && codePrimitiveExpression != null)
		{
			obj = codePrimitiveExpression.Value;
		}
		CodePropertyReferenceExpression codePropertyReferenceExpression = expression as CodePropertyReferenceExpression;
		if (obj == null && codePropertyReferenceExpression != null)
		{
			object obj3 = DeserializeExpression(manager, null, codePropertyReferenceExpression.TargetObject);
			if (obj3 != null && obj3 != _errorMarker)
			{
				bool flag2 = false;
				if (obj3 is Type)
				{
					PropertyInfo property = ((Type)obj3).GetProperty(codePropertyReferenceExpression.PropertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetProperty);
					if (property != null)
					{
						obj = property.GetValue(null, null);
						flag2 = true;
					}
					FieldInfo field = ((Type)obj3).GetField(codePropertyReferenceExpression.PropertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField);
					if (field != null)
					{
						obj = field.GetValue(null);
						flag2 = true;
					}
				}
				else
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj3)[codePropertyReferenceExpression.PropertyName];
					if (propertyDescriptor != null)
					{
						obj = propertyDescriptor.GetValue(obj3);
						flag2 = true;
					}
					FieldInfo field2 = obj3.GetType().GetField(codePropertyReferenceExpression.PropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField);
					if (field2 != null)
					{
						obj = field2.GetValue(null);
						flag2 = true;
					}
				}
				if (!flag2)
				{
					ReportError(manager, "Missing field '" + codePropertyReferenceExpression.PropertyName + " 'in type " + ((obj3 is Type) ? ((Type)obj3).Name : obj3.GetType().Name) + "'");
					flag = true;
				}
			}
		}
		CodeObjectCreateExpression codeObjectCreateExpression = expression as CodeObjectCreateExpression;
		if (obj == null && codeObjectCreateExpression != null)
		{
			Type type = manager.GetType(codeObjectCreateExpression.CreateType.BaseType);
			if (type == null)
			{
				ReportError(manager, "Type '" + codeObjectCreateExpression.CreateType.BaseType + "' not found.Are you missing a reference?");
				flag = true;
			}
			else
			{
				object[] array = new object[codeObjectCreateExpression.Parameters.Count];
				for (int i = 0; i < codeObjectCreateExpression.Parameters.Count; i++)
				{
					array[i] = DeserializeExpression(manager, null, codeObjectCreateExpression.Parameters[i]);
					if (array[i] == _errorMarker)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					bool addToContainer = false;
					if (typeof(IComponent).IsAssignableFrom(type))
					{
						addToContainer = true;
					}
					obj = DeserializeInstance(manager, type, array, name, addToContainer);
					if (obj == _errorMarker || obj == null)
					{
						string text = "Type to create: " + codeObjectCreateExpression.CreateType.BaseType + Environment.NewLine + "Name: " + name + Environment.NewLine + "addToContainer: " + addToContainer.ToString() + Environment.NewLine + "Parameters Count: " + codeObjectCreateExpression.Parameters.Count + Environment.NewLine;
						for (int j = 0; j < array.Length; j++)
						{
							text = text + "Parameter Number: " + j + Environment.NewLine + "Parameter Type: " + ((array[j] == null) ? "null" : array[j].GetType().Name) + Environment.NewLine + "Parameter '" + j + "' Value: " + array[j].ToString() + Environment.NewLine;
						}
						ReportError(manager, "Unable to create an instance of type '" + codeObjectCreateExpression.CreateType.BaseType + "'", text);
						flag = true;
					}
				}
			}
		}
		CodeArrayCreateExpression codeArrayCreateExpression = expression as CodeArrayCreateExpression;
		if (obj == null && codeArrayCreateExpression != null)
		{
			Type type2 = manager.GetType(codeArrayCreateExpression.CreateType.BaseType);
			if (type2 == null)
			{
				ReportError(manager, "Type '" + codeArrayCreateExpression.CreateType.BaseType + "' not found.Are you missing a reference?");
				flag = true;
			}
			else
			{
				ArrayList arrayList = new ArrayList();
				Type elementType = type2.GetElementType();
				obj = Array.CreateInstance(type2, codeArrayCreateExpression.Initializers.Count);
				for (int k = 0; k < codeArrayCreateExpression.Initializers.Count; k++)
				{
					object obj4 = DeserializeExpression(manager, null, codeArrayCreateExpression.Initializers[k]);
					flag = obj4 == _errorMarker;
					if (!flag)
					{
						if (type2.IsInstanceOfType(obj4))
						{
							arrayList.Add(obj4);
							continue;
						}
						ReportError(manager, "Array initializer element type incompatible with array type.", string.Concat("Array Type: ", type2.Name, Environment.NewLine, "Array Element Type: ", elementType, Environment.NewLine, "Initializer Type: ", (obj4 == null) ? "null" : obj4.GetType().Name));
						flag = true;
					}
				}
				if (!flag)
				{
					arrayList.CopyTo((Array)obj, 0);
				}
			}
		}
		CodeMethodInvokeExpression codeMethodInvokeExpression = expression as CodeMethodInvokeExpression;
		if (obj == null && codeMethodInvokeExpression != null)
		{
			object obj5 = DeserializeExpression(manager, null, codeMethodInvokeExpression.Method.TargetObject);
			object[] array2 = null;
			if (obj5 == _errorMarker || obj5 == null)
			{
				flag = true;
			}
			else
			{
				array2 = new object[codeMethodInvokeExpression.Parameters.Count];
				for (int l = 0; l < codeMethodInvokeExpression.Parameters.Count; l++)
				{
					array2[l] = DeserializeExpression(manager, null, codeMethodInvokeExpression.Parameters[l]);
					if (array2[l] == _errorMarker)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				MethodInfo methodInfo = null;
				methodInfo = ((!(obj5 is Type)) ? GetExactMethod(obj5.GetType(), codeMethodInvokeExpression.Method.MethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, array2) : GetExactMethod((Type)obj5, codeMethodInvokeExpression.Method.MethodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, array2));
				if (methodInfo != null)
				{
					obj = methodInfo.Invoke(obj5, array2);
				}
				else
				{
					string text2 = "Method Name: " + codeMethodInvokeExpression.Method.MethodName + Environment.NewLine + "Method is: " + ((obj5 is Type) ? "static" : "instance") + Environment.NewLine + "Method Holder Type: " + ((obj5 is Type) ? ((Type)obj5).Name : obj5.GetType().Name) + Environment.NewLine + "Parameters Count: " + codeMethodInvokeExpression.Parameters.Count + Environment.NewLine + Environment.NewLine;
					for (int m = 0; m < array2.Length; m++)
					{
						text2 = text2 + "Parameter Number: " + m + Environment.NewLine + "Parameter Type: " + ((array2[m] == null) ? "null" : array2[m].GetType().Name) + Environment.NewLine + "Parameter " + m + " Value: " + array2[m].ToString() + Environment.NewLine;
					}
					ReportError(manager, "Method '" + codeMethodInvokeExpression.Method.MethodName + "' missing in type '" + ((obj5 is Type) ? ((Type)obj5).Name : (obj5.GetType().Name + "'")), text2);
					flag = true;
				}
			}
		}
		CodeTypeReferenceExpression codeTypeReferenceExpression = expression as CodeTypeReferenceExpression;
		if (obj == null && codeTypeReferenceExpression != null)
		{
			obj = manager.GetType(codeTypeReferenceExpression.Type.BaseType);
			if (obj == null)
			{
				ReportError(manager, "Type '" + codeTypeReferenceExpression.Type.BaseType + "' not found.Are you missing a reference?");
				flag = true;
			}
		}
		CodeCastExpression codeCastExpression = expression as CodeCastExpression;
		if (obj == null && codeCastExpression != null)
		{
			Type type3 = manager.GetType(codeCastExpression.TargetType.BaseType);
			object obj6 = DeserializeExpression(manager, null, codeCastExpression.Expression);
			if (obj6 != null && obj6 != _errorMarker && type3 != null)
			{
				if (obj6 is IConvertible convertible)
				{
					try
					{
						obj6 = convertible.ToType(type3, null);
					}
					catch
					{
						flag = true;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					ReportError(manager, "Unable to convert type '" + obj6.GetType().Name + "' to type '" + codeCastExpression.TargetType.BaseType + "'", "Target Type: " + codeCastExpression.TargetType.BaseType + Environment.NewLine + "Instance Type: " + ((obj6 == null) ? "null" : obj6.GetType().Name) + Environment.NewLine + "Instance Value: " + ((obj6 == null) ? "null" : obj6.ToString()) + Environment.NewLine + "Instance is IConvertible: " + (obj6 is IConvertible));
				}
				obj = obj6;
			}
		}
		CodeBinaryOperatorExpression codeBinaryOperatorExpression = expression as CodeBinaryOperatorExpression;
		if (obj == null && codeBinaryOperatorExpression != null)
		{
			string message = null;
			IConvertible convertible2 = null;
			IConvertible convertible3 = null;
			CodeBinaryOperatorType @operator = codeBinaryOperatorExpression.Operator;
			if (@operator == CodeBinaryOperatorType.BitwiseOr)
			{
				convertible2 = DeserializeExpression(manager, null, codeBinaryOperatorExpression.Left) as IConvertible;
				convertible3 = DeserializeExpression(manager, null, codeBinaryOperatorExpression.Right) as IConvertible;
				if (convertible2 is Enum && convertible3 is Enum)
				{
					obj = Enum.ToObject(convertible2.GetType(), Convert.ToInt64(convertible2) | Convert.ToInt64(convertible3));
				}
				else
				{
					message = "CodeBinaryOperatorType.BitwiseOr allowed only on Enum types";
					flag = true;
				}
			}
			else
			{
				message = "Unsupported CodeBinaryOperatorType: " + codeBinaryOperatorExpression.Operator;
				flag = true;
			}
			if (flag)
			{
				string details = "BinaryOperator Type: " + codeBinaryOperatorExpression.Operator.ToString() + Environment.NewLine + "Left Type: " + ((convertible2 == null) ? "null" : convertible2.GetType().Name) + Environment.NewLine + "Left Value: " + ((convertible2 == null) ? "null" : convertible2.ToString()) + Environment.NewLine + "Left Expression Type: " + codeBinaryOperatorExpression.Left.GetType().Name + Environment.NewLine + "Right Type: " + ((convertible3 == null) ? "null" : convertible3.GetType().Name) + Environment.NewLine + "Right Value: " + ((convertible3 == null) ? "null" : convertible3.ToString()) + Environment.NewLine + "Right Expression Type: " + codeBinaryOperatorExpression.Right.GetType().Name;
				ReportError(manager, message, details);
			}
		}
		if (!flag && obj == null && !(expression is CodePrimitiveExpression) && !(expression is CodeMethodInvokeExpression))
		{
			ReportError(manager, "Unsupported Expression Type: " + expression.GetType().Name);
			flag = true;
		}
		if (flag)
		{
			obj = _errorMarker;
		}
		return obj;
	}

	private MethodInfo GetExactMethod(Type type, string methodName, BindingFlags flags, ICollection argsCollection)
	{
		object[] array = null;
		Type[] array2 = Type.EmptyTypes;
		if (argsCollection != null)
		{
			array = new object[argsCollection.Count];
			array2 = new Type[argsCollection.Count];
			argsCollection.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					array2[i] = null;
				}
				else
				{
					array2[i] = array[i].GetType();
				}
			}
		}
		return type.GetMethod(methodName, flags, null, array2, null);
	}

	/// <summary>Deserializes a statement by interpreting and executing a CodeDOM statement.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="statement">The <see cref="T:System.CodeDom.CodeStatement" /> to deserialize.</param>
	protected void DeserializeStatement(IDesignerSerializationManager manager, CodeStatement statement)
	{
		if (statement == null)
		{
			throw new ArgumentNullException("statement");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (statement is CodeAssignStatement statement2)
		{
			DeserializeAssignmentStatement(manager, statement2);
		}
		if (statement is CodeExpressionStatement codeExpressionStatement)
		{
			DeserializeExpression(manager, null, codeExpressionStatement.Expression);
		}
		if (!(statement is CodeAttachEventStatement codeAttachEventStatement))
		{
			return;
		}
		string text = null;
		if (codeAttachEventStatement.Listener is CodeObjectCreateExpression codeObjectCreateExpression && codeObjectCreateExpression.Parameters.Count == 1 && codeObjectCreateExpression.Parameters[0] is CodeMethodReferenceExpression codeMethodReferenceExpression)
		{
			text = codeMethodReferenceExpression.MethodName;
		}
		if (codeAttachEventStatement.Listener is CodeDelegateCreateExpression codeDelegateCreateExpression)
		{
			text = codeDelegateCreateExpression.MethodName;
		}
		CodeMethodReferenceExpression codeMethodReferenceExpression2 = codeAttachEventStatement.Listener as CodeMethodReferenceExpression;
		if (codeMethodReferenceExpression2 != null)
		{
			text = codeMethodReferenceExpression2.MethodName;
		}
		object obj = DeserializeExpression(manager, null, codeAttachEventStatement.Event.TargetObject);
		if (obj == null || obj == _errorMarker || text == null)
		{
			return;
		}
		string text2 = null;
		EventDescriptor eventDescriptor = TypeDescriptor.GetEvents(obj)[codeAttachEventStatement.Event.EventName];
		if (eventDescriptor != null)
		{
			if (manager.GetService(typeof(IEventBindingService)) is IEventBindingService eventBindingService)
			{
				eventBindingService.GetEventProperty(eventDescriptor).SetValue(obj, text);
			}
			else
			{
				text2 = "IEventBindingService missing";
			}
		}
		else
		{
			text2 = "No event '" + codeAttachEventStatement.Event.EventName + "' found in type '" + obj.GetType().Name + "'";
		}
		if (text2 != null)
		{
			ReportError(manager, text2, "Method Name: " + text + Environment.NewLine + "Event Name: " + codeAttachEventStatement.Event.EventName + Environment.NewLine + "Listener Expression Type: " + codeMethodReferenceExpression2.GetType().Name + Environment.NewLine + "Event Holder Type: " + obj.GetType().Name + Environment.NewLine + "Event Holder Expression Type: " + codeAttachEventStatement.Event.TargetObject.GetType().Name);
		}
	}

	private void DeserializeAssignmentStatement(IDesignerSerializationManager manager, CodeAssignStatement statement)
	{
		CodeExpression left = statement.Left;
		if (left is CodePropertyReferenceExpression codePropertyReferenceExpression)
		{
			object obj = DeserializeExpression(manager, null, codePropertyReferenceExpression.TargetObject);
			object obj2 = null;
			if (obj != null && obj != _errorMarker)
			{
				obj2 = DeserializeExpression(manager, null, statement.Right);
			}
			if (obj2 != null && obj2 != _errorMarker && obj != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj)[codePropertyReferenceExpression.PropertyName];
				if (propertyDescriptor != null)
				{
					propertyDescriptor.SetValue(obj, obj2);
				}
				else
				{
					ReportError(manager, "Missing property '" + codePropertyReferenceExpression.PropertyName + "' in type '" + obj.GetType().Name + "'");
				}
			}
		}
		if (left is CodeFieldReferenceExpression { FieldName: not null } codeFieldReferenceExpression)
		{
			object obj3 = DeserializeExpression(manager, null, codeFieldReferenceExpression.TargetObject);
			object obj4 = null;
			if (obj3 != null && obj3 != _errorMarker)
			{
				obj4 = DeserializeExpression(manager, codeFieldReferenceExpression.FieldName, statement.Right);
			}
			FieldInfo fieldInfo = null;
			RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
			if (obj3 != null && obj3 != _errorMarker && obj4 != _errorMarker && (!(codeFieldReferenceExpression.TargetObject is CodeThisReferenceExpression) || rootContext == null || rootContext.Value != obj3))
			{
				fieldInfo = ((!(obj3 is Type)) ? obj3.GetType().GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetField) : ((Type)obj3).GetField(codeFieldReferenceExpression.FieldName, BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField));
				if (fieldInfo != null)
				{
					fieldInfo.SetValue(obj3, obj4);
				}
				else
				{
					ReportError(manager, "Field '" + codeFieldReferenceExpression.FieldName + "' missing in type '" + obj3.GetType().Name + "'", "Field Name: " + codeFieldReferenceExpression.FieldName + Environment.NewLine + "Field is: " + ((obj3 is Type) ? "static" : "instance") + Environment.NewLine + "Field Value: " + ((obj4 == null) ? "null" : obj4.ToString()) + Environment.NewLine + "Field Holder Type: " + obj3.GetType().Name + Environment.NewLine + "Field Holder Expression Type: " + codeFieldReferenceExpression.TargetObject.GetType().Name);
				}
			}
		}
		if (left is CodeVariableReferenceExpression { VariableName: not null } codeVariableReferenceExpression)
		{
			object obj5 = DeserializeExpression(manager, codeVariableReferenceExpression.VariableName, statement.Right);
			if (obj5 != _errorMarker && manager.GetName(obj5) == null)
			{
				manager.SetName(obj5, codeVariableReferenceExpression.VariableName);
			}
		}
	}

	internal void ReportError(IDesignerSerializationManager manager, string message)
	{
		ReportError(manager, message, string.Empty);
	}

	internal void ReportError(IDesignerSerializationManager manager, string message, string details)
	{
		try
		{
			throw new Exception(message);
		}
		catch (Exception ex)
		{
			ex.Data["Details"] = message + Environment.NewLine + Environment.NewLine + details;
			manager.ReportError(ex);
		}
	}

	/// <summary>Serializes the given object into an expression.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object to serialize.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> containing <paramref name="value" /> as a serialized expression.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> is <see langword="null" />.</exception>
	protected CodeExpression SerializeToResourceExpression(IDesignerSerializationManager manager, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Serializes the given object into an expression appropriate for the invariant culture.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object to serialize.</param>
	/// <param name="ensureInvariant">
	///   <see langword="true" /> to serialize into the invariant culture; otherwise, <see langword="false" />.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> containing <paramref name="value" /> as a serialized expression.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> is <see langword="null" />.</exception>
	protected CodeExpression SerializeToResourceExpression(IDesignerSerializationManager manager, object value, bool ensureInvariant)
	{
		throw new NotImplementedException();
	}

	/// <summary>Serializes the properties on the given object into the invariant culture's resource bundle.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="statements">Not used.</param>
	/// <param name="value">The object whose properties will be serialized.</param>
	/// <param name="filter">An <see cref="T:System.Attribute" /> array that filters which properties will be serialized.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" />, <paramref name="value" />, or <paramref name="statements" /> is <see langword="null" />.</exception>
	protected void SerializePropertiesToResources(IDesignerSerializationManager manager, CodeStatementCollection statements, object value, Attribute[] filter)
	{
		throw new NotImplementedException();
	}

	/// <summary>Serializes the given object into a resource bundle using the given resource name.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="resourceName">The name of the resource bundle into which <paramref name="value" /> will be serialized.</param>
	/// <param name="value">The object to serialize.</param>
	protected void SerializeResource(IDesignerSerializationManager manager, string resourceName, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Serializes the given object into a resource bundle using the given resource name.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="resourceName">The name of the resource bundle into which <paramref name="value" /> will be serialized.</param>
	/// <param name="value">The object to serialize.</param>
	protected void SerializeResourceInvariant(IDesignerSerializationManager manager, string resourceName, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Deserializes properties on the given object from the invariant culture's resource bundle.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use for serialization.</param>
	/// <param name="value">The object from which the properties are to be deserialized.</param>
	/// <param name="filter">An <see cref="T:System.Attribute" /> array that filters which properties will be deserialized.</param>
	protected void DeserializePropertiesFromResources(IDesignerSerializationManager manager, object value, Attribute[] filter)
	{
		throw new NotImplementedException();
	}
}
