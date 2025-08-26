using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(OnWillRenderObjectTrigger))]
public class BlueNoiseDitheringUpdater : MonoBehaviour, CameraRenderCallbacksManager.ICameraRenderCallbacks
{
	[SerializeField]
	private BlueNoiseDithering _blueNoiseDithering;

	[SerializeField]
	private RandomValueToShader _randomValueToShader;

	private void Awake()
	{
	}

	private void OnDisable()
	{
		CameraRenderCallbacksManager.UnregisterFromCameraCallbacks(this);
	}

	private void OnWillRenderObject()
	{
		CameraRenderCallbacksManager.RegisterForCameraCallbacks(Camera.current, this);
	}

	private void OnBecameInvisible()
	{
		CameraRenderCallbacksManager.UnregisterFromCameraCallbacks(this);
	}

	public void OnCameraPreRender(Camera camera)
	{
		_randomValueToShader.SetRandomValueToShaders();
		_blueNoiseDithering.SetBlueNoiseShaderParams(camera.pixelWidth, camera.pixelHeight);
	}

	public void OnCameraPostRender(Camera camera)
	{
	}
}
