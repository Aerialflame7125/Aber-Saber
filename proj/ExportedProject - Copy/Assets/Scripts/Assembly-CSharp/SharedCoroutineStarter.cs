using UnityEngine;

public class SharedCoroutineStarter : MonoBehaviour
{
	private static SharedCoroutineStarter _instance;

	public static SharedCoroutineStarter instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = new GameObject("SharedCoroutineStarter");
				gameObject.hideFlags = HideFlags.HideInHierarchy;
				Object.DontDestroyOnLoad(gameObject);
				_instance = gameObject.AddComponent<SharedCoroutineStarter>();
			}
			return _instance;
		}
	}
}
