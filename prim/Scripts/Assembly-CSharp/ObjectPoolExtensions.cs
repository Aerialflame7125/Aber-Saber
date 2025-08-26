using System;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPoolExtensions
{
	public static void CreatePool<T>(this T prefab, int initialPoolSize = 0, Action<T> initAction = null) where T : Component
	{
		ObjectPool.CreatePool(prefab, initialPoolSize, initAction);
	}

	public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation) where T : Component
	{
		bool wasInstantiated;
		return ObjectPool.Spawn(prefab, position, rotation, out wasInstantiated);
	}

	public static T Spawn<T>(this T prefab, Vector3 position, Quaternion rotation, out bool wasInstantiated) where T : Component
	{
		return ObjectPool.Spawn(prefab, position, rotation, out wasInstantiated);
	}

	public static void Recycle<T>(this T obj) where T : Component
	{
		ObjectPool.Recycle(obj);
	}

	public static void Recycle(this GameObject obj)
	{
		ObjectPool.Recycle(obj);
	}

	public static void RecycleAll<T>(this T prefab) where T : Component
	{
		ObjectPool.RecycleAll(prefab);
	}

	public static void RecycleAll(this GameObject prefab)
	{
		ObjectPool.RecycleAll(prefab);
	}

	public static int CountPooled<T>(this T prefab) where T : Component
	{
		return ObjectPool.CountPooled(prefab);
	}

	public static int CountPooled(this GameObject prefab)
	{
		return ObjectPool.CountPooled(prefab);
	}

	public static int CountSpawned<T>(this T prefab) where T : Component
	{
		return ObjectPool.CountSpawned(prefab);
	}

	public static int CountSpawned(this GameObject prefab)
	{
		return ObjectPool.CountSpawned(prefab);
	}

	public static List<GameObject> GetSpawned(this GameObject prefab, List<GameObject> list, bool appendList)
	{
		return ObjectPool.GetSpawned(prefab, list, appendList);
	}

	public static List<GameObject> GetSpawned(this GameObject prefab, List<GameObject> list)
	{
		return ObjectPool.GetSpawned(prefab, list, appendList: false);
	}

	public static List<GameObject> GetSpawned(this GameObject prefab)
	{
		return ObjectPool.GetSpawned(prefab, null, appendList: false);
	}

	public static List<T> GetSpawned<T>(this T prefab, List<T> list, bool appendList) where T : Component
	{
		return ObjectPool.GetSpawned(prefab, list, appendList);
	}

	public static List<T> GetSpawned<T>(this T prefab, List<T> list) where T : Component
	{
		return ObjectPool.GetSpawned(prefab, list, appendList: false);
	}

	public static List<T> GetSpawned<T>(this T prefab) where T : Component
	{
		return ObjectPool.GetSpawned(prefab, null, appendList: false);
	}

	public static List<GameObject> GetPooled(this GameObject prefab, List<GameObject> list, bool appendList)
	{
		return ObjectPool.GetPooled(prefab, list, appendList);
	}

	public static List<GameObject> GetPooled(this GameObject prefab, List<GameObject> list)
	{
		return ObjectPool.GetPooled(prefab, list, appendList: false);
	}

	public static List<GameObject> GetPooled(this GameObject prefab)
	{
		return ObjectPool.GetPooled(prefab, null, appendList: false);
	}

	public static List<T> GetPooled<T>(this T prefab, List<T> list, bool appendList) where T : Component
	{
		return ObjectPool.GetPooled(prefab, list, appendList);
	}

	public static List<T> GetPooled<T>(this T prefab, List<T> list) where T : Component
	{
		return ObjectPool.GetPooled(prefab, list, appendList: false);
	}

	public static List<T> GetPooled<T>(this T prefab) where T : Component
	{
		return ObjectPool.GetPooled(prefab, null, appendList: false);
	}
}
