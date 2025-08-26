using UnityEngine;

public class MixedRealityFarCamCompositor : MixedRealityCompositor
{
	private Camera _externalCamera;

	private Camera _overlayCamera;

	private Material _colorMat;

	private Material _overlayColorMat;

	private bool _initialized;

	public void Init(Camera externalCamera, Camera overlayCamera)
	{
		_initialized = true;
		_externalCamera = externalCamera;
		_overlayCamera = overlayCamera;
		_colorMat = new Material(Shader.Find("Custom/SimpleTexture"));
		_overlayColorMat = new Material(Shader.Find("Custom/MROverlayColor"));
		int width = Screen.width;
		int height = Screen.height;
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
		DrawComposition();
	}

	public void DrawComposition()
	{
		GL.Clear(clearDepth: true, clearColor: true, Color.clear);
		int width = Screen.width;
		int height = Screen.height;
		_externalCamera.Render();
		DrawTools.DrawTexture(_externalCamera.targetTexture, 0f, 0f, width, height, _colorMat);
		if (_overlayCamera != null)
		{
			_overlayCamera.Render();
			DrawTools.DrawTexture(_overlayCamera.targetTexture, 0f, 0f, width, height, _overlayColorMat);
		}
	}
}
