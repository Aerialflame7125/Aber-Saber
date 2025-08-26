using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class MainCamera : MonoBehaviour
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	[SerializeField]
	private MainEffect _mainEffect;

	private Camera _camera;

	private Transform _transform;

	public MainEffect mainEffect => _mainEffect;

	public Camera camera => _camera;

	public Vector3 position => _transform.position;

	private void Awake()
	{
		_transform = base.transform;
		_camera = GetComponent<Camera>();
		LIVBS.Init(_camera);
	}
}
