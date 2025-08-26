using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class EditorLevelCoverImagePanelController : MonoBehaviour
{
	[SerializeField]
	private EditorLevelCoverImageSO _coverImage;

	[SerializeField]
	private AlertPanelController _alertPanelController;

	[SerializeField]
	private LoadingIndicator _loadingIndicator;

	[SerializeField]
	private Texture _defaultTexture;

	[SerializeField]
	private RawImage _image;

	[SerializeField]
	private Button _importButton;

	[SerializeField]
	private Button _clearButton;

	private ButtonBinder _buttonBinder;

	private void Awake()
	{
		_buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
		{
			{
				_importButton,
				(Action)ImportButtonPressed
			},
			{
				_clearButton,
				(Action)delegate
				{
					_coverImage.Clear();
				}
			}
		});
		RefreshImageTexture(_coverImage.texture);
		_coverImage.didChangeTextureEvent += HandleCoverImageDidChangeTexture;
	}

	private void OnDestroy()
	{
		if (_buttonBinder != null)
		{
			_buttonBinder.ClearBindings();
		}
		_coverImage.didChangeTextureEvent -= HandleCoverImageDidChangeTexture;
	}

	private void HandleCoverImageDidChangeTexture(Texture2D texture)
	{
		RefreshImageTexture(texture);
	}

	private void RefreshImageTexture(Texture2D texture)
	{
		if (texture == null)
		{
			_image.texture = _defaultTexture;
		}
		else
		{
			_image.texture = texture;
		}
	}

	private void ImportButtonPressed()
	{
		StartCoroutine(ImportImageCoroutine());
	}

	private IEnumerator ImportImageCoroutine()
	{
		_loadingIndicator.ShowLoading();
		yield return null;
		string filePath = NativeFileDialogs.OpenFileDialog("Choose image", "png", null);
		if (filePath == null)
		{
			_loadingIndicator.HideLoading();
			yield break;
		}
		yield return _coverImage.LoadImageCoroutine(filePath, delegate(EditorLevelCoverImageSO.LoadingResult loadingResult)
		{
			switch (loadingResult)
			{
			case EditorLevelCoverImageSO.LoadingResult.BadAspect:
				_alertPanelController.Show("Warning", "Loading failed. Only images with 1:1 aspect ratio and resoluton higher or equal to 256x256 pixels are supported.", "OK", delegate
				{
					_alertPanelController.Hide();
				});
				break;
			case EditorLevelCoverImageSO.LoadingResult.LowResolution:
				_alertPanelController.Show("Warning", "Loading failed. Only images with 1:1 aspect ratio and resoluton higher or equal to 256x256 pixels are supported.", "OK", delegate
				{
					_alertPanelController.Hide();
				});
				break;
			case EditorLevelCoverImageSO.LoadingResult.NoTextureLoaded:
				_alertPanelController.Show("Error", "Texture could not be loaded.", "OK", delegate
				{
					_alertPanelController.Hide();
				});
				break;
			}
		});
		_loadingIndicator.HideLoading();
	}
}
