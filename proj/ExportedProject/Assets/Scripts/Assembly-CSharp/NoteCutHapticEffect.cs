using UnityEngine;
using UnityEngine.XR;

public class NoteCutHapticEffect : MonoBehaviour
{
	[SerializeField]
	private HapticFeedbackController _hapticFeedbackController;

	public void RumbleController(Saber.SaberType saberType)
	{
		XRNode node = ((saberType != 0) ? XRNode.RightHand : XRNode.LeftHand);
		_hapticFeedbackController.Rumble(node, 0.1f, 1f, 0.01f);
	}
}
