using UnityEngine;

public class MixedRealityQuadrantsCompositor : MixedRealityCompositor
{
	private Camera _externalCamera;

	private Camera _overlayCamera;

	private MainCamera _mainCamera;

	private Transform _externalCameraTransform;

	private Transform _mainCameraTransform;

	private LayerMask _dontRenderWithNearCamera;

	private Material _colorMat;

	private Material _alphaMat;

	private Material _overlayColorMat;

	private Material _overlayAlphaMat;

	private bool _initialized;

	public void Init(Camera externalCamera, Camera overlayCamera, MainCamera mainCamera, LayerMask dontRenderWithNearCamera)
	{
		_initialized = true;
		_externalCamera = externalCamera;
		_externalCameraTransform = _externalCamera.transform;
		_overlayCamera = overlayCamera;
		_mainCamera = mainCamera;
		_mainCameraTransform = _mainCamera.transform;
		_dontRenderWithNearCamera = dontRenderWithNearCamera;
		_colorMat = new Material(Shader.Find("Custom/SimpleTexture"));
		_alphaMat = new Material(Shader.Find("Custom/DepthAndLuminanceToWhite"));
		_overlayColorMat = new Material(Shader.Find("Custom/MROverlayColor"));
		_overlayAlphaMat = new Material(Shader.Find("Custom/MROverlayAlpha"));
		int width = Screen.width / 2;
		int height = Screen.height / 2;
		RenderTexture renderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
		int num = QualitySettings.antiAliasing / 2;
		if (num <= 0)
		{
			num = 1;
		}
		renderTexture.antiAliasing = num;
		_externalCamera.targetTexture = renderTexture;
		renderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
		_overlayCamera.targetTexture = renderTexture;
	}

	public override void Cleanup()
	{
		_externalCamera.targetTexture.Release();
		Object.Destroy(_externalCamera.targetTexture);
		_externalCamera.targetTexture = null;
		_overlayCamera.targetTexture.Release();
		Object.Destroy(_overlayCamera.targetTexture);
		_overlayCamera.targetTexture = null;
	}

	public override void DoComposition(int frameNum)
	{
		if (frameNum % 3 == 0)
		{
			GL.Clear(true, true, Color.clear);
			DrawComposition();
		}
	}

	private void DrawComposition()
	{
		int num = Screen.width / 2;
		int num2 = Screen.height / 2;
		float farClipPlane = _externalCamera.farClipPlane;
		CameraClearFlags clearFlags = _externalCamera.clearFlags;
		Color backgroundColor = _externalCamera.backgroundColor;
		_externalCamera.Render();
		DrawTools.DrawTexture(_externalCamera.targetTexture, 0f, 0f, num, num2, _colorMat);
		_externalCamera.clearFlags = CameraClearFlags.Color;
		_externalCamera.backgroundColor = Color.clear;
		float mainCameraProjectedDistance = GetMainCameraProjectedDistance();
		if (mainCameraProjectedDistance > _externalCamera.nearClipPlane)
		{
			int cullingMask = _externalCamera.cullingMask;
			_externalCamera.cullingMask &= ~_dontRenderWithNearCamera.value;
			_externalCamera.farClipPlane = Mathf.Min(mainCameraProjectedDistance, _externalCamera.farClipPlane);
			_externalCamera.Render();
			_externalCamera.cullingMask = cullingMask;
			DrawTools.DrawTexture(_externalCamera.targetTexture, 0f, num2, num, num2, _colorMat);
			DrawTools.DrawTexture(_externalCamera.targetTexture, num, num2, num, num2, _alphaMat);
		}
		if (_overlayCamera != null)
		{
			_overlayCamera.Render();
			DrawTools.DrawTexture(_overlayCamera.targetTexture, 0f, num2, num, num2, _overlayColorMat);
			DrawTools.DrawTexture(_overlayCamera.targetTexture, num, num2, num, num2, _overlayAlphaMat);
		}
		_externalCamera.clearFlags = clearFlags;
		_externalCamera.backgroundColor = backgroundColor;
		_externalCamera.farClipPlane = farClipPlane;
		RenderTexture finalImageRenderTexture = _mainCamera.mainEffect.finalImageRenderTexture;
		if ((bool)finalImageRenderTexture)
		{
			if (_mainCamera.camera.stereoTargetEye == StereoTargetEyeMask.Both)
			{
				DrawTools.DrawTexture(finalImageRenderTexture, num, 0f, num, num2, _colorMat, 0.05f, 0.3f, 0.4f, 0.4f);
			}
			else
			{
				DrawTools.DrawTexture(finalImageRenderTexture, num, 0f, num, num2, _colorMat);
			}
		}
	}

	private float GetMainCameraProjectedDistance()
	{
		Vector3 forward = _mainCameraTransform.forward;
		Vector3 normalized = new Vector3(forward.x, 0f, forward.z).normalized;
		Vector3 position = _mainCameraTransform.position;
		return 0f - new Plane(normalized, position).GetDistanceToPoint(_externalCameraTransform.position);
	}
}
