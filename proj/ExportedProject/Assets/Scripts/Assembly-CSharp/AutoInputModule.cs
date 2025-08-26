using UnityEngine;
using UnityEngine.EventSystems;
using VRUIControls;

public class AutoInputModule : MonoBehaviour
{
	private static bool _useVRUIControls = true;

	[SerializeField]
	private VRInputModule _VRInputModule;

	[SerializeField]
	private StandaloneInputModule _standaloneInputModule;

	public static bool useVRUIControls
	{
		get
		{
			return _useVRUIControls;
		}
	}

	private void Awake()
	{
		if (useVRUIControls)
		{
			_VRInputModule.enabled = true;
			_standaloneInputModule.enabled = false;
		}
		else
		{
			_VRInputModule.enabled = false;
			_standaloneInputModule.enabled = true;
		}
	}
}
