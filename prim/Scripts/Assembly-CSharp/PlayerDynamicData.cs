using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerDynamicData
{
	[SerializeField]
	private List<PlayerLevelStatsData> _playerLevelStatsData = new List<PlayerLevelStatsData>();

	[SerializeField]
	private List<PlayerMissionStatsData> _playerMissionStatsData = new List<PlayerMissionStatsData>();

	[SerializeField]
	private GameplayOptions _gameplayOptions = GameplayOptions.defaultOptions;

	public GameplayOptions gameplayOptions
	{
		get
		{
			return _gameplayOptions;
		}
		set
		{
			_gameplayOptions = value;
		}
	}

	public PlayerLevelStatsData GetPlayerLevelStatsData(string levelID, LevelDifficulty difficulty, GameplayMode gameplayMode)
	{
		foreach (PlayerLevelStatsData playerLevelStatsDatum in _playerLevelStatsData)
		{
			if (playerLevelStatsDatum.levelID == levelID && playerLevelStatsDatum.difficulty == difficulty && playerLevelStatsDatum.gameplayMode == gameplayMode)
			{
				return playerLevelStatsDatum;
			}
		}
		PlayerLevelStatsData playerLevelStatsData = new PlayerLevelStatsData(levelID, difficulty, gameplayMode);
		_playerLevelStatsData.Add(playerLevelStatsData);
		return playerLevelStatsData;
	}

	public PlayerMissionStatsData GetPlayerMissionStatsData(string missionId)
	{
		foreach (PlayerMissionStatsData playerMissionStatsDatum in _playerMissionStatsData)
		{
			if (playerMissionStatsDatum.missionId == missionId)
			{
				return playerMissionStatsDatum;
			}
		}
		PlayerMissionStatsData playerMissionStatsData = new PlayerMissionStatsData(missionId, cleared: false);
		_playerMissionStatsData.Add(playerMissionStatsData);
		return playerMissionStatsData;
	}
}
