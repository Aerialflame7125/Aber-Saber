using System;
using HMUI;
using UnityEngine;

public class MainSettingsTableView : MonoBehaviour, TableView.IDataSource
{
	[SerializeField]
	private TableView _tableView;

	[SerializeField]
	private MainSettingsTableCell _cellPrefab;

	[SerializeField]
	private float _cellHeight = 8f;

	public Action<MainSettingsTableView, int> didSelectRow;

	private const string kCellIdentifier = "Cell";

	private SettingsSubMenuInfo[] _settingsSubMenuInfos;

	public void Init(SettingsSubMenuInfo[] settingsSubMenuInfos, Action<MainSettingsTableView, int> didSelectRow)
	{
		_settingsSubMenuInfos = settingsSubMenuInfos;
		_tableView.dataSource = this;
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
		if (_settingsSubMenuInfos == null)
		{
			return 0;
		}
		return _settingsSubMenuInfos.Length;
	}

	public TableCell CellForRow(int row)
	{
		MainSettingsTableCell mainSettingsTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as MainSettingsTableCell;
		if (mainSettingsTableCell == null)
		{
			mainSettingsTableCell = UnityEngine.Object.Instantiate(_cellPrefab);
			mainSettingsTableCell.reuseIdentifier = "Cell";
		}
		SettingsSubMenuInfo settingsSubMenuInfo = _settingsSubMenuInfos[row];
		mainSettingsTableCell.settingsSubMenuText = settingsSubMenuInfo.menuName;
		return mainSettingsTableCell;
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
