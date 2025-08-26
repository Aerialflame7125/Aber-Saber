using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

public sealed class LightmapSettings : Object
{
	public static extern LightmapData[] lightmaps
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern LightmapsMode lightmapsMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern LightProbes lightProbes
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("Use lightmapsMode instead.", false)]
	public static LightmapsModeLegacy lightmapsModeLegacy
	{
		get
		{
			return LightmapsModeLegacy.Single;
		}
		set
		{
		}
	}

	[Obsolete("Use QualitySettings.desiredColorSpace instead.", false)]
	public static ColorSpace bakedColorSpace
	{
		get
		{
			return QualitySettings.desiredColorSpace;
		}
		set
		{
		}
	}

	private LightmapSettings()
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal static extern void Reset();
}
