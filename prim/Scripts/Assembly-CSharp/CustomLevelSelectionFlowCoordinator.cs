using System;
using UnityEngine;
using VRUI;

public class CustomLevelSelectionFlowCoordinator : FlowCoordinator
{
	[SerializeField]
	private MenuSceneSetupData _menuSceneSetupData;

	[Space]
	[SerializeField]
	private ResultsFlowCoordinator _resultsFlowCoordinator;

	[Space]
	[SerializeField]
	private StandardLevelSelectionNavigationController _levelSelectionNavigationController;

	[SerializeField]
	private CustomLevelListViewController _customLevelListViewController;

	[SerializeField]
	private StandardLevelDifficultyViewController _difficultyViewController;

	[SerializeField]
	private StandardLevelDetailViewController _songDetailViewController;

	[SerializeField]
	private SimpleDialogPromptViewController _simpleDialogPromptViewController;

	[Space]
	[SerializeField]
	private CustomLevelImportFlowCoordinator _customLevelImportFlowCoordinator;

	protected bool _initialized;

	private GameplayMode _gameplayMode;

	public event Action didFinishEvent;

	public void Present(VRUIViewController parentViewController)
	{
		if (!_initialized)
		{
			didFinishEvent += Finish;
			_levelSelectionNavigationController.didFinishEvent += HandleLevelSelectionNavigationControllerDidFinish;
			_customLevelListViewController.importButtonWasPressedEvent += HandleCustomLevelListViewControllerImportButtonWasPressed;
			_customLevelListViewController.didSelectSongEvent += HandleCustomLevelListViewControllerDidSelectSong;
			StandardLevelDifficultyViewController difficultyViewController = _difficultyViewController;
			difficultyViewController.didSelectDifficultyEvent = (Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap>)Delegate.Combine(difficultyViewController.didSelectDifficultyEvent, new Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap>(HandleDifficultyViewControllerDidSelectDifficulty));
			_songDetailViewController.didPressPlayButtonEvent += HandleSongDetailViewControllerDidPressPlayButton;
			_customLevelImportFlowCoordinator.didImportEvent += HandleCustomLevelImportFlowCoordinatorDidImport;
			_initialized = true;
		}
		_gameplayMode = GameplayMode.PartyStandard;
		_levelSelectionNavigationController.InitWithGameplayModeIndicator(_gameplayMode, showBackButton: true);
		parentViewController.PresentModalViewController(_levelSelectionNavigationController, null, parentViewController.isRebuildingHierarchy);
		_customLevelListViewController.Init();
		_levelSelectionNavigationController.PushViewController(_customLevelListViewController, parentViewController.isRebuildingHierarchy);
	}

	private void Finish()
	{
		if (_initialized)
		{
			didFinishEvent -= Finish;
			_levelSelectionNavigationController.didFinishEvent -= HandleLevelSelectionNavigationControllerDidFinish;
			_customLevelListViewController.importButtonWasPressedEvent -= HandleCustomLevelListViewControllerImportButtonWasPressed;
			_customLevelListViewController.didSelectSongEvent -= HandleCustomLevelListViewControllerDidSelectSong;
			StandardLevelDifficultyViewController difficultyViewController = _difficultyViewController;
			difficultyViewController.didSelectDifficultyEvent = (Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap>)Delegate.Remove(difficultyViewController.didSelectDifficultyEvent, new Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap>(HandleDifficultyViewControllerDidSelectDifficulty));
			_songDetailViewController.didPressPlayButtonEvent -= HandleSongDetailViewControllerDidPressPlayButton;
			_customLevelImportFlowCoordinator.didImportEvent -= HandleCustomLevelImportFlowCoordinatorDidImport;
			_initialized = false;
		}
	}

