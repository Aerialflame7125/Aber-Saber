using UnityEngine;

public class MenuSceneSetupData : GameSceneSetupData
{
	[SerializeField]
	private InitSceneSetupData _initSceneSetupData;

	[SerializeField]
	private MainGameSceneSetupData _mainGameSceneSetupData;

	[SerializeField]
	private TutorialSceneSetupData _tutorialSceneSetupData;

	public LevelCompletionResults levelCompletionResults { get; private set; }

	public void StartLevel(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayOptions gameplayOptions, GameplayMode gameplayMode)
	{
		levelCompletionResults = null;
		_mainGameSceneSetupData.Init(difficultyLevel, gameplayOptions, gameplayMode, 0f);
		_mainGameSceneSetupData.didFinishEvent += HandleMainGameSceneDidFinish;
		_mainGameSceneSetupData.TransitionToScene(0.7f);
	}

	public void StartTutorial()
	{
		_tutorialSceneSetupData.didFinishEvent += HandleTutorialSceneDidFinish;
		_tutorialSceneSetupData.TransitionToScene(0.7f);
	}

	public void Restart()
	{
		_initSceneSetupData.TransitionToScene(0.35f);
	}

	private void HandleMainGameSceneDidFinish(MainGameSceneSetupData mainGameSceneSetupData, LevelCompletionResults levelCompletionResults)
	{
		this.levelCompletionResults = levelCompletionResults;
		mainGameSceneSetupData.didFinishEvent -= HandleMainGameSceneDidFinish;
		TransitionToScene((levelCompletionResults == null) ? 0.35f : 1.3f);
	}

	private void HandleTutorialSceneDidFinish(TutorialSceneSetupData tutorialSceneSetupData, bool completed)
	{
		tutorialSceneSetupData.didFinishEvent -= HandleTutorialSceneDidFinish;
		TransitionToScene((!completed) ? 0.35f : 1.3f);
	}
}
