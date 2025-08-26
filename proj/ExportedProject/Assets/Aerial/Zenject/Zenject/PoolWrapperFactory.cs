using System;
using Zenject.Internal;

namespace Zenject;

public class PoolWrapperFactory<T> : IFactory<T>, IFactory where T : IDisposable
{
	private readonly IMemoryPool<T> _pool;

	public PoolWrapperFactory(IMemoryPool<T> pool)
	{
		_pool = pool;
	}

	public T Create()
	{
		return _pool.Spawn();
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PoolWrapperFactory<T>((IMemoryPool<T>)P_0[0]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PoolWrapperFactory<T>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[1]
		{
			new InjectableInfo(optional: false, null, "pool", typeof(IMemoryPool<T>), null, InjectSources.Any)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class PoolWrapperFactory<TParam1, TValue> : IFactory<TParam1, TValue>, IFactory where TValue : IDisposable
{
	private readonly IMemoryPool<TParam1, TValue> _pool;

	public PoolWrapperFactory(IMemoryPool<TParam1, TValue> pool)
	{
		_pool = pool;
	}

	public TValue Create(TParam1 arg)
	{
		return _pool.Spawn(arg);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PoolWrapperFactory<TParam1, TValue>((IMemoryPool<TParam1, TValue>)P_0[0]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PoolWrapperFactory<TParam1, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[1]
		{
			new InjectableInfo(optional: false, null, "pool", typeof(IMemoryPool<TParam1, TValue>), null, InjectSources.Any)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
