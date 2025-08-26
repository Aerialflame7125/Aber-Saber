using UnityEngine;

public class BeatmapDataNoArrowsTransform
{
	public static BeatmapData CreateTransformedData(BeatmapData beatmapData)
	{
		beatmapData = beatmapData.GetCopy();
		BeatmapLineData[] beatmapLinesData = beatmapData.beatmapLinesData;
		int[] array = new int[beatmapLinesData.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = 0;
		}
		Random.InitState(0);
		bool flag = false;
		do
		{
			flag = false;
			float num = 999999f;
			int num2 = 0;
			for (int j = 0; j < beatmapLinesData.Length; j++)
			{
				BeatmapObjectData[] beatmapObjectsData = beatmapLinesData[j].beatmapObjectsData;
				for (int k = array[j]; k < beatmapObjectsData.Length && beatmapObjectsData[k].time < num + 0.001f; k++)
				{
					flag = true;
					BeatmapObjectData beatmapObjectData = beatmapObjectsData[k];
					float time = beatmapObjectData.time;
					if (Mathf.Abs(time - num) < 0.001f)
					{
						if (beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note)
						{
							num2++;
						}
					}
					else if (time < num)
					{
						num = time;
						num2 = ((beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note) ? 1 : 0);
					}
				}
			}
			for (int l = 0; l < beatmapLinesData.Length; l++)
			{
				BeatmapObjectData[] beatmapObjectsData2 = beatmapLinesData[l].beatmapObjectsData;
				for (int m = array[l]; m < beatmapObjectsData2.Length && beatmapObjectsData2[m].time < num + 0.001f; m++)
				{
					BeatmapObjectData beatmapObjectData2 = beatmapObjectsData2[m];
					if (beatmapObjectData2.beatmapObjectType == BeatmapObjectType.Note && beatmapObjectData2 is NoteData noteData)
					{
						noteData.SetNoteToAnyCutDirection();
						if (num2 <= 2)
						{
							noteData.TransformNoteAOrBToRandomType();
						}
					}
					array[l]++;
				}
			}
		}
		while (flag);
		return beatmapData;
	}
}
