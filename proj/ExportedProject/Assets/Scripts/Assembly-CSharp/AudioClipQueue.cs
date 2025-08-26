using System.Collections.Generic;
using UnityEngine;

public class AudioClipQueue : MonoBehaviour
{
	[SerializeField]
	private AudioSource _audioSource;

	private List<AudioClip> _queue = new List<AudioClip>();

	private float _delay;

	private void Awake()
	{
		_audioSource.loop = false;
	}

	private void Update()
	{
		if (_delay > 0f)
		{
			_delay -= Time.deltaTime;
		}
		else if (_queue.Count > 0 && !_audioSource.isPlaying)
		{
			AudioClip clip = _queue[0];
			_queue.RemoveAt(0);
			_audioSource.clip = clip;
			_audioSource.Play();
		}
		else if (_queue.Count == 0)
		{
			base.enabled = false;
		}
	}

	public void PlayAudioClipWithDelay(AudioClip audioClip, float delay)
	{
		_delay = Mathf.Max(_delay, delay);
		_queue.Add(audioClip);
		base.enabled = true;
	}
}
