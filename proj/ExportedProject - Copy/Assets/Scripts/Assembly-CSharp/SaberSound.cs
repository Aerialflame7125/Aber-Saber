using UnityEngine;

public class SaberSound : MonoBehaviour
{
	[SerializeField]
	private Transform _saberTop;

	[SerializeField]
	private AudioSource _audioSource;

	[SerializeField]
	private AnimationCurve _pitchBySpeedCurve;

	[SerializeField]
	private AnimationCurve _gainBySpeedCurve;

	[SerializeField]
	private float _speedMultiplier = 0.05f;

	[SerializeField]
	private float _upSmooth = 4f;

	[SerializeField]
	private float _downSmooth = 4f;

	[Tooltip("No sound is produced if saber point moves more than this distance in one frame. This basically fixes the start sound problem.")]
	[SerializeField]
	private float _noSoundTopThresholdSqr = 1f;

	private Vector3 _prevPos;

	private float _speed;

	private void Start()
	{
		_prevPos = _saberTop.position;
	}

	private void Update()
	{
		Vector3 position = _saberTop.position;
		if ((_prevPos - position).sqrMagnitude > _noSoundTopThresholdSqr)
		{
			_prevPos = position;
		}
		float num = ((Time.deltaTime != 0f) ? (_speedMultiplier * Vector3.Distance(position, _prevPos) / Time.deltaTime) : 0f);
		if (num < _speed)
		{
			_speed = Mathf.Clamp01(Mathf.Lerp(_speed, num, Time.deltaTime * _downSmooth));
		}
		else
		{
			_speed = Mathf.Clamp01(Mathf.Lerp(_speed, num, Time.deltaTime * _upSmooth));
		}
		_audioSource.pitch = _pitchBySpeedCurve.Evaluate(_speed);
		_audioSource.volume = _gainBySpeedCurve.Evaluate(_speed);
		_prevPos = position;
	}
}
