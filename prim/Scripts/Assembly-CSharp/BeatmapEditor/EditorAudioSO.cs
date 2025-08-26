using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace BeatmapEditor;

public class EditorAudioSO : ScriptableObject
{
	[SerializeField]
	private AudioClipLoaderSO _audioClipLoader;

	private AudioClip _audioClip;

	private string _audioFilePath;

	public AudioClip audioClip => _audioClip;

	public bool isAudioLoaded => _audioClip != null;

	public float songDuration => (!(_audioClip != null)) ? 0f : _audioClip.length;

	public string audioFilePath => _audioFilePath;

	public string audioFileName => Path.GetFileName(_audioFilePath);

	public event Action<AudioClip> didChangeAudioEvent = delegate
	{
	};

	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
	}

	public IEnumerator LoadAudioCoroutine(string filePath, Action didLoadAction)
	{
		return _audioClipLoader.LoadAudioFileCoroutine(filePath, delegate(AudioClip audioClip)
		{
			HandleLoadAudio(audioClip, filePath, didLoadAction);
		});
	}

	public void LoadAudio(string filePath, Action didLoadAction)
	{
		_audioClipLoader.LoadAudioFile(filePath, delegate(AudioClip audioClip)
		{
			HandleLoadAudio(audioClip, filePath, didLoadAction);
		});
	}

	private void HandleLoadAudio(AudioClip audioClip, string filePath, Action didLoadAction)
	{
		_audioFilePath = filePath;
		_audioClip = audioClip;
		if (this.didChangeAudioEvent != null)
		{
			this.didChangeAudioEvent(audioClip);
		}
		didLoadAction?.Invoke();
	}

	public void Clear()
	{
		_audioClip = null;
		_audioFilePath = null;
		if (this.didChangeAudioEvent != null)
		{
			this.didChangeAudioEvent(null);
		}
	}
}
