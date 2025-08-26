using UnityEngine;

public class EnableAlphaFeaturesSettingsController : SwitchSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	protected override bool GetInitValue()
	{
		return _mainSettingsModel.enableAlphaFeatures;
	}

	protected override void ApplyValue(bool value)
	{
		_mainSettingsModel.enableAlphaFeatures = value;
	}

	protected override string TextForValue(bool value)
	{
		return (!value) ? "OFF" : "ON";
	}
}
