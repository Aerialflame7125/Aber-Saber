using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OnWillRenderObjectTrigger))]
[ExecuteInEditMode]
public class BloomPrePass : MonoBehaviour, CameraRenderCallbacksManager.ICameraRenderCallbacks
{
	[SerializeField]
	private BloomPrePassRenderer _bloomPrepassRenderer;

	[SerializeField]
	private KawaseBlurRenderer _kawaseBlurRenderer;

	[SerializeField]
	private BloomFog _bloomFog;

	[SerializeField]
	private BloomPrePassParams _params;

	private int _bloomPrePassTextureID;

	private int _customFogTextureToScreenRatioID;

	private int _stereoCameraEyeOffsetID;

	private RenderTexture _bloomPrePassRenderTexture;

	private void Awake()
	{
		_bloomPrePassRenderTexture = new RenderTexture(_params.textureWidth, _params.textureHeight, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
		_bloomPrePassRenderTexture.name = "BloomRenderTexture";
		_bloomPrePassTextureID = Shader.PropertyToID("_BloomPrePassTexture");
		_stereoCameraEyeOffsetID = Shader.PropertyToID("_StereoCameraEyeOffset");
		_customFogTextureToScreenRatioID = Shader.PropertyToID("_CustomFogTextureToScreenRatio");
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
		_bloomFog.UpdateShaderParams();
		float value;
		if (camera.stereoEnabled)
		{
			Matrix4x4 stereoProjectionMatrix = camera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
			Matrix4x4 stereoProjectionMatrix2 = camera.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
			value = Mathf.Abs(stereoProjectionMatrix.m02 - stereoProjectionMatrix2.m02) * 0.25f;
		}
		else
		{
			value = 0f;
		}
		_bloomPrepassRenderer.Render(camera, _params, _bloomPrePassRenderTexture);
		_bloomFog.bloomFogEnabled = true;
		Shader.SetGlobalTexture(_bloomPrePassTextureID, _bloomPrePassRenderTexture);
		Shader.SetGlobalFloat(_stereoCameraEyeOffsetID, value);
		Shader.SetGlobalFloat(_customFogTextureToScreenRatioID, _params.textureToScreenRatio);
	}

	public void OnCameraPostRender(Camera camera)
	{
		_bloomFog.bloomFogEnabled = false;
	}

	public static BloomPrePassLight[] GetLightsWithID(int id)
	{
		List<BloomPrePassLight> lightList = BloomPrePassLight.lightList;
		List<BloomPrePassLight> list = new List<BloomPrePassLight>();
		for (int i = 0; i < lightList.Count; i++)
		{
			BloomPrePassLight bloomPrePassLight = lightList[i];
			if (bloomPrePassLight.ID == id)
			{
				list.Add(bloomPrePassLight);
			}
		}
		return list.ToArray();
	}
}
