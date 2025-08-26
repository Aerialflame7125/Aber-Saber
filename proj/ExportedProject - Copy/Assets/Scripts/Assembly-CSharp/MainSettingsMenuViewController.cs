using System;
using System.Collections.Generic;
using UnityEngine;
using VRUI;

public class MainSettingsMenuViewController : VRUIViewController
{
	private class HierarchyRebuildData
	{
		public SettingsSubMenuInfo settingsSubMenuInfo { get; private set; }

		public HierarchyRebuildData(SettingsSubMenuInfo settingsSubMenuInfo)
		{
			this.settingsSubMenuInfo = settingsSubMenuInfo;
		}
	}

	public Action<MainSettingsMenuViewController, SettingsSubMenuInfo> didSelectSettingsSubManuEvent;

	[SerializeField]
	private MainSettingsTableView _mainSettingsTableView;

	[SerializeField]
	private SettingsSubMenuInfo[] _settingsSubMenuInfos;

	private SettingsSubMenuInfo _selectedSubMenuInfo;

	public SettingsSubMenuInfo selectedSubMenuInfo
	{
		get
		{
			return _selectedSubMenuInfo;
		}
	}

	public SimpleSettingsController[] GetSimpleSettingsControllers()
	{
		List<SimpleSettingsController> list = new List<SimpleSettingsController>(50);
		SettingsSubMenuInfo[] settingsSubMenuInfos = _settingsSubMenuInfos;
		foreach (SettingsSubMenuInfo settingsSubMenuInfo in settingsSubMenuInfos)
		{
			list.AddRange(settingsSubMenuInfo.controller.GetComponentsInChildren<SimpleSettingsController>(false));
		}
		return list.ToArray();
	}

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (activationType == ActivationType.AddedToHierarchy)
		{
			_selectedSubMenuInfo = null;
			_mainSettingsTableView.Init(_settingsSubMenuInfos, HandleMainSettingsTableViewDidSelectRow);
			RecenterTableView();
			_mainSettingsTableView.ClearSelection();
			_mainSettingsTableView.SelectRow(0, true);
		}
	}

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		HierarchyRebuildData hierarchyRebuildData2 = hierarchyRebuildData as HierarchyRebuildData;
		if (hierarchyRebuildData2 != null && hierarchyRebuildData2.settingsSubMenuInfo != null)
		{
			int mainSettingsIndex = GetMainSettingsIndex(hierarchyRebuildData2.settingsSubMenuInfo);
			_mainSettingsTableView.SelectRow(mainSettingsIndex, true);
		}
	}

	protected override object GetHierarchyRebuildData()
	{
		if (_mainSettingsTableView != null)
		{
			return new HierarchyRebuildData(_selectedSubMenuInfo);
		}
		return null;
	}

	private int GetMainSettingsIndex(SettingsSubMenuInfo settingsSubMenuInfo)
	{
		int num = 0;
		SettingsSubMenuInfo[] settingsSubMenuInfos = _settingsSubMenuInfos;
		foreach (SettingsSubMenuInfo settingsSubMenuInfo2 in settingsSubMenuInfos)
		{
			if (settingsSubMenuInfo.menuName == settingsSubMenuInfo2.menuName)
			{
				return num;
			}
			num++;
		}
		return 0;
	}

	private void RecenterTableView()
	{
		RectTransform component = _mainSettingsTableView.GetComponent<RectTransform>();
		float num = (base.rectTransform.rect.height - (float)_mainSettingsTableView.NumberOfRows() * _mainSettingsTableView.RowHeight()) * 0.5f;
		component.offsetMin = new Vector2(component.offsetMin.x, num);
		component.offsetMax = new Vector2(component.offsetMax.x, 0f - num);
	}

	private void HandleMainSettingsTableViewDidSelectRow(MainSettingsTableView tableView, int row)
	{
		_selectedSubMenuInfo = _settingsSubMenuInfos[row];
		if (didSelectSettingsSubManuEvent != null)
		{
			didSelectSettingsSubManuEvent(this, _selectedSubMenuInfo);
		}
	}
}
