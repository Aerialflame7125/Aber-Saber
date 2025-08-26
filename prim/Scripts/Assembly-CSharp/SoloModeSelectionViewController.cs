using System;
using VRUI;

public class SoloModeSelectionViewController : VRUIViewController
{
	public enum SubMenuType
	{
		FreePlayMode,
		NoArrowsMode,
		OneSaberMode,
		Back
	}

	private class HierarchyRebuildData
	{
		public SubMenuType subMenuType;

		public HierarchyRebuildData(SubMenuType subMenuType)
		{
			this.subMenuType = subMenuType;
		}
	}

	public Action<SoloModeSelectionViewController, SubMenuType> didFinishEvent;

	private HierarchyRebuildData _hierarchyRebuildData;

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		if (hierarchyRebuildData is HierarchyRebuildData hierarchyRebuildData2)
		{
			HandleSubMenuButton(hierarchyRebuildData2.subMenuType);
		}
	}

	protected override object GetHierarchyRebuildData()
	{
		return _hierarchyRebuildData;
	}

	private void HandleSubMenuButton(SubMenuType subMenuType)
	{
		_hierarchyRebuildData = new HierarchyRebuildData(subMenuType);
		if (didFinishEvent != null)
		{
			didFinishEvent(this, subMenuType);
		}
	}

	public void FreePlayModeButtonWasPressed()
	{
		HandleSubMenuButton(SubMenuType.FreePlayMode);
	}

	public void OneSaberModeButtonWasPressed()
	{
		HandleSubMenuButton(SubMenuType.OneSaberMode);
	}

	public void NoArrowsModeButtonWasPressed()
	{
		HandleSubMenuButton(SubMenuType.NoArrowsMode);
	}

	public void DismissButtonWasPressed()
	{
		if (didFinishEvent != null)
		{
			didFinishEvent(this, SubMenuType.Back);
		}
	}
}
