using System;
using TMPro;
using UnityEngine;
using VRUI;

public class StandardLevelDetailViewController : VRUIViewController
{
	public enum LeftPanelViewControllerType
	{
		GameplayOptions,
		HowToPlay
	}

	private class HierarchyRebuildData
	{
	}

	[SerializeField]
	private GameplayOptionsViewController _gameplayOptionsViewController;

	[SerializeField]
	private HowToPlayViewController _howToPlayViewController;

	[SerializeField]
	private LeaderboardViewControllerManager _leaderboardViewControllerManager;

	[Space]
	[SerializeField]
	private TextMeshProUGUI _songNameText;

	[SerializeField]
	private TextMeshProUGUI _durationText;

	[SerializeField]
	private TextMeshProUGUI _bpmText;

	[SerializeField]
	private TextMeshProUGUI _notesCountText;

	[SerializeField]
	private TextMeshProUGUI _obstaclesCountText;

	[SerializeField]
	private TextMeshProUGUI _highScoreText;

	[SerializeField]
	private TextMeshProUGUI _maxComboText;

	[SerializeField]
	private TextMeshProUGUI _maxRankText;

	[SerializeField]
	private GameObject _playerStatsContainer;

	private IStandardLevelDifficultyBeatmap _difficultyLevel;

	private GameplayMode _gameplayMode;

	private VRUIViewController _leaderboardViewController;

	private LeftPanelViewControllerType _leftPanelViewControllerType;

	private HierarchyRebuildData _hierarchyRebuildData;

	public IStandardLevelDifficultyBeatmap difficultyLevel => _difficultyLevel;

	public GameplayMode gameplayMode => _gameplayMode;

	public GameplayOptions gameplayOptions => _gameplayOptionsViewController.gameplayOptions;

	public event Action<StandardLevelDetailViewController> didPressPlayButtonEvent;

	public void Init(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayMode gameplayMode, LeftPanelViewControllerType leftPanelViewControllerType)
	{
		_difficultyLevel = difficultyLevel;
		_gameplayMode = gameplayMode;
		_leftPanelViewControllerType = leftPanelViewControllerType;
		if (_leftPanelViewControllerType == LeftPanelViewControllerType.GameplayOptions)
		{
			_gameplayOptionsViewController.Init(_gameplayMode);
		}
		else if (_leftPanelViewControllerType == LeftPanelViewControllerType.HowToPlay)
		{
			_howToPlayViewController.Init(showTutorialButton: true);
		}
		_leaderboardViewControllerManager.Init(_difficultyLevel, _gameplayMode, out _leaderboardViewController);
	}

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (activationType == ActivationType.AddedToHierarchy)
		{
			RefreshContent();
		}
		else
		{
			_leaderboardViewControllerManager.Refresh();
		}
	}

	protected override void DidDeactivate(DeactivationType deactivationType)
	{
		if (deactivationType == DeactivationType.RemovedFromHierarchy)
		{
			PersistentSingleton<LocalLeaderboardsModel>.instance.ClearLastScorePosition();
		}
	}

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		if (hierarchyRebuildData is HierarchyRebuildData)
		{
			PlayButtonPressed();
		}
	}

	protected override object GetHierarchyRebuildData()
	{
		return _hierarchyRebuildData;
	}

	protected override void LeftAndRightScreenViewControllers(out VRUIViewController leftScreenViewController, out VRUIViewController rightScreenViewController)
	{
		if (_leftPanelViewControllerType == LeftPanelViewControllerType.GameplayOptions)
		{
			leftScreenViewController = _gameplayOptionsViewController;
		}
		else if (_leftPanelViewControllerType == LeftPanelViewControllerType.HowToPlay)
		{
			leftScreenViewController = _howToPlayViewController;
		}
		else
		{
			leftScreenViewController = null;
		}
		rightScreenViewController = _leaderboardViewController;
	}

	private string GetSongDurationText(float songDuration)
	{
		int num = (int)(songDuration / 60f);
		int num2 = (int)(songDuration - (float)(num * 60));
		return num + ":" + $"{num2:00}";
	}

	public void RefreshContent()
	{
		IStandardLevel level = _difficultyLevel.level;
		_songNameText.text = $"{level.songName}\n<size=80%>{level.songSubName}</size>";
		_durationText.text = GetSongDurationText(level.audioClip.length);
		_bpmText.text = level.beatsPerMinute.ToString();
		_notesCountText.text = difficultyLevel.beatmapData.notesCount.ToString();
		_obstaclesCountText.text = difficultyLevel.beatmapData.obstaclesCount.ToString();
		bool flag = _gameplayMode.IsSolo();
		_playerStatsContainer.SetActive(flag);
		if (flag)
		{
			PlayerLevelStatsData playerLevelStatsData = PersistentSingleton<GameDataModel>.instance.gameDynamicData.GetCurrentPlayerDynamicData().GetPlayerLevelStatsData(level.levelID, _difficultyLevel.difficulty, _gameplayMode);
			_highScoreText.text = ((!playerLevelStatsData.validScore) ? "-" : playerLevelStatsData.highScore.ToString());
			_maxComboText.text = ((!playerLevelStatsData.validScore) ? "-" : playerLevelStatsData.maxCombo.ToString());
			_maxRankText.text = ((!playerLevelStatsData.validScore) ? "-" : LevelCompletionResults.GetRankName(playerLevelStatsData.maxRank));
		}
		_leaderboardViewControllerManager.Refresh();
	}

	public void SetContent(IStandardLevelDifficultyBeatmap difficultyLevel, GameplayMode gameplayMode)
	{
		_difficultyLevel = difficultyLevel;
		_gameplayMode = gameplayMode;
		_leaderboardViewControllerManager.Init(_difficultyLevel, _gameplayMode, out _leaderboardViewController);
		RefreshContent();
	}

	public void PlayButtonPressed()
	{
		_hierarchyRebuildData = new HierarchyRebuildData();
		if (this.didPressPlayButtonEvent != null)
		{
			this.didPressPlayButtonEvent(this);
		}
	}

	public void ClearHierarchyRebuildData()
	{
		_hierarchyRebuildData = null;
	}
}
