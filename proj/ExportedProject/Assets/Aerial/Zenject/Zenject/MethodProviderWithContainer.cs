using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class MethodProviderWithContainer<TValue> : IProvider
{
	private readonly Func<DiContainer, TValue> _method;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public MethodProviderWithContainer(Func<DiContainer, TValue> method)
	{
		_method = method;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return typeof(TValue);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEmpty(args);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TValue), context.MemberType));
		injectAction = null;
		if (context.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TValue)));
		}
		else
		{
			buffer.Add(_method(context.Container));
		}
	}
}
[NoReflectionBaking]
public class MethodProviderWithContainer<TParam1, TValue> : IProvider
{
	private readonly Func<DiContainer, TParam1, TValue> _method;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public MethodProviderWithContainer(Func<DiContainer, TParam1, TValue> method)
	{
		_method = method;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return typeof(TValue);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 1);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TValue), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[0].Type, typeof(TParam1)));
		injectAction = null;
		if (context.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TValue)));
		}
		else
		{
			buffer.Add(_method(context.Container, (TParam1)args[0].Value));
		}
	}
}
[NoReflectionBaking]
public class MethodProviderWithContainer<TParam1, TParam2, TValue> : IProvider
{
	private readonly Func<DiContainer, TParam1, TParam2, TValue> _method;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public MethodProviderWithContainer(Func<DiContainer, TParam1, TParam2, TValue> method)
	{
		_method = method;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return typeof(TValue);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 2);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TValue), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[0].Type, typeof(TParam1)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[1].Type, typeof(TParam2)));
		injectAction = null;
		if (context.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TValue)));
		}
		else
		{
			buffer.Add(_method(context.Container, (TParam1)args[0].Value, (TParam2)args[1].Value));
		}
	}
}
[NoReflectionBaking]
public class MethodProviderWithContainer<TParam1, TParam2, TParam3, TValue> : IProvider
{
	private readonly Func<DiContainer, TParam1, TParam2, TParam3, TValue> _method;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public MethodProviderWithContainer(Func<DiContainer, TParam1, TParam2, TParam3, TValue> method)
	{
		_method = method;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return typeof(TValue);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 3);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TValue), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[0].Type, typeof(TParam1)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[1].Type, typeof(TParam2)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[2].Type, typeof(TParam3)));
		injectAction = null;
		if (context.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TValue)));
		}
		else
		{
			buffer.Add(_method(context.Container, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value));
		}
	}
}
[NoReflectionBaking]
public class MethodProviderWithContainer<TParam1, TParam2, TParam3, TParam4, TValue> : IProvider
{
	private readonly Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TValue> _method;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public MethodProviderWithContainer(Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TValue> method)
	{
		_method = method;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return typeof(TValue);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 4);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TValue), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[0].Type, typeof(TParam1)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[1].Type, typeof(TParam2)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[2].Type, typeof(TParam3)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[3].Type, typeof(TParam4)));
		injectAction = null;
		if (context.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TValue)));
		}
		else
		{
			buffer.Add(_method(context.Container, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value));
		}
	}
}
[NoReflectionBaking]
public class MethodProviderWithContainer<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> : IProvider
{
	private readonly Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TValue> _method;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public MethodProviderWithContainer(Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TValue> method)
	{
		_method = method;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return typeof(TValue);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 5);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TValue), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[0].Type, typeof(TParam1)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[1].Type, typeof(TParam2)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[2].Type, typeof(TParam3)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[3].Type, typeof(TParam4)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[4].Type, typeof(TParam5)));
		injectAction = null;
		if (context.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TValue)));
		}
		else
		{
			buffer.Add(_method(context.Container, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value));
		}
	}
}
[NoReflectionBaking]
public class MethodProviderWithContainer<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> : IProvider
{
	private readonly Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> _method;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public MethodProviderWithContainer(Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> method)
	{
		_method = method;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return typeof(TValue);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 5);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TValue), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[0].Type, typeof(TParam1)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[1].Type, typeof(TParam2)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[2].Type, typeof(TParam3)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[3].Type, typeof(TParam4)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[4].Type, typeof(TParam5)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[5].Type, typeof(TParam6)));
		injectAction = null;
		if (context.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TValue)));
		}
		else
		{
			buffer.Add(_method(context.Container, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value, (TParam6)args[5].Value));
		}
	}
}
[NoReflectionBaking]
public class MethodProviderWithContainer<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TValue> : IProvider
{
	private readonly Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TValue> _method;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public MethodProviderWithContainer(Func<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TValue> method)
	{
		_method = method;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return typeof(TValue);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 10);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TValue), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[0].Type, typeof(TParam1)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[1].Type, typeof(TParam2)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[2].Type, typeof(TParam3)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[3].Type, typeof(TParam4)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[4].Type, typeof(TParam5)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[5].Type, typeof(TParam6)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[6].Type, typeof(TParam7)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[7].Type, typeof(TParam8)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[8].Type, typeof(TParam9)));
		Assert.That(TypeExtensions.DerivesFromOrEqual(args[9].Type, typeof(TParam10)));
		injectAction = null;
		if (context.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TValue)));
		}
		else
		{
			buffer.Add(_method(context.Container, (TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value, (TParam6)args[5].Value, (TParam7)args[6].Value, (TParam8)args[7].Value, (TParam9)args[8].Value, (TParam10)args[9].Value));
		}
	}
}
