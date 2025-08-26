namespace System.Web.UI.Design;

/// <summary>Provides properties and methods for selecting a data connection expression that is associated with a control property at design time.</summary>
public class ConnectionStringsExpressionEditor : ExpressionEditor
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ConnectionStringsExpressionEditor" /> class.</summary>
	public ConnectionStringsExpressionEditor()
	{
	}

	/// <summary>Evaluates a connection string expression and provides the design-time value for a control property.</summary>
	/// <param name="expression">A connection string expression to evaluate. The expression does not include the <see langword="ConnectionStrings" /> expression prefix.</param>
	/// <param name="parseTimeData">An object containing additional parsing information for evaluating the expression.</param>
	/// <param name="propertyType">The type of the control property.</param>
	/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
	/// <returns>The object referenced by the evaluated expression string if the expression evaluation succeeded; otherwise, <see langword="null" />.</returns>
	public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an expression editor sheet for a connection string expression.</summary>
	/// <param name="expression">The expression string set for a control property, used to initialize the expression editor sheet. The expression does not include the <see langword="ConnectionStrings" /> expression prefix.</param>
	/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
	/// <returns>An <see cref="T:System.Web.UI.Design.ExpressionEditorSheet" /> instance that defines the connection string expression properties.</returns>
	public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
	{
		throw new NotImplementedException();
	}
}
