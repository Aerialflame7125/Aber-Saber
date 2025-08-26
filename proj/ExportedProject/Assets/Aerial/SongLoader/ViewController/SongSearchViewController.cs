using System;
using UnityEngine;
using VRUI;
using TMPro;

public class SongSearchViewController : VRUIViewController
{

    public enum SubMenuType
    {
        Download = 0,
        Back = 1
    }

    private class HierarchyRebuildData
    {
        public SubMenuType subMenuType;

        public HierarchyRebuildData(SubMenuType subMenuType)
        {
            this.subMenuType = subMenuType;
        }
    }

    public Action<SongSearchViewController, SubMenuType> didFinishEvent;

    [SerializeField]
    private MainMenuViewController _mmvc;

    [SerializeField]
    private MainFlowCoordinator mainFlowCoordinator;

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

    public void DismissButtonWasPressed(MainFlowCoordinator mainFlowCoordinator)
    {
        // Notify listeners that the user wants to go back
        //HandleSubMenuButton(SubMenuType.Back);
        mainFlowCoordinator.ReturnToMainMenu(_mmvc);
    }

    public void DownloadButtonWasPressed()
    {
        Debug.Log("User clicked Download button in SongSearchViewController.");
    }
}
