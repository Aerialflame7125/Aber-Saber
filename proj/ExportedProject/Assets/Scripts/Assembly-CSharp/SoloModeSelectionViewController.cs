using System;
using UnityEngine;
using VRUI;

public class SoloModeSelectionViewController : VRUIViewController
{

	public enum SubMenuType
	{
		FreePlayMode = 0,
		NoArrowsMode = 1,
		OneSaberMode = 2,
		Back = 3
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

    [SerializeField]
    private SimpleDialogPromptViewController _simpleDialogPromptViewController;

    private HierarchyRebuildData _hierarchyRebuildData;

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		HierarchyRebuildData hierarchyRebuildData2 = hierarchyRebuildData as HierarchyRebuildData;
		if (hierarchyRebuildData2 != null)
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
        // Set up the event handler for dialog completion
        _simpleDialogPromptViewController.didFinishEvent += HandleNoArrowsDialogFinished;

        // Initialize the dialog with appropriate text
        _simpleDialogPromptViewController.Init(
            "Custom Levels",
            "Would you like to play with Custom Levels? Be aware, these maps are very retarded and may not work. I am going to throw a brick at the person responsible for making this shitty implemenation but whatever.",
            "Maybe",
            "Beat Games Sucks"
        );
		
        // Present the dialog as a modal view controller
        PresentModalViewController(_simpleDialogPromptViewController, null);
    }

    private void HandleNoArrowsDialogFinished(SimpleDialogPromptViewController viewController, bool ok)
    {
        // Remove the event handler
        viewController.didFinishEvent -= HandleNoArrowsDialogFinished;

        if (!viewController.isRebuildingHierarchy)
        {
            // Dismiss the dialog
            viewController.DismissModalViewController(null);

            // If user confirmed, proceed with No Arrows Mode
            if (ok)
            {
				HandleSubMenuButton(SubMenuType.NoArrowsMode);
            }
            // If not ok, we simply return without doing anything
        }
    }

    public void DismissButtonWasPressed()
	{
		if (didFinishEvent != null)
		{
			didFinishEvent(this, SubMenuType.Back);
		}
	}
}
