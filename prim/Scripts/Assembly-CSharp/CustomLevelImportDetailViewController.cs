using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRUI;

public class CustomLevelImportDetailViewController : VRUIViewController
{
	[SerializeField]
	private Button _selectButton;

	[SerializeField]
	private TextMeshProUGUI _infoText;

	public Action<CustomLevelImportDetailViewController, CustomLevelFileBrowserModel.CustomLevelFileBrowserItem> didSelectCustomLevelButtonPressEvent;

	public Action<CustomLevelImportDetailViewController, CustomMusicFileBrowserModel.MusicFileBrowserItem> didSelectCustomMusicButtonPressEvent;

	private FileBrowserItem _fileBrowserItem;

	public void Init(FileBrowserItem fileBrowserItem)
	{
		SetFileDetail(fileBrowserItem);
	}

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation)
		{
			_selectButton.onClick.AddListener(HandleSelectButton);
		}
	}

	public void SetFileDetail(FileBrowserItem fileBrowserItem)
	{
		_fileBrowserItem = fileBrowserItem;
		if (CustomLevelFileBrowserModel.CustomLevelFileBrowserItem.IsItemMember(_fileBrowserItem))
		{
			_infoText.text = _fileBrowserItem.displayName;
			_selectButton.gameObject.SetActive(value: true);
			_infoText.gameObject.SetActive(value: true);
		}
		else if (CustomMusicFileBrowserModel.MusicFileBrowserItem.IsItemMember(_fileBrowserItem))
		{
			_infoText.text = _fileBrowserItem.displayName;
			_selectButton.gameObject.SetActive(value: true);
			_infoText.gameObject.SetActive(value: true);
		}
		else
		{
			_selectButton.gameObject.SetActive(value: false);
			_infoText.gameObject.SetActive(value: false);
		}
	}

	public void HandleSelectButton()
	{
		if (CustomLevelFileBrowserModel.CustomLevelFileBrowserItem.IsItemMember(_fileBrowserItem) && didSelectCustomLevelButtonPressEvent != null)
		{
			CustomLevelFileBrowserModel.CustomLevelFileBrowserItem arg = (CustomLevelFileBrowserModel.CustomLevelFileBrowserItem)_fileBrowserItem;
			didSelectCustomLevelButtonPressEvent(this, arg);
		}
		else if (CustomMusicFileBrowserModel.MusicFileBrowserItem.IsItemMember(_fileBrowserItem) && didSelectCustomMusicButtonPressEvent != null)
		{
			CustomMusicFileBrowserModel.MusicFileBrowserItem arg2 = (CustomMusicFileBrowserModel.MusicFileBrowserItem)_fileBrowserItem;
			didSelectCustomMusicButtonPressEvent(this, arg2);
		}
	}
}
