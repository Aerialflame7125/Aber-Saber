using System.Configuration;
using System.Web.Compilation;
using System.Web.Configuration;

namespace System.Web.UI.Design;

/// <summary>Defines a set of properties and methods for evaluating an expression that is associated with a control property at design time and to provide an expression editor sheet to the visual design host for use in the expression editor dialog box. This class is abstract.</summary>
public abstract class ExpressionEditor
{
	private Type expressionBuilderType;

	private string prefixFromReflection;

	/// <summary>Gets the expression prefix that identifies expression strings that are supported by the expression editor implementation.</summary>
	/// <returns>A string representing the prefix for expressions supported by the class derived from the <see cref="T:System.Web.UI.Design.ExpressionEditor" />; otherwise, an empty string (""), if the expression editor does not have an associated expression prefix.</returns>
	public string ExpressionPrefix => prefixFromReflection;

	private Type ExpressionBuilderType
	{
		set
		{
			expressionBuilderType = value;
			prefixFromReflection = "";
			object[] customAttributes = expressionBuilderType.GetCustomAttributes(typeof(ExpressionPrefixAttribute), inherit: false);
			if (customAttributes != null && customAttributes.Length != 0)
			{
				ExpressionPrefixAttribute expressionPrefixAttribute = (ExpressionPrefixAttribute)customAttributes[0];
				prefixFromReflection = expressionPrefixAttribute.ExpressionPrefix;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ExpressionEditor" /> class.</summary>
	protected ExpressionEditor()
	{
	}

	/// <summary>Evaluates an expression string and provides the design-time value for a control property.</summary>
	/// <param name="expression">An expression string to evaluate. The expression does not include the expression prefix.</param>
	/// <param name="parseTimeData">An object containing additional parsing information for evaluating <paramref name="expression" />. This typically is provided by the expression builder.</param>
	/// <param name="propertyType">The type of the control property to which <paramref name="expression" /> is bound.</param>
	/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
	/// <returns>The object referenced by the evaluated expression string, if the expression evaluation succeeded; otherwise, <see langword="null" />.</returns>
	public abstract object EvaluateExpression(string expression, object parseTimeData, Type propertyType, IServiceProvider serviceProvider);

	/// <summary>Returns an <see cref="T:System.Web.UI.Design.ExpressionEditor" /> implementation that is associated with the specified expression prefix.</summary>
	/// <param name="expressionPrefix">The expression prefix used to find the associated expression editor.</param>
	/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
	/// <returns>An <see cref="T:System.Web.UI.Design.ExpressionEditor" /> implementation associated with <paramref name="expressionPrefix" />; otherwise, <see langword="null" />, if <paramref name="expressionPrefix" /> is not defined or is not associated with an <see cref="T:System.Web.UI.Design.ExpressionEditor" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="serviceProvider" /> is <see langword="null" />.</exception>
	public static ExpressionEditor GetExpressionEditor(string expressionPrefix, IServiceProvider serviceProvider)
	{
		if (serviceProvider == null)
		{
			return null;
		}
		IWebApplication webApplication = (IWebApplication)serviceProvider.GetService(typeof(IWebApplication));
		if (webApplication == null)
		{
			return null;
		}
		System.Configuration.Configuration configuration = webApplication.OpenWebConfiguration(isReadOnly: true);
		if (configuration == null)
		{
			return null;
		}
		System.Web.Configuration.ExpressionBuilder expressionBuilder = ((CompilationSection)configuration.GetSection("system.web/compilation")).ExpressionBuilders[expressionPrefix];
		if (expressionBuilder == null)
		{
			return null;
		}
		return GetExpressionEditor(Type.GetType(expressionBuilder.Type), serviceProvider);
	}

	/// <summary>Returns an <see cref="T:System.Web.UI.Design.ExpressionEditor" /> implementation that is associated with the specified expression builder type.</summary>
	/// <param name="expressionBuilderType">The type of the derived expression builder class, used to locate the associated expression editor.</param>
	/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
	/// <returns>An <see cref="T:System.Web.UI.Design.ExpressionEditor" /> implementation associated with <paramref name="expressionBuilderType" />; otherwise, <see langword="null" />, if <paramref name="expressionBuilderType" /> cannot be located or has no associated <see cref="T:System.Web.UI.Design.ExpressionEditor" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="expressionBuilderType" /> is <see langword="null" />.  
	/// -or-
	///  <paramref name="serviceProvider" /> is <see langword="null" />.</exception>
	[System.MonoTODO("the docs make it sound like this still requires accessing <expressionBuilders>")]
	public static ExpressionEditor GetExpressionEditor(Type expressionBuilderType, IServiceProvider serviceProvider)
	{
		object[] customAttributes = expressionBuilderType.GetCustomAttributes(typeof(ExpressionEditorAttribute), inherit: false);
		if (customAttributes == null || customAttributes.Length == 0)
		{
			return null;
		}
		ExpressionEditor obj = (ExpressionEditor)Activator.CreateInstance(Type.GetType(((ExpressionEditorAttribute)customAttributes[0]).EditorTypeName));
		obj.ExpressionBuilderType = expressionBuilderType;
		return obj;
	}

	/// <summary>Returns an expression editor sheet that is associated with the current expression editor.</summary>
	/// <param name="expression">The expression string set for a control property, used to initialize the expression editor sheet.</param>
	/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
	/// <returns>An <see cref="T:System.Web.UI.Design.ExpressionEditorSheet" /> that defines the custom expression properties.</returns>
	public virtual ExpressionEditorSheet GetExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
	{
		throw new NotImplementedException();
	}
}
