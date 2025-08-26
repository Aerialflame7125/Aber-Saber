using System;
using UnityEngine;
using UnityEngine.Serialization;

public class StandardLevelSO : ScriptableObject, IStandardLevel
{
	[Serializable]
	public class DifficultyBeatmap : IStandardLevelDifficultyBeatmap
	{
		[SerializeField]
		public LevelDifficulty _difficulty;

		[SerializeField]
		public int _difficultyRank;

		[SerializeField]
		public float _noteJumpMovementSpeed;

		[SerializeField]
		public BeatmapDataSO _beatmapData;

		public IStandardLevel _parentLevel;

		public LevelDifficulty difficulty
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

		public float noteJumpMovementSpeed
		{
			get
			{
				return _noteJumpMovementSpeed;
			}
		}

		public BeatmapData beatmapData
		{
			get
			{
				return _beatmapData.beatmapData;
			}
		}

		public IStandardLevel level
		{
			get
			{
				return _parentLevel;
			}
		}

		public DifficultyBeatmap(IStandardLevel parentLevel, LevelDifficulty difficulty, int difficultyRank, BeatmapDataSO beatmapData)
			: this(parentLevel, difficulty, difficultyRank, 0f, beatmapData)
		{
		}

		public DifficultyBeatmap(IStandardLevel parentLevel, LevelDifficulty difficulty, int difficultyRank, float noteJumpMovementSpeed, BeatmapDataSO beatmapData)
		{
			_parentLevel = parentLevel;
			_difficulty = difficulty;
			_difficultyRank = difficultyRank;
			_noteJumpMovementSpeed = noteJumpMovementSpeed;
			_beatmapData = beatmapData;
			_beatmapData.SetRequiredDataForLoad(_parentLevel.beatsPerMinute, parentLevel.shuffle, parentLevel.shufflePeriod);
		}

		public void SetParentLevel(IStandardLevel parentLevel)
		{
			_parentLevel = parentLevel;
			_beatmapData.SetRequiredDataForLoad(_parentLevel.beatsPerMinute, parentLevel.shuffle, parentLevel.shufflePeriod);
		}
	}

	[SerializeField]
	public string _levelID;

	[SerializeField]
	public string _songName;

	[SerializeField]
	public string _songSubName;

	[FormerlySerializedAs("_authorName")]
	[SerializeField]
	public string _songAuthorName;

	[SerializeField]
	public AudioClip _audioClip;

	[SerializeField]
	public float _beatsPerMinute;

	[SerializeField]
	public float _songTimeOffset;

	[SerializeField]
	public float _shuffle;

	[SerializeField]
	public float _shufflePeriod;

	[SerializeField]
	public float _previewStartTime;

	[SerializeField]
	public float _previewDuration;

	[SerializeField]
	public Sprite _coverImage;

	[SerializeField]
	public SceneInfo _environmentSceneInfo;

	[SerializeField]
	public DifficultyBeatmap[] _difficultyBeatmaps;

	public DifficultyBeatmap[] _sortedDifficultyBeatmaps;

	public string levelID
	{
		get
		{
			return _levelID;
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

	public AudioClip audioClip
	{
		get
		{
			return _audioClip;
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

	public Sprite coverImage
	{
		get
		{
			return _coverImage;
		}
	}

	public IStandardLevelDifficultyBeatmap[] difficultyBeatmaps
	{
		get
		{
			return _sortedDifficultyBeatmaps;
		}
	}

	public SceneInfo environmentSceneInfo
	{
		get
		{
			return _environmentSceneInfo;
		}
	}

	public void InitSimple(string levelID, string songName, SceneInfo environmentSceneInfo, BeatmapDataSO beatmapData, AudioClip audioClip, LevelDifficulty difficulty)
	{
		_audioClip = audioClip;
		_levelID = levelID;
		_songName = songName;
		_songSubName = string.Empty;
		_songAuthorName = string.Empty;
		_beatsPerMinute = 100f;
		_songTimeOffset = 0f;
		_shuffle = 0f;
		_shufflePeriod = 0f;
		_previewStartTime = 0f;
		_previewDuration = 10f;
		_coverImage = null;
		_environmentSceneInfo = environmentSceneInfo;
		_difficultyBeatmaps = new DifficultyBeatmap[1];
		_difficultyBeatmaps[0] = new DifficultyBeatmap(this, difficulty, 0, difficulty.NoteJumpMovementSpeed(), beatmapData);
		InitData();
	}

	public void InitFull(string levelID, string songName, string songSubName, string songAuthorName, AudioClip audioClip, float audioDuration, float beatsPerMinute, float songTimeOffset, float shuffle, float shufflePeriod, float previewStartTime, float previewDuration, Sprite coverImage, DifficultyBeatmap[] difficultyLevels, SceneInfo environmentSceneInfo)
	{
		_levelID = levelID;
		_songName = songName;
		_songSubName = songSubName;
		_songAuthorName = songAuthorName;
		_audioClip = audioClip;
		_beatsPerMinute = beatsPerMinute;
		_songTimeOffset = songTimeOffset;
		_shuffle = shuffle;
		_shufflePeriod = shufflePeriod;
		_previewStartTime = previewStartTime;
		_previewDuration = previewDuration;
		_coverImage = coverImage;
		_environmentSceneInfo = environmentSceneInfo;
		_difficultyBeatmaps = difficultyLevels;
		InitData();
	}

	public void InitData()
	{
		if (_difficultyBeatmaps == null)
		{
			return;
		}
		_sortedDifficultyBeatmaps = new DifficultyBeatmap[_difficultyBeatmaps.Length];
		Array.Copy(_difficultyBeatmaps, _sortedDifficultyBeatmaps, _difficultyBeatmaps.Length);
		int num = _sortedDifficultyBeatmaps.Length;
		while (num > 1)
		{
			int num2 = 0;
			for (int i = 1; i < num; i++)
			{
				if (_sortedDifficultyBeatmaps[i - 1].difficultyRank > _sortedDifficultyBeatmaps[i].difficultyRank)
				{
					DifficultyBeatmap difficultyBeatmap = _sortedDifficultyBeatmaps[i - 1];
					_sortedDifficultyBeatmaps[i - 1] = _sortedDifficultyBeatmaps[i];
					_sortedDifficultyBeatmaps[i] = difficultyBeatmap;
					num2 = i;
				}
			}
			num = num2;
		}
		for (int j = 0; j < _difficultyBeatmaps.Length; j++)
		{
			_difficultyBeatmaps[j].SetParentLevel(this);
		}
	}

	public void OnEnable()
	{
		InitData();
	}

	public IStandardLevelDifficultyBeatmap GetDifficultyLevel(LevelDifficulty difficulty)
	{
		DifficultyBeatmap[] array = _difficultyBeatmaps;
		foreach (DifficultyBeatmap difficultyBeatmap in array)
		{
			if (difficultyBeatmap.difficulty == difficulty)
			{
				return difficultyBeatmap;
			}
		}
		return null;
	}
}
