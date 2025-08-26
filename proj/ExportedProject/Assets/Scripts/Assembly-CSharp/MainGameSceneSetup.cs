using UnityEngine;

public class MainGameSceneSetup : MonoBehaviour
{
	[SerializeField]
	private StandardLevelSO _defaultStandardLevel;

	[SerializeField]
	private LevelDifficulty _defaultDifficulty;

	[Space]
	[SerializeField]
	private MainGameSceneSetupData _mainGameSceneSetupData;

	[Space]
	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[Space]
	[SerializeField]
	private AudioTimeSyncController _audioTimeSyncController;

	[SerializeField]
	private GameEnergyCounter _gameEnergyCounter;

	[SerializeField]
	private PauseMenuManager _pauseMenuManager;

	[SerializeField]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	[SerializeField]
	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	[SerializeField]
	private PrepareLevelEndMenuSceneSetupData _prepareLevelEndMenuSceneSetupData;

	[SerializeField]
	private BeatmapDataModel _beatmapDataModel;

	private void Awake()
	{
		if (!_mainGameSceneSetupData.wasUsedInLastTransition)
		{
			if (_defaultStandardLevel.GetDifficultyLevel(_defaultDifficulty) == null)
			{
				_defaultDifficulty = _defaultStandardLevel.difficultyBeatmaps[0].difficulty;
				Debug.LogWarning("Default difficulty not found. Using first one instead.");
			}
			GameplayOptions gameplayOptions = new GameplayOptions();
			gameplayOptions.noEnergy = true;
			gameplayOptions.mirror = false;
			_mainGameSceneSetupData.Init(_defaultStandardLevel.GetDifficultyLevel(_defaultDifficulty), gameplayOptions, GameplayMode.PartyStandard, 0f);
		}
		IStandardLevelDifficultyBeatmap difficultyLevel = _mainGameSceneSetupData.difficultyLevel;
		IStandardLevel level = difficultyLevel.level;
		float forcedStartSongTime = _mainGameSceneSetupData.forcedStartSongTime;
		BeatmapData beatmapData = BeatDataTransformHelper.CreateTransformedBeatmapData(difficultyLevel.beatmapData, _mainGameSceneSetupData.gameplayOptions, _mainGameSceneSetupData.gameplayMode);
		_beatmapDataModel.beatmapData = beatmapData;
		_beatmapObjectCallbackController.Init(forcedStartSongTime);
		float num = difficultyLevel.noteJumpMovementSpeed;
		if (num <= 0f)
		{
			num = difficultyLevel.difficulty.NoteJumpMovementSpeed();
		}
		_beatmapObjectSpawnController.Init(level.beatsPerMinute, beatmapData.beatmapLinesData.Length, num);
		_audioTimeSyncController.Init(level.audioClip, forcedStartSongTime, level.songTimeOffset);
		_gameEnergyCounter.Init(_mainGameSceneSetupData.gameplayOptions.noEnergy);
		_pauseMenuManager.Init(level.songName, level.songSubName, difficultyLevel.difficulty.Name());
		_prepareLevelEndMenuSceneSetupData.Init(level.audioClip.length, beatmapData.notesCount);
		Saber.SaberType saberType;
		if (UseOneSwordOnly(out saberType))
		{
			_playerControllerProvider.GetProvidedObject<PlayerController>().AllowOnlyOneSaber(saberType);
		}
	}

	private bool UseOneSwordOnly(out Saber.SaberType saberType)
	{
		saberType = Saber.SaberType.SaberA;
		if (_mainGameSceneSetupData.gameplayMode == GameplayMode.SoloOneSaber)
		{
			saberType = ((!_mainGameSceneSetupData.gameplayOptions.mirror) ? Saber.SaberType.SaberB : Saber.SaberType.SaberA);
			return true;
		}
		return false;
	}
}
