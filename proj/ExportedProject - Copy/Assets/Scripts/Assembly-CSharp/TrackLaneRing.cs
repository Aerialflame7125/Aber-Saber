using UnityEngine;

public class TrackLaneRing : MonoBehaviour
{
	private float _rotZ;

	private float _destRotZ;

	private float _rotateSpeed;

	private float _destPosZ;

	private float _moveSpeed;

	private Vector3 _positionOffset;

	private Transform _transform;

	public void Init(Vector3 position, Vector3 positionOffset)
	{
		_transform = base.transform;
		_positionOffset = positionOffset;
		_transform.localPosition = position + positionOffset;
	}

	public void UpdateRing(float deltaTime)
	{
		_rotZ = Mathf.Lerp(_rotZ, _destRotZ, deltaTime * _rotateSpeed);
		_transform.localEulerAngles = new Vector3(0f, 0f, _rotZ);
		_transform.localPosition = new Vector3(_positionOffset.x, _positionOffset.y, Mathf.Lerp(_transform.localPosition.z, _positionOffset.z + _destPosZ, deltaTime * _moveSpeed));
	}

	public void SetRotation(float destRotZ, float rotateSpeed)
	{
		_destRotZ = destRotZ;
		_rotateSpeed = rotateSpeed;
	}

	public float GetRotation()
	{
		return _rotZ;
	}

	public float GetDestinationRotation()
	{
		return _destRotZ;
	}

	public void SetPosition(float destPosZ, float moveSpeed)
	{
		_destPosZ = destPosZ;
		_moveSpeed = moveSpeed;
	}
}
