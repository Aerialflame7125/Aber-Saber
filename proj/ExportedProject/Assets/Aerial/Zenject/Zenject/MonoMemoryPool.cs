using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class MonoMemoryPool<TValue> : MemoryPool<TValue> where TValue : Component
{
	private Transform _originalParent;

	[Inject]
	public MonoMemoryPool()
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

	protected override void OnSpawned(TValue item)
	{
		item.gameObject.SetActive(value: true);
	}

	protected override void OnDespawned(TValue item)
	{
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent && _originalParent != null)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoMemoryPool<TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoMemoryPool<TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoMemoryPool<TParam1, TValue> : MemoryPool<TParam1, TValue> where TValue : Component
{
	private Transform _originalParent;

	[Inject]
	public MonoMemoryPool()
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

	protected override void OnSpawned(TValue item)
	{
		item.gameObject.SetActive(value: true);
	}

	protected override void OnDespawned(TValue item)
	{
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent && _originalParent != null)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoMemoryPool<TParam1, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoMemoryPool<TParam1, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoMemoryPool<TParam1, TParam2, TValue> : MemoryPool<TParam1, TParam2, TValue> where TValue : Component
{
	private Transform _originalParent;

	[Inject]
	public MonoMemoryPool()
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

	protected override void OnSpawned(TValue item)
	{
		item.gameObject.SetActive(value: true);
	}

	protected override void OnDespawned(TValue item)
	{
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent && _originalParent != null)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoMemoryPool<TParam1, TParam2, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoMemoryPool<TParam1, TParam2, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoMemoryPool<TParam1, TParam2, TParam3, TValue> : MemoryPool<TParam1, TParam2, TParam3, TValue> where TValue : Component
{
	private Transform _originalParent;

	[Inject]
	public MonoMemoryPool()
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

	protected override void OnSpawned(TValue item)
	{
		item.gameObject.SetActive(value: true);
	}

	protected override void OnDespawned(TValue item)
	{
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent && _originalParent != null)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoMemoryPool<TParam1, TParam2, TParam3, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoMemoryPool<TParam1, TParam2, TParam3, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue> : MemoryPool<TParam1, TParam2, TParam3, TParam4, TValue> where TValue : Component
{
	private Transform _originalParent;

	[Inject]
	public MonoMemoryPool()
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

	protected override void OnSpawned(TValue item)
	{
		item.gameObject.SetActive(value: true);
	}

	protected override void OnDespawned(TValue item)
	{
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent && _originalParent != null)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoMemoryPool<TParam1, TParam2, TParam3, TParam4, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class MonoMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> : MemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> where TValue : Component
{
	private Transform _originalParent;

	[Inject]
	public MonoMemoryPool()
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

	protected override void OnSpawned(TValue item)
	{
		item.gameObject.SetActive(value: true);
	}

	protected override void OnDespawned(TValue item)
	{
		item.gameObject.SetActive(value: false);
		if (item.transform.parent != _originalParent && _originalParent != null)
		{
			item.transform.SetParent(_originalParent, worldPositionStays: false);
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MonoMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoMemoryPool<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
