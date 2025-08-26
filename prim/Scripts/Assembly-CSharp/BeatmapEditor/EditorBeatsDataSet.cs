using System.Collections.Generic;

namespace BeatmapEditor;

public class EditorBeatsDataSet
{
	public Dictionary<LevelDifficulty, EditorBeatsData> _editorBeatmapsData;

	public EditorBeatsData this[LevelDifficulty levelDifficulty]
	{
		get
		{
			if (_editorBeatmapsData == null)
			{
				return null;
			}
			EditorBeatsData value = null;
			_editorBeatmapsData.TryGetValue(levelDifficulty, out value);
			return value;
		}
		set
		{
			if (_editorBeatmapsData == null)
			{
				_editorBeatmapsData = new Dictionary<LevelDifficulty, EditorBeatsData>();
			}
			_editorBeatmapsData[levelDifficulty] = value;
		}
	}

	public void Clear()
	{
		if (_editorBeatmapsData != null)
		{
			_editorBeatmapsData.Clear();
		}
	}
}
