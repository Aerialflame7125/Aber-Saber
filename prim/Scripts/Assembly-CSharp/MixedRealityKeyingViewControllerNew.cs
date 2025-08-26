using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MixedRealityKeyingViewControllerNew : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
{
	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	[SerializeField]
	private MixedRealityWebCamManager _mixedRealityWebCamManager;

	[Space]
	[SerializeField]
	private AVProLiveCameraUGUIComponent _webCamPreview;

	[SerializeField]
	private InputField _colorRInputField;

	[SerializeField]
	private InputField _colorGInputField;

	[SerializeField]
	private InputField _colorBInputField;

	[SerializeField]
	private Slider _thresholdSlider;

	[SerializeField]
	private Slider _smoothnessSlider;

	private void Start()
	{
		HandleMixedRealitySettingsDidChangeEvent();
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent += HandleMixedRealitySettingsDidChangeEvent;
	}

	private void OnEnable()
	{
		_mixedRealityWebCamManager.StartUsingCamera();
		_webCamPreview.m_liveCamera = _mixedRealityWebCamManager.proLiveCamera;
	}

	private void OnDestroy()
	{
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent -= HandleMixedRealitySettingsDidChangeEvent;
	}

	private void HandleMixedRealitySettingsDidChangeEvent()
	{
		_thresholdSlider.value = _mixedRealitySettings.keyingThreshold;
		_smoothnessSlider.value = _mixedRealitySettings.keyingSmoothness;
		Color keyingRGBColor = _mixedRealitySettings.keyingRGBColor;
		_colorRInputField.text = Mathf.RoundToInt(keyingRGBColor.r * 255f).ToString();
		_colorGInputField.text = Mathf.RoundToInt(keyingRGBColor.g * 255f).ToString();
		_colorBInputField.text = Mathf.RoundToInt(keyingRGBColor.b * 255f).ToString();
		_webCamPreview.material.SetColor("_KeyColor", keyingRGBColor);
		_webCamPreview.material.SetFloat("_Threshold", _mixedRealitySettings.keyingThreshold);
		_webCamPreview.material.SetFloat("_Smoothness", _mixedRealitySettings.keyingSmoothness);
	}

	public void KeyingSliderValueChanged(Slider slider)
	{
		if (slider == _smoothnessSlider)
		{
			_mixedRealitySettings.keyingSmoothness = slider.value;
		}
		else if (slider == _thresholdSlider)
		{
			_mixedRealitySettings.keyingThreshold = slider.value;
		}
	}

	public void ColorInputFieldOnEndEdit(InputField inputField)
	{
		Color keyingRGBColor = _mixedRealitySettings.keyingRGBColor;
		if (inputField == _colorRInputField)
		{
			keyingRGBColor.r = (float)int.Parse(inputField.text) / 255f;
		}
		else if (inputField == _colorGInputField)
		{
			keyingRGBColor.g = (float)int.Parse(inputField.text) / 255f;
		}
		else if (inputField == _colorBInputField)
		{
			keyingRGBColor.b = (float)int.Parse(inputField.text) / 255f;
		}
		_mixedRealitySettings.keyingRGBColor = keyingRGBColor;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Vector3 mousePosition = Input.mousePosition;
		Vector2 vector = new Vector2(mousePosition.x / (float)Screen.width, 1f - mousePosition.y / (float)Screen.height);
		RenderTexture renderTexture = _webCamPreview.mainTexture as RenderTexture;
		Graphics.SetRenderTarget(renderTexture);
		Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, mipmap: false);
		texture2D.ReadPixels(new Rect(0f, 0f, renderTexture.width, renderTexture.height), 0, 0, recalculateMipMaps: false);
		texture2D.Apply();
		_mixedRealitySettings.keyingRGBColor = texture2D.GetPixel(Mathf.RoundToInt(vector.x * (float)renderTexture.width), Mathf.RoundToInt(vector.y * (float)renderTexture.height));
		Object.Destroy(texture2D);
	}
}
