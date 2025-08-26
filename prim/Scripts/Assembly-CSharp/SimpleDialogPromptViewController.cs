using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using VRUI;

public class SimpleDialogPromptViewController : VRUIViewController
{
	[SerializeField]
	private TextMeshProUGUI _titleText;

	[SerializeField]
	private TextMeshProUGUI _messageText;

	[SerializeField]
	private TextMeshProButton _buttonPrefab;

	[SerializeField]
	private RectTransform _okButtonContainerRectTransform;

	[SerializeField]
	private RectTransform _cancelButtonContainerRectTransform;

	private TextMeshProButton _okButton;

	private TextMeshProButton _cancelButton;

	public event Action<SimpleDialogPromptViewController, bool> didFinishEvent;

	private TextMeshProButton CreateButton(string text, RectTransform containerRectTransfrom, UnityAction callback)
	{
		TextMeshProButton textMeshProButton = UnityEngine.Object.Instantiate(_buttonPrefab);
		textMeshProButton.text.text = text;
		textMeshProButton.button.onClick.AddListener(callback);
		RectTransform component = textMeshProButton.GetComponent<RectTransform>();
		component.SetParent(containerRectTransfrom, worldPositionStays: false);
		component.localPosition = Vector2.zero;
		component.localScale = Vector3.one;
		component.anchoredPosition = Vector2.zero;
		component.anchorMin = Vector2.zero;
		component.anchorMax = Vector3.one;
		component.offsetMin = Vector2.zero;
		component.offsetMax = Vector2.zero;
		return textMeshProButton;
	}

	public void Init(string title, string message, string okButtonText, string cancelButtonText)
	{
		_titleText.text = title;
		_messageText.text = message;
		if (_okButton != null)
		{
			_okButton.text.text = okButtonText;
		}
		else
		{
			_okButton = CreateButton(okButtonText, _okButtonContainerRectTransform, delegate
			{
				if (this.didFinishEvent != null)
				{
					this.didFinishEvent(this, arg2: true);
				}
			});
		}
		if (_cancelButton != null)
		{
			_cancelButton.text.text = cancelButtonText;
			return;
		}
		_cancelButton = CreateButton(cancelButtonText, _cancelButtonContainerRectTransform, delegate
		{
			if (this.didFinishEvent != null)
			{
				this.didFinishEvent(this, arg2: false);
			}
		});
	}
}
