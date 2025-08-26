using UnityEngine;

public class VolumeSettingsController : ListSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private float[] _volumes;

	protected override void GetInitValues(out int idx, out int numberOfElements)
	{
		float num = 0.1f;
		float num2 = 0.2f;
		numberOfElements = 9;
		_volumes = new float[numberOfElements];
		for (int i = 0; i < _volumes.Length; i++)
		{
			_volumes[i] = num2 + num * (float)i;
		}
		float volume = _mainSettingsModel.volume;
		idx = numberOfElements - 1;
		for (int j = 0; j < _volumes.Length; j++)
		{
			if (volume == _volumes[j])
			{
				idx = j;
				break;
			}
		}
	}

	protected override void ApplyValue(int idx)
	{
		_mainSettingsModel.volume = _volumes[idx];
	}

	protected override string TextForValue(int idx)
	{
		return $"{_volumes[idx]:0.0}";
	}
}
