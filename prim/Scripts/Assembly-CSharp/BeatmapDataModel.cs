using System;
using UnityEngine;

public class BeatmapDataModel : MonoBehaviour
{
	private BeatmapData _beatmapData;

	public BeatmapData beatmapData
	{
		get
		{
			return _beatmapData;
		}
		set
		{
			if (_beatmapData != value)
			{
				_beatmapData = value;
				if (this.beatmapDataDidChangeEvent != null)
				{
					this.beatmapDataDidChangeEvent();
				}
			}
		}
	}

	public event Action beatmapDataDidChangeEvent;

	public bool IsNoteTypeInTimeInterval(NoteType noteType, float startTime, float intervalDuration)
	{
		for (int i = 0; i < beatmapData.beatmapLinesData.Length; i++)
		{
			for (int j = 0; j < beatmapData.beatmapLinesData[i].beatmapObjectsData.Length; j++)
			{
				BeatmapObjectData beatmapObjectData = beatmapData.beatmapLinesData[i].beatmapObjectsData[j];
				if (beatmapObjectData.time > startTime + intervalDuration)
				{
					break;
				}
				if (!(beatmapObjectData.time < startTime) && beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note && ((NoteData)beatmapObjectData).noteType == noteType)
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool IsNoteInTimeInterval(float startTime, float intervalDuration)
	{
		for (int i = 0; i < beatmapData.beatmapLinesData.Length; i++)
		{
			for (int j = 0; j < beatmapData.beatmapLinesData[i].beatmapObjectsData.Length; j++)
			{
				BeatmapObjectData beatmapObjectData = beatmapData.beatmapLinesData[i].beatmapObjectsData[j];
				if (beatmapObjectData.time > startTime + intervalDuration)
				{
					break;
				}
				if (!(beatmapObjectData.time < startTime) && beatmapObjectData.beatmapObjectType == BeatmapObjectType.Note)
				{
					return true;
				}
			}
		}
		return false;
	}
}
