using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRUI;

public class CustomLevelImportNavigationController : VRUINavigationController
{
	[SerializeField]
	private TMP_Text _pathText;

	[SerializeField]
	private Button _backButton;

	[SerializeField]
	private LoadingIndicator _loadingIndicator;

	public LoadingIndicator loadingIndicator
	{
		get
		{
			return _loadingIndicator;
		}
	}

	public event Action<CustomLevelImportNavigationController> didFinishEvent;

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation)
		{
			_backButton.onClick.AddListener(HandleBackButton);
		}
		if (activationType == ActivationType.AddedToHierarchy)
		{
			_pathText.text = string.Empty;
		}
	}

	public void SetPath(string path)
	{
		_pathText.text = path;
	}

	public void HandleBackButton()
	{
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent(this);
		}
	}
}
