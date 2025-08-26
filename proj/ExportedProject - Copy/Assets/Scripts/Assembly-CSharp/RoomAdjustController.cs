using UnityEngine;

public class RoomAdjustController : SimpleSettingsController
{
	[SerializeField]
	private VRCenterAdjust _vrCenterAdjust;

	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	public override void Init()
	{
	}

	public override void ApplySettings()
	{
		_mainSettingsModel.roomCenter = _vrCenterAdjust.transform.position;
		_mainSettingsModel.roomRotation = _vrCenterAdjust.transform.rotation.eulerAngles.y;
	}

	public override void CancelSettings()
	{
		_vrCenterAdjust.SetTransformFromSettings();
	}
}
