using System;
using HMUI;
using UnityEngine;

namespace BeatmapEditor;

public class OpenLevelTableController : MonoBehaviour, TableView.IDataSource
{
	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private OpenLevelTableCell _cellPrefab;

	[SerializeField]
	private float _cellHeight = 8f;

	public Action<int> didSelectRowAction;

	public Action<int> didRequestDeleteAction;

	private const string kCellIdentifier = "Cell";

	private string[] _levelNames;

	public void Init(string[] levelNames, Action<int> didSelectRowAction, Action<int> didRequestDeleteAction)
	{
		_levelNames = levelNames;
		_tableView.dataSource = this;
		this.didSelectRowAction = didSelectRowAction;
		this.didRequestDeleteAction = didRequestDeleteAction;
		_tableView.didSelectRowEvent -= HandleDidSelectRowEvent;
		_tableView.didSelectRowEvent += HandleDidSelectRowEvent;
	}

	public void SetContent(string[] levelNames)
	{
		_levelNames = levelNames;
		_tableView.ReloadData();
	}

	public float RowHeight()
	{
		return _cellHeight;
	}

	public int NumberOfRows()
	{
		if (_levelNames == null)
		{
			return 0;
		}
		return _levelNames.Length;
	}

	public TableCell CellForRow(int row)
	{
		OpenLevelTableCell openLevelTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as OpenLevelTableCell;
		if (openLevelTableCell == null)
		{
			openLevelTableCell = UnityEngine.Object.Instantiate(_cellPrefab);
			openLevelTableCell.reuseIdentifier = "Cell";
			openLevelTableCell.deleteButtonPressedEvent += HandleCellDeleteButtonPressed;
		}
		openLevelTableCell.text = _levelNames[row];
		return openLevelTableCell;
	}

	private void HandleCellDeleteButtonPressed(int row)
	{
		if (didRequestDeleteAction != null)
		{
			didRequestDeleteAction(row);
		}
	}

	private void HandleDidSelectRowEvent(TableView tableView, int row)
	{
		if (didSelectRowAction != null)
		{
			didSelectRowAction(row);
		}
	}

	public void SelectRow(int row, bool callbackTable)
	{
		_tableView.SelectRow(row, callbackTable);
	}

	public void ClearSelection()
	{
		_tableView.ClearSelection();
	}
}
