using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Vector3 _rotationVector = new Vector3(0f, 1f, 0f);

	public float _speed = 1f;

	private Transform _transform;

	private Vector3 _startRotationAngles;

	private void Awake()
	{
		_transform = base.transform;
		_startRotationAngles = _transform.localEulerAngles;
		if ((bool)GetComponent<Renderer>())
		{
			base.enabled = false;
		}
	}

	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	private void Update()
	{
		_transform.localEulerAngles = _startRotationAngles + _rotationVector * _speed * Time.timeSinceLevelLoad;
	}
}
