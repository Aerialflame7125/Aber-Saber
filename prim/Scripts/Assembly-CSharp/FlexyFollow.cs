using UnityEngine;

public class FlexyFollow : MonoBehaviour
{
	public GameObject _followObject;

	public float _followSpeed = 2f;

	public Vector3 _offset;

	public bool _fixedXOffset;

	public bool _fixedYOffset;

	public bool _fixedZOffset;

	public bool _useLocalPosition;

	private Transform _followTransform;

	private Transform _transform;

	private void Start()
	{
		_transform = base.transform;
		_followTransform = _followObject.transform;
		if (_useLocalPosition)
		{
			_transform.position = _followTransform.position + _offset;
		}
		else
		{
			_transform.localPosition = _followTransform.position + _offset;
		}
	}

	private void LateUpdate()
	{
		Vector3 a = ((!_useLocalPosition) ? _transform.position : _transform.localPosition);
		Vector3 b = _followTransform.position + _offset;
		Vector3 vector = Vector3.Lerp(a, b, Time.deltaTime * _followSpeed);
		if (_fixedXOffset)
		{
			vector.x = b.x;
		}
		if (_fixedYOffset)
		{
			vector.y = b.y;
		}
		if (_fixedZOffset)
		{
			vector.z = b.z;
		}
		if (_useLocalPosition)
		{
			_transform.localPosition = vector;
		}
		else
		{
			_transform.position = vector;
		}
	}
}
