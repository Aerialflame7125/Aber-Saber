using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRUI;

public class CustomLevelImportViewController : VRUIViewController
{
	public enum SelectedButton
	{
		SelectLevelFile,
		SelectMusicFile,
		Import,
		Cancel
	}

	private class HierarchyRebuildData
	{
		public SelectedButton selectedButton;

		public HierarchyRebuildData(SelectedButton selectedButton)
		{
			this.selectedButton = selectedButton;
		}
	}

	[SerializeField]
	private Button _selectCustomLevelFileButton;

	[SerializeField]
	private Button _selectMusicFileButton;

	[SerializeField]
	private Button _importButton;

	[SerializeField]
	private Button _backButton;

	[SerializeField]
	private LoadingIndicator _loadingIndicator;

	[SerializeField]
	private TMP_Text _levelPathText;

	[SerializeField]
	private TMP_Text _musicPathText;

	[SerializeField]
	private CustomLevelsModelSO _customLevelsModel;

	private HierarchyRebuildData _hierarchyRebuildData;

	public string selectedLevelID { get; private set; }

	public event Action<CustomLevelImportViewController, SelectedButton> didFinishEvent;

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation)
		{
			_selectCustomLevelFileButton.onClick.AddListener(HandleSelectCustomLevelFileButton);
			_selectMusicFileButton.onClick.AddListener(HandleSelectMusicFileButton);
			_backButton.onClick.AddListener(HandleBackButton);
			_importButton.onClick.AddListener(HandleSelectImportButton);
		}
		if (activationType == ActivationType.AddedToHierarchy)
		{
			_levelPathText.text = string.Empty;
			_musicPathText.text = string.Empty;
		}
	}

	public void SetCustomLevelFileBrowserItem(CustomLevelFileBrowserModel.CustomLevelFileBrowserItem item)
	{
		_levelPathText.text = item.fullPath;
		_selectMusicFileButton.interactable = true;
	}

	public void SetMusicBrowserItem(CustomMusicFileBrowserModel.MusicFileBrowserItem item)
	{
		_musicPathText.text = item.fullPath;
		_importButton.interactable = true;
	}

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		HierarchyRebuildData hierarchyRebuildData2 = hierarchyRebuildData as HierarchyRebuildData;
		if (hierarchyRebuildData2 == null)
		{
		}
	}

	protected override object GetHierarchyRebuildData()
	{
		return _hierarchyRebuildData;
	}

	public void HandleSelectCustomLevelFileButton()
	{
		_hierarchyRebuildData = new HierarchyRebuildData(SelectedButton.SelectLevelFile);
		HandleFinishedButton(_hierarchyRebuildData.selectedButton);
	}

	public void HandleSelectMusicFileButton()
	{
		_hierarchyRebuildData = new HierarchyRebuildData(SelectedButton.SelectMusicFile);
		HandleFinishedButton(_hierarchyRebuildData.selectedButton);
	}

	public void HandleBackButton()
	{
		_hierarchyRebuildData = new HierarchyRebuildData(SelectedButton.Cancel);
		HandleFinishedButton(_hierarchyRebuildData.selectedButton);
	}

	public void HandleSelectImportButton()
	{
		_hierarchyRebuildData = new HierarchyRebuildData(SelectedButton.Import);
		_loadingIndicator.ShowLoading();
		string tmpDirPath = null;
		try
		{
			tmpDirPath = Path.Combine(Application.temporaryCachePath, "LevelImport");
			if (Directory.Exists(tmpDirPath))
			{
				Directory.Delete(tmpDirPath, recursive: true);
			}
			Directory.CreateDirectory(tmpDirPath);
		}
		catch
		{
			_loadingIndicator.HideLoading();
			tmpDirPath = null;
		}
		Action finishAndCleanup = delegate
		{
			_loadingIndicator.HideLoading();
			try
			{
				if (Directory.Exists(tmpDirPath))
				{
					Directory.Delete(tmpDirPath, recursive: true);
				}
			}
			catch
			{
			}
		};
		if (tmpDirPath == null)
		{
			return;
		}
		CustomLevelsModelSO.ExtractStandardLevelAndGetSongFilename(_levelPathText.text, tmpDirPath, delegate(CustomLevelsModelSO.ExtractStandardLevelAndGetSongFilenameResult result, string songFilename)
		{
			if (result == CustomLevelsModelSO.ExtractStandardLevelAndGetSongFilenameResult.Success)
			{
				CustomLevelsModelSO.ImportStandardLevel(tmpDirPath, _musicPathText.text, delegate(CustomLevelsModelSO.ImportStandardLevelResult result2)
				{
					if (result2 == CustomLevelsModelSO.ImportStandardLevelResult.Success)
					{
					}
					finishAndCleanup();
				});
			}
			else
			{
				finishAndCleanup();
			}
		});
	}

	private void HandleFinishedButton(SelectedButton button)
	{
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent(this, button);
		}
	}
}
