using UnityEngine;

public class AverageSpeedComputer : MonoBehaviour
{
	[SerializeField]
	private float _smoothUp = 16f;

	[SerializeField]
	private float _smoothDown = 2f;

	private Vector3 _prevPos;

	private float _speed;

	public void Init(Vector3 pos)
	{
		_speed = 0f;
		_prevPos = pos;
	}

	public float ComputeNewSpeed(Vector3 pos, float deltaTime)
	{
		float num = (pos - _prevPos).magnitude / deltaTime;
		_speed = Mathf.Lerp(_speed, num, ((!(num > _speed)) ? _smoothDown : _smoothUp) - deltaTime);
		_prevPos = pos;
		return _speed;
	}
}
