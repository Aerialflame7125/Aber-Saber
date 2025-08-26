using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyTableCell : TableCell
{
	[SerializeField]
	private TextMeshProUGUI _difficultyText;

	[SerializeField]
	private UnityEngine.UI.Image _bgImage;

	[SerializeField]
	private UnityEngine.UI.Image _highlightImage;

	[SerializeField]
	private FillIndicator _fillIndicator;

	public string difficultyText
	{
		get
		{
			return _difficultyText.text;
		}
		set
		{
			_difficultyText.text = value;
		}
	}

	public int difficultyValue
	{
		set
		{
			_fillIndicator.fillAmount = Mathf.Clamp((float)value / 10f, 0f, 1f);
		}
	}

	protected override void SelectionDidChange(TransitionType transitionType)
	{
		if (base.selected)
		{
			_highlightImage.enabled = false;
			_bgImage.enabled = true;
			_difficultyText.color = Color.black;
		}
		else
		{
			_bgImage.enabled = false;
			_difficultyText.color = Color.white;
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
