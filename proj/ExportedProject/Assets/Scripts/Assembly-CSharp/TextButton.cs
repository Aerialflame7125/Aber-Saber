using UnityEngine;
using UnityEngine.UI;

public class TextButton : MonoBehaviour
{
	[SerializeField]
	private Text _text;

	[SerializeField]
	private Button _button;

	public Text text
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
