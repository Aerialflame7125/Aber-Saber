using System;
using UnityEngine;
using UnityEngine.Serialization;

public class StandardLevelSaveData
{
	[Serializable]
	public class DifficultyBeatmap
	{
		[SerializeField]
		private string _difficulty;

		[SerializeField]
		private int _difficultyRank;

		[SerializeField]
		private string _beatmapFilename;

		[SerializeField]
		private int _noteJumpMovementSpeed;

		public string difficulty
		{
			get
			{
				return _difficulty;
			}
		}

		public int difficultyRank
		{
			get
			{
				return _difficultyRank;
			}
		}

		public string beatmapFilename
		{
			get
			{
				return _beatmapFilename;
			}
		}

		public int noteJumpMovementSpeed
		{
			get
			{
				return _noteJumpMovementSpeed;
			}
		}

		public DifficultyBeatmap(string difficulty, int difficultyRank, string beatmapFilename, int noteJumpMovementSpeed)
		{
			_difficulty = difficulty;
			_difficultyRank = difficultyRank;
			_beatmapFilename = beatmapFilename;
			_noteJumpMovementSpeed = noteJumpMovementSpeed;
		}
	}

	private const string kCurrentVersion = "1.0.0";

	[SerializeField]
	private string _version;

	[SerializeField]
	private string _songName;

	[SerializeField]
	private string _songSubName;

	[FormerlySerializedAs("_authorName")]
	[SerializeField]
	private string _songAuthorName;

	[SerializeField]
	private string _levelAuthorName;

	[SerializeField]
	private float _beatsPerMinute;

	[SerializeField]
	private float _songTimeOffset;

	[SerializeField]
	private float _shuffle;

	[SerializeField]
	private float _shufflePeriod;

	[SerializeField]
	private float _previewStartTime;

	[SerializeField]
	private float _previewDuration;

	[FormerlySerializedAs("_songFileName")]
	[SerializeField]
	private string _songFilename;

	[FormerlySerializedAs("_coverImageFileName")]
	[SerializeField]
	private string _coverImageFilename;

	[SerializeField]
	private string _environmentName;

	[SerializeField]
	private DifficultyBeatmap[] _difficultyBeatmaps;

	public string version
	{
		get
		{
			return _version;
		}
	}

	public string songName
	{
		get
		{
			return _songName;
		}
	}

	public string songSubName
	{
		get
		{
			return _songSubName;
		}
	}

	public string songAuthorName
	{
		get
		{
			return _songAuthorName;
		}
	}

	public string levelAuthorName
	{
		get
		{
			return _levelAuthorName;
		}
	}

	public float beatsPerMinute
	{
		get
		{
			return _beatsPerMinute;
		}
	}

	public float songTimeOffset
	{
		get
		{
			return _songTimeOffset;
		}
	}

	public float shuffle
	{
		get
		{
			return _shuffle;
		}
	}

	public float shufflePeriod
	{
		get
		{
			return _shufflePeriod;
		}
	}

	public float previewStartTime
	{
		get
		{
			return _previewStartTime;
		}
	}

	public float previewDuration
	{
		get
		{
			return _previewDuration;
		}
	}

	public string songFilename
	{
		get
		{
			return _songFilename;
		}
	}

	public string coverImageFilename
	{
		get
		{
			return _coverImageFilename;
		}
	}

	public string environmentName
	{
		get
		{
			return _environmentName;
		}
	}

	public DifficultyBeatmap[] difficultyBeatmaps
	{
		get
		{
			return _difficultyBeatmaps;
		}
	}

	public bool hasAllData
	{
		get
		{
			if (_version == null)
			{
				return false;
			}
			if (_songName == null)
			{
				return false;
			}
			if (_songSubName == null)
			{
				return false;
			}
			if (_songAuthorName == null)
			{
				return false;
			}
			if (_levelAuthorName == null)
			{
				return false;
			}
			if (_beatsPerMinute == 0f)
			{
				return false;
			}
			if (_songFilename == null)
			{
				return false;
			}
			if (_coverImageFilename == null)
			{
				return false;
			}
			if (_environmentName == null)
			{
				return false;
			}
			if (_difficultyBeatmaps == null)
			{
				return false;
			}
			return true;
		}
	}

	public StandardLevelSaveData(string songName, string songSubName, string songAuthorName, string levelAuthorName, float beatsPerMinute, float songTimeOffset, float shuffle, float shufflePeriod, float previewStartTime, float previewDuration, string songFilename, string coverImageFilename, string environmentName, DifficultyBeatmap[] difficultyBeatmaps)
	{
		_version = "1.0.0";
		_songName = songName;
		_songSubName = songSubName;
		_songAuthorName = songAuthorName;
		_levelAuthorName = levelAuthorName;
		_beatsPerMinute = beatsPerMinute;
		_songTimeOffset = songTimeOffset;
		_shuffle = shuffle;
		_shufflePeriod = shufflePeriod;
		_previewStartTime = previewStartTime;
		_previewDuration = previewDuration;
		_songFilename = songFilename;
		_coverImageFilename = coverImageFilename;
		_environmentName = environmentName;
		_difficultyBeatmaps = difficultyBeatmaps;
	}

	public void SetSongFilename(string songFilename)
	{
		_songFilename = songFilename;
	}

	public string SerializeToJSONString()
	{
		return JsonUtility.ToJson(this);
	}

	public static StandardLevelSaveData DeserializeFromJSONString(string stringData)
	{
		StandardLevelSaveData standardLevelSaveData = JsonUtility.FromJson<StandardLevelSaveData>(stringData);
		if (standardLevelSaveData == null || standardLevelSaveData.version != "1.0.0")
		{
		}
		return standardLevelSaveData;
	}
}
