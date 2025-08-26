using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeClass("UI::CanvasRenderer")]
public sealed class CanvasRenderer : Component
{
	[Obsolete("isMask is no longer supported. See EnableClipping for vertex clipping configuration")]
	public extern bool isMask
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool hasRectClipping
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool hasPopInstruction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern int materialCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern int popMaterialCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern int relativeDepth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool cull
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern int absoluteDepth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool hasMoved
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public void SetColor(Color color)
	{
		INTERNAL_CALL_SetColor(this, ref color);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_SetColor(CanvasRenderer self, ref Color color);

	public Color GetColor()
	{
		INTERNAL_CALL_GetColor(this, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_GetColor(CanvasRenderer self, out Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern float GetAlpha();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetAlpha(float alpha);

	[Obsolete("UI System now uses meshes. Generate a mesh and use 'SetMesh' instead")]
	public void SetVertices(List<UIVertex> vertices)
	{
		SetVertices(vertices.ToArray(), vertices.Count);
	}

	[Obsolete("UI System now uses meshes. Generate a mesh and use 'SetMesh' instead")]
	public void SetVertices(UIVertex[] vertices, int size)
	{
		Mesh mesh = new Mesh();
		List<Vector3> list = new List<Vector3>();
		List<Color32> list2 = new List<Color32>();
		List<Vector2> list3 = new List<Vector2>();
		List<Vector2> list4 = new List<Vector2>();
		List<Vector2> list5 = new List<Vector2>();
		List<Vector2> list6 = new List<Vector2>();
		List<Vector3> list7 = new List<Vector3>();
		List<Vector4> list8 = new List<Vector4>();
		List<int> list9 = new List<int>();
		for (int i = 0; i < size; i += 4)
		{
			for (int j = 0; j < 4; j++)
			{
				list.Add(vertices[i + j].position);
				list2.Add(vertices[i + j].color);
				list3.Add(vertices[i + j].uv0);
				list4.Add(vertices[i + j].uv1);
				list5.Add(vertices[i + j].uv2);
				list6.Add(vertices[i + j].uv3);
				list7.Add(vertices[i + j].normal);
				list8.Add(vertices[i + j].tangent);
			}
			list9.Add(i);
			list9.Add(i + 1);
			list9.Add(i + 2);
			list9.Add(i + 2);
			list9.Add(i + 3);
			list9.Add(i);
		}
		mesh.SetVertices(list);
		mesh.SetColors(list2);
		mesh.SetNormals(list7);
		mesh.SetTangents(list8);
		mesh.SetUVs(0, list3);
		mesh.SetUVs(1, list4);
		mesh.SetUVs(2, list5);
		mesh.SetUVs(3, list6);
		mesh.SetIndices(list9.ToArray(), MeshTopology.Triangles, 0);
		SetMesh(mesh);
		Object.DestroyImmediate(mesh);
	}

	public void EnableRectClipping(Rect rect)
	{
		INTERNAL_CALL_EnableRectClipping(this, ref rect);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_EnableRectClipping(CanvasRenderer self, ref Rect rect);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void DisableRectClipping();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetMaterial(Material material, int index);

	public void SetMaterial(Material material, Texture texture)
	{
		materialCount = Math.Max(1, materialCount);
		SetMaterial(material, 0);
		SetTexture(texture);
	}

	public Material GetMaterial()
	{
		return GetMaterial(0);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern Material GetMaterial(int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetPopMaterial(Material material, int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern Material GetPopMaterial(int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetTexture(Texture texture);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetAlphaTexture(Texture texture);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetMesh(Mesh mesh);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void Clear();

	public static void SplitUIVertexStreams(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector2> uv0S, List<Vector2> uv1S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
	{
		SplitUIVertexStreams(verts, positions, colors, uv0S, uv1S, new List<Vector2>(), new List<Vector2>(), normals, tangents, indices);
	}

	public static void SplitUIVertexStreams(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector2> uv0S, List<Vector2> uv1S, List<Vector2> uv2S, List<Vector2> uv3S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
	{
		SplitUIVertexStreamsInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents);
		SplitIndicesStreamsInternal(verts, indices);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void SplitUIVertexStreamsInternal(object verts, object positions, object colors, object uv0S, object uv1S, object uv2S, object uv3S, object normals, object tangents);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void SplitIndicesStreamsInternal(object verts, object indices);

	public static void CreateUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector2> uv0S, List<Vector2> uv1S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
	{
		CreateUIVertexStream(verts, positions, colors, uv0S, uv1S, new List<Vector2>(), new List<Vector2>(), normals, tangents, indices);
	}

	public static void CreateUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector2> uv0S, List<Vector2> uv1S, List<Vector2> uv2S, List<Vector2> uv3S, List<Vector3> normals, List<Vector4> tangents, List<int> indices)
	{
		CreateUIVertexStreamInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents, indices);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void CreateUIVertexStreamInternal(object verts, object positions, object colors, object uv0S, object uv1S, object uv2S, object uv3S, object normals, object tangents, object indices);

	public static void AddUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector2> uv0S, List<Vector2> uv1S, List<Vector3> normals, List<Vector4> tangents)
	{
		AddUIVertexStream(verts, positions, colors, uv0S, uv1S, new List<Vector2>(), new List<Vector2>(), normals, tangents);
	}

	public static void AddUIVertexStream(List<UIVertex> verts, List<Vector3> positions, List<Color32> colors, List<Vector2> uv0S, List<Vector2> uv1S, List<Vector2> uv2S, List<Vector2> uv3S, List<Vector3> normals, List<Vector4> tangents)
	{
		SplitUIVertexStreamsInternal(verts, positions, colors, uv0S, uv1S, uv2S, uv3S, normals, tangents);
	}
}
