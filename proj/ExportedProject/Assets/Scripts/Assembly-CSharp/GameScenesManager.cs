using UnityEngine;

public class GameScenesManager : ScriptableObject
{
	[SerializeField]
	[EventSender]
	private FloatGameEvent transitionDidStartEvent;

	public const float kStandardTransitionLength = 0.7f;

	public const float kShortTransitionLength = 0.35f;

	public const float kLongTransitionLength = 1.3f;

	private GameScenesManagerExecutor _executor;

	private GameScenesManagerExecutor executor
	{
		get
		{
			if (!_executor)
			{
				GameObject gameObject = new GameObject("GameScenesManager");
				Object.DontDestroyOnLoad(gameObject);
				_executor = gameObject.AddComponent<GameScenesManagerExecutor>();
				_executor.Init(transitionDidStartEvent);
			}
			return _executor;
		}
	}

	public void TransitionToScene(SceneInfo sceneInfo, float minDuration)
	{
		executor.TransitionToScene(sceneInfo, minDuration);
	}

	public void TransitionToScene(GameSceneSetupData gameSceneSetupData, float minDuration)
	{
		executor.TransitionToScene(gameSceneSetupData, minDuration);
	}

	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
	}
}
