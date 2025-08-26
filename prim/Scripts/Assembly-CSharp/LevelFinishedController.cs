using UnityEngine;

public class LevelFinishedController : MonoBehaviour
{
	[SerializeField]
	private PrepareLevelEndMenuSceneSetupData _prepareLevelFinishedMenuSceneSetupData;

	[SerializeField]
	private MainGameSceneSetupData _mainGameSceneSetupData;

	public void StartLevelFinished()
	{
		LevelCompletionResults levelCompletionResults = _prepareLevelFinishedMenuSceneSetupData.FillLevelCompletionResults(LevelCompletionResults.LevelEndStateType.Cleared);
		_mainGameSceneSetupData.Finish(levelCompletionResults);
	}
}
