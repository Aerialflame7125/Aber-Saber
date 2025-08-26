using System.CodeDom;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Serializes collections.</summary>
public class CollectionCodeDomSerializer : CodeDomSerializer
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.CollectionCodeDomSerializer" /> class.</summary>
	public CollectionCodeDomSerializer()
	{
	}

	/// <summary>Verifies serialization support by the <paramref name="method" />.</summary>
	/// <param name="method">The <see cref="T:System.Reflection.MethodInfo" /> to check for serialization attributes.</param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="method" /> supports serialization; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="method" /> is <see langword="null" />.</exception>
	protected bool MethodSupportsSerialization(MethodInfo method)
	{
		return true;
	}

	/// <summary>Serializes the given collection into a CodeDOM object.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use during serialization.</param>
	/// <param name="value">The object to serialize.</param>
	/// <returns>A CodeDOM object representing <paramref name="value" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" /> or <paramref name="value" /> is <see langword="null" />.</exception>
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
		if (!(value is ICollection collection))
		{
			throw new ArgumentException("originalCollection is not an ICollection");
		}
		CodeExpression targetExpression = null;
		ExpressionContext expressionContext = manager.Context[typeof(ExpressionContext)] as ExpressionContext;
		RootContext rootContext = manager.Context[typeof(RootContext)] as RootContext;
		if (expressionContext != null && expressionContext.PresetValue == value)
		{
			targetExpression = expressionContext.Expression;
		}
		else if (rootContext != null)
		{
			targetExpression = rootContext.Expression;
		}
		ArrayList arrayList = new ArrayList();
		foreach (object item in collection)
		{
			arrayList.Add(item);
		}
		return SerializeCollection(manager, targetExpression, value.GetType(), collection, arrayList);
	}

	/// <summary>Serializes the given collection.</summary>
	/// <param name="manager">The <see cref="T:System.ComponentModel.Design.Serialization.IDesignerSerializationManager" /> to use during serialization.</param>
	/// <param name="targetExpression">The <see cref="T:System.CodeDom.CodeExpression" /> that refers to the collection</param>
	/// <param name="targetType">The <see cref="T:System.Type" /> of the collection.</param>
	/// <param name="originalCollection">The collection to serialize.</param>
	/// <param name="valuesToSerialize">The values to serialize.</param>
	/// <returns>Serialized collection if the serialization process succeeded; otherwise, <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="manager" />, <paramref name="targetType" />, <paramref name="originalCollection" />, or <paramref name="valuesToSerialize" /> is <see langword="null" />.</exception>
	protected virtual object SerializeCollection(IDesignerSerializationManager manager, CodeExpression targetExpression, Type targetType, ICollection originalCollection, ICollection valuesToSerialize)
	{
		if (valuesToSerialize == null)
		{
			throw new ArgumentNullException("valuesToSerialize");
		}
		if (originalCollection == null)
		{
			throw new ArgumentNullException("originalCollection");
		}
		if (targetType == null)
		{
			throw new ArgumentNullException("targetType");
		}
		if (manager == null)
		{
			throw new ArgumentNullException("manager");
		}
		if (valuesToSerialize.Count == 0)
		{
			return null;
		}
		MethodInfo methodInfo = null;
		try
		{
			object obj = null;
			IEnumerator enumerator = valuesToSerialize.GetEnumerator();
			enumerator.MoveNext();
			obj = enumerator.Current;
			methodInfo = GetExactMethod(targetType, "Add", new object[1] { obj });
		}
		catch
		{
			ReportError(manager, "A compatible Add/AddRange method is missing in the collection type '" + targetType.Name + "'");
		}
		if (methodInfo == null)
		{
			return null;
		}
		CodeStatementCollection codeStatementCollection = new CodeStatementCollection();
		foreach (object item in valuesToSerialize)
		{
			CodeMethodInvokeExpression codeMethodInvokeExpression = new CodeMethodInvokeExpression();
			codeMethodInvokeExpression.Method = new CodeMethodReferenceExpression(targetExpression, "Add");
			CodeExpression codeExpression = SerializeToExpression(manager, item);
			if (codeExpression != null)
			{
				codeMethodInvokeExpression.Parameters.AddRange(new CodeExpression[1] { codeExpression });
				codeStatementCollection.Add(codeMethodInvokeExpression);
			}
		}
		return codeStatementCollection;
	}

	private MethodInfo GetExactMethod(Type type, string methodName, ICollection argsCollection)
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
		return type.GetMethod(methodName, array2);
	}
}
