using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class BeatLineTableCell : BeatmapEditorTableCell
{
	public enum Type
	{
		Bar,
		Subdivision
	}

	[SerializeField]
	private Image _lineImage;

	[SerializeField]
	private Sprite _barLineSprite;

	[SerializeField]
	private Sprite _subdivisionLineSprite;

	[SerializeField]
	private Text _text;

	private Type _type;

	private float _alpha;

	public string text
	{
		set
		{
			_text.text = value;
		}
	}

	public Type type
	{
		set
		{
			if (value != _type)
			{
				_type = value;
				switch (value)
				{
				case Type.Bar:
					_lineImage.sprite = _barLineSprite;
					break;
				case Type.Subdivision:
					_lineImage.sprite = _subdivisionLineSprite;
					break;
				}
			}
		}
	}

	public float alpha
	{
		set
		{
			if (_alpha != value)
			{
				_lineImage.color = _lineImage.color.ColorWithAlpha(value);
				_alpha = value;
			}
		}
	}
}
