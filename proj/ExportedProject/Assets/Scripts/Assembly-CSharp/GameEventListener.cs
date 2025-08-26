using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
	[SerializeField]
	private GameEvent _gameEvent;

	[SerializeField]
	private UnityEvent _unityEvent;

	private void OnEnable()
	{
		_gameEvent.Subscribe(HandleEvent);
	}

	private void OnDisable()
	{
		_gameEvent.Unsubscribe(HandleEvent);
	}

	private void HandleEvent()
	{
		_unityEvent.Invoke();
	}
}
