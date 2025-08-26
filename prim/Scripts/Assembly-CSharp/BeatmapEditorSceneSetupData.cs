using UnityEngine;

public class BeatmapEditorSceneSetupData : GameSceneSetupData
{
	[SerializeField]
	private MainGameSceneSetupData _mainGameSceneSetupData;

	private void HandleMainGameSceneDidFinish(MainGameSceneSetupData mainGameSceneSetupData, LevelCompletionResults levelCompletionResults)
	{
		mainGameSceneSetupData.didFinishEvent -= HandleMainGameSceneDidFinish;
		TransitionToScene(0.35f);
	}

	public void StartLevel(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayOptions gameplayOptions, GameplayMode gameplayMode, float startSongTime)
	{
		_mainGameSceneSetupData.Init(difficultyLevel, gameplayOptions, gameplayMode, startSongTime);
		_mainGameSceneSetupData.didFinishEvent += HandleMainGameSceneDidFinish;
		_mainGameSceneSetupData.TransitionToScene(0f);
	}
}
