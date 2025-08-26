using System;
using UnityEngine;

namespace Zenject;

public static class FactoryFromBinder2Extensions
{
	public static ArgConditionCopyNonLazyBinder FromIFactory<TParam1, TParam2, TContract>(this FactoryFromBinder<TParam1, TParam2, TContract> fromBinder, Action<ConcreteBinderGeneric<IFactory<TParam1, TParam2, TContract>>> factoryBindGenerator)
	{
		factoryBindGenerator(fromBinder.CreateIFactoryBinder<IFactory<TParam1, TParam2, TContract>>(out var factoryId));
		fromBinder.ProviderFunc = (DiContainer container) => new IFactoryProvider<TParam1, TParam2, TContract>(container, factoryId);
		return new ArgConditionCopyNonLazyBinder(fromBinder.BindInfo);
	}

	public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TParam1, TParam2, TContract>(this FactoryFromBinder<TParam1, TParam2, TContract> fromBinder) where TContract : IPoolable<TParam1, TParam2, IMemoryPool>
	{
		return FromPoolableMemoryPool(fromBinder, delegate
		{
		});
	}

	public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TParam1, TParam2, TContract>(this FactoryFromBinder<TParam1, TParam2, TContract> fromBinder, Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator) where TContract : IPoolable<TParam1, TParam2, IMemoryPool>
	{
		return FromPoolableMemoryPool<TParam1, TParam2, TContract, PoolableMemoryPool<TParam1, TParam2, IMemoryPool, TContract>>(fromBinder, poolBindGenerator);
	}

	public static ArgConditionCopyNonLazyBinder FromMonoPoolableMemoryPool<TParam1, TParam2, TContract>(this FactoryFromBinder<TParam1, TParam2, TContract> fromBinder) where TContract : Component, IPoolable<TParam1, TParam2, IMemoryPool>
	{
		return FromMonoPoolableMemoryPool(fromBinder, delegate
		{
		});
	}

	public static ArgConditionCopyNonLazyBinder FromMonoPoolableMemoryPool<TParam1, TParam2, TContract>(this FactoryFromBinder<TParam1, TParam2, TContract> fromBinder, Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator) where TContract : Component, IPoolable<TParam1, TParam2, IMemoryPool>
	{
		return FromPoolableMemoryPool<TParam1, TParam2, TContract, MonoPoolableMemoryPool<TParam1, TParam2, IMemoryPool, TContract>>(fromBinder, poolBindGenerator);
	}

	public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TParam1, TParam2, TContract, TMemoryPool>(this FactoryFromBinder<TParam1, TParam2, TContract> fromBinder) where TContract : IPoolable<TParam1, TParam2, IMemoryPool> where TMemoryPool : MemoryPool<TParam1, TParam2, IMemoryPool, TContract>
	{
		return FromPoolableMemoryPool<TParam1, TParam2, TContract, TMemoryPool>(fromBinder, delegate
		{
		});
	}

	public static ArgConditionCopyNonLazyBinder FromPoolableMemoryPool<TParam1, TParam2, TContract, TMemoryPool>(this FactoryFromBinder<TParam1, TParam2, TContract> fromBinder, Action<MemoryPoolInitialSizeMaxSizeBinder<TContract>> poolBindGenerator) where TContract : IPoolable<TParam1, TParam2, IMemoryPool> where TMemoryPool : MemoryPool<TParam1, TParam2, IMemoryPool, TContract>
	{
		Guid poolId = Guid.NewGuid();
		MemoryPoolInitialSizeMaxSizeBinder<TContract> memoryPoolInitialSizeMaxSizeBinder = fromBinder.BindContainer.BindMemoryPoolCustomInterfaceNoFlush<TContract, TMemoryPool, TMemoryPool>().WithId(poolId);
		memoryPoolInitialSizeMaxSizeBinder.NonLazy();
		poolBindGenerator(memoryPoolInitialSizeMaxSizeBinder);
		fromBinder.ProviderFunc = (DiContainer container) => new PoolableMemoryPoolProvider<TParam1, TParam2, TContract, TMemoryPool>(container, poolId);
		return new ArgConditionCopyNonLazyBinder(fromBinder.BindInfo);
	}
}
