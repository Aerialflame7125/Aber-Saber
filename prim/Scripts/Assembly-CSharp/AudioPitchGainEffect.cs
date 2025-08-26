using System;
using System.Collections;
using UnityEngine;

public class AudioPitchGainEffect : MonoBehaviour
{
	[SerializeField]
	private AudioSource _audioSource;

	[SerializeField]
	private float _duration = 0.3f;

	[SerializeField]
	private AnimationCurve _pitchCurve;

	[SerializeField]
	private AnimationCurve _gainCurve;

	private IEnumerator StartEffectCoroutine(float volumeScale, Action finishCallback)
	{
		float time = 0f;
		while (time < _duration)
		{
			float t = time / _duration;
			_audioSource.pitch = _pitchCurve.Evaluate(t);
			_audioSource.volume = _gainCurve.Evaluate(t) * volumeScale;
			time += Time.deltaTime;
			yield return null;
		}
		_audioSource.pitch = _pitchCurve.Evaluate(1f);
		_audioSource.volume = _gainCurve.Evaluate(1f) * volumeScale;
		finishCallback?.Invoke();
	}

	public void StartEffect(float volumeScale, Action finishCallback)
	{
		StartCoroutine(StartEffectCoroutine(volumeScale, finishCallback));
	}
}
