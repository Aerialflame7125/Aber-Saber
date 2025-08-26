using UnityEngine;
using VRUI;
using System.Collections.Generic;
using System.Linq;

public class LeaderboardViewControllerManager : MonoBehaviour
{
	[SerializeField]
	private PlatformLeaderboardViewController _platformLeaderboardViewController;

	[SerializeField]
	private LocalLeaderboardViewController _localLeaderboardViewController;

	[SerializeField]
	private BlLeaderboardViewController _BlLeaderboardViewController;

	private bool _usePlaformLeaderboard;

	private VRUIViewController _leaderboardViewController;

	public bool useBL;

	public bool forcedLoadingIndicator
	{
		get
		{
			if (_usePlaformLeaderboard)
			{
				return _platformLeaderboardViewController.forcedLoadingIndicator;
			}
			return false;
		}
		set
		{
			if (_usePlaformLeaderboard)
			{
				_platformLeaderboardViewController.forcedLoadingIndicator = value;
			}
		}
	}

	public void Init(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayMode gameplayMode, out VRUIViewController leaderboardViewController)
	{
		_usePlaformLeaderboard = PersistentSingleton<PlatformLeaderboardsModel>.instance.IsValidForGameplayMode(gameplayMode);
		if (_usePlaformLeaderboard)
		{
			Debug.Log("Using BeatLeader");
			useBL = true;
			_BlLeaderboardViewController.Init(difficultyLevel, gameplayMode, 1, 1, 1);
			_leaderboardViewController = _BlLeaderboardViewController;
		}
		else
		{
			_localLeaderboardViewController.Init(difficultyLevel, gameplayMode);
			_leaderboardViewController = _localLeaderboardViewController;
		}
		leaderboardViewController = _leaderboardViewController;
	}

	public void Refresh()
	{
		if (_usePlaformLeaderboard)
		{
			_BlLeaderboardViewController.Refresh();
		}
		else
		{
			_localLeaderboardViewController.Refresh();
		}
	}
}
