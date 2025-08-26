using UnityEngine;

namespace BeatmapEditor;

public class EventsTable : GridTable
{
	[SerializeField]
	private EventsTableCell _eventsTableCellPrefab;

	private static readonly string kCellIdentifier = "Cell";

	private EditorBeatmapSO _editorBeatmap;

	private EventSetDrawStyleSO _eventSetDrawStyle;

	private float _columnWidth;

	protected override int numberOfColumns => 16;

	protected override float columnWidth => _columnWidth;

	public void Init(EditorBeatmapSO editorBeatmap, EventSetDrawStyleSO eventSetDrawStyle)
	{
		_editorBeatmap = editorBeatmap;
		_eventSetDrawStyle = eventSetDrawStyle;
		_editorBeatmap.didChangeAllDataEvent -= HandleEditorBeatmapDidChangeAllData;
		_editorBeatmap.didChangeAllDataEvent += HandleEditorBeatmapDidChangeAllData;
	}

	protected override void Awake()
	{
		base.Awake();
		_columnWidth = _eventsTableCellPrefab.GetComponent<RectTransform>().sizeDelta.x / (float)numberOfColumns;
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
		bool flag = false;
		if (row < _editorBeatmap.beatsDataLength)
		{
			beatData = _editorBeatmap.BeatData(row);
		}
		else if (_editorBeatmap.beatsDataLength > 0)
		{
			beatData = _editorBeatmap.BeatData(_editorBeatmap.beatsDataLength - 1);
			flag = true;
		}
		if (beatData == null)
		{
			return null;
		}
		bool flag2 = true;
		EditorEventData[] array = null;
		if (beatData != null)
		{
			array = beatData.eventsData;
			for (int i = 0; i < numberOfColumns; i++)
			{
				if (array[i] != null)
				{
					flag2 = false;
					break;
				}
			}
		}
		if (flag2)
		{
			return null;
		}
		EventsTableCell eventsTableCell = (EventsTableCell)DequeueReusableCell(kCellIdentifier);
		if (eventsTableCell == null)
		{
			eventsTableCell = Object.Instantiate(_eventsTableCellPrefab);
			eventsTableCell.reuseIdentifier = kCellIdentifier;
		}
		for (int j = 0; j < numberOfColumns; j++)
		{
			int num = 0;
			Color color = Color.clear;
			Sprite image = null;
			if (array != null && array[j] != null)
			{
				num = array[j].value;
				if (_eventSetDrawStyle.events[j] != null)
				{
					EventDrawStyleSO.SubEventDrawStyle[] subEvents = _eventSetDrawStyle.events[j].subEvents;
					foreach (EventDrawStyleSO.SubEventDrawStyle subEventDrawStyle in subEvents)
					{
						if (subEventDrawStyle.eventValue == num)
						{
							if (array[j].isPreviousValidValue || flag)
							{
								color = subEventDrawStyle.eventActiveColor;
								continue;
							}
							color = subEventDrawStyle.color;
							image = subEventDrawStyle.image;
						}
					}
				}
			}
			bool flag3 = array != null && array[j] != null;
			string text = string.Empty;
			if (flag3 && !array[j].isPreviousValidValue && !flag)
			{
				text = num.ToString();
			}
			eventsTableCell.SetLineActive(j, flag3, color, image, text);
		}
		return eventsTableCell;
	}
}
