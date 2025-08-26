using System;
using HMUI;
using UnityEngine;

public class DifficultyTableView : MonoBehaviour, TableView.IDataSource
{
	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private DifficultyTableCell _cellPrefab;

	[SerializeField]
	private float _cellHeight = 8f;

	public Action<DifficultyTableView, int> didSelectRow;

	private const string kCellIdentifier = "Cell";

	private IStandardLevelDifficultyBeatmap[] _difficultyLevels;

	public void Init(IStandardLevelDifficultyBeatmap[] difficultyLevels, Action<DifficultyTableView, int> didSelectRow)
	{
		_difficultyLevels = difficultyLevels;
		_tableView.dataSource = this;
		this.didSelectRow = didSelectRow;
		_tableView.didSelectRowEvent -= HandleDidSelectRowEvent;
		_tableView.didSelectRowEvent += HandleDidSelectRowEvent;
	}

	public void SetDifficultyLevels(IStandardLevelDifficultyBeatmap[] difficultyLevels)
	{
		_difficultyLevels = difficultyLevels;
		_tableView.ReloadData();
	}

	public float RowHeight()
	{
		return _cellHeight;
	}

	public int NumberOfRows()
	{
		if (_difficultyLevels == null)
		{
			return 0;
		}
		return _difficultyLevels.Length;
	}

	public TableCell CellForRow(int row)
	{
		DifficultyTableCell difficultyTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as DifficultyTableCell;
		if (difficultyTableCell == null)
		{
			difficultyTableCell = UnityEngine.Object.Instantiate(_cellPrefab);
			difficultyTableCell.reuseIdentifier = "Cell";
		}
		IStandardLevelDifficultyBeatmap standardLevelDifficultyBeatmap = _difficultyLevels[row];
		difficultyTableCell.difficultyText = standardLevelDifficultyBeatmap.difficulty.Name();
		difficultyTableCell.difficultyValue = standardLevelDifficultyBeatmap.difficultyRank;
		return difficultyTableCell;
	}

	private void HandleDidSelectRowEvent(TableView tableView, int row)
	{
		if (didSelectRow != null)
		{
			didSelectRow(this, row);
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
