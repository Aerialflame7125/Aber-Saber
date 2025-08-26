using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class ObjectPool : MonoBehaviour
{
	public enum StartupPoolMode
	{
		Awake = 0,
		Start = 1,
		CallManually = 2
	}

	[Serializable]
	public class StartupPool
	{
		public int size;

		public GameObject prefab;
	}

	[SerializeField]
	private StartupPoolMode startupPoolMode;

	[NullAllowed]
	[SerializeField]
	private StartupPool[] startupPools;

	private static ObjectPool _instance;

	private static List<GameObject> tempList = new List<GameObject>(1000);

	private Dictionary<GameObject, List<GameObject>> pooledObjects = new Dictionary<GameObject, List<GameObject>>(1000);

	private Dictionary<GameObject, GameObject> spawnedObjects = new Dictionary<GameObject, GameObject>(1000);

	private bool startupPoolsCreated;

	public static ObjectPool instance
	{
		get
		{
			if (_instance != null)
			{
				return _instance;
			}
			_instance = UnityEngine.Object.FindObjectOfType<ObjectPool>();
			if (_instance != null)
			{
				return _instance;
			}
			GameObject gameObject = new GameObject("ObjectPool");
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			_instance = gameObject.AddComponent<ObjectPool>();
			return _instance;
		}
	}

	private void Awake()
	{
		_instance = this;
		if (startupPoolMode == StartupPoolMode.Awake)
		{
			CreateStartupPools();
		}
	}

	private void Start()
	{
		if (startupPoolMode == StartupPoolMode.Start)
		{
			CreateStartupPools();
		}
	}

	public static void CreateStartupPools()
	{
		if (instance.startupPoolsCreated)
		{
			return;
		}
		instance.startupPoolsCreated = true;
		StartupPool[] array = instance.startupPools;
		if (array != null && array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				CreatePool(array[i].prefab, array[i].size);
			}
		}
	}

	public static void CreatePool<T>(T prefab, int initialPoolSize, Action<T> initAction = null) where T : Component
	{
		CreatePool(prefab.gameObject, initialPoolSize, delegate(GameObject go)
		{
			if (initAction != null)
			{
				initAction(go.GetComponent<T>());
			}
		});
	}

	public static void CreatePool(GameObject prefab, int initialPoolSize, Action<GameObject> initAction = null)
	{
		if (!(prefab != null) || instance.pooledObjects.ContainsKey(prefab))
		{
			return;
		}
		List<GameObject> list = new List<GameObject>();
		instance.pooledObjects.Add(prefab, list);
		if (initialPoolSize <= 0)
		{
			return;
		}
		while (list.Count < initialPoolSize)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
			gameObject.SetActive(false);
			list.Add(gameObject);
			if (initAction != null)
			{
				initAction(gameObject);
			}
		}
	}

	public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, out bool wasInstantiated) where T : Component
	{
		return Spawn(prefab.gameObject, position, rotation, out wasInstantiated).GetComponent<T>();
	}

	public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, out bool wasInstantiated)
	{
		List<GameObject> value;
		GameObject gameObject;
		if (instance.pooledObjects.TryGetValue(prefab, out value))
		{
			gameObject = null;
			if (value.Count > 0)
			{
				while (gameObject == null && value.Count > 0)
				{
					gameObject = value[0];
					value.RemoveAt(0);
				}
				if (gameObject != null)
				{
					Transform transform = gameObject.transform;
					transform.SetPositionAndRotation(position, rotation);
					gameObject.SetActive(true);
					instance.spawnedObjects.Add(gameObject, prefab);
					wasInstantiated = false;
					return gameObject;
				}
			}
			gameObject = UnityEngine.Object.Instantiate(prefab, position, rotation);
			instance.spawnedObjects.Add(gameObject, prefab);
			wasInstantiated = true;
			return gameObject;
		}
		gameObject = UnityEngine.Object.Instantiate(prefab, position, rotation);
		wasInstantiated = true;
		return gameObject;
	}

	public static void Recycle<T>(T obj) where T : Component
	{
		Recycle(obj.gameObject);
	}

	public static void Recycle(GameObject obj)
	{
		GameObject value;
		if (instance.spawnedObjects.TryGetValue(obj, out value))
		{
			Recycle(obj, value);
		}
		else
		{
			UnityEngine.Object.Destroy(obj);
		}
	}

	private static void Recycle(GameObject obj, GameObject prefab)
	{
		instance.pooledObjects[prefab].Add(obj);
		instance.spawnedObjects.Remove(obj);
		obj.SetActive(false);
	}

	public static void RecycleAll<T>(T prefab) where T : Component
	{
		RecycleAll(prefab.gameObject);
	}

	public static void RecycleAll(GameObject prefab)
	{
		foreach (KeyValuePair<GameObject, GameObject> spawnedObject in instance.spawnedObjects)
		{
			if (spawnedObject.Value == prefab)
			{
				tempList.Add(spawnedObject.Key);
			}
		}
		for (int i = 0; i < tempList.Count; i++)
		{
			Recycle(tempList[i]);
		}
		tempList.Clear();
	}

	public static void RecycleAll()
	{
		tempList.AddRange(instance.spawnedObjects.Keys);
		for (int i = 0; i < tempList.Count; i++)
		{
			Recycle(tempList[i]);
		}
		tempList.Clear();
	}

	public static bool IsSpawned(GameObject obj)
	{
		return instance.spawnedObjects.ContainsKey(obj);
	}

	public static int CountPooled<T>(T prefab) where T : Component
	{
		return CountPooled(prefab.gameObject);
	}

	public static int CountPooled(GameObject prefab)
	{
		List<GameObject> value;
		if (instance.pooledObjects.TryGetValue(prefab, out value))
		{
			return value.Count;
		}
		return 0;
	}

	public static int CountSpawned<T>(T prefab) where T : Component
	{
		return CountSpawned(prefab.gameObject);
	}

	public static int CountSpawned(GameObject prefab)
	{
		int num = 0;
		foreach (GameObject value in instance.spawnedObjects.Values)
		{
			if (prefab == value)
			{
				num++;
			}
		}
		return num;
	}

	public static int CountAllPooled()
	{
		int num = 0;
		foreach (List<GameObject> value in instance.pooledObjects.Values)
		{
			num += value.Count;
		}
		return num;
	}

	public static List<GameObject> GetPooled(GameObject prefab, List<GameObject> list, bool appendList)
	{
		if (list == null)
		{
			list = new List<GameObject>();
		}
		if (!appendList)
		{
			list.Clear();
		}
		List<GameObject> value;
		if (instance.pooledObjects.TryGetValue(prefab, out value))
		{
			list.AddRange(value);
		}
		return list;
	}

	public static List<T> GetPooled<T>(T prefab, List<T> list, bool appendList) where T : Component
	{
		if (list == null)
		{
			list = new List<T>();
		}
		if (!appendList)
		{
			list.Clear();
		}
		List<GameObject> value;
		if (instance.pooledObjects.TryGetValue(prefab.gameObject, out value))
		{
			for (int i = 0; i < value.Count; i++)
			{
				list.Add(value[i].GetComponent<T>());
			}
		}
		return list;
	}

	public static List<GameObject> GetSpawned(GameObject prefab, List<GameObject> list, bool appendList)
	{
		if (list == null)
		{
			list = new List<GameObject>();
		}
		if (!appendList)
		{
			list.Clear();
		}
		foreach (KeyValuePair<GameObject, GameObject> spawnedObject in instance.spawnedObjects)
		{
			if (spawnedObject.Value == prefab)
			{
				list.Add(spawnedObject.Key);
			}
		}
		return list;
	}

	public static List<T> GetSpawned<T>(T prefab, List<T> list, bool appendList) where T : Component
	{
		if (list == null)
		{
			list = new List<T>();
		}
		if (!appendList)
		{
			list.Clear();
		}
		GameObject gameObject = prefab.gameObject;
		foreach (KeyValuePair<GameObject, GameObject> spawnedObject in instance.spawnedObjects)
		{
			if (spawnedObject.Value == gameObject)
			{
				list.Add(spawnedObject.Key.GetComponent<T>());
			}
		}
		return list;
	}
}
