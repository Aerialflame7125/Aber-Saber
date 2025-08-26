using System.CodeDom;
using System.Web.UI;

namespace System.Web.Compilation;

/// <summary>Evaluates expressions during page parsing.</summary>
public abstract class ExpressionBuilder
{
	/// <summary>When overridden in a derived class, returns a value indicating whether the current <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object supports no-compile pages. </summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Compilation.ExpressionBuilder" /> supports expression evaluation; otherwise, <see langword="false" />.</returns>
	public virtual bool SupportsEvaluate => false;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionBuilder" /> class.</summary>
	protected ExpressionBuilder()
	{
	}

	/// <summary>When overridden in a derived class, returns code that is used during page execution to obtain the evaluated expression.</summary>
	/// <param name="entry">The object that represents information about the property bound to by the expression.</param>
	/// <param name="parsedData">The object containing parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />. </param>
	/// <param name="context">Contextual information for the evaluation of the expression.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that is used for property assignment.</returns>
	public abstract CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context);

	/// <summary>When overridden in a derived class, returns an object that represents an evaluated expression.</summary>
	/// <param name="target">The object containing the expression.</param>
	/// <param name="entry">The object that represents information about the property bound to by the expression.</param>
	/// <param name="parsedData">The object containing parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
	/// <param name="context">Contextual information for the evaluation of the expression.</param>
	/// <returns>An object that represents the evaluated expression; otherwise, <see langword="null" /> if the inheritor does not implement <see cref="M:System.Web.Compilation.ExpressionBuilder.EvaluateExpression(System.Object,System.Web.UI.BoundPropertyEntry,System.Object,System.Web.Compilation.ExpressionBuilderContext)" />.</returns>
	public virtual object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
	{
		return null;
	}

	/// <summary>When overridden in a derived class, returns an object that represents the parsed expression.</summary>
	/// <param name="expression">The value of the declarative expression.</param>
	/// <param name="propertyType">The type of the property bound to by the expression.</param>
	/// <param name="context">Contextual information for the evaluation of the expression.</param>
	/// <returns>An <see cref="T:System.Object" /> containing the parsed representation of the expression; otherwise, <see langword="null" /> if <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" /> is not implemented.</returns>
	public virtual object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
	{
		return null;
	}
}
