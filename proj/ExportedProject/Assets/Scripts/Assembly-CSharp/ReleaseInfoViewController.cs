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
		_newText.text = "erm we have custom sabers, development is back on maybe\n\n Added: \n * Added SongLoader Implementation\n * Added SaberFactory Implementation\n * Added EasyOffset Implementation\n * Added BL Leaderboard Implementation(Only checks scores)";
		_firstText.gameObject.SetActive(false);
		_newText.gameObject.SetActive(true);
	}
}
