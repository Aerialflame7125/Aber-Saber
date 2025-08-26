using System;
using UnityEngine;

namespace BeatmapEditor;

public class ActiveDifficultySO : ScriptableObject
{
	private LevelDifficulty _difficulty;

	public LevelDifficulty difficulty
	{
		get
		{
			return _difficulty;
		}
		set
		{
			if (_difficulty != value)
			{
				LevelDifficulty arg = _difficulty;
				_difficulty = value;
				this.didChangeEvent(arg, value);
			}
		}
	}

	public event Action<LevelDifficulty, LevelDifficulty> didChangeEvent = delegate
	{
	};

	private void OnEnable()
	{
		base.hideFlags = HideFlags.DontUnloadUnusedAsset;
	}
}
