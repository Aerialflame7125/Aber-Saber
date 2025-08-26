using System;
using UnityEngine;

public class EnableOnVisible : MonoBehaviour
{
	public Behaviour[] _components;

	public event Action<bool> VisibilityChangedEvent;

	private void Awake()
	{
		for (int i = 0; i < _components.Length; i++)
		{
			_components[i].enabled = false;
		}
	}

	private void OnBecameVisible()
	{
		for (int i = 0; i < _components.Length; i++)
		{
			_components[i].enabled = true;
		}
		if (this.VisibilityChangedEvent != null)
		{
			this.VisibilityChangedEvent(true);
		}
	}

	private void OnBecameInvisible()
	{
		for (int i = 0; i < _components.Length; i++)
		{
			_components[i].enabled = false;
		}
		if (this.VisibilityChangedEvent != null)
		{
			this.VisibilityChangedEvent(false);
		}
	}
}
