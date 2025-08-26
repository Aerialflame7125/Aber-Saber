using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "HMLib/GameEvent")]
public class GameEvent : ScriptableObject
{
	private event Action _event;

	public virtual void Raise()
	{
		if (this._event != null)
		{
			this._event();
		}
	}

	public void Subscribe(Action foo)
	{
		_event -= foo;
		_event += foo;
	}

	public void Unsubscribe(Action foo)
	{
		_event -= foo;
	}
}
