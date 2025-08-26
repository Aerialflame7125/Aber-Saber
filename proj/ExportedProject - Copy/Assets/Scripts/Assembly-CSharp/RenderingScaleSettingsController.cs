using UnityEngine;

public class RenderingScaleSettingsController : ListSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private float[] _resolutionScales;

	protected override void GetInitValues(out int idx, out int numberOfElements)
	{
		_resolutionScales = new float[11]
		{
			0.8f, 0.9f, 1f, 1.1f, 1.2f, 1.3f, 1.4f, 1.5f, 1.6f, 1.7f,
			1.8f
		};
		float vrResolutionScale = _mainSettingsModel.vrResolutionScale;
		idx = 2;
		numberOfElements = _resolutionScales.Length;
		for (int i = 0; i < _resolutionScales.Length; i++)
		{
			if (vrResolutionScale == _resolutionScales[i])
			{
				idx = i;
				break;
			}
		}
	}

	protected override void ApplyValue(int idx)
	{
		_mainSettingsModel.vrResolutionScale = _resolutionScales[idx];
	}

	protected override string TextForValue(int idx)
	{
		return string.Format("{0:0.0}", _resolutionScales[idx]);
	}
}
