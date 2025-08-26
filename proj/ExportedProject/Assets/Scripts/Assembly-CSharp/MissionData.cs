using UnityEngine;

[CreateAssetMenu(fileName = "MissionData", menuName = "BS/Data/MissionData")]
public class MissionData : ScriptableObject
{
	[SerializeField]
	private string _missionId;

	[SerializeField]
	private string _formula;

	[SerializeField]
	private string _description;

	[SerializeField]
	private StandardLevelSO _levelData;

	[SerializeField]
	private LevelDifficulty _difficulty;

	[SerializeField]
	private GameplayOptions _gameplayOptions;

	public string missionId
	{
		get
		{
			return _missionId;
		}
	}

	public string formula
	{
		get
		{
			return _formula;
		}
	}

	public string description
	{
		get
		{
			return _description;
		}
	}

	public StandardLevelSO levelData
	{
		get
		{
			return _levelData;
		}
	}

	public LevelDifficulty difficulty
	{
		get
		{
			return _difficulty;
		}
	}

	public GameplayOptions gameplayOptions
	{
		get
		{
			return _gameplayOptions;
		}
	}

	private void OnValidate()
	{
		if (_levelData != null)
		{
			IStandardLevelDifficultyBeatmap difficultyLevel = _levelData.GetDifficultyLevel(_difficulty);
		}
	}
}
