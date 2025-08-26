using UnityEngine;

public class MixedRealityWebCamCompositor : MixedRealityCompositor
{
	private int _framesDelay;

	private RenderTexture[] _farCameraRenderTexturesBuffer;

	private RenderTexture[] _nearCameraRenderTexturesBuffer;

	private LayerMask _dontRenderWithNearCamera;

	private Camera _externalCamera;

	private MainCamera _mainCamera;

	private Camera _overlayCamera;

	private Camera _webCamCamera;

	private int _lastUsedBufferIdx;

	private int _numberOfStoreToBuffersOperations;

	private Transform _externalCameraTransform;

	private Transform _mainCameraTransform;

	private Material _colorMat;

	private Material _alphaBlendMat;

	private bool _initialized;

	private MixedRealitySettings.PIPPosition _pipPosition;

	private float _pipRelativeSize;

	public void Init(Camera externalCamera, Camera overlayCamera, Camera webCamCamera, MainCamera mainCamera, LayerMask dontRenderWithNearCamera, int texturesWidth, int texturesHeight, int framesDelay, MixedRealitySettings.PIPPosition pIPPosition, float pipRelativeSize)
	{
		_initialized = true;
		_pipPosition = pIPPosition;
		_pipRelativeSize = pipRelativeSize;
		_colorMat = new Material(Shader.Find("Custom/SimpleTexture"));
		_alphaBlendMat = new Material(Shader.Find("Custom/MROverlayColor"));
		_externalCamera = externalCamera;
		_externalCameraTransform = externalCamera.transform;
		_webCamCamera = webCamCamera;
		_mainCamera = mainCamera;
		_mainCameraTransform = mainCamera.transform;
		_overlayCamera = overlayCamera;
		_dontRenderWithNearCamera = dontRenderWithNearCamera;
		_framesDelay = framesDelay;
		int num = QualitySettings.antiAliasing / 2;
		if (num <= 0)
		{
			num = 1;
		}
		_farCameraRenderTexturesBuffer = new RenderTexture[Mathf.Max(_framesDelay + 1, 1)];
		for (int i = 0; i < _farCameraRenderTexturesBuffer.Length; i++)
		{
			_farCameraRenderTexturesBuffer[i] = new RenderTexture(texturesWidth, texturesHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			_farCameraRenderTexturesBuffer[i].antiAliasing = num;
			_farCameraRenderTexturesBuffer[i].Create();
		}
		_nearCameraRenderTexturesBuffer = new RenderTexture[Mathf.Max(_framesDelay + 1, 1)];
		for (int j = 0; j < _nearCameraRenderTexturesBuffer.Length; j++)
		{
			_nearCameraRenderTexturesBuffer[j] = new RenderTexture(texturesWidth, texturesHeight, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			_nearCameraRenderTexturesBuffer[j].Create();
		}
		RenderTexture targetTexture = new RenderTexture(texturesWidth, texturesHeight, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
		_overlayCamera.targetTexture = targetTexture;
	}

	public override void Cleanup()
	{
		_initialized = false;
		for (int i = 0; i < _farCameraRenderTexturesBuffer.Length; i++)
		{
			_farCameraRenderTexturesBuffer[i].Release();
		}
		for (int j = 0; j < _nearCameraRenderTexturesBuffer.Length; j++)
		{
			_nearCameraRenderTexturesBuffer[j].Release();
		}
		_overlayCamera.targetTexture.Release();
		Object.Destroy(_overlayCamera.targetTexture);
		_overlayCamera.targetTexture = null;
	}

	public override void DoComposition(int frameNum)
	{
		switch (frameNum % 3)
		{
		case 0:
			if (_numberOfStoreToBuffersOperations > 0)
			{
				_lastUsedBufferIdx = (_lastUsedBufferIdx + 1) % _farCameraRenderTexturesBuffer.Length;
			}
			_numberOfStoreToBuffersOperations++;
			RenderFarCamera(_lastUsedBufferIdx);
			break;
		case 1:
			RenderNearCamera(_lastUsedBufferIdx);
			break;
		case 2:
			if (!renderEveryFrame)
			{
				DrawComposition();
			}
			break;
		}
		if (renderEveryFrame)
		{
			DrawComposition();
		}
	}

	private void RenderFarCamera(int bufferIdx)
	{
		_externalCamera.targetTexture = _farCameraRenderTexturesBuffer[bufferIdx];
		_externalCamera.Render();
	}

	private void RenderNearCamera(int bufferIdx)
	{
		float farClipPlane = _externalCamera.farClipPlane;
		CameraClearFlags clearFlags = _externalCamera.clearFlags;
		Color backgroundColor = _externalCamera.backgroundColor;
		_externalCamera.clearFlags = CameraClearFlags.Color;
		_externalCamera.backgroundColor = Color.clear;
		float mainCameraProjectedDistance = GetMainCameraProjectedDistance();
		if (mainCameraProjectedDistance > _externalCamera.nearClipPlane)
		{
			int cullingMask = _externalCamera.cullingMask;
			_externalCamera.cullingMask &= ~_dontRenderWithNearCamera.value;
			_externalCamera.farClipPlane = Mathf.Min(mainCameraProjectedDistance, _externalCamera.farClipPlane);
			_externalCamera.targetTexture = _nearCameraRenderTexturesBuffer[bufferIdx];
			_externalCamera.Render();
			_externalCamera.cullingMask = cullingMask;
		}
		_externalCamera.clearFlags = clearFlags;
		_externalCamera.backgroundColor = backgroundColor;
		_externalCamera.farClipPlane = farClipPlane;
	}

	private void DrawComposition()
	{
		_webCamCamera.Render();
		GL.Clear(clearDepth: true, clearColor: true, Color.clear);
		if (_numberOfStoreToBuffersOperations < _framesDelay)
		{
			return;
		}
		int width = Screen.width;
		int height = Screen.height;
		int lastUsedBufferIdx = _lastUsedBufferIdx;
		lastUsedBufferIdx = ((lastUsedBufferIdx < _framesDelay) ? (lastUsedBufferIdx - _framesDelay + _farCameraRenderTexturesBuffer.Length) : (lastUsedBufferIdx - _framesDelay));
		DrawTools.DrawTexture(_farCameraRenderTexturesBuffer[lastUsedBufferIdx], 0f, 0f, width, height, _colorMat);
		_webCamCamera.Render();
		DrawTools.DrawTexture(_nearCameraRenderTexturesBuffer[lastUsedBufferIdx], 0f, 0f, width, height, _alphaBlendMat);
		Vector2 vector = default(Vector2);
		switch (_pipPosition)
		{
		case MixedRealitySettings.PIPPosition.BottomRight:
			vector = new Vector2((float)width * (1f - _pipRelativeSize) - (float)height * 0.02f, (float)height * 0.02f);
			break;
		case MixedRealitySettings.PIPPosition.BottomLeft:
			vector = new Vector2((float)height * 0.02f, (float)height * 0.02f);
			break;
		case MixedRealitySettings.PIPPosition.TopLeft:
			vector = new Vector2((float)height * 0.02f, (float)height * (1f - _pipRelativeSize) - (float)height * 0.02f);
			break;
		case MixedRealitySettings.PIPPosition.TopRight:
			vector = new Vector2((float)width * (1f - _pipRelativeSize) - (float)height * 0.02f, (float)height * (1f - _pipRelativeSize) - (float)height * 0.02f);
			break;
		}
		RenderTexture finalImageRenderTexture = _mainCamera.mainEffect.finalImageRenderTexture;
		if ((bool)finalImageRenderTexture)
		{
			if (_mainCamera.camera.stereoTargetEye == StereoTargetEyeMask.Both)
			{
				DrawTools.DrawTexture(finalImageRenderTexture, vector.x, vector.y, (float)width * _pipRelativeSize, (float)height * _pipRelativeSize, _colorMat, 0.05f, 0.3f, 0.4f, 0.4f);
			}
			else
			{
				DrawTools.DrawTexture(finalImageRenderTexture, vector.x, vector.y, (float)width * _pipRelativeSize, (float)height * _pipRelativeSize, _colorMat);
			}
		}
		if (_overlayCamera != null)
		{
			_overlayCamera.Render();
			DrawTools.DrawTexture(_overlayCamera.targetTexture, 0f, 0f, width, height, _alphaBlendMat);
		}
	}

	private float GetMainCameraProjectedDistance()
	{
		Vector3 forward = _externalCameraTransform.forward;
		Vector3 normalized = new Vector3(forward.x, 0f, forward.z).normalized;
		Vector3 position = _mainCameraTransform.position;
		return 0f - new Plane(normalized, position).GetDistanceToPoint(_externalCameraTransform.position);
	}
}
