using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class MixedRealityCameraCalibration : MonoBehaviour
{
	public struct CameraInternalSettings
	{
		public Vector3 cameraPosition;

		public Vector3 topLeftMarker;

		public Vector3 topRightMarker;

		public Vector3 bottomLeftMarker;

		public Vector3 bottomRightMarker;
	}

	public enum STATE
	{
		INTRO,
		CAMERA,
		TOP_LEFT,
		TOP_RIGHT,
		BOTTOM_LEFT,
		BOTTOM_RIGHT,
		COMPLETED
	}

	[SerializeField]
	[Provider(typeof(MixedReality))]
	private ObjectProvider _mixedRealityProvider;

	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	public Image cameraMarker;

	public Image topLeftMarker;

	public Image topRightMarker;

	public Image bottomLeftMarker;

	public Image bottomRightMarker;

	public RectTransform introScreen;

	public RectTransform cameraScreen;

	public RectTransform topLeftScreen;

	public RectTransform topRightScreen;

	public RectTransform bottomLeftScreen;

	public RectTransform bottomRightScreen;

	public RectTransform endScreen;

	private STATE _state;

	private MixedReality _mixedReality;

	protected CameraInternalSettings internalSettings;

	protected Vector3 controllerOffset = new Vector3(0f, -0.036f, 0.012f);

	private float lastPressDown;

	private float lastTrackerContinute;

	public STATE state
	{
		get
		{
			return _state;
		}
		set
		{
			SetState(value);
		}
	}

	public void SetState(STATE value)
	{
		_state = value;
		HideScreens();
		switch (state)
		{
		case STATE.INTRO:
			introScreen.gameObject.SetActive(value: true);
			break;
		case STATE.CAMERA:
			cameraScreen.gameObject.SetActive(value: true);
			break;
		case STATE.TOP_LEFT:
			topLeftScreen.gameObject.SetActive(value: true);
			break;
		case STATE.TOP_RIGHT:
			topRightScreen.gameObject.SetActive(value: true);
			break;
		case STATE.BOTTOM_LEFT:
			bottomLeftScreen.gameObject.SetActive(value: true);
			break;
		case STATE.BOTTOM_RIGHT:
			bottomRightScreen.gameObject.SetActive(value: true);
			break;
		case STATE.COMPLETED:
			endScreen.gameObject.SetActive(value: true);
			break;
		}
	}

	public void Restart()
	{
		SetState(STATE.INTRO);
	}

	private void Awake()
	{
		state = STATE.INTRO;
	}

	private void Start()
	{
		_mixedReality = _mixedRealityProvider.GetProvidedObject<MixedReality>();
	}

	private void OnEnable()
	{
		Restart();
	}

	public void HideScreens()
	{
		introScreen.gameObject.SetActive(value: false);
		cameraScreen.gameObject.SetActive(value: false);
		topLeftScreen.gameObject.SetActive(value: false);
		topRightScreen.gameObject.SetActive(value: false);
		bottomLeftScreen.gameObject.SetActive(value: false);
		bottomRightScreen.gameObject.SetActive(value: false);
		endScreen.gameObject.SetActive(value: false);
	}

	private void LateUpdate()
	{
		Vector3 position;
		switch (state)
		{
		case STATE.INTRO:
			HideMarkers();
			if (GetTrackerContinue())
			{
				state = STATE.CAMERA;
			}
			break;
		case STATE.CAMERA:
			HideMarkers();
			cameraMarker.enabled = true;
			if (GetTrackerPosition(out position))
			{
				internalSettings.cameraPosition = position;
				_mixedRealitySettings.cameraPositionOffset = _mixedReality.mixedRealityCompositorCamera.externalCameraTrackedObjectTransform.InverseTransformPoint(internalSettings.cameraPosition);
				state = STATE.TOP_LEFT;
			}
			break;
		case STATE.TOP_LEFT:
			HideMarkers();
			topLeftMarker.enabled = true;
			if (GetTrackerPosition(out position))
			{
				internalSettings.topLeftMarker = position;
				state = STATE.TOP_RIGHT;
			}
			break;
		case STATE.TOP_RIGHT:
			HideMarkers();
			topRightMarker.enabled = true;
			if (GetTrackerPosition(out position))
			{
				internalSettings.topRightMarker = position;
				state = STATE.BOTTOM_LEFT;
			}
			break;
		case STATE.BOTTOM_LEFT:
			HideMarkers();
			bottomLeftMarker.enabled = true;
			if (GetTrackerPosition(out position))
			{
				internalSettings.bottomLeftMarker = position;
				state = STATE.BOTTOM_RIGHT;
			}
			break;
		case STATE.BOTTOM_RIGHT:
			HideMarkers();
			bottomRightMarker.enabled = true;
			if (GetTrackerPosition(out position))
			{
				internalSettings.bottomRightMarker = position;
				CalibrateCamera();
				state = STATE.COMPLETED;
			}
			break;
		case STATE.COMPLETED:
			HideMarkers();
			break;
		}
	}

	private void CalibrateCamera()
	{
		Vector3 vector = internalSettings.topLeftMarker - internalSettings.cameraPosition;
		Vector3 vector2 = internalSettings.topRightMarker - internalSettings.cameraPosition;
		Vector3 vector3 = internalSettings.bottomLeftMarker - internalSettings.cameraPosition;
		Vector3 vector4 = internalSettings.bottomRightMarker - internalSettings.cameraPosition;
		Vector3 normalized = vector.normalized;
		Vector3 normalized2 = vector2.normalized;
		Vector3 normalized3 = vector3.normalized;
		Vector3 normalized4 = vector4.normalized;
		Transform externalCameraTrackedObjectTransform = _mixedReality.mixedRealityCompositorCamera.externalCameraTrackedObjectTransform;
		float cameraFOV = Vector3.Angle((vector + vector2) * 0.5f, (vector3 + vector4) * 0.5f);
		Vector3 view = externalCameraTrackedObjectTransform.InverseTransformDirection(-((Vector3.Cross((normalized2 - normalized).normalized, (normalized3 - normalized).normalized) + Vector3.Cross((normalized4 - normalized3).normalized, (normalized4 - normalized2).normalized)) * 0.5f).normalized);
		Vector3 up = externalCameraTrackedObjectTransform.InverseTransformDirection(-(((normalized4 - normalized2).normalized + (normalized3 - normalized).normalized) * 0.5f).normalized);
		Quaternion quaternion = default(Quaternion);
		quaternion.SetLookRotation(view, up);
		_mixedRealitySettings.cameraFOV = cameraFOV;
		_mixedRealitySettings.cameraRotationOffset = quaternion.eulerAngles;
		_mixedRealitySettings.cameraPositionOffset = externalCameraTrackedObjectTransform.InverseTransformPoint(internalSettings.cameraPosition);
	}

	public void HideMarkers()
	{
		cameraMarker.enabled = false;
		topLeftMarker.enabled = false;
		topRightMarker.enabled = false;
		bottomLeftMarker.enabled = false;
		bottomRightMarker.enabled = false;
	}

	private bool GetTrackerPosition(out Vector3 position)
	{
		position = Vector3.zero;
		int deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);
		SteamVR_Controller.Device controller = SteamVR_Controller.Input(deviceIndex);
		if (GetDevicePose(controller, out position))
		{
			return true;
		}
		int deviceIndex2 = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Rightmost);
		SteamVR_Controller.Device controller2 = SteamVR_Controller.Input(deviceIndex2);
		if (GetDevicePose(controller2, out position))
		{
			return true;
		}
		return false;
	}

	public bool GetDevicePose(SteamVR_Controller.Device controller, out Vector3 position)
	{
		position = Vector3.zero;
		if (Time.realtimeSinceStartup - lastPressDown > 1f && controller != null && controller.hasTracking)
		{
			if (controller.GetPressDown(EVRButtonId.k_EButton_Axis0))
			{
				lastPressDown = Time.realtimeSinceStartup;
				SteamVR_Utils.RigidTransform rigidTransform = new SteamVR_Utils.RigidTransform(controller.GetPose().mDeviceToAbsoluteTracking);
				position = rigidTransform.pos + rigidTransform.rot * controllerOffset;
				controller.TriggerHapticPulse(800);
				return true;
			}
			controller.TriggerHapticPulse((ushort)Mathf.RoundToInt(controller.velocity.magnitude * 1000f));
		}
		return false;
	}

	private bool GetTrackerContinue()
	{
		if (Time.realtimeSinceStartup - lastTrackerContinute < 1f)
		{
			return false;
		}
		for (int i = 0; i < 15; i++)
		{
			SteamVR_Controller.Device device = SteamVR_Controller.Input(i);
			if (device != null && device.hasTracking && (device.GetPressDown(EVRButtonId.k_EButton_Axis0) || device.GetPressDown(EVRButtonId.k_EButton_Axis0) || device.GetPressDown(EVRButtonId.k_EButton_Grip)))
			{
				lastTrackerContinute = Time.realtimeSinceStartup;
				device.TriggerHapticPulse(800);
				return true;
			}
		}
		return false;
	}
}
