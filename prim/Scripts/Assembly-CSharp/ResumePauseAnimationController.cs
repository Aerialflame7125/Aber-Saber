using System;
using UnityEngine;

public class ResumePauseAnimationController : MonoBehaviour
{
	private bool _resuming;

	public bool resuming => _resuming;

	public event Action animationDidFinishEvent;

	public void StartAnimation()
	{
		_resuming = true;
		base.gameObject.SetActive(value: true);
	}

	public void StopAnimation()
	{
		_resuming = false;
		base.gameObject.SetActive(value: false);
	}

	public void AnimationDidFinish()
	{
		StopAnimation();
		if (this.animationDidFinishEvent != null)
		{
			this.animationDidFinishEvent();
		}
	}
}
