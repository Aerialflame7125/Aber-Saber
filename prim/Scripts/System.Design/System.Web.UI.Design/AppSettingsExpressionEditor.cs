namespace System.Web.UI.Design;

/// <summary>Provides properties and methods for evaluating and editing an application setting expression in a configuration file at design time.</summary>
public class AppSettingsExpressionEditor : ExpressionEditor
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.AppSettingsExpressionEditor" /> class.</summary>
	public AppSettingsExpressionEditor()
	{
	}

	/// <summary>Evaluates an application setting expression string and provides the design-time value for a control property.</summary>
	/// <param name="expression">An application setting expression string to evaluate. <paramref name="expression" /> does not include the <see langword="AppSettings" /> expression prefix.</param>
	/// <param name="parseTimeData">An object containing additional parsing information for evaluating <paramref name="expression" />.</param>
	/// <param name="propertyType">The control property type.</param>
	/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
	/// <returns>The object referenced by <paramref name="expression" />, if the expression evaluation succeeded; otherwise, <see langword="null" />.</returns>
	public override object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an expression editor sheet for an application setting expression.</summary>
	/// <param name="expression">The expression string set for a control property, used to initialize the expression editor sheet. <paramref name="expression" /> does not include the <see langword="AppSettings" /> expression prefix.</param>
	/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
	/// <returns>An <see cref="T:System.Web.UI.Design.ExpressionEditorSheet" /> implementation that defines the application setting expression properties.</returns>
	public override ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
	{
		throw new NotImplementedException();
	}
}
