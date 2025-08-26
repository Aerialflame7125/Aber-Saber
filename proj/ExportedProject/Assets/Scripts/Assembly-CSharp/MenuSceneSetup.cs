using UnityEngine;

public class MenuSceneSetup : MonoBehaviour
{
	[SerializeField]
	private GameBuildMode _gameBuildMode;

	[Space]
	[SerializeField]
	private MainFlowCoordinator _mainFlowCoordinator;

	[SerializeField]
	private ArcadeLevelSelectionFlowCoordinator _arcadeLevelSelectionFlowCoordinator;

	[SerializeField]
	private DemoFlowCoordinator _demoFlowCoordinator;

	private void Start()
	{
		if (_gameBuildMode.mode == GameBuildMode.Mode.Full)
		{
			_mainFlowCoordinator.Present();
		}
		else if (_gameBuildMode.mode == GameBuildMode.Mode.Arcade)
		{
			_arcadeLevelSelectionFlowCoordinator.Present();
		}
		else if (_gameBuildMode.mode == GameBuildMode.Mode.Demo)
		{
			_demoFlowCoordinator.Present();
		}
	}
}
