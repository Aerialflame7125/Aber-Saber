using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine;

[NativeHeader("Modules/Physics2D/Public/PolygonCollider2D.h")]
public sealed class PolygonCollider2D : Collider2D
{
	public extern bool autoTiling
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern Vector2[] points
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetPoints_Binding")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetPoints_Binding")]
		set;
	}

	public extern int pathCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetPointCount")]
	public extern int GetTotalPointCount();

	public Vector2[] GetPath(int index)
	{
		if (index >= pathCount)
		{
			throw new ArgumentOutOfRangeException($"Path {index} does not exist.");
		}
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException($"Path {index} does not exist; negative path index is invalid.");
		}
		return GetPath_Internal(index);
	}

	public void SetPath(int index, Vector2[] points)
	{
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException(string.Format("Negative path index is invalid.", index));
		}
		SetPath_Internal(index, points);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetPath_Binding")]
	private extern Vector2[] GetPath_Internal(int index);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("SetPath_Binding")]
	private extern void SetPath_Internal(int index, Vector2[] points);

	[ExcludeFromDocs]
	public void CreatePrimitive(int sides)
	{
		CreatePrimitive(sides, Vector2.one, Vector2.zero);
	}

	[ExcludeFromDocs]
	public void CreatePrimitive(int sides, Vector2 scale)
	{
		CreatePrimitive(sides, scale, Vector2.zero);
	}

	public void CreatePrimitive(int sides, [DefaultValue("Vector2.one")] Vector2 scale, [DefaultValue("Vector2.zero")] Vector2 offset)
	{
		if (sides < 3)
		{
			Debug.LogWarning("Cannot create a 2D polygon primitive collider with less than two sides.", this);
		}
		else if (!(scale.x > 0f) || !(scale.y > 0f))
		{
			Debug.LogWarning("Cannot create a 2D polygon primitive collider with an axis scale less than or equal to zero.", this);
		}
		else
		{
			CreatePrimitive_Internal(sides, scale, offset, autoRefresh: true);
		}
	}

	[NativeMethod("CreatePrimitive")]
	private void CreatePrimitive_Internal(int sides, [DefaultValue("Vector2.one")] Vector2 scale, [DefaultValue("Vector2.zero")] Vector2 offset, bool autoRefresh)
	{
		CreatePrimitive_Internal_Injected(sides, ref scale, ref offset, autoRefresh);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern void CreatePrimitive_Internal_Injected(int sides, [DefaultValue("Vector2.one")] ref Vector2 scale, [DefaultValue("Vector2.zero")] ref Vector2 offset, bool autoRefresh);
}
