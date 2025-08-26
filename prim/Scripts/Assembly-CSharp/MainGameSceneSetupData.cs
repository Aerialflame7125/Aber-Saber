using System;

public class MainGameSceneSetupData : GameSceneSetupData
{
	private IStandardLevelDifficultyBeatmap _difficultyLevel;

	private GameplayOptions _gameplayOptions;

	private GameplayMode _gameplayMode;

	private float _forcedStartSongTime;

	public IStandardLevelDifficultyBeatmap difficultyLevel => _difficultyLevel;

	public GameplayOptions gameplayOptions => _gameplayOptions;

	public GameplayMode gameplayMode => _gameplayMode;

	public float forcedStartSongTime => _forcedStartSongTime;

	public event Action<MainGameSceneSetupData, LevelCompletionResults> didFinishEvent;

	public void Init(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayOptions gameplayOptions, GameplayMode gameplayMode, float forcedStartSongTime)
	{
		_difficultyLevel = difficultyLevel;
		_gameplayOptions = gameplayOptions;
		_gameplayMode = gameplayMode;
		_forcedStartSongTime = forcedStartSongTime;
	}

	public void Finish(LevelCompletionResults levelCompletionResults)
	{
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent(this, levelCompletionResults);
		}
	}
}
