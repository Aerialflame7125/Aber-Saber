using System;
using UnityEngine;
using UnityEngine.UI;

public class GameplayModeIndicator : MonoBehaviour
{
	[Serializable]
	private class ModeIconsData
	{
		public GameplayMode gameplayMode = GameplayMode.PartyStandard;

		public Sprite[] icons;
	}

	[SerializeField]
	private Image[] _icons;

	[Space]
	[SerializeField]
	private ModeIconsData[] _modeIcons;

	public void SetupForGameplayMode(GameplayMode gameplayMode)
	{
		for (int i = 0; i < _icons.Length; i++)
		{
			_icons[i].gameObject.SetActive(value: false);
		}
		for (int j = 0; j < _modeIcons.Length; j++)
		{
			ModeIconsData modeIconsData = _modeIcons[j];
			if (modeIconsData.gameplayMode == gameplayMode)
			{
				for (int k = 0; k < modeIconsData.icons.Length; k++)
				{
					Sprite sprite = modeIconsData.icons[k];
					_icons[k].sprite = sprite;
					_icons[k].gameObject.SetActive(value: true);
				}
				break;
			}
		}
	}
}
