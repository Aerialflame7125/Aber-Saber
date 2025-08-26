using UnityEngine;

public class SceneInfo : ScriptableObject
{
	[SerializeField]
	private string _sceneName;

	[SerializeField]
	private GameScenesManager _gameScenesManager;

	public string sceneName
	{
		get
		{
			return _sceneName;
		}
	}

	public GameScenesManager gameScenesManager
	{
		get
		{
			return _gameScenesManager;
		}
	}

	public void TransitionToScene(float minDuration)
	{
		_gameScenesManager.TransitionToScene(this, minDuration);
	}
}
