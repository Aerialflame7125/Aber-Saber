using UnityEngine;
using UnityEngine.UI;

public class ExternalCameraTrackingViewController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(MixedReality))]
	private ObjectProvider _mixedRealityProvider;

	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	[Space]
	[SerializeField]
	private Text _trackedObjectXPosText;

	[SerializeField]
	private Text _trackedObjectYPosText;

	[SerializeField]
	private Text _trackedObjectZPosText;

	[Space]
	[SerializeField]
	private Toggle _trackedObjectEnabledToggle;

	[SerializeField]
	private Button _nodeDecButton;

	[SerializeField]
	private Button _nodeIncButton;

	[SerializeField]
	private Text _nodeText;

	[Space]
	[SerializeField]
	private Text _cameraFOVOffsetText;

	[SerializeField]
	private Slider _cameraFOVOffsetSlider;

	[SerializeField]
	private Text _additionalRotationOffsetText;

	[SerializeField]
	private Slider _additionalRotationOffsetSlider;

	[SerializeField]
	private Text _trackedObjectSmoothText;

	[SerializeField]
	private Slider _trackedObjectSmoothSlider;

	[Space]
	[SerializeField]
	private Text _cameraFOVText;

	[SerializeField]
	private Slider _cameraFOVSlider;

	[SerializeField]
	private Text _cameraPositionOffsetText;

	[SerializeField]
	private Text _cameraRotationOffsetText;

	[SerializeField]
	private Slider _cameraRotationXOffsetSlider;

	[SerializeField]
	private Slider _cameraRotationYOffsetSlider;

	[SerializeField]
	private Slider _cameraRotationZOffsetSlider;

	private MixedRealityCompositorCamera _mixedRealityCompositorCamera;

	private void Start()
	{
		_mixedRealityCompositorCamera = _mixedRealityProvider.GetProvidedObject<MixedReality>().mixedRealityCompositorCamera;
		RefreshUI();
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent += HandleMixedRealitySettingsDidChangeEvent;
	}

	private void OnDestroy()
	{
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent -= HandleMixedRealitySettingsDidChangeEvent;
	}

	private void Update()
	{
		Vector3 position = _mixedRealityCompositorCamera.externalCameraTrackedObjectTransform.position;
		_trackedObjectXPosText.text = $"{position.x:0.0#}";
		_trackedObjectYPosText.text = $"{position.y:0.0#}";
		_trackedObjectZPosText.text = $"{position.z:0.0#}";
	}

	private void HandleMixedRealitySettingsDidChangeEvent()
	{
		RefreshUI();
	}

	private void RefreshUI()
	{
		RefreshTrackingNodeControls();
		_trackedObjectSmoothText.text = $"Smooth: {_mixedRealitySettings.cameraTrackedObjectSmooth:0.0#}";
		_trackedObjectSmoothSlider.value = _mixedRealitySettings.cameraTrackedObjectSmooth;
		Vector3 cameraPositionOffset = _mixedRealitySettings.cameraPositionOffset;
		_cameraPositionOffsetText.text = $"Position Offset\nX: {cameraPositionOffset.x:#,0.0000}   Y: {cameraPositionOffset.y:#,0.0000}   Z: {cameraPositionOffset.z:#,0.0000}";
		Vector3 cameraRotationOffset = _mixedRealitySettings.cameraRotationOffset;
		_cameraRotationOffsetText.text = $"Rotation Offset\nX: {cameraRotationOffset.x:#,0.0000}   Y: {cameraRotationOffset.y:#,0.0000}   Z: {cameraRotationOffset.z:#,0.0000}";
		_cameraRotationXOffsetSlider.value = cameraRotationOffset.x;
		_cameraRotationYOffsetSlider.value = cameraRotationOffset.y;
		_cameraRotationZOffsetSlider.value = cameraRotationOffset.z;
		_cameraFOVText.text = $"FOV: {_mixedRealitySettings.cameraFOV:#,0.0}";
		_cameraFOVSlider.value = _mixedRealitySettings.cameraFOV;
		_cameraFOVOffsetSlider.value = _mixedRealitySettings.cameraFOVOffset;
		_cameraFOVOffsetText.text = $"FOV Offset: {_mixedRealitySettings.cameraFOVOffset:0.0#}";
		_additionalRotationOffsetSlider.value = _mixedRealitySettings.additionalRotationOffset / 90f;
		_additionalRotationOffsetText.text = $"Add Rot Offset: {_mixedRealitySettings.additionalRotationOffset:0.0#}";
		_trackedObjectEnabledToggle.isOn = _mixedRealitySettings.cameraTrackedObjectEnabled;
	}

	private void RefreshTrackingNodeControls()
	{
		int cameraTrackedObjectID = _mixedRealitySettings.cameraTrackedObjectID;
		_nodeDecButton.interactable = cameraTrackedObjectID > 0;
		_nodeIncButton.interactable = cameraTrackedObjectID < 16;
		_nodeText.text = cameraTrackedObjectID.ToString();
	}

	public void NodeDecButtonPressed()
	{
		int num = _mixedRealitySettings.cameraTrackedObjectID;
		if (num > 0)
		{
			num--;
		}
		_mixedRealitySettings.cameraTrackedObjectID = num;
	}

	public void NodeIncButtonPressed()
	{
		int num = _mixedRealitySettings.cameraTrackedObjectID;
		if (num < 15)
		{
			num++;
		}
		_mixedRealitySettings.cameraTrackedObjectID = num;
	}

	public void EnableTrackingToggleValueChanged(Toggle toggle)
	{
		_mixedRealitySettings.cameraTrackedObjectEnabled = toggle.isOn;
	}

	public void CameraPositionOffsetButtonPressed(string type)
	{
		float num = 0.003f;
		Vector3 cameraPositionOffset = _mixedRealitySettings.cameraPositionOffset;
		switch (type)
		{
		case "x+":
			cameraPositionOffset.x += num;
			break;
		case "x-":
			cameraPositionOffset.x -= num;
			break;
		case "y+":
			cameraPositionOffset.y += num;
			break;
		case "y-":
			cameraPositionOffset.y -= num;
			break;
		case "z+":
			cameraPositionOffset.z += num;
			break;
		case "z-":
			cameraPositionOffset.z -= num;
			break;
		}
		_mixedRealitySettings.cameraPositionOffset = cameraPositionOffset;
	}

	public void SliderValueChanged(Slider slider)
	{
		if (slider == _cameraFOVOffsetSlider)
		{
			_mixedRealitySettings.cameraFOVOffset = slider.value;
		}
		else if (slider == _additionalRotationOffsetSlider)
		{
			_mixedRealitySettings.additionalRotationOffset = slider.value * 90f;
		}
		else if (slider == _trackedObjectSmoothSlider)
		{
			_mixedRealitySettings.cameraTrackedObjectSmooth = slider.value;
		}
		else if (slider == _cameraRotationXOffsetSlider || slider == _cameraRotationYOffsetSlider || slider == _cameraRotationZOffsetSlider)
		{
			_mixedRealitySettings.cameraRotationOffset = new Vector3(_cameraRotationXOffsetSlider.value, _cameraRotationYOffsetSlider.value, _cameraRotationZOffsetSlider.value);
		}
		else if (slider == _cameraFOVSlider)
		{
			_mixedRealitySettings.cameraFOV = slider.value;
		}
	}
}
