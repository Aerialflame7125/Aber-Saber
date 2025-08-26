using UnityEngine;

public class SmokeSettingsController : SwitchSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	protected override bool GetInitValue()
	{
		return _mainSettingsModel.smokeGraphicsSettings > 0;
	}

	protected override void ApplyValue(bool value)
	{
		_mainSettingsModel.smokeGraphicsSettings = (value ? 1 : 0);
	}

	protected override string TextForValue(bool value)
	{
		return (!value) ? "OFF" : "ON";
	}
}
