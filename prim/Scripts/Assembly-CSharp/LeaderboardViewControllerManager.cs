using UnityEngine;
using VRUI;

public class LeaderboardViewControllerManager : MonoBehaviour
{
	[SerializeField]
	private PlatformLeaderboardViewController _platformLeaderboardViewController;

	[SerializeField]
	private LocalLeaderboardViewController _localLeaderboardViewController;

	private bool _usePlaformLeaderboard;

	private VRUIViewController _leaderboardViewController;

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
			_platformLeaderboardViewController.Init(difficultyLevel, gameplayMode);
			_leaderboardViewController = _platformLeaderboardViewController;
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
			_platformLeaderboardViewController.Refresh();
		}
		else
		{
			_localLeaderboardViewController.Refresh();
		}
	}
}
