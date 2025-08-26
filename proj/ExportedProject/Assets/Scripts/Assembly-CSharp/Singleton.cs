using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T _instance;

	public static T instance
	{
		get
		{
			if (!_instance)
			{
				_instance = (T)Object.FindObjectOfType(typeof(T));
			}
			return _instance;
		}
	}

	public static bool isInstanceAvailable
	{
		get
		{
			if (!_instance)
			{
				_instance = (T)Object.FindObjectOfType(typeof(T));
			}
			return _instance != null;
		}
	}
}
