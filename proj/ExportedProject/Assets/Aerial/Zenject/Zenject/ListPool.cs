using System;
using System.Collections.Generic;

namespace Zenject;

public class ListPool<T> : StaticMemoryPool<List<T>>
{
	private static ListPool<T> _instance = new ListPool<T>();

	public static ListPool<T> Instance => _instance;

	public ListPool()
		: base((Action<List<T>>)null, (Action<List<T>>)null)
	{
		base.OnDespawnedMethod = OnDespawned;
	}

	private void OnDespawned(List<T> list)
	{
		list.Clear();
	}
}
