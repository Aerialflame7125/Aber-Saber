using UnityEngine;

namespace BeatmapEditor;

public class BeatLinesTable : BeatmapEditorTable
{
	[SerializeField]
	private EditorBeatmapSO _editorBeatmap;

	[SerializeField]
	private BeatLineTableCell _beatLineTableCellPrefab;

	private static readonly string kCellIdentifier = "Cell";

	protected override void Start()
	{
		base.Start();
		_editorBeatmap.didChangeAllDataEvent += HandleEditorBeatmapDidChangeAllData;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		_editorBeatmap.didChangeAllDataEvent -= HandleEditorBeatmapDidChangeAllData;
	}

	protected override BeatmapEditorTableCell CellForRow(int row)
	{
		int beatsPerBar = _editorBeatmap.beatsPerBar;
		BeatLineTableCell beatLineTableCell = (BeatLineTableCell)DequeueReusableCell(kCellIdentifier);
		if (beatLineTableCell == null)
		{
			beatLineTableCell = Object.Instantiate(_beatLineTableCellPrefab);
			beatLineTableCell.reuseIdentifier = kCellIdentifier;
		}
		beatLineTableCell.type = ((row % 2 != 0) ? BeatLineTableCell.Type.Subdivision : BeatLineTableCell.Type.Bar);
		beatLineTableCell.text = ((row % beatsPerBar != 0) ? null : (row / beatsPerBar).ToString());
		if (beatsPerBar > 8)
		{
			if (row % beatsPerBar == 0)
			{
				beatLineTableCell.alpha = 1f;
			}
			else if (row % (beatsPerBar / 2) == 0)
			{
				beatLineTableCell.alpha = 0.3f;
			}
			else if (row % (beatsPerBar / 4) == 0)
			{
				beatLineTableCell.alpha = 0.2f;
			}
			else
			{
				beatLineTableCell.alpha = 0.05f;
			}
		}
		else if (row % beatsPerBar == 0)
		{
			beatLineTableCell.alpha = 1f;
		}
		else if (row % (beatsPerBar / 2) == 0)
		{
			beatLineTableCell.alpha = 0.3f;
		}
		else
		{
			beatLineTableCell.alpha = 0.05f;
		}
		return beatLineTableCell;
	}

	private void HandleEditorBeatmapDidChangeAllData()
	{
		UpdateAllCells();
	}
}
