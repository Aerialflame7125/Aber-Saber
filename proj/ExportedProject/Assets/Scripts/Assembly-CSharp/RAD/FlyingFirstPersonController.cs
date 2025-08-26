using UnityEngine;

namespace RAD
{
	public class FlyingFirstPersonController : MonoBehaviour
	{
		[SerializeField]
		private float _moveSensitivity = 0.1f;

		[SerializeField]
		private Transform _cameraTransform;

		[SerializeField]
		private MouseLook _mouseLook = new MouseLook();

		private void Start()
		{
			_mouseLook.Init(base.transform, _cameraTransform);
		}

		private void Update()
		{
			_mouseLook.LookRotation(base.transform, _cameraTransform);
			Vector3 position = base.transform.position;
			Vector3 vector = _cameraTransform.forward * Input.GetAxis("Vertical");
			Vector3 vector2 = _cameraTransform.right * Input.GetAxis("Horizontal");
			position += (vector + vector2) * _moveSensitivity;
			base.transform.position = position;
		}
	}
}
