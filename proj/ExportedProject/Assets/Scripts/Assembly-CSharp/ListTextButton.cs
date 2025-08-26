using TMPro;
using UnityEngine;

public class ListTextButton : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI _leftText;

	[SerializeField]
	private TextMeshProUGUI _rightText;

	[SerializeField]
	private MarkableUIButton _button;

	public bool marked
	{
		get
		{
			return _button.marked;
		}
		set
		{
			_button.marked = value;
		}
	}

	public void SetLeftText(string text)
	{
		_leftText.text = text;
	}

	public void SetRightText(string text)
	{
		_leftText.text = text;
	}
}
