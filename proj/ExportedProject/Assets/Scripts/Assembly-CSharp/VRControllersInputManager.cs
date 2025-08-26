using UnityEngine;
using UnityEngine.XR;

public class VRControllersInputManager : MonoBehaviour
{
	private static string kTriggerLeftHand = "TriggerLeftHand";

	private static string kTriggerRightHand = "TriggerRightHand";

	private static string kVerticalLeftHand = "VerticalLeftHand";

	private static string kVerticalRightHand = "VerticalRightHand";

	private static string kHorizontalLeftHand = "HorizontalLeftHand";

	private static string kHorizontalRightHand = "HorizontalRightHand";

	private static string kMenuButtonLeftHandOculusTouch = "MenuButtonLeftHandOculusTouch";

	private static string kMenuButtonLeftHand = "MenuButtonLeftHand";

	private static string kMenuButtonRightHandOculusTouch = "MenuButtonRightHandOculusTouch";

	private static string kMenuButtonRightHand = "MenuButtonRightHand";

	private static string kMenuButtonOculusTouch = "MenuButtonOculusTouch";

	private static bool _lastOculusMenuButtonState = false;
	private static int _lastMenuButtonDownFrame = -1;
	private static bool _cachedMenuButtonDown = false;

	public static float TriggerValue(XRNode node)
	{
		switch (node)
		{
		case XRNode.LeftHand:
			return Input.GetAxis(kTriggerLeftHand);
		case XRNode.RightHand:
			return Input.GetAxis(kTriggerRightHand);
		default:
			return 0f;
		}
	}

	public static float VerticalAxisValue(XRNode node)
	{
		switch (node)
		{
		case XRNode.LeftHand:
			return Input.GetAxis(kVerticalLeftHand);
		case XRNode.RightHand:
			return Input.GetAxis(kVerticalRightHand);
		default:
			return 0f;
		}
	}

	public static float HorizontalAxisValue(XRNode node)
	{
		switch (node)
		{
		case XRNode.LeftHand:
			return Input.GetAxis(kHorizontalLeftHand);
		case XRNode.RightHand:
			return Input.GetAxis(kHorizontalRightHand);
		default:
			return 0f;
		}
	}

	public static bool MenuButtonDown()
	{
		int currentFrame = Time.frameCount;
		if (_lastMenuButtonDownFrame == currentFrame)
			return _cachedMenuButtonDown;

		_lastMenuButtonDownFrame = currentFrame;

		if (VRPlatformHelper.UsingOculusTouch())
		{
			if (PersistentSingleton<VRPlatformHelper>.instance.vrPlatformSDK == VRPlatformHelper.VRPlatformSDK.Oculus)
			{
				var device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
				bool pressed = false;
				device.TryGetFeatureValue(CommonUsages.menuButton, out pressed);

				bool buttonDown = pressed && !_lastOculusMenuButtonState;
				_cachedMenuButtonDown = buttonDown;
				_lastOculusMenuButtonState = pressed;

				return buttonDown;
			}
			_cachedMenuButtonDown = Input.GetButtonDown(kMenuButtonLeftHandOculusTouch) || Input.GetButtonDown(kMenuButtonRightHandOculusTouch);
			return _cachedMenuButtonDown;
		}
		_cachedMenuButtonDown = Input.GetButtonDown(kMenuButtonLeftHand) || Input.GetButtonDown(kMenuButtonRightHand);
		return _cachedMenuButtonDown;
	}

	public static bool MenuButton()
	{
		if (VRPlatformHelper.UsingOculusTouch())
		{
			if (PersistentSingleton<VRPlatformHelper>.instance.vrPlatformSDK == VRPlatformHelper.VRPlatformSDK.Oculus)
			{
				return MenuButtonDown();
			}
			return Input.GetButton(kMenuButtonLeftHandOculusTouch) || Input.GetButton(kMenuButtonRightHandOculusTouch);
		}
		return Input.GetButton(kMenuButtonLeftHand) || Input.GetButton(kMenuButtonRightHand);
	}
}
