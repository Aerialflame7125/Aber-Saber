using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventsPanelButton : MonoBehaviour
{
	[SerializeField]
	private Toggle _toggle;

	[SerializeField]
	private Image _image;

	[SerializeField]
	private Image _bgImage;

	[SerializeField]
	private TextMeshProUGUI _text;

	private int _eventNum;

	public int eventNum
	{
		get
		{
			return _eventNum;
		}
	}

	public void Init(int eventNum, Sprite sprite, Color color, Action<bool> onValueChangedCallback, ToggleGroup toggleGroup, bool isOn)
	{
		_eventNum = eventNum;
		_text.enabled = false;
		_image.sprite = sprite;
		_bgImage.color = color;
		_toggle.onValueChanged.AddListener(delegate(bool value)
		{
			onValueChangedCallback(value);
		});
		_toggle.group = toggleGroup;
		_toggle.isOn = isOn;
	}

	public void Init(int eventNum, string text, Color color, Action<bool> onValueChangedCallback, ToggleGroup toggleGroup, bool isOn)
	{
		_eventNum = eventNum;
		_image.enabled = false;
		_text.text = text;
		_bgImage.color = color;
		_toggle.onValueChanged.AddListener(delegate(bool value)
		{
			onValueChangedCallback(value);
		});
		_toggle.group = toggleGroup;
		_toggle.isOn = isOn;
	}

	public void Select()
	{
		_toggle.isOn = true;
	}
}
