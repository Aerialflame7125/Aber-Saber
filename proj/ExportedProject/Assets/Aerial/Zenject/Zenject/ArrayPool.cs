using System;
using System.Collections.Generic;

namespace Zenject;

public class ArrayPool<T> : StaticMemoryPoolBaseBase<T[]>
{
	private readonly int _length;

	private static readonly Dictionary<int, ArrayPool<T>> _pools = new Dictionary<int, ArrayPool<T>>();

	public ArrayPool(int length)
		: base((Action<T[]>)OnDespawned)
	{
		_length = length;
	}

	private static void OnDespawned(T[] arr)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = default(T);
		}
	}

	public T[] Spawn()
	{
		return SpawnInternal();
	}

	protected override T[] Alloc()
	{
		return new T[_length];
	}

	public static ArrayPool<T> GetPool(int length)
	{
		if (!_pools.TryGetValue(length, out var value))
		{
			value = new ArrayPool<T>(length);
			_pools.Add(length, value);
		}
		return value;
	}
}
