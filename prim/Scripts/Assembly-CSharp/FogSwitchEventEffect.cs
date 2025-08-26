using System.Collections;
using UnityEngine;

public class FogSwitchEventEffect : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectCallbackController))]
	private ObjectProvider _beatmapObjectCallbackControllerProvider;

	[SerializeField]
	private BloomFog _bloomFog;

	[SerializeField]
	private BeatmapEventType _event;

	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	private const float kTransitionDuration = 2f;

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
		if (beatmapEventData.type == _event)
		{
			if (beatmapEventData.value == 0)
			{
				AnimateToFog(2f, 0f);
			}
			else if (beatmapEventData.value > 0 || beatmapEventData.value == -1)
			{
				AnimateToFog(2f, 1f);
			}
		}
	}

	private void AnimateToFog(float duration, float value)
	{
		StopAllCoroutines();
		StartCoroutine(AnimateToFogCoroutine(duration, value));
	}

	private IEnumerator AnimateToFogCoroutine(float duration, float value)
	{
		float startTransition = _bloomFog.transition;
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			_bloomFog.transition = Mathf.Lerp(startTransition, value, elapsedTime / duration);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		_bloomFog.transition = value;
	}
}
