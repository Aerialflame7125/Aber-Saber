using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeHeader("Runtime/2D/Common/ScriptBindings/SpritesMarshalling.h")]
[NativeHeader("Runtime/Graphics/SpriteUtility.h")]
[NativeType("Runtime/Graphics/SpriteFrame.h")]
public sealed class Sprite : Object
{
	public Bounds bounds
	{
		get
		{
			INTERNAL_get_bounds(out var value);
			return value;
		}
	}

	public Rect rect
	{
		get
		{
			INTERNAL_get_rect(out var value);
			return value;
		}
	}

	public extern Texture2D texture
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern Texture2D associatedAlphaSplitTexture
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public Rect textureRect
	{
		get
		{
			INTERNAL_get_textureRect(out var value);
			return value;
		}
	}

	public Vector2 textureRectOffset
	{
		get
		{
			Internal_GetTextureRectOffset(this, out var output);
			return output;
		}
	}

	public extern bool packed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern SpritePackingMode packingMode
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern SpritePackingRotation packingRotation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public Vector2 pivot
	{
		get
		{
			Internal_GetPivot(this, out var output);
			return output;
		}
	}

	public Vector4 border
	{
		get
		{
			INTERNAL_get_border(out var value);
			return value;
		}
	}

	public extern Vector2[] vertices
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern ushort[] triangles
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern Vector2[] uv
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern float pixelsPerUnit
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetPixelsToUnits")]
		get;
	}

	private Sprite()
	{
	}

	public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, [DefaultValue("100.0f")] float pixelsPerUnit, [DefaultValue("0")] uint extrude, [DefaultValue("SpriteMeshType.Tight")] SpriteMeshType meshType, [DefaultValue("Vector4.zero")] Vector4 border, [DefaultValue("false")] bool generateFallbackPhysicsShape)
	{
		return INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref border, generateFallbackPhysicsShape);
	}

	[ExcludeFromDocs]
	public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border)
	{
		bool generateFallbackPhysicsShape = false;
		return INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref border, generateFallbackPhysicsShape);
	}

	[ExcludeFromDocs]
	public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType)
	{
		bool generateFallbackPhysicsShape = false;
		Vector4 zero = Vector4.zero;
		return INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref zero, generateFallbackPhysicsShape);
	}

	[ExcludeFromDocs]
	public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude)
	{
		bool generateFallbackPhysicsShape = false;
		Vector4 zero = Vector4.zero;
		SpriteMeshType meshType = SpriteMeshType.Tight;
		return INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref zero, generateFallbackPhysicsShape);
	}

	[ExcludeFromDocs]
	public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit)
	{
		bool generateFallbackPhysicsShape = false;
		Vector4 zero = Vector4.zero;
		SpriteMeshType meshType = SpriteMeshType.Tight;
		uint extrude = 0u;
		return INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref zero, generateFallbackPhysicsShape);
	}

	[ExcludeFromDocs]
	public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot)
	{
		bool generateFallbackPhysicsShape = false;
		Vector4 zero = Vector4.zero;
		SpriteMeshType meshType = SpriteMeshType.Tight;
		uint extrude = 0u;
		float num = 100f;
		return INTERNAL_CALL_Create(texture, ref rect, ref pivot, num, extrude, meshType, ref zero, generateFallbackPhysicsShape);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern Sprite INTERNAL_CALL_Create(Texture2D texture, ref Rect rect, ref Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, ref Vector4 border, bool generateFallbackPhysicsShape);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_bounds(out Bounds value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_rect(out Rect value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_textureRect(out Rect value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_GetTextureRectOffset(Sprite sprite, out Vector2 output);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_GetPivot(Sprite sprite, out Vector2 output);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_border(out Vector4 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void OverrideGeometry(Vector2[] vertices, ushort[] triangles);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern int GetPhysicsShapeCount();

	public int GetPhysicsShapePointCount(int shapeIdx)
	{
		int physicsShapeCount = GetPhysicsShapeCount();
		if (shapeIdx < 0 || shapeIdx >= physicsShapeCount)
		{
			throw new IndexOutOfRangeException($"Index({shapeIdx}) is out of bounds(0 - {physicsShapeCount - 1})");
		}
		return Internal_GetPhysicsShapePointCount(shapeIdx);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("GetPhysicsShapePointCount")]
	private extern int Internal_GetPhysicsShapePointCount(int shapeIdx);

	public int GetPhysicsShape(int shapeIdx, List<Vector2> physicsShape)
	{
		int physicsShapeCount = GetPhysicsShapeCount();
		if (shapeIdx < 0 || shapeIdx >= physicsShapeCount)
		{
			throw new IndexOutOfRangeException($"Index({shapeIdx}) is out of bounds(0 - {physicsShapeCount - 1})");
		}
		GetPhysicsShapeImpl(this, shapeIdx, physicsShape);
		return physicsShape.Count;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("SpritesBindings::GetPhysicsShape", ThrowsException = true)]
	private static extern void GetPhysicsShapeImpl(Sprite sprite, int shapeIdx, List<Vector2> physicsShape);

	public void OverridePhysicsShape(IList<Vector2[]> physicsShapes)
	{
		for (int i = 0; i < physicsShapes.Count; i++)
		{
			Vector2[] array = physicsShapes[i];
			if (array == null)
			{
				throw new ArgumentNullException($"Physics Shape at {i} is null.");
			}
			if (array.Length < 3)
			{
				throw new ArgumentException($"Physics Shape at {i} has less than 3 vertices ({array.Length}).");
			}
		}
		OverridePhysicsShapeCount(this, physicsShapes.Count);
		for (int j = 0; j < physicsShapes.Count; j++)
		{
			OverridePhysicsShape(this, physicsShapes[j], j);
		}
	}

	[FreeFunction("CreateSpriteWithoutTextureScripting")]
	internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture = null)
	{
		return Create_Injected(ref rect, ref pivot, pixelsToUnits, texture);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("SpritesBindings::OverridePhysicsShapeCount")]
	private static extern void OverridePhysicsShapeCount(Sprite sprite, int physicsShapeCount);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[FreeFunction("SpritesBindings::OverridePhysicsShape", ThrowsException = true)]
	private static extern void OverridePhysicsShape(Sprite sprite, Vector2[] physicsShape, int idx);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Sprite Create_Injected(ref Rect rect, ref Vector2 pivot, float pixelsToUnits, Texture2D texture = null);
}
