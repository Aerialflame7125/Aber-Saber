using System;
using UnityEngine;
using VRUI;

public class CustomSettingsViewController : VRUINavigationController
{

	public enum FinishAction
	{
		Ok = 0,
		Cancel = 1,
		Apply = 2
	}

	private class HierarchyRebuildData
	{
		public FinishAction finishAction;

		public HierarchyRebuildData(FinishAction finishAction)
		{
			this.finishAction = finishAction;
		}
	}

	[SerializeField]
	private MainSettingsMenuViewController _mainSettingsMenuViewController;

	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private HierarchyRebuildData _hierarchyRebuildData;

	private SimpleSettingsController[] _simpleSettingsControllers;

	private SettingsSubMenuInfo selectedSettingsSubMenuInfo;

	public event Action<CustomSettingsViewController, FinishAction> didFinishEvent;

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
        UnityEngine.Debug.LogError("Activated i think");
		if (firstActivation)
		{
			_simpleSettingsControllers = _mainSettingsMenuViewController.GetSimpleSettingsControllers();
			SimpleSettingsController[] simpleSettingsControllers = _simpleSettingsControllers;
			foreach (SimpleSettingsController simpleSettingsController in simpleSettingsControllers)
			{
				simpleSettingsController.Init();
			}
		}
		selectedSettingsSubMenuInfo = null;
		MainSettingsMenuViewController mainSettingsMenuViewController = _mainSettingsMenuViewController;
		mainSettingsMenuViewController.didSelectSettingsSubManuEvent = (Action<MainSettingsMenuViewController, SettingsSubMenuInfo>)Delegate.Combine(mainSettingsMenuViewController.didSelectSettingsSubManuEvent, new Action<MainSettingsMenuViewController, SettingsSubMenuInfo>(HandleDidSelectSettingsSubManuEvent));
		PushViewController(_mainSettingsMenuViewController, true);
	}

	protected override void DidDeactivate(DeactivationType deactivationType)
	{
		MainSettingsMenuViewController mainSettingsMenuViewController = _mainSettingsMenuViewController;
		mainSettingsMenuViewController.didSelectSettingsSubManuEvent = (Action<MainSettingsMenuViewController, SettingsSubMenuInfo>)Delegate.Remove(mainSettingsMenuViewController.didSelectSettingsSubManuEvent, new Action<MainSettingsMenuViewController, SettingsSubMenuInfo>(HandleDidSelectSettingsSubManuEvent));
	}

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		HierarchyRebuildData hierarchyRebuildData2 = hierarchyRebuildData as HierarchyRebuildData;
		if (hierarchyRebuildData2 != null)
		{
			HandleFinishButton(hierarchyRebuildData2.finishAction);
		}
	}

	protected override object GetHierarchyRebuildData()
	{
		return _hierarchyRebuildData;
	}

	private void HandleDidSelectSettingsSubManuEvent(MainSettingsMenuViewController mainSettingsViewController, SettingsSubMenuInfo settingsSubMenuInfo)
	{
		bool immediately = selectedSettingsSubMenuInfo != null;
		if (selectedSettingsSubMenuInfo != null)
		{
			PopViewControllerImmediately();
		}
		selectedSettingsSubMenuInfo = settingsSubMenuInfo;
		PushViewController(selectedSettingsSubMenuInfo.controller, immediately);
	}

	private void HandleFinishButton(FinishAction finishAction)
	{
		_hierarchyRebuildData = new HierarchyRebuildData(finishAction);
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent(this, finishAction);
		}
	}

	private void ApplySettings()
	{
		SimpleSettingsController[] simpleSettingsControllers = _simpleSettingsControllers;
		foreach (SimpleSettingsController simpleSettingsController in simpleSettingsControllers)
		{
			simpleSettingsController.ApplySettings();
		}
		_mainSettingsModel.Save();
	}

	private void CancelSettings()
	{
		SimpleSettingsController[] simpleSettingsControllers = _simpleSettingsControllers;
		foreach (SimpleSettingsController simpleSettingsController in simpleSettingsControllers)
		{
			simpleSettingsController.CancelSettings();
		}
		_mainSettingsModel.Save();
	}

	public void OkButtonPressed()
	{
		ApplySettings();
		HandleFinishButton(FinishAction.Ok);
	}

	public void ApplyButtonPressed()
	{
		ApplySettings();
		HandleFinishButton(FinishAction.Apply);
	}

	public void CancelButtonPressed()
	{
		CancelSettings();
		HandleFinishButton(FinishAction.Cancel);
	}
}
