using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeHeader("Modules/Terrain/Public/TerrainDataScriptingInterface.h")]
[NativeHeader("TerrainScriptingClasses.h")]
public sealed class TerrainData : Object
{
	private enum BoundaryValueType
	{
		MaxHeightmapRes,
		MinDetailResPerPatch,
		MaxDetailResPerPatch,
		MaxDetailPatchCount,
		MinAlphamapRes,
		MaxAlphamapRes,
		MinBaseMapRes,
		MaxBaseMapRes
	}

	private const string k_ScriptingInterfaceName = "TerrainDataScriptingInterface";

	private const string k_ScriptingInterfacePrefix = "TerrainDataScriptingInterface::";

	private const string k_HeightmapPrefix = "GetHeightmap().";

	private const string k_DetailDatabasePrefix = "GetDetailDatabase().";

	private const string k_TreeDatabasePrefix = "GetTreeDatabase().";

	private const string k_SplatDatabasePrefix = "GetSplatDatabase().";

	private static readonly int k_MaximumResolution = GetBoundaryValue(BoundaryValueType.MaxHeightmapRes);

	private static readonly int k_MinimumDetailResolutionPerPatch = GetBoundaryValue(BoundaryValueType.MinDetailResPerPatch);

	private static readonly int k_MaximumDetailResolutionPerPatch = GetBoundaryValue(BoundaryValueType.MaxDetailResPerPatch);

	private static readonly int k_MaximumDetailPatchCount = GetBoundaryValue(BoundaryValueType.MaxDetailPatchCount);

	private static readonly int k_MinimumAlphamapResolution = GetBoundaryValue(BoundaryValueType.MinAlphamapRes);

	private static readonly int k_MaximumAlphamapResolution = GetBoundaryValue(BoundaryValueType.MaxAlphamapRes);

	private static readonly int k_MinimumBaseMapResolution = GetBoundaryValue(BoundaryValueType.MinBaseMapRes);

	private static readonly int k_MaximumBaseMapResolution = GetBoundaryValue(BoundaryValueType.MaxBaseMapRes);

