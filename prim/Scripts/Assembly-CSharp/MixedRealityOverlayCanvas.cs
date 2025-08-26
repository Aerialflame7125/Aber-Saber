using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class MixedRealityOverlayCanvas : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(MixedReality))]
	private ObjectProvider _mixedRealityProvider;

	[SerializeField]
	private MixedRealitySettings _mixedRealitySettings;

	private void Start()
	{
		if ((bool)_mixedRealityProvider.GetProvidedObject<MixedReality>() && _mixedRealityProvider.GetProvidedObject<MixedReality>().isMixedRealityEnabled)
		{
			Canvas component = GetComponent<Canvas>();
			component.worldCamera = _mixedRealityProvider.GetProvidedObject<MixedReality>().mixedRealityCompositorCamera.overlayCamera;
			component.renderMode = RenderMode.ScreenSpaceCamera;
			if (_mixedRealitySettings.mixedRealityType == MixedRealitySettings.MixedRealityType.WebCam)
			{
				GetComponent<CanvasScaler>().referenceResolution /= 2f;
			}
		}
		else
		{
			base.gameObject.SetActive(value: false);
		}
	}
}
