using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class PoolableMemoryPoolProvider<TContract, TMemoryPool> : PoolableMemoryPoolProviderBase<TContract>, IValidatable where TContract : IPoolable<IMemoryPool> where TMemoryPool : MemoryPool<IMemoryPool, TContract>
{
	private TMemoryPool _pool;

	public PoolableMemoryPoolProvider(DiContainer container, Guid poolId)
		: base(container, poolId)
	{
	}

	public void Validate()
	{
		base.Container.ResolveId<TMemoryPool>(base.PoolId);
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.That(LinqExtensions.IsEmpty(args));
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		injectAction = null;
		if (_pool == null)
		{
			_pool = base.Container.ResolveId<TMemoryPool>(base.PoolId);
		}
		ref TMemoryPool pool = ref _pool;
		TMemoryPool pool2 = _pool;
		buffer.Add(pool.Spawn(pool2));
	}
}
[NoReflectionBaking]
public class PoolableMemoryPoolProvider<TParam1, TContract, TMemoryPool> : PoolableMemoryPoolProviderBase<TContract>, IValidatable where TContract : IPoolable<TParam1, IMemoryPool> where TMemoryPool : MemoryPool<TParam1, IMemoryPool, TContract>
{
	private TMemoryPool _pool;

	public PoolableMemoryPoolProvider(DiContainer container, Guid poolId)
		: base(container, poolId)
	{
	}

	public void Validate()
	{
		base.Container.ResolveId<TMemoryPool>(base.PoolId);
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 1);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		injectAction = null;
		if (_pool == null)
		{
			_pool = base.Container.ResolveId<TMemoryPool>(base.PoolId);
		}
		ref TMemoryPool pool = ref _pool;
		TParam1 param = (TParam1)args[0].Value;
		TMemoryPool pool2 = _pool;
		buffer.Add(pool.Spawn(param, pool2));
	}
}
[NoReflectionBaking]
public class PoolableMemoryPoolProvider<TParam1, TParam2, TContract, TMemoryPool> : PoolableMemoryPoolProviderBase<TContract>, IValidatable where TContract : IPoolable<TParam1, TParam2, IMemoryPool> where TMemoryPool : MemoryPool<TParam1, TParam2, IMemoryPool, TContract>
{
	private TMemoryPool _pool;

	public PoolableMemoryPoolProvider(DiContainer container, Guid poolId)
		: base(container, poolId)
	{
	}

	public void Validate()
	{
		base.Container.ResolveId<TMemoryPool>(base.PoolId);
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 2);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		injectAction = null;
		if (_pool == null)
		{
			_pool = base.Container.ResolveId<TMemoryPool>(base.PoolId);
		}
		ref TMemoryPool pool = ref _pool;
		TParam1 param = (TParam1)args[0].Value;
		TParam2 param2 = (TParam2)args[1].Value;
		TMemoryPool pool2 = _pool;
		buffer.Add(pool.Spawn(param, param2, pool2));
	}
}
[NoReflectionBaking]
public class PoolableMemoryPoolProvider<TParam1, TParam2, TParam3, TContract, TMemoryPool> : PoolableMemoryPoolProviderBase<TContract>, IValidatable where TContract : IPoolable<TParam1, TParam2, TParam3, IMemoryPool> where TMemoryPool : MemoryPool<TParam1, TParam2, TParam3, IMemoryPool, TContract>
{
	private TMemoryPool _pool;

	public PoolableMemoryPoolProvider(DiContainer container, Guid poolId)
		: base(container, poolId)
	{
	}

	public void Validate()
	{
		base.Container.ResolveId<TMemoryPool>(base.PoolId);
	}

