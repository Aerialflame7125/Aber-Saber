using UnityEngine;

namespace BeatmapEditor;

public class NotesTable : GridTable
{
	[SerializeField]
	private NotesTableCell _notesTableCellPrefab;

	private static readonly string kCellIdentifier = "Cell";

	private EditorBeatmapSO _editorBeatmap;

	private NoteLineLayer _noteLineLayer;

	private float _columnWidth;

	protected override int numberOfColumns => 4;

	protected override float columnWidth => _columnWidth;

	public void Init(EditorBeatmapSO editorBeatmap, NoteLineLayer noteLineLayer)
	{
		_editorBeatmap = editorBeatmap;
		_noteLineLayer = noteLineLayer;
		_editorBeatmap.didChangeAllDataEvent -= HandleEditorBeatmapDidChangeAllData;
		_editorBeatmap.didChangeAllDataEvent += HandleEditorBeatmapDidChangeAllData;
	}

	protected override void Awake()
	{
		base.Awake();
		_columnWidth = _notesTableCellPrefab.GetComponent<RectTransform>().sizeDelta.x / (float)numberOfColumns;
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
		if (_editorBeatmap == null)
		{
			return null;
		}
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
		EditorNoteData[] array = null;
		if (beatData != null)
		{
			array = beatData.NoteDataForLineLayer(_noteLineLayer);
			for (int i = 0; i < numberOfColumns; i++)
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
		NotesTableCell notesTableCell = (NotesTableCell)DequeueReusableCell(kCellIdentifier);
		if (notesTableCell == null)
		{
			notesTableCell = Object.Instantiate(_notesTableCellPrefab);
			notesTableCell.reuseIdentifier = kCellIdentifier;
		}
		for (int j = 0; j < numberOfColumns; j++)
		{
			notesTableCell.SetLineActive(j, array != null && array[j] != null);
			if (array != null && array[j] != null)
			{
				notesTableCell.SetLineType(j, array[j].type, array[j].cutCirection);
			}
		}
		return notesTableCell;
	}
}
