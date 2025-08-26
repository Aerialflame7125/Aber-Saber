using System.Collections.Generic;
using UnityEngine;

public class BeatmapDataLoader
{
	private static float GetRealTimeFromBpmTime(float bmpTime, float beatsPerMinute, float shuffle, float shufflePeriod)
	{
		float num = bmpTime;
		if (shufflePeriod > 0f && (int)(num * (1f / shufflePeriod)) % 2 == 1)
		{
			num += shuffle * shufflePeriod;
		}
		if (beatsPerMinute > 0f)
		{
			num = num / beatsPerMinute * 60f;
		}
		return num;
	}

	public static BeatmapData GetBeatmapDataFromBeatmapSaveData(BeatmapSaveData beatmapSaveData, float beatsPerMinute, float shuffle, float shufflePeriod)
	{
		List<BeatmapObjectData>[] array = new List<BeatmapObjectData>[4];
		List<BeatmapEventData> list = new List<BeatmapEventData>(beatmapSaveData.events.Count);
		for (int i = 0; i < 4; i++)
		{
			array[i] = new List<BeatmapObjectData>(1000);
		}
		NoteData noteData = null;
		NoteData noteData2 = null;
		NoteData noteData3 = null;
		float num = -1f;
		int num2 = 0;
		int num3 = 0;
		foreach (BeatmapSaveData.NoteData note in beatmapSaveData.notes)
		{
			float realTimeFromBpmTime = GetRealTimeFromBpmTime(note.time, beatsPerMinute, shuffle, shufflePeriod);
			if (num != realTimeFromBpmTime)
			{
				if (num3 == 2 && noteData2 != null && noteData3 != null && noteData2.lineIndex > noteData3.lineIndex)
				{
					SetNotesForJumpFlip(noteData2, noteData3);
				}
				noteData2 = null;
				noteData3 = null;
				num3 = 0;
			}
			int lineIndex = note.lineIndex;
			NoteLineLayer lineLayer = note.lineLayer;
			NoteLineLayer startNoteLineLayer = NoteLineLayer.Base;
			if (noteData != null && noteData.lineIndex == lineIndex && Mathf.Abs(noteData.time - realTimeFromBpmTime) < 0.0001f)
			{
				startNoteLineLayer = ((noteData.startNoteLineLayer == NoteLineLayer.Base) ? NoteLineLayer.Upper : NoteLineLayer.Top);
			}
			NoteType type = note.type;
			NoteCutDirection cutDirection = note.cutDirection;
			NoteData noteData4 = new NoteData(num2++, realTimeFromBpmTime, lineIndex, lineLayer, startNoteLineLayer, type, cutDirection);
			array[lineIndex].Add(noteData4);
			if (noteData4.noteType == NoteType.NoteA)
			{
				noteData2 = noteData4;
			}
			else if (noteData4.noteType == NoteType.NoteB)
			{
				noteData3 = noteData4;
			}
			noteData = noteData4;
			num = realTimeFromBpmTime;
			if (noteData4.noteType == NoteType.NoteA || noteData4.noteType == NoteType.NoteB)
			{
				num3++;
			}
		}
		if (num3 == 2 && noteData2 != null && noteData3 != null && noteData2.lineIndex > noteData3.lineIndex)
		{
			SetNotesForJumpFlip(noteData2, noteData3);
		}
		foreach (BeatmapSaveData.ObstacleData obstacle in beatmapSaveData.obstacles)
		{
			float realTimeFromBpmTime2 = GetRealTimeFromBpmTime(obstacle.time, beatsPerMinute, shuffle, shufflePeriod);
			int lineIndex2 = obstacle.lineIndex;
			ObstacleType type2 = obstacle.type;
			float realTimeFromBpmTime3 = GetRealTimeFromBpmTime(obstacle.duration, beatsPerMinute, shuffle, shufflePeriod);
			int width = obstacle.width;
			ObstacleData item = new ObstacleData(num2++, realTimeFromBpmTime2, lineIndex2, type2, realTimeFromBpmTime3, width);
			array[lineIndex2].Add(item);
		}
		foreach (BeatmapSaveData.EventData @event in beatmapSaveData.events)
		{
			float realTimeFromBpmTime4 = GetRealTimeFromBpmTime(@event.time, beatsPerMinute, shuffle, shufflePeriod);
			BeatmapEventType type3 = @event.type;
			int value = @event.value;
			BeatmapEventData item2 = new BeatmapEventData(realTimeFromBpmTime4, type3, value);
			list.Add(item2);
		}
		if (list.Count == 0)
		{
			list.Add(new BeatmapEventData(0f, BeatmapEventType.Event0, 1));
			list.Add(new BeatmapEventData(0f, BeatmapEventType.Event4, 1));
		}
		BeatmapLineData[] array2 = new BeatmapLineData[4];
		for (int j = 0; j < 4; j++)
		{
			array[j].Sort((BeatmapObjectData x, BeatmapObjectData y) => (x.time != y.time) ? ((x.time > y.time) ? 1 : (-1)) : 0);
			array2[j] = new BeatmapLineData();
			array2[j].beatmapObjectsData = array[j].ToArray();
		}
		return new BeatmapData(array2, list.ToArray());
	}

	public static BeatmapData GetBeatmapDataFromJson(string json, float beatsPerMinute, float shuffle, float shufflePeriod)
	{
		BeatmapSaveData beatmapSaveData = BeatmapSaveData.DeserializeFromJSONString(json);
		if (beatmapSaveData == null)
		{
			return null;
		}
		return GetBeatmapDataFromBeatmapSaveData(beatmapSaveData, beatsPerMinute, shuffle, shufflePeriod);
	}

	private static void SetNotesForJumpFlip(NoteData noteA, NoteData noteB)
	{
		noteA.SetNoteFlipToNote(noteB);
		noteB.SetNoteFlipToNote(noteA);
	}
}
