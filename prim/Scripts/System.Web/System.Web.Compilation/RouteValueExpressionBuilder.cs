using System.CodeDom;
using System.ComponentModel;
using System.Web.Routing;
using System.Web.UI;

namespace System.Web.Compilation;

/// <summary>Retrieves the value that corresponds to a specified URL parameter in a routed page. </summary>
[ExpressionEditor("System.Web.UI.Design.RouteValueExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ExpressionPrefix("Routes")]
public class RouteValueExpressionBuilder : ExpressionBuilder
{
	/// <summary>Gets a value that indicates whether an expression can be evaluated in a page that is not compiled.</summary>
	/// <returns>Always <see langword="true" />.</returns>
	public override bool SupportsEvaluate => true;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.RouteValueExpressionBuilder" /> class.</summary>
	public RouteValueExpressionBuilder()
	{
	}

	/// <summary>Retrieves the value that corresponds to a specified route key.</summary>
	/// <param name="target">The control that the expression is bound to.</param>
	/// <param name="entry">The property that the expression is bound to.</param>
	/// <param name="parsedData">(This parameter is not used in this implementation.)</param>
	/// <param name="context">Properties for the control or page.</param>
	/// <returns>The value that corresponds to the URL parameter that is specified for the current page. The method returns <see langword="null" /> if <paramref name="target" /> is <see langword="null" /> or if it does not derive from <see cref="T:System.Web.UI.Control" />.</returns>
	public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a code expression that is used to perform the property assignment in the generated page class.</summary>
	/// <param name="entry">The property that the expression is bound to.</param>
	/// <param name="parsedData">The object that represents parsed data, as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
	/// <param name="context">Properties for the control or page.</param>
	/// <returns>An expression.</returns>
	public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
	{
		if (entry == null)
		{
			throw new NullReferenceException(".NET emulation (entry == null)");
		}
		CodeMethodInvokeExpression obj = new CodeMethodInvokeExpression
		{
			Method = new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(typeof(RouteValueExpressionBuilder)), "GetRouteValue")
		};
		CodeThisReferenceExpression targetObject = new CodeThisReferenceExpression();
		CodeExpressionCollection parameters = obj.Parameters;
		parameters.Add(new CodePropertyReferenceExpression(targetObject, "Page"));
		parameters.Add(new CodePrimitiveExpression(entry.Expression));
		parameters.Add(new CodeTypeOfExpression(new CodeTypeReference(entry.DeclaringType)));
		parameters.Add(new CodePrimitiveExpression(entry.Name));
		return obj;
	}

	/// <summary>Retrieves the value that corresponds to the specified URL parameter.</summary>
	/// <param name="page">The current page.</param>
	/// <param name="key">The URL parameter.</param>
	/// <param name="controlType">The type of the control that the expression is bound to.</param>
	/// <param name="propertyName">The name of the property that is being set by the expression.</param>
	/// <returns>The value that corresponds to the specified URL parameter for the current page. If <paramref name="page" /> is <see langword="null" />, if the <see cref="P:System.Web.UI.Page.RouteData" /> property of <paramref name="page" /> is <see langword="null" />, or if <paramref name="key" /> is empty or <see langword="null" />, the method returns <see langword="null" />.</returns>
	public static object GetRouteValue(Page page, string key, Type controlType, string propertyName)
	{
		RouteData routeData = page?.RouteData;
		if (routeData == null || string.IsNullOrEmpty(key))
		{
			return null;
		}
		object obj = routeData.Values[key];
		if (obj == null)
		{
			return null;
		}
		if (controlType == null || string.IsNullOrEmpty(propertyName) || !(obj is string))
		{
			return obj;
		}
		PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(controlType);
		if (properties == null || properties.Count == 0)
		{
			return obj;
		}
		PropertyDescriptor propertyDescriptor = properties[propertyName];
		if (propertyDescriptor == null)
		{
			return obj;
		}
		TypeConverter converter = propertyDescriptor.Converter;
		if (converter == null || !converter.CanConvertFrom(typeof(string)))
		{
			return obj;
		}
		return converter.ConvertFrom(obj);
	}
}
