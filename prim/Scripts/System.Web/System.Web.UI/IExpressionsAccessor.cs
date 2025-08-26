namespace System.Web.UI;

/// <summary>Defines the properties a class must implement to support collections of expressions.</summary>
public interface IExpressionsAccessor
{
	/// <summary>Gets a value indicating whether the instance of the class that implements this interface has any properties bound by an expression.</summary>
	/// <returns>
	///     <see langword="true" /> if the control has properties set through expressions; otherwise, <see langword="false" />. </returns>
	bool HasExpressions { get; }

	/// <summary>Gets a collection of <see cref="T:System.Web.UI.ExpressionBinding" /> objects.</summary>
	/// <returns>An <see cref="T:System.Web.UI.ExpressionBindingCollection" /> containing <see cref="T:System.Web.UI.ExpressionBinding" /> objects that represent the properties and expressions for a control.</returns>
	ExpressionBindingCollection Expressions { get; }
}
