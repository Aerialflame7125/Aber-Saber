using UnityEngine;
using UnityEngine.XR;

public class VRController : MonoBehaviour
{
	[SerializeField]
	private XRNode _node;

	public XRNode node => _node;

	public Vector3 position => base.transform.position;

	public Vector3 forward => base.transform.forward;

	public float triggerValue => VRControllersInputManager.TriggerValue(_node);

	public float verticalAxisValue => VRControllersInputManager.VerticalAxisValue(_node);

	public float horizontalAxisValue => VRControllersInputManager.HorizontalAxisValue(_node);

	public bool active => base.gameObject.activeInHierarchy;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
		Vector3 localPosition = InputTracking.GetLocalPosition(_node);
		Quaternion localRotation = InputTracking.GetLocalRotation(_node);
		base.transform.localPosition = localPosition;
		base.transform.localRotation = localRotation;
		PersistentSingleton<VRPlatformHelper>.instance.AdjustPlatformSpecificControllerTransform(base.transform);
	}
}
