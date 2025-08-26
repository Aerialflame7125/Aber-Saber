using UnityEngine;

public class MenuButtonPressDurationSettingsController : ListSettingsController
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private string[] _textValues;

	private int[] _pressDurationLevelValues;

	protected override void GetInitValues(out int idx, out int numberOfElements)
	{
		_pressDurationLevelValues = new int[2] { 0, 1 };
		_textValues = new string[2] { "Instant", "Long" };
		int pauseButtonPressDurationLevel = _mainSettingsModel.pauseButtonPressDurationLevel;
		numberOfElements = _pressDurationLevelValues.Length;
		idx = numberOfElements - 1;
		for (int i = 0; i < _pressDurationLevelValues.Length; i++)
		{
			if (pauseButtonPressDurationLevel == _pressDurationLevelValues[i])
			{
				idx = i;
				break;
			}
		}
	}

	protected override void ApplyValue(int idx)
	{
		_mainSettingsModel.pauseButtonPressDurationLevel = _pressDurationLevelValues[idx];
	}

	protected override string TextForValue(int idx)
	{
		return _textValues[idx];
	}
}
