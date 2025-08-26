using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMeshProButton : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _text;

	[SerializeField]
	private Button _button;

	public TextMeshProUGUI text => _text;

	public Button button => _button;
}
