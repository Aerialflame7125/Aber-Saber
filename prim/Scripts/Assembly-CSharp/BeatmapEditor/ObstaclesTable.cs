using UnityEngine;

namespace BeatmapEditor;

public class ObstaclesTable : GridTable
{
	[SerializeField]
	private ObstaclesTableCell _obstaclesTableCellPrefab;

	private static readonly string kCellIdentifier = "Cell";

	private EditorBeatmapSO _editorBeatmap;

	private float _columnWidth;

	protected override int numberOfColumns => 4;

	protected override float columnWidth => _columnWidth;

	public void Init(EditorBeatmapSO editorBeatmap)
	{
		_editorBeatmap = editorBeatmap;
		_editorBeatmap.didChangeAllDataEvent -= HandleEditorBeatmapDidChangeAllData;
		_editorBeatmap.didChangeAllDataEvent += HandleEditorBeatmapDidChangeAllData;
	}

	protected override void Awake()
	{
		base.Awake();
		_columnWidth = _obstaclesTableCellPrefab.GetComponent<RectTransform>().sizeDelta.x / (float)numberOfColumns;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_editorBeatmap.didChangeAllDataEvent -= HandleEditorBeatmapDidChangeAllData;
	}

	private void HandleEditorBeatmapDidChangeAllData()
	{
		UpdateAllCells();
	}

	protected override BeatmapEditorTableCell CellForRow(int row)
	{
		BeatData beatData = null;
		if (row < _editorBeatmap.beatsDataLength)
		{
			beatData = _editorBeatmap.BeatData(row);
		}
		if (beatData == null)
		{
			return null;
		}
		bool flag = true;
		EditorObstacleData[] array = null;
		if (beatData != null)
		{
			array = beatData.obstaclesData;
			for (int i = 0; i < 4; i++)
			{
				if (array[i] != null)
				{
					flag = false;
					break;
				}
			}
		}
		if (flag)
		{
			return null;
		}
		ObstaclesTableCell obstaclesTableCell = (ObstaclesTableCell)DequeueReusableCell(kCellIdentifier);
		if (obstaclesTableCell == null)
		{
			obstaclesTableCell = Object.Instantiate(_obstaclesTableCellPrefab);
			obstaclesTableCell.reuseIdentifier = kCellIdentifier;
		}
		for (int j = 0; j < 4; j++)
		{
			obstaclesTableCell.SetLineActive(j, array != null && array[j] != null);
			if (array != null && array[j] != null)
			{
				obstaclesTableCell.SetLineType(j, array[j].type);
			}
		}
		return obstaclesTableCell;
	}
}
