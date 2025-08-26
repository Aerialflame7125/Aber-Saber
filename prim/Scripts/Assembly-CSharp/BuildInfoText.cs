using TMPro;
using UnityEngine;

public class BuildInfoText : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _text;

	private void Start()
	{
		_text.text = "v" + Application.version;
	}
}
