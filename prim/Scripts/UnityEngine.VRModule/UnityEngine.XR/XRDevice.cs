using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.XR;

public static class XRDevice
{
	public static extern bool isPresent
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern UserPresenceState userPresence
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	[Obsolete("family is deprecated.  Use XRSettings.loadedDeviceName instead.")]
	public static extern string family
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern string model
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern float refreshRate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static extern float fovZoomFactor
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern TrackingSpaceType GetTrackingSpaceType();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern bool SetTrackingSpaceType(TrackingSpaceType trackingSpaceType);

	public static IntPtr GetNativePtr()
	{
		INTERNAL_CALL_GetNativePtr(out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_GetNativePtr(out IntPtr value);

	public static void DisableAutoXRCameraTracking(Camera camera, bool disabled)
	{
		if (camera == null)
		{
			throw new ArgumentNullException("camera");
		}
		DisableAutoXRCameraTrackingInternal(camera, disabled);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void DisableAutoXRCameraTrackingInternal(Camera camera, bool disabled);
}
