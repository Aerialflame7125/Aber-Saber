using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.XR;

public static class XRSettings
{
	public static extern bool enabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern bool isDeviceActive
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern bool showDeviceView
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("renderScale is deprecated, use XRSettings.eyeTextureResolutionScale instead (UnityUpgradable) -> eyeTextureResolutionScale")]
	public static extern float renderScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern float eyeTextureResolutionScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern int eyeTextureWidth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern int eyeTextureHeight
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	internal static extern float renderViewportScaleInternal
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static RenderTextureDescriptor eyeTextureDesc
	{
		get
		{
			INTERNAL_get_eyeTextureDesc(out var value);
			return value;
		}
	}

	public static float renderViewportScale
	{
		get
		{
			return renderViewportScaleInternal;
		}
		set
		{
			if (value < 0f || value > 1f)
			{
				throw new ArgumentOutOfRangeException("value", "Render viewport scale should be between 0 and 1.");
			}
			renderViewportScaleInternal = value;
		}
	}

	public static extern float occlusionMaskScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern bool useOcclusionMesh
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern string loadedDeviceName
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern string[] supportedDevices
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_get_eyeTextureDesc(out RenderTextureDescriptor value);

	public static void LoadDeviceByName(string deviceName)
	{
		LoadDeviceByName(new string[1] { deviceName });
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void LoadDeviceByName(string[] prioritizedDeviceNameList);
}
