using System;
using System.Collections;
using UnityEngine;

public class ShowHideAnimationController : MonoBehaviour
{
	public Animator _animator;

	public bool _deactivateSelfAfterDelay;

	public float _deactivationDelay = 1f;

	private bool _show;

	private int _showAnimatorParam;

	public bool Show
	{
		get
		{
			return _show;
		}
		set
		{
			if (_show != value)
			{
				base.gameObject.SetActive(value: true);
				_animator.SetBool(_showAnimatorParam, value);
				Func<float, IEnumerator> func = DeactivateSelfAfterDelayCoroutine;
				StopCoroutine(func.Method.Name);
				if (!value && _deactivateSelfAfterDelay)
				{
					StartCoroutine(func.Method.Name, _deactivationDelay);
				}
				_show = value;
			}
		}
	}

	private void Awake()
	{
		_showAnimatorParam = Animator.StringToHash("Show");
		_show = _animator.GetBool(_showAnimatorParam);
	}

	private IEnumerator DeactivateSelfAfterDelayCoroutine(float delay)
	{
		yield return new WaitForSeconds(delay);
		base.gameObject.SetActive(value: false);
	}
}
