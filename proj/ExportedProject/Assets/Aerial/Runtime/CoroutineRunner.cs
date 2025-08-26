using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;

    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                // Try to find an existing instance
                _instance = FindObjectOfType<CoroutineRunner>();

                if (_instance == null)
                {
                    // Otherwise, create a new GameObject
                    GameObject runnerObject = new GameObject("CoroutineRunner");
                    _instance = runnerObject.AddComponent<CoroutineRunner>();
                    DontDestroyOnLoad(runnerObject);
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        // Ensure there's only one
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
