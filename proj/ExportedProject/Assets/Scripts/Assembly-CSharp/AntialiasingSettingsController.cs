using UnityEngine;

public class AntialiasingSettingsController : ListSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private int[] _antiAliasingLevels;

	protected override void GetInitValues(out int idx, out int numberOfElements)
	{
		_antiAliasingLevels = new int[4] { 1, 2, 4, 8 };
		int antiAliasingLevel = _mainSettingsModel.antiAliasingLevel;
		idx = 2;
		numberOfElements = _antiAliasingLevels.Length;
		for (int i = 0; i < _antiAliasingLevels.Length; i++)
		{
			if (antiAliasingLevel == _antiAliasingLevels[i])
			{
				idx = i;
				break;
			}
		}
	}

	protected override void ApplyValue(int idx)
	{
		_mainSettingsModel.antiAliasingLevel = _antiAliasingLevels[idx];
	}

	protected override string TextForValue(int idx)
	{
		int num = _antiAliasingLevels[idx];
		return (num > 1) ? (num + "x") : "OFF";
	}
}
