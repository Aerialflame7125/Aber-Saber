using System;
using UnityEngine;
using VRUI;

public class MultiplayerMenuViewController : VRUIViewController
{
	public enum MenuButton
	{
		SongSelector = 0,
		Ready = 1,
		Start = 2,
		Leave = 3
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
	private MultiplayerReleaseInfoViewController _releaseInfoViewController;
	private HierarchyRebuildData _hierarchyRebuildData;

	public event Action<MultiplayerMenuViewController, MenuButton> didFinishEvent;

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

	public void SongSelectorButtonPressed()
	{
		HandleSubMenuButton(MenuButton.SongSelector);
	}

	public void ReadyButtonPressed()
	{
		HandleSubMenuButton(MenuButton.Ready);
	}

	public void StartButtonPressed()
	{
		HandleSubMenuButton(MenuButton.Start);
	}

	public void LeaveMultiplayerLobbyButtonPressed()
	{
		System.Diagnostics.Process.Start("CMD.exe", "/C start https://m.youtube.com/watch?v=h6UA7UM_Aog&pp=ygUGI21ydm9u");
		HandleSubMenuButton(MenuButton.Leave);
	}
}
