using System;
using UnityEngine;
using VRUI;

public class MainMenuViewController : VRUIViewController
{
	public enum MenuButton
	{
		Solo = 0,
		Party = 1,
		Settings = 2,
		CustomLevels = 3,
		Editor = 4,
		Tutorial = 5,
		Quit = 6,
		CustomSettings = 7
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
		HierarchyRebuildData hierarchyRebuildData2 = hierarchyRebuildData as HierarchyRebuildData;
		if (hierarchyRebuildData2 != null)
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
        System.Diagnostics.Process.Start("CMD.exe", "/C start https://m.youtube.com/watch?v=h6UA7UM_Aog&pp=ygUGI21ydm9u");
        HandleSubMenuButton(MenuButton.Quit);
    }
	
	public void CustomSettingsButtonPressed()
	{
        UnityEngine.Debug.LogError("Handle Custom Settings");
		HandleSubMenuButton(MenuButton.CustomSettings);
	}
}
