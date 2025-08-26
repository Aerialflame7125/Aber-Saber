using System.Linq.Expressions;

namespace System.Web.Util;

internal sealed class OrderingMethodFinder : ExpressionVisitor
{
	private bool isTopLevelMethodCall = true;

	private bool OrderingMethodFound { get; set; }

	protected override Expression VisitMethodCall(MethodCallExpression node)
	{
		if (isTopLevelMethodCall && QueryableUtility.IsOrderingMethod(node))
		{
			OrderingMethodFound = true;
		}
		isTopLevelMethodCall = false;
		Expression result = base.VisitMethodCall(node);
		isTopLevelMethodCall = true;
		return result;
	}

	internal static bool OrderMethodExists(Expression expression)
	{
		OrderingMethodFinder orderingMethodFinder = new OrderingMethodFinder();
		orderingMethodFinder.OrderingMethodFound = false;
		orderingMethodFinder.Visit(expression);
		return orderingMethodFinder.OrderingMethodFound;
	}
}
