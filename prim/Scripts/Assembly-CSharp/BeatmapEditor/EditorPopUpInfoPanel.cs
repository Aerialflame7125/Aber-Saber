using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class EditorPopUpInfoPanel : MonoBehaviour
{
	[SerializeField]
	private Text _text;

	[SerializeField]
	private Image _bgImage;

	[SerializeField]
	private RectTransform _transform;

	public Color color
	{
		get
		{
			return _bgImage.color;
		}
		set
		{
			_bgImage.color = value;
		}
	}

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

	public Vector3 anchoredPosition
	{
		get
		{
			return _transform.anchoredPosition;
		}
		set
		{
			_transform.anchoredPosition = value;
		}
	}
}
