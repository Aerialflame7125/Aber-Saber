using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameScenesManagerExecutor : MonoBehaviour
{
	private bool _inTransition;

	private FloatGameEvent _transitionDidStartEvent;

	private GameSceneSetupData _lastUsedGameSceneSetupData;

	public bool IsInTransition => _inTransition;

	public void Init(FloatGameEvent transitionDidStartEvent)
	{
		_transitionDidStartEvent = transitionDidStartEvent;
	}

	public void TransitionToScene(SceneInfo sceneInfo, float minDuration)
	{
		if (!_inTransition)
		{
			_inTransition = true;
			StartCoroutine(TransitionToSceneCoroutine(sceneInfo.sceneName, minDuration));
		}
	}

	public void TransitionToScene(GameSceneSetupData gameSceneSetupData, float minDuration)
	{
		if (!_inTransition)
		{
			_inTransition = true;
			if (_lastUsedGameSceneSetupData != null)
			{
				_lastUsedGameSceneSetupData.ResetWasUsedInTransition();
			}
			gameSceneSetupData.WillBeUsedInTransition();
			_lastUsedGameSceneSetupData = gameSceneSetupData;
			StartCoroutine(TransitionToSceneCoroutine(gameSceneSetupData.sceneInfo.sceneName, minDuration));
		}
	}

	private IEnumerator TransitionToSceneCoroutine(string sceneName, float minDuration)
	{
		EventSystem eventSystem = EventSystem.current;
		if (eventSystem != null)
		{
			eventSystem.enabled = false;
		}
		_transitionDidStartEvent.Raise(minDuration);
		yield return null;
		AsyncOperation loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
		loadSceneOperation.allowSceneActivation = false;
		loadSceneOperation.priority = int.MaxValue;
		yield return new WaitForSeconds(minDuration);
		if (eventSystem != null)
		{
			eventSystem.enabled = true;
		}
		loadSceneOperation.allowSceneActivation = true;
		Shader.WarmupAllShaders();
		_inTransition = false;
	}

	private void Awake()
	{
		_inTransition = false;
	}

	private void OnDestroy()
	{
		if (_lastUsedGameSceneSetupData != null)
		{
			_lastUsedGameSceneSetupData.ResetWasUsedInTransition();
		}
	}
}
