using System.CodeDom;

namespace System.ComponentModel.Design.Serialization;

/// <summary>A <see cref="T:System.ComponentModel.Design.Serialization.CodeDomSerializer" /> adds a root context to provide a definition of the root object. This class cannot be inherited</summary>
public sealed class RootContext
{
	private CodeExpression _expression;

	private object _value;

	/// <summary>Gets the expression representing the root object in the object graph.</summary>
	/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> representing the root object in the object graph.</returns>
	public CodeExpression Expression => _expression;

	/// <summary>Gets the root object of the object graph.</summary>
	/// <returns>The root object of the object graph.</returns>
	public object Value => _value;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootContext" /> class.</summary>
	/// <param name="expression">The expression representing the root object in the object graph.</param>
	/// <param name="value">The root object of the object graph.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="expression" /> or <paramref name="value" /> is <see langword="null" />.</exception>
	public RootContext(CodeExpression expression, object value)
	{
		if (expression == null)
		{
			throw new ArgumentNullException("expression");
		}
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		_expression = expression;
		_value = value;
	}
}
