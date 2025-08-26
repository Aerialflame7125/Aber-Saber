using UnityEngine;

public class ReturnFromLevelToMenuController : MonoBehaviour
{
	[SerializeField]
	private MainGameSceneSetupData _mainGameSceneSetupData;

	public void ReturnToMenu()
	{
		_mainGameSceneSetupData.Finish(null);
	}
}
