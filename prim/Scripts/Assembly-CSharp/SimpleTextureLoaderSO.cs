using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTextureLoaderSO : ScriptableObject
{
	private Dictionary<string, Texture2D> cache = new Dictionary<string, Texture2D>();

	public void LoadTexture(string filePath, bool useCache, Action<Texture2D> finishedCallback)
	{
		SharedCoroutineStarter.instance.StartCoroutine(LoadTextureCoroutine(filePath, useCache, finishedCallback));
	}

	public IEnumerator LoadTextureCoroutine(string filePath, bool useCache, Action<Texture2D> finishedCallback)
	{
		if (useCache && cache.ContainsKey(filePath))
		{
			finishedCallback?.Invoke(cache[filePath]);
			yield break;
		}
		using WWW www = new WWW(FileHelpers.GetEscapedURLForFilePath(filePath));
		yield return www;
		if (www.error == null)
		{
			Texture2D texture = www.texture;
			cache[filePath] = texture;
			finishedCallback?.Invoke(texture);
		}
		else
		{
			finishedCallback?.Invoke(null);
		}
	}
}
