using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class IFactoryProvider<TContract> : IFactoryProviderBase<TContract>
{
	public IFactoryProvider(DiContainer container, Guid factoryId)
		: base(container, factoryId)
	{
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.That(LinqExtensions.IsEmpty(args));
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		object obj = base.Container.ResolveId(typeof(IFactory<TContract>), base.FactoryId);
		injectAction = null;
		if (base.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TContract)));
		}
		else
		{
			buffer.Add(((IFactory<TContract>)obj).Create());
		}
	}
}
[NoReflectionBaking]
public class IFactoryProvider<TParam1, TContract> : IFactoryProviderBase<TContract>
{
	public IFactoryProvider(DiContainer container, Guid factoryId)
		: base(container, factoryId)
	{
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 1);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		object obj = base.Container.ResolveId(typeof(IFactory<TParam1, TContract>), base.FactoryId);
		injectAction = null;
		if (base.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TContract)));
		}
		else
		{
			buffer.Add(((IFactory<TParam1, TContract>)obj).Create((TParam1)args[0].Value));
		}
	}
}
[NoReflectionBaking]
public class IFactoryProvider<TParam1, TParam2, TContract> : IFactoryProviderBase<TContract>
{
	public IFactoryProvider(DiContainer container, Guid factoryId)
		: base(container, factoryId)
	{
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 2);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		object obj = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TContract>), base.FactoryId);
		injectAction = null;
		if (base.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TContract)));
		}
		else
		{
			buffer.Add(((IFactory<TParam1, TParam2, TContract>)obj).Create((TParam1)args[0].Value, (TParam2)args[1].Value));
		}
	}
}
[NoReflectionBaking]
public class IFactoryProvider<TParam1, TParam2, TParam3, TContract> : IFactoryProviderBase<TContract>
{
	public IFactoryProvider(DiContainer container, Guid factoryId)
		: base(container, factoryId)
	{
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 3);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		object obj = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TParam3, TContract>), base.FactoryId);
		injectAction = null;
		if (base.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TContract)));
		}
		else
		{
			buffer.Add(((IFactory<TParam1, TParam2, TParam3, TContract>)obj).Create((TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value));
		}
	}
}
[NoReflectionBaking]
public class IFactoryProvider<TParam1, TParam2, TParam3, TParam4, TContract> : IFactoryProviderBase<TContract>
{
	public IFactoryProvider(DiContainer container, Guid factoryId)
		: base(container, factoryId)
	{
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 4);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		object obj = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TParam3, TParam4, TContract>), base.FactoryId);
		injectAction = null;
		if (base.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TContract)));
		}
		else
		{
			buffer.Add(((IFactory<TParam1, TParam2, TParam3, TParam4, TContract>)obj).Create((TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value));
		}
	}
}
[NoReflectionBaking]
public class IFactoryProvider<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> : IFactoryProviderBase<TContract>
{
	public IFactoryProvider(DiContainer container, Guid factoryId)
		: base(container, factoryId)
	{
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 5);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam5>(args[4].Type));
		object obj = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>), base.FactoryId);
		injectAction = null;
		if (base.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TContract)));
		}
		else
		{
			buffer.Add(((IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TContract>)obj).Create((TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value));
		}
	}
}
[NoReflectionBaking]
public class IFactoryProvider<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> : IFactoryProviderBase<TContract>
{
	public IFactoryProvider(DiContainer container, Guid factoryId)
		: base(container, factoryId)
	{
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 6);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam5>(args[4].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam6>(args[5].Type));
		object obj = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>), base.FactoryId);
		injectAction = null;
		if (base.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TContract)));
		}
		else
		{
			buffer.Add(((IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract>)obj).Create((TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value, (TParam6)args[5].Value));
		}
	}
}
[NoReflectionBaking]
public class IFactoryProvider<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> : IFactoryProviderBase<TContract>
{
	public IFactoryProvider(DiContainer container, Guid factoryId)
		: base(container, factoryId)
	{
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 10);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam4>(args[3].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam5>(args[4].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam6>(args[5].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam7>(args[6].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam8>(args[7].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam9>(args[8].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam10>(args[9].Type));
		object obj = base.Container.ResolveId(typeof(IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>), base.FactoryId);
		injectAction = null;
		if (base.Container.IsValidating)
		{
			buffer.Add(new ValidationMarker(typeof(TContract)));
		}
		else
		{
			buffer.Add(((IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract>)obj).Create((TParam1)args[0].Value, (TParam2)args[1].Value, (TParam3)args[2].Value, (TParam4)args[3].Value, (TParam5)args[4].Value, (TParam6)args[5].Value, (TParam7)args[6].Value, (TParam8)args[7].Value, (TParam9)args[8].Value, (TParam10)args[9].Value));
		}
	}
}
