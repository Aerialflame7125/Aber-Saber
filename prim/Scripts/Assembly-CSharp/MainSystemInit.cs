using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.XR;

public class MainSystemInit : MonoBehaviour
{
	[SerializeField]
	private InitSceneSetupData _initSceneSetupData;

	[SerializeField]
	private MenuSceneSetupData _menuSceneSetupData;

	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	[SerializeField]
	private AudioMixer _mainAudioMixer;

	[SerializeField]
	private MirrorRendererGraphicsSettingsPresets _mirrorRendererGraphicsSettingsPresets;

	[SerializeField]
	private MainEffectGraphicsSettingsPresets _mainEffectGraphicsSettingsPresets;

	[SerializeField]
	private BloomPrePassGraphicsSettingsPresets _bloomGraphicsSettingsPresets;

	[SerializeField]
	private MirrorRenderer _mirrorRenderer;

	[SerializeField]
	private MainEffectParams _mainEffectParams;

	[SerializeField]
	private BloomPrePassParams _bloomPrePassParams;

	[SerializeField]
	private SmokeParams _smokeParams;

	private void Awake()
	{
		_mainSettingsModel.LoadIfNeeded();
		UnityEngine.Application.backgroundLoadingPriority = ThreadPriority.Low;
		_mainAudioMixer.SetFloat("MainVolume", Mathf.Log(_mainSettingsModel.volume, 1.1f));
		Vector2Int windowResolution = _mainSettingsModel.windowResolution;
		if (Screen.width != windowResolution.x || Screen.height != windowResolution.y)
		{
			Screen.SetResolution(windowResolution.x, windowResolution.y, fullscreen: false);
		}
		Screen.fullScreen = _mainSettingsModel.windowMode == MainSettingsModel.WindowMode.Fullscreen;
		QualitySettings.antiAliasing = _mainSettingsModel.antiAliasingLevel;
		XRSettings.renderViewportScale = 1f;
		XRSettings.eyeTextureResolutionScale = _mainSettingsModel.vrResolutionScale;
		XRDevice.SetTrackingSpaceType(TrackingSpaceType.RoomScale);
		MirrorRendererGraphicsSettingsPresets.Preset preset = _mirrorRendererGraphicsSettingsPresets.presets[_mainSettingsModel.mirrorGraphicsSettings];
		MainEffectGraphicsSettingsPresets.Preset preset2 = _mainEffectGraphicsSettingsPresets.presets[_mainSettingsModel.mainEffectGraphicsSettings];
		BloomPrePassGraphicsSettingsPresets.Preset preset3 = _bloomGraphicsSettingsPresets.presets[_mainSettingsModel.bloomGraphicsSettings];
		bool flag = _mainSettingsModel.smokeGraphicsSettings != 0;
		_mirrorRenderer.InitFromPreset(preset);
		_mainEffectParams.InitFromPreset(preset2);
		_mainEffectParams.depthTextureEnabled = flag;
		_bloomPrePassParams.InitFromPreset(preset3);
		_smokeParams.Init(flag);
		UnityEngine.Application.targetFrameRate = -1;
		UnityEngine.Application.runInBackground = true;
		QualitySettings.maxQueuedFrames = -1;
		QualitySettings.vSyncCount = 0;
		try
		{
			Core.AsyncInitialize();
			Entitlements.IsUserEntitledToApplication().OnComplete(delegate(Message message)
			{
				if (message.IsError)
				{
					Error error = message.GetError();
					Debug.LogWarning("Oculus user entitlement error: " + error.Message);
					Debug.LogError("Oculus user entitlement check failed. You are NOT entitled to use this app.");
					UnityEngine.Application.Quit();
				}
				else
				{
					TransitionToMenu();
				}
			});
		}
		catch (UnityException exception)
		{
			Debug.LogError("Oculus platform failed to initialize due to exception.");
			Debug.LogException(exception);
			UnityEngine.Application.Quit();
		}
	}

	private void TransitionToMenu()
	{
		_menuSceneSetupData.TransitionToScene(0f);
	}
}
