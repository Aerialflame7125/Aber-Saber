using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Web.Util;

internal static class QueryableUtility
{
	private static readonly string[] _orderMethods = new string[4] { "OrderBy", "ThenBy", "OrderByDescending", "ThenByDescending" };

	private static readonly MethodInfo[] _methods = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public);

	private static MethodInfo GetQueryableMethod(Expression expression)
	{
		if (expression.NodeType == ExpressionType.Call)
		{
			MethodCallExpression methodCallExpression = (MethodCallExpression)expression;
			if (methodCallExpression.Method.IsStatic && methodCallExpression.Method.DeclaringType == typeof(Queryable))
			{
				return methodCallExpression.Method.GetGenericMethodDefinition();
			}
		}
		return null;
	}

	public static bool IsQueryableMethod(Expression expression, string method)
	{
		return _methods.Where((MethodInfo m) => m.Name == method).Contains(GetQueryableMethod(expression));
	}

	public static bool IsOrderingMethod(Expression expression)
	{
		return _orderMethods.Any((string method) => IsQueryableMethod(expression, method));
	}
}
