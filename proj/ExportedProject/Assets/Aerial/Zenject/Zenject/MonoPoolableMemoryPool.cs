using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class MonoPoolableMemoryPool<TValue> : MemoryPool<TValue> where TValue : Component, IPoolable
{
	private Transform _originalParent;

	[Inject]
	public MonoPoolableMemoryPool()
	{
	}

	protected override void OnCreated(TValue item)
	{
		item.gameObject.SetActive(value: false);
		_originalParent = item.transform.parent;
	}

	protected override void OnDestroyed(TValue item)
	{
		Object.Destroy(item.gameObject);
	}

	protected override void OnDespawned(TValue item)
	{
		item.OnDespawned();
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	protected override void Reinitialize(TValue item)
	{
		item.gameObject.SetActive(value: true);
		item.OnSpawned();
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoPoolableMemoryPool<TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoPoolableMemoryPool<TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoPoolableMemoryPool<TParam1, TValue> : MemoryPool<TParam1, TValue> where TValue : Component, IPoolable<TParam1>
{
	private Transform _originalParent;

	[Inject]
	public MonoPoolableMemoryPool()
	{
	}

	protected override void OnCreated(TValue item)
	{
		item.gameObject.SetActive(value: false);
		_originalParent = item.transform.parent;
	}

	protected override void OnDestroyed(TValue item)
	{
		Object.Destroy(item.gameObject);
	}

	protected override void OnDespawned(TValue item)
	{
		item.OnDespawned();
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	protected override void Reinitialize(TParam1 p1, TValue item)
	{
		item.gameObject.SetActive(value: true);
		item.OnSpawned(p1);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoPoolableMemoryPool<TParam1, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoPoolableMemoryPool<TParam1, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoPoolableMemoryPool<TParam1, TParam2, TValue> : MemoryPool<TParam1, TParam2, TValue> where TValue : Component, IPoolable<TParam1, TParam2>
{
	private Transform _originalParent;

	[Inject]
	public MonoPoolableMemoryPool()
	{
	}

	protected override void OnCreated(TValue item)
	{
		item.gameObject.SetActive(value: false);
		_originalParent = item.transform.parent;
	}

	protected override void OnDestroyed(TValue item)
	{
		Object.Destroy(item.gameObject);
	}

	protected override void OnDespawned(TValue item)
	{
		item.OnDespawned();
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	protected override void Reinitialize(TParam1 p1, TParam2 p2, TValue item)
	{
		item.gameObject.SetActive(value: true);
		item.OnSpawned(p1, p2);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoPoolableMemoryPool<TParam1, TParam2, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoPoolableMemoryPool<TParam1, TParam2, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TValue> : MemoryPool<TParam1, TParam2, TParam3, TValue> where TValue : Component, IPoolable<TParam1, TParam2, TParam3>
{
	private Transform _originalParent;

	[Inject]
	public MonoPoolableMemoryPool()
	{
	}

	protected override void OnCreated(TValue item)
	{
		item.gameObject.SetActive(value: false);
		_originalParent = item.transform.parent;
	}

	protected override void OnDestroyed(TValue item)
	{
		Object.Destroy(item.gameObject);
	}

	protected override void OnDespawned(TValue item)
	{
		item.OnDespawned();
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TValue item)
	{
		item.gameObject.SetActive(value: true);
		item.OnSpawned(p1, p2, p3);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue> : MemoryPool<TParam1, TParam2, TParam3, TParam4, TValue> where TValue : Component, IPoolable<TParam1, TParam2, TParam3, TParam4>
{
	private Transform _originalParent;

	[Inject]
	public MonoPoolableMemoryPool()
	{
	}

	protected override void OnCreated(TValue item)
	{
		item.gameObject.SetActive(value: false);
		_originalParent = item.transform.parent;
	}

	protected override void OnDestroyed(TValue item)
	{
		Object.Destroy(item.gameObject);
	}

	protected override void OnDespawned(TValue item)
	{
		item.OnDespawned();
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TValue item)
	{
		item.gameObject.SetActive(value: true);
		item.OnSpawned(p1, p2, p3, p4);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> where TValue : Component, IPoolable<TParam1, TParam2, TParam3, TParam4, TParam5>
{
	private Transform _originalParent;

	[Inject]
	public MonoPoolableMemoryPool()
	{
	}

	protected override void OnCreated(TValue item)
	{
		item.gameObject.SetActive(value: false);
		_originalParent = item.transform.parent;
	}

	protected override void OnDestroyed(TValue item)
	{
		Object.Destroy(item.gameObject);
	}

	protected override void OnDespawned(TValue item)
	{
		item.OnDespawned();
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TValue item)
	{
		item.gameObject.SetActive(value: true);
		item.OnSpawned(p1, p2, p3, p4, p5);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> where TValue : Component, IPoolable<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>
{
	private Transform _originalParent;

	[Inject]
	public MonoPoolableMemoryPool()
	{
	}

	protected override void OnCreated(TValue item)
	{
		item.gameObject.SetActive(value: false);
		_originalParent = item.transform.parent;
	}

	protected override void OnDestroyed(TValue item)
	{
		Object.Destroy(item.gameObject);
	}

	protected override void OnDespawned(TValue item)
	{
		item.OnDespawned();
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TValue item)
	{
		item.gameObject.SetActive(value: true);
		item.OnSpawned(p1, p2, p3, p4, p5, p6);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue> where TValue : Component, IPoolable<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7>
{
	private Transform _originalParent;

	[Inject]
	public MonoPoolableMemoryPool()
	{
	}

	protected override void OnCreated(TValue item)
	{
		item.gameObject.SetActive(value: false);
		_originalParent = item.transform.parent;
	}

	protected override void OnDestroyed(TValue item)
	{
		Object.Destroy(item.gameObject);
	}

	protected override void OnDespawned(TValue item)
	{
		item.OnDespawned();
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7, TValue item)
	{
		item.gameObject.SetActive(value: true);
		item.OnSpawned(p1, p2, p3, p4, p5, p6, p7);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue> : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue> where TValue : Component, IPoolable<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8>
{
	private Transform _originalParent;

	[Inject]
	public MonoPoolableMemoryPool()
	{
	}

	protected override void OnCreated(TValue item)
	{
		item.gameObject.SetActive(value: false);
		_originalParent = item.transform.parent;
	}

	protected override void OnDestroyed(TValue item)
	{
		Object.Destroy(item.gameObject);
	}

	protected override void OnDespawned(TValue item)
	{
		item.OnDespawned();
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	protected override void Reinitialize(TParam1 p1, TParam2 p2, TParam3 p3, TParam4 p4, TParam5 p5, TParam6 p6, TParam7 p7, TParam8 p8, TValue item)
	{
		item.gameObject.SetActive(value: true);
		item.OnSpawned(p1, p2, p3, p4, p5, p6, p7, p8);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoPoolableMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
