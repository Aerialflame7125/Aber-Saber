using UnityEngine;

public class ReturnFromTutorialToMenuController : MonoBehaviour
{
	[SerializeField]
	private TutorialSceneSetupData _tutorialSceneSetupData;

	public void ReturnToMenu()
	{
		_tutorialSceneSetupData.Finish(completed: false);
	}
}
