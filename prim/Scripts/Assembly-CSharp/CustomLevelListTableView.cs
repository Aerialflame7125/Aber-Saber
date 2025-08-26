using System;
using HMUI;
using UnityEngine;

public class CustomLevelListTableView : MonoBehaviour, TableView.IDataSource
{
	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private CustomLevelListTableCell _cellPrefab;

	[SerializeField]
	private float _cellHeight = 12f;

	[SerializeField]
	private SimpleTextureLoaderSO customLevelCoverImageLoader;

	private Action<CustomLevelListTableView, int> didSelectRow;

	private const string kCellIdentifier = "Cell";

	private CustomLevelInfoWrapper[] _levels;

	public void Init(CustomLevelInfoWrapper[] levels, Action<CustomLevelListTableView, int> didSelectRow)
	{
		SetItems(levels);
		this.didSelectRow = didSelectRow;
		_tableView.didSelectRowEvent -= HandleDidSelectRowEvent;
		_tableView.didSelectRowEvent += HandleDidSelectRowEvent;
	}

	public void SetItems(CustomLevelInfoWrapper[] levels)
	{
		_levels = levels;
		_tableView.dataSource = this;
		if (_levels != null)
		{
			_tableView.ScrollToRow(0, animated: false);
		}
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
		CustomLevelListTableCell cell = _tableView.DequeueReusableCellForIdentifier("Cell") as CustomLevelListTableCell;
		if (cell == null)
		{
			cell = UnityEngine.Object.Instantiate(_cellPrefab);
			cell.reuseIdentifier = "Cell";
		}
		CustomLevelInfoWrapper level = _levels[row];
		cell.levelID = level.levelID;
		cell.songName = $"{level.customLevelInfo.songName}\n<size=80%>{level.customLevelInfo.songSubName}</size>";
		cell.author = level.customLevelInfo.authorName;
		cell.coverImageTexture = null;
		Action<Texture> finishedCallback = delegate(Texture texture)
		{
			if (cell.levelID == level.levelID)
			{
				cell.coverImageTexture = texture;
			}
		};
		customLevelCoverImageLoader.LoadTexture(level.coverImageFilePath, useCache: true, finishedCallback);
		return cell;
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
