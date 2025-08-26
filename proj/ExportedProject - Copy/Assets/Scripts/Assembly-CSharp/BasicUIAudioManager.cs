using UnityEngine;

public class BasicUIAudioManager : MonoBehaviour
{
	[SerializeField]
	private GameEvent[] _buttonClickEvents;

	[SerializeField]
	private AudioSource _audioSource;

	[SerializeField]
	private AudioClip[] _clickSounds;

	[SerializeField]
	private float _minPitch = 1f;

	[SerializeField]
	private float _maxPitch = 1f;

	private RandomObjectPicker<AudioClip> _randomSoundPicker;

	private void Start()
	{
		for (int i = 0; i < _buttonClickEvents.Length; i++)
		{
			_buttonClickEvents[i].Subscribe(HandleButtonClickEvent);
		}
		_randomSoundPicker = new RandomObjectPicker<AudioClip>(_clickSounds, 0.07f);
	}

	private void OnDestroy()
	{
		for (int i = 0; i < _buttonClickEvents.Length; i++)
		{
			_buttonClickEvents[i].Unsubscribe(HandleButtonClickEvent);
		}
	}

	private void HandleButtonClickEvent()
	{
		AudioClip audioClip = _randomSoundPicker.PickRandomObject();
		if ((bool)audioClip)
		{
			_audioSource.pitch = Random.Range(_minPitch, _maxPitch);
			_audioSource.PlayOneShot(audioClip, 1f);
		}
	}
}
