using System;
using System.Linq;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class ConditionCopyNonLazyBinder : CopyNonLazyBinder
{
	public ConditionCopyNonLazyBinder(BindInfo bindInfo)
		: base(bindInfo)
	{
	}

	public CopyNonLazyBinder When(BindingCondition condition)
	{
		base.BindInfo.Condition = condition;
		return this;
	}

	public CopyNonLazyBinder WhenInjectedIntoInstance(object instance)
	{
		return When((InjectContext r) => object.ReferenceEquals(r.ObjectInstance, instance));
	}

	public CopyNonLazyBinder WhenInjectedInto(params Type[] targets)
	{
		return When((InjectContext r) => targets.Where((Type x) => r.ObjectType != null && TypeExtensions.DerivesFromOrEqual(r.ObjectType, x)).Any());
	}

	public CopyNonLazyBinder WhenInjectedInto<T>()
	{
		return When((InjectContext r) => r.ObjectType != null && TypeExtensions.DerivesFromOrEqual(r.ObjectType, typeof(T)));
	}

	public CopyNonLazyBinder WhenNotInjectedInto<T>()
	{
		return When((InjectContext r) => r.ObjectType == null || !TypeExtensions.DerivesFromOrEqual(r.ObjectType, typeof(T)));
	}
}
