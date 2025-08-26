using System;
using HMUI;
using UnityEngine;

public class StandardLevelListTableView : MonoBehaviour, TableView.IDataSource
{
	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private StandardLevelListTableCell _cellPrefab;

	[SerializeField]
	private float _cellHeight = 12f;

	private Action<StandardLevelListTableView, int> didSelectRow;

	private const string kCellIdentifier = "Cell";

	private IStandardLevel[] _levels;

	public void Init(IStandardLevel[] levels, Action<StandardLevelListTableView, int> didSelectRow)
	{
		_levels = levels;
		_tableView.dataSource = this;
		_tableView.ScrollToRow(0, animated: false);
		this.didSelectRow = didSelectRow;
		_tableView.didSelectRowEvent -= HandleDidSelectRowEvent;
		_tableView.didSelectRowEvent += HandleDidSelectRowEvent;
	}

	public float RowHeight()
	{
		return _cellHeight;
	}

	public int NumberOfRows()
	{
		if (_levels == null)
		{
			return 0;
		}
		return _levels.Length;
	}

	public TableCell CellForRow(int row)
	{
		StandardLevelListTableCell standardLevelListTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as StandardLevelListTableCell;
		if (standardLevelListTableCell == null)
		{
			standardLevelListTableCell = UnityEngine.Object.Instantiate(_cellPrefab);
			standardLevelListTableCell.reuseIdentifier = "Cell";
		}
		IStandardLevel standardLevel = _levels[row];
		standardLevelListTableCell.songName = $"{standardLevel.songName}\n<size=80%>{standardLevel.songSubName}</size>";
		standardLevelListTableCell.author = standardLevel.songAuthorName;
		standardLevelListTableCell.coverImage = standardLevel.coverImage;
		return standardLevelListTableCell;
	}

	private void HandleDidSelectRowEvent(TableView tableView, int row)
	{
		if (didSelectRow != null)
		{
			didSelectRow(this, row);
		}
	}

	public void SelectAndScrollToLevel(string levelID)
	{
		int row = RowNumberForLevelID(levelID);
		_tableView.SelectRow(row);
		_tableView.ScrollToRow(row, animated: false);
	}

	public void ClearSelection()
	{
		_tableView.ClearSelection();
		_tableView.ScrollToRow(0, animated: false);
	}

	public int RowNumberForLevelID(string levelID)
	{
		for (int i = 0; i < _levels.Length; i++)
		{
			if (_levels[i].levelID == levelID)
			{
				return i;
			}
		}
		return 0;
	}
}
