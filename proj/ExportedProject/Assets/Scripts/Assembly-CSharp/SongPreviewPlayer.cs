using UnityEngine;

public class SongPreviewPlayer : MonoBehaviour
{
	[Tooltip("Minimum 2, maximum 6")]
	[SerializeField]
	private int _channelsCount = 3;

	[SerializeField]
	private AudioSource _audioSourcePrefab;

	[SerializeField]
	private AudioClip _defaultAudioClip;

	[SerializeField]
	private float _volume = 1f;

	[SerializeField]
	private float _ambientVolumeScale = 1f;

	[SerializeField]
	private float _defaultCrossfadeSpeed = 1f;

	[SerializeField]
	private float _defaultFadeOutSpeed = 2f;

	private AudioSource[] _audioSources;

	private int _activeChannel;

	private float _timeToDefaultAudioTransition;

	private bool _transitionAfterDelay;

	private float _volumeScale;

	private float _fadeSpeed;

	public float volume
	{
		get
		{
			return _volume;
		}
		set
		{
			_volume = value;
		}
	}

	private void Awake()
	{
		_fadeSpeed = _defaultCrossfadeSpeed;
		_audioSources = new AudioSource[_channelsCount];
		for (int i = 0; i < _channelsCount; i++)
		{
			_audioSources[i] = Object.Instantiate(_audioSourcePrefab);
			_audioSources[i].volume = 0f;
			_audioSources[i].loop = false;
			_audioSources[i].reverbZoneMix = 0f;
			_audioSources[i].playOnAwake = false;
		}
	}

	private void Start()
	{
		CrossfadeTo(_defaultAudioClip, Mathf.Max(Random.Range(0f, _defaultAudioClip.length - 0.1f), 0f), -1f, _ambientVolumeScale);
	}

	private void Update()
	{
		for (int i = 0; i < _audioSources.Length; i++)
		{
			AudioSource audioSource = _audioSources[i];
			float num = audioSource.volume;
			if (_activeChannel == i)
			{
				if (num < _volume * _volumeScale)
				{
					audioSource.volume = Mathf.Min(_volume * _volumeScale, num + Time.deltaTime * _fadeSpeed);
				}
				else if (num > _volume * _volumeScale)
				{
					audioSource.volume = _volume * _volumeScale;
				}
			}
			else if (num > 0f)
			{
				num -= Time.deltaTime * _fadeSpeed;
				if (num <= 0f)
				{
					audioSource.volume = 0f;
					audioSource.Stop();
				}
				else
				{
					audioSource.volume = num;
				}
			}
		}
		if (_transitionAfterDelay)
		{
			_timeToDefaultAudioTransition -= Time.deltaTime;
			if (_timeToDefaultAudioTransition <= 0f)
			{
				CrossfadeTo(_defaultAudioClip, 0f, -1f, _ambientVolumeScale);
			}
		}
	}

	public void CrossfadeTo(AudioClip audioClip, float startTime, float duration, float volumeScale = 1f)
	{
		_fadeSpeed = _defaultCrossfadeSpeed;
		float num = _volume;
		int num2 = 0;
		for (int i = 0; i < _audioSources.Length; i++)
		{
			float num3 = _audioSources[i].volume;
			if (num3 <= num)
			{
				num2 = i;
				num = num3;
			}
		}
		_volumeScale = volumeScale;
		_activeChannel = num2;
		AudioSource audioSource = _audioSources[num2];
		audioSource.volume = 0f;
		audioSource.clip = audioClip;
		audioSource.time = startTime;
		_timeToDefaultAudioTransition = duration;
		_transitionAfterDelay = duration > 0f;
		audioSource.loop = !_transitionAfterDelay;
		audioSource.Play();
	}

	public void FadeOut()
	{
		_fadeSpeed = _defaultFadeOutSpeed;
		_transitionAfterDelay = false;
		_activeChannel = -1;
	}

	public void CrossfadeToDefault()
	{
		if (_transitionAfterDelay || _activeChannel <= 0 || !(_audioSources[_activeChannel].clip == _defaultAudioClip))
		{
			CrossfadeTo(_defaultAudioClip, Mathf.Max(Random.Range(0f, _defaultAudioClip.length - 0.1f), 0f), -1f, _ambientVolumeScale);
		}
	}
}
