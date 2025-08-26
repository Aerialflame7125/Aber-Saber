using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextMeshProButton : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _text;

	[SerializeField]
	private Button _button;

	public TextMeshProUGUI text
	{
		get
		{
			return _text;
		}
	}

	public Button button
	{
		get
		{
			return _button;
		}
	}
}
