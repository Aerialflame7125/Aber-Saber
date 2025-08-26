using UnityEngine;

public class WindowResolutionSettingsController : ListSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private Vector2Int[] _windowResolutions;

	protected override void GetInitValues(out int idx, out int numberOfElements)
	{
		_windowResolutions = new Vector2Int[Screen.resolutions.Length + 1];
		idx = -1;
		numberOfElements = 0;
		for (int i = 0; i < Screen.resolutions.Length; i++)
		{
			int width = Screen.resolutions[i].width;
			int height = Screen.resolutions[i].height;
			if (numberOfElements == 0 || _windowResolutions[numberOfElements - 1].x != width || _windowResolutions[numberOfElements - 1].y != height)
			{
				_windowResolutions[numberOfElements] = new Vector2Int(width, height);
				Vector2Int windowResolution = _mainSettingsModel.windowResolution;
				if (width == windowResolution.x && height == windowResolution.y)
				{
					idx = numberOfElements;
				}
				numberOfElements++;
			}
		}
		if (idx == -1)
		{
			idx = numberOfElements;
			_windowResolutions[idx] = _mainSettingsModel.windowResolution;
			numberOfElements++;
		}
	}

	protected override void ApplyValue(int idx)
	{
		_mainSettingsModel.windowResolution = _windowResolutions[idx];
	}

	protected override string TextForValue(int idx)
	{
		return _windowResolutions[idx].x + " x " + _windowResolutions[idx].y;
	}
}
