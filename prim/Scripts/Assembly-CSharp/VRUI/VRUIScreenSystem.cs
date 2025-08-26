using UnityEngine;

namespace VRUI;

public class VRUIScreenSystem : MonoBehaviour
{
	[SerializeField]
	private VRUIScreen _mainScreen;

	[SerializeField]
	private VRUIScreen _leftScreen;

	[SerializeField]
	private VRUIScreen _rightScreen;

	public VRUIScreen mainScreen => _mainScreen;

	public VRUIScreen leftScreen => _leftScreen;

	public VRUIScreen rightScreen => _rightScreen;

	private void Awake()
	{
		_mainScreen.screenSystem = this;
		_leftScreen.screenSystem = this;
		_rightScreen.screenSystem = this;
	}
}
