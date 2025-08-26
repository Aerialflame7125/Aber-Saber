using System;
using HMUI;
using UnityEngine;

public class CustomLevelListTableView : MonoBehaviour, TableView.IDataSource
{
    [SerializeField]
    private TableView _tableView;

    [SerializeField]
    private TableView _stableview;

    private StandardLevelListTableView _stableviews;

    [SerializeField]
    private CustomLevelListTableCell _cellPrefab;

    [SerializeField]
    private float _cellHeight = 12f;

    private Action<StandardLevelListTableView, int> didSelectRow;

    private const string kCellIdentifier = "Cell";

    private IStandardLevel[] _levels;

    public void Init(IStandardLevel[] levels, Action<StandardLevelListTableView, int> didSelectRow)
    {
        _levels = levels;
        _tableView.dataSource = this;
        _tableView.ScrollToRow(0, false);
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
        CustomLevelListTableCell customLevelListTableCell = _tableView.DequeueReusableCellForIdentifier("Cell") as CustomLevelListTableCell;
        if (customLevelListTableCell == null)
        {
            customLevelListTableCell = UnityEngine.Object.Instantiate(_cellPrefab);
            customLevelListTableCell.reuseIdentifier = "Cell";
        }
        IStandardLevel customLevel = _levels[row];
        customLevelListTableCell.songName = string.Format("{0}\n<size=80%>{1}</size>", customLevel.songName, customLevel.songSubName);
        customLevelListTableCell.author = customLevel.songAuthorName;
        customLevelListTableCell.coverImage = customLevel.coverImage;
        return customLevelListTableCell;
    }

    private void HandleDidSelectRowEvent(TableView _stableView, int row)
    {
        CustomLevelListTableView customLevelListTableView = this;
        customLevelListTableView.didSelectRow?.Invoke(_stableviews, row);
    }

    public void SelectAndScrollToLevel(string levelID)
    {
        int row = RowNumberForLevelID(levelID);
        _tableView.SelectRow(row);
        _tableView.ScrollToRow(row, false);
    }

    public void ClearSelection()
    {
        _tableView.ClearSelection();
        _tableView.ScrollToRow(0, false);
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
