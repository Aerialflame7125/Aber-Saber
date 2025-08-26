using Zenject.Internal;

namespace Zenject;

public class MemoryPool<TValue> : MemoryPoolBase<TValue>, IMemoryPool<TValue>, IFactory<TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory
{
	public TValue Spawn()
	{
		TValue val = GetInternal();
		if (!base.Container.IsValidating)
		{
			Reinitialize(val);
		}
		return val;
	}

	protected virtual void Reinitialize(TValue item)
	{
	}

	TValue IFactory<TValue>.Create()
	{
		return Spawn();
	}

	void IDespawnableMemoryPool<TValue>.Despawn(TValue item)
	{
		Despawn(item);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPool<TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPool<TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MemoryPool<TParam1, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TValue>, IFactory<TParam1, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory
{
	public TValue Spawn(TParam1 param)
	{
		TValue val = GetInternal();
		if (!base.Container.IsValidating)
		{
			Reinitialize(param, val);
		}
		return val;
	}

	protected virtual void Reinitialize(TParam1 p1, TValue item)
	{
	}

	TValue IFactory<TParam1, TValue>.Create(TParam1 p1)
	{
		return Spawn(p1);
	}

	void IDespawnableMemoryPool<TValue>.Despawn(TValue item)
	{
		Despawn(item);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPool<TParam1, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPool<TParam1, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MemoryPool<TParam1, TParam2, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TValue>, IFactory<TParam1, TParam2, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory
{
	public TValue Spawn(TParam1 param1, TParam2 param2)
	{
		TValue val = GetInternal();
		if (!base.Container.IsValidating)
		{
			Reinitialize(param1, param2, val);
		}
		return val;
	}

	protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TValue item)
	{
	}

	TValue IFactory<TParam1, TParam2, TValue>.Create(TParam1 p1, TParam2 p2)
	{
		return Spawn(p1, p2);
	}

	void IDespawnableMemoryPool<TValue>.Despawn(TValue item)
	{
		Despawn(item);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPool<TParam1, TParam2, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPool<TParam1, TParam2, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MemoryPool<TParam1, TParam2, TParam3, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TValue>, IFactory<TParam1, TParam2, TParam3, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory
{
	public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3)
	{
		TValue val = GetInternal();
		if (!base.Container.IsValidating)
		{
			Reinitialize(param1, param2, param3, val);
		}
		return val;
	}

	protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TValue item)
	{
	}

	TValue IFactory<TParam1, TParam2, TParam3, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3)
	{
		return Spawn(p1, p2, p3);
	}

	void IDespawnableMemoryPool<TValue>.Despawn(TValue item)
	{
		Despawn(item);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPool<TParam1, TParam2, TParam3, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPool<TParam1, TParam2, TParam3, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MemoryPool<TParam1, TParam2, TParam3, TParam4, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>, IFactory<TParam1, TParam2, TParam3, TParam4, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory
{
	public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
	{
		TValue val = GetInternal();
		if (!base.Container.IsValidating)
		{
			Reinitialize(param1, param2, param3, param4, val);
		}
		return val;
	}

	protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TValue item)
	{
	}

	TValue IFactory<TParam1, TParam2, TParam3, TParam4, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
	{
		return Spawn(p1, p2, p3, p4);
	}

	void IDespawnableMemoryPool<TValue>.Despawn(TValue item)
	{
		Despawn(item);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory
{
	public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
	{
		TValue val = GetInternal();
		if (!base.Container.IsValidating)
		{
			Reinitialize(param1, param2, param3, param4, param5, val);
		}
		return val;
	}

	protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TValue item)
	{
	}

	TValue IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5)
	{
		return Spawn(p1, p2, p3, p4, p5);
	}

	void IDespawnableMemoryPool<TValue>.Despawn(TValue item)
	{
		Despawn(item);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory
{
	public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
	{
		TValue val = GetInternal();
		if (!base.Container.IsValidating)
		{
			Reinitialize(param1, param2, param3, param4, param5, param6, val);
		}
		return val;
	}

	protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TValue item)
	{
	}

	TValue IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6)
	{
		return Spawn(p1, p2, p3, p4, p5, p6);
	}

	void IDespawnableMemoryPool<TValue>.Despawn(TValue item)
	{
		Despawn(item);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory
{
	public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7)
	{
		TValue val = GetInternal();
		if (!base.Container.IsValidating)
		{
			Reinitialize(param1, param2, param3, param4, param5, param6, param7, val);
		}
		return val;
	}

	protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7, TValue item)
	{
	}

	TValue IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7)
	{
		return Spawn(p1, p2, p3, p4, p5, p6, p7);
	}

	void IDespawnableMemoryPool<TValue>.Despawn(TValue item)
	{
		Despawn(item);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue> : MemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool, IFactory
{
	public TValue Spawn(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8)
	{
		TValue val = GetInternal();
		if (!base.Container.IsValidating)
		{
			Reinitialize(param1, param2, param3, param4, param5, param6, param7, param8, val);
		}
		return val;
	}

	protected virtual void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7, TParam8 p8, TValue item)
	{
	}

	TValue IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>.Create(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7, TParam8 p8)
	{
		return Spawn(p1, p2, p3, p4, p5, p6, p7, p8);
	}

	void IDespawnableMemoryPool<TValue>.Despawn(TValue item)
	{
		Despawn(item);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
