using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class CommandBufferBlurryScreenGrab : CommandBufferGOCore
{
	[SerializeField]
	private KawaseBlurRenderer _kawaseBlurRenderer;

	[SerializeField]
	private KawaseBlurRenderer.KernelSize _kernelSize = KawaseBlurRenderer.KernelSize.Kernel63;

	[SerializeField]
	private int _downsample;

	private static Dictionary<Camera, CommandBufferOwners> _cameras = new Dictionary<Camera, CommandBufferOwners>();

	protected override CommandBuffer CreateCommandBuffer()
	{
		CommandBuffer commandBuffer = _kawaseBlurRenderer.CreateBlurCommandBuffer("_GrabBlurTexture", _kernelSize, 0f, _downsample);
		commandBuffer.name = "Grab screen and blur";
		return commandBuffer;
	}

	protected override Dictionary<Camera, CommandBufferOwners> CamerasDict()
	{
		return _cameras;
	}

	protected override CameraEvent CommandBufferCameraEvent()
	{
		return CameraEvent.BeforeForwardAlpha;
	}
}