	public extern int heightmapWidth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().GetWidth")]
		get;
	}

	public extern int heightmapHeight
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().GetHeight")]
		get;
	}

	public int heightmapResolution
	{
		get
		{
			return internalHeightmapResolution;
		}
		set
		{
			int num = value;
			if (value < 0 || value > k_MaximumResolution)
			{
				Debug.LogWarning("heightmapResolution is clamped to the range of [0, " + k_MaximumResolution + "].");
				num = Math.Min(k_MaximumResolution, Math.Max(value, 0));
			}
			internalHeightmapResolution = num;
		}
	}

	private extern int internalHeightmapResolution
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().GetResolution")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().SetResolution")]
		set;
	}

	public Vector3 heightmapScale
	{
		[NativeName("GetHeightmap().GetScale")]
		get
		{
			get_heightmapScale_Injected(out var ret);
			return ret;
		}
	}

	public Vector3 size
	{
		[NativeName("GetHeightmap().GetSize")]
		get
		{
			get_size_Injected(out var ret);
			return ret;
		}
		[NativeName("GetHeightmap().SetSize")]
		set
		{
			set_size_Injected(ref value);
		}
	}

	public Bounds bounds
	{
		[NativeName("GetHeightmap().CalculateBounds")]
		get
		{
			get_bounds_Injected(out var ret);
			return ret;
		}
	}

	public extern float thickness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().GetThickness")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetHeightmap().SetThickness")]
		set;
	}

	public extern float wavingGrassStrength
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().GetWavingGrassStrength")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassStrength", HasExplicitThis = true)]
		set;
	}

	public extern float wavingGrassAmount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().GetWavingGrassAmount")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassAmount", HasExplicitThis = true)]
		set;
	}

	public extern float wavingGrassSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().GetWavingGrassSpeed")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassSpeed", HasExplicitThis = true)]
		set;
	}

	public Color wavingGrassTint
	{
		[NativeName("GetDetailDatabase().GetWavingGrassTint")]
		get
		{
			get_wavingGrassTint_Injected(out var ret);
			return ret;
		}
		[FreeFunction("TerrainDataScriptingInterface::SetWavingGrassTint", HasExplicitThis = true)]
		set
		{
			set_wavingGrassTint_Injected(ref value);
		}
	}

	public extern int detailWidth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().GetWidth")]
		get;
	}

	public extern int detailHeight
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().GetHeight")]
		get;
	}

	public extern int detailResolution
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().GetResolution")]
		get;
	}

	internal extern int detailResolutionPerPatch
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetDetailDatabase().GetResolutionPerPatch")]
		get;
	}

	public extern DetailPrototype[] detailPrototypes
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetDetailPrototypes", HasExplicitThis = true)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetDetailPrototypes", HasExplicitThis = true)]
		set;
	}

	public TreeInstance[] treeInstances
	{
		get
		{
			return Internal_GetTreeInstances();
		}
		set
		{
			Internal_SetTreeInstances(value);
		}
	}

	public extern int treeInstanceCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetTreeDatabase().GetInstances().size")]
		get;
	}

	public extern TreePrototype[] treePrototypes
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetTreePrototypes", HasExplicitThis = true)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetTreePrototypes", HasExplicitThis = true)]
		set;
	}

	public extern int alphamapLayers
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().GetDepth")]
		get;
	}

	public int alphamapResolution
	{
		get
		{
			return Internal_alphamapResolution;
		}
		set
		{
			int internal_alphamapResolution = value;
			if (value < k_MinimumAlphamapResolution || value > k_MaximumAlphamapResolution)
			{
				Debug.LogWarning("alphamapResolution is clamped to the range of [" + k_MinimumAlphamapResolution + ", " + k_MaximumAlphamapResolution + "].");
				internal_alphamapResolution = Math.Min(k_MaximumAlphamapResolution, Math.Max(value, k_MinimumAlphamapResolution));
			}
			Internal_alphamapResolution = internal_alphamapResolution;
		}
	}

	private extern int Internal_alphamapResolution
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().GetAlphamapResolution")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().SetAlphamapResolution")]
		set;
	}

	public int alphamapWidth => alphamapResolution;

	public int alphamapHeight => alphamapResolution;

	public int baseMapResolution
	{
		get
		{
			return Internal_baseMapResolution;
		}
		set
		{
			int internal_baseMapResolution = value;
			if (value < k_MinimumBaseMapResolution || value > k_MaximumBaseMapResolution)
			{
				Debug.LogWarning("baseMapResolution is clamped to the range of [" + k_MinimumBaseMapResolution + ", " + k_MaximumBaseMapResolution + "].");
				internal_baseMapResolution = Math.Min(k_MaximumBaseMapResolution, Math.Max(value, k_MinimumBaseMapResolution));
			}
			Internal_baseMapResolution = internal_baseMapResolution;
		}
	}

	private extern int Internal_baseMapResolution
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().GetBaseMapResolution")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().SetBaseMapResolution")]
		set;
	}

	private extern int alphamapTextureCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeName("GetSplatDatabase().GetAlphaTextureCount")]
		get;
	}

	public Texture2D[] alphamapTextures
	{
		get
		{
			Texture2D[] array = new Texture2D[alphamapTextureCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = GetAlphamapTexture(i);
			}
			return array;
		}
	}

	public extern SplatPrototype[] splatPrototypes
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::GetSplatPrototypes", HasExplicitThis = true)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[FreeFunction("TerrainDataScriptingInterface::SetSplatPrototypes", HasExplicitThis = true)]
		set;
	}

	public TerrainData()
	{
		Internal_Create(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	[StaticAccessor("TerrainDataScriptingInterface", StaticAccessorType.DoubleColon)]
	private static extern int GetBoundaryValue(BoundaryValueType type);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::Create")]
	private static extern void Internal_Create([Writable] TerrainData terrainData);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal extern bool HasUser(GameObject user);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal extern void AddUser(GameObject user);

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal extern void RemoveUser(GameObject user);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetHeightmap().GetHeight")]
	public extern float GetHeight(int x, int y);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetHeightmap().GetInterpolatedHeight")]
	public extern float GetInterpolatedHeight(float x, float y);

	public float[,] GetHeights(int xBase, int yBase, int width, int height)
	{
		if (xBase < 0 || yBase < 0 || xBase + width < 0 || yBase + height < 0 || xBase + width > heightmapWidth || yBase + height > heightmapHeight)
		{
			throw new ArgumentException("Trying to access out-of-bounds terrain height information.");
		}
		return Internal_GetHeights(xBase, yBase, width, height);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::GetHeights", HasExplicitThis = true)]
	private extern float[,] Internal_GetHeights(int xBase, int yBase, int width, int height);

	public void SetHeights(int xBase, int yBase, float[,] heights)
	{
		if (heights == null)
		{
			throw new NullReferenceException();
		}
		if (xBase + heights.GetLength(1) > heightmapWidth || xBase + heights.GetLength(1) < 0 || yBase + heights.GetLength(0) < 0 || xBase < 0 || yBase < 0 || yBase + heights.GetLength(0) > heightmapHeight)
		{
			throw new ArgumentException(UnityString.Format("X or Y base out of bounds. Setting up to {0}x{1} while map size is {2}x{3}", xBase + heights.GetLength(1), yBase + heights.GetLength(0), heightmapWidth, heightmapHeight));
		}
		Internal_SetHeights(xBase, yBase, heights.GetLength(1), heights.GetLength(0), heights);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::SetHeights", HasExplicitThis = true)]
	private extern void Internal_SetHeights(int xBase, int yBase, int width, int height, float[,] heights);

	public void SetHeightsDelayLOD(int xBase, int yBase, float[,] heights)
	{
		if (heights == null)
		{
			throw new ArgumentNullException("heights");
		}
		int length = heights.GetLength(0);
		int length2 = heights.GetLength(1);
		if (xBase < 0 || xBase + length2 < 0 || xBase + length2 > heightmapWidth)
		{
			throw new ArgumentException(UnityString.Format("X out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", xBase, xBase + length2, heightmapWidth));
		}
		if (yBase < 0 || yBase + length < 0 || yBase + length > heightmapHeight)
		{
			throw new ArgumentException(UnityString.Format("Y out of bounds - trying to set {0}-{1} but the terrain ranges from 0-{2}", yBase, yBase + length, heightmapHeight));
		}
		Internal_SetHeightsDelayLOD(xBase, yBase, length2, length, heights);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::SetHeightsDelayLOD", HasExplicitThis = true)]
	private extern void Internal_SetHeightsDelayLOD(int xBase, int yBase, int width, int height, float[,] heights);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetHeightmap().GetSteepness")]
	public extern float GetSteepness(float x, float y);

	[NativeName("GetHeightmap().GetInterpolatedNormal")]
	public Vector3 GetInterpolatedNormal(float x, float y)
	{
		GetInterpolatedNormal_Injected(x, y, out var ret);
		return ret;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetHeightmap().GetAdjustedSize")]
	internal extern int GetAdjustedSize(int size);

	public void SetDetailResolution(int detailResolution, int resolutionPerPatch)
	{
		if (detailResolution < 0)
		{
			Debug.LogWarning("detailResolution must not be negative.");
			detailResolution = 0;
		}
		if (resolutionPerPatch < k_MinimumDetailResolutionPerPatch || resolutionPerPatch > k_MaximumDetailResolutionPerPatch)
		{
			Debug.LogWarning("resolutionPerPatch is clamped to the range of [" + k_MinimumDetailResolutionPerPatch + ", " + k_MaximumDetailResolutionPerPatch + "].");
			resolutionPerPatch = Math.Min(k_MaximumDetailResolutionPerPatch, Math.Max(resolutionPerPatch, k_MinimumDetailResolutionPerPatch));
		}
		int num = detailResolution / resolutionPerPatch;
		if (num > k_MaximumDetailPatchCount)
		{
			Debug.LogWarning("Patch count (detailResolution / resolutionPerPatch) is clamped to the range of [0, " + k_MaximumDetailPatchCount + "].");
			num = Math.Min(k_MaximumDetailPatchCount, Math.Max(num, 0));
		}
		Internal_SetDetailResolution(num, resolutionPerPatch);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetDetailDatabase().SetDetailResolution")]
	private extern void Internal_SetDetailResolution(int patchCount, int resolutionPerPatch);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetDetailDatabase().ResetDirtyDetails")]
	internal extern void ResetDirtyDetails();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::RefreshPrototypes", HasExplicitThis = true)]
	public extern void RefreshPrototypes();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::GetSupportedLayers", HasExplicitThis = true)]
	public extern int[] GetSupportedLayers(int xBase, int yBase, int totalWidth, int totalHeight);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::GetDetailLayer", HasExplicitThis = true)]
	public extern int[,] GetDetailLayer(int xBase, int yBase, int width, int height, int layer);

	public void SetDetailLayer(int xBase, int yBase, int layer, int[,] details)
	{
		Internal_SetDetailLayer(xBase, yBase, details.GetLength(1), details.GetLength(0), layer, details);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::SetDetailLayer", HasExplicitThis = true)]
	private extern void Internal_SetDetailLayer(int xBase, int yBase, int totalWidth, int totalHeight, int detailIndex, int[,] data);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetTreeDatabase().GetInstances")]
	private extern TreeInstance[] Internal_GetTreeInstances();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::SetTreeInstances", HasExplicitThis = true)]
	private extern void Internal_SetTreeInstances([NotNull] TreeInstance[] instances);

	public TreeInstance GetTreeInstance(int index)
	{
		if (index < 0 || index >= treeInstanceCount)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		return Internal_GetTreeInstance(index);
	}

	[FreeFunction("TerrainDataScriptingInterface::GetTreeInstance", HasExplicitThis = true)]
	private TreeInstance Internal_GetTreeInstance(int index)
	{
		Internal_GetTreeInstance_Injected(index, out var ret);
		return ret;
	}

	[FreeFunction("TerrainDataScriptingInterface::SetTreeInstance", HasExplicitThis = true)]
	[NativeThrows]
	public void SetTreeInstance(int index, TreeInstance instance)
	{
		SetTreeInstance_Injected(index, ref instance);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetTreeDatabase().RemoveTreePrototype")]
	internal extern void RemoveTreePrototype(int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetTreeDatabase().RecalculateTreePositions")]
	internal extern void RecalculateTreePositions();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetDetailDatabase().RemoveDetailPrototype")]
	internal extern void RemoveDetailPrototype(int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetTreeDatabase().NeedUpgradeScaledPrototypes")]
	internal extern bool NeedUpgradeScaledTreePrototypes();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::UpgradeScaledTreePrototype", HasExplicitThis = true)]
	internal extern void UpgradeScaledTreePrototype();

	public float[,,] GetAlphamaps(int x, int y, int width, int height)
	{
		if (x < 0 || y < 0 || width < 0 || height < 0)
		{
			throw new ArgumentException("Invalid argument for GetAlphaMaps");
		}
		return Internal_GetAlphamaps(x, y, width, height);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::GetAlphamaps", HasExplicitThis = true)]
	private extern float[,,] Internal_GetAlphamaps(int x, int y, int width, int height);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[RequiredByNativeCode]
	[NativeName("GetSplatDatabase().GetAlphamapResolution")]
	internal extern float GetAlphamapResolutionInternal();

	public void SetAlphamaps(int x, int y, float[,,] map)
	{
		if (map.GetLength(2) != alphamapLayers)
		{
			throw new Exception(UnityString.Format("Float array size wrong (layers should be {0})", alphamapLayers));
		}
		Internal_SetAlphamaps(x, y, map.GetLength(1), map.GetLength(0), map);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("TerrainDataScriptingInterface::SetAlphamaps", HasExplicitThis = true)]
	private extern void Internal_SetAlphamaps(int x, int y, int width, int height, float[,,] map);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetSplatDatabase().RecalculateBasemapIfDirty")]
	internal extern void RecalculateBasemapIfDirty();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetSplatDatabase().SetBasemapDirty")]
	internal extern void SetBasemapDirty(bool dirty);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetSplatDatabase().GetAlphaTexture")]
	private extern Texture2D GetAlphamapTexture(int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeName("GetTreeDatabase().AddTree")]
	internal extern void AddTree(ref TreeInstance tree);

	[NativeName("GetTreeDatabase().RemoveTrees")]
	internal int RemoveTrees(Vector2 position, float radius, int prototypeIndex)
	{
		return RemoveTrees_Injected(ref position, radius, prototypeIndex);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_heightmapScale_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_size_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_size_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_bounds_Injected(out Bounds ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void GetInterpolatedNormal_Injected(float x, float y, out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_wavingGrassTint_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_wavingGrassTint_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void Internal_GetTreeInstance_Injected(int index, out TreeInstance ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void SetTreeInstance_Injected(int index, ref TreeInstance instance);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int RemoveTrees_Injected(ref Vector2 position, float radius, int prototypeIndex);
}