	public override void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsEqual(args.Count, 3);
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFromOrEqual(typeof(TContract), context.MemberType));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam1>(args[0].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam2>(args[1].Type));
		Assert.That(TypeExtensions.DerivesFromOrEqual<TParam3>(args[2].Type));
		injectAction = null;
		if (_pool == null)
		{
			_pool = base.Container.ResolveId<TMemoryPool>(base.PoolId);
		}
		ref TMemoryPool pool = ref _pool;
		TParam1 param = (TParam1)args[0].Value;
		TParam2 param2 = (TParam2)args[1].Value;
		TParam3 param3 = (TParam3)args[2].Value;
		TMemoryPool pool2 = _pool;
		buffer.Add(pool.Spawn(param, param2, param3, pool2));
	}
}
[NoReflectionBaking]
public class PoolableMemoryPoolProvider<TParam1, TParam2, TParam3, TParam4, TContract, TMemoryPool> : PoolableMemoryPoolProviderBase<TContract>, IValidatable where TContract : IPoolable<TParam1, TParam2, TParam3, TParam4, IMemoryPool> where TMemoryPool : MemoryPool<TParam1, TParam2, TParam3, TParam4, IMemoryPool, TContract>
{
	private TMemoryPool _pool;

	public PoolableMemoryPoolProvider(DiContainer container, Guid poolId)
		: base(container, poolId)
	{
	}

	public void Validate()
	{
		base.Container.ResolveId<TMemoryPool>(base.PoolId);
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
		injectAction = null;
		if (_pool == null)
		{
			_pool = base.Container.ResolveId<TMemoryPool>(base.PoolId);
		}
		ref TMemoryPool pool = ref _pool;
		TParam1 param = (TParam1)args[0].Value;
		TParam2 param2 = (TParam2)args[1].Value;
		TParam3 param3 = (TParam3)args[2].Value;
		TParam4 param4 = (TParam4)args[3].Value;
		TMemoryPool pool2 = _pool;
		buffer.Add(pool.Spawn(param, param2, param3, param4, pool2));
	}
}
[NoReflectionBaking]
public class PoolableMemoryPoolProvider<TParam1, TParam2, TParam3, TParam4, TParam5, TContract, TMemoryPool> : PoolableMemoryPoolProviderBase<TContract>, IValidatable where TContract : IPoolable<TParam1, TParam2, TParam3, TParam4, TParam5, IMemoryPool> where TMemoryPool : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, IMemoryPool, TContract>
{
	private TMemoryPool _pool;

	public PoolableMemoryPoolProvider(DiContainer container, Guid poolId)
		: base(container, poolId)
	{
	}

	public void Validate()
	{
		base.Container.ResolveId<TMemoryPool>(base.PoolId);
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
		injectAction = null;
		if (_pool == null)
		{
			_pool = base.Container.ResolveId<TMemoryPool>(base.PoolId);
		}
		ref TMemoryPool pool = ref _pool;
		TParam1 param = (TParam1)args[0].Value;
		TParam2 param2 = (TParam2)args[1].Value;
		TParam3 param3 = (TParam3)args[2].Value;
		TParam4 param4 = (TParam4)args[3].Value;
		TParam5 param5 = (TParam5)args[4].Value;
		TMemoryPool pool2 = _pool;
		buffer.Add(pool.Spawn(param, param2, param3, param4, param5, pool2));
	}
}
[NoReflectionBaking]
public class PoolableMemoryPoolProvider<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract, TMemoryPool> : PoolableMemoryPoolProviderBase<TContract>, IValidatable where TContract : IPoolable<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, IMemoryPool> where TMemoryPool : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, IMemoryPool, TContract>
{
	private TMemoryPool _pool;

	public PoolableMemoryPoolProvider(DiContainer container, Guid poolId)
		: base(container, poolId)
	{
	}

	public void Validate()
	{
		base.Container.ResolveId<TMemoryPool>(base.PoolId);
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
		injectAction = null;
		if (_pool == null)
		{
			_pool = base.Container.ResolveId<TMemoryPool>(base.PoolId);
		}
		ref TMemoryPool pool = ref _pool;
		TParam1 param = (TParam1)args[0].Value;
		TParam2 param2 = (TParam2)args[1].Value;
		TParam3 param3 = (TParam3)args[2].Value;
		TParam4 param4 = (TParam4)args[3].Value;
		TParam5 param5 = (TParam5)args[4].Value;
		TParam6 param6 = (TParam6)args[5].Value;
		TMemoryPool pool2 = _pool;
		buffer.Add(pool.Spawn(param, param2, param3, param4, param5, param6, pool2));
	}
}
