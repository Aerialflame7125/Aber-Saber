using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BeatmapSaveData
{
	[Serializable]
	public class EventData
	{
		[SerializeField]
		private float _time;

		[SerializeField]
		private BeatmapEventType _type;

		[SerializeField]
		private int _value;

		public float time => _time;

		public BeatmapEventType type => _type;

		public int value => _value;

		public EventData(float time, BeatmapEventType type, int value)
		{
			_time = time;
			_type = type;
			_value = value;
		}
	}

	[Serializable]
	public class NoteData
	{
		[SerializeField]
		private float _time;

		[SerializeField]
		private int _lineIndex;

		[SerializeField]
		private NoteLineLayer _lineLayer;

		[SerializeField]
		private NoteType _type;

		[SerializeField]
		private NoteCutDirection _cutDirection;

		public float time => _time;

		public int lineIndex => _lineIndex;

		public NoteLineLayer lineLayer => _lineLayer;

		public NoteType type => _type;

		public NoteCutDirection cutDirection => _cutDirection;

		public NoteData(float time, int lineIndex, NoteLineLayer lineLayer, NoteType type, NoteCutDirection cutDirection)
		{
			_time = time;
			_lineIndex = lineIndex;
			_lineLayer = lineLayer;
			_type = type;
			_cutDirection = cutDirection;
		}
	}

	[Serializable]
	public class ObstacleData
	{
		[SerializeField]
		private float _time;

		[SerializeField]
		private int _lineIndex;

		[SerializeField]
		private ObstacleType _type;

		[SerializeField]
		private float _duration;

		[SerializeField]
		private int _width;

		public float time => _time;

		public int lineIndex => _lineIndex;

		public ObstacleType type => _type;

		public float duration => _duration;

		public int width => _width;

		public ObstacleData(float time, int lineIndex, ObstacleType type, float duration, int width)
		{
			_time = time;
			_lineIndex = lineIndex;
			_type = type;
			_duration = duration;
			_width = width;
		}
	}

	private const string kCurrentVersion = "2.0.0";

	[SerializeField]
	private string _version;

	[SerializeField]
	private List<EventData> _events;

	[SerializeField]
	private List<NoteData> _notes;

	[SerializeField]
	private List<ObstacleData> _obstacles;

	public string version => _version;

	public List<EventData> events => _events;

	public List<NoteData> notes => _notes;

	public List<ObstacleData> obstacles => _obstacles;

	public BeatmapSaveData(List<EventData> events, List<NoteData> notes, List<ObstacleData> obstacles)
	{
		_version = "2.0.0";
		_events = events;
		_notes = notes;
		_obstacles = obstacles;
	}

	public string SerializeToJSONString()
	{
		return JsonUtility.ToJson(this);
	}

	public static BeatmapSaveData DeserializeFromJSONString(string stringData)
	{
		BeatmapSaveData beatmapSaveData = JsonUtility.FromJson<BeatmapSaveData>(stringData);
		if (beatmapSaveData == null || beatmapSaveData.version != "2.0.0")
		{
		}
		return beatmapSaveData;
	}
}
