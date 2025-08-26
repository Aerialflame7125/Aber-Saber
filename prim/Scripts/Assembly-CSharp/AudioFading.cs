using UnityEngine;

public class AudioFading : MonoBehaviour
{
	[SerializeField]
	private AudioSource _audioSource;

	[SerializeField]
	private float _smooth = 4f;

	[SerializeField]
	private bool _fadeInOnStart;

	private float _targetVolume;

	private void Start()
	{
		if (_fadeInOnStart)
		{
			_audioSource.volume = 0f;
			FadeIn();
		}
		else
		{
			base.enabled = false;
		}
	}

	private void Update()
	{
		if (Mathf.Abs(_targetVolume - _audioSource.volume) < 0.001f)
		{
			_audioSource.volume = _targetVolume;
			base.enabled = false;
		}
		else
		{
			_audioSource.volume = Mathf.Lerp(_audioSource.volume, _targetVolume, Time.deltaTime * _smooth);
		}
	}

	public void FadeOut()
	{
		base.enabled = true;
		_targetVolume = 0f;
	}

	public void FadeIn()
	{
		base.enabled = true;
		_targetVolume = 1f;
	}
}
