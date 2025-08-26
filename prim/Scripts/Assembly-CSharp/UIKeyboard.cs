using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIKeyboard : MonoBehaviour
{
	public Action<char> uiKeyboardKeyEvent;

	public Action uiKeyboardDeleteEvent;

	[SerializeField]
	private TextMeshProButton _keyButtonPrefab;

	private void Start()
	{
		string[] array = new string[28]
		{
			"q", "w", "e", "r", "t", "y", "u", "i", "o", "p",
			"a", "s", "d", "f", "g", "h", "j", "k", "l", "z",
			"x", "c", "v", "b", "n", "m", "<-", "space"
		};
		for (int i = 0; i < array.Length; i++)
		{
			TextMeshProButton textMeshProButton = UnityEngine.Object.Instantiate(_keyButtonPrefab);
			textMeshProButton.text.text = array[i];
			if (i < array.Length - 2)
			{
				string key = array[i];
				textMeshProButton.button.onClick.AddListener(delegate
				{
					KeyButtonWasPressed(key);
				});
			}
			else if (i == array.Length - 2)
			{
				textMeshProButton.button.onClick.AddListener(delegate
				{
					DeleteButtonWasPressed();
				});
			}
			else
			{
				textMeshProButton.button.onClick.AddListener(delegate
				{
					SpaceButtonWasPressed();
				});
			}
			RectTransform component = textMeshProButton.GetComponent<RectTransform>();
			RectTransform component2 = base.transform.GetChild(i).gameObject.GetComponent<RectTransform>();
			component.SetParent(component2, worldPositionStays: false);
			component.localPosition = Vector2.zero;
			component.localScale = Vector3.one;
			component.anchoredPosition = Vector2.zero;
			component.anchorMin = Vector2.zero;
			component.anchorMax = Vector3.one;
			component.offsetMin = Vector2.zero;
			component.offsetMax = Vector2.zero;
		}
	}

	public void KeyButtonWasPressed(string key)
	{
		if (uiKeyboardKeyEvent != null)
		{
			uiKeyboardKeyEvent(key[0]);
		}
		EventSystem.current.SetSelectedGameObject(null);
	}

	public void DeleteButtonWasPressed()
	{
		EventSystem.current.SetSelectedGameObject(null);
		if (uiKeyboardDeleteEvent != null)
		{
			uiKeyboardDeleteEvent();
		}
	}

	public void SpaceButtonWasPressed()
	{
		EventSystem.current.SetSelectedGameObject(null);
		if (uiKeyboardKeyEvent != null)
		{
			uiKeyboardKeyEvent(' ');
		}
	}
}
