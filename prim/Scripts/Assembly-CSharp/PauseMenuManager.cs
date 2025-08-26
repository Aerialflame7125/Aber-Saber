using TMPro;
using UnityEngine;

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

	private void Awake()
	{
		_resumePauseAnimationController.animationDidFinishEvent += HandleResumeAnimationDidFinish;
		base.enabled = false;
	}

	private void Update()
	{
		if (_ignoreFirstFrameVRControllerInteraction)
		{
			_ignoreFirstFrameVRControllerInteraction = false;
		}
		else if (VRControllersInputManager.MenuButtonDown())
		{
			ContinueButtonPressed();
		}
	}

	public void ShowMenu()
	{
		base.enabled = true;
		_ignoreFirstFrameVRControllerInteraction = true;
		_gameObjectsWrapper.SetActive(value: true);
		_resumePauseAnimationController.StopAnimation();
	}

	public void Init(string songName, string songSubName, string difficulty)
	{
		_levelNameText.text = $"{songName}\n<size=80%>{songSubName}</size>";
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
	}

	public void MenuButtonPressed()
	{
		_gameObjectsWrapper.SetActive(value: false);
		_didFinishWithMenuButtonEvent.Raise();
	}

	public void RestartButtonPressed()
	{
		_gameObjectsWrapper.SetActive(value: false);
		_didFinishWithRestartButtonEvent.Raise();
	}

	public void ContinueButtonPressed()
	{
		if (!_resumePauseAnimationController.resuming)
		{
			base.enabled = false;
			_gameObjectsWrapper.SetActive(value: false);
			_resumePauseAnimationController.StartAnimation();
		}
	}
}
