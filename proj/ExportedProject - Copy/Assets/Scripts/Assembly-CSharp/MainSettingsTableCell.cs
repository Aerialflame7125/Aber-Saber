using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSettingsTableCell : TableCell
{
	[SerializeField]
	private TextMeshProUGUI _settingsSubMenuText;

	[SerializeField]
	private UnityEngine.UI.Image _bgImage;

	[SerializeField]
	private UnityEngine.UI.Image _highlightImage;

	public string settingsSubMenuText
	{
		get
		{
			return _settingsSubMenuText.text;
		}
		set
		{
			_settingsSubMenuText.text = value;
		}
	}

	protected override void SelectionDidChange(TransitionType transitionType)
	{
		if (base.selected)
		{
			_highlightImage.enabled = false;
			_bgImage.enabled = true;
			_settingsSubMenuText.color = Color.black;
		}
		else
		{
			_bgImage.enabled = false;
			_settingsSubMenuText.color = Color.white;
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
