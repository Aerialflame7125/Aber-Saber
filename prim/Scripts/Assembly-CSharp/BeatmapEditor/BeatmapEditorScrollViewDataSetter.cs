using UnityEngine;

namespace BeatmapEditor;

public class BeatmapEditorScrollViewDataSetter : MonoBehaviour
{
	[SerializeField]
	private EditorBeatmapSO _editorBeatmap;

	[SerializeField]
	private ObservableFloatSO _beatsPerMinute;

	[SerializeField]
	private EditorAudioSO _editorAudio;

	[Space]
	[SerializeField]
	private BeatmapEditorScrollView _beatmapEditorScrollView;

	private void Start()
	{
		RefreshParams();
		_editorBeatmap.didChangeAllDataEvent += HandleEditorBeatmapDidChangeAllData;
		_editorAudio.didChangeAudioEvent += HandleEditorAudioDidChangeAudio;
		_beatsPerMinute.didChangeEvent += HandleBeatsPerMinutDidChange;
	}

	private void OnDestroy()
	{
		_editorBeatmap.didChangeAllDataEvent -= HandleEditorBeatmapDidChangeAllData;
		_editorAudio.didChangeAudioEvent -= HandleEditorAudioDidChangeAudio;
		_beatsPerMinute.didChangeEvent -= HandleBeatsPerMinutDidChange;
	}

	private void RefreshParams()
	{
		float num = 60f / (float)_beatsPerMinute * 4f / (float)_editorBeatmap.beatsPerBar;
		float num2 = _editorAudio.songDuration;
		if (num2 < 0.001f)
		{
			num2 = (float)_editorBeatmap.beatsDataLength * num;
		}
		_beatmapEditorScrollView.ChangeParams(_beatsPerMinute, _editorBeatmap.beatsPerBar, num2);
	}

	private void HandleBeatsPerMinutDidChange()
	{
		RefreshParams();
	}

	private void HandleEditorBeatmapDidChangeAllData()
	{
		RefreshParams();
	}

	private void HandleEditorAudioDidChangeAudio(AudioClip audioClip)
	{
		RefreshParams();
	}
}
