using System;
using UnityEngine;

public class AudioTimeSyncController : MonoBehaviour
{
	[SerializeField]
	private AudioSource _audioSource;

	[Space]
	[SerializeField]
	private FloatVariableSetter _songTime;

	[SerializeField]
	private DoubleVariableSetter _dspTimeOffset;

	[Space]
	[SerializeField]
	private float _audioSyncLerpSpeed = 1f;

	[SerializeField]
	private float _forcedSyncDeltaTime = 0.03f;

	[SerializeField]
	private float _startSyncDeltaTime = 0.02f;

	[SerializeField]
	private float _stopSyncDeltaTime = 0.01f;

	[NonSerialized]
	public bool forcedAudioSync;

	private float _songLength = 1f;

	private bool _fixingAudioSyncError;

	private float _audioStartTimeOffsetSinceLevelLoad;

	private int _playbackLoopIndex;

	private int _prevAudioSamplePos;

	private bool _pause;

	private float _startSongTime;

	private float _songTimeOffset;

	private bool _audioStarted;

	public float songTime
	{
		get
		{
			return _songTime.value;
		}
	}

	public float songLength
	{
		get
		{
			return _songLength;
		}
	}

	public void Init(AudioClip audioClip, float startSongTime, float songTimeOffset)
	{
		_audioSource.clip = audioClip;
		_songLength = _audioSource.clip.length;
		_startSongTime = startSongTime;
		_songTimeOffset = songTimeOffset;
		_songTime.SetValue(startSongTime);
	}

	private void Awake()
	{
		base.enabled = false;
		_songTime.SetValue(0f);
		_audioSource.Stop();
		_audioSource.clip = null;
	}

	private void Update()
	{
		if (_audioSource.clip == null)
		{
			return;
		}
		if (Time.captureFramerate != 0)
		{
			_songTime.SetValue(_songTime.value + Time.deltaTime);
			return;
		}
		if (Time.timeSinceLevelLoad < _audioStartTimeOffsetSinceLevelLoad)
		{
			_songTime.SetValue(_songTime.value + Time.deltaTime);
			return;
		}
		if (!_audioStarted)
		{
			_audioStarted = true;
			_audioSource.Play();
		}
		int timeSamples = _audioSource.timeSamples;
		float time = _audioSource.time;
		float num = Time.timeSinceLevelLoad - _audioStartTimeOffsetSinceLevelLoad;
		if (_prevAudioSamplePos > timeSamples)
		{
			_playbackLoopIndex++;
		}
		_prevAudioSamplePos = timeSamples;
		time += (float)_playbackLoopIndex * _audioSource.clip.length;
		_dspTimeOffset.SetValue(AudioSettings.dspTime - (double)time);
		float num2 = Mathf.Abs(num - time);
		if (num2 > _forcedSyncDeltaTime || _pause || forcedAudioSync)
		{
			_audioStartTimeOffsetSinceLevelLoad = Time.timeSinceLevelLoad - time;
			num = time;
		}
		else
		{
			if (_fixingAudioSyncError)
			{
				if (num2 < _stopSyncDeltaTime)
				{
					_fixingAudioSyncError = false;
				}
			}
			else if (num2 > _startSyncDeltaTime)
			{
				_fixingAudioSyncError = true;
			}
			if (_fixingAudioSyncError)
			{
				_audioStartTimeOffsetSinceLevelLoad = Mathf.Lerp(_audioStartTimeOffsetSinceLevelLoad, Time.timeSinceLevelLoad - time, Time.deltaTime * _audioSyncLerpSpeed);
			}
		}
		_songTime.SetValue(num - _songTimeOffset);
	}

	public void StartSong()
	{
		if (!(_audioSource.clip == null))
		{
			base.enabled = true;
			float num = _startSongTime + _songTimeOffset;
			if (num >= 0f)
			{
				_audioSource.time = num;
				_audioSource.Play();
				_audioStarted = true;
			}
			else
			{
				_audioSource.time = 0f;
				_audioStarted = false;
			}
			_audioStartTimeOffsetSinceLevelLoad = Time.timeSinceLevelLoad - num;
			_fixingAudioSyncError = false;
			_prevAudioSamplePos = (int)((float)_audioSource.clip.frequency * num);
			_playbackLoopIndex = 0;
			_dspTimeOffset.SetValue(AudioSettings.dspTime - (double)num);
			_songTime.SetValue(_startSongTime);
		}
	}

	public void StopSong()
	{
		_audioSource.Stop();
		base.enabled = false;
	}

	public void Pause()
	{
		if (!_pause && _audioSource.isPlaying)
		{
			_pause = true;
			_audioSource.Pause();
			base.enabled = false;
		}
	}

	public void Resume()
	{
		if (_pause)
		{
			_pause = false;
			_audioSource.UnPause();
			base.enabled = true;
		}
	}
}
