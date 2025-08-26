using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class EventsTableCell : BeatmapEditorTableCell
{
	[SerializeField]
	private Image[] _backgrounds;

	[SerializeField]
	private Image[] _images;

	[SerializeField]
	private TextMeshProUGUI[] _texts;

	private void Awake()
	{
	}

	public void SetLineActive(int lineIdx, bool active, Color color, Sprite image, string text)
	{
		_backgrounds[lineIdx].gameObject.SetActive(active || color.a > 0f);
		_backgrounds[lineIdx].color = color;
		_images[lineIdx].gameObject.SetActive(active && image != null);
		_images[lineIdx].sprite = image;
		_texts[lineIdx].gameObject.SetActive(active && image == null);
		_texts[lineIdx].text = text;
	}
}
