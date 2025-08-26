using UnityEngine;
using UnityEngine.XR;

public class MixedReality : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(MainCamera))]
	private ObjectProvider _mainCameraProvider;

	[SerializeField]
	private SceneInfo _mixedRealitySetupSceneInfo;

	[Space]
	[SerializeField]
	private GameObject[] _gameObjects;

	[SerializeField]
	private MixedRealityCompositorCamera _mixedRealityCompositorCamera;

	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	private MainCamera _mainCamera;

	public MixedRealityCompositorCamera mixedRealityCompositorCamera => _mixedRealityCompositorCamera;

	public bool isMixedRealityEnabled => _mixedRealitySettings.enableMixedReality;

	private void Awake()
	{
		_mainCamera = _mainCameraProvider.GetProvidedObject<MainCamera>();
		GameObject[] gameObjects = _gameObjects;
		foreach (GameObject gameObject in gameObjects)
		{
		}
		GameObject[] gameObjects2 = _gameObjects;
		foreach (GameObject gameObject2 in gameObjects2)
		{
			gameObject2.SetActive(_mixedRealitySettings.enableMixedReality);
		}
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent += HandleMixedRealitySettingsDidChangeEvent;
	}

	private void Start()
	{
		HandleMixedRealitySettingsDidChangeEvent();
	}

	private void OnDestroy()
	{
		_mixedRealitySettings.mixedRealityBasicSettingsDidChangeEvent -= HandleMixedRealitySettingsDidChangeEvent;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.F11) && Input.GetKey(KeyCode.LeftControl))
		{
			_mixedRealitySetupSceneInfo.TransitionToScene(0f);
		}
	}

	private void HandleMixedRealitySettingsDidChangeEvent()
	{
		GameObject[] gameObjects = _gameObjects;
		foreach (GameObject gameObject in gameObjects)
		{
			gameObject.SetActive(_mixedRealitySettings.enableMixedReality);
		}
		_mainCamera.mainEffect.grabFinalImage = _mixedRealitySettings.enableMixedReality;
		XRSettings.showDeviceView = !_mixedRealitySettings.enableMixedReality;
	}
}
