using System;
using UnityEngine;
using VRUI;

public class ArcadeLevelSelectionFlowCoordinator : FlowCoordinator
{
	[SerializeField]
	private MenuSceneSetupData _menuSceneSetupData;

	[Space]
	[SerializeField]
	private ResultsFlowCoordinator _resultsFlowCoordinator;

	[Space]
	[SerializeField]
	private VRUIScreenSystem _screenSystem;

	[SerializeField]
	private StandardLevelSelectionNavigationController _levelSelectionNavigationController;

	[SerializeField]
	private StandardLevelListViewController _levelListViewController;

	[SerializeField]
	private StandardLevelDifficultyViewController _levelDifficultyViewController;

	[SerializeField]
	private StandardLevelDetailViewController _levelDetailViewController;

	[SerializeField]
	private HowToPlayViewController _howtoPlayViewController;

	[SerializeField]
	private GameplayOptionsViewController _gameplayOptionsViewController;

	[SerializeField]
	private ArcadeInitialViewController _arcadeInitialViewController;

	[SerializeField]
	private StandardLevelCollectionSO _levelCollection;

	private bool _initialized;

	private GameplayMode _gameplayMode = GameplayMode.PartyStandard;

	private GameplayOptions _gameplayOptions = GameplayOptions.defaultOptions;

	public void Present()
	{
		if (!_initialized)
		{
			_gameplayOptions.noEnergy = true;
			_levelListViewController.didSelectLevelEvent += HandleLevelListViewControllerDidSelectLevel;
			StandardLevelDifficultyViewController levelDifficultyViewController = _levelDifficultyViewController;
			levelDifficultyViewController.didSelectDifficultyEvent = (Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap>)Delegate.Combine(levelDifficultyViewController.didSelectDifficultyEvent, new Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap>(HandleDifficultyViewControllerDidSelectDifficulty));
			_levelDetailViewController.didPressPlayButtonEvent += HandleLevelDetailViewControllerDidPressPlayButton;
			_howtoPlayViewController.didPressTutorialButtonEvent += HandleHowToPlayViewControllerDidPressTutorialButton;
			_arcadeInitialViewController.didActivateEvent += HandleArcadeInitiaViewControllerDidActivate;
			_initialized = true;
		}
		_levelSelectionNavigationController.InitWithoutGameplayModeIndicator(showBackButton: false);
		_levelListViewController.Init(_levelCollection.levels, showHowToPlayViewController: true);
		_screenSystem.mainScreen.SetRootViewController(_arcadeInitialViewController);
	}

	private void HandleArcadeInitiaViewControllerDidActivate()
	{
		_arcadeInitialViewController.PresentModalViewController(_levelSelectionNavigationController, null, immediately: true);
		_levelSelectionNavigationController.PushViewController(_levelListViewController, immediately: true);
	}

	private void HandleLevelListViewControllerDidSelectLevel(StandardLevelListViewController viewController, IStandardLevel level)
	{
		if (!_levelDifficultyViewController.isInViewControllerHierarchy)
		{
			_levelDifficultyViewController.Init(level.difficultyBeatmaps, showHowToPlayViewController: true);
			_levelSelectionNavigationController.PushViewController(_levelDifficultyViewController, viewController.isRebuildingHierarchy);
		}
		else
		{
			_levelDifficultyViewController.SetDifficultyLevels(level.difficultyBeatmaps, _levelDifficultyViewController.selectedDifficultyLevel);
		}
	}

	private void HandleDifficultyViewControllerDidSelectDifficulty(StandardLevelDifficultyViewController viewController, IStandardLevelDifficultyBeatmap difficultyLevel)
	{
		if (!_levelDetailViewController.isInViewControllerHierarchy)
		{
			_levelDetailViewController.Init(_levelListViewController.selectedLevel.GetDifficultyLevel(_levelDifficultyViewController.selectedDifficultyLevel.difficulty), _gameplayMode, StandardLevelDetailViewController.LeftPanelViewControllerType.HowToPlay);
			_levelSelectionNavigationController.PushViewController(_levelDetailViewController, viewController.isRebuildingHierarchy);
		}
		else
		{
			_levelDetailViewController.SetContent(_levelListViewController.selectedLevel.GetDifficultyLevel(_levelDifficultyViewController.selectedDifficultyLevel.difficulty), _gameplayMode);
		}
	}

	private void StartLevel()
	{
		IStandardLevelDifficultyBeatmap difficultyLevel = _levelDetailViewController.difficultyLevel;
		_menuSceneSetupData.StartLevel(difficultyLevel, _gameplayOptions, _gameplayMode);
	}

	private void HandleLevelDetailViewControllerDidPressPlayButton(StandardLevelDetailViewController viewController)
	{
		if (viewController.isRebuildingHierarchy)
		{
			LevelCompletionResults levelCompletionResults = _menuSceneSetupData.levelCompletionResults;
			if (levelCompletionResults != null)
			{
				IStandardLevelDifficultyBeatmap difficultyLevel = viewController.difficultyLevel;
				_resultsFlowCoordinator.didFinishEvent += HandleResultsFlowCoordinatorDidFinish;
				_resultsFlowCoordinator.Present(_levelSelectionNavigationController, levelCompletionResults, difficultyLevel, _gameplayOptions, _gameplayMode);
			}
		}
		else
		{
			StartLevel();
		}
	}

	private void HandleResultsFlowCoordinatorDidFinish()
	{
		_levelDetailViewController.ClearHierarchyRebuildData();
		_resultsFlowCoordinator.didFinishEvent -= HandleResultsFlowCoordinatorDidFinish;
		_levelDetailViewController.RefreshContent();
	}

	private void HandleHowToPlayViewControllerDidPressTutorialButton()
	{
		_menuSceneSetupData.StartTutorial();
	}
}
