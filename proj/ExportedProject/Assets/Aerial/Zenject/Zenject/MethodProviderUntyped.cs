using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class MethodProviderUntyped : IProvider
{
	private readonly DiContainer _container;

	private readonly Func<InjectContext, object> _method;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public MethodProviderUntyped(Func<InjectContext, object> method, DiContainer container)
	{
		_container = container;
		_method = method;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return context.MemberType;
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEmpty(args);
		Assert.IsNotNull(context);
		injectAction = null;
		if (_container.IsValidating && !TypeAnalyzer.ShouldAllowDuringValidation(context.MemberType))
		{
			buffer.Add(new ValidationMarker(context.MemberType));
			return;
		}
		object obj = _method(context);
		if (obj == null)
		{
			Assert.That(!TypeExtensions.IsPrimitive(context.MemberType), "Invalid value returned from FromMethod.  Expected non-null.");
		}
		else
		{
			Assert.That(TypeExtensions.DerivesFromOrEqual(obj.GetType(), context.MemberType));
		}
		buffer.Add(obj);
	}
}
