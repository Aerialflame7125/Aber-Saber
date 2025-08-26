using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

[RequireComponent(typeof(Transform))]
[RequiredByNativeCode]
public class Collider : Component
{
	public extern bool enabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern Rigidbody attachedRigidbody
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool isTrigger
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float contactOffset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern PhysicMaterial material
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern PhysicMaterial sharedMaterial
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public Bounds bounds
	{
		get
		{
			INTERNAL_get_bounds(out var value);
			return value;
		}
	}

	public Vector3 ClosestPointOnBounds(Vector3 position)
	{
		INTERNAL_CALL_ClosestPointOnBounds(this, ref position, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_ClosestPointOnBounds(Collider self, ref Vector3 position, out Vector3 value);

	public Vector3 ClosestPoint(Vector3 position)
	{
		INTERNAL_CALL_ClosestPoint(this, ref position, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_ClosestPoint(Collider self, ref Vector3 position, out Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_bounds(out Bounds value);

	private static bool Internal_Raycast(Collider col, Ray ray, out RaycastHit hitInfo, float maxDistance)
	{
		return INTERNAL_CALL_Internal_Raycast(col, ref ray, out hitInfo, maxDistance);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool INTERNAL_CALL_Internal_Raycast(Collider col, ref Ray ray, out RaycastHit hitInfo, float maxDistance);

	public bool Raycast(Ray ray, out RaycastHit hitInfo, float maxDistance)
	{
		return Internal_Raycast(this, ray, out hitInfo, maxDistance);
	}
}
