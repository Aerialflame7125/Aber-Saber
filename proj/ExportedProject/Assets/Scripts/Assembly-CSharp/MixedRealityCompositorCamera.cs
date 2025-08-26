using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MixedRealityCompositorCamera : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(MainCamera))]
	private ObjectProvider _mainCameraProvider;

	[SerializeField]
	private Camera _externalCamera;

	[SerializeField]
	private Camera _overlayCamera;

	[SerializeField]
	private Camera _webCamCamera;

	[Space]
	[SerializeField]
	private GameObject _webCamObjectsWrapper;

	[Space]
	[SerializeField]
	private MixedRealityWebCamManager _mixedRealityWebCamManager;

	[Space]
	[SerializeField]
	private Transform _externalCameraTrackedObjectTransform;

	[SerializeField]
	private Transform _externalCameraWrapperTransform;

	[Space]
	[SerializeField]
	private LayerMask _dontRenderWithNearCamera;

	[Space]
	[SerializeField]
	private Material _keyedHSLMaterial;

	[Space]
	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	private bool _renderEveryFrame;

	private MainCamera _mainCamera;

	private int _frameNumber;

	private MixedRealitySettings.MixedRealityType _activeMixedRealityType;

	private MixedRealityCompositor _compositor;

	public Transform externalCameraTrackedObjectTransform
	{
		get
		{
			return _externalCameraTrackedObjectTransform;
		}
	}

	public Camera overlayCamera
	{
		get
		{
			return _overlayCamera;
		}
	}

	public bool renderEveryFrame
	{
		set
		{
			_renderEveryFrame = value;
			if (_compositor != null)
			{
				Debug.Log("BBB");
				_compositor.renderEveryFrame = _renderEveryFrame;
			}
		}
	}

	private void Start()
	{
		_mainCamera = _mainCameraProvider.GetProvidedObject<MainCamera>();
		Camera component = GetComponent<Camera>();
		component.cullingMask = 0;
		component.clearFlags = CameraClearFlags.Nothing;
		int cullingMask = _externalCamera.cullingMask;
		_externalCamera.CopyFrom(_mainCamera.camera);
		_externalCamera.cullingMask = cullingMask;
		_externalCamera.stereoTargetEye = StereoTargetEyeMask.None;
		_externalCamera.enabled = false;
		int cullingMask2 = _webCamCamera.cullingMask;
		_webCamCamera.CopyFrom(_mainCamera.camera);
		_webCamCamera.clearFlags = CameraClearFlags.Depth;
		_webCamCamera.cullingMask = cullingMask2;
		_webCamCamera.stereoTargetEye = StereoTargetEyeMask.None;
		_webCamCamera.enabled = false;
		_externalCamera.transform.localPosition = Vector3.zero;
		Quaternion localRotation = default(Quaternion);
		localRotation.eulerAngles = new Vector3(0f, 0f, _mixedRealitySettings.additionalRotationOffset);
		_externalCamera.transform.localRotation = localRotation;
		HandleMixedRealitySettingsDidChangeEvent();
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent += HandleMixedRealitySettingsDidChangeEvent;
	}

	private void OnDestroy()
	{
		_compositor.Cleanup();
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent -= HandleMixedRealitySettingsDidChangeEvent;
	}

	private void HandleMixedRealitySettingsDidChangeEvent()
	{
		_externalCamera.fieldOfView = _mixedRealitySettings.cameraFOV + _mixedRealitySettings.cameraFOVOffset;
		_webCamCamera.fieldOfView = _externalCamera.fieldOfView;
		_externalCameraWrapperTransform.localPosition = _mixedRealitySettings.cameraPositionOffset;
		Quaternion localRotation = default(Quaternion);
		localRotation.eulerAngles = _mixedRealitySettings.cameraRotationOffset;
		_externalCameraWrapperTransform.localRotation = localRotation;
		float additionalRotationOffset = _mixedRealitySettings.additionalRotationOffset;
		localRotation = default(Quaternion);
		localRotation.eulerAngles = new Vector3(0f, 0f, additionalRotationOffset);
		_externalCamera.transform.localRotation = localRotation;
		string keyword = "_ORIENTATION_DEFAULT";
		string keyword2 = "_ORIENTATION_RIGHT";
		string keyword3 = "_ORIENTATION_LEFT";
		if (additionalRotationOffset > 89f)
		{
			_keyedHSLMaterial.DisableKeyword(keyword);
			_keyedHSLMaterial.DisableKeyword(keyword3);
			_keyedHSLMaterial.EnableKeyword(keyword2);
		}
		else if (additionalRotationOffset < -89f)
		{
			_keyedHSLMaterial.DisableKeyword(keyword);
			_keyedHSLMaterial.DisableKeyword(keyword2);
			_keyedHSLMaterial.EnableKeyword(keyword3);
		}
		else
		{
			_keyedHSLMaterial.DisableKeyword(keyword2);
			_keyedHSLMaterial.DisableKeyword(keyword3);
			_keyedHSLMaterial.EnableKeyword(keyword);
		}
		_keyedHSLMaterial.SetColor("_KeyColor", _mixedRealitySettings.keyingRGBColor);
		_keyedHSLMaterial.SetFloat("_Threshold", _mixedRealitySettings.keyingThreshold);
		_keyedHSLMaterial.SetFloat("_Smoothness", _mixedRealitySettings.keyingSmoothness);
		_keyedHSLMaterial.SetFloat("_ScreenAspect", (float)Screen.width / (float)Screen.height);
		float num = _mixedRealitySettings.cameraFOV * ((float)Math.PI / 180f);
		float num2 = num + _mixedRealitySettings.cameraFOVOffset * ((float)Math.PI / 180f);
		_keyedHSLMaterial.SetFloat("_Scale", Mathf.Tan(num2 * 0.5f) / Mathf.Tan(num * 0.5f));
		if (_mixedRealitySettings.mixedRealityType == MixedRealitySettings.MixedRealityType.WebCam)
		{
			_mixedRealityWebCamManager.StartUsingCamera();
			_webCamObjectsWrapper.SetActive(true);
		}
		else
		{
			_mixedRealityWebCamManager.StopUsingCamera();
			_webCamObjectsWrapper.SetActive(false);
		}
		RefreshMixedRealityCompositor();
	}

	private void RefreshMixedRealityCompositor()
	{
		MixedRealitySettings.MixedRealityType mixedRealityType = _mixedRealitySettings.mixedRealityType;
		if (_activeMixedRealityType != mixedRealityType)
		{
			_activeMixedRealityType = mixedRealityType;
			if ((bool)_compositor)
			{
				_compositor.Cleanup();
				_compositor = null;
			}
			switch (_activeMixedRealityType)
			{
			case MixedRealitySettings.MixedRealityType.Quadrants:
			{
				MixedRealityQuadrantsCompositor mixedRealityQuadrantsCompositor = new MixedRealityQuadrantsCompositor();
				mixedRealityQuadrantsCompositor.Init(_externalCamera, _overlayCamera, _mainCamera, _dontRenderWithNearCamera);
				_compositor = mixedRealityQuadrantsCompositor;
				break;
			}
			case MixedRealitySettings.MixedRealityType.WebCam:
			{
				MixedRealityWebCamCompositor mixedRealityWebCamCompositor = new MixedRealityWebCamCompositor();
				mixedRealityWebCamCompositor.Init(_externalCamera, _overlayCamera, _webCamCamera, _mainCamera, _dontRenderWithNearCamera, Screen.width, Screen.height, _mixedRealitySettings.webCamCompositorFramesDelay, _mixedRealitySettings.pipPosition, _mixedRealitySettings.pipRelativeSize);
				_compositor = mixedRealityWebCamCompositor;
				break;
			}
			case MixedRealitySettings.MixedRealityType.FarCameraOnly:
			{
				MixedRealityFarCamCompositor mixedRealityFarCamCompositor = new MixedRealityFarCamCompositor();
				mixedRealityFarCamCompositor.Init(_externalCamera, _overlayCamera);
				_compositor = mixedRealityFarCamCompositor;
				break;
			}
			}
			_compositor.renderEveryFrame = _renderEveryFrame;
		}
	}

	private void OnPostRender()
	{
		_frameNumber++;
		_compositor.DoComposition(_frameNumber);
	}
}
