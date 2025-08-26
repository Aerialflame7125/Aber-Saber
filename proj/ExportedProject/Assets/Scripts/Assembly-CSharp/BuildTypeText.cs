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
		_text.text = "We 'Un-sequeled' the sequel!";
	}
}
