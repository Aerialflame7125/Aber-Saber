using UnityEngine;

public class MixedRealityCameraTrackedObject : MonoBehaviour
{
	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	private int _trackedObjectID;

	private float _smooth = 4f;

	private void Start()
	{
		HandleMixedRealitySettingsDidChangeEvent();
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent += HandleMixedRealitySettingsDidChangeEvent;
	}

	private void OnDestroy()
	{
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent -= HandleMixedRealitySettingsDidChangeEvent;
	}

	private void HandleMixedRealitySettingsDidChangeEvent()
	{
		base.enabled = _mixedRealitySettings.cameraTrackedObjectEnabled;
		if (!base.enabled)
		{
			base.transform.localPosition = new Vector3(0f, 0f, 0f);
			base.transform.localRotation = Quaternion.identity;
		}
		_trackedObjectID = _mixedRealitySettings.cameraTrackedObjectID;
		_smooth = _mixedRealitySettings.cameraTrackedObjectSmooth;
	}

	private void Update()
	{
		VRPlatformHelper.VRNodeTransform nodePos = PersistentSingleton<VRPlatformHelper>.instance.GetNodePos(_trackedObjectID);
		if (_smooth < 0.1f)
		{
			base.transform.localPosition = nodePos.pos;
			base.transform.localRotation = nodePos.rot;
		}
		else
		{
			base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, nodePos.pos, Time.deltaTime * _smooth);
			base.transform.localRotation = Quaternion.Slerp(base.transform.localRotation, nodePos.rot, Time.deltaTime * _smooth);
		}
	}
}
