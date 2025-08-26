using UnityEngine;

namespace BeatmapEditor;

public class BeatmapTestStarter : MonoBehaviour
{
	public enum TestStartResult
	{
		Success,
		NoAudio,
		NoBeatsData
	}

	[SerializeField]
	private BeatmapEditorSceneSetupData _beatmapEditorSceneSetupData;

	[SerializeField]
	private ObservableFloatSO _beatsPerMinute;

	[SerializeField]
	private ObservableFloatSO _shuffleStrength;

	[SerializeField]
	private ObservableFloatSO _shufflePeriod;

	[SerializeField]
	private EditorBeatmapSO _editorBeatmap;

	[SerializeField]
	private EditorAudioSO _editorAudio;

	[SerializeField]
	private PlaybackController _playbackController;

	[SerializeField]
	private ActiveDifficultySO _activeDifficulty;

	public TestStartResult TestBeatmap()
	{
		if (!_editorBeatmap.hasBeatsData)
		{
			return TestStartResult.NoBeatsData;
		}
		if (!_editorAudio.isAudioLoaded)
		{
			return TestStartResult.NoAudio;
		}
		float songTime = _playbackController.songTime;
		_playbackController.PauseSong();
		GameplayOptions defaultOptions = GameplayOptions.defaultOptions;
		defaultOptions.noEnergy = true;
		BeatmapSaveData beatmapSaveData = _editorBeatmap.beatsData.ConvertToBeatsSaveData(_beatsPerMinute, clipToTime: true, _editorAudio.songDuration);
		if (beatmapSaveData == null)
		{
			return TestStartResult.NoBeatsData;
		}
		StandardLevelSO standardLevelSO = ScriptableObject.CreateInstance<StandardLevelSO>();
		BeatmapDataSO beatmapDataSO = ScriptableObject.CreateInstance<BeatmapDataSO>();
		beatmapDataSO.beatmapData = BeatmapDataLoader.GetBeatmapDataFromBeatmapSaveData(beatmapSaveData, _beatsPerMinute, _shuffleStrength, _shufflePeriod);
		standardLevelSO.InitSimple("CustomLevel", "CustomLevel", null, beatmapDataSO, _editorAudio.audioClip, _activeDifficulty.difficulty);
		_beatmapEditorSceneSetupData.StartLevel(standardLevelSO.difficultyBeatmaps[0], defaultOptions, GameplayMode.SoloStandard, songTime);
		return TestStartResult.Success;
	}
}
