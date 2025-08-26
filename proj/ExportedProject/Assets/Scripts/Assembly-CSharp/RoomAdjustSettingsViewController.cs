using UnityEngine;
using VRUI;

public class RoomAdjustSettingsViewController : VRUIViewController
{
	[SerializeField]
	private VRCenterAdjust _vrCenterAdjust;

	public void AdjustXOffset(bool up)
	{
		_vrCenterAdjust.Move(new Vector3((!up) ? 0.1f : (-0.1f), 0f, 0f), false);
	}

	public void AdjustYOffset(bool up)
	{
		_vrCenterAdjust.Move(new Vector3(0f, (!up) ? 0.1f : (-0.1f), 0f), false);
	}

	public void AdjustZOffset(bool up)
	{
		_vrCenterAdjust.Move(new Vector3(0f, 0f, (!up) ? 0.1f : (-0.1f)), false);
	}

	public void AdjustRotation(bool up)
	{
		_vrCenterAdjust.Rotate((!up) ? 5 : (-5), false);
	}

	public void ResetButtonWasPressed()
	{
		_vrCenterAdjust.ResetRoomTransform(false);
	}
}
