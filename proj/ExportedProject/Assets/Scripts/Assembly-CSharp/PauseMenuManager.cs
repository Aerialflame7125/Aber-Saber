using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using BL.Replays;

public class PauseMenuManager : MonoBehaviour
{
	[SerializeField]
	[EventSender]
	private GameEvent _didFinishWithContinueButtonEvent;

	[SerializeField]
	[EventSender]
	private GameEvent _didFinishWithMenuButtonEvent;

	[SerializeField]
	[EventSender]
	private GameEvent _didFinishWithRestartButtonEvent;

	[SerializeField]
	private GameObject _gameObjectsWrapper;

	[SerializeField]
	private ResumePauseAnimationController _resumePauseAnimationController;

	[Space]
	[SerializeField]
	private TextMeshProUGUI _levelNameText;

	[SerializeField]
	private TextMeshProUGUI _levelDifficultyText;

	private bool _ignoreFirstFrameVRControllerInteraction;

	private GameObject _handControllers;

	private void Awake()
	{
		_resumePauseAnimationController.animationDidFinishEvent += HandleResumeAnimationDidFinish;
		base.enabled = true;
		Debug.LogError("PauseMenuManager Awake - base.enabled: " + base.enabled);
	}

	private void Update()
	{
		bool menuButtonDown = VRControllersInputManager.MenuButtonDown();

		if (_ignoreFirstFrameVRControllerInteraction)
		{
			Debug.LogError("Ignoring first frame VR controller interaction - PauseMenuManager");
			_ignoreFirstFrameVRControllerInteraction = false;
		}
		else if (menuButtonDown && !_resumePauseAnimationController.resuming && !_gameObjectsWrapper.activeSelf)
		{
			Debug.LogError("MenuButtonPressed - PauseMenuManager");
			ShowMenu();
		}
		else if (menuButtonDown && _gameObjectsWrapper.activeSelf)
		{
			Debug.LogError("ContinueButtonPressed - PauseMenuManager");
			ContinueButtonPressed();
		}
	}

	public void ShowMenu()
	{
		_ignoreFirstFrameVRControllerInteraction = true;
		_gameObjectsWrapper.SetActive(true);
		_resumePauseAnimationController.StopAnimation();
		Time.timeScale = 0f;
		_handControllers = FindHandControllers();

		if (_handControllers != null)
		{
			_handControllers.SetActive(false);
		}
		else
		{
			Debug.LogError("Hand controllers not found in the scene.");
		}
		foreach (var audio in FindObjectsOfType<AudioSource>())
			audio.Pause();
	}

	public void Init(string songName, string songSubName, string difficulty)
	{
		_levelNameText.text = string.Format("{0}\n<size=80%>{1}</size>", songName, songSubName);
		if (difficulty != null && difficulty != string.Empty)
		{
			_levelDifficultyText.text = "Difficulty - " + difficulty;
		}
		else
		{
			_levelDifficultyText.text = string.Empty;
		}
	}

	private void HandleResumeAnimationDidFinish()
	{
		_didFinishWithContinueButtonEvent.Raise();
		Time.timeScale = 1f;

		foreach (var audio in FindObjectsOfType<AudioSource>())
			audio.UnPause();
			
		if (_handControllers != null)
			_handControllers.SetActive(true);
		else if (_handControllers == null)
		{
			Debug.LogError("Hand controllers were not found when trying to re-enable them.");
		}
	}

	public void MenuButtonPressed()
	{
		_gameObjectsWrapper.SetActive(false);
		_didFinishWithMenuButtonEvent.Raise();
		Time.timeScale = 1f;
		VRControllersRecorder.Instance.StopAndSaveReplay("replay.bsor", 0, Time.time);
	}

	public void RestartButtonPressed()
	{
		_gameObjectsWrapper.SetActive(false);
		_didFinishWithRestartButtonEvent.Raise();
		VRControllersRecorder.Instance.StopAndSaveReplay("replay.bsor", 0, Time.time);
	}

	public void ContinueButtonPressed()
	{
		if (!_resumePauseAnimationController.resuming)
		{
			_gameObjectsWrapper.SetActive(false);
			_resumePauseAnimationController.StartAnimation();
			
		}
	}

	public static GameObject FindHandControllers()
	{
		for (int i = 0; i < SceneManager.sceneCount; i++)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			if (!scene.isLoaded) continue;

			foreach (var rootObj in scene.GetRootGameObjects())
			{
				if (rootObj.name == "GameCore")
				{
					foreach (var t in rootObj.GetComponentsInChildren<Transform>(true))
					{
						if (t.name == "HandControllers")
							return t.gameObject;
					}
				}
			}
		}
		Debug.LogWarning("No GameCore with HandControllers found in any loaded scene.");
		return null;
	}
}
