using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleListTableCell : TableCell
{
	[SerializeField]
	private TextMeshProUGUI _text;

	[SerializeField]
	private UnityEngine.UI.Image _bgImage;

	[SerializeField]
	private UnityEngine.UI.Image _highlightImage;

	public string text
	{
		get
		{
			return _text.text;
		}
		set
		{
			_text.text = value;
		}
	}

	protected override void SelectionDidChange(TransitionType transitionType)
	{
		if (base.selected)
		{
			_highlightImage.enabled = false;
			_bgImage.enabled = true;
			_text.color = Color.black;
		}
		else
		{
			_bgImage.enabled = false;
			_text.color = Color.white;
		}
	}

	protected override void HighlightDidChange(TransitionType transitionType)
	{
		if (base.selected)
		{
			_highlightImage.enabled = false;
		}
		else
		{
			_highlightImage.enabled = base.highlighted;
		}
	}
}
