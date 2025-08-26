namespace System.Web.Compilation;

/// <summary>Specifies the prefix attribute to use for the expression builder. This class cannot be inherited. </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public sealed class ExpressionPrefixAttribute : Attribute
{
	private string _expressionPrefix;

	/// <summary>Gets the prefix value for the current <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object.</summary>
	/// <returns>The expression prefix for the configured <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</returns>
	public string ExpressionPrefix => _expressionPrefix;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionPrefixAttribute" /> class.</summary>
	/// <param name="expressionPrefix">The prefix of the current <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="expressionPrefix" /> is null or an empty string ("").</exception>
	public ExpressionPrefixAttribute(string expressionPrefix)
	{
		if (string.IsNullOrEmpty(expressionPrefix))
		{
			throw new ArgumentNullException("expressionPrefix");
		}
		_expressionPrefix = expressionPrefix;
	}
}
