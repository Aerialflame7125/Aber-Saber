using UnityEngine;

public class TunnelSmokeEventEffect : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectCallbackController))]
	private ObjectProvider _beatmapObjectCallbackControllerProvider;

	[SerializeField]
	private BeatmapEventType _event;

	[SerializeField]
	private ParticleSystem _particleSystem;

	private const float kSpeedMultiplier = 5f;

	private const int kMaxParticles = 50;

	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	private ParticleSystem.MainModule _mainModule;

	private ParticleSystem.ShapeModule _shapeModule;

	private ParticleSystem.Particle[] _particles;

	private void Start()
	{
		_beatmapObjectCallbackController = _beatmapObjectCallbackControllerProvider.GetProvidedObject<BeatmapObjectCallbackController>();
		_beatmapObjectCallbackController.beatmapEventDidTriggerEvent += HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		_mainModule = _particleSystem.main;
		_shapeModule = _particleSystem.shape;
		_particles = new ParticleSystem.Particle[50];
	}

	private void OnDestroy()
	{
		if ((bool)_beatmapObjectCallbackController)
		{
			_beatmapObjectCallbackController.beatmapEventDidTriggerEvent -= HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		}
	}

	private void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type == _event)
		{
			_mainModule.startSpeedMultiplier = (float)beatmapEventData.value * 5f;
			float num = _mainModule.startSpeed.constant * _mainModule.startLifetime.constant * 0.5f;
			_shapeModule.position = new Vector3(0f - num, 0f, 0f);
			int particles = _particleSystem.GetParticles(_particles);
			for (int i = 0; i < particles; i++)
			{
				_particles[i].velocity = new Vector3(_mainModule.startSpeed.constant * 0.5f, 0f, 0f);
			}
			_particleSystem.SetParticles(_particles, particles);
		}
	}
}
