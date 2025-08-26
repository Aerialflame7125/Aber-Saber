using System.Collections.Generic;
using HMUI;
using UnityEngine;
using VRUI;

public class PlatformLeaderboardViewController : VRUIViewController
{
	[SerializeField]
	private LeaderboardTableView _leaderboardTableView;

	[SerializeField]
	private SimpleSegmentedControl _scopeSegmentedControl;

	[SerializeField]
	private GameObject _loadingIndicator;

	private static PlatformLeaderboardsModel.ScoresScope _scoresScope;

	private IStandardLevelDifficultyBeatmap _difficultyLevel;

	private GameplayMode _gameplayMode;

	private HMAsyncRequest _asyncRequest;

	private PlatformLeaderboardsModel _leaderboardsModel;

	private int[] _playerScorePos;

	private List<LeaderboardTableView.ScoreData> _scores = new List<LeaderboardTableView.ScoreData>(10);

	private bool _forcedLoadingIndicator;

	public bool forcedLoadingIndicator
	{
		get
		{
			return _forcedLoadingIndicator;
		}
		set
		{
			_forcedLoadingIndicator = value;
			_loadingIndicator.SetActive(_forcedLoadingIndicator);
			if (_forcedLoadingIndicator)
			{
				if (_asyncRequest != null)
				{
					_asyncRequest.Cancel();
					_asyncRequest = null;
				}
				Clear();
			}
		}
	}

	private void OnDestroy()
	{
		if (_scopeSegmentedControl != null)
		{
			_scopeSegmentedControl.didSelectCellEvent -= HandleScopeSegmentedControlDidSelectCell;
		}
		if (_asyncRequest != null)
		{
			_asyncRequest.Cancel();
			_asyncRequest = null;
		}
	}

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation)
		{
			_playerScorePos = new int[3] { -1, -1, -1 };
			_leaderboardsModel = PersistentSingleton<PlatformLeaderboardsModel>.instance;
			_scopeSegmentedControl.didSelectCellEvent += HandleScopeSegmentedControlDidSelectCell;
			base.rectTransform.anchorMin = Vector2.zero;
			base.rectTransform.anchorMax = Vector2.one;
			base.rectTransform.offsetMin = Vector2.zero;
			base.rectTransform.offsetMax = Vector2.zero;
			_scopeSegmentedControl.SetTexts(new string[3] { "Global", "Player", "Friends" });
		}
		if (activationType == ActivationType.AddedToHierarchy)
		{
			_scopeSegmentedControl.SelectColumn((int)_scoresScope);
		}
	}

	protected override void DidDeactivate(DeactivationType deactivationType)
	{
		if (_asyncRequest != null)
		{
			_asyncRequest.Cancel();
			_asyncRequest = null;
		}
	}

	private void LeaderboardsResultsReturned(PlatformLeaderboardsModel.GetScoresResult result, PlatformLeaderboardsModel.LeaderboardScore[] scores, int playerScoreIndex)
	{
		_loadingIndicator.SetActive(false);
		_asyncRequest = null;
		if (result != 0)
		{
			return;
		}
		_scores.Clear();
		for (int i = 0; i < 10; i++)
		{
			if (i < scores.Length)
			{
				PlatformLeaderboardsModel.LeaderboardScore leaderboardScore = scores[i];
				_scores.Add(new LeaderboardTableView.ScoreData(leaderboardScore.score, leaderboardScore.playerName, leaderboardScore.rank, false));
			}
		}
		_playerScorePos[(int)_scoresScope] = playerScoreIndex;
		_leaderboardTableView.SetScores(_scores, _playerScorePos[(int)_scoresScope]);
	}

	private void HandleScopeSegmentedControlDidSelectCell(SegmentedControl segmentedControl, int column)
	{
		switch (column)
		{
		case 0:
			_scoresScope = PlatformLeaderboardsModel.ScoresScope.Global;
			break;
		case 1:
			_scoresScope = PlatformLeaderboardsModel.ScoresScope.AroundPlayer;
			break;
		case 2:
			_scoresScope = PlatformLeaderboardsModel.ScoresScope.Friends;
			break;
		}
		Refresh();
	}

	public void Init(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayMode gameplayMode)
	{
		_difficultyLevel = difficultyLevel;
		_gameplayMode = gameplayMode;
	}

	public void Refresh()
	{
		if (!_forcedLoadingIndicator)
		{
			_loadingIndicator.SetActive(true);
			Clear();
			string leaderboardID = LeaderboardsModel.GetLeaderboardID(_difficultyLevel, _gameplayMode);
			if (_asyncRequest != null)
			{
				_asyncRequest.Cancel();
			}
			_asyncRequest = new HMAsyncRequest();
			if (_scoresScope == PlatformLeaderboardsModel.ScoresScope.Global)
			{
				_leaderboardsModel.GetScores(leaderboardID, 10, 1, _asyncRequest, LeaderboardsResultsReturned);
			}
			else if (_scoresScope == PlatformLeaderboardsModel.ScoresScope.Friends)
			{
				_leaderboardsModel.GetFriendsScores(leaderboardID, 10, 1, _asyncRequest, LeaderboardsResultsReturned);
			}
			else
			{
				_leaderboardsModel.GetScoresAroundPlayer(leaderboardID, 10, _asyncRequest, LeaderboardsResultsReturned);
			}
		}
	}

	public void Clear()
	{
		_scores.Clear();
		_leaderboardTableView.SetScores(_scores, _playerScorePos[(int)_scoresScope]);
	}
}
