using System;
using UnityEngine;

public class BombCutSoundEffect : MonoBehaviour
{
	[SerializeField]
	private AudioSource _audioSource;

	private Saber _saber;

	public event Action<BombCutSoundEffect> didFinishEvent;

	private void Start()
	{
		_audioSource.loop = false;
	}

	public void Init(AudioClip audioClip, Saber saber, float volume)
	{
		if (audioClip == null || saber == null)
		{
			Debug.LogError("BombCutSoundEffect.Init was called with null parameters!");
			return;
		}

		base.enabled = true;
		_audioSource.clip = audioClip;
		_saber = saber;
		_audioSource.volume = volume;
		_audioSource.Play();
	}


	private void LateUpdate()
	{
		if (_audioSource == null || _audioSource.clip == null || _saber == null)
			return; // nothing to do

		if (_audioSource.timeSamples >= _audioSource.clip.samples - 1)
		{
			StopPlayingAndFinish();
		}
		else
		{
			base.transform.position = _saber.saberBladeTopPos;
		}
	}


	private void StopPlayingAndFinish()
	{
		base.enabled = false;
		_audioSource.Stop();
		if (this.didFinishEvent != null)
		{
			this.didFinishEvent(this);
		}
	}
}
