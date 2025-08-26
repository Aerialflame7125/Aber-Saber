using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLevelAudioClipLoader : ScriptableObject
{
	private Dictionary<string, AudioClip> cache = new Dictionary<string, AudioClip>();

	public void LoadAudioClipFromPath(string path, Action<AudioClip> callback, bool stream, bool useCahe = true)
	{
		SharedCoroutineStarter.instance.StartCoroutine(LoadAudioClipFromPathCoroutine(path, callback, useCahe));
	}

	public IEnumerator LoadAudioClipFromPathCoroutine(string path, Action<AudioClip> callback, bool stream, bool useCahe = true)
	{
		if (useCahe && cache.ContainsKey(path))
		{
			callback(cache[path]);
			yield return null;
			yield break;
		}
		yield return new WaitForSecondsRealtime(UnityEngine.Random.Range(0f, 0.1f));
		using WWW www = new WWW(FileHelpers.GetEscapedURLForFilePath(path));
		yield return www;
		AudioClip audioClip = www.GetAudioClip(threeD: true, stream);
		cache[path] = audioClip;
		yield return WaitUntillAudioIsLoaded(audioClip, delegate
		{
			if (callback != null)
			{
				callback(audioClip);
			}
		});
	}

	private IEnumerator WaitUntillAudioIsLoaded(AudioClip audioClip, Action action)
	{
		if (audioClip != null && audioClip.loadState != AudioDataLoadState.Loaded)
		{
			yield return new WaitUntil(() => audioClip.loadState == AudioDataLoadState.Loaded);
		}
		action();
	}
}
