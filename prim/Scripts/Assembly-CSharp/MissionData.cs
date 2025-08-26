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

	public string missionId => _missionId;

	public string formula => _formula;

	public string description => _description;

	public StandardLevelSO levelData => _levelData;

	public LevelDifficulty difficulty => _difficulty;

	public GameplayOptions gameplayOptions => _gameplayOptions;

	private void OnValidate()
	{
		if (_levelData != null)
		{
			IStandardLevelDifficultyBeatmap difficultyLevel = _levelData.GetDifficultyLevel(_difficulty);
		}
	}
}
