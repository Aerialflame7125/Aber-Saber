using UnityEngine;

public class ObstacleSaberSoundEffect : MonoBehaviour
{
	[SerializeField]
	private ObstacleSaberSparkleEffectManager _obstacleSaberSparkleEffectManager;

	[SerializeField]
	private Saber.SaberType _saberType;

	[SerializeField]
	private AudioSource _audioSource;

	[SerializeField]
	private float _volume;

	private const float kSmooth = 8f;

	private float _targetVolume;

	private void Awake()
	{
		base.enabled = false;
		_audioSource.volume = 0f;
		_obstacleSaberSparkleEffectManager.sparkleEffectDidStartEvent += HandleSparkleEffectDidStart;
		_obstacleSaberSparkleEffectManager.sparkleEffectDidEndEvent += HandleSparkleEffecDidEnd;
	}

	private void OnDestroy()
	{
		if (_obstacleSaberSparkleEffectManager != null)
		{
			_obstacleSaberSparkleEffectManager.sparkleEffectDidStartEvent -= HandleSparkleEffectDidStart;
			_obstacleSaberSparkleEffectManager.sparkleEffectDidEndEvent -= HandleSparkleEffecDidEnd;
		}
	}

	private void LateUpdate()
	{
		_audioSource.volume = Mathf.Lerp(_audioSource.volume, _targetVolume, Time.deltaTime * 8f);
		if (_audioSource.volume <= 0.01f && _audioSource.isPlaying)
		{
			base.enabled = false;
			_audioSource.Stop();
		}
		base.transform.position = _obstacleSaberSparkleEffectManager.BurnMarkPosForSaberType(_saberType);
	}

	private void HandleSparkleEffectDidStart(Saber.SaberType saberType)
	{
		if (saberType == _saberType)
		{
			base.enabled = true;
			if (!_audioSource.isPlaying)
			{
				_audioSource.time = Random.Range(0f, Mathf.Max(0f, _audioSource.clip.length - 0.1f));
				_audioSource.Play();
			}
			_targetVolume = _volume;
			_audioSource.volume = _volume;
		}
	}

	private void HandleSparkleEffecDidEnd(Saber.SaberType saberType)
	{
		if (saberType == _saberType)
		{
			_targetVolume = 0f;
		}
	}
}
