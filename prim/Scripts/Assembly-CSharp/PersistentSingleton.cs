using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	private static object _lock = new object();

	private static bool _applicationIsQuitting = false;

	public static T instance
	{
		get
		{
			if (_applicationIsQuitting)
			{
				Debug.LogWarning(string.Concat("[Singleton] Instance '", typeof(T), "' already destroyed on application quit. Won't create again - returning null."));
				return (T)null;
			}
			lock (_lock)
			{
				if (_instance == null)
				{
					_instance = (T)Object.FindObjectOfType(typeof(T));
					if (Object.FindObjectsOfType(typeof(T)).Length > 1)
					{
						Debug.LogError("[Singleton] Something went really wrong  - there should never be more than 1 singleton! Reopenning the scene might fix it.");
						return _instance;
					}
					if (_instance == null)
					{
						GameObject gameObject = new GameObject();
						_instance = gameObject.AddComponent<T>();
						gameObject.name = typeof(T).ToString();
						Object.DontDestroyOnLoad(gameObject);
					}
				}
				return _instance;
			}
		}
	}

	public static bool IsSingletonAvailable => !_applicationIsQuitting && _instance != null;

	public static void TouchInstance()
	{
		if (!(instance == null))
		{
		}
	}

	private void OnEnable()
	{
		Object.DontDestroyOnLoad(this);
	}

	protected virtual void OnDestroy()
	{
		_applicationIsQuitting = true;
	}
}
