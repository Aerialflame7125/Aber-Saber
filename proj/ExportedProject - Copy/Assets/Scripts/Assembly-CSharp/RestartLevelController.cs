using UnityEngine;

public class RestartLevelController : MonoBehaviour
{
	[SerializeField]
	private MainGameSceneSetupData _mainGameSceneSetupData;

	public void RestartLevel()
	{
		_mainGameSceneSetupData.TransitionToScene(0.35f);
	}
}
