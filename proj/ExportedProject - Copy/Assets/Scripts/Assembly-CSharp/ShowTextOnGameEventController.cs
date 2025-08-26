using System;
using UnityEngine;

public class ShowTextOnGameEventController : MonoBehaviour
{
	[Serializable]
	private class EventTextBinding
	{
		[SerializeField]
		private GameEvent _gameEvent;

		[TextArea(2, 2)]
		[SerializeField]
		private string _text;

		private TextFadeTransitions _textFadeTransitions;

		public void Init(TextFadeTransitions textFadeTransitions)
		{
			_textFadeTransitions = textFadeTransitions;
			_gameEvent.Subscribe(HandleGameEvent);
		}

		public void Deinit()
		{
			_gameEvent.Unsubscribe(HandleGameEvent);
		}

		private void HandleGameEvent()
		{
			_textFadeTransitions.ShowText(_text);
		}
	}

	[SerializeField]
	private TextFadeTransitions _textFadeTransitions;

	[SerializeField]
	private EventTextBinding[] _eventTextBindings;

	private void Awake()
	{
		EventTextBinding[] eventTextBindings = _eventTextBindings;
		foreach (EventTextBinding eventTextBinding in eventTextBindings)
		{
			eventTextBinding.Init(_textFadeTransitions);
		}
	}

	private void OnDestroy()
	{
		EventTextBinding[] eventTextBindings = _eventTextBindings;
		foreach (EventTextBinding eventTextBinding in eventTextBindings)
		{
			eventTextBinding.Deinit();
		}
	}
}
