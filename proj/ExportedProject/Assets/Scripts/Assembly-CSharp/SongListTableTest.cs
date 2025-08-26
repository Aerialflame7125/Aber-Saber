using HMUI;
using UnityEngine;

public class SongListTableTest : MonoBehaviour, TableView.IDataSource
{
	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private StandardLevelListTableCell _cellPrefab;

	private const string kCellIdentifier = "Cell";

	private void Start()
	{
		_tableView.dataSource = this;
		_tableView.didSelectRowEvent += HandleDidSelectRowEvent;
	}

	public float RowHeight()
	{
		return 10f;
	}

	public int NumberOfRows()
	{
		return 50;
	}

	public TableCell CellForRow(int row)
	{
		StandardLevelListTableCell standardLevelListTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as StandardLevelListTableCell;
		if (standardLevelListTableCell == null)
		{
			standardLevelListTableCell = Object.Instantiate(_cellPrefab);
			standardLevelListTableCell.reuseIdentifier = "Cell";
		}
		standardLevelListTableCell.songName = "Beat Saber " + row;
		standardLevelListTableCell.author = "Jaroslav Beck";
		return standardLevelListTableCell;
	}

	private void HandleDidSelectRowEvent(TableView tableView, int row)
	{
		Debug.Log(row);
	}
}
