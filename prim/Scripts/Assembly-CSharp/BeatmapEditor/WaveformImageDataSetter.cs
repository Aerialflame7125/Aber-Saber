using UnityEngine;

namespace BeatmapEditor;

public class WaveformImageDataSetter : MonoBehaviour
{
	[SerializeField]
	private ObservableFloatSO _songTimeOffset;

	[SerializeField]
	private EditorAudioSO _editorAudio;

	[Space]
	[SerializeField]
	private WaveformImage _waveFormImage;

	private void Start()
	{
		_waveFormImage.ChangeParams(_songTimeOffset);
		_waveFormImage.SetDataFromAudioClip(_editorAudio.audioClip);
		_editorAudio.didChangeAudioEvent += HandleEditorAudioDidChangeAudio;
		_songTimeOffset.didChangeEvent += HandleSongTimeOffsetDidChange;
	}

	private void OnDestroy()
	{
		_editorAudio.didChangeAudioEvent -= HandleEditorAudioDidChangeAudio;
		_songTimeOffset.didChangeEvent -= HandleSongTimeOffsetDidChange;
	}

	private void HandleEditorAudioDidChangeAudio(AudioClip audioClip)
	{
		_waveFormImage.SetDataFromAudioClip(_editorAudio.audioClip);
	}

	private void HandleSongTimeOffsetDidChange()
	{
		_waveFormImage.ChangeParams(_songTimeOffset);
	}
}
