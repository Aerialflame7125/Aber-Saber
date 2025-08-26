using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine;

[UsedByNativeCode]
[NativeHeader("Runtime/Interfaces/ITerrainManager.h")]
[NativeHeader("TerrainScriptingClasses.h")]
[StaticAccessor("GetITerrainManager()", StaticAccessorType.Arrow)]
[NativeHeader("Modules/Terrain/Public/Terrain.h")]
public sealed class Terrain : Behaviour
{
	public enum MaterialType
	{
		BuiltInStandard,
		BuiltInLegacyDiffuse,
		BuiltInLegacySpecular,
		Custom
	}

	public extern TerrainData terrainData
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float treeDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float treeBillboardDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float treeCrossFadeLength
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int treeMaximumFullLODCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float detailObjectDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float detailObjectDensity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float heightmapPixelError
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern int heightmapMaximumLOD
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float basemapDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[Obsolete("splatmapDistance is deprecated, please use basemapDistance instead. (UnityUpgradable) -> basemapDistance", true)]
	public float splatmapDistance
	{
		get
		{
			return basemapDistance;
		}
		set
		{
			basemapDistance = value;
		}
	}

	[NativeProperty("StaticLightmapIndexInt")]
	public extern int lightmapIndex
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("DynamicLightmapIndexInt")]
	public extern int realtimeLightmapIndex
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[NativeProperty("StaticLightmapST")]
	public Vector4 lightmapScaleOffset
	{
		get
		{
			get_lightmapScaleOffset_Injected(out var ret);
			return ret;
		}
		set
		{
			set_lightmapScaleOffset_Injected(ref value);
		}
	}

	[NativeProperty("DynamicLightmapST")]
	public Vector4 realtimeLightmapScaleOffset
	{
		get
		{
			get_realtimeLightmapScaleOffset_Injected(out var ret);
			return ret;
		}
		set
		{
			set_realtimeLightmapScaleOffset_Injected(ref value);
		}
	}

	[NativeProperty("GarbageCollectRenderers")]
	public extern bool freeUnusedRenderingResources
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool castShadows
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern ReflectionProbeUsage reflectionProbeUsage
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern MaterialType materialType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern Material materialTemplate
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Color legacySpecular
	{
		get
		{
			get_legacySpecular_Injected(out var ret);
			return ret;
		}
		set
		{
			set_legacySpecular_Injected(ref value);
		}
	}

	public extern float legacyShininess
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool drawHeightmap
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool drawTreesAndFoliage
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 patchBoundsMultiplier
	{
		get
		{
			get_patchBoundsMultiplier_Injected(out var ret);
			return ret;
		}
		set
		{
			set_patchBoundsMultiplier_Injected(ref value);
		}
	}

	public extern float treeLODBiasMultiplier
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool collectDetailPatches
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern TerrainRenderFlags editorRenderFlags
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public static extern Terrain activeTerrain
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[NativeProperty("ActiveTerrainsScriptingArray")]
	public static extern Terrain[] activeTerrains
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void GetClosestReflectionProbes(List<ReflectionProbeBlendInfo> result);

	public float SampleHeight(Vector3 worldPosition)
	{
		return SampleHeight_Injected(ref worldPosition);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void ApplyDelayedHeightmapModification();

	public void AddTreeInstance(TreeInstance instance)
	{
		AddTreeInstance_Injected(ref instance);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void SetNeighbors(Terrain left, Terrain top, Terrain right, Terrain bottom);

	public Vector3 GetPosition()
	{
		GetPosition_Injected(out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void Flush();

	internal void RemoveTrees(Vector2 position, float radius, int prototypeIndex)
	{
		RemoveTrees_Injected(ref position, radius, prototypeIndex);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("CopySplatMaterialCustomProps")]
	public extern void SetSplatMaterialPropertyBlock(MaterialPropertyBlock properties);

	public void GetSplatMaterialPropertyBlock(MaterialPropertyBlock dest)
	{
		if (dest == null)
		{
			throw new ArgumentNullException("dest");
		}
		Internal_GetSplatMaterialPropertyBlock(dest);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetSplatMaterialCustomProps")]
	private extern void Internal_GetSplatMaterialPropertyBlock(MaterialPropertyBlock dest);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[UsedByNativeCode]
	public static extern GameObject CreateTerrainGameObject(TerrainData assignTerrain);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_lightmapScaleOffset_Injected(out Vector4 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_lightmapScaleOffset_Injected(ref Vector4 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_realtimeLightmapScaleOffset_Injected(out Vector4 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_realtimeLightmapScaleOffset_Injected(ref Vector4 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_legacySpecular_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_legacySpecular_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_patchBoundsMultiplier_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_patchBoundsMultiplier_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern float SampleHeight_Injected(ref Vector3 worldPosition);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void AddTreeInstance_Injected(ref TreeInstance instance);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetPosition_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void RemoveTrees_Injected(ref Vector2 position, float radius, int prototypeIndex);
}
