using UnityEngine;

public class TrackLaneRingsRotationEffectSpawner : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectCallbackController))]
	private ObjectProvider _beatmapObjectCallbackControllerProvider;

	[SerializeField]
	private TrackLaneRingsRotationEffect _trackLaneRingsRotationEffect;

	[Space]
	[SerializeField]
	private BeatmapEventType _beatmapEventType;

	[Space]
	[SerializeField]
	private float _rotationStep = 5f;

	[SerializeField]
	private float _rotationPropagationSpeed = 1f;

	[SerializeField]
	private float _rotationFlexySpeed = 1f;

	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	private void Start()
	{
		_beatmapObjectCallbackController = _beatmapObjectCallbackControllerProvider.GetProvidedObject<BeatmapObjectCallbackController>();
		_beatmapObjectCallbackController.beatmapEventDidTriggerEvent += HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
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
		if (beatmapEventData.type == _beatmapEventType)
		{
			_trackLaneRingsRotationEffect.AddRingRotationEffect(_trackLaneRingsRotationEffect.GetFirstRingDestinationRotationAngle() + (float)(90 * ((Random.value < 0.5f) ? 1 : (-1))), Random.Range(0f, _rotationStep), _rotationPropagationSpeed, _rotationFlexySpeed);
		}
	}
}
