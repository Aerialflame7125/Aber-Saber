using UnityEngine;
using VRUI;

public class TestFlowCoordinator : MonoBehaviour
{
	[SerializeField]
	private VRUIScreenSystem _screenSystem;

	[SerializeField]
	private VRUIViewController _viewController;

	private void Start()
	{
		_screenSystem.mainScreen.SetRootViewController(_viewController);
	}
}
