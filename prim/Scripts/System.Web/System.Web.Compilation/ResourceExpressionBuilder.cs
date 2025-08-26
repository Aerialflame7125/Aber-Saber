using System.CodeDom;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Web.UI;

namespace System.Web.Compilation;

/// <summary>Provides code to the page parser for assigning property values on a control.</summary>
[ExpressionEditor("System.Web.UI.Design.ResourceExpressionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[ExpressionPrefix("Resources")]
public class ResourceExpressionBuilder : ExpressionBuilder
{
	/// <summary>Returns a value indicating whether an expression can be evaluated in a page that uses the no-compile feature.</summary>
	/// <returns>
	///     <see langword="true" /> in all cases.</returns>
	public override bool SupportsEvaluate => true;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ResourceExpressionBuilder" /> class.</summary>
	public ResourceExpressionBuilder()
	{
	}

	/// <summary>Returns a value from a resource file.</summary>
	/// <param name="target">The object containing the expression.</param>
	/// <param name="entry">The object that represents information about the property bound to by the expression.</param>
	/// <param name="parsedData">The object containing parsed data as returned by the <see cref="Overload:System.Web.Compilation.ResourceExpressionBuilder.ParseExpression" /> method.</param>
	/// <param name="context">Contextual information for the evaluation of the expression.</param>
	/// <returns>An <see cref="T:System.Object" /> associated with the parsed expression. The parsed expression contains the class name and resource key.</returns>
	public override object EvaluateExpression(object target, BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
	{
		ResourceExpressionFields resourceExpressionFields = parsedData as ResourceExpressionFields;
		return HttpContext.GetGlobalResourceObject(resourceExpressionFields.ClassKey, resourceExpressionFields.ResourceKey);
	}

	/// <summary>Returns a code expression to evaluate during page execution.</summary>
	/// <param name="entry">The property name of the object.</param>
	/// <param name="parsedData">The parsed value of the expression.</param>
	/// <param name="context">Properties for the control or page.</param>
	/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that invokes a method.</returns>
	public override CodeExpression GetCodeExpression(BoundPropertyEntry entry, object parsedData, ExpressionBuilderContext context)
	{
		ResourceExpressionFields resourceExpressionFields = parsedData as ResourceExpressionFields;
		if (entry == null)
		{
			return null;
		}
		if (!string.IsNullOrEmpty(resourceExpressionFields.ClassKey))
		{
			if ((object)entry.PropertyInfo == null)
			{
				return null;
			}
			CodeExpression[] parameters = new CodeExpression[2]
			{
				new CodePrimitiveExpression(resourceExpressionFields.ClassKey),
				new CodePrimitiveExpression(resourceExpressionFields.ResourceKey)
			};
			CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetGlobalResourceObject", parameters);
			return new CodeCastExpression(entry.PropertyInfo.PropertyType, expression);
		}
		return CreateGetLocalResourceObject(entry, resourceExpressionFields.ResourceKey);
	}

	/// <summary>Returns an object that represents the parsed expression.</summary>
	/// <param name="expression">The expression value to be parsed.</param>
	/// <returns>The <see cref="T:System.Web.Compilation.ResourceExpressionFields" /> for the expression.</returns>
	public static ResourceExpressionFields ParseExpression(string expression)
	{
		int num = expression.IndexOf(',');
		if (num == -1)
		{
			return new ResourceExpressionFields(expression.Trim());
		}
		return new ResourceExpressionFields(expression.Substring(0, num).Trim(), expression.Substring(num + 1).Trim());
	}

	/// <summary>Returns an object that represents the parsed expression.</summary>
	/// <param name="expression">The value of the declarative expression.</param>
	/// <param name="propertyType">The type of the property bound to by the expression.</param>
	/// <param name="context">Contextual information for the evaluation of the expression.</param>
	/// <returns>An <see cref="T:System.Object" /> that represents the parsed expression.</returns>
	/// <exception cref="T:System.Web.HttpException">The resource expression cannot be found or is invalid.</exception>
	public override object ParseExpression(string expression, Type propertyType, ExpressionBuilderContext context)
	{
		return ParseExpression(expression);
	}

	internal static CodeExpression CreateGetLocalResourceObject(BoundPropertyEntry bpe, string resname)
	{
		if (bpe == null || string.IsNullOrEmpty(resname))
		{
			return null;
		}
		if (bpe.UseSetAttribute)
		{
			return CreateGetLocalResourceObject(bpe.Type, typeof(string), null, resname);
		}
		return CreateGetLocalResourceObject(bpe.PropertyInfo, resname);
	}

	internal static CodeExpression CreateGetLocalResourceObject(MemberInfo mi, string resname)
	{
		if (string.IsNullOrEmpty(resname))
		{
			return null;
		}
		Type type = null;
		if (mi is PropertyInfo)
		{
			type = ((PropertyInfo)mi).PropertyType;
		}
		else
		{
			if (!(mi is FieldInfo))
			{
				return null;
			}
			type = ((FieldInfo)mi).FieldType;
		}
		return CreateGetLocalResourceObject(type, mi.DeclaringType, mi.Name, resname);
	}

	private static CodeExpression CreateGetLocalResourceObject(Type member_type, Type declaringType, string memberName, string resname)
	{
		TypeConverter typeConverter = (string.IsNullOrEmpty(memberName) ? null : TypeDescriptor.GetProperties(declaringType)[memberName].Converter);
		if (member_type != typeof(Color) && (typeConverter == null || typeConverter.CanConvertFrom(typeof(string))))
		{
			CodeMethodInvokeExpression expr = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetLocalResourceObject", new CodePrimitiveExpression(resname));
			return TemplateControlCompiler.CreateConvertToCall(Type.GetTypeCode(member_type), expr);
		}
		if (!string.IsNullOrEmpty(memberName))
		{
			CodeMethodInvokeExpression expression = new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetLocalResourceObject", new CodePrimitiveExpression(resname), new CodeTypeOfExpression(new CodeTypeReference(declaringType)), new CodePrimitiveExpression(memberName));
			return new CodeCastExpression(member_type, expression);
		}
		return null;
	}
}
