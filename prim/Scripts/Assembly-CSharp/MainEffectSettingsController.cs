using UnityEngine;

public class MainEffectSettingsController : ListSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	[SerializeField]
	private MainEffectGraphicsSettingsPresets _presets;

	protected override void GetInitValues(out int idx, out int numberOfElements)
	{
		idx = _mainSettingsModel.mainEffectGraphicsSettings;
		idx = Mathf.Clamp(idx, 0, _presets.presets.Length - 1);
		numberOfElements = _presets.presets.Length;
	}

	protected override void ApplyValue(int idx)
	{
		_mainSettingsModel.mainEffectGraphicsSettings = idx;
	}

	protected override string TextForValue(int idx)
	{
		return _presets.presets[idx].presetName;
	}
}
