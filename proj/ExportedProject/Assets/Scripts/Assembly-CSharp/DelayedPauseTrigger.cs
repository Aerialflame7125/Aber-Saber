using System;
using UnityEngine;

public class DelayedPauseTrigger : MonoBehaviour, IPauseTrigger
{
	private float _longPressDuration = 0.3f;

	private float _timer;

	private bool _waitingForButtonRelease;

	private Action _pauseWasTriggeredCallback;

	private void Update()
	{
		if (VRControllersInputManager.MenuButton() && !_waitingForButtonRelease)
		{
			_timer += Time.deltaTime;
			if (_timer > _longPressDuration)
			{
				_waitingForButtonRelease = true;
				if (_pauseWasTriggeredCallback != null)
				{
					_pauseWasTriggeredCallback();
				}
			}
		}
		else
		{
			_waitingForButtonRelease = false;
			_timer = 0f;
		}
	}

	public void SetCallback(Action pauseWasTriggeredCallback)
	{
		_pauseWasTriggeredCallback = pauseWasTriggeredCallback;
	}

	public void SetLongPressDuration(float longPressDuration)
	{
		_longPressDuration = longPressDuration;
	}

	public void EnableTrigger(bool enable)
	{
		base.enabled = enable;
	}
}
