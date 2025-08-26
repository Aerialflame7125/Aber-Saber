using UnityEngine;

public class SwapColorsSettingsController : SwitchSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	[SerializeField]
	private ColorManager _colorManager;

	protected override bool GetInitValue()
	{
		return _mainSettingsModel.swapColors;
	}

	protected override void ApplyValue(bool value)
	{
		_mainSettingsModel.swapColors = value;
	}

	protected override string TextForValue(bool value)
	{
		return (!value) ? "OFF" : "ON";
	}
}
