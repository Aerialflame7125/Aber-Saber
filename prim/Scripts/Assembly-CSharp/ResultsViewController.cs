using System;
using TMPro;
using UnityEngine;
using VRUI;

public class ResultsViewController : VRUIViewController
{
	[SerializeField]
	private GameBuildMode _gameBuildMode;

	[SerializeField]
	private LeaderboardViewControllerManager _leaderboardViewControllerManager;

	[SerializeField]
	private GameObject _failedPanel;

	[SerializeField]
	private GameObject _clearedPanel;

	[SerializeField]
	private TextMeshProUGUI _scoreText;

	[SerializeField]
	private TextMeshProUGUI _scoreInfoText;

	[SerializeField]
	private TextMeshProUGUI _rankText;

	[SerializeField]
	private TextMeshProUGUI _goodCutsPercentageText;

	[SerializeField]
	private TextMeshProUGUI _fullComboText;

	[Space]
	[SerializeField]
	private TextMeshProUGUI _clearedSongNameText;

	[SerializeField]
	private TextMeshProUGUI _clearedSongAuthorText;

	[SerializeField]
	private TextMeshProUGUI _clearedDifficultyText;

	[SerializeField]
	private TextMeshProUGUI _failedSongNameText;

	[SerializeField]
	private TextMeshProUGUI _failedSongAuthorText;

	[SerializeField]
	private TextMeshProUGUI _failedDifficultyText;

	private LevelCompletionResults _levelCompletionResults;

	private IStandardLevelDifficultyBeatmap _difficultyLevel;

	private GameplayOptions _gameplayOptions;

	private GameplayMode _gameplayMode;

	private string _playerName;

	private VRUIViewController _leaderboardViewController;

	public LevelCompletionResults levelCompletionResults => _levelCompletionResults;

	public IStandardLevelDifficultyBeatmap difficultyLevel => _difficultyLevel;

	public GameplayOptions gameplayOptions => _gameplayOptions;

	public GameplayMode gameplayMode => _gameplayMode;

	public event Action<ResultsViewController> continueButtonPressedEvent;

	public event Action<ResultsViewController> restartButtonPressedEvent;

