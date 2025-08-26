using System;
using UnityEngine;

public class PlayAudioOnGameEventController : MonoBehaviour
{
	[Serializable]
	private class EventAudioBinding
	{
		[Header("==================")]
		[SerializeField]
		private GameEvent _gameEvent;

		[SerializeField]
		private float _delay;

		[SerializeField]
		private AudioClip[] _audioClips;

		private AudioClipQueue _audioClipQueue;

		private RandomObjectPicker<AudioClip> _randomObjectPicker;

		public void Init(AudioClipQueue audioClipQueue)
		{
			_audioClipQueue = audioClipQueue;
			_randomObjectPicker = new RandomObjectPicker<AudioClip>(_audioClips, 0.2f);
			_gameEvent.Subscribe(HandleGameEvent);
		}

		public void Deinit()
		{
			_gameEvent.Unsubscribe(HandleGameEvent);
		}

		private void HandleGameEvent()
		{
			AudioClip audioClip = _randomObjectPicker.PickRandomObject();
			if ((bool)audioClip)
			{
				_audioClipQueue.PlayAudioClipWithDelay(audioClip, _delay);
			}
		}
	}

	[SerializeField]
	private AudioClipQueue _audioClipQueue;

	[SerializeField]
	private EventAudioBinding[] _eventAudioBindings;

	private void Awake()
	{
		EventAudioBinding[] eventAudioBindings = _eventAudioBindings;
		foreach (EventAudioBinding eventAudioBinding in eventAudioBindings)
		{
			eventAudioBinding.Init(_audioClipQueue);
		}
	}

	private void OnDestroy()
	{
		EventAudioBinding[] eventAudioBindings = _eventAudioBindings;
		foreach (EventAudioBinding eventAudioBinding in eventAudioBindings)
		{
			eventAudioBinding.Deinit();
		}
	}
}
