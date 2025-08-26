using System;

public class PlatformLeaderboardsModel : PersistentSingleton<PlatformLeaderboardsModel>
{
	public enum GetScoresResult
	{
		OK = 0,
		Failed = 1
	}

	public enum UploadScoreResult
	{
		OK = 0,
		Falied = 1
	}

	public enum ScoresScope
	{
		Global = 0,
		AroundPlayer = 1,
		Friends = 2
	}

	public enum GetPlayerIdResult
	{
		OK = 0,
		Failed = 1
	}

	public delegate void GetScoresCompletionHandler(GetScoresResult result, LeaderboardScore[] scores, int referencePlayerScoreIndex);

	public delegate void UploadScoreCompletionHandler(UploadScoreResult result);

	public delegate void GetPlayerIdCompletionHandler(GetPlayerIdResult result, string playerId);

	public struct LeaderboardScore
	{
		public int score { get; private set; }

		public int rank { get; private set; }

		public string playerName { get; private set; }

		public string playerId { get; private set; }

		public LeaderboardScore(int score, int rank, string playerName, string playerId)
		{
			this.score = score;
			this.rank = rank;
			this.playerName = playerName;
			this.playerId = playerId;
		}
	}

	private LeaderboardScoreUploader _scoreUploader;

	private PlatformLeaderboardsHandler _platformLeaderboardsHandler;

	private string _playerId;

	private HMAsyncRequest _getPlayerIdAsyncRequest;

	public bool valid
	{
		get
		{
			return _platformLeaderboardsHandler != null;
		}
	}

	public event Action allScoresDidUploadEvent;

	private void Awake()
	{
		_platformLeaderboardsHandler = new OculusPlatformLeaderboardsHandler();
		if (_platformLeaderboardsHandler == null)
		{
			return;
		}
		_getPlayerIdAsyncRequest = new HMAsyncRequest();
		_platformLeaderboardsHandler.GetPlayerId(_getPlayerIdAsyncRequest, delegate(GetPlayerIdResult result, string playerId)
		{
			_getPlayerIdAsyncRequest = null;
			if (result == GetPlayerIdResult.OK)
			{
				_playerId = playerId;
				_scoreUploader = base.gameObject.AddComponent<LeaderboardScoreUploader>();
				_scoreUploader.Init(UploadScore, _playerId);
				_scoreUploader.allScoresDidUploadEvent += HandleAllScoresDidUpload;
			}
		});
	}

	private void UploadScore(string leaderboadId, int score, HMAsyncRequest asyncRequest, UploadScoreCompletionHandler completionHandler)
	{
		if (_platformLeaderboardsHandler != null)
		{
			_platformLeaderboardsHandler.UploadScore(leaderboadId, score, asyncRequest, completionHandler);
		}
		else if (completionHandler == null)
		{
			completionHandler(UploadScoreResult.Falied);
		}
	}

	private void GetScores(string leaderboadId, int count, int fromRank, ScoresScope scope, HMAsyncRequest asyncRequest, GetScoresCompletionHandler completionHandler)
	{
		if (_platformLeaderboardsHandler != null)
		{
			_platformLeaderboardsHandler.GetScores(leaderboadId, count, fromRank, scope, _playerId, asyncRequest, completionHandler);
		}
		else if (completionHandler != null)
		{
			completionHandler(GetScoresResult.Failed, null, -1);
		}
	}

	private void HandleAllScoresDidUpload()
	{
		if (this.allScoresDidUploadEvent != null)
		{
			this.allScoresDidUploadEvent();
		}
	}

	public void GetScores(string leaderboadId, int count, int fromRank, HMAsyncRequest asyncRequest, GetScoresCompletionHandler completionHandler)
	{
		GetScores(leaderboadId, count, fromRank, ScoresScope.Global, asyncRequest, completionHandler);
	}

	public void GetScoresAroundPlayer(string leaderboadId, int count, HMAsyncRequest asyncRequest, GetScoresCompletionHandler completionHandler)
	{
		GetScores(leaderboadId, count, 0, ScoresScope.AroundPlayer, asyncRequest, completionHandler);
	}

	public void GetFriendsScores(string leaderboadId, int count, int fromRank, HMAsyncRequest asyncRequest, GetScoresCompletionHandler completionHandler)
	{
		GetScores(leaderboadId, count, fromRank, ScoresScope.Friends, asyncRequest, completionHandler);
	}

	public void AddScore(string leaderboadId, int score)
	{
		if (_scoreUploader != null)
		{
			_scoreUploader.AddScore(leaderboadId, score);
		}
	}

	public bool IsValidForGameplayMode(GameplayMode gameplayMode)
	{
		return PersistentSingleton<PlatformLeaderboardsModel>.instance.valid && gameplayMode.IsSolo();
	}
}
