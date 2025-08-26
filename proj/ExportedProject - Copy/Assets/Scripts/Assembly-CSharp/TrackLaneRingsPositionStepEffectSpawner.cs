using UnityEngine;

public class TrackLaneRingsPositionStepEffectSpawner : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectCallbackController))]
	private ObjectProvider _beatmapObjectCallbackControllerProvider;

	[SerializeField]
	private TrackLaneRingsManager _trackLaneRingsManager;

	[Space]
	[SerializeField]
	private BeatmapEventType _beatmapEventType;

	[Space]
	[SerializeField]
	private float _minPositionStep = 1f;

	[SerializeField]
	private float _maxPositionStep = 2f;

	[SerializeField]
	private float _moveSpeed = 1f;

	private bool _prevWasMinStep;

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

	public void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type == _beatmapEventType)
		{
			float num = ((!_prevWasMinStep) ? _minPositionStep : _maxPositionStep);
			_prevWasMinStep = !_prevWasMinStep;
			TrackLaneRing[] rings = _trackLaneRingsManager.Rings;
			for (int i = 0; i < rings.Length; i++)
			{
				float destPosZ = (float)i * num;
				rings[i].SetPosition(destPosZ, _moveSpeed);
			}
		}
	}
}
