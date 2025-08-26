using UnityEngine;

public class TutorialSceneSetup : MonoBehaviour
{
	[SerializeField]
	private TutorialSceneSetupData _tutorialSceneSetupData;

	[SerializeField]
	private TutorialController _tutorialController;

	[SerializeField]
	private RestartTutorialController _restartTutorialController;

	[SerializeField]
	private PauseMenuManager _pauseMenuManager;

	private void Start()
	{
		_restartTutorialController.Init(_tutorialSceneSetupData);
	}
}
