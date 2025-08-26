using System;
using UnityEngine;
using VRUI;

public class ResultsFlowCoordinator : FlowCoordinator
{
	[SerializeField]
	private MenuSceneSetupData _menuSceneSetupData;

	[Space]
	[SerializeField]
	private ResultsViewController _resultsViewController;

	[SerializeField]
	private NameEntryViewController _nameEntryViewController;

	private bool _initialized;

	public event Action didFinishEvent;

	public event Action<string, LevelDifficulty, string> didEnterPlayerNameEvent;

	public void Present(VRUIViewController parentViewController, LevelCompletionResults levelCompletionResults, IStandardLevelDifficultyBeatmap difficultyLevel, GameplayOptions gameplayOptions, GameplayMode gameplayMode)
	{
		if (!_initialized)
		{
			_resultsViewController.continueButtonPressedEvent += HandleResultsViewControllerContinueButtonPressed;
			_resultsViewController.restartButtonPressedEvent += HandleResultsViewControllerRestartButtonPressed;
			_nameEntryViewController.didFinishEvent += HandleNameEntryViewControllerDidFinish;
			_initialized = true;
		}
		_resultsViewController.Init(levelCompletionResults, difficultyLevel, gameplayOptions, gameplayMode, null);
		if (PersistentSingleton<PlatformLeaderboardsModel>.instance.IsValidForGameplayMode(gameplayMode))
		{
			parentViewController.PresentModalViewController(_resultsViewController, null, parentViewController.isRebuildingHierarchy);
		}
		else if (!PersistentSingleton<LocalLeaderboardsModel>.instance.WillScoreGoIntoLeaderboard(difficultyLevel.level.levelID, levelCompletionResults.score) || levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed)
		{
			parentViewController.PresentModalViewController(_resultsViewController, null, parentViewController.isRebuildingHierarchy);
		}
		else
		{
			parentViewController.PresentModalViewController(_nameEntryViewController, null, parentViewController.isRebuildingHierarchy);
		}
	}

	private void HandleResultsViewControllerContinueButtonPressed(ResultsViewController viewController)
	{
		if (!viewController.isRebuildingHierarchy)
		{
			_resultsViewController.DismissModalViewController(null);
			if (this.didFinishEvent != null)
			{
				this.didFinishEvent();
			}
		}
	}

	private void HandleResultsViewControllerRestartButtonPressed(ResultsViewController viewController)
	{
		if (!viewController.isRebuildingHierarchy)
		{
			IStandardLevelDifficultyBeatmap difficultyLevel = viewController.difficultyLevel;
			GameplayMode gameplayMode = viewController.gameplayMode;
			GameplayOptions gameplayOptions = viewController.gameplayOptions;
			_menuSceneSetupData.StartLevel(difficultyLevel, gameplayOptions, gameplayMode);
		}
	}

	private void HandleNameEntryViewControllerDidFinish(NameEntryViewController viewController, string playerName)
	{
		if (this.didEnterPlayerNameEvent != null)
		{
			this.didEnterPlayerNameEvent(playerName, _resultsViewController.difficultyLevel.difficulty, _resultsViewController.difficultyLevel.level.levelID);
		}
		_resultsViewController.Init(_resultsViewController.levelCompletionResults, _resultsViewController.difficultyLevel, _resultsViewController.gameplayOptions, _resultsViewController.gameplayMode, playerName);
		viewController.ReplaceViewController(_resultsViewController, null, viewController.isRebuildingHierarchy);
	}
}
