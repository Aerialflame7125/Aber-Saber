using UnityEngine;

public class MainAudioEffects : MonoBehaviour
{
	[SerializeField]
	private PlayerHeadAndObstacleInteraction _playerHeadAndObstacleInteraction;

	[SerializeField]
	private AudioLowPassFilter _audioLowPassFilter;

	[SerializeField]
	private float _smooth = 8f;

	private const int kDefaultCutoffFrequency = 22000;

	private const int kLowPassCutoffFrequency = 150;

	private float _targetFrequency;

	private void Update()
	{
		_targetFrequency = 22000f;
		if (_playerHeadAndObstacleInteraction.intersectingObstacles.Count > 0)
		{
			_targetFrequency = 150f;
		}
		float cutoffFrequency = _audioLowPassFilter.cutoffFrequency;
		if (_targetFrequency < cutoffFrequency)
		{
			_audioLowPassFilter.cutoffFrequency = _targetFrequency;
		}
		else
		{
			_audioLowPassFilter.cutoffFrequency = Mathf.Lerp(cutoffFrequency, _targetFrequency, Time.deltaTime * _smooth);
		}
	}
}
