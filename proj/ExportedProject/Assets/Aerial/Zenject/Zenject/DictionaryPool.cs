using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject;

public class DictionaryPool<TKey, TValue> : StaticMemoryPool<Dictionary<TKey, TValue>>
{
	private static DictionaryPool<TKey, TValue> _instance = new DictionaryPool<TKey, TValue>();

	public static DictionaryPool<TKey, TValue> Instance => _instance;

	public DictionaryPool()
		: base((Action<Dictionary<TKey, TValue>>)null, (Action<Dictionary<TKey, TValue>>)null)
	{
		base.OnSpawnMethod = OnSpawned;
		base.OnDespawnedMethod = OnDespawned;
	}

	private static void OnSpawned(Dictionary<TKey, TValue> items)
	{
		Assert.That(LinqExtensions.IsEmpty(items));
	}

	private static void OnDespawned(Dictionary<TKey, TValue> items)
	{
		items.Clear();
	}
}
