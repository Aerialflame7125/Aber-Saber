using UnityEngine;
using UnityEngine.UI;

public class MixedRealityUIController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(MixedReality))]
	private ObjectProvider _mixedRealityProvider;

	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	[SerializeField]
	private SceneInfo _gameMenuSceneInfo;

	[Space]
	[SerializeField]
	private GameObject _keyingParamsPanel;

	[SerializeField]
	private GameObject _wallParamsPanel;

	[SerializeField]
	private GameObject _cameraTrackedObjectPanel;

	[SerializeField]
	private GameObject _webCamOutputPanel;

	[SerializeField]
	private Toggle _mixedRealityTypeWebCamToggle;

	[SerializeField]
	private Toggle _mixedRealityTypeQuadrantsToggle;

	[SerializeField]
	private Toggle _mixedRealityTypeFarCamToggle;

	[SerializeField]
	private Toggle _enableMRToggle;

	private MixedRealityCompositorCamera _mixedRealityCompositorCamera;

	private GameObject _lastShownPanelGO;

	private void Start()
	{
		_mixedRealityCompositorCamera = _mixedRealityProvider.GetProvidedObject<MixedReality>().mixedRealityCompositorCamera;
		_mixedRealityCompositorCamera.renderEveryFrame = true;
		HandleMixedRealitySettingsDidChangeEvent();
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent += HandleMixedRealitySettingsDidChangeEvent;
	}

	private void OnDestroy()
	{
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent -= HandleMixedRealitySettingsDidChangeEvent;
		_mixedRealitySettings.Save();
	}

	private void HandleMixedRealitySettingsDidChangeEvent()
	{
		switch (_mixedRealitySettings.mixedRealityType)
		{
		case MixedRealitySettings.MixedRealityType.FarCameraOnly:
			_mixedRealityTypeFarCamToggle.isOn = true;
			_mixedRealityTypeWebCamToggle.isOn = false;
			_mixedRealityTypeQuadrantsToggle.isOn = false;
			break;
		case MixedRealitySettings.MixedRealityType.WebCam:
			_mixedRealityTypeWebCamToggle.isOn = true;
			_mixedRealityTypeFarCamToggle.isOn = false;
			_mixedRealityTypeQuadrantsToggle.isOn = false;
			break;
		case MixedRealitySettings.MixedRealityType.Quadrants:
			_mixedRealityTypeQuadrantsToggle.isOn = true;
			_mixedRealityTypeFarCamToggle.isOn = false;
			_mixedRealityTypeWebCamToggle.isOn = false;
			break;
		}
		_enableMRToggle.isOn = _mixedRealitySettings.enableMixedReality;
	}

	public void WebCamButtonPressed()
	{
		SwitchPanel(_webCamOutputPanel);
	}

	public void KeyingParamsButtonPressed()
	{
		SwitchPanel(_keyingParamsPanel);
	}

	public void ExternalCameraPropertiesButtonPressed()
	{
		SwitchPanel(_cameraTrackedObjectPanel);
	}

	public void WallParamsButtonPressed()
	{
		SwitchPanel(_wallParamsPanel);
	}

	private void SwitchPanel(GameObject panel)
	{
		if (_lastShownPanelGO != panel && _lastShownPanelGO != null)
		{
			_lastShownPanelGO.SetActive(value: false);
		}
		panel.SetActive(!panel.activeSelf);
		if (panel.activeSelf)
		{
			_lastShownPanelGO = panel;
		}
		else
		{
			_lastShownPanelGO = null;
		}
	}

	public void MixedRealityTypeToggleValueChanged(Toggle toggle)
	{
		if (toggle.isOn)
		{
			if (toggle == _mixedRealityTypeFarCamToggle)
			{
				_mixedRealitySettings.mixedRealityType = MixedRealitySettings.MixedRealityType.FarCameraOnly;
			}
			else if (toggle == _mixedRealityTypeQuadrantsToggle)
			{
				_mixedRealitySettings.mixedRealityType = MixedRealitySettings.MixedRealityType.Quadrants;
			}
			else if (toggle == _mixedRealityTypeWebCamToggle)
			{
				_mixedRealitySettings.mixedRealityType = MixedRealitySettings.MixedRealityType.WebCam;
			}
		}
	}

	public void EnableMRToggleValueChanged(Toggle toggle)
	{
		_mixedRealitySettings.enableMixedReality = toggle.isOn;
	}

	public void BackToGameMenuButtonPressed()
	{
		_gameMenuSceneInfo.TransitionToScene(0f);
	}
}
