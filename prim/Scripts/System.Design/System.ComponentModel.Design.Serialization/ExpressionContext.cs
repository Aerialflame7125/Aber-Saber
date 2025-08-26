using System.CodeDom;

namespace System.ComponentModel.Design.Serialization;

/// <summary>Provides a means of passing context state among serializers. This class cannot be inherited.</summary>
public sealed class ExpressionContext
{
	private object _owner;

	private Type _expressionType;

	private CodeExpression _expression;

	private object _presetValue;

	/// <summary>Gets the preset value of an expression.</summary>
	/// <returns>The preset value of this expression, or <see langword="null" /> if not assigned.</returns>
	public object PresetValue => _presetValue;

	/// <summary>Gets the expression this context represents.</summary>
	/// <returns>The expression this context represents.</returns>
	public CodeExpression Expression => _expression;

	/// <summary>Gets the <see cref="T:System.Type" /> of the expression.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the expression.</returns>
	public Type ExpressionType => _expressionType;

	/// <summary>Gets the object owning this expression.</summary>
	/// <returns>The object owning this expression.</returns>
	public object Owner => _owner;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ExpressionContext" /> class with the given expression and owner.</summary>
	/// <param name="expression">The given code expression.</param>
	/// <param name="expressionType">The given code expression type.</param>
	/// <param name="owner">The given code expression owner.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="expression" />, <paramref name="expressionType" />, or <paramref name="owner" /> is <see langword="null" />.</exception>
	public ExpressionContext(CodeExpression expression, Type expressionType, object owner)
	{
		_expression = expression;
		_expressionType = expressionType;
		_owner = owner;
		_presetValue = null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.ExpressionContext" /> class with a current value.</summary>
	/// <param name="expression">The given code expression.</param>
	/// <param name="expressionType">The given code expression type.</param>
	/// <param name="owner">The given code expression owner.</param>
	/// <param name="presetValue">The given code expression preset value.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="expression" />, <paramref name="expressionType" />, or <paramref name="owner" /> is <see langword="null" />.</exception>
	public ExpressionContext(CodeExpression expression, Type expressionType, object owner, object presetValue)
	{
		_expression = expression;
		_expressionType = expressionType;
		_owner = owner;
		_presetValue = presetValue;
	}
}
