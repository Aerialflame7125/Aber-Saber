using System;
using UnityEngine;

public class ResumePauseAnimationController : MonoBehaviour
{
	private bool _resuming;

	public bool resuming
	{
		get
		{
			return _resuming;
		}
	}

	public event Action animationDidFinishEvent;

	public void StartAnimation()
	{
		_resuming = true;
		base.gameObject.SetActive(true);
	}

	public void StopAnimation()
	{
		_resuming = false;
		base.gameObject.SetActive(false);
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
