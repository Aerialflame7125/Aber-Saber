using System;

public class MainGameSceneSetupData : GameSceneSetupData
{
	private IStandardLevelDifficultyBeatmap _difficultyLevel;

	private GameplayOptions _gameplayOptions;

	private GameplayMode _gameplayMode;

	private float _forcedStartSongTime;

	public IStandardLevelDifficultyBeatmap difficultyLevel
	{
		get
		{
			return _difficultyLevel;
		}
	}

	public GameplayOptions gameplayOptions
	{
		get
		{
			return _gameplayOptions;
		}
	}

	public GameplayMode gameplayMode
	{
		get
		{
			return _gameplayMode;
		}
	}

	public float forcedStartSongTime
	{
		get
		{
			return _forcedStartSongTime;
		}
	}

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
