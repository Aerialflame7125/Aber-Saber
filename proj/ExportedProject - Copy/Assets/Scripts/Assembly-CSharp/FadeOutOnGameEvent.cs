using UnityEngine;

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
}
