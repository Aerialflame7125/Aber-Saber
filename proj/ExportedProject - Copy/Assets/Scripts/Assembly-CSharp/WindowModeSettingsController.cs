using UnityEngine;

public class WindowModeSettingsController : SwitchSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	protected override bool GetInitValue()
	{
		return _mainSettingsModel.windowMode == MainSettingsModel.WindowMode.Fullscreen;
	}

	protected override void ApplyValue(bool value)
	{
		_mainSettingsModel.windowMode = (value ? MainSettingsModel.WindowMode.Fullscreen : MainSettingsModel.WindowMode.Windowed);
	}

	protected override string TextForValue(bool value)
	{
		return (!value) ? "OFF" : "ON";
	}
}
