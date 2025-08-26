using TMPro;
using UnityEngine;

public class BuildTypeText : MonoBehaviour
{
	[SerializeField]
	private GameBuildMode _gameBuildMode;

	[SerializeField]
	private TextMeshPro _text;

	private void Start()
	{
		if (_gameBuildMode.mode == GameBuildMode.Mode.Arcade)
		{
			_text.text = "ARCADE";
		}
		else if (_gameBuildMode.mode == GameBuildMode.Mode.Demo)
		{
			_text.text = "DEMO";
		}
		else
		{
			_text.gameObject.SetActive(value: false);
		}
	}
}
