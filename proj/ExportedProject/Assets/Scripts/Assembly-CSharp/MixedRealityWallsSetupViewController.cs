using UnityEngine;
using UnityEngine.UI;

public class MixedRealityWallsSetupViewController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(MixedReality))]
	private ObjectProvider _mixedRealityProvider;

	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	[Space]
	[SerializeField]
	private Text _wallPosXText;

	[SerializeField]
	private Text _wallPosZText;

	[SerializeField]
	private Slider _wallRotYSlider;

	[SerializeField]
	private Slider _wallSizeXSlider;

	[SerializeField]
	private Slider _wallSizeYSlider;

	[SerializeField]
	private Slider _wallSizeZSlider;

	[SerializeField]
	private Text _wallMeasureButtonText;

	[SerializeField]
	private Text _wallMeasureInfoText;

	[SerializeField]
	private GameObject _visibleWall;

	[SerializeField]
	private VRController _leftController;

	[SerializeField]
	private VRController _rightController;

	private bool _measuringWall;

	private VRController _measuringLeftController;

	private VRController _measuringRightController;

	private void Start()
	{
		HandleMixedRealitySettingsDidChangeEvent();
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent += HandleMixedRealitySettingsDidChangeEvent;
	}

	private void OnEnable()
	{
		_measuringWall = false;
		_measuringLeftController = null;
		_measuringRightController = null;
	}

	private void Update()
	{
		if (!_measuringWall)
		{
			return;
		}
		if (_measuringLeftController == null || _measuringRightController == null)
		{
			VRController vRController = null;
			if (_leftController.triggerValue > 0.5f)
			{
				vRController = _leftController;
			}
			else if (_rightController.triggerValue > 0.5f)
			{
				vRController = _rightController;
			}
			if (vRController != null)
			{
				if (_measuringLeftController == null)
				{
					_wallMeasureInfoText.text = "PULL A TRIGGER ON RIGHT CONTROLLER";
					_measuringLeftController = vRController;
				}
				else if (_measuringRightController == null && vRController != _measuringLeftController)
				{
					_wallMeasureInfoText.text = "PUT CONTROLLERS CLOSE TO THE WALL\nAND\nPULL BOTH TRIGGERS TO MEASURE";
					_measuringRightController = vRController;
				}
			}
		}
		bool flag = false;
		bool flag2 = false;
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		if (_measuringLeftController != null && _measuringLeftController.gameObject.activeInHierarchy)
		{
			flag = _measuringLeftController.triggerValue > 0.5f;
			vector = _measuringLeftController.transform.position;
		}
		if (_measuringRightController != null && _measuringRightController.active)
		{
			flag2 = _measuringRightController.triggerValue > 0.5f;
			vector2 = _measuringRightController.transform.position;
		}
		if (flag && flag2)
		{
			_measuringWall = false;
			_wallMeasureButtonText.text = "MEASURE";
			_wallMeasureInfoText.text = string.Empty;
			Vector3 wallPosition = (vector + vector2) * 0.5f;
			wallPosition.y = 0f;
			_mixedRealitySettings.wallPosition = wallPosition;
			_mixedRealitySettings.wallRotationY = Mathf.Atan2(vector.z - vector2.z, 0f - vector.x + vector2.x) * 57.29578f + 180f;
		}
	}

	private void OnDestroy()
	{
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent -= HandleMixedRealitySettingsDidChangeEvent;
	}

	private void HandleMixedRealitySettingsDidChangeEvent()
	{
		Vector3 wallPosition = _mixedRealitySettings.wallPosition;
		_wallPosXText.text = string.Format("X: {0:#,0.00}", wallPosition.x);
		_wallPosZText.text = string.Format("Z: {0:#,0.00}", wallPosition.z);
		_wallRotYSlider.value = _mixedRealitySettings.wallRotationY;
		Vector3 wallSize = _mixedRealitySettings.wallSize;
		_wallSizeXSlider.value = wallSize.x;
		_wallSizeYSlider.value = wallSize.y;
		_wallSizeZSlider.value = wallSize.z;
	}

	public void EnableVisibleWallToggleValueChanged(Toggle toggle)
	{
		_visibleWall.SetActive(toggle.isOn);
	}

	public void WallPositionButtonPressed(string type)
	{
		float num = 0.1f;
		Vector3 wallPosition = _mixedRealitySettings.wallPosition;
		switch (type)
		{
		case "x+":
			wallPosition.x += num;
			break;
		case "x-":
			wallPosition.x -= num;
			break;
		case "y+":
			wallPosition.y += num;
			break;
		case "y-":
			wallPosition.y -= num;
			break;
		case "z+":
			wallPosition.z += num;
			break;
		case "z-":
			wallPosition.z -= num;
			break;
		}
		_mixedRealitySettings.wallPosition = wallPosition;
	}

	public void SliderValueChanged(Slider slider)
	{
		if (slider == _wallRotYSlider)
		{
			_mixedRealitySettings.wallRotationY = slider.value;
		}
		else if (slider == _wallSizeXSlider)
		{
			Vector3 wallSize = _mixedRealitySettings.wallSize;
			wallSize.x = slider.value;
			_mixedRealitySettings.wallSize = wallSize;
		}
		else if (slider == _wallSizeYSlider)
		{
			Vector3 wallSize2 = _mixedRealitySettings.wallSize;
			wallSize2.y = slider.value;
			_mixedRealitySettings.wallSize = wallSize2;
		}
		else if (slider == _wallSizeZSlider)
		{
			Vector3 wallSize3 = _mixedRealitySettings.wallSize;
			wallSize3.z = slider.value;
			_mixedRealitySettings.wallSize = wallSize3;
		}
	}

	public void MeasureWallButtonPressed()
	{
		_measuringWall = !_measuringWall;
		if (_measuringWall)
		{
			_measuringLeftController = null;
			_measuringRightController = null;
			_wallMeasureButtonText.text = "CANCEL";
			_wallMeasureInfoText.text = "PULL A TRIGGER ON LEFT CONTROLLER";
		}
		else
		{
			_wallMeasureButtonText.text = "MEASURE";
			_wallMeasureInfoText.text = string.Empty;
		}
	}
}
