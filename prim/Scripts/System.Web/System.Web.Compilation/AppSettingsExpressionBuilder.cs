using System.CodeDom;
using System.ComponentModel;
using System.Reflection;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web.Compilation;

/// <summary>Retrieves values, as specified in a declarative expression, from the <see langword="&lt;appSettings&gt;" /> section of the Web.config file.</summary>
[ExpressionEditor("System.Web.UI.Design.AppSettingsExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ExpressionPrefix("AppSettings")]
public class AppSettingsExpressionBuilder : ExpressionBuilder
{
	/// <summary>Returns a value indicating whether an expression can be evaluated in a page that is not compiled.</summary>
	/// <returns>
	///     <see langword="true" /> in all cases.</returns>
	public override bool SupportsEvaluate => true;

	/// <summary>Returns a value from the <see langword="&lt;appSettings&gt;" /> section of the Web.config file.</summary>
	/// <param name="target">The object that contains the property entry.</param>
	/// <param name="entry">The property to which the expression is bound..</param>
	/// <param name="parsedData">The object that represents parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
	/// <param name="context">Properties for the control or page.</param>
	/// <returns>The <see cref="T:System.Object" /> associated with a key in the <see langword="&lt;appSettings&gt;" /> section of the Web.config file.</returns>
	public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
	{
		return GetAppSetting(entry.Expression.Trim());
	}

	/// <summary>Returns a value from the <see langword="&lt;appSettings&gt;" /> section of the Web.config file.</summary>
	/// <param name="key">The key for the value to be retrieved from the configuration file. </param>
	/// <returns>The <see cref="T:System.Object" /> associated with the key in the <see langword="&lt;appSettings&gt;" /> section of the Web.config file.</returns>
	/// <exception cref="T:System.InvalidOperationException">The key is not found in Web.config.</exception>
	public static object GetAppSetting(string key)
	{
		return WebConfigurationManager.AppSettings[key] ?? throw new InvalidOperationException($"The application setting '{key}' was not found.");
	}

	/// <summary>Returns a value from the <see langword="&lt;appSettings&gt;" /> section of the Web.config file with the value converted to a target type.</summary>
	/// <param name="key">The key for a value to be retrieved from the configuration file.</param>
	/// <param name="targetType">The type of the object that contains the property entry.</param>
	/// <param name="propertyName">The name of the property to which the expression is bound.</param>
	/// <returns>The <see cref="T:System.Object" /> associated with the key in the <see langword="&lt;appSettings&gt;" /> section of the Web.config file.</returns>
	/// <exception cref="T:System.InvalidOperationException">The key is not found in Web.config.- or -The return value could not be converted.</exception>
	public static object GetAppSetting(string key, Type targetType, string propertyName)
	{
		object appSetting = GetAppSetting(key);
		if (targetType == null)
		{
			return appSetting.ToString();
		}
		PropertyInfo property = targetType.GetProperty(propertyName);
		if (property == null)
		{
			return appSetting.ToString();
		}
		try
		{
			return TypeDescriptor.GetConverter(property.PropertyType).ConvertFrom(appSetting);
		}
		catch (NotSupportedException)
		{
			throw new InvalidOperationException($"Could not convert application setting '{appSetting}'  to type '{property.PropertyType.Name}' for property '{property.Name}'.");
		}
	}

	/// <summary>Returns a code expression that is used to perform the property assignment in the generated page class.</summary>
	/// <param name="entry">The property to which the expression is bound.</param>
	/// <param name="parsedData">The object that represents parsed data as returned by <see cref="M:System.Web.Compilation.ExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
	/// <param name="context">Properties for the control or page.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that is used in the property assignment.</returns>
	public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
	{
		PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(entry.DeclaringType)[entry.PropertyInfo.Name];
		CodeExpression[] parameters = new CodeExpression[3]
		{
			new CodePrimitiveExpression(entry.Expression.Trim()),
			new CodeTypeOfExpression(entry.Type),
			new CodePrimitiveExpression(entry.Name)
		};
		return new CodeCastExpression(propertyDescriptor.PropertyType, new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(GetType()), "GetAppSetting", parameters));
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.AppSettingsExpressionBuilder" /> class. </summary>
	public AppSettingsExpressionBuilder()
	{
	}
}
