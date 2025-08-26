using System;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class StaticMemoryPool<TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
{
	private Action<TValue> _onSpawnMethod;

	public Action<TValue> OnSpawnMethod
	{
		set
		{
			_onSpawnMethod = value;
		}
	}

	public StaticMemoryPool(Action<TValue> onSpawnMethod = null, Action<TValue> onDespawnedMethod = null)
		: base(onDespawnedMethod)
	{
		_onSpawnMethod = onSpawnMethod;
	}

	public TValue Spawn()
	{
		TValue val = SpawnInternal();
		if (_onSpawnMethod != null)
		{
			_onSpawnMethod(val);
		}
		return val;
	}
}
[NoReflectionBaking]
public class StaticMemoryPool<TParam1, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
{
	private Action<TParam1, TValue> _onSpawnMethod;

	public Action<TParam1, TValue> OnSpawnMethod
	{
		set
		{
			_onSpawnMethod = value;
		}
	}

	public StaticMemoryPool(Action<TParam1, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
		: base(onDespawnedMethod)
	{
		Assert.IsNotNull(onSpawnMethod);
		_onSpawnMethod = onSpawnMethod;
	}

	public TValue Spawn(TParam1 param)
	{
		TValue val = SpawnInternal();
		if (_onSpawnMethod != null)
		{
			_onSpawnMethod(param, val);
		}
		return val;
	}
}
[NoReflectionBaking]
public class StaticMemoryPool<TParam1, TParam2, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
{
	private Action<TParam1, TParam2, TValue> _onSpawnMethod;

	public Action<TParam1, TParam2, TValue> OnSpawnMethod
	{
		set
		{
			_onSpawnMethod = value;
		}
	}

	public StaticMemoryPool(Action<TParam1, TParam2, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
		: base(onDespawnedMethod)
	{
		Assert.IsNotNull(onSpawnMethod);
		_onSpawnMethod = onSpawnMethod;
	}

	public TValue Spawn(TParam1 p1, TParam2 p2)
	{
		TValue val = SpawnInternal();
		if (_onSpawnMethod != null)
		{
			_onSpawnMethod(p1, p2, val);
		}
		return val;
	}
}
[NoReflectionBaking]
public class StaticMemoryPool<TParam1, TParam2, TParam3, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
{
	private Action<TParam1, TParam2, TParam3, TValue> _onSpawnMethod;

	public Action<TParam1, TParam2, TParam3, TValue> OnSpawnMethod
	{
		set
		{
			_onSpawnMethod = value;
		}
	}

	public StaticMemoryPool(Action<TParam1, TParam2, TParam3, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
		: base(onDespawnedMethod)
	{
		Assert.IsNotNull(onSpawnMethod);
		_onSpawnMethod = onSpawnMethod;
	}

	public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3)
	{
		TValue val = SpawnInternal();
		if (_onSpawnMethod != null)
		{
			_onSpawnMethod(p1, p2, p3, val);
		}
		return val;
	}
}
[NoReflectionBaking]
public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
{
	private Action<TParam1, TParam2, TParam3, TParam4, TValue> _onSpawnMethod;

	public Action<TParam1, TParam2, TParam3, TParam4, TValue> OnSpawnMethod
	{
		set
		{
			_onSpawnMethod = value;
		}
	}

	public StaticMemoryPool(Action<TParam1, TParam2, TParam3, TParam4, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
		: base(onDespawnedMethod)
	{
		Assert.IsNotNull(onSpawnMethod);
		_onSpawnMethod = onSpawnMethod;
	}

	public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4)
	{
		TValue val = SpawnInternal();
		if (_onSpawnMethod != null)
		{
			_onSpawnMethod(p1, p2, p3, p4, val);
		}
		return val;
	}
}
[NoReflectionBaking]
public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
{
	private Action<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> _onSpawnMethod;

	public Action<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> OnSpawnMethod
	{
		set
		{
			_onSpawnMethod = value;
		}
	}

	public StaticMemoryPool(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
		: base(onDespawnedMethod)
	{
		Assert.IsNotNull(onSpawnMethod);
		_onSpawnMethod = onSpawnMethod;
	}

	public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5)
	{
		TValue val = SpawnInternal();
		if (_onSpawnMethod != null)
		{
			_onSpawnMethod(p1, p2, p3, p4, p5, val);
		}
		return val;
	}
}
[NoReflectionBaking]
public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
{
	private Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> _onSpawnMethod;

	public Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> OnSpawnMethod
	{
		set
		{
			_onSpawnMethod = value;
		}
	}

	public StaticMemoryPool(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
		: base(onDespawnedMethod)
	{
		Assert.IsNotNull(onSpawnMethod);
		_onSpawnMethod = onSpawnMethod;
	}

	public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6)
	{
		TValue val = SpawnInternal();
		if (_onSpawnMethod != null)
		{
			_onSpawnMethod(p1, p2, p3, p4, p5, p6, val);
		}
		return val;
	}
}
[NoReflectionBaking]
public class StaticMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> : StaticMemoryPoolBase<TValue>, IMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>, IDespawnableMemoryPool<TValue>, IMemoryPool where TValue : class, new()
{
	private Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> _onSpawnMethod;

	public Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> OnSpawnMethod
	{
		set
		{
			_onSpawnMethod = value;
		}
	}

	public StaticMemoryPool(Action<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> onSpawnMethod, Action<TValue> onDespawnedMethod = null)
		: base(onDespawnedMethod)
	{
		Assert.IsNotNull(onSpawnMethod);
		_onSpawnMethod = onSpawnMethod;
	}

	public TValue Spawn(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7)
	{
		TValue val = SpawnInternal();
		if (_onSpawnMethod != null)
		{
			_onSpawnMethod(p1, p2, p3, p4, p5, p6, p7, val);
		}
		return val;
	}
}
