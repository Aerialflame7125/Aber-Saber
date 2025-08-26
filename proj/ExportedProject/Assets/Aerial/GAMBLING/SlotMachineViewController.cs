using System;
using UnityEngine;
using VRUI;
using TMPro;

public class SlotMachineViewController : VRUIViewController
{
	[SerializeField]
	public SoundPlayer boowomp;

	[SerializeField]
	public SoundPlayer balatro;

	[SerializeField]
	private MenuSceneSetupData menuSceneSetupData;

	[SerializeField]
	private TextMeshProUGUI _mainText;

	[SerializeField]
	private GameObject _mainTextObject;

	public enum SubMenuType
	{
		Machine = 0,
		Back = 1,
		Free100 = 2
	}

	int count = 0;
	private class HierarchyRebuildData
	{
		public SubMenuType subMenuType;

		public HierarchyRebuildData(SubMenuType subMenuType)
		{
			this.subMenuType = subMenuType;
		}
	}

	public Action<SlotMachineViewController, SubMenuType> didFinishEvent;

	[SerializeField]
	private SimpleDialogPromptViewController _simpleDialogPromptViewController;

	[SerializeField]
	private Scores gamblingViewController;

	[SerializeField]
	private MainMenuViewController _mmvc;

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

	public void Free100WasPressed()
	{
		balatro.PlaySound();
		count++;
		if (count == 5)
		{
			gamblingViewController.AddChips(100);
			_mainTextObject.SetActive(true);
			_mainText.text = "You received 100 free chips, Reload the page.";
            Scores.Sleep(this, 10f);
			_mainTextObject.SetActive(false);
			boowomp.PlaySound();
			count = 7;
		}

	}
}
