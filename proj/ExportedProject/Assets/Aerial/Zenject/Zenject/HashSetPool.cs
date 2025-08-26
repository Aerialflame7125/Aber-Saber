using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject;

public class HashSetPool<T> : StaticMemoryPool<HashSet<T>>
{
	private static HashSetPool<T> _instance = new HashSetPool<T>();

	public static HashSetPool<T> Instance => _instance;

	public HashSetPool()
		: base((Action<HashSet<T>>)null, (Action<HashSet<T>>)null)
	{
		base.OnSpawnMethod = OnSpawned;
		base.OnDespawnedMethod = OnDespawned;
	}

	private static void OnSpawned(HashSet<T> items)
	{
		Assert.That(LinqExtensions.IsEmpty(items));
	}

	private static void OnDespawned(HashSet<T> items)
	{
		items.Clear();
	}
}
