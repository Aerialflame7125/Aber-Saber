using TMPro;
using UnityEngine;
using VRUI;

public class ReleaseInfoViewController : VRUIViewController
{
	[SerializeField]
	private TextMeshProUGUI _firstText;

	[SerializeField]
	private TextMeshProUGUI _newText;

	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	private void Start()
	{
		_newText.text = "This minor update of Beat Saber adds Angel Voices by Virtual Self as a new track into the game and it is now available by default from the start. No need to \"unlock\" it by finding an Easter egg anymore.\n\nAdditionally we also fixed incorrect BPM for $100 Bills level in One Saber mode which made it look like it ended in the middle of the level.\n";
		if (!_mainSettingsModel.playingForTheFirstTime)
		{
			_firstText.gameObject.SetActive(value: false);
			_newText.gameObject.SetActive(value: true);
		}
		else
		{
			_firstText.gameObject.SetActive(value: true);
			_newText.gameObject.SetActive(value: false);
		}
	}
}