	public void Init(LevelCompletionResults levelCompletionResults, IStandardLevelDifficultyBeatmap difficultyLevel, GameplayOptions gameplayOptions, GameplayMode gameplayMode, string playerName)
	{
		_levelCompletionResults = levelCompletionResults;
		_difficultyLevel = difficultyLevel;
		_gameplayOptions = gameplayOptions;
		_gameplayMode = gameplayMode;
		_playerName = playerName;
		_leaderboardViewControllerManager.Init(_difficultyLevel, _gameplayMode, out _leaderboardViewController);
	}

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (activationType != 0)
		{
			return;
		}
		bool flag = (gameplayOptions.validForScoreUse || !_gameplayMode.IsSolo()) && _levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared;
		string levelID = _difficultyLevel.level.levelID;
		LevelDifficulty difficulty = _difficultyLevel.difficulty;
		PlayerLevelStatsData playerLevelStatsData = PersistentSingleton<GameDataModel>.instance.gameDynamicData.GetCurrentPlayerDynamicData().GetPlayerLevelStatsData(levelID, difficulty, _gameplayMode);
		playerLevelStatsData.IncreaseNumberOfGameplays();
		if (flag)
		{
			if (_gameplayMode == GameplayMode.PartyStandard && !gameplayOptions.validForScoreUse && _gameBuildMode.mode == GameBuildMode.Mode.Full)
			{
				_levelCompletionResults.ModifyScore((int)((float)_levelCompletionResults.score * LevelCompletionResults.kScoreMulForNoEnergyOrNoObstaclesGameplayOption));
			}
			if (_gameplayMode.IsSolo())
			{
				playerLevelStatsData.UpdateScoreData(_levelCompletionResults.score, _levelCompletionResults.maxCombo, _levelCompletionResults.rank);
			}
		}
		PlatformLeaderboardsModel instance = PersistentSingleton<PlatformLeaderboardsModel>.instance;
		LocalLeaderboardsModel instance2 = PersistentSingleton<LocalLeaderboardsModel>.instance;
		bool flag2 = instance.IsValidForGameplayMode(_gameplayMode);
		if (flag)
		{
			string leaderboardID = LeaderboardsModel.GetLeaderboardID(_difficultyLevel, _gameplayMode);
			if (flag2)
			{
				_leaderboardViewControllerManager.forcedLoadingIndicator = true;
				instance.allScoresDidUploadEvent += HandleAllScoresDidUpload;
				instance.AddScore(leaderboardID, _levelCompletionResults.score);
			}
			else
			{
				instance2.AddScore(leaderboardID, _playerName, _levelCompletionResults.score, _levelCompletionResults.fullCombo);
				_leaderboardViewControllerManager.Refresh();
			}
		}
		else
		{
			if (!flag2)
			{
				instance2.ClearLastScorePosition();
			}
			_leaderboardViewControllerManager.Refresh();
		}
		SetDataToUI();
	}

	protected override void DidDeactivate(DeactivationType deactivationType)
	{
		PersistentSingleton<PlatformLeaderboardsModel>.instance.allScoresDidUploadEvent -= HandleAllScoresDidUpload;
		_leaderboardViewControllerManager.forcedLoadingIndicator = false;
	}

	private void OnDestroy()
	{
		if (PersistentSingleton<PlatformLeaderboardsModel>.IsSingletonAvailable)
		{
			PersistentSingleton<PlatformLeaderboardsModel>.instance.allScoresDidUploadEvent -= HandleAllScoresDidUpload;
		}
	}

	protected override void LeftAndRightScreenViewControllers(out VRUIViewController leftScreenViewController, out VRUIViewController rightScreenViewController)
	{
		leftScreenViewController = null;
		rightScreenViewController = _leaderboardViewController;
	}

	private void SetDataToUI()
	{
		_failedPanel.SetActive(_levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed);
		_clearedPanel.SetActive(_levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared);
		IStandardLevel level = _difficultyLevel.level;
		LevelDifficulty difficulty = _difficultyLevel.difficulty;
		int notesCount = level.GetDifficultyLevel(difficulty).beatmapData.notesCount;
		if (_levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Failed)
		{
			_failedSongNameText.text = level.songName + " <size=80%>" + level.songSubName;
			_failedSongAuthorText.text = level.songAuthorName;
			_failedDifficultyText.text = "Difficulty - " + difficulty.Name();
		}
		else if (_levelCompletionResults.levelEndStateType == LevelCompletionResults.LevelEndStateType.Cleared)
		{
			_clearedSongNameText.text = level.songName + " <size=80%>" + level.songSubName;
			_clearedSongAuthorText.text = level.songAuthorName;
			_clearedDifficultyText.text = "Difficulty - " + difficulty.Name();
		}
		_scoreText.text = ScoreFormatter.Format(_levelCompletionResults.score);
		_scoreInfoText.gameObject.SetActive(!_gameplayOptions.validForScoreUse && _gameBuildMode.mode == GameBuildMode.Mode.Full);
		if (!_gameplayOptions.validForScoreUse && _gameBuildMode.mode == GameBuildMode.Mode.Full)
		{
			if (_gameplayMode.IsSolo())
			{
				_scoreInfoText.text = GameOptionsTextsHelper.PlayedWithOptionsText(_gameplayOptions.noEnergy, _gameplayOptions.noObstacles);
			}
			else
			{
				_scoreInfoText.text = ScoreFormatter.Format(_levelCompletionResults.unmodifiedScore) + " - <color=#FF175A>" + Mathf.FloorToInt((1f - LevelCompletionResults.kScoreMulForNoEnergyOrNoObstaclesGameplayOption) * 100f) + "% (" + GameOptionsTextsHelper.OptionsToText(_gameplayOptions.noEnergy, _gameplayOptions.noObstacles) + ")</color>";
			}
		}
		_rankText.text = LevelCompletionResults.GetRankName(_levelCompletionResults.rank);
		_goodCutsPercentageText.text = _levelCompletionResults.goodCutsCount + "<size=50%> / " + notesCount + "</size>";
		_fullComboText.gameObject.SetActive(_levelCompletionResults.fullCombo);
	}

	private void HandleAllScoresDidUpload()
	{
		_leaderboardViewControllerManager.forcedLoadingIndicator = false;
		_leaderboardViewControllerManager.Refresh();
	}

	public void ContinueButtonPressed()
	{
		if (this.continueButtonPressedEvent != null)
		{
			this.continueButtonPressedEvent(this);
		}
	}

	public void RestartButtonPressed()
	{
		if (this.restartButtonPressedEvent != null)
		{
			this.restartButtonPressedEvent(this);
		}
	}
}
