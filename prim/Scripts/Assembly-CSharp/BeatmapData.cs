public class BeatmapData
{
	public BeatmapLineData[] beatmapLinesData { get; private set; }

	public BeatmapEventData[] beatmapEventData { get; private set; }

	public int notesCount { get; private set; }

	public int obstaclesCount { get; private set; }

	public BeatmapData(BeatmapLineData[] beatmapLinesData, BeatmapEventData[] beatmapEventData)
	{
		this.beatmapLinesData = beatmapLinesData;
		this.beatmapEventData = beatmapEventData;
		foreach (BeatmapLineData beatmapLineData in beatmapLinesData)
		{
			BeatmapObjectData[] beatmapObjectsData = beatmapLineData.beatmapObjectsData;
			foreach (BeatmapObjectData beatmapObjectData in beatmapObjectsData)
			{
				if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note)
				{
					NoteType noteType = ((NoteData)beatmapObjectData).noteType;
					if (noteType == NoteType.NoteA || noteType == NoteType.NoteB)
					{
						notesCount++;
					}
				}
				else if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Obstacle)
				{
					obstaclesCount++;
				}
			}
		}
	}

	public BeatmapData GetCopy()
	{
		BeatmapLineData[] array = new BeatmapLineData[beatmapLinesData.Length];
		for (int i = 0; i < beatmapLinesData.Length; i++)
		{
			BeatmapLineData beatmapLineData = beatmapLinesData[i];
			BeatmapLineData beatmapLineData2 = new BeatmapLineData();
			beatmapLineData2.beatmapObjectsData = new BeatmapObjectData[beatmapLineData.beatmapObjectsData.Length];
			for (int j = 0; j < beatmapLineData.beatmapObjectsData.Length; j++)
			{
				BeatmapObjectData beatmapObjectData = beatmapLineData.beatmapObjectsData[j];
				beatmapLineData2.beatmapObjectsData[j] = beatmapObjectData.GetCopy();
			}
			array[i] = beatmapLineData2;
		}
		BeatmapEventData[] array2 = new BeatmapEventData[this.beatmapEventData.Length];
		for (int k = 0; k < this.beatmapEventData.Length; k++)
		{
			BeatmapEventData beatmapEventData = this.beatmapEventData[k];
			array2[k] = beatmapEventData.GetCopy();
		}
		return new BeatmapData(array, array2);
	}
}
