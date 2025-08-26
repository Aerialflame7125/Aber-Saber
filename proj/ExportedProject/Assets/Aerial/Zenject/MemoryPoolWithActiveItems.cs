using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MemoryPoolWithActiveItems<TValue> : MonoMemoryPool<TValue> where TValue : Component
{
	private HashSet<TValue> _activeItems = new HashSet<TValue>();

	public HashSet<TValue> activeItems => _activeItems;

	protected override void OnSpawned(TValue item)
	{
		base.OnSpawned(item);
		_activeItems.Add(item);
	}

	protected override void OnDespawned(TValue item)
	{
		base.OnDespawned(item);
		_activeItems.Remove(item);
	}

	protected override void OnDestroyed(TValue item)
	{
		base.OnDestroyed(item);
		_activeItems.Remove(item);
	}
}
