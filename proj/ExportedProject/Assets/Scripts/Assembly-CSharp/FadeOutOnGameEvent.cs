using UnityEngine;
using System.Collections;
using System;

public class FadeOutOnGameEvent : MonoBehaviour
{
	[SerializeField]
	private FloatGameEvent _gameEvent;

	[SerializeField]
	private Ease01 _fadeInOut;

	private void OnEnable()
	{
		_gameEvent.Subscribe(HandleGameEvent);
	}

	private void OnDisable()
	{
		_gameEvent.Unsubscribe(HandleGameEvent);
	}

	private void HandleGameEvent(float duration)
	{
		duration -= 0.1f;
		if (duration < 0f)
		{
			duration = 0f;
		}
		_fadeInOut.FadeOut(duration);
	}

	public IEnumerator HandleGameEventCoroutine(float dur, Action continuation = null)
	{
		dur -= 0.1f;
		if (dur < 0f)
		{
			dur = 0f;
		}

		// Call the Ease01 Coroutine, and pass a lambda for its 'onComplete' action
		yield return _fadeInOut.FadeOutCoroutine(dur, () =>
		{
			continuation?.Invoke(); // Invoke the next step
		});
	}
}
