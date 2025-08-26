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

	public static float TriggerValue(XRNode node)
	{
		return node switch
		{
			XRNode.LeftHand => Input.GetAxis(kTriggerLeftHand), 
			XRNode.RightHand => Input.GetAxis(kTriggerRightHand), 
			_ => 0f, 
		};
	}

	public static float VerticalAxisValue(XRNode node)
	{
		return node switch
		{
			XRNode.LeftHand => Input.GetAxis(kVerticalLeftHand), 
			XRNode.RightHand => Input.GetAxis(kVerticalRightHand), 
			_ => 0f, 
		};
	}

	public static float HorizontalAxisValue(XRNode node)
	{
		return node switch
		{
			XRNode.LeftHand => Input.GetAxis(kHorizontalLeftHand), 
			XRNode.RightHand => Input.GetAxis(kHorizontalRightHand), 
			_ => 0f, 
		};
	}

	public static bool MenuButtonDown()
	{
		if (VRPlatformHelper.UsingOculusTouch())
		{
			if (PersistentSingleton<VRPlatformHelper>.instance.vrPlatformSDK == VRPlatformHelper.VRPlatformSDK.Oculus)
			{
				return Input.GetButtonDown(kMenuButtonOculusTouch);
			}
			return Input.GetButtonDown(kMenuButtonLeftHandOculusTouch) || Input.GetButtonDown(kMenuButtonRightHandOculusTouch);
		}
		return Input.GetButtonDown(kMenuButtonLeftHand) || Input.GetButtonDown(kMenuButtonRightHand);
	}

	public static bool MenuButton()
	{
		if (VRPlatformHelper.UsingOculusTouch())
		{
			if (PersistentSingleton<VRPlatformHelper>.instance.vrPlatformSDK == VRPlatformHelper.VRPlatformSDK.Oculus)
			{
				return Input.GetButton(kMenuButtonOculusTouch);
			}
			return Input.GetButton(kMenuButtonLeftHandOculusTouch) || Input.GetButton(kMenuButtonRightHandOculusTouch);
		}
		return Input.GetButton(kMenuButtonLeftHand) || Input.GetButton(kMenuButtonRightHand);
	}
}
