using System.Collections.Generic;
using UnityEngine;

namespace BeatmapEditor;

public static class EditorBeatmapDataConversions
{
	public static BeatmapSaveData ConvertToBeatsSaveData(this EditorBeatsData beatsData, float beatsPerMinute, bool clipToTime, float maxTime)
	{
		List<BeatmapSaveData.EventData> list = new List<BeatmapSaveData.EventData>(100);
		List<BeatmapSaveData.NoteData> list2 = new List<BeatmapSaveData.NoteData>(1000);
		List<BeatmapSaveData.ObstacleData> list3 = new List<BeatmapSaveData.ObstacleData>(100);
		float[] array = new float[4];
		int[] array2 = new int[4];
		ObstacleType[] array3 = new ObstacleType[4];
		for (int i = 0; i < 4; i++)
		{
			array[i] = 0f;
			array2[i] = 0;
			array3[i] = ObstacleType.FullHeight;
		}
		for (int j = 0; j < beatsData.length; j++)
		{
			BeatData beatData = beatsData[j];
			float timeForBeatIndex = GetTimeForBeatIndex(j, beatsData.beatsPerBar);
			if (clipToTime && timeForBeatIndex / beatsPerMinute * 60f > maxTime)
			{
				break;
			}
			for (int k = 0; k < 4; k++)
			{
				EditorNoteData editorNoteData = beatData.baseNotesData[k];
				if (editorNoteData != null)
				{
					list2.Add(new BeatmapSaveData.NoteData(timeForBeatIndex, k, NoteLineLayer.Base, editorNoteData.type, editorNoteData.cutCirection));
				}
				editorNoteData = beatData.upperNotesData[k];
				if (editorNoteData != null)
				{
					list2.Add(new BeatmapSaveData.NoteData(timeForBeatIndex, k, NoteLineLayer.Upper, editorNoteData.type, editorNoteData.cutCirection));
				}
				editorNoteData = beatData.topNotesData[k];
				if (editorNoteData != null)
				{
					list2.Add(new BeatmapSaveData.NoteData(timeForBeatIndex, k, NoteLineLayer.Top, editorNoteData.type, editorNoteData.cutCirection));
				}
				EditorObstacleData editorObstacleData = beatData.obstaclesData[k];
				if (array2[k] > 0 && (editorObstacleData == null || editorObstacleData.type != array3[k]))
				{
					int l;
					for (l = 1; l < 4 - k; l++)
					{
						EditorObstacleData editorObstacleData2 = beatData.obstaclesData[k + l];
						if (array2[k + l] <= 0 || (editorObstacleData2 != null && editorObstacleData2.type == array3[k + l]) || array2[k + l] != array2[k] || array3[k + l] != array3[k])
						{
							break;
						}
						array2[k + l] = 0;
					}
					list3.Add(new BeatmapSaveData.ObstacleData(array[k], k, array3[k], GetTimeForBeatIndex(array2[k], beatsData.beatsPerBar), l));
				}
				if (editorObstacleData != null)
				{
					if (array2[k] == 0 || editorObstacleData.type != array3[k])
					{
						array2[k] = 1;
						array3[k] = editorObstacleData.type;
						array[k] = timeForBeatIndex;
					}
					else
					{
						array2[k]++;
					}
				}
				else
				{
					array2[k] = 0;
				}
			}
			for (int m = 0; m < 16; m++)
			{
				EditorEventData editorEventData = beatData.eventsData[m];
				if (editorEventData != null && !editorEventData.isPreviousValidValue)
				{
					list.Add(new BeatmapSaveData.EventData(timeForBeatIndex, (BeatmapEventType)m, editorEventData.value));
				}
			}
		}
		if (list.Count == 0 && list2.Count == 0 && list3.Count == 0)
		{
			return null;
		}
		return new BeatmapSaveData(list, list2, list3);
	}

