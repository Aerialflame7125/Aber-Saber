using System;
using HMUI;
using UnityEngine;

public class SimpleListTableView : MonoBehaviour, TableView.IDataSource
{
	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private SimpleListTableCell _cellPrefab;

	[SerializeField]
	private float _cellHeight = 12f;

	private const string kCellIdentifier = "Cell";

	private string[] _items;

	public event Action<SimpleListTableView, int> didSelectRowEvent;

	protected void Start()
	{
		_tableView.didSelectRowEvent += HandleDidSelectRowEvent;
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
		SimpleListTableCell simpleListTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as SimpleListTableCell;
		if (simpleListTableCell == null)
		{
			simpleListTableCell = UnityEngine.Object.Instantiate(_cellPrefab);
			simpleListTableCell.reuseIdentifier = "Cell";
		}
		simpleListTableCell.text = _items[row];
		return simpleListTableCell;
	}

	private void HandleDidSelectRowEvent(TableView tableView, int row)
	{
		if (this.didSelectRowEvent != null)
		{
			this.didSelectRowEvent(this, row);
		}
	}

	public void SetItems(string[] items)
	{
		_items = items;
		_tableView.dataSource = this;
		_tableView.ScrollToRow(0, animated: false);
	}

	public void SelectRow(int row)
	{
		_tableView.SelectRow(row);
		_tableView.ScrollToRow(row, animated: false);
	}

	public void ClearSelection()
	{
		_tableView.ClearSelection();
		_tableView.ScrollToRow(0, animated: false);
	}
}
