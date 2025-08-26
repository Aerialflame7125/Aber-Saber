using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
[NativeHeader("Runtime/Graphics/CopyTexture.h")]
[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
[NativeHeader("Runtime/Shaders/ComputeShader.h")]
public sealed class Graphics
{
	internal static readonly int kMaxDrawMeshInstanceCount = Internal_GetMaxDrawMeshInstanceCount();

	public static RenderBuffer activeColorBuffer
	{
		get
		{
			GetActiveColorBuffer(out var res);
			return res;
		}
	}

	public static RenderBuffer activeDepthBuffer
	{
		get
		{
			GetActiveDepthBuffer(out var res);
			return res;
		}
	}

	public static extern GraphicsTier activeTier
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern ColorGamut activeColorGamut
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	private static void Internal_DrawMeshNow1(Mesh mesh, int subsetIndex, Vector3 position, Quaternion rotation)
	{
		INTERNAL_CALL_Internal_DrawMeshNow1(mesh, subsetIndex, ref position, ref rotation);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Internal_DrawMeshNow1(Mesh mesh, int subsetIndex, ref Vector3 position, ref Quaternion rotation);

	private static void Internal_DrawMeshNow2(Mesh mesh, int subsetIndex, Matrix4x4 matrix)
	{
		INTERNAL_CALL_Internal_DrawMeshNow2(mesh, subsetIndex, ref matrix);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Internal_DrawMeshNow2(Mesh mesh, int subsetIndex, ref Matrix4x4 matrix);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void DrawProcedural(MeshTopology topology, int vertexCount, [DefaultValue("1")] int instanceCount);

	[ExcludeFromDocs]
	public static void DrawProcedural(MeshTopology topology, int vertexCount)
	{
		int instanceCount = 1;
		DrawProcedural(topology, vertexCount, instanceCount);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void DrawProceduralIndirect(MeshTopology topology, ComputeBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset);

	[ExcludeFromDocs]
	public static void DrawProceduralIndirect(MeshTopology topology, ComputeBuffer bufferWithArgs)
	{
		int argsOffset = 0;
		DrawProceduralIndirect(topology, bufferWithArgs, argsOffset);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern int Internal_GetMaxDrawMeshInstanceCount();

	[ExcludeFromDocs]
	public static void DrawTexture(Rect screenRect, Texture texture, Material mat)
	{
		int pass = -1;
		DrawTexture(screenRect, texture, mat, pass);
	}

	[ExcludeFromDocs]
	public static void DrawTexture(Rect screenRect, Texture texture)
	{
		int pass = -1;
		Material mat = null;
		DrawTexture(screenRect, texture, mat, pass);
	}

	public static void DrawTexture(Rect screenRect, Texture texture, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
	{
		DrawTexture(screenRect, texture, 0, 0, 0, 0, mat, pass);
	}

	[ExcludeFromDocs]
	public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
	{
		int pass = -1;
		DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat, pass);
	}

	[ExcludeFromDocs]
	public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
	{
		int pass = -1;
		Material mat = null;
		DrawTexture(screenRect, texture, leftBorder, rightBorder, topBorder, bottomBorder, mat, pass);
	}

	public static void DrawTexture(Rect screenRect, Texture texture, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
	{
		DrawTexture(screenRect, texture, new Rect(0f, 0f, 1f, 1f), leftBorder, rightBorder, topBorder, bottomBorder, mat, pass);
	}

	[ExcludeFromDocs]
	public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Material mat)
	{
		int pass = -1;
		DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, mat, pass);
	}

	[ExcludeFromDocs]
	public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder)
	{
		int pass = -1;
		Material mat = null;
		DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, mat, pass);
	}

	public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
	{
		Color32 color = new Color32(128, 128, 128, 128);
		DrawTextureImpl(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
	}

	[ExcludeFromDocs]
	public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, Material mat)
	{
		int pass = -1;
		DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
	}

	[ExcludeFromDocs]
	public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color)
	{
		int pass = -1;
		Material mat = null;
		DrawTexture(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
	}

	public static void DrawTexture(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, [DefaultValue("null")] Material mat, [DefaultValue("-1")] int pass)
	{
		DrawTextureImpl(screenRect, texture, sourceRect, leftBorder, rightBorder, topBorder, bottomBorder, color, mat, pass);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule" })]
	[GeneratedByOldBindingsGenerator]
	internal static extern void Internal_DrawTexture(ref Internal_DrawTextureArguments args);

	[ExcludeFromDocs]
	public static GPUFence CreateGPUFence()
	{
		SynchronisationStage stage = SynchronisationStage.PixelProcessing;
		return CreateGPUFence(stage);
	}

	public static GPUFence CreateGPUFence([DefaultValue("SynchronisationStage.PixelProcessing")] SynchronisationStage stage)
	{
		GPUFence result = default(GPUFence);
		result.m_Ptr = Internal_CreateGPUFence(stage);
		result.InitPostAllocation();
		result.Validate();
		return result;
	}

	private static IntPtr Internal_CreateGPUFence(SynchronisationStage stage)
	{
		INTERNAL_CALL_Internal_CreateGPUFence(stage, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Internal_CreateGPUFence(SynchronisationStage stage, out IntPtr value);

	[ExcludeFromDocs]
	public static void WaitOnGPUFence(GPUFence fence)
	{
		SynchronisationStage stage = SynchronisationStage.VertexProcessing;
		WaitOnGPUFence(fence, stage);
	}

	public static void WaitOnGPUFence(GPUFence fence, [DefaultValue("SynchronisationStage.VertexProcessing")] SynchronisationStage stage)
	{
		fence.Validate();
		if (fence.IsFencePending())
		{
			WaitOnGPUFence_Internal(fence.m_Ptr, stage);
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void WaitOnGPUFence_Internal(IntPtr fencePtr, SynchronisationStage stage);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void ExecuteCommandBuffer(CommandBuffer buffer);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void ExecuteCommandBufferAsync(CommandBuffer buffer, ComputeQueueType queueType);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_SetNullRT();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_SetRTSimple(out RenderBuffer color, out RenderBuffer depth, int mip, CubemapFace face, int depthSlice);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_SetMRTFullSetup(RenderBuffer[] colorSA, out RenderBuffer depth, int mip, CubemapFace face, int depthSlice, RenderBufferLoadAction[] colorLoadSA, RenderBufferStoreAction[] colorStoreSA, RenderBufferLoadAction depthLoad, RenderBufferStoreAction depthStore);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_SetMRTSimple(RenderBuffer[] colorSA, out RenderBuffer depth, int mip, CubemapFace face, int depthSlice);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void GetActiveColorBuffer(out RenderBuffer res);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void GetActiveDepthBuffer(out RenderBuffer res);

	public static void SetRandomWriteTarget(int index, RenderTexture uav)
	{
		Internal_SetRandomWriteTargetRT(index, uav);
	}

	[ExcludeFromDocs]
	public static void SetRandomWriteTarget(int index, ComputeBuffer uav)
	{
		bool preserveCounterValue = false;
		SetRandomWriteTarget(index, uav, preserveCounterValue);
	}

	public static void SetRandomWriteTarget(int index, ComputeBuffer uav, [DefaultValue("false")] bool preserveCounterValue)
	{
		if (uav == null)
		{
			throw new ArgumentNullException("uav");
		}
		if (uav.m_Ptr == IntPtr.Zero)
		{
			throw new ObjectDisposedException("uav");
		}
		Internal_SetRandomWriteTargetBuffer(index, uav, preserveCounterValue);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern void ClearRandomWriteTargets();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_SetRandomWriteTargetRT(int index, RenderTexture uav);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_SetRandomWriteTargetBuffer(int index, ComputeBuffer uav, bool preserveCounterValue);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("CopyTexture")]
	private static extern void CopyTexture_Full(Texture src, Texture dst);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("CopyTexture")]
	private static extern void CopyTexture_Slice_AllMips(Texture src, int srcElement, Texture dst, int dstElement);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("CopyTexture")]
	private static extern void CopyTexture_Slice(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("CopyTexture")]
	private static extern void CopyTexture_Region(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ConvertTexture")]
	private static extern bool ConvertTexture_Full(Texture src, Texture dst);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("ConvertTexture")]
	private static extern bool ConvertTexture_Slice(Texture src, int srcElement, Texture dst, int dstElement);

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, null, 0, null, ShadowCastingMode.On, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, 0, null, ShadowCastingMode.On, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, null, ShadowCastingMode.On, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, ShadowCastingMode.On, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, LightProbeUsage.BlendProbes, null);
	}

	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("true")] bool castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("true")] bool useLightProbes)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, LightProbeUsage.BlendProbes, null);
	}

	public static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("null")] Transform probeAnchor, [DefaultValue("true")] bool useLightProbes)
	{
		DrawMesh(mesh, Matrix4x4.TRS(position, rotation, Vector3.one), material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer)
	{
		DrawMesh(mesh, matrix, material, layer, null, 0, null, ShadowCastingMode.On, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera)
	{
		DrawMesh(mesh, matrix, material, layer, camera, 0, null, ShadowCastingMode.On, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex)
	{
		DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, null, ShadowCastingMode.On, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties)
	{
		DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, ShadowCastingMode.On, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows)
	{
		DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, bool castShadows, bool receiveShadows)
	{
		DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, LightProbeUsage.BlendProbes, null);
	}

	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, [DefaultValue("null")] Camera camera, [DefaultValue("0")] int submeshIndex, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("true")] bool castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("true")] bool useLightProbes)
	{
		DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows ? ShadowCastingMode.On : ShadowCastingMode.Off, receiveShadows, null, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
	{
		DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows: true, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
	{
		DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor)
	{
		DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, LightProbeUsage.BlendProbes, null);
	}

	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("null")] Transform probeAnchor, [DefaultValue("true")] bool useLightProbes)
	{
		DrawMesh(mesh, matrix, material, layer, camera, submeshIndex, properties, castShadows, receiveShadows, probeAnchor, useLightProbes ? LightProbeUsage.BlendProbes : LightProbeUsage.Off, null);
	}

	[ExcludeFromDocs]
	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage)
	{
		Internal_DrawMesh(mesh, submeshIndex, matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, null);
	}

	public static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera, int submeshIndex, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
	{
		if (lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null)
		{
			throw new ArgumentException("lightProbeProxyVolume", "Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.");
		}
		Internal_DrawMesh(mesh, submeshIndex, matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, lightProbeProxyVolume);
	}

	[FreeFunction("DrawMeshMatrixFromScript")]
	internal static void Internal_DrawMesh(Mesh mesh, int submeshIndex, Matrix4x4 matrix, Material material, int layer, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
	{
		Internal_DrawMesh_Injected(mesh, submeshIndex, ref matrix, material, layer, camera, properties, castShadows, receiveShadows, probeAnchor, lightProbeUsage, lightProbeProxyVolume);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, matrices.Length, null, ShadowCastingMode.On, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, null, ShadowCastingMode.On, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, ShadowCastingMode.On, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
	}

	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, [DefaultValue("matrices.Length")] int count, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
	{
		if (!SystemInfo.supportsInstancing)
		{
			throw new InvalidOperationException("Instancing is not supported.");
		}
		if (mesh == null)
		{
			throw new ArgumentNullException("mesh");
		}
		if (submeshIndex < 0 || submeshIndex >= mesh.subMeshCount)
		{
			throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
		}
		if (material == null)
		{
			throw new ArgumentNullException("material");
		}
		if (!material.enableInstancing)
		{
			throw new InvalidOperationException("Material needs to enable instancing for use with DrawMeshInstanced.");
		}
		if (matrices == null)
		{
			throw new ArgumentNullException("matrices");
		}
		if (count < 0 || count > Mathf.Min(kMaxDrawMeshInstanceCount, matrices.Length))
		{
			throw new ArgumentOutOfRangeException("count", $"Count must be in the range of 0 to {Mathf.Min(kMaxDrawMeshInstanceCount, matrices.Length)}.");
		}
		if (lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null)
		{
			throw new ArgumentException("lightProbeProxyVolume", "Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.");
		}
		if (count > 0)
		{
			Internal_DrawMeshInstanced(mesh, submeshIndex, material, matrices, count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
		}
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, null, ShadowCastingMode.On, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, ShadowCastingMode.On, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, camera, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage)
	{
		DrawMeshInstanced(mesh, submeshIndex, material, matrices, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
	}

	public static void DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, List<Matrix4x4> matrices, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
	{
		if (matrices == null)
		{
			throw new ArgumentNullException("matrices");
		}
		DrawMeshInstanced(mesh, submeshIndex, material, NoAllocHelpers.ExtractArrayFromListT(matrices), matrices.Count, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("DrawMeshInstancedFromScript")]
	internal static extern void Internal_DrawMeshInstanced(Mesh mesh, int submeshIndex, Material material, Matrix4x4[] matrices, int count, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

	[ExcludeFromDocs]
	public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs)
	{
		DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, 0, null, ShadowCastingMode.On, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset)
	{
		DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, null, ShadowCastingMode.On, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties)
	{
		DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, ShadowCastingMode.On, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows)
	{
		DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows: true, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows)
	{
		DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, 0, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer)
	{
		DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, null, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera)
	{
		DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, LightProbeUsage.BlendProbes, null);
	}

	[ExcludeFromDocs]
	public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage)
	{
		DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, null);
	}

	public static void DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, [DefaultValue("0")] int argsOffset, [DefaultValue("null")] MaterialPropertyBlock properties, [DefaultValue("ShadowCastingMode.On")] ShadowCastingMode castShadows, [DefaultValue("true")] bool receiveShadows, [DefaultValue("0")] int layer, [DefaultValue("null")] Camera camera, [DefaultValue("LightProbeUsage.BlendProbes")] LightProbeUsage lightProbeUsage, [DefaultValue("null")] LightProbeProxyVolume lightProbeProxyVolume)
	{
		if (!SystemInfo.supportsInstancing)
		{
			throw new InvalidOperationException("Instancing is not supported.");
		}
		if (mesh == null)
		{
			throw new ArgumentNullException("mesh");
		}
		if (submeshIndex < 0 || submeshIndex >= mesh.subMeshCount)
		{
			throw new ArgumentOutOfRangeException("submeshIndex", "submeshIndex out of range.");
		}
		if (material == null)
		{
			throw new ArgumentNullException("material");
		}
		if (bufferWithArgs == null)
		{
			throw new ArgumentNullException("bufferWithArgs");
		}
		if (lightProbeUsage == LightProbeUsage.UseProxyVolume && lightProbeProxyVolume == null)
		{
			throw new ArgumentException("lightProbeProxyVolume", "Argument lightProbeProxyVolume must not be null if lightProbeUsage is set to UseProxyVolume.");
		}
		Internal_DrawMeshInstancedIndirect(mesh, submeshIndex, material, bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
	}

	[FreeFunction("DrawMeshInstancedIndirectFromScript")]
	internal static void Internal_DrawMeshInstancedIndirect(Mesh mesh, int submeshIndex, Material material, Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume)
	{
		Internal_DrawMeshInstancedIndirect_Injected(mesh, submeshIndex, material, ref bounds, bufferWithArgs, argsOffset, properties, castShadows, receiveShadows, layer, camera, lightProbeUsage, lightProbeProxyVolume);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GraphicsScripting::BlitMaterial")]
	private static extern void Internal_BlitMaterial(Texture source, RenderTexture dest, [NotNull] Material mat, int pass, bool setRT);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GraphicsScripting::BlitMultitap")]
	private static extern void Internal_BlitMultiTap(Texture source, RenderTexture dest, [NotNull] Material mat, [NotNull] Vector2[] offsets);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("GraphicsScripting::Blit")]
	private static extern void Blit2(Texture source, RenderTexture dest);

	[FreeFunction("GraphicsScripting::Blit")]
	private static void Blit4(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
	{
		Blit4_Injected(source, dest, ref scale, ref offset);
	}

	internal static void CheckLoadActionValid(RenderBufferLoadAction load, string bufferType)
	{
		if (load != 0 && load != RenderBufferLoadAction.DontCare)
		{
			throw new ArgumentException(UnityString.Format("Bad {0} LoadAction provided.", bufferType));
		}
	}

	internal static void CheckStoreActionValid(RenderBufferStoreAction store, string bufferType)
	{
		if (store != 0 && store != RenderBufferStoreAction.DontCare)
		{
			throw new ArgumentException(UnityString.Format("Bad {0} StoreAction provided.", bufferType));
		}
	}

	internal static void SetRenderTargetImpl(RenderTargetSetup setup)
	{
		if (setup.color.Length == 0)
		{
			throw new ArgumentException("Invalid color buffer count for SetRenderTarget");
		}
		if (setup.color.Length != setup.colorLoad.Length)
		{
			throw new ArgumentException("Color LoadAction and Buffer arrays have different sizes");
		}
		if (setup.color.Length != setup.colorStore.Length)
		{
			throw new ArgumentException("Color StoreAction and Buffer arrays have different sizes");
		}
		RenderBufferLoadAction[] colorLoad = setup.colorLoad;
		foreach (RenderBufferLoadAction load in colorLoad)
		{
			CheckLoadActionValid(load, "Color");
		}
		RenderBufferStoreAction[] colorStore = setup.colorStore;
		foreach (RenderBufferStoreAction store in colorStore)
		{
			CheckStoreActionValid(store, "Color");
		}
		CheckLoadActionValid(setup.depthLoad, "Depth");
		CheckStoreActionValid(setup.depthStore, "Depth");
		if (setup.cubemapFace < CubemapFace.Unknown || setup.cubemapFace > CubemapFace.NegativeZ)
		{
			throw new ArgumentException("Bad CubemapFace provided");
		}
		Internal_SetMRTFullSetup(setup.color, out setup.depth, setup.mipLevel, setup.cubemapFace, setup.depthSlice, setup.colorLoad, setup.colorStore, setup.depthLoad, setup.depthStore);
	}

	internal static void SetRenderTargetImpl(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
	{
		RenderBuffer color = colorBuffer;
		RenderBuffer depth = depthBuffer;
		Internal_SetRTSimple(out color, out depth, mipLevel, face, depthSlice);
	}

	internal static void SetRenderTargetImpl(RenderTexture rt, int mipLevel, CubemapFace face, int depthSlice)
	{
		if ((bool)rt)
		{
			SetRenderTargetImpl(rt.colorBuffer, rt.depthBuffer, mipLevel, face, depthSlice);
		}
		else
		{
			Internal_SetNullRT();
		}
	}

	internal static void SetRenderTargetImpl(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
	{
		RenderBuffer depth = depthBuffer;
		Internal_SetMRTSimple(colorBuffers, out depth, mipLevel, face, depthSlice);
	}

	public static void SetRenderTarget(RenderTexture rt)
	{
		SetRenderTargetImpl(rt, 0, CubemapFace.Unknown, 0);
	}

	public static void SetRenderTarget(RenderTexture rt, int mipLevel)
	{
		SetRenderTargetImpl(rt, mipLevel, CubemapFace.Unknown, 0);
	}

	public static void SetRenderTarget(RenderTexture rt, int mipLevel, CubemapFace face)
	{
		SetRenderTargetImpl(rt, mipLevel, face, 0);
	}

	public static void SetRenderTarget(RenderTexture rt, int mipLevel, CubemapFace face, int depthSlice)
	{
		SetRenderTargetImpl(rt, mipLevel, face, depthSlice);
	}

	public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
	{
		SetRenderTargetImpl(colorBuffer, depthBuffer, 0, CubemapFace.Unknown, 0);
	}

	public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel)
	{
		SetRenderTargetImpl(colorBuffer, depthBuffer, mipLevel, CubemapFace.Unknown, 0);
	}

	public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face)
	{
		SetRenderTargetImpl(colorBuffer, depthBuffer, mipLevel, face, 0);
	}

	public static void SetRenderTarget(RenderBuffer colorBuffer, RenderBuffer depthBuffer, int mipLevel, CubemapFace face, int depthSlice)
	{
		SetRenderTargetImpl(colorBuffer, depthBuffer, mipLevel, face, depthSlice);
	}

	public static void SetRenderTarget(RenderBuffer[] colorBuffers, RenderBuffer depthBuffer)
	{
		SetRenderTargetImpl(colorBuffers, depthBuffer, 0, CubemapFace.Unknown, 0);
	}

	public static void SetRenderTarget(RenderTargetSetup setup)
	{
		SetRenderTargetImpl(setup);
	}

	public static void CopyTexture(Texture src, Texture dst)
	{
		CopyTexture_Full(src, dst);
	}

	public static void CopyTexture(Texture src, int srcElement, Texture dst, int dstElement)
	{
		CopyTexture_Slice_AllMips(src, srcElement, dst, dstElement);
	}

	public static void CopyTexture(Texture src, int srcElement, int srcMip, Texture dst, int dstElement, int dstMip)
	{
		CopyTexture_Slice(src, srcElement, srcMip, dst, dstElement, dstMip);
	}

	public static void CopyTexture(Texture src, int srcElement, int srcMip, int srcX, int srcY, int srcWidth, int srcHeight, Texture dst, int dstElement, int dstMip, int dstX, int dstY)
	{
		CopyTexture_Region(src, srcElement, srcMip, srcX, srcY, srcWidth, srcHeight, dst, dstElement, dstMip, dstX, dstY);
	}

	public static bool ConvertTexture(Texture src, Texture dst)
	{
		return ConvertTexture_Full(src, dst);
	}

	public static bool ConvertTexture(Texture src, int srcElement, Texture dst, int dstElement)
	{
		return ConvertTexture_Slice(src, srcElement, dst, dstElement);
	}

	private static void DrawTextureImpl(Rect screenRect, Texture texture, Rect sourceRect, int leftBorder, int rightBorder, int topBorder, int bottomBorder, Color color, Material mat, int pass)
	{
		Internal_DrawTextureArguments args = default(Internal_DrawTextureArguments);
		args.screenRect = screenRect;
		args.sourceRect = sourceRect;
		args.leftBorder = leftBorder;
		args.rightBorder = rightBorder;
		args.topBorder = topBorder;
		args.bottomBorder = bottomBorder;
		args.color = color;
		args.pass = pass;
		args.texture = texture;
		args.mat = mat;
		Internal_DrawTexture(ref args);
	}

	public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation)
	{
		DrawMeshNow(mesh, position, rotation, -1);
	}

	public static void DrawMeshNow(Mesh mesh, Vector3 position, Quaternion rotation, int materialIndex)
	{
		if (mesh == null)
		{
			throw new ArgumentNullException("mesh");
		}
		Internal_DrawMeshNow1(mesh, materialIndex, position, rotation);
	}

	public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix)
	{
		DrawMeshNow(mesh, matrix, -1);
	}

	public static void DrawMeshNow(Mesh mesh, Matrix4x4 matrix, int materialIndex)
	{
		if (mesh == null)
		{
			throw new ArgumentNullException("mesh");
		}
		Internal_DrawMeshNow2(mesh, materialIndex, matrix);
	}

	public static void Blit(Texture source, RenderTexture dest)
	{
		Blit2(source, dest);
	}

	public static void Blit(Texture source, RenderTexture dest, Vector2 scale, Vector2 offset)
	{
		Blit4(source, dest, scale, offset);
	}

	public static void Blit(Texture source, RenderTexture dest, Material mat, [DefaultValue("-1")] int pass)
	{
		Internal_BlitMaterial(source, dest, mat, pass, setRT: true);
	}

	public static void Blit(Texture source, RenderTexture dest, Material mat)
	{
		Blit(source, dest, mat, -1);
	}

	public static void Blit(Texture source, Material mat, [DefaultValue("-1")] int pass)
	{
		Internal_BlitMaterial(source, null, mat, pass, setRT: false);
	}

	public static void Blit(Texture source, Material mat)
	{
		Blit(source, mat, -1);
	}

	public static void BlitMultiTap(Texture source, RenderTexture dest, Material mat, params Vector2[] offsets)
	{
		if (offsets.Length == 0)
		{
			throw new ArgumentException("empty offsets list passed.", "offsets");
		}
		Internal_BlitMultiTap(source, dest, mat, offsets);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_DrawMesh_Injected(Mesh mesh, int submeshIndex, ref Matrix4x4 matrix, Material material, int layer, Camera camera, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, Transform probeAnchor, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Internal_DrawMeshInstancedIndirect_Injected(Mesh mesh, int submeshIndex, Material material, ref Bounds bounds, ComputeBuffer bufferWithArgs, int argsOffset, MaterialPropertyBlock properties, ShadowCastingMode castShadows, bool receiveShadows, int layer, Camera camera, LightProbeUsage lightProbeUsage, LightProbeProxyVolume lightProbeProxyVolume);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Blit4_Injected(Texture source, RenderTexture dest, ref Vector2 scale, ref Vector2 offset);
}
