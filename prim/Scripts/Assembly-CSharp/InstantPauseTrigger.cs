using System;
using UnityEngine;

public class InstantPauseTrigger : MonoBehaviour, IPauseTrigger
{
	private Action _pauseWasTriggeredCallback;

	private void Update()
	{
		if (VRControllersInputManager.MenuButtonDown() && _pauseWasTriggeredCallback != null)
		{
			_pauseWasTriggeredCallback();
		}
	}

	public void SetCallback(Action pauseWasTriggeredCallback)
	{
		_pauseWasTriggeredCallback = pauseWasTriggeredCallback;
	}

	public void EnableTrigger(bool enable)
	{
		base.enabled = enable;
	}
}
