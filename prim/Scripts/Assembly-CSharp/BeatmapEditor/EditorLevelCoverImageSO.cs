using System;
using System.Collections;
using System.IO;
using UnityEngine;

namespace BeatmapEditor;

public class EditorLevelCoverImageSO : ScriptableObject
{
	public enum LoadingResult
	{
		Sucess,
		NoTextureLoaded,
		LowResolution,
		BadAspect
	}

	[SerializeField]
	private SimpleTextureLoaderSO _imageLoader;

	private Texture2D _texture;

	private string _imageFilePath;

	public Texture2D texture => _texture;

	public string imageFilePath => _imageFilePath;

	public string imageFileName => Path.GetFileName(_imageFilePath);

	public event Action<Texture2D> didChangeTextureEvent;

	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
	}

	public IEnumerator LoadImageCoroutine(string filePath, Action<LoadingResult> didLoadAction)
	{
		return _imageLoader.LoadTextureCoroutine(filePath, useCache: false, delegate(Texture2D texture)
		{
			HandleLoadedTexture(texture, filePath, didLoadAction);
		});
	}

	public void LoadImage(string filePath, Action<LoadingResult> didLoadAction)
	{
		_imageLoader.LoadTexture(filePath, useCache: false, delegate(Texture2D texture)
		{
			HandleLoadedTexture(texture, filePath, didLoadAction);
		});
	}

	private void HandleLoadedTexture(Texture2D texture, string filePath, Action<LoadingResult> didLoadAction)
	{
		LoadingResult obj = LoadingResult.Sucess;
		if (texture == null)
		{
			obj = LoadingResult.NoTextureLoaded;
		}
		else if (texture.width != texture.height)
		{
			texture = null;
			obj = LoadingResult.BadAspect;
		}
		else if (texture.width < 256 || texture.height < 256)
		{
			texture = null;
			obj = LoadingResult.LowResolution;
		}
		if (texture != null)
		{
			_imageFilePath = filePath;
			_texture = texture;
			if (this.didChangeTextureEvent != null)
			{
				this.didChangeTextureEvent(texture);
			}
		}
		didLoadAction?.Invoke(obj);
	}

	public void Clear()
	{
		_texture = null;
		_imageFilePath = null;
		if (this.didChangeTextureEvent != null)
		{
			this.didChangeTextureEvent(null);
		}
	}
}
