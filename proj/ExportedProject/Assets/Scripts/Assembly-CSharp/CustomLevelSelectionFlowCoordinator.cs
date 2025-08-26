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
    private CustomLevelSelectionNavigationController _levelSelectionNavigationController;

    private StandardLevelSelectionNavigationController _slevelnav;

    [SerializeField]
    private CustomLevelListViewController _levelListViewController;

    [SerializeField]
    private StandardLevelDifficultyViewController _levelDifficultyViewController;

    [SerializeField]
    private StandardLevelDetailViewController _levelDetailViewController;

    [SerializeField]
    private SimpleDialogPromptViewController _simpleDialogPromptViewController;

    private bool _initialized;

    private GameplayMode _gameplayMode;

    public event Action didFinishEvent;

    public void Present(VRUIViewController parentViewController, IStandardLevel[] levels, GameplayMode gameplayMode)
    {
        _simpleDialogPromptViewController.didFinishEvent += HandleSimpleDialogPromptViewControllerDidFinish;
        _simpleDialogPromptViewController.Init("Content Warning", "The following songs you are going to encounter are seriously broken and literal cancer to play. Do you want to continue?", "MAYBE", "NO NO PLS SEND ME BACK");
        _levelSelectionNavigationController.PresentModalViewController(_simpleDialogPromptViewController, null);
        if (!_initialized)
        {
            didFinishEvent += Finish;
            _slevelnav.didFinishEvent += HandleLevelSelectionNavigationControllerDidFinish;
            _levelListViewController.didSelectLevelEvent += HandleLevelListViewControllerDidSelectLevel;
            StandardLevelDifficultyViewController levelDifficultyViewController = _levelDifficultyViewController;
            levelDifficultyViewController.didSelectDifficultyEvent = (Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap>)Delegate.Combine(levelDifficultyViewController.didSelectDifficultyEvent, new Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap>(HandleDifficultyViewControllerDidSelectDifficulty));
            _levelDetailViewController.didPressPlayButtonEvent += HandleLevelDetailViewControllerDidPressPlayButton;
            _initialized = true;
        }
        _gameplayMode = gameplayMode;
        _levelSelectionNavigationController.InitWithGameplayModeIndicator(gameplayMode, true);
        parentViewController.PresentModalViewController(_levelSelectionNavigationController, null, parentViewController.isRebuildingHierarchy);
        _levelListViewController.Init(levels, false);
        _levelSelectionNavigationController.PushViewController(_levelListViewController, parentViewController.isRebuildingHierarchy);
    }

    private void Finish()
    {
        if (_initialized)
        {
            didFinishEvent -= Finish;
            _slevelnav.didFinishEvent -= HandleLevelSelectionNavigationControllerDidFinish;
            _levelListViewController.didSelectLevelEvent -= HandleLevelListViewControllerDidSelectLevel;
            StandardLevelDifficultyViewController levelDifficultyViewController = _levelDifficultyViewController;
            levelDifficultyViewController.didSelectDifficultyEvent = (Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap>)Delegate.Remove(levelDifficultyViewController.didSelectDifficultyEvent, new Action<StandardLevelDifficultyViewController, IStandardLevelDifficultyBeatmap>(HandleDifficultyViewControllerDidSelectDifficulty));
            _levelDetailViewController.didPressPlayButtonEvent -= HandleLevelDetailViewControllerDidPressPlayButton;
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

    private void HandleLevelListViewControllerDidSelectLevel(StandardLevelListViewController viewController, IStandardLevel level)
    {
        if (!_levelDifficultyViewController.isInViewControllerHierarchy)
        {
            _levelDifficultyViewController.Init(level.difficultyBeatmaps, false);
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
            _levelDetailViewController.Init(_levelListViewController.selectedLevel.GetDifficultyLevel(_levelDifficultyViewController.selectedDifficultyLevel.difficulty), _gameplayMode, StandardLevelDetailViewController.LeftPanelViewControllerType.GameplayOptions);
            _levelSelectionNavigationController.PushViewController(_levelDetailViewController, viewController.isRebuildingHierarchy);
        }
        else
        {
            _levelDetailViewController.SetContent(_levelListViewController.selectedLevel.GetDifficultyLevel(_levelDifficultyViewController.selectedDifficultyLevel.difficulty), _gameplayMode);
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
        IStandardLevelDifficultyBeatmap difficultyLevel = _levelDetailViewController.difficultyLevel;
        GameplayOptions gameplayOptions = _levelDetailViewController.gameplayOptions;
        _menuSceneSetupData.StartLevel(difficultyLevel, gameplayOptions, _gameplayMode);
    }

    private void HandleLevelDetailViewControllerDidPressPlayButton(StandardLevelDetailViewController viewController)
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
            if (!gameplayOptions2.validForScoreUse && _gameplayMode.IsSolo())
            {
                _simpleDialogPromptViewController.didFinishEvent += HandleSimpleDialogPromptViewControllerDidFinish;
                _simpleDialogPromptViewController.Init("Gameplay options warning", GameOptionsTextsHelper.OptionsAreTurnOnText(gameplayOptions2.noEnergy, gameplayOptions2.noObstacles) + " Your score will not be submitted to the leaderboards. (Like it originally was.. smh) Do you want to continue?", "YES", "NO");
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
        _levelDetailViewController.RefreshContent();
    }
}
