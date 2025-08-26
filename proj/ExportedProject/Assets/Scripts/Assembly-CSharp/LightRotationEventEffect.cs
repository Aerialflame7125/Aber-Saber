using UnityEngine;

public class LightRotationEventEffect : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectCallbackController))]
	private ObjectProvider _beatmapObjectCallbackControllerProvider;

	[SerializeField]
	private BeatmapEventType _event;

	[SerializeField]
	private Vector3 _rotationVector = Vector3.up;

	private const float kSpeedMultiplier = 20f;

	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	private Transform _transform;

	private Quaternion _startRotation;

	private float _rotationSpeed;

	private void Awake()
	{
		_transform = base.transform;
	}

	private void Start()
	{
		_beatmapObjectCallbackController = _beatmapObjectCallbackControllerProvider.GetProvidedObject<BeatmapObjectCallbackController>();
		_beatmapObjectCallbackController.beatmapEventDidTriggerEvent += HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		_transform = base.transform;
		_startRotation = _transform.rotation;
		base.enabled = false;
	}

	private void Update()
	{
		_transform.Rotate(_rotationVector, Time.deltaTime * _rotationSpeed, Space.Self);
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
			if (beatmapEventData.value == 0)
			{
				base.enabled = false;
				_transform.localRotation = _startRotation;
			}
			else if (beatmapEventData.value > 0)
			{
				_transform.localRotation = _startRotation;
				_transform.Rotate(_rotationVector, Random.Range(0f, 180f), Space.Self);
				base.enabled = true;
				_rotationSpeed = (float)beatmapEventData.value * 20f * ((!(Random.value > 0.5f)) ? (-1f) : 1f);
			}
		}
	}
}
