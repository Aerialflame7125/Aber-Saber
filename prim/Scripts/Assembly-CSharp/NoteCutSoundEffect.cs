using System;
using UnityEngine;

public class NoteCutSoundEffect : MonoBehaviour
{
	[SerializeField]
	private AudioSource _audioSource;

	[SerializeField]
	private AnimationCurve _speedToVolumeCurve;

	[SerializeField]
	private AudioClip[] _badCutSoundEffectAudioClips;

	[SerializeField]
	private float _badCutVolume = 1f;

	[SerializeField]
	private float _goodCutVolume = 1f;

	private bool _isPlaying;

	private Saber _saber;

	private NoteData _noteData;

	private bool _noteWasCut;

	private float _aheadTime;

	private float _noteMissedTimeOffset;

	private float _volumeScale;

	private float _beforeCutVolume;

	private RandomObjectPicker<AudioClip> _badCutRandomSoundPicker;

	private bool _handleWrongSaberTypeAsGood;

	public event Action<NoteCutSoundEffect> didFinishEvent;

	private void Awake()
	{
		_badCutRandomSoundPicker = new RandomObjectPicker<AudioClip>(_badCutSoundEffectAudioClips, 0.01f);
	}

	private void Start()
	{
		_audioSource.loop = false;
	}

	public void Init(AudioClip audioClip, float volumeScale, double noteDSPTime, float aheadTime, float missedTimeOffset, Saber saber, NoteData noteData, bool handleWrongSaberTypeAsGood)
	{
		_beforeCutVolume = 0f;
		base.enabled = true;
		_volumeScale = volumeScale;
		_audioSource.clip = audioClip;
		_noteMissedTimeOffset = missedTimeOffset;
		_aheadTime = aheadTime;
		_saber = saber;
		_noteData = noteData;
		_handleWrongSaberTypeAsGood = handleWrongSaberTypeAsGood;
		_noteWasCut = false;
		_audioSource.volume = _speedToVolumeCurve.Evaluate(saber.bladeSpeed) * _volumeScale;
		_audioSource.PlayScheduled(noteDSPTime - (double)aheadTime);
	}

	private void LateUpdate()
	{
		if (_audioSource.timeSamples >= _audioSource.clip.samples - 1)
		{
			StopPlayingAndFinish();
			return;
		}
		base.transform.position = _saber.saberBladeTopPos;
		if (!_noteWasCut)
		{
			float num = _goodCutVolume * _speedToVolumeCurve.Evaluate(_saber.bladeSpeed) * (1f - Mathf.Clamp01((_audioSource.time - _aheadTime) / _noteMissedTimeOffset)) * _volumeScale;
			if (num < _beforeCutVolume)
			{
				_beforeCutVolume = Mathf.Lerp(_beforeCutVolume, num, Time.deltaTime * 4f);
			}
			else
			{
				_beforeCutVolume = num;
			}
			_audioSource.volume = _beforeCutVolume;
		}
	}

	private void StopPlayingAndFinish()
	{
		base.enabled = false;
		_audioSource.Stop();
		_isPlaying = false;
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent(this);
		}
	}

	public void NoteWasCut(NoteController noteController, NoteCutInfo noteCutInfo)
	{
		if (noteController.noteData.id == _noteData.id)
		{
			_noteWasCut = true;
			if ((!_handleWrongSaberTypeAsGood && !noteCutInfo.allIsOK) || (_handleWrongSaberTypeAsGood && (!noteCutInfo.allExceptSaberTypeIsOK || noteCutInfo.saberTypeOK)))
			{
				AudioClip clip = _badCutRandomSoundPicker.PickRandomObject();
				_audioSource.clip = clip;
				_audioSource.time = 0f;
				_audioSource.Play();
				_audioSource.volume = _badCutVolume;
			}
			else
			{
				_audioSource.volume = _goodCutVolume * _volumeScale;
			}
		}
	}
}
