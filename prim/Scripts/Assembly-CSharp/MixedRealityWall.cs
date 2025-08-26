using UnityEngine;

public class MixedRealityWall : MonoBehaviour
{
	[SerializeField]
	private Transform _frontTransfrom;

	[SerializeField]
	private Transform _floorTransfrom;

	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	private void OnEnable()
	{
		HandleMixedRealitySettingsDidChangeEvent();
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent += HandleMixedRealitySettingsDidChangeEvent;
	}

	private void OnDisable()
	{
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent -= HandleMixedRealitySettingsDidChangeEvent;
	}

	private void HandleMixedRealitySettingsDidChangeEvent()
	{
		base.transform.localPosition = _mixedRealitySettings.wallPosition;
		base.transform.eulerAngles = new Vector3(0f, _mixedRealitySettings.wallRotationY, 0f);
		Vector3 wallSize = _mixedRealitySettings.wallSize;
		base.transform.localScale = new Vector3(wallSize.x, wallSize.y, wallSize.z);
	}
}
