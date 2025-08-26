using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class AudioClipLoaderSO : ScriptableObject
{
	private bool _isLoading;

	private void OnEnable()
	{
		_isLoading = false;
	}

	public void LoadAudioFile(string filePath, Action<AudioClip> finishCallback)
	{
		if (!_isLoading)
		{
			SharedCoroutineStarter.instance.StartCoroutine(LoadAudioFileCoroutine(filePath, finishCallback));
		}
	}

	public IEnumerator LoadAudioFileCoroutine(string filePath, Action<AudioClip> finishCallback)
	{
		if (!File.Exists(filePath))
		{
			finishCallback(null);
			yield break;
		}
		_isLoading = true;
		using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(FileHelpers.GetEscapedURLForFilePath(filePath), AudioType.UNKNOWN);
		yield return www.SendWebRequest();
		_isLoading = false;
		AudioClip audioClip = null;
		if (!www.isNetworkError)
		{
			audioClip = DownloadHandlerAudioClip.GetContent(www);
			if (audioClip != null && audioClip.loadState != AudioDataLoadState.Loaded)
			{
				audioClip = null;
			}
		}
		finishCallback?.Invoke(audioClip);
	}
}
