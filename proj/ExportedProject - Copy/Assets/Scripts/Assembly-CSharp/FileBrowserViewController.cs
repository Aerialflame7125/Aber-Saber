using System;
using UnityEngine;
using VRUI;

public class FileBrowserViewController : VRUIViewController
{
	private class HierarchyRebuildData
	{
		public FileBrowserItem[] fileBrowserItems { get; private set; }

		public int fileBrowserSelectedItemIndex { get; private set; }

		public HierarchyRebuildData(FileBrowserItem[] fileBrowserItems, int fileBrowserSelectedItemIndex)
		{
			this.fileBrowserItems = fileBrowserItems;
			this.fileBrowserSelectedItemIndex = fileBrowserSelectedItemIndex;
		}
	}

	[SerializeField]
	private FileBrowserTableView _fileBrowserTableView;

	private FileBrowserItem[] _fileBrowserItems;

	private bool _shouldSelectFirstItem;

	private HierarchyRebuildData _hierarchyRebuildData;

	public Action<FileBrowserViewController, FileBrowserItem> didSelectRowEvent;

	public void Init(FileBrowserItem[] fileBrowserItems, bool shouldSelectFirstItem = false)
	{
		_fileBrowserItems = fileBrowserItems;
		_shouldSelectFirstItem = shouldSelectFirstItem;
	}

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation)
		{
			_fileBrowserTableView.didSelectRow += HandleSelectRow;
		}
		if (activationType == ActivationType.AddedToHierarchy)
		{
			_fileBrowserTableView.Init(_fileBrowserItems);
			if (_shouldSelectFirstItem && _fileBrowserItems.Length > 0)
			{
				_fileBrowserTableView.SelectAndScrollRow(0);
			}
		}
	}

	public void SetFileBrowserItems(FileBrowserItem[] fileBrowserItems)
	{
		_fileBrowserItems = fileBrowserItems;
		if (_fileBrowserTableView.isActiveAndEnabled)
		{
			_fileBrowserTableView.SetItems(_fileBrowserItems);
		}
	}

	protected override void RebuildHierarchy(object hierarchyRebuildData)
	{
		HierarchyRebuildData hierarchyRebuildData2 = hierarchyRebuildData as HierarchyRebuildData;
		if (hierarchyRebuildData2 != null)
		{
			_fileBrowserTableView.Init(hierarchyRebuildData2.fileBrowserItems);
			_fileBrowserTableView.SelectAndScrollRow(hierarchyRebuildData2.fileBrowserSelectedItemIndex);
		}
	}

	protected override object GetHierarchyRebuildData()
	{
		return _hierarchyRebuildData;
	}

	private void HandleSelectRow(FileBrowserTableView fileBrowserTableView, int row)
	{
		_hierarchyRebuildData = new HierarchyRebuildData(_fileBrowserItems, row);
		if (didSelectRowEvent != null)
		{
			didSelectRowEvent(this, _fileBrowserItems[row]);
		}
	}
}
