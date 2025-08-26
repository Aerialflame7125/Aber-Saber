using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraDeviceSetupViewController : MonoBehaviour
{
	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	[SerializeField]
	private MixedRealityWebCamManager _mixedRealityWebCamManager;

	[Space]
	[SerializeField]
	private GameObject _mixedRelityCalibration;

	[SerializeField]
	private AVProLiveCameraUGUIComponent _webCamPreview;

	[SerializeField]
	private Slider _webCamOutputTransparencySlider;

	[SerializeField]
	private Dropdown _cameraDevicesDropDown;

	[SerializeField]
	private Dropdown _modesDropDown;

	[SerializeField]
	private Toggle _flipYToggle;

	[SerializeField]
	private InputField _latencyInputField;

	private AVProLiveCameraDevice _selectedCameraDevice;

	private void Start()
	{
		_mixedRelityCalibration.SetActive(value: false);
		RefreshDevicesAndModes();
		_flipYToggle.isOn = _mixedRealitySettings.webCamFlipY;
		_latencyInputField.text = _mixedRealitySettings.webCamCompositorFramesDelay.ToString();
	}

	private void OnEnable()
	{
		_mixedRealityWebCamManager.StartUsingCamera();
		_webCamPreview.m_liveCamera = _mixedRealityWebCamManager.proLiveCamera;
	}

	private void RefreshDevicesAndModes()
	{
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		int value = -1;
		for (int i = 0; i < AVProLiveCameraManager.Instance.NumDevices; i++)
		{
			AVProLiveCameraDevice device = AVProLiveCameraManager.Instance.GetDevice(i);
			Dropdown.OptionData optionData = new Dropdown.OptionData(device.Name);
			list.Add(optionData);
			if (optionData.text == _mixedRealitySettings.webCamName)
			{
				_selectedCameraDevice = device;
				value = i;
			}
		}
		_cameraDevicesDropDown.options = list;
		if (_selectedCameraDevice != null)
		{
			_cameraDevicesDropDown.value = value;
			RefreshModesForDevice(_selectedCameraDevice);
		}
		else if (AVProLiveCameraManager.Instance.NumDevices > 0)
		{
			_selectedCameraDevice = AVProLiveCameraManager.Instance.GetDevice(0);
			_mixedRealitySettings.webCamName = _selectedCameraDevice.Name;
			_mixedRealitySettings.webCamModeIdx = 0;
			RefreshModesForDevice(_selectedCameraDevice);
		}
		else
		{
			RefreshModesForDevice(null);
		}
	}

	private void RefreshModesForDevice(AVProLiveCameraDevice device)
	{
		if (device == null)
		{
			_modesDropDown.options = new List<Dropdown.OptionData>();
			return;
		}
		int num = _mixedRealitySettings.webCamModeIdx;
		if (num >= device.NumModes)
		{
			_mixedRealitySettings.webCamModeIdx = 0;
			num = 0;
		}
		List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
		for (int i = 0; i < device.NumModes; i++)
		{
			AVProLiveCameraDeviceMode mode = device.GetMode(i);
			string text = $"{mode.Width}x{mode.Height}:{mode.FPS:0} {mode.Format}";
			Dropdown.OptionData item = new Dropdown.OptionData(text);
			list.Add(item);
		}
		_modesDropDown.options = list;
		_modesDropDown.value = num;
	}

	public void SliderValueChanged(Slider slider)
	{
		if (slider == _webCamOutputTransparencySlider)
		{
			_webCamPreview.color = new Color(1f, 1f, 1f, slider.value);
		}
	}

	public void DevicesDropDownValueChanged(int value)
	{
		AVProLiveCameraDevice device = AVProLiveCameraManager.Instance.GetDevice(value);
		if (_mixedRealitySettings.webCamName != device.Name)
		{
			_mixedRealitySettings.webCamName = device.Name;
			_mixedRealitySettings.webCamModeIdx = 0;
			RefreshModesForDevice(device);
		}
	}

	public void ModesDropDownValueChanged(int value)
	{
		_mixedRealitySettings.webCamModeIdx = value;
	}

	public void FlipYToggleValueChanged(Toggle toggle)
	{
		_mixedRealitySettings.webCamFlipY = toggle.isOn;
	}

	public void CalibrationButtonPressed()
	{
		_mixedRelityCalibration.SetActive(!_mixedRelityCalibration.activeSelf);
	}

	public void LatencyInputFieldOnEndEdit(string value)
	{
		int result = 0;
		if (int.TryParse(value, out result))
		{
			_mixedRealitySettings.webCamCompositorFramesDelay = result;
		}
	}

	public void RefreshButtonPressed()
	{
		RefreshDevicesAndModes();
	}

	public void ConfigButtonPressed()
	{
		_mixedRealityWebCamManager.ShowCameraDeviceConfig();
	}
}
