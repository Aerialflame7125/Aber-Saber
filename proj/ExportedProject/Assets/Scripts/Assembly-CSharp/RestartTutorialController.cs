using UnityEngine;

public class RestartTutorialController : MonoBehaviour
{
	private TutorialSceneSetupData _tutorialSceneSetupData;

	public void RestartLevel()
	{
		_tutorialSceneSetupData.TransitionToScene(0.35f);
	}

	public void Init(TutorialSceneSetupData tutorialSceneSetupData)
	{
		_tutorialSceneSetupData = tutorialSceneSetupData;
	}
}
