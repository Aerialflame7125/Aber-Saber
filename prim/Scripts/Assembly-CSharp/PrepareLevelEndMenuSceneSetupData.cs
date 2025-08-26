using UnityEngine;

public class PrepareLevelEndMenuSceneSetupData : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(GameEnergyCounter))]
	private ObjectProvider _gameEnergyCounterProvider;

	[Space]
	[SerializeField]
	private SaberActivityCounter _saberActivityCounter;

	[SerializeField]
	private BeatmapObjectExecutionRatingsRecorder _beatmapObjectExecutionRatingsRecorder;

	[SerializeField]
	private MultiplierValuesRecorder _multiplierValuesRecorder;

	[SerializeField]
	private ScoreController _scoreController;

	private float _songDuration;

	private int _levelNotesCount;

	private GameEnergyCounter _gameEnergyCounter;

	public void Init(float songDuration, int levelNotesCount)
	{
		_songDuration = songDuration;
		_levelNotesCount = levelNotesCount;
	}

	private void Start()
	{
		_gameEnergyCounter = _gameEnergyCounterProvider.GetProvidedObject<GameEnergyCounter>();
	}

	public LevelCompletionResults FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType levelEndStateType)
	{
		BeatmapObjectExecutionRating[] beatmapObjectExecutionRatings = _beatmapObjectExecutionRatingsRecorder.beatmapObjectExecutionRatings.ToArray();
		MultiplierValuesRecorder.MultiplierValue[] multiplierValues = _multiplierValuesRecorder.multiplierValues.ToArray();
		int prevFrameScore = _scoreController.prevFrameScore;
		int maxCombo = _scoreController.maxCombo;
		float[] saberActivityValues = _saberActivityCounter.saberMovementAveragingValueRecorder.GetHistoryValues().ToArray();
		float leftSaberMovementDistance = _saberActivityCounter.leftSaberMovementDistance;
		float rightSaberMovementDistance = _saberActivityCounter.rightSaberMovementDistance;
		float[] handActivityValues = _saberActivityCounter.handMovementAveragingValueRecorder.GetHistoryValues().ToArray();
		float leftHandMovementDistance = _saberActivityCounter.leftHandMovementDistance;
		float rightHandMovementDistance = _saberActivityCounter.rightHandMovementDistance;
		float songDuration = _songDuration;
		float energy = _gameEnergyCounter.energy;
		int levelNotesCount = _levelNotesCount;
		return new LevelCompletionResults(levelNotesCount, beatmapObjectExecutionRatings, multiplierValues, prevFrameScore, maxCombo, saberActivityValues, leftSaberMovementDistance, rightSaberMovementDistance, handActivityValues, leftHandMovementDistance, rightHandMovementDistance, songDuration, levelEndStateType, energy);
	}
}
