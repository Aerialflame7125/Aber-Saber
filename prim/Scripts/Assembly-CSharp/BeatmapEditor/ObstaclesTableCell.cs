using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class ObstaclesTableCell : BeatmapEditorTableCell
{
	[SerializeField]
	private Image[] _backgrounds;

	private void Awake()
	{
	}

	public void SetLineActive(int lineIdx, bool active)
	{
		_backgrounds[lineIdx].gameObject.SetActive(active);
	}

	public void SetLineType(int lineIdx, ObstacleType type)
	{
		_backgrounds[lineIdx].color = ((type != 0) ? Color.green : Color.magenta);
	}
}
