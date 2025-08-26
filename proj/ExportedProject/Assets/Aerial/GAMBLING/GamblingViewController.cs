using System;
using UnityEngine;
using VRUI;
using TMPro;

public class GamblingViewController : VRUIViewController
{
	[SerializeField]
	public SoundPlayer boowomp;

	[SerializeField]
	public MenuSceneSetupData menuSceneSetupData;

	public enum SubMenuType
	{
		Gambling = 0,
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

	public Action<GamblingViewController, SubMenuType> didFinishEvent;

    [SerializeField]
    private SimpleDialogPromptViewController _simpleDialogPromptViewController;

	[SerializeField]
	private MainMenuViewController _mmvc;

	[SerializeField]
	private MainFlowCoordinator mainFlowCoordinator;

	[SerializeField]
	private TextMeshProUGUI _aerialText;

	[SerializeField]
	private SlotMachineViewController _slotMachineViewController;

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

    public void GamblingButtonWasPressed()
    {
        bool ready = checkGamblingStatus(_aerialText);
        if (ready)
        {
            _aerialText.text = "GamblingViewController: 'Gambling button pressed. LETS FUCKING GAMBLE BAYBEEEE'";
            // Dismiss this modal, then present the slot machine
            this.DismissModalViewController(() =>
            {
                _mmvc.PresentModalViewController(_slotMachineViewController, null, false);
                _slotMachineViewController.didFinishEvent += HandleSlotMachineViewControllerDidFinish;
            }, false);
        }
    }
	private bool checkGamblingStatus(TextMeshProUGUI stattext) 
	{
		bool ready = true;
		if (ready != false)
		{
			stattext.text = "GamblingPlugin: 'Ready = True'";
			return ready;
		} else {
			if (boowomp != null)
			{
				boowomp.PlaySound();
			}
			stattext.text = "GamblingPlugin: 'Gambling is not implemented yet, but you can still enjoy the boowomp!'";
			return ready;
		}
		
	}

	public void DismissButtonWasPressed(MainFlowCoordinator mainFlowCoordinator)
	{
		// Notify listeners that the user wants to go back
		//HandleSubMenuButton(SubMenuType.Back);
		mainFlowCoordinator.ReturnToMainMenu(_mmvc);
	}

	private void HandleSlotMachineViewControllerDidFinish(SlotMachineViewController vc, SlotMachineViewController.SubMenuType subMenuType)
	{
	    // Remove the event handler to avoid memory leaks
	    _slotMachineViewController.didFinishEvent -= HandleSlotMachineViewControllerDidFinish;

	    // Optionally, do something when the slot machine is finished
	    _aerialText.text = "SlotMachineViewController: 'Slot machine finished!'";
	}
}
