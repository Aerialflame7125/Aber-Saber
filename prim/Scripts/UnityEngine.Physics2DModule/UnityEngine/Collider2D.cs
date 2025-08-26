using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine;

[NativeHeader("Modules/Physics2D/Public/Collider2D.h")]
[RequireComponent(typeof(Transform))]
public class Collider2D : Behaviour
{
	public extern float density
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool isTrigger
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool usedByEffector
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool usedByComposite
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern CompositeCollider2D composite
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public Vector2 offset
	{
		get
		{
			get_offset_Injected(out var ret);
			return ret;
		}
		set
		{
			set_offset_Injected(ref value);
		}
	}

	public extern Rigidbody2D attachedRigidbody
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetAttachedRigidbody_Binding")]
		get;
	}

	public extern int shapeCount
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public Bounds bounds
	{
		get
		{
			get_bounds_Injected(out var ret);
			return ret;
		}
	}

	internal extern ColliderErrorState2D errorState
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	internal extern bool compositeCapable
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetCompositeCapable_Binding")]
		get;
	}

	public extern PhysicsMaterial2D sharedMaterial
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("GetMaterial")]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[NativeMethod("SetMaterial")]
		set;
	}

	public extern float friction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern float bounciness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsTouching([Writable][NotNull] Collider2D collider);

	public bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
	{
		return IsTouching_OtherColliderWithFilter(collider, contactFilter);
	}

	[NativeMethod("IsTouching")]
	private bool IsTouching_OtherColliderWithFilter([NotNull][Writable] Collider2D collider, ContactFilter2D contactFilter)
	{
		return IsTouching_OtherColliderWithFilter_Injected(collider, ref contactFilter);
	}

	public bool IsTouching(ContactFilter2D contactFilter)
	{
		return IsTouching_AnyColliderWithFilter(contactFilter);
	}

	[NativeMethod("IsTouching")]
	private bool IsTouching_AnyColliderWithFilter(ContactFilter2D contactFilter)
	{
		return IsTouching_AnyColliderWithFilter_Injected(ref contactFilter);
	}

	[ExcludeFromDocs]
	public bool IsTouchingLayers()
	{
		return IsTouchingLayers(-1);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern bool IsTouchingLayers([DefaultValue("Physics2D.AllLayers")] int layerMask);

	public bool OverlapPoint(Vector2 point)
	{
		return OverlapPoint_Injected(ref point);
	}

	public ColliderDistance2D Distance([Writable] Collider2D collider)
	{
		return Physics2D.Distance(this, collider);
	}

	public int OverlapCollider(ContactFilter2D contactFilter, Collider2D[] results)
	{
		return Physics2D.OverlapCollider(this, contactFilter, results);
	}

	public int GetContacts(ContactPoint2D[] contacts)
	{
		return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), contacts);
	}

	public int GetContacts(ContactFilter2D contactFilter, ContactPoint2D[] contacts)
	{
		return Physics2D.GetContacts(this, contactFilter, contacts);
	}

	public int GetContacts(Collider2D[] colliders)
	{
		return Physics2D.GetContacts(this, default(ContactFilter2D).NoFilter(), colliders);
	}

	public int GetContacts(ContactFilter2D contactFilter, Collider2D[] colliders)
	{
		return Physics2D.GetContacts(this, contactFilter, colliders);
	}

	[ExcludeFromDocs]
	public int Cast(Vector2 direction, RaycastHit2D[] results)
	{
		ContactFilter2D contactFilter = default(ContactFilter2D);
		contactFilter.useTriggers = Physics2D.queriesHitTriggers;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
		return Cast_Internal(direction, float.PositiveInfinity, contactFilter, ignoreSiblingColliders: true, results);
	}

	[ExcludeFromDocs]
	public int Cast(Vector2 direction, RaycastHit2D[] results, float distance)
	{
		ContactFilter2D contactFilter = default(ContactFilter2D);
		contactFilter.useTriggers = Physics2D.queriesHitTriggers;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
		return Cast_Internal(direction, distance, contactFilter, ignoreSiblingColliders: true, results);
	}

	public int Cast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("true")] bool ignoreSiblingColliders)
	{
		ContactFilter2D contactFilter = default(ContactFilter2D);
		contactFilter.useTriggers = Physics2D.queriesHitTriggers;
		contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(base.gameObject.layer));
		return Cast_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
	}

	[ExcludeFromDocs]
	public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return Cast_Internal(direction, float.PositiveInfinity, contactFilter, ignoreSiblingColliders: true, results);
	}

	[ExcludeFromDocs]
	public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, float distance)
	{
		return Cast_Internal(direction, distance, contactFilter, ignoreSiblingColliders: true, results);
	}

	public int Cast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("true")] bool ignoreSiblingColliders)
	{
		return Cast_Internal(direction, distance, contactFilter, ignoreSiblingColliders, results);
	}

	[NativeMethod("Cast_Binding")]
	private int Cast_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, bool ignoreSiblingColliders, [Out] RaycastHit2D[] results)
	{
		return Cast_Internal_Injected(ref direction, distance, ref contactFilter, ignoreSiblingColliders, results);
	}

	[ExcludeFromDocs]
	public int Raycast(Vector2 direction, RaycastHit2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
		return Raycast_Internal(direction, float.PositiveInfinity, contactFilter, results);
	}

	[ExcludeFromDocs]
	public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-1, float.NegativeInfinity, float.PositiveInfinity);
		return Raycast_Internal(direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return Raycast_Internal(direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public int Raycast(Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return Raycast_Internal(direction, distance, contactFilter, results);
	}

	public int Raycast(Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("Physics2D.AllLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return Raycast_Internal(direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return Raycast_Internal(direction, float.PositiveInfinity, contactFilter, results);
	}

	public int Raycast(Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
	{
		return Raycast_Internal(direction, distance, contactFilter, results);
	}

	[NativeMethod("Raycast_Binding")]
	private int Raycast_Internal(Vector2 direction, float distance, ContactFilter2D contactFilter, [Out] RaycastHit2D[] results)
	{
		return Raycast_Internal_Injected(ref direction, distance, ref contactFilter, results);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_offset_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_offset_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_bounds_Injected(out Bounds ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool IsTouching_OtherColliderWithFilter_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool IsTouching_AnyColliderWithFilter_Injected(ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern bool OverlapPoint_Injected(ref Vector2 point);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int Cast_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, bool ignoreSiblingColliders, [Out] RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private extern int Raycast_Internal_Injected(ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, [Out] RaycastHit2D[] results);
}
