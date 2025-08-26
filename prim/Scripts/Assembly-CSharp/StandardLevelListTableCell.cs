using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StandardLevelListTableCell : TableCell
{
	[SerializeField]
	private TextMeshProUGUI _songNameText;

	[SerializeField]
	private TextMeshProUGUI _authorText;

	[SerializeField]
	private UnityEngine.UI.Image _bgImage;

	[SerializeField]
	private UnityEngine.UI.Image _highlightImage;

	[SerializeField]
	private UnityEngine.UI.Image _coverImage;

	public string songName
	{
		get
		{
			return _songNameText.text;
		}
		set
		{
			_songNameText.text = value.Replace("\n", " ");
		}
	}

	public string author
	{
		get
		{
			return _authorText.text;
		}
		set
		{
			_authorText.text = value;
		}
	}

	public Sprite coverImage
	{
		get
		{
			return _coverImage.sprite;
		}
		set
		{
			_coverImage.sprite = value;
		}
	}

	protected override void SelectionDidChange(TransitionType transitionType)
	{
		if (base.selected)
		{
			_highlightImage.enabled = false;
			_bgImage.enabled = true;
			_songNameText.color = Color.black;
			_authorText.color = Color.black;
		}
		else
		{
			_bgImage.enabled = false;
			_songNameText.color = Color.white;
			_authorText.color = new Color(1f, 1f, 1f, 0.2f);
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
