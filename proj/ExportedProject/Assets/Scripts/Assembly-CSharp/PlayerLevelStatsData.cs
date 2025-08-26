using System;
using UnityEngine;

[Serializable]
public class PlayerLevelStatsData
{
	[SerializeField]
	private int _highScore;

	[SerializeField]
	private int _maxCombo;

	[SerializeField]
	private LevelCompletionResults.Rank _maxRank;

	[SerializeField]
	private bool _validScore;

	[SerializeField]
	private int _playCount;

	[SerializeField]
	private string _levelID;

	[SerializeField]
	private LevelDifficulty _difficulty;

	[SerializeField]
	private GameplayMode _gameplayMode;

	public string levelID
	{
		get
		{
			return _levelID;
		}
	}

	public LevelDifficulty difficulty
	{
		get
		{
			return _difficulty;
		}
	}

	public GameplayMode gameplayMode
	{
		get
		{
			return _gameplayMode;
		}
	}

	public int highScore
	{
		get
		{
			return _highScore;
		}
	}

	public int maxCombo
	{
		get
		{
			return _maxCombo;
		}
	}

	public LevelCompletionResults.Rank maxRank
	{
		get
		{
			return _maxRank;
		}
	}

	public bool validScore
	{
		get
		{
			return _validScore;
		}
	}

	public int playCount
	{
		get
		{
			return _playCount;
		}
	}

	public PlayerLevelStatsData(string levelID, LevelDifficulty difficulty, GameplayMode gameplayMode)
	{
		_levelID = levelID;
		_difficulty = difficulty;
		_gameplayMode = gameplayMode;
	}

	public void UpdateScoreData(int score, int maxCombo, LevelCompletionResults.Rank rank)
	{
		_highScore = Mathf.Max(_highScore, score);
		_maxCombo = Mathf.Max(_maxCombo, maxCombo);
		_maxRank = (LevelCompletionResults.Rank)Mathf.Max((int)_maxRank, (int)rank);
		_validScore = true;
	}

	public void IncreaseNumberOfGameplays()
	{
		_playCount++;
	}
}
