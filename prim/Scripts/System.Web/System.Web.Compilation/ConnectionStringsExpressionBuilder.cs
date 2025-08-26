using System.CodeDom;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI;

namespace System.Web.Compilation;

/// <summary>Retrieves, or generates code to retrieve, values from the &lt;<see langword="connectionStrings" />&gt; section of the Web.config file.</summary>
[ExpressionEditor("System.Web.UI.Design.ConnectionStringsExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ExpressionPrefix("ConnectionStrings")]
public class ConnectionStringsExpressionBuilder : ExpressionBuilder
{
	/// <summary>Returns a value indicating whether an expression can be evaluated in a page that is not compiled.</summary>
	/// <returns>
	///     <see langword="true" /> in all cases.</returns>
	public override bool SupportsEvaluate => true;

	/// <summary>Returns a value from the &lt;<see langword="connectionStrings" />&gt; section of the Web.config file.</summary>
	/// <param name="target">The object that contains the expression.</param>
	/// <param name="entry">The property to which the expression is bound.</param>
	/// <param name="parsedData">The object that represents parsed data as returned by <see cref="M:System.Web.Compilation.ConnectionStringsExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
	/// <param name="context">Properties for the control or page.</param>
	/// <returns>The <see cref="T:System.Object" /> associated with a key in the &lt;<see langword="connectionStrings" />&gt; section of the Web.config file.</returns>
	/// <exception cref="T:System.InvalidOperationException">The connection string name could not be found in the Web.config file.</exception>
	public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
	{
		return GetConnectionString(entry.Expression.Trim());
	}

	/// <summary>Returns a code expression to evaluate during page parsing.</summary>
	/// <param name="entry">An object that represents information about the property bound to by the expression.</param>
	/// <param name="parsedData">The object that represents parsed data as returned by <see cref="M:System.Web.Compilation.ConnectionStringsExpressionBuilder.ParseExpression(System.String,System.Type,System.Web.Compilation.ExpressionBuilderContext)" />.</param>
	/// <param name="context">Properties for the control or page.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that invokes a method.</returns>
	public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
	{
		Pair pair = parsedData as Pair;
		return new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(typeof(ConnectionStringsExpressionBuilder)), ((bool)pair.Second) ? "GetConnectionStringProviderName" : "GetConnectionString", new CodePrimitiveExpression(pair.First));
	}

	/// <summary>Returns a connection string from the &lt;<see langword="connectionStrings" />&gt; section of the Web.config file.</summary>
	/// <param name="connectionStringName">The name of the connection string.</param>
	/// <returns>The connection string as a <see cref="T:System.String" /> for this connection string name.</returns>
	/// <exception cref="T:System.InvalidOperationException">The connection string name could not be found in the Web.config file.</exception>
	public static string GetConnectionString(string connectionStringName)
	{
		ConnectionStringSettings connectionStringSettings = WebConfigurationManager.ConnectionStrings[connectionStringName];
		if (connectionStringSettings == null)
		{
			return string.Empty;
		}
		return connectionStringSettings.ConnectionString;
	}

	/// <summary>Returns the connection string provider from the &lt;<see langword="connectionStrings" />&gt; section of the Web.config file.</summary>
	/// <param name="connectionStringName">The name of the connection string.</param>
	/// <returns>The provider as a <see cref="T:System.String" /> for this connection string name.</returns>
	/// <exception cref="T:System.InvalidOperationException">The connection string name could not be found in the Web.config file.</exception>
	public static string GetConnectionStringProviderName(string connectionStringName)
	{
		ConnectionStringSettings connectionStringSettings = WebConfigurationManager.ConnectionStrings[connectionStringName];
		if (connectionStringSettings == null)
		{
			return string.Empty;
		}
		return connectionStringSettings.ProviderName;
	}

	/// <summary>Returns an object that represents the parsed expression.</summary>
	/// <param name="expression">The value of the declarative expression.</param>
	/// <param name="propertyType">The targeted type for the expression.</param>
	/// <param name="context">Properties for the control or page.</param>
	/// <returns>An <see cref="T:System.Object" /> containing the parsed representation of the expression.</returns>
	public override object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
	{
		bool flag = false;
		string x = string.Empty;
		if (!string.IsNullOrEmpty(expression))
		{
			int num = expression.Length;
			if (expression.EndsWith(".providername", StringComparison.InvariantCultureIgnoreCase))
			{
				flag = true;
				num -= 13;
			}
			else if (expression.EndsWith(".connectionstring", StringComparison.InvariantCultureIgnoreCase))
			{
				num -= 17;
			}
			x = expression.Substring(0, num);
		}
		return new Pair(x, flag);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ConnectionStringsExpressionBuilder" /> class. </summary>
	public ConnectionStringsExpressionBuilder()
	{
	}
}
