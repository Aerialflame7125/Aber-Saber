using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class AlertPanelController : MonoBehaviour
{
	private const int kMaxButtons = 3;

	[SerializeField]
	private TextButton _button0;

	[SerializeField]
	private TextButton _button1;

	[SerializeField]
	private TextButton _button2;

	[Space]
	[SerializeField]
	private Text _titleText;

	[SerializeField]
	private Text _subtitleText;

	private bool _isShown;

	private TextButton[] _textButtons;

	private ButtonBinder _buttonBinder;

	public bool isShown => _isShown;

	private void OnDestroy()
	{
		if (_buttonBinder != null)
		{
			_buttonBinder.ClearBindings();
		}
	}

	public IEnumerator ShowCoroutine(string title, string subtitle, string button0Text, Action button0Action, string button1Text = null, Action button1Action = null, string button2Text = null, Action button2Action = null)
	{
		Show(title, subtitle, button0Text, button0Action, button1Text, button1Action, button2Text, button2Action);
		yield return new WaitUntil(() => !_isShown);
	}

	public void Show(string title, string subtitle, string button0Text, Action button0Action, string button1Text = null, Action button1Action = null, string button2Text = null, Action button2Action = null)
	{
		List<string> list = new List<string>();
		List<Action> list2 = new List<Action>();
		if (button0Text != null)
		{
			list.Add(button0Text);
		}
		if (button1Text != null)
		{
			list.Add(button1Text);
		}
		if (button2Text != null)
		{
			list.Add(button2Text);
		}
		if (button0Action != null)
		{
			list2.Add(button0Action);
		}
		if (button1Action != null)
		{
			list2.Add(button1Action);
		}
		if (button2Action != null)
		{
			list2.Add(button2Action);
		}
		Show(title, subtitle, list.ToArray(), list2.ToArray());
	}

	public void Show(string title, string subtitle, string[] buttonTexts, Action[] buttonActions)
	{
		_isShown = true;
		if (_textButtons == null)
		{
			_textButtons = new TextButton[3] { _button0, _button1, _button2 };
		}
		if (_buttonBinder == null)
		{
			_buttonBinder = new ButtonBinder();
		}
		_titleText.text = title.ToUpper();
		_subtitleText.text = subtitle;
		for (int i = 0; i < buttonTexts.Length; i++)
		{
			_textButtons[i].text.text = buttonTexts[i].ToUpper();
			_textButtons[i].gameObject.SetActive(value: true);
		}
		for (int j = buttonTexts.Length; j < 3; j++)
		{
			_textButtons[j].gameObject.SetActive(value: false);
		}
		for (int k = 0; k < buttonActions.Length; k++)
		{
			_buttonBinder.AddBinding(_textButtons[k].button, buttonActions[k]);
		}
		base.gameObject.SetActive(value: true);
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
		_buttonBinder.ClearBindings();
		_isShown = false;
	}
}
