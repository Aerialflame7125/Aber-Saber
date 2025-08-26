using System;
using UnityEngine;
using VRUI;

public class StandardLevelSelectionNavigationController : VRUINavigationController
{
	[SerializeField]
	private GameplayModeIndicator _gameplayModeIndicator;

	[SerializeField]
	private GameObject _backButtonObject;

	private GameplayMode _gameplayMode;

	public event Action<StandardLevelSelectionNavigationController> didFinishEvent;

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (activationType == ActivationType.AddedToHierarchy)
		{
			_gameplayModeIndicator.SetupForGameplayMode(_gameplayMode);
		}
	}

	public void InitWithGameplayModeIndicator(GameplayMode gameplayMode, bool showBackButton)
	{
		_gameplayModeIndicator.gameObject.SetActive(value: true);
		_gameplayMode = gameplayMode;
		_backButtonObject.gameObject.SetActive(showBackButton);
	}

	public void InitWithoutGameplayModeIndicator(bool showBackButton)
	{
		_gameplayModeIndicator.gameObject.SetActive(value: false);
		_backButtonObject.gameObject.SetActive(showBackButton);
	}

	public void DismissButtonWasPressed()
	{
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent(this);
		}
	}
}