	private void HandleLevelSelectionNavigationControllerDidFinish(StandardLevelSelectionNavigationController viewController)
	{
		viewController.DismissModalViewController(null);
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent();
		}
	}

	private void HandleCustomLevelListViewControllerDidSelectSong(CustomLevelListViewController viewController, IStandardLevel level)
	{
		if (!_difficultyViewController.isInViewControllerHierarchy)
		{
			_difficultyViewController.Init(level.difficultyBeatmaps, showHowToPlayViewController: false);
			_levelSelectionNavigationController.PushViewController(_difficultyViewController, viewController.isRebuildingHierarchy);
		}
		else
		{
			_difficultyViewController.SetDifficultyLevels(level.difficultyBeatmaps, _difficultyViewController.selectedDifficultyLevel);
		}
	}

	private void HandleDifficultyViewControllerDidSelectDifficulty(StandardLevelDifficultyViewController viewController, IStandardLevelDifficultyBeatmap difficultyLevel)
	{
		if (!_songDetailViewController.isInViewControllerHierarchy)
		{
			_songDetailViewController.Init(difficultyLevel.level.GetDifficultyLevel(_difficultyViewController.selectedDifficultyLevel.difficulty), _gameplayMode, StandardLevelDetailViewController.LeftPanelViewControllerType.GameplayOptions);
			_levelSelectionNavigationController.PushViewController(_songDetailViewController, viewController.isRebuildingHierarchy);
		}
		else
		{
			_songDetailViewController.SetContent(difficultyLevel.level.GetDifficultyLevel(_difficultyViewController.selectedDifficultyLevel.difficulty), _gameplayMode);
		}
	}

	private void HandleSimpleDialogPromptViewControllerDidFinish(SimpleDialogPromptViewController viewController, bool ok)
	{
		viewController.didFinishEvent -= HandleSimpleDialogPromptViewControllerDidFinish;
		if (!viewController.isRebuildingHierarchy)
		{
			if (!ok)
			{
				viewController.DismissModalViewController(null);
			}
			else
			{
				StartLevel();
			}
		}
	}

	private void StartLevel()
	{
		IStandardLevelDifficultyBeatmap difficultyLevel = _songDetailViewController.difficultyLevel;
		GameplayOptions gameplayOptions = _songDetailViewController.gameplayOptions;
		_menuSceneSetupData.StartLevel(difficultyLevel, gameplayOptions, _gameplayMode);
	}

	private void HandleSongDetailViewControllerDidPressPlayButton(StandardLevelDetailViewController viewController)
	{
		if (viewController.isRebuildingHierarchy)
		{
			LevelCompletionResults levelCompletionResults = _menuSceneSetupData.levelCompletionResults;
			if (levelCompletionResults != null)
			{
				IStandardLevelDifficultyBeatmap difficultyLevel = viewController.difficultyLevel;
				GameplayOptions gameplayOptions = viewController.gameplayOptions;
				_resultsFlowCoordinator.didFinishEvent += HandleResultsFlowCoordinatorDidFinish;
				_resultsFlowCoordinator.Present(_levelSelectionNavigationController, levelCompletionResults, difficultyLevel, gameplayOptions, _gameplayMode);
			}
		}
		else
		{
			GameplayOptions gameplayOptions2 = viewController.gameplayOptions;
			if (gameplayOptions2.validForScoreUse && _gameplayMode.IsSolo())
			{
				_simpleDialogPromptViewController.didFinishEvent += HandleSimpleDialogPromptViewControllerDidFinish;
				_simpleDialogPromptViewController.Init("Gameplay options warning", GameOptionsTextsHelper.OptionsAreTurnOnText(gameplayOptions2.noEnergy, gameplayOptions2.noObstacles) + " Your score will not be submitted to the leaderboards. Do you want to continue?", "YES", "NO");
				_levelSelectionNavigationController.PresentModalViewController(_simpleDialogPromptViewController, null);
			}
			else
			{
				StartLevel();
			}
		}
	}

	private void HandleResultsFlowCoordinatorDidFinish()
	{
		_resultsFlowCoordinator.didFinishEvent -= HandleResultsFlowCoordinatorDidFinish;
		_songDetailViewController.RefreshContent();
	}

	private void HandleCustomLevelListViewControllerImportButtonWasPressed()
	{
		_customLevelImportFlowCoordinator.Present(_levelSelectionNavigationController);
	}

	private void HandleCustomLevelImportNAvigationControllerDidCancel(CustomLevelImportNavigationController controller)
	{
		controller.DismissModalViewController(null);
	}

	private void HandleCustomLevelImportFlowCoordinatorDidImport(CustomLevelImportFlowCoordinator flowCoordinator, string importedLevelID)
	{
		_customLevelListViewController.Reload(delegate
		{
			_customLevelListViewController.SelectSongWithLevelID(importedLevelID);
		});
	}
}
