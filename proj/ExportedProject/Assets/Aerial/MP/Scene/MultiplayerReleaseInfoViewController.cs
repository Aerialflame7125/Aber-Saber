using TMPro;
using UnityEngine;
using VRUI;

public class MultiplayerReleaseInfoViewController : VRUIViewController
{
	[SerializeField]
	private TextMeshProUGUI _firstText;

	[SerializeField]
	private TextMeshProUGUI _newText;

	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private void Start()
	{
		_newText.text = "This a multiplayer lobby hosted by none other than the game creator. This is a copy of the original base game menu, but with my spin on it.";
		if (!_mainSettingsModel.playingForTheFirstTime)
		{
			_firstText.gameObject.SetActive(false);
			_newText.gameObject.SetActive(true);
		}
		else
		{
			_firstText.gameObject.SetActive(true);
			_newText.gameObject.SetActive(false);
		}
	}
}
