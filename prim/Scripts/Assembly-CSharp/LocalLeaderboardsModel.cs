using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LocalLeaderboardsModel : PersistentSingleton<LocalLeaderboardsModel>
{
	public enum LeaderboardType
	{
		AllTime,
		Daily
	}

	[Serializable]
	public class ScoreData
	{
		public int _score;

		public string _playerName;

		public bool _fullCombo;

		public long _timestamp;
	}

	[Serializable]
	private class LeaderboardData
	{
		public string _leaderboardId;

		public List<ScoreData> _scores;
	}

	[Serializable]
	private class SavedLeaderboardsData
	{
		public List<LeaderboardData> _leaderboardsData;
	}

	[SerializeField]
	private int _maxNumberOfScoresInLeaderboard = 10;

	private const string kLocalLeaderboardsFileName = "LocalLeaderboards.dat";

	private const string kLocalDailyLeaderboardsFileName = "LocalDailyLeaderboards.dat";

	private Dictionary<LeaderboardType, int> _lastScorePositions;

	private string _lastScoreLeaderboardId;

	private List<LeaderboardData> _leaderboardsData;

	private List<LeaderboardData> _dailyLeaderboardsData;

	private void Awake()
	{
		_lastScorePositions = new Dictionary<LeaderboardType, int>();
		LoadData();
	}

	private void OnApplicationQuit()
	{
		SaveData();
	}

	private static void LoadLeaderboardsData(string filename, out List<LeaderboardData> leaderboardsData)
	{
		leaderboardsData = null;
		string path = Application.persistentDataPath + "/" + filename;
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SavedLeaderboardsData savedLeaderboardsData = JsonUtility.FromJson<SavedLeaderboardsData>(json);
			if (savedLeaderboardsData != null)
			{
				leaderboardsData = savedLeaderboardsData._leaderboardsData;
			}
		}
		if (leaderboardsData == null)
		{
			leaderboardsData = new List<LeaderboardData>();
		}
	}

	private static void SaveLeaderboardsData(string filename, List<LeaderboardData> leaderboardsData)
	{
		if (leaderboardsData != null)
		{
			SavedLeaderboardsData savedLeaderboardsData = new SavedLeaderboardsData();
			savedLeaderboardsData._leaderboardsData = leaderboardsData;
			string contents = JsonUtility.ToJson(savedLeaderboardsData);
			string path = Application.persistentDataPath + "/" + filename;
			File.WriteAllText(path, contents);
		}
	}

	private void LoadData()
	{
		LoadLeaderboardsData("LocalLeaderboards.dat", out _leaderboardsData);
		LoadLeaderboardsData("LocalDailyLeaderboards.dat", out _dailyLeaderboardsData);
		for (int i = 0; i < _dailyLeaderboardsData.Count; i++)
		{
			LeaderboardData leaderboardData = _dailyLeaderboardsData[i];
			UpdateDailyLeaderboard(leaderboardData._leaderboardId);
		}
	}

	private void SaveData()
	{
		SaveLeaderboardsData("LocalLeaderboards.dat", _leaderboardsData);
		SaveLeaderboardsData("LocalDailyLeaderboards.dat", _dailyLeaderboardsData);
	}

	private List<LeaderboardData> GetLeaderboardsData(LeaderboardType leaderboardType)
	{
		List<LeaderboardData> result = null;
		switch (leaderboardType)
		{
		case LeaderboardType.AllTime:
			result = _leaderboardsData;
			break;
		case LeaderboardType.Daily:
			result = _dailyLeaderboardsData;
			break;
		}
		return result;
	}

	private LeaderboardData GetLeaderboardData(string leaderboardId, LeaderboardType leaderboardType)
	{
		List<LeaderboardData> leaderboardsData = GetLeaderboardsData(leaderboardType);
		for (int i = 0; i < leaderboardsData.Count; i++)
		{
			LeaderboardData leaderboardData = leaderboardsData[i];
			if (leaderboardData._leaderboardId == leaderboardId)
			{
				return leaderboardData;
			}
		}
		return null;
	}

	private long GetCurrentTimestamp()
	{
		DateTime dateTime = DateTime.Now.ToUniversalTime();
		DateTime value = new DateTime(1970, 1, 1);
		return (long)dateTime.Subtract(value).TotalSeconds;
	}

	private void UpdateDailyLeaderboard(string leaderboardId)
	{
		LeaderboardData leaderboardData = GetLeaderboardData(leaderboardId, LeaderboardType.Daily);
		long num = GetCurrentTimestamp() - 86400;
		if (leaderboardData == null)
		{
			return;
		}
		for (int num2 = leaderboardData._scores.Count - 1; num2 >= 0; num2--)
		{
			if (leaderboardData._scores[num2]._timestamp < num)
			{
				leaderboardData._scores.RemoveAt(num2);
			}
		}
	}

	private void AddScore(string leaderboardId, LeaderboardType leaderboardType, string playerName, int score, bool fullCombo)
	{
		LeaderboardData leaderboardData = GetLeaderboardData(leaderboardId, leaderboardType);
		int i = 0;
		if (leaderboardData != null)
		{
			List<ScoreData> scores = leaderboardData._scores;
			for (i = 0; i < scores.Count && scores[i]._score >= score; i++)
			{
			}
		}
		else
		{
			leaderboardData = new LeaderboardData();
			leaderboardData._leaderboardId = leaderboardId;
			leaderboardData._scores = new List<ScoreData>(_maxNumberOfScoresInLeaderboard);
			List<LeaderboardData> leaderboardsData = GetLeaderboardsData(leaderboardType);
			leaderboardsData.Add(leaderboardData);
		}
		if (i < _maxNumberOfScoresInLeaderboard)
		{
			ScoreData scoreData = new ScoreData();
			scoreData._score = score;
			scoreData._playerName = playerName;
			scoreData._fullCombo = fullCombo;
			scoreData._timestamp = GetCurrentTimestamp();
			List<ScoreData> scores2 = leaderboardData._scores;
			scores2.Insert(i, scoreData);
			if (scores2.Count > _maxNumberOfScoresInLeaderboard)
			{
				scores2.RemoveAt(scores2.Count - 1);
			}
		}
		_lastScorePositions[leaderboardType] = i;
		_lastScoreLeaderboardId = leaderboardId;
	}

	private bool WillScoreGoIntoLeaderboard(string leaderboardId, LeaderboardType leaderboardType, int score)
	{
		if (leaderboardType == LeaderboardType.Daily)
		{
			UpdateDailyLeaderboard(leaderboardId);
		}
		LeaderboardData leaderboardData = GetLeaderboardData(leaderboardId, leaderboardType);
		if (leaderboardData != null)
		{
			List<ScoreData> scores = leaderboardData._scores;
			if (scores.Count < _maxNumberOfScoresInLeaderboard)
			{
				return true;
			}
			return scores[scores.Count - 1]._score < score;
		}
		return true;
	}

	public List<ScoreData> GetScores(string leaderboardId, LeaderboardType leaderboardType)
	{
		return GetLeaderboardData(leaderboardId, leaderboardType)?._scores;
	}

	public int GetHighScore(string leaderboardId, LeaderboardType leaderboardType)
	{
		LeaderboardData leaderboardData = GetLeaderboardData(leaderboardId, leaderboardType);
		if (leaderboardData != null && leaderboardData._scores.Count > 0)
		{
			return leaderboardData._scores[0]._score;
		}
		return 0;
	}

	public int GetPositionInLeaderboard(string leaderboardId, LeaderboardType leaderboardType, int score)
	{
		LeaderboardData leaderboardData = GetLeaderboardData(leaderboardId, leaderboardType);
		if (leaderboardData != null)
		{
			List<ScoreData> scores = leaderboardData._scores;
			int i;
			for (i = 0; i < scores.Count && scores[i]._score >= score; i++)
			{
			}
			if (i < _maxNumberOfScoresInLeaderboard)
			{
				return i;
			}
			return -1;
		}
		return 0;
	}

	public int GetLastScorePosition(string leaderboardId, LeaderboardType leaderboardType)
	{
		if (_lastScoreLeaderboardId == leaderboardId && _lastScorePositions.TryGetValue(leaderboardType, out var value))
		{
			return value;
		}
		return -1;
	}

	public void ClearLastScorePosition()
	{
		_lastScorePositions.Clear();
		_lastScoreLeaderboardId = null;
	}

	public void AddScore(string leaderboardId, string playerName, int score, bool fullCombo)
	{
		AddScore(leaderboardId, LeaderboardType.AllTime, playerName, score, fullCombo);
		AddScore(leaderboardId, LeaderboardType.Daily, playerName, score, fullCombo);
	}

	public bool WillScoreGoIntoLeaderboard(string leaderboardId, int score)
	{
		return WillScoreGoIntoLeaderboard(leaderboardId, LeaderboardType.AllTime, score) || WillScoreGoIntoLeaderboard(leaderboardId, LeaderboardType.Daily, score);
	}

	public void ClearLeaderboard(string leaderboardId)
	{
		for (int i = 0; i < _leaderboardsData.Count; i++)
		{
			LeaderboardData leaderboardData = _leaderboardsData[i];
			if (leaderboardData._leaderboardId == leaderboardId)
			{
				_leaderboardsData.RemoveAt(i);
				return;
			}
		}
		for (int j = 0; j < _dailyLeaderboardsData.Count; j++)
		{
			LeaderboardData leaderboardData2 = _dailyLeaderboardsData[j];
			if (leaderboardData2._leaderboardId == leaderboardId)
			{
				_dailyLeaderboardsData.RemoveAt(j);
				break;
			}
		}
	}

	public void ClearAllLeaderboards(bool deleteLeaderboardFile)
	{
		_leaderboardsData.Clear();
		_dailyLeaderboardsData.Clear();
		if (deleteLeaderboardFile)
		{
			string path = Application.persistentDataPath + "/LocalLeaderboards.dat";
			File.Delete(path);
			path = Application.persistentDataPath + "/LocalDailyLeaderboards.dat";
			File.Delete(path);
		}
	}
}
