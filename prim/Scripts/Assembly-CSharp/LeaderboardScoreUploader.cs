using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeaderboardScoreUploader : MonoBehaviour
{
	[Serializable]
	public class ScoreData
	{
		public string _playerId;

		public string _leaderboardId;

		public int _score;

		public int _uploadAttemptCount;

		[NonSerialized]
		public int _currentUploadAttemptCount;

		public ScoreData(string playerId, string leaderboardId, int score)
		{
			_playerId = playerId;
			_leaderboardId = leaderboardId;
			_score = score;
			_uploadAttemptCount = 0;
			_currentUploadAttemptCount = 0;
		}
	}

	[Serializable]
	private class ScoresToUploadData
	{
		public List<ScoreData> _scores;
	}

	public delegate void UploadScoreCallback(string leaderboadId, int score, HMAsyncRequest asyncRequest, PlatformLeaderboardsModel.UploadScoreCompletionHandler completionHandler);

	private const string kScoresToUploadFileName = "ScoresToUpload.dat";

	private List<ScoreData> _scoresToUpload;

	private List<ScoreData> _scoresToUploadForCurrentPlayer;

	private bool _uploading;

	private UploadScoreCallback _uploadScoreCallback;

	private string _playerId;

	public event Action allScoresDidUploadEvent;

	private void OnApplicationQuit()
	{
		SaveScores();
	}

	private IEnumerator UploadScoresCoroutine()
	{
		while (_scoresToUploadForCurrentPlayer.Count > 0)
		{
			_uploading = true;
			ScoreData scoreData = _scoresToUploadForCurrentPlayer[0];
			scoreData._uploadAttemptCount++;
			scoreData._currentUploadAttemptCount++;
			_uploadScoreCallback(scoreData._leaderboardId, scoreData._score, null, delegate(PlatformLeaderboardsModel.UploadScoreResult result)
			{
				_uploading = false;
				if (result == PlatformLeaderboardsModel.UploadScoreResult.OK)
				{
					_scoresToUploadForCurrentPlayer.RemoveAt(0);
				}
				else
				{
					scoreData = _scoresToUploadForCurrentPlayer[0];
					_scoresToUploadForCurrentPlayer.RemoveAt(0);
					if (scoreData._currentUploadAttemptCount < 3)
					{
						_scoresToUploadForCurrentPlayer.Add(scoreData);
					}
					else
					{
						_scoresToUpload.Add(scoreData);
					}
				}
				if (_scoresToUploadForCurrentPlayer.Count == 0 && this.allScoresDidUploadEvent != null)
				{
					this.allScoresDidUploadEvent();
				}
			});
			while (_uploading)
			{
				yield return new WaitForSeconds(3f);
			}
		}
	}

	private void LoadScores()
	{
		string path = Application.persistentDataPath + "/ScoresToUpload.dat";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			ScoresToUploadData scoresToUploadData = JsonUtility.FromJson<ScoresToUploadData>(json);
			if (scoresToUploadData != null)
			{
				_scoresToUpload = scoresToUploadData._scores;
			}
		}
		else
		{
			_scoresToUpload = null;
		}
		if (_scoresToUpload == null)
		{
			_scoresToUpload = new List<ScoreData>();
		}
		_scoresToUploadForCurrentPlayer = new List<ScoreData>(_scoresToUpload.Count);
		for (int num = _scoresToUpload.Count - 1; num >= 0; num--)
		{
			ScoreData scoreData = _scoresToUpload[num];
			if (scoreData._playerId == _playerId && _playerId != null)
			{
				_scoresToUploadForCurrentPlayer.Add(scoreData);
				_scoresToUpload.RemoveAt(num);
			}
		}
	}

	private void SaveScores()
	{
		if (_scoresToUpload != null)
		{
			_scoresToUpload.AddRange(_scoresToUploadForCurrentPlayer);
			string path = Application.persistentDataPath + "/ScoresToUpload.dat";
			if (_scoresToUpload.Count > 0)
			{
				ScoresToUploadData scoresToUploadData = new ScoresToUploadData();
				scoresToUploadData._scores = _scoresToUpload;
				string contents = JsonUtility.ToJson(scoresToUploadData);
				File.WriteAllText(path, contents);
			}
			else
			{
				File.Delete(path);
			}
		}
	}

	public void Init(UploadScoreCallback uploadScoreCallback, string playerId)
	{
		if (uploadScoreCallback != null && playerId != null)
		{
			_uploadScoreCallback = uploadScoreCallback;
			_playerId = playerId;
			LoadScores();
			StartCoroutine(UploadScoresCoroutine());
		}
	}

	public void AddScore(string leaderboadId, int score)
	{
		if (_uploadScoreCallback != null && _playerId != null)
		{
			ScoreData item = new ScoreData(_playerId, leaderboadId, score);
			_scoresToUploadForCurrentPlayer.Add(item);
			if (_scoresToUploadForCurrentPlayer.Count == 1)
			{
				StopAllCoroutines();
				StartCoroutine(UploadScoresCoroutine());
			}
		}
	}
}
