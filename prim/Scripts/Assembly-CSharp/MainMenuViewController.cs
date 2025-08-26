using System;
using UnityEngine;
using VRUI;

public class MainMenuViewController : VRUIViewController
{
	public enum MenuButton
	{
		Solo,
		Party,
		Settings,
		CustomLevels,
		Editor,
		Tutorial,
		Quit
	}

	private class HierarchyRebuildData
	{
		public MenuButton menuButton;

		public HierarchyRebuildData(MenuButton menuButton)
		{
			this.menuButton = menuButton;
		}
	}

	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	[SerializeField]
	private VRUIViewController _howToPlayViewController;

	[SerializeField]
	private VRUIViewController _releaseInfoViewController;

	[Space]
	[SerializeField]
	private GameObject _customLevelsButtonGO;

	[SerializeField]
	private GameObject _editorButtonGO;

	private HierarchyRebuildData _hierarchyRebuildData;

	public event Action<MainMenuViewController, MenuButton> didFinishEvent;

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		_customLevelsButtonGO.SetActive(_mainSettingsModel.enableAlphaFeatures);
		_editorButtonGO.SetActive(_mainSettingsModel.enableAlphaFeatures);
	}

	protected override void LeftAndRightScreenViewControllers(out VRUIViewController leftScreenViewController, out VRUIViewController rightScreenViewController)
	{
		leftScreenViewController = _howToPlayViewController;
		rightScreenViewController = _releaseInfoViewController;
	}

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		if (hierarchyRebuildData is HierarchyRebuildData hierarchyRebuildData2)
		{
			HandleSubMenuButton(hierarchyRebuildData2.menuButton);
		}
	}

	protected override object GetHierarchyRebuildData()
	{
		return _hierarchyRebuildData;
	}

	private void HandleSubMenuButton(MenuButton menuButton)
	{
		_hierarchyRebuildData = new HierarchyRebuildData(menuButton);
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent(this, menuButton);
		}
	}

	public void SoloButtonPressed()
	{
		HandleSubMenuButton(MenuButton.Solo);
	}

	public void PartyButtonPressed()
	{
		HandleSubMenuButton(MenuButton.Party);
	}

	public void CustomLevelsButtonPressed()
	{
		HandleSubMenuButton(MenuButton.CustomLevels);
	}

	public void SettingsButtonPressed()
	{
		HandleSubMenuButton(MenuButton.Settings);
	}

	public void TutorialButtonPressed()
	{
		HandleSubMenuButton(MenuButton.Tutorial);
	}

	public void EditorButtonPressed()
	{
		HandleSubMenuButton(MenuButton.Editor);
	}

	public void QuitGameButtonPressed()
	{
		HandleSubMenuButton(MenuButton.Quit);
	}
}
