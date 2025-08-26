using UnityEngine;
using UnityEngine.Rendering;

public class GrabScreen : MonoBehaviour
{
	private CommandBuffer _commandBuffer;

	private Camera _camera;

	public void OnEnable()
	{
		Cleanup();
		Init();
	}

	public void OnDisable()
	{
		Cleanup();
	}

	private void Awake()
	{
		_camera = GetComponent<Camera>();
		Init();
	}

	private void Cleanup()
	{
		if (_camera != null && _commandBuffer != null)
		{
			_camera.RemoveCommandBuffer(CameraEvent.AfterSkybox, _commandBuffer);
		}
		_commandBuffer = null;
	}

	private void Init()
	{
		if (_commandBuffer == null && !(_camera == null))
		{
			_commandBuffer = new CommandBuffer();
			_commandBuffer.name = "GrabScreen";
			int num = Shader.PropertyToID("_ScreenCopyTexture");
			_commandBuffer.GetTemporaryRT(num, -1, -1, 0, FilterMode.Bilinear);
			_commandBuffer.Blit(BuiltinRenderTextureType.CurrentActive, num);
			_commandBuffer.ReleaseTemporaryRT(num);
			_commandBuffer.SetGlobalTexture("_GlobalScreenTex", num);
			_camera.AddCommandBuffer(CameraEvent.AfterSkybox, _commandBuffer);
		}
	}
}
