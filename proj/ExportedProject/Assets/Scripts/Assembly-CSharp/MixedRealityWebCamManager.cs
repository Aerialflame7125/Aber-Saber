using System.Collections.Generic;
using UnityEngine;

public class MixedRealityWebCamManager : ScriptableObject
{
	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	[SerializeField]
	private GameObject _mixedRealityWebCamPrefab;

	private AVProLiveCamera _proLiveCamera;

	private GameObject _mixedRealityWebCamGO;

	public AVProLiveCamera proLiveCamera
	{
		get
		{
			return _proLiveCamera;
		}
	}

	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
		_mixedRealitySettings.mixedRealityCameraDeviceSettingsDidChangeEvent += HandleMixedRealityCameraDeviceSettingsDidChange;
	}

	private void OnDisable()
	{
		_mixedRealitySettings.mixedRealityCameraDeviceSettingsDidChangeEvent -= HandleMixedRealityCameraDeviceSettingsDidChange;
		if ((bool)_mixedRealityWebCamGO)
		{
			Object.Destroy(_mixedRealityWebCamGO);
		}
	}

	public void StartUsingCamera()
	{
		if (!(_mixedRealityWebCamGO != null))
		{
			_mixedRealityWebCamGO = Object.Instantiate(_mixedRealityWebCamPrefab);
			Object.DontDestroyOnLoad(_mixedRealityWebCamGO);
			_proLiveCamera = _mixedRealityWebCamGO.GetComponent<AVProLiveCamera>();
			RefreshCameraDeviceAndMode();
		}
	}

	public void StopUsingCamera()
	{
		if (_mixedRealityWebCamGO != null)
		{
			Object.Destroy(_mixedRealityWebCamGO);
			_mixedRealityWebCamGO = null;
		}
		if (_proLiveCamera != null)
		{
			if (_proLiveCamera.Device != null)
			{
				_proLiveCamera.Device.Close();
			}
			_proLiveCamera = null;
		}
	}

	public void ShowCameraDeviceConfig()
	{
		_proLiveCamera.Device.ShowConfigWindow();
	}

	private void RefreshCameraDeviceAndMode()
	{
		if (!(_mixedRealityWebCamGO == null))
		{
			string webCamName = _mixedRealitySettings.webCamName;
			_proLiveCamera._modeSelection = AVProLiveCamera.SelectModeBy.Index;
			_proLiveCamera._deviceSelection = AVProLiveCamera.SelectDeviceBy.Name;
			_proLiveCamera._desiredModeIndex = _mixedRealitySettings.webCamModeIdx;
			List<string> list = new List<string>();
			list.Add(webCamName);
			_proLiveCamera._desiredDeviceNames = list;
			_proLiveCamera._flipY = _mixedRealitySettings.webCamFlipY;
			_proLiveCamera.Begin();
			_proLiveCamera.SelectDeviceAndMode();
		}
	}

	private void HandleMixedRealityCameraDeviceSettingsDidChange()
	{
		RefreshCameraDeviceAndMode();
	}
}