	public static EditorBeatsData ConvertToEditorBeatsData(this BeatmapSaveData beatmapSaveData)
	{
		if (beatmapSaveData == null)
		{
			return null;
		}
		int num = 4;
		float num2 = 0f;
		foreach (BeatmapSaveData.NoteData note in beatmapSaveData.notes)
		{
			if (num2 < note.time)
			{
				num2 = note.time;
			}
			int beatsPerBarForTime = GetBeatsPerBarForTime(note.time);
			if (beatsPerBarForTime > num)
			{
				num = beatsPerBarForTime;
			}
		}
		foreach (BeatmapSaveData.ObstacleData obstacle in beatmapSaveData.obstacles)
		{
			if (num2 < obstacle.time + obstacle.duration)
			{
				num2 = obstacle.time + obstacle.duration;
			}
			int beatsPerBarForTime2 = GetBeatsPerBarForTime(obstacle.time);
			if (beatsPerBarForTime2 > num)
			{
				num = beatsPerBarForTime2;
			}
			beatsPerBarForTime2 = GetBeatsPerBarForTime(obstacle.duration);
			if (beatsPerBarForTime2 > num)
			{
				num = beatsPerBarForTime2;
			}
		}
		foreach (BeatmapSaveData.EventData @event in beatmapSaveData.events)
		{
			if (num2 < @event.time)
			{
				num2 = @event.time;
			}
			int beatsPerBarForTime3 = GetBeatsPerBarForTime(@event.time);
			if (beatsPerBarForTime3 > num)
			{
				num = beatsPerBarForTime3;
			}
		}
		int lengthInBeats = GetBeatIndexForTime(num2, num) + 1;
		EditorBeatsData editorBeatsData = new EditorBeatsData(lengthInBeats, num);
		foreach (BeatmapSaveData.NoteData note2 in beatmapSaveData.notes)
		{
			int beatIndexForTime = GetBeatIndexForTime(note2.time, editorBeatsData.beatsPerBar);
			BeatData beatData = editorBeatsData[beatIndexForTime];
			EditorNoteData editorNoteData = new EditorNoteData(note2.type, note2.cutDirection);
			if (note2.lineLayer == NoteLineLayer.Base)
			{
				beatData.baseNotesData[note2.lineIndex] = editorNoteData;
			}
			else if (note2.lineLayer == NoteLineLayer.Upper)
			{
				beatData.upperNotesData[note2.lineIndex] = editorNoteData;
			}
			else if (note2.lineLayer == NoteLineLayer.Top)
			{
				beatData.topNotesData[note2.lineIndex] = editorNoteData;
			}
		}
		foreach (BeatmapSaveData.ObstacleData obstacle2 in beatmapSaveData.obstacles)
		{
			int beatIndexForTime2 = GetBeatIndexForTime(obstacle2.time, editorBeatsData.beatsPerBar);
			int beatIndexForTime3 = GetBeatIndexForTime(obstacle2.duration, editorBeatsData.beatsPerBar);
			for (int i = 0; i < beatIndexForTime3; i++)
			{
				BeatData beatData2 = editorBeatsData[beatIndexForTime2 + i];
				for (int j = 0; j < obstacle2.width; j++)
				{
					beatData2.obstaclesData[obstacle2.lineIndex + j] = new EditorObstacleData(obstacle2.type);
				}
			}
		}
		foreach (BeatmapSaveData.EventData event2 in beatmapSaveData.events)
		{
			int beatIndexForTime4 = GetBeatIndexForTime(event2.time, editorBeatsData.beatsPerBar);
			BeatData beatData3 = editorBeatsData[beatIndexForTime4];
			int type = (int)event2.type;
			beatData3.eventsData[type] = new EditorEventData(event2.value, isPreviousValidValue: false);
		}
		for (int k = 0; k < 16; k++)
		{
			editorBeatsData.FillPreviousValidEvent(0, k, untilNextValid: false);
		}
		return editorBeatsData;
	}

	private static int GetBeatsPerBarForTime(float time)
	{
		float num = time - Mathf.Floor(time);
		float num2 = 0.5f;
		float num3 = 0f;
		do
		{
			num2 *= 0.5f;
			num3 = num / num2;
		}
		while (Mathf.Abs(num3 - Mathf.Floor(num3)) > 1E-05f);
		return Mathf.RoundToInt(1f / num2) * 4;
	}

	private static float GetTimeForBeatIndex(int beatIndex, float beatsPerBar)
	{
		return (float)beatIndex * 4f / beatsPerBar;
	}

	private static int GetBeatIndexForTime(float time, float beatsPerBar)
	{
		return (int)(time * beatsPerBar / 4f + 0.5f);
	}
}
