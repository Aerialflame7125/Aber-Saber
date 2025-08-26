using System;
using System.Collections;
using TMPro;
using UnityEngine;
using VRUI;

public class NameEntryViewController : VRUIViewController
{
	[SerializeField]
	private UIKeyboard _uiKeyboard;

	[SerializeField]
	private TextMeshProUGUI _nameText;

	[SerializeField]
	private TextMeshProUGUI _cursorText;

	[SerializeField]
	private int _maxNameLength = 5;

	private bool _stopBlinkingCursor;

	public event Action<NameEntryViewController, string> didFinishEvent;

	protected override void DidActivate(bool firstActivation, ActivationType activationType)
	{
		if (firstActivation)
		{
			_nameText.text = string.Empty;
			UIKeyboard uiKeyboard = _uiKeyboard;
			uiKeyboard.uiKeyboardKeyEvent = (Action<char>)Delegate.Combine(uiKeyboard.uiKeyboardKeyEvent, new Action<char>(HandleUIKeyboardKey));
			UIKeyboard uiKeyboard2 = _uiKeyboard;
			uiKeyboard2.uiKeyboardDeleteEvent = (Action)Delegate.Combine(uiKeyboard2.uiKeyboardDeleteEvent, new Action(HandleUIKeyboardDelete));
		}
		_stopBlinkingCursor = false;
		StartCoroutine(BlinkCursor());
	}

	protected override void DidDeactivate(DeactivationType deactivationType)
	{
		_stopBlinkingCursor = true;
	}

	private IEnumerator BlinkCursor()
	{
		Color cursorColor = _cursorText.color;
		while (!_stopBlinkingCursor)
		{
			_cursorText.color = Color.clear;
			yield return new WaitForSeconds(0.4f);
			_cursorText.color = cursorColor;
			yield return new WaitForSeconds(0.4f);
		}
	}

	private void HandleUIKeyboardKey(char key)
	{
		if (_nameText.text.Length < _maxNameLength)
		{
			_nameText.text += key.ToString().ToUpper();
		}
	}

	private void HandleUIKeyboardDelete()
	{
		if (_nameText.text.Length > 0)
		{
			_nameText.text = _nameText.text.Remove(_nameText.text.Length - 1);
		}
	}

	public void OkButtonPressed()
	{
		if (this.didFinishEvent != null)
		{
			string text = _nameText.text;
			if (text.Length == 0)
			{
				text = "PLAYER";
			}
			this.didFinishEvent(this, text);
		}
	}
}
