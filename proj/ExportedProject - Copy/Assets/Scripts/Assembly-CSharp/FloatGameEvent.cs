using System;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatGameEvent", menuName = "HMLib/GameEvent")]
public class FloatGameEvent : GameEvent
{
	private Action<float> _floatEvent;

	public override void Raise()
	{
		base.Raise();
		if (_floatEvent != null)
		{
			_floatEvent(0f);
		}
	}

	public void Raise(float f)
	{
		base.Raise();
		if (_floatEvent != null)
		{
			_floatEvent(f);
		}
	}

	public void Subscribe(Action<float> foo)
	{
		_floatEvent = (Action<float>)Delegate.Remove(_floatEvent, foo);
		_floatEvent = (Action<float>)Delegate.Combine(_floatEvent, foo);
	}

	public void Unsubscribe(Action<float> foo)
	{
		_floatEvent = (Action<float>)Delegate.Remove(_floatEvent, foo);
	}
}
