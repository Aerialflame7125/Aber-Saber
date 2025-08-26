using System.Collections;
using UnityEngine;

public class TweenPosition : MonoBehaviour
{
	public bool _unscaledTime;

	public bool _localPosition;

	public float _duration = 1f;

	public AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

	private Transform _transform;

	private Vector3 _targetPos;

	public Vector3 TargetPos
	{
		get
		{
			return _targetPos;
		}
		set
		{
			if (!(TargetPos == value))
			{
				_targetPos = value;
				AnimateToNewPos(_targetPos);
			}
		}
	}

	private void Awake()
	{
		_transform = base.transform;
	}

	private void AnimateToNewPos(Vector3 pos)
	{
		this.StartUniqueCoroutine(AnimateToNewPosCoroutine, pos);
	}

	private IEnumerator AnimateToNewPosCoroutine(Vector3 pos)
	{
		Vector3 startPos = ((!_localPosition) ? _transform.position : _transform.localPosition);
		float elapsedTime = 0f;
		while (elapsedTime < _duration)
		{
			if (_localPosition)
			{
				_transform.localPosition = Vector3.Lerp(startPos, _targetPos, _animationCurve.Evaluate(elapsedTime / _duration));
			}
			else
			{
				_transform.position = Vector3.Lerp(startPos, _targetPos, _animationCurve.Evaluate(elapsedTime / _duration));
			}
			elapsedTime += ((!_unscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime);
			yield return null;
		}
		if (_localPosition)
		{
			_transform.localPosition = _targetPos;
		}
		else
		{
			_transform.position = _targetPos;
		}
	}
}
