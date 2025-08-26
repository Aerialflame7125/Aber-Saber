using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class SaveAsPanelController : MonoBehaviour
{
	[SerializeField]
	private InputField _inputField;

	[SerializeField]
	private Button _saveButton;

	[SerializeField]
	private Button _closeButton;

	private bool _isShown;

	private ButtonBinder _buttonBinder;

	public bool isShown => _isShown;

	public IEnumerator ShowCoroutine(string defaultName, Action<string> finishCallback)
	{
		Show(defaultName, finishCallback);
		yield return new WaitUntil(() => !_isShown);
	}

	public void Show(string defaultName, Action<string> finishCallback)
	{
		_isShown = true;
		base.gameObject.SetActive(value: true);
		_inputField.text = defaultName;
		_buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
		{
			{
				_saveButton,
				(Action)delegate
				{
					finishCallback(_inputField.text);
				}
			},
			{
				_closeButton,
				(Action)delegate
				{
					finishCallback(null);
				}
			}
		});
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
		_buttonBinder.ClearBindings();
		_isShown = false;
	}

	private void OnDestroy()
	{
		if (_buttonBinder != null)
		{
			_buttonBinder.ClearBindings();
		}
	}
}
