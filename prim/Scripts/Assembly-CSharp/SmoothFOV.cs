using UnityEngine;

public class SmoothFOV : MonoBehaviour
{
	[SerializeField]
	private Camera _camera;

	[SerializeField]
	private float _smooth;

	[SerializeField]
	[Range(30f, 140f)]
	private float _fov;

	private void Update()
	{
		_camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _fov, Time.deltaTime * _smooth);
	}
}
