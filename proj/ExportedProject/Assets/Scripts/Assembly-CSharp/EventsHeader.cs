using UnityEngine;
using UnityEngine.UI;

public class EventsHeader : MonoBehaviour
{
	[SerializeField]
	private EventSetDrawStyleSO _eventSetDrawStyle;

	[SerializeField]
	private Image[] _images;

	private void Awake()
	{
		for (int i = 0; i < _images.Length; i++)
		{
			if (_eventSetDrawStyle.overrideImages[i] != null)
			{
				_images[i].sprite = _eventSetDrawStyle.overrideImages[i];
			}
			else if (_eventSetDrawStyle.events[i] != null)
			{
				_images[i].sprite = _eventSetDrawStyle.events[i].image;
			}
			else
			{
				_images[i].enabled = false;
			}
		}
	}
}
