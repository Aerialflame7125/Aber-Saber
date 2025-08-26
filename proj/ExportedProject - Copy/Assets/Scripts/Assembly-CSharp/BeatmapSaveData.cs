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

		public float time
		{
			get
			{
				return _time;
			}
		}

		public BeatmapEventType type
		{
			get
			{
				return _type;
			}
		}

		public int value
		{
			get
			{
				return _value;
			}
		}

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

		public float time
		{
			get
			{
				return _time;
			}
		}

		public int lineIndex
		{
			get
			{
				return _lineIndex;
			}
		}

		public NoteLineLayer lineLayer
		{
			get
			{
				return _lineLayer;
			}
		}

		public NoteType type
		{
			get
			{
				return _type;
			}
		}

		public NoteCutDirection cutDirection
		{
			get
			{
				return _cutDirection;
			}
		}

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

		public float time
		{
			get
			{
				return _time;
			}
		}

		public int lineIndex
		{
			get
			{
				return _lineIndex;
			}
		}

		public ObstacleType type
		{
			get
			{
				return _type;
			}
		}

		public float duration
		{
			get
			{
				return _duration;
			}
		}

		public int width
		{
			get
			{
				return _width;
			}
		}

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

	public string version
	{
		get
		{
			return _version;
		}
	}

	public List<EventData> events
	{
		get
		{
			return _events;
		}
	}

	public List<NoteData> notes
	{
		get
		{
			return _notes;
		}
	}

	public List<ObstacleData> obstacles
	{
		get
		{
			return _obstacles;
		}
	}

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
