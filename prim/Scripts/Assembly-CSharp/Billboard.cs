using UnityEngine;

[ExecuteInEditMode]
public class Billboard : MonoBehaviour
{
	public enum RotationMode
	{
		AllAxis,
		XAxis,
		YAxis,
		ZAxis
	}

	[SerializeField]
	private RotationMode _rotationMode;

	[SerializeField]
	private bool _flipDirection;

	private Transform _transform;

	private void Awake()
	{
		_transform = base.transform;
	}

	private void OnWillRenderObject()
	{
		Vector3 position = Camera.current.transform.position;
		Vector3 position2 = _transform.position;
		switch (_rotationMode)
		{
		case RotationMode.XAxis:
			position.x = position2.x;
			break;
		case RotationMode.YAxis:
			position.y = position2.y;
			break;
		case RotationMode.ZAxis:
			position.z = position2.z;
			break;
		}
		if (_flipDirection)
		{
			_transform.LookAt(2f * position2 - position);
		}
		else
		{
			_transform.LookAt(position);
		}
	}
}
