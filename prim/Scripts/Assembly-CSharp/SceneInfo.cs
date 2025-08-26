using UnityEngine;

public class SceneInfo : ScriptableObject
{
	[SerializeField]
	private string _sceneName;

	[SerializeField]
	private GameScenesManager _gameScenesManager;

	public string sceneName => _sceneName;

	public GameScenesManager gameScenesManager => _gameScenesManager;

	public void TransitionToScene(float minDuration)
	{
		_gameScenesManager.TransitionToScene(this, minDuration);
	}
}
