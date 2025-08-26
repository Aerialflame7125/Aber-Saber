using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

public sealed class DynamicGI
{
	public static extern float indirectScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern float updateThreshold
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern bool synchronousMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern bool isConverged
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public static void SetEmissive(Renderer renderer, Color color)
	{
		INTERNAL_CALL_SetEmissive(renderer, ref color);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_SetEmissive(Renderer renderer, ref Color color);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void SetEnvironmentData(float[] input);

	[Obsolete("DynamicGI.UpdateMaterials(Renderer) is deprecated; instead, use extension method from RendererExtensions: 'renderer.UpdateGIMaterials()' (UnityUpgradable).", true)]
	public static void UpdateMaterials(Renderer renderer)
	{
	}

	[Obsolete("DynamicGI.UpdateMaterials(Terrain) is deprecated; instead, use extension method from TerrainExtensions: 'terrain.UpdateGIMaterials()' (UnityUpgradable).", true)]
	public static void UpdateMaterials(Object renderer)
	{
	}

	[Obsolete("DynamicGI.UpdateMaterials(Terrain, int, int, int, int) is deprecated; instead, use extension method from TerrainExtensions: 'terrain.UpdateGIMaterials(x, y, width, height)' (UnityUpgradable).", true)]
	public static void UpdateMaterials(Object renderer, int x, int y, int width, int height)
	{
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void UpdateEnvironment();
}
