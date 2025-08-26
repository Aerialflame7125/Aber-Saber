using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class EventsPanelController : MonoBehaviour, IBeatmapEditorEventType
{
	[SerializeField]
	private EventSetDrawStyleSO _eventSetDrawStyle;

	[Space]
	[SerializeField]
	private RectTransform _buttonContainerRectTransform;

	[SerializeField]
	private EventsPanelButton _eventsPanelButtonPrefab;

	[SerializeField]
	private Image _eventsPanelImagePrefab;

	[SerializeField]
	private float _buttonWidth = 20f;

	[SerializeField]
	private float _buttonHeight = 20f;

	[SerializeField]
	private float _verticalSeparator = 2f;

	private Dictionary<string, EventDrawStyleSO> _eventDrawStyles = new Dictionary<string, EventDrawStyleSO>(5);

	private Dictionary<string, EventDrawStyleSO.SubEventDrawStyle> _selectedSubEventDrawStyles = new Dictionary<string, EventDrawStyleSO.SubEventDrawStyle>(5);

	private List<EventsPanelButton> _eventButtons;

	private void Awake()
	{
		_eventButtons = new List<EventsPanelButton>();
		int num = 0;
		EventDrawStyleSO[] events = _eventSetDrawStyle.events;
		foreach (EventDrawStyleSO eventDrawStyleSO in events)
		{
			if (eventDrawStyleSO != null && !_eventDrawStyles.ContainsKey(eventDrawStyleSO.eventId))
			{
				_eventDrawStyles.Add(eventDrawStyleSO.eventId, eventDrawStyleSO);
				if (eventDrawStyleSO.subEvents.Length <= 1)
				{
					_selectedSubEventDrawStyles[eventDrawStyleSO.eventId] = eventDrawStyleSO.subEvents[0];
					continue;
				}
				GameObject gameObject = new GameObject("Panel");
				RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
				rectTransform.SetParent(_buttonContainerRectTransform, worldPositionStays: false);
				rectTransform.anchorMax = new Vector3(1f, 1f);
				rectTransform.anchorMin = new Vector3(0f, 1f);
				rectTransform.pivot = new Vector3(0f, 1f);
				rectTransform.anchoredPosition = new Vector2(0f, (float)(-num) * (_buttonHeight + _verticalSeparator));
				rectTransform.sizeDelta = new Vector2(0f, _buttonHeight);
				Image image = UnityEngine.Object.Instantiate(_eventsPanelImagePrefab);
				RectTransform rectTransform2 = image.rectTransform;
				rectTransform2.transform.SetParent(rectTransform, worldPositionStays: false);
				rectTransform2.anchoredPosition = Vector3.zero;
				image.sprite = eventDrawStyleSO.image;
				AddEventButtons(eventDrawStyleSO, rectTransform);
				num++;
			}
		}
	}

	private void AddEventButtons(EventDrawStyleSO eventDrawStyle, Transform parentTransform)
	{
		ToggleGroup toggleGroup = parentTransform.gameObject.AddComponent<ToggleGroup>();
		int num = 0;
		EventDrawStyleSO.SubEventDrawStyle[] subEvents = eventDrawStyle.subEvents;
		foreach (EventDrawStyleSO.SubEventDrawStyle subEventDrawStyle in subEvents)
		{
			EventsPanelButton eventsPanelButton = UnityEngine.Object.Instantiate(_eventsPanelButtonPrefab);
			Action<bool> onValueChangedCallback = delegate
			{
				_selectedSubEventDrawStyles[eventDrawStyle.eventId] = subEventDrawStyle;
			};
			if (subEventDrawStyle.image != null)
			{
				eventsPanelButton.Init(num, subEventDrawStyle.image, subEventDrawStyle.color, onValueChangedCallback, toggleGroup, num == 0);
			}
			else
			{
				eventsPanelButton.Init(num, subEventDrawStyle.eventValue.ToString(), subEventDrawStyle.color, onValueChangedCallback, toggleGroup, num == 0);
			}
			eventsPanelButton.gameObject.transform.SetParent(parentTransform);
			RectTransform component = eventsPanelButton.GetComponent<RectTransform>();
			component.localPosition = Vector3.zero;
			component.localRotation = Quaternion.identity;
			component.localScale = Vector3.one;
			component.anchoredPosition = new Vector2((float)(num + 2) * _buttonWidth, 0f);
			num++;
			_eventButtons.Add(eventsPanelButton);
		}
	}

	public EventDrawStyleSO.SubEventDrawStyle GetSelectedEventType(string eventId)
	{
		return _selectedSubEventDrawStyles[eventId];
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Keypad0) && !EventSystemHelper.IsInputFieldSelected())
		{
			SetButtonsWithNum(0);
		}
		if (Input.GetKeyDown(KeyCode.Keypad1) && !EventSystemHelper.IsInputFieldSelected())
		{
			SetButtonsWithNum(1);
		}
		if (Input.GetKeyDown(KeyCode.Keypad2) && !EventSystemHelper.IsInputFieldSelected())
		{
			SetButtonsWithNum(2);
		}
		if (Input.GetKeyDown(KeyCode.Keypad3) && !EventSystemHelper.IsInputFieldSelected())
		{
			SetButtonsWithNum(3);
		}
		if (Input.GetKeyDown(KeyCode.Keypad4) && !EventSystemHelper.IsInputFieldSelected())
		{
			SetButtonsWithNum(4);
		}
		if (Input.GetKeyDown(KeyCode.Keypad5) && !EventSystemHelper.IsInputFieldSelected())
		{
			SetButtonsWithNum(5);
		}
		if (Input.GetKeyDown(KeyCode.Keypad6) && !EventSystemHelper.IsInputFieldSelected())
		{
			SetButtonsWithNum(6);
		}
		if (Input.GetKeyDown(KeyCode.Keypad7) && !EventSystemHelper.IsInputFieldSelected())
		{
			SetButtonsWithNum(7);
		}
		if (Input.GetKeyDown(KeyCode.Keypad8) && !EventSystemHelper.IsInputFieldSelected())
		{
			SetButtonsWithNum(8);
		}
	}

	private void SetButtonsWithNum(int num)
	{
		foreach (EventsPanelButton eventButton in _eventButtons)
		{
			if (eventButton.eventNum == num)
			{
				eventButton.Select();
			}
		}
	}
}
