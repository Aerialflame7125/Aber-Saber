using System;
using UnityEngine;

public class SinMove : MonoBehaviour
{
	[SerializeField]
	private float _speed = 1f;

	[SerializeField]
	private Vector3 _offset = new Vector3(1f, 0f, 0f);

	private float _time;

	private Vector3 _startPos;

	private void Start()
	{
		_startPos = base.transform.localPosition;
	}

	private void Update()
	{
		_time += Time.deltaTime;
		base.transform.localPosition = _startPos + Mathf.Sin(_time * _speed) * _offset;
		if (Mathf.Abs(_time * _speed) > (float)Math.PI * 2f * _speed)
		{
			_time -= (float)Math.PI * 2f / _speed * Mathf.Sign(_time);
		}
	}
}
