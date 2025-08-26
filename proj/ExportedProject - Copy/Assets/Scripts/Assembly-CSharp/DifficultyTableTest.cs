using HMUI;
using UnityEngine;

public class DifficultyTableTest : MonoBehaviour, TableView.IDataSource
{
	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private DifficultyTableCell _cellPrefab;

	private const string kCellIdentifier = "Cell";

	private void Start()
	{
		_tableView.dataSource = this;
		_tableView.didSelectRowEvent += HandleDidSelectRowEvent;
	}

	public float RowHeight()
	{
		return 7f;
	}

	public int NumberOfRows()
	{
		return 5;
	}

	public TableCell CellForRow(int row)
	{
		DifficultyTableCell difficultyTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as DifficultyTableCell;
		if (difficultyTableCell == null)
		{
			difficultyTableCell = Object.Instantiate(_cellPrefab);
			difficultyTableCell.reuseIdentifier = "Cell";
		}
		difficultyTableCell.difficultyText = "Normal " + row;
		difficultyTableCell.difficultyValue = row + 1;
		return difficultyTableCell;
	}

	private void HandleDidSelectRowEvent(TableView tableView, int row)
	{
		Debug.Log(row);
	}
}
