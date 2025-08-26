using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.XR;

[UsedByNativeCode]
[NativeType(Header = "Modules/XR/Subsystems/Camera/XRCameraSubsystem.h")]
[NativeHeader("Runtime/Graphics/Texture2D.h")]
[NativeHeader("Modules/XR/XRPrefix.h")]
[NativeConditional("ENABLE_XR")]
public class XRCameraSubsystem : Subsystem<XRCameraSubsystemDescriptor>
{
	public extern int LastUpdatedFrame
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern bool LightEstimationRequested
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern Material Material
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern Camera Camera
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public event Action<FrameReceivedEventArgs> FrameReceived;

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool TryGetAverageBrightness(ref float averageBrightness);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool TryGetAverageColorTemperature(ref float averageColorTemperature);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool TryGetProjectionMatrix(ref Matrix4x4 projectionMatrix);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool TryGetDisplayMatrix(ref Matrix4x4 displayMatrix);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool TryGetTimestamp(ref long timestampNs);

	public bool TryGetShaderName(ref string shaderName)
	{
		return Internal_TryGetShaderName(ref shaderName);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool Internal_TryGetShaderName(ref string shaderName);

	public void GetTextures(List<Texture2D> texturesOut)
	{
		if (texturesOut == null)
		{
			throw new ArgumentNullException("texturesOut");
		}
		GetTexturesAsList(texturesOut);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetTexturesAsList(List<Texture2D> textures);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern Texture2D[] GetTexturesAsFixedArray();

	[RequiredByNativeCode]
	private void InvokeFrameReceivedEvent()
	{
		if (this.FrameReceived != null)
		{
			this.FrameReceived(new FrameReceivedEventArgs
			{
				m_CameraSubsystem = this
			});
		}
	}
}
