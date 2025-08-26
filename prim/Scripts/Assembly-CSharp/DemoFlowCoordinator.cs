using UnityEngine;
using VRUI;

public class DemoFlowCoordinator : FlowCoordinator
{
	[SerializeField]
	private MenuSceneSetupData _menuSceneSetupData;

	[SerializeField]
	private VRUIScreenSystem _screenSystem;

	[Space]
	[SerializeField]
	private DemoMenuViewController _demoMenuViewController;

	[SerializeField]
	private ResultsFlowCoordinator _resultsFlowCoordinator;

	private bool _initialized;

	public void Present()
	{
		if (!_initialized)
		{
			_demoMenuViewController.didSelectLevelEvent += HandleDemoMenuViewControllerDidSelectLevel;
			_demoMenuViewController.didSelectTutorialEvent += HandleDemoMenuViewControllerDidSelectTutorial;
			_initialized = true;
		}
		_screenSystem.mainScreen.SetRootViewController(_demoMenuViewController);
	}

	private void HandleDemoMenuViewControllerDidSelectLevel(DemoMenuViewController viewController, IStandardLevelDifficultyBeatmap difficultyLevel)
	{
		if (viewController.isRebuildingHierarchy)
		{
			LevelCompletionResults levelCompletionResults = _menuSceneSetupData.levelCompletionResults;
			if (levelCompletionResults != null)
			{
				_resultsFlowCoordinator.didFinishEvent += HandleResultsFlowCoordinatorDidFinish;
				_resultsFlowCoordinator.Present(_demoMenuViewController, levelCompletionResults, difficultyLevel, _demoMenuViewController.gameplayOptions, _demoMenuViewController.gameplayMode);
			}
		}
		else
		{
			_menuSceneSetupData.StartLevel(difficultyLevel, _demoMenuViewController.gameplayOptions, _demoMenuViewController.gameplayMode);
		}
	}

	private void HandleDemoMenuViewControllerDidSelectTutorial(DemoMenuViewController viewController)
	{
		if (!viewController.isRebuildingHierarchy)
		{
			_menuSceneSetupData.StartTutorial();
		}
	}

	private void HandleResultsFlowCoordinatorDidFinish()
	{
		_resultsFlowCoordinator.didFinishEvent -= HandleResultsFlowCoordinatorDidFinish;
	}
}
