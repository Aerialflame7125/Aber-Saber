using System;
using UnityEngine;

public class NoteFloorMovement : MonoBehaviour
{
	[SerializeField]
	private FloatVariable _songTime;

	[Space]
	[SerializeField]
	private Transform _rotatedObject;

	private Vector3 _startPos;

	private Vector3 _endPos;

	private float _movementDuration;

	private float _startTime;

	public event Action floorMovementDidFinishEvent;

	public void Init(Vector3 startPos, Vector3 endPos, float movementDuration, float startTime)
	{
		_startPos = startPos;
		_endPos = endPos;
		_movementDuration = movementDuration;
		_startTime = startTime;
	}

	public void SetToStart()
	{
		base.transform.position = _startPos;
		_rotatedObject.transform.rotation = Quaternion.identity;
	}

	public void ManualUpdate()
	{
		float num = _songTime.value - _startTime;
		base.transform.position = Vector3.Lerp(_startPos, _endPos, num / _movementDuration);
		if (num >= _movementDuration && this.floorMovementDidFinishEvent != null)
		{
			this.floorMovementDidFinishEvent();
		}
	}
}
