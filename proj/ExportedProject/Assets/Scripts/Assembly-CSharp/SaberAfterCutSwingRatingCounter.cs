using UnityEngine;

public class SaberAfterCutSwingRatingCounter : HMAutoincrementedRequestId
{
	public delegate void DidChangeDelegate(SaberAfterCutSwingRatingCounter afterCutRating, float rating);

	public delegate void DidFinishDelegate(SaberAfterCutSwingRatingCounter afterCutRating);

	public DidChangeDelegate didChangeEvent;

	public DidFinishDelegate didFinishEvent;

	private Vector3 _cutPlaneNormal;

	private float _cutTime;

	private Vector3 _prevTopPos;

	private Vector3 _prevBottomPos;

	private float _rating;

	private bool _didFinish;

	public bool didFinish
	{
		get
		{
			return _didFinish;
		}
	}

	public float rating
	{
		get
		{
			return _rating;
		}
	}

	public void Init(Vector3 cutPlaneNormal, Vector3 topPos, Vector3 bottomPos, float cutTime)
	{
		_cutPlaneNormal = cutPlaneNormal;
		_cutTime = cutTime;
		_prevTopPos = topPos;
		_prevBottomPos = bottomPos;
		_didFinish = false;
		_rating = 0f;
	}

	public void ProcessNewSaberData(Vector3 topPos, Vector3 bottomPos, float time)
	{
		if (time - _cutTime > 0.4f)
		{
			if (didFinishEvent != null)
			{
				_didFinish = true;
				didFinishEvent(this);
			}
			return;
		}
		Vector3 prevTopPos = _prevTopPos;
		Vector3 prevBottomPos = _prevBottomPos;
		Vector3 normalized = Vector3.Cross(topPos - bottomPos, (prevTopPos + prevBottomPos) * 0.5f - bottomPos).normalized;
		Vector3 vector = prevTopPos - prevBottomPos + bottomPos;
		float angleDiff = Vector3.Angle(vector - bottomPos, topPos - bottomPos);
		float num = Vector3.Angle(normalized, _cutPlaneNormal);
		_prevTopPos = topPos;
		_prevBottomPos = bottomPos;
		if (num > 90f)
		{
			if (didFinishEvent != null)
			{
				_didFinish = true;
				didFinishEvent(this);
			}
			return;
		}
		_rating += SaberSwingRating.AfterCutStepRating(angleDiff, num);
		if (_rating > 1f)
		{
			_rating = 1f;
		}
		if (didChangeEvent != null)
		{
			didChangeEvent(this, _rating);
		}
	}
}
