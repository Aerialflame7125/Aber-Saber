using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class VisibilityChecker : MonoBehaviour
{
	public event Action OnBecameVisibleEvent;

	public event Action OnBecameInvisibleEvent;

	private void OnBecameVisible()
	{
		if (this.OnBecameVisibleEvent != null)
		{
			this.OnBecameVisibleEvent();
		}
	}

	private void OnBecameInvisible()
	{
		if (this.OnBecameInvisibleEvent != null)
		{
			this.OnBecameInvisibleEvent();
		}
	}
}
