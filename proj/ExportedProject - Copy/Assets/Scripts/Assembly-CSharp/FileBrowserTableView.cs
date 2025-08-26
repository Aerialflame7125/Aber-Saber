using System;
using HMUI;
using UnityEngine;

public class FileBrowserTableView : MonoBehaviour, TableView.IDataSource
{
	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private FileBrowserTableCell _cellPrefab;

	[SerializeField]
	private float _cellHeight = 12f;

	private const string kCellIdentifier = "Cell";

	private FileBrowserItem[] _items;

	public event Action<FileBrowserTableView, int> didSelectRow;

	public void Init(FileBrowserItem[] items)
	{
		_items = items;
		_tableView.dataSource = this;
		_tableView.ScrollToRow(0, false);
		_tableView.didSelectRowEvent -= HandleDidSelectRowEvent;
		_tableView.didSelectRowEvent += HandleDidSelectRowEvent;
	}

	public void SetItems(FileBrowserItem[] items)
	{
		_items = items;
		_tableView.ReloadData();
		_tableView.ScrollToRow(0, false);
	}

	public bool SelectAndScrollRowToItemWithPath(string folderPath)
	{
		int num = 0;
		FileBrowserItem[] items = _items;
		foreach (FileBrowserItem fileBrowserItem in items)
		{
			if (folderPath == fileBrowserItem.fullPath)
			{
				SelectAndScrollRow(num);
				return true;
			}
			num++;
		}
		return false;
	}

	public float RowHeight()
	{
		return _cellHeight;
	}

	public int NumberOfRows()
	{
		if (_items == null)
		{
			return 0;
		}
		return _items.Length;
	}

	public TableCell CellForRow(int row)
	{
		FileBrowserTableCell fileBrowserTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as FileBrowserTableCell;
		if (fileBrowserTableCell == null)
		{
			fileBrowserTableCell = UnityEngine.Object.Instantiate(_cellPrefab);
			fileBrowserTableCell.reuseIdentifier = "Cell";
		}
		FileBrowserItem fileBrowserItem = _items[row];
		fileBrowserTableCell.text = fileBrowserItem.displayName;
		return fileBrowserTableCell;
	}

	private void HandleDidSelectRowEvent(TableView tableView, int row)
	{
		if (this.didSelectRow != null)
		{
			this.didSelectRow(this, row);
		}
	}

	public void SelectAndScrollRow(int row)
	{
		_tableView.SelectRow(row, true);
		_tableView.ScrollToRow(row, false);
	}

	public void ClearSelection(bool animated = false, bool scrollToRow0 = true)
	{
		_tableView.ClearSelection();
		if (scrollToRow0)
		{
			_tableView.ScrollToRow(0, animated);
		}
	}
}
