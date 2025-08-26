using UnityEngine;
using UnityEngine.XR;

public class VRController : MonoBehaviour
{
	[SerializeField]
	private XRNode _node;

	public XRNode node
	{
		get
		{
			return _node;
		}
	}

	public Vector3 position
	{
		get
		{
			return base.transform.position;
		}
	}

	public Vector3 forward
	{
		get
		{
			return base.transform.forward;
		}
	}

	public float triggerValue
	{
		get
		{
			return VRControllersInputManager.TriggerValue(_node);
		}
	}

	public float verticalAxisValue
	{
		get
		{
			return VRControllersInputManager.VerticalAxisValue(_node);
		}
	}

	public float horizontalAxisValue
	{
		get
		{
			return VRControllersInputManager.HorizontalAxisValue(_node);
		}
	}

	public bool active
	{
		get
		{
			return base.gameObject.activeInHierarchy;
		}
	}

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
