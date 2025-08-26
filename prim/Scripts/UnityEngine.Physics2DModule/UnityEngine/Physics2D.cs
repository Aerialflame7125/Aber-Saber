using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeHeader("Physics2DScriptingClasses.h")]
[NativeHeader("Physics2DScriptingClasses.h")]
[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
[NativeHeader("Modules/Physics2D/PhysicsManager2D.h")]
public class Physics2D
{
	public const int IgnoreRaycastLayer = 4;

	public const int DefaultRaycastLayers = -5;

	public const int AllLayers = -1;

	private static List<Rigidbody2D> m_LastDisabledRigidbody2D = new List<Rigidbody2D>();

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern int velocityIterations
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern int positionIterations
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static Vector2 gravity
	{
		get
		{
			get_gravity_Injected(out var ret);
			return ret;
		}
		set
		{
			set_gravity_Injected(ref value);
		}
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool queriesHitTriggers
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool queriesStartInColliders
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool callbacksOnDisable
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool autoSyncTransforms
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool autoSimulation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static PhysicsJobOptions2D jobOptions
	{
		get
		{
			get_jobOptions_Injected(out var ret);
			return ret;
		}
		set
		{
			set_jobOptions_Injected(ref value);
		}
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float velocityThreshold
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float maxLinearCorrection
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float maxAngularCorrection
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float maxTranslationSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float maxRotationSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float defaultContactOffset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float baumgarteScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float baumgarteTOIScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float timeToSleep
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float linearSleepTolerance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float angularSleepTolerance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool alwaysShowColliders
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool showColliderSleep
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool showColliderContacts
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern bool showColliderAABB
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static extern float contactArrowScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static Color colliderAwakeColor
	{
		get
		{
			get_colliderAwakeColor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_colliderAwakeColor_Injected(ref value);
		}
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static Color colliderAsleepColor
	{
		get
		{
			get_colliderAsleepColor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_colliderAsleepColor_Injected(ref value);
		}
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static Color colliderContactColor
	{
		get
		{
			get_colliderContactColor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_colliderContactColor_Injected(ref value);
		}
	}

	[StaticAccessor("GetPhysics2DSettings()")]
	public static Color colliderAABBColor
	{
		get
		{
			get_colliderAABBColor_Injected(out var ret);
			return ret;
		}
		set
		{
			set_colliderAABBColor_Injected(ref value);
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("Simulate_Binding")]
	public static extern bool Simulate(float step);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void SyncTransforms();

	[ExcludeFromDocs]
	public static void IgnoreCollision([Writable] Collider2D collider1, [Writable] Collider2D collider2)
	{
		IgnoreCollision(collider1, collider2, ignore: true);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern void IgnoreCollision([Writable][NotNull] Collider2D collider1, [Writable][NotNull] Collider2D collider2, [DefaultValue("true")] bool ignore);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool GetIgnoreCollision([Writable] Collider2D collider1, [Writable] Collider2D collider2);

	[ExcludeFromDocs]
	public static void IgnoreLayerCollision(int layer1, int layer2)
	{
		IgnoreLayerCollision(layer1, layer2, ignore: true);
	}

	public static void IgnoreLayerCollision(int layer1, int layer2, bool ignore)
	{
		if (layer1 < 0 || layer1 > 31)
		{
			throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		if (layer2 < 0 || layer2 > 31)
		{
			throw new ArgumentOutOfRangeException("layer2 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		IgnoreLayerCollision_Internal(layer1, layer2, ignore);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("GetPhysics2DSettings()")]
	[NativeMethod("IgnoreLayerCollision")]
	private static extern void IgnoreLayerCollision_Internal(int layer1, int layer2, bool ignore);

	public static bool GetIgnoreLayerCollision(int layer1, int layer2)
	{
		if (layer1 < 0 || layer1 > 31)
		{
			throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		if (layer2 < 0 || layer2 > 31)
		{
			throw new ArgumentOutOfRangeException("layer2 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		return GetIgnoreLayerCollision_Internal(layer1, layer2);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("GetPhysics2DSettings()")]
	[NativeMethod("GetIgnoreLayerCollision")]
	private static extern bool GetIgnoreLayerCollision_Internal(int layer1, int layer2);

	public static void SetLayerCollisionMask(int layer, int layerMask)
	{
		if (layer < 0 || layer > 31)
		{
			throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		SetLayerCollisionMask_Internal(layer, layerMask);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("GetPhysics2DSettings()")]
	[NativeMethod("SetLayerCollisionMask")]
	private static extern void SetLayerCollisionMask_Internal(int layer, int layerMask);

	public static int GetLayerCollisionMask(int layer)
	{
		if (layer < 0 || layer > 31)
		{
			throw new ArgumentOutOfRangeException("layer1 is out of range. Layer numbers must be in the range 0 to 31.");
		}
		return GetLayerCollisionMask_Internal(layer);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[StaticAccessor("GetPhysics2DSettings()")]
	[NativeMethod("GetLayerCollisionMask")]
	private static extern int GetLayerCollisionMask_Internal(int layer);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool IsTouching([NotNull][Writable] Collider2D collider1, [NotNull][Writable] Collider2D collider2);

	public static bool IsTouching([Writable] Collider2D collider1, [Writable] Collider2D collider2, ContactFilter2D contactFilter)
	{
		return IsTouching_TwoCollidersWithFilter(collider1, collider2, contactFilter);
	}

	[NativeMethod("IsTouching")]
	private static bool IsTouching_TwoCollidersWithFilter([Writable][NotNull] Collider2D collider1, [Writable][NotNull] Collider2D collider2, ContactFilter2D contactFilter)
	{
		return IsTouching_TwoCollidersWithFilter_Injected(collider1, collider2, ref contactFilter);
	}

	public static bool IsTouching([Writable] Collider2D collider, ContactFilter2D contactFilter)
	{
		return IsTouching_SingleColliderWithFilter(collider, contactFilter);
	}

	[NativeMethod("IsTouching")]
	private static bool IsTouching_SingleColliderWithFilter([NotNull][Writable] Collider2D collider, ContactFilter2D contactFilter)
	{
		return IsTouching_SingleColliderWithFilter_Injected(collider, ref contactFilter);
	}

	[ExcludeFromDocs]
	public static bool IsTouchingLayers([Writable] Collider2D collider)
	{
		return IsTouchingLayers(collider, -1);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	public static extern bool IsTouchingLayers([NotNull][Writable] Collider2D collider, [DefaultValue("Physics2D.AllLayers")] int layerMask);

	public static ColliderDistance2D Distance([Writable] Collider2D colliderA, [Writable] Collider2D colliderB)
	{
		if (colliderA == null)
		{
			throw new ArgumentNullException("ColliderA cannot be NULL.");
		}
		if (colliderB == null)
		{
			throw new ArgumentNullException("ColliderB cannot be NULL.");
		}
		if (colliderA == colliderB)
		{
			throw new ArgumentException("Cannot calculate the distance between the same collider.");
		}
		return Distance_Internal(colliderA, colliderB);
	}

	[NativeMethod("Distance")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static ColliderDistance2D Distance_Internal([NotNull][Writable] Collider2D colliderA, [NotNull][Writable] Collider2D colliderB)
	{
		Distance_Internal_Injected(colliderA, colliderB, out var ret);
		return ret;
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Linecast(Vector2 start, Vector2 end)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return Linecast_Internal(start, end, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return Linecast_Internal(start, end, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return Linecast_Internal(start, end, contactFilter);
	}

	public static RaycastHit2D Linecast(Vector2 start, Vector2 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return Linecast_Internal(start, end, contactFilter);
	}

	public static int Linecast(Vector2 start, Vector2 end, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return LinecastNonAlloc_Internal(start, end, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return LinecastAll_Internal(start, end, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return LinecastAll_Internal(start, end, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return LinecastAll_Internal(start, end, contactFilter);
	}

	public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return LinecastAll_Internal(start, end, contactFilter);
	}

	[ExcludeFromDocs]
	public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return LinecastNonAlloc_Internal(start, end, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return LinecastNonAlloc_Internal(start, end, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return LinecastNonAlloc_Internal(start, end, contactFilter, results);
	}

	public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return LinecastNonAlloc_Internal(start, end, contactFilter, results);
	}

	[NativeMethod("Linecast_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static RaycastHit2D Linecast_Internal(Vector2 start, Vector2 end, ContactFilter2D contactFilter)
	{
		Linecast_Internal_Injected(ref start, ref end, ref contactFilter, out var ret);
		return ret;
	}

	[NativeMethod("LinecastAll_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static RaycastHit2D[] LinecastAll_Internal(Vector2 start, Vector2 end, ContactFilter2D contactFilter)
	{
		return LinecastAll_Internal_Injected(ref start, ref end, ref contactFilter);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("LinecastNonAlloc_Binding")]
	private static int LinecastNonAlloc_Internal(Vector2 start, Vector2 end, ContactFilter2D contactFilter, [Out] RaycastHit2D[] results)
	{
		return LinecastNonAlloc_Internal_Injected(ref start, ref end, ref contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return Raycast_Internal(origin, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return Raycast_Internal(origin, direction, distance, contactFilter);
	}

	[RequiredByNativeCode]
	[ExcludeFromDocs]
	public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return Raycast_Internal(origin, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return Raycast_Internal(origin, direction, distance, contactFilter);
	}

	public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return Raycast_Internal(origin, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return RaycastNonAlloc_Internal(origin, direction, float.PositiveInfinity, contactFilter, results);
	}

	public static int Raycast(Vector2 origin, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
	{
		return RaycastNonAlloc_Internal(origin, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastAll_Internal(origin, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastAll_Internal(origin, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastAll_Internal(origin, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return RaycastAll_Internal(origin, direction, distance, contactFilter);
	}

	public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return RaycastAll_Internal(origin, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastNonAlloc_Internal(origin, direction, float.PositiveInfinity, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastNonAlloc_Internal(origin, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return RaycastNonAlloc_Internal(origin, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return RaycastNonAlloc_Internal(origin, direction, distance, contactFilter, results);
	}

	public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return RaycastNonAlloc_Internal(origin, direction, distance, contactFilter, results);
	}

	[NativeMethod("Raycast_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static RaycastHit2D Raycast_Internal(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		Raycast_Internal_Injected(ref origin, ref direction, distance, ref contactFilter, out var ret);
		return ret;
	}

	[NativeMethod("RaycastAll_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static RaycastHit2D[] RaycastAll_Internal(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return RaycastAll_Internal_Injected(ref origin, ref direction, distance, ref contactFilter);
	}

	[NativeMethod("RaycastNonAlloc_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static int RaycastNonAlloc_Internal(Vector2 origin, Vector2 direction, float distance, ContactFilter2D contactFilter, [Out] RaycastHit2D[] results)
	{
		return RaycastNonAlloc_Internal_Injected(ref origin, ref direction, distance, ref contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCast_Internal(origin, radius, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCast_Internal(origin, radius, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCast_Internal(origin, radius, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return CircleCast_Internal(origin, radius, direction, distance, contactFilter);
	}

	public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return CircleCast_Internal(origin, radius, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return CircleCastNonAlloc_Internal(origin, radius, direction, float.PositiveInfinity, contactFilter, results);
	}

	public static int CircleCast(Vector2 origin, float radius, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
	{
		return CircleCastNonAlloc_Internal(origin, radius, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCastAll_Internal(origin, radius, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCastAll_Internal(origin, radius, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCastAll_Internal(origin, radius, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return CircleCastAll_Internal(origin, radius, direction, distance, contactFilter);
	}

	public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return CircleCastAll_Internal(origin, radius, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCastNonAlloc_Internal(origin, radius, direction, float.PositiveInfinity, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCastNonAlloc_Internal(origin, radius, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CircleCastNonAlloc_Internal(origin, radius, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return CircleCastNonAlloc_Internal(origin, radius, direction, distance, contactFilter, results);
	}

	public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return CircleCastNonAlloc_Internal(origin, radius, direction, distance, contactFilter, results);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("CircleCast_Binding")]
	private static RaycastHit2D CircleCast_Internal(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		CircleCast_Internal_Injected(ref origin, radius, ref direction, distance, ref contactFilter, out var ret);
		return ret;
	}

	[NativeMethod("CircleCastAll_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static RaycastHit2D[] CircleCastAll_Internal(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return CircleCastAll_Internal_Injected(ref origin, radius, ref direction, distance, ref contactFilter);
	}

	[NativeMethod("CircleCastNonAlloc_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static int CircleCastNonAlloc_Internal(Vector2 origin, float radius, Vector2 direction, float distance, ContactFilter2D contactFilter, [Out] RaycastHit2D[] results)
	{
		return CircleCastNonAlloc_Internal_Injected(ref origin, radius, ref direction, distance, ref contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCast_Internal(origin, size, angle, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCast_Internal(origin, size, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCast_Internal(origin, size, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return BoxCast_Internal(origin, size, angle, direction, distance, contactFilter);
	}

	public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("Physics2D.AllLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return BoxCast_Internal(origin, size, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return BoxCastNonAlloc_Internal(origin, size, angle, direction, float.PositiveInfinity, contactFilter, results);
	}

	public static int BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
	{
		return BoxCastNonAlloc_Internal(origin, size, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCastAll_Internal(origin, size, angle, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCastAll_Internal(origin, size, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCastAll_Internal(origin, size, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return BoxCastAll_Internal(origin, size, angle, direction, distance, contactFilter);
	}

	public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return BoxCastAll_Internal(origin, size, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCastNonAlloc_Internal(origin, size, angle, direction, float.PositiveInfinity, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCastNonAlloc_Internal(origin, size, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return BoxCastNonAlloc_Internal(origin, size, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return BoxCastNonAlloc_Internal(origin, size, angle, direction, distance, contactFilter, results);
	}

	public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return BoxCastNonAlloc_Internal(origin, size, angle, direction, distance, contactFilter, results);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("BoxCast_Binding")]
	private static RaycastHit2D BoxCast_Internal(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		BoxCast_Internal_Injected(ref origin, ref size, angle, ref direction, distance, ref contactFilter, out var ret);
		return ret;
	}

	[NativeMethod("BoxCastAll_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static RaycastHit2D[] BoxCastAll_Internal(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return BoxCastAll_Internal_Injected(ref origin, ref size, angle, ref direction, distance, ref contactFilter);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("BoxCastNonAlloc_Binding")]
	private static int BoxCastNonAlloc_Internal(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [Out] RaycastHit2D[] results)
	{
		return BoxCastNonAlloc_Internal_Injected(ref origin, ref size, angle, ref direction, distance, ref contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCast_Internal(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCast_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCast_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return CapsuleCast_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	public static RaycastHit2D CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return CapsuleCast_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results)
	{
		return CapsuleCastNonAlloc_Internal(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, contactFilter, results);
	}

	public static int CapsuleCast(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, ContactFilter2D contactFilter, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance)
	{
		return CapsuleCastNonAlloc_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCastAll_Internal(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCastAll_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCastAll_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return CapsuleCastAll_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	public static RaycastHit2D[] CapsuleCastAll(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return CapsuleCastAll_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCastNonAlloc_Internal(origin, size, capsuleDirection, angle, direction, float.PositiveInfinity, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCastNonAlloc_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return CapsuleCastNonAlloc_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return CapsuleCastNonAlloc_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	public static int CapsuleCastNonAlloc(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return CapsuleCastNonAlloc_Internal(origin, size, capsuleDirection, angle, direction, distance, contactFilter, results);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("CapsuleCast_Binding")]
	private static RaycastHit2D CapsuleCast_Internal(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		CapsuleCast_Internal_Injected(ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, out var ret);
		return ret;
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("CapsuleCastAll_Binding")]
	private static RaycastHit2D[] CapsuleCastAll_Internal(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter)
	{
		return CapsuleCastAll_Internal_Injected(ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("CapsuleCastNonAlloc_Binding")]
	private static int CapsuleCastNonAlloc_Internal(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, float distance, ContactFilter2D contactFilter, [Out] RaycastHit2D[] results)
	{
		return CapsuleCastNonAlloc_Internal_Injected(ref origin, ref size, capsuleDirection, angle, ref direction, distance, ref contactFilter, results);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D GetRayIntersection(Ray ray)
	{
		return GetRayIntersection_Internal(ray.origin, ray.direction, float.PositiveInfinity, -5);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D GetRayIntersection(Ray ray, float distance)
	{
		return GetRayIntersection_Internal(ray.origin, ray.direction, distance, -5);
	}

	public static RaycastHit2D GetRayIntersection(Ray ray, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
	{
		return GetRayIntersection_Internal(ray.origin, ray.direction, distance, layerMask);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] GetRayIntersectionAll(Ray ray)
	{
		return GetRayIntersectionAll_Internal(ray.origin, ray.direction, float.PositiveInfinity, -5);
	}

	[ExcludeFromDocs]
	public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance)
	{
		return GetRayIntersectionAll_Internal(ray.origin, ray.direction, distance, -5);
	}

	[RequiredByNativeCode]
	public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
	{
		return GetRayIntersectionAll_Internal(ray.origin, ray.direction, distance, layerMask);
	}

	[ExcludeFromDocs]
	public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results)
	{
		return GetRayIntersectionNonAlloc_Internal(ray.origin, ray.direction, float.PositiveInfinity, -5, results);
	}

	[ExcludeFromDocs]
	public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance)
	{
		return GetRayIntersectionNonAlloc_Internal(ray.origin, ray.direction, distance, -5, results);
	}

	[RequiredByNativeCode]
	public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
	{
		return GetRayIntersectionNonAlloc_Internal(ray.origin, ray.direction, distance, layerMask, results);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("GetRayIntersection_Binding")]
	private static RaycastHit2D GetRayIntersection_Internal(Vector3 origin, Vector3 direction, float distance, int layerMask)
	{
		GetRayIntersection_Internal_Injected(ref origin, ref direction, distance, layerMask, out var ret);
		return ret;
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("GetRayIntersectionAll_Binding")]
	private static RaycastHit2D[] GetRayIntersectionAll_Internal(Vector3 origin, Vector3 direction, float distance, int layerMask)
	{
		return GetRayIntersectionAll_Internal_Injected(ref origin, ref direction, distance, layerMask);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("GetRayIntersectionNonAlloc_Binding")]
	private static int GetRayIntersectionNonAlloc_Internal(Vector3 origin, Vector3 direction, float distance, int layerMask, [Out] RaycastHit2D[] results)
	{
		return GetRayIntersectionNonAlloc_Internal_Injected(ref origin, ref direction, distance, layerMask, results);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapPoint(Vector2 point)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapPoint_Internal(point, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapPoint(Vector2 point, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapPoint_Internal(point, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapPoint_Internal(point, contactFilter);
	}

	public static Collider2D OverlapPoint(Vector2 point, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapPoint_Internal(point, contactFilter);
	}

	public static int OverlapPoint(Vector2 point, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return OverlapPointNonAlloc_Internal(point, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapPointAll(Vector2 point)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapPointAll_Internal(point, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapPointAll_Internal(point, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapPointAll_Internal(point, contactFilter);
	}

	public static Collider2D[] OverlapPointAll(Vector2 point, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapPointAll_Internal(point, contactFilter);
	}

	[ExcludeFromDocs]
	public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapPointNonAlloc_Internal(point, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapPointNonAlloc_Internal(point, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapPointNonAlloc_Internal(point, contactFilter, results);
	}

	public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapPointNonAlloc_Internal(point, contactFilter, results);
	}

	[NativeMethod("OverlapPoint_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static Collider2D OverlapPoint_Internal(Vector2 point, ContactFilter2D contactFilter)
	{
		return OverlapPoint_Internal_Injected(ref point, ref contactFilter);
	}

	[NativeMethod("OverlapPointAll_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static Collider2D[] OverlapPointAll_Internal(Vector2 point, ContactFilter2D contactFilter)
	{
		return OverlapPointAll_Internal_Injected(ref point, ref contactFilter);
	}

	[NativeMethod("OverlapPointNonAlloc_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static int OverlapPointNonAlloc_Internal(Vector2 point, ContactFilter2D contactFilter, [Out] Collider2D[] results)
	{
		return OverlapPointNonAlloc_Internal_Injected(ref point, ref contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCircle(Vector2 point, float radius)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCircle_Internal(point, radius, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCircle_Internal(point, radius, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapCircle_Internal(point, radius, contactFilter);
	}

	public static Collider2D OverlapCircle(Vector2 point, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapCircle_Internal(point, radius, contactFilter);
	}

	public static int OverlapCircle(Vector2 point, float radius, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return OverlapCircleNonAlloc_Internal(point, radius, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCircleAll(Vector2 point, float radius)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCircleAll_Internal(point, radius, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCircleAll_Internal(point, radius, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapCircleAll_Internal(point, radius, contactFilter);
	}

	public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapCircleAll_Internal(point, radius, contactFilter);
	}

	[ExcludeFromDocs]
	public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCircleNonAlloc_Internal(point, radius, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCircleNonAlloc_Internal(point, radius, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapCircleNonAlloc_Internal(point, radius, contactFilter, results);
	}

	public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapCircleNonAlloc_Internal(point, radius, contactFilter, results);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("OverlapCircle_Binding")]
	private static Collider2D OverlapCircle_Internal(Vector2 point, float radius, ContactFilter2D contactFilter)
	{
		return OverlapCircle_Internal_Injected(ref point, radius, ref contactFilter);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("OverlapCircleAll_Binding")]
	private static Collider2D[] OverlapCircleAll_Internal(Vector2 point, float radius, ContactFilter2D contactFilter)
	{
		return OverlapCircleAll_Internal_Injected(ref point, radius, ref contactFilter);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("OverlapCircleNonAlloc_Binding")]
	private static int OverlapCircleNonAlloc_Internal(Vector2 point, float radius, ContactFilter2D contactFilter, [Out] Collider2D[] results)
	{
		return OverlapCircleNonAlloc_Internal_Injected(ref point, radius, ref contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapBox_Internal(point, size, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapBox_Internal(point, size, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapBox_Internal(point, size, angle, contactFilter);
	}

	public static Collider2D OverlapBox(Vector2 point, Vector2 size, float angle, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapBox_Internal(point, size, angle, contactFilter);
	}

	public static int OverlapBox(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return OverlapBoxNonAlloc_Internal(point, size, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapBoxAll_Internal(point, size, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapBoxAll_Internal(point, size, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapBoxAll_Internal(point, size, angle, contactFilter);
	}

	public static Collider2D[] OverlapBoxAll(Vector2 point, Vector2 size, float angle, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapBoxAll_Internal(point, size, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapBoxNonAlloc_Internal(point, size, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapBoxNonAlloc_Internal(point, size, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapBoxNonAlloc_Internal(point, size, angle, contactFilter, results);
	}

	public static int OverlapBoxNonAlloc(Vector2 point, Vector2 size, float angle, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapBoxNonAlloc_Internal(point, size, angle, contactFilter, results);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("OverlapBox_Binding")]
	private static Collider2D OverlapBox_Internal(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
	{
		return OverlapBox_Internal_Injected(ref point, ref size, angle, ref contactFilter);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("OverlapBoxAll_Binding")]
	private static Collider2D[] OverlapBoxAll_Internal(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter)
	{
		return OverlapBoxAll_Internal_Injected(ref point, ref size, angle, ref contactFilter);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("OverlapBoxNonAlloc_Binding")]
	private static int OverlapBoxNonAlloc_Internal(Vector2 point, Vector2 size, float angle, ContactFilter2D contactFilter, [Out] Collider2D[] results)
	{
		return OverlapBoxNonAlloc_Internal_Injected(ref point, ref size, angle, ref contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB)
	{
		return OverlapAreaToBox_Internal(pointA, pointB, -5, float.NegativeInfinity, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask)
	{
		return OverlapAreaToBox_Internal(pointA, pointB, layerMask, float.NegativeInfinity, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
	{
		return OverlapAreaToBox_Internal(pointA, pointB, layerMask, minDepth, float.PositiveInfinity);
	}

	public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		return OverlapAreaToBox_Internal(pointA, pointB, layerMask, minDepth, maxDepth);
	}

	private static Collider2D OverlapAreaToBox_Internal(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, float maxDepth)
	{
		Vector2 point = (pointA + pointB) * 0.5f;
		Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
		return OverlapBox(point, size, 0f, layerMask, minDepth, maxDepth);
	}

	public static int OverlapArea(Vector2 pointA, Vector2 pointB, ContactFilter2D contactFilter, Collider2D[] results)
	{
		Vector2 point = (pointA + pointB) * 0.5f;
		Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
		return OverlapBox(point, size, 0f, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB)
	{
		return OverlapAreaAllToBox_Internal(pointA, pointB, -5, float.NegativeInfinity, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask)
	{
		return OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, float.NegativeInfinity, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
	{
		return OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, minDepth, float.PositiveInfinity);
	}

	public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		return OverlapAreaAllToBox_Internal(pointA, pointB, layerMask, minDepth, maxDepth);
	}

	private static Collider2D[] OverlapAreaAllToBox_Internal(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth, float maxDepth)
	{
		Vector2 point = (pointA + pointB) * 0.5f;
		Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
		return OverlapBoxAll(point, size, 0f, layerMask, minDepth, maxDepth);
	}

	[ExcludeFromDocs]
	public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results)
	{
		return OverlapAreaNonAllocToBox_Internal(pointA, pointB, results, -5, float.NegativeInfinity, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask)
	{
		return OverlapAreaNonAllocToBox_Internal(pointA, pointB, results, layerMask, float.NegativeInfinity, float.PositiveInfinity);
	}

	[ExcludeFromDocs]
	public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth)
	{
		return OverlapAreaNonAllocToBox_Internal(pointA, pointB, results, layerMask, minDepth, float.PositiveInfinity);
	}

	public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		return OverlapAreaNonAllocToBox_Internal(pointA, pointB, results, layerMask, minDepth, maxDepth);
	}

	private static int OverlapAreaNonAllocToBox_Internal(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth, float maxDepth)
	{
		Vector2 point = (pointA + pointB) * 0.5f;
		Vector2 size = new Vector2(Mathf.Abs(pointA.x - pointB.x), Math.Abs(pointA.y - pointB.y));
		return OverlapBoxNonAlloc(point, size, 0f, results, layerMask, minDepth, maxDepth);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCapsule_Internal(point, size, direction, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCapsule_Internal(point, size, direction, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapCapsule_Internal(point, size, direction, angle, contactFilter);
	}

	public static Collider2D OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapCapsule_Internal(point, size, direction, angle, contactFilter);
	}

	public static int OverlapCapsule(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, Collider2D[] results)
	{
		return OverlapCapsuleNonAlloc_Internal(point, size, direction, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCapsuleAll_Internal(point, size, direction, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCapsuleAll_Internal(point, size, direction, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapCapsuleAll_Internal(point, size, direction, angle, contactFilter);
	}

	public static Collider2D[] OverlapCapsuleAll(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapCapsuleAll_Internal(point, size, direction, angle, contactFilter);
	}

	[ExcludeFromDocs]
	public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(-5, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCapsuleNonAlloc_Internal(point, size, direction, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, float.NegativeInfinity, float.PositiveInfinity);
		return OverlapCapsuleNonAlloc_Internal(point, size, direction, angle, contactFilter, results);
	}

	[ExcludeFromDocs]
	public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, int layerMask, float minDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, float.PositiveInfinity);
		return OverlapCapsuleNonAlloc_Internal(point, size, direction, angle, contactFilter, results);
	}

	public static int OverlapCapsuleNonAlloc(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
	{
		ContactFilter2D contactFilter = ContactFilter2D.CreateLegacyFilter(layerMask, minDepth, maxDepth);
		return OverlapCapsuleNonAlloc_Internal(point, size, direction, angle, contactFilter, results);
	}

	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	[NativeMethod("OverlapCapsule_Binding")]
	private static Collider2D OverlapCapsule_Internal(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
	{
		return OverlapCapsule_Internal_Injected(ref point, ref size, direction, angle, ref contactFilter);
	}

	[NativeMethod("OverlapCapsuleAll_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static Collider2D[] OverlapCapsuleAll_Internal(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter)
	{
		return OverlapCapsuleAll_Internal_Injected(ref point, ref size, direction, angle, ref contactFilter);
	}

	[NativeMethod("OverlapCapsuleNonAlloc_Binding")]
	[StaticAccessor("GetPhysicsQuery2D()", StaticAccessorType.Arrow)]
	private static int OverlapCapsuleNonAlloc_Internal(Vector2 point, Vector2 size, CapsuleDirection2D direction, float angle, ContactFilter2D contactFilter, [Out] Collider2D[] results)
	{
		return OverlapCapsuleNonAlloc_Internal_Injected(ref point, ref size, direction, angle, ref contactFilter, results);
	}

	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	[NativeMethod("OverlapCollider_Binding")]
	public static int OverlapCollider([NotNull] Collider2D collider, ContactFilter2D contactFilter, [Out] Collider2D[] results)
	{
		return OverlapCollider_Injected(collider, ref contactFilter, results);
	}

	public static int GetContacts(Collider2D collider1, Collider2D collider2, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
	{
		return GetColliderColliderContacts(collider1, collider2, contactFilter, contacts);
	}

	public static int GetContacts(Collider2D collider, ContactPoint2D[] contacts)
	{
		return GetColliderContacts(collider, default(ContactFilter2D).NoFilter(), contacts);
	}

	public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
	{
		return GetColliderContacts(collider, contactFilter, contacts);
	}

	public static int GetContacts(Collider2D collider, Collider2D[] colliders)
	{
		return GetColliderContactsCollidersOnly(collider, default(ContactFilter2D).NoFilter(), colliders);
	}

	public static int GetContacts(Collider2D collider, ContactFilter2D contactFilter, Collider2D[] colliders)
	{
		return GetColliderContactsCollidersOnly(collider, contactFilter, colliders);
	}

	public static int GetContacts(Rigidbody2D rigidbody, ContactPoint2D[] contacts)
	{
		return GetRigidbodyContacts(rigidbody, default(ContactFilter2D).NoFilter(), contacts);
	}

	public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, ContactPoint2D[] contacts)
	{
		return GetRigidbodyContacts(rigidbody, contactFilter, contacts);
	}

	public static int GetContacts(Rigidbody2D rigidbody, Collider2D[] colliders)
	{
		return GetRigidbodyContactsCollidersOnly(rigidbody, default(ContactFilter2D).NoFilter(), colliders);
	}

	public static int GetContacts(Rigidbody2D rigidbody, ContactFilter2D contactFilter, Collider2D[] colliders)
	{
		return GetRigidbodyContactsCollidersOnly(rigidbody, contactFilter, colliders);
	}

	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	[NativeMethod("GetColliderContacts_Binding")]
	private static int GetColliderContacts([NotNull] Collider2D collider, ContactFilter2D contactFilter, [Out] ContactPoint2D[] results)
	{
		return GetColliderContacts_Injected(collider, ref contactFilter, results);
	}

	[NativeMethod("GetColliderColliderContacts_Binding")]
	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	private static int GetColliderColliderContacts([NotNull] Collider2D collider1, [NotNull] Collider2D collider2, ContactFilter2D contactFilter, [Out] ContactPoint2D[] results)
	{
		return GetColliderColliderContacts_Injected(collider1, collider2, ref contactFilter, results);
	}

	[NativeMethod("GetRigidbodyContacts_Binding")]
	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	private static int GetRigidbodyContacts([NotNull] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [Out] ContactPoint2D[] results)
	{
		return GetRigidbodyContacts_Injected(rigidbody, ref contactFilter, results);
	}

	[NativeMethod("GetColliderContactsCollidersOnly_Binding")]
	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	private static int GetColliderContactsCollidersOnly([NotNull] Collider2D collider, ContactFilter2D contactFilter, [Out] Collider2D[] results)
	{
		return GetColliderContactsCollidersOnly_Injected(collider, ref contactFilter, results);
	}

	[NativeMethod("GetRigidbodyContactsCollidersOnly_Binding")]
	[StaticAccessor("GetPhysicsManager2D()", StaticAccessorType.Arrow)]
	private static int GetRigidbodyContactsCollidersOnly([NotNull] Rigidbody2D rigidbody, ContactFilter2D contactFilter, [Out] Collider2D[] results)
	{
		return GetRigidbodyContactsCollidersOnly_Injected(rigidbody, ref contactFilter, results);
	}

	internal static void SetEditorDragMovement(bool dragging, GameObject[] objs)
	{
		foreach (Rigidbody2D item in m_LastDisabledRigidbody2D)
		{
			if (item != null)
			{
				item.SetDragBehaviour(dragged: false);
			}
		}
		m_LastDisabledRigidbody2D.Clear();
		if (!dragging)
		{
			return;
		}
		foreach (GameObject gameObject in objs)
		{
			Rigidbody2D[] componentsInChildren = gameObject.GetComponentsInChildren<Rigidbody2D>(includeInactive: false);
			Rigidbody2D[] array = componentsInChildren;
			foreach (Rigidbody2D rigidbody2D in array)
			{
				m_LastDisabledRigidbody2D.Add(rigidbody2D);
				rigidbody2D.SetDragBehaviour(dragged: true);
			}
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_gravity_Injected(out Vector2 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_gravity_Injected(ref Vector2 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_jobOptions_Injected(out PhysicsJobOptions2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_jobOptions_Injected(ref PhysicsJobOptions2D value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_colliderAwakeColor_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_colliderAwakeColor_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_colliderAsleepColor_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_colliderAsleepColor_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_colliderContactColor_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_colliderContactColor_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void get_colliderAABBColor_Injected(out Color ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private static extern void set_colliderAABBColor_Injected(ref Color value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsTouching_TwoCollidersWithFilter_Injected([Writable] Collider2D collider1, [Writable] Collider2D collider2, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsTouching_SingleColliderWithFilter_Injected([Writable] Collider2D collider, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Distance_Internal_Injected([Writable] Collider2D colliderA, [Writable] Collider2D colliderB, out ColliderDistance2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Linecast_Internal_Injected(ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] LinecastAll_Internal_Injected(ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int LinecastNonAlloc_Internal_Injected(ref Vector2 start, ref Vector2 end, ref ContactFilter2D contactFilter, [Out] RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Raycast_Internal_Injected(ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] RaycastAll_Internal_Injected(ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int RaycastNonAlloc_Internal_Injected(ref Vector2 origin, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, [Out] RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void CircleCast_Internal_Injected(ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] CircleCastAll_Internal_Injected(ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int CircleCastNonAlloc_Internal_Injected(ref Vector2 origin, float radius, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, [Out] RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void BoxCast_Internal_Injected(ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] BoxCastAll_Internal_Injected(ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int BoxCastNonAlloc_Internal_Injected(ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, [Out] RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void CapsuleCast_Internal_Injected(ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] CapsuleCastAll_Internal_Injected(ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int CapsuleCastNonAlloc_Internal_Injected(ref Vector2 origin, ref Vector2 size, CapsuleDirection2D capsuleDirection, float angle, ref Vector2 direction, float distance, ref ContactFilter2D contactFilter, [Out] RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void GetRayIntersection_Internal_Injected(ref Vector3 origin, ref Vector3 direction, float distance, int layerMask, out RaycastHit2D ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern RaycastHit2D[] GetRayIntersectionAll_Internal_Injected(ref Vector3 origin, ref Vector3 direction, float distance, int layerMask);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetRayIntersectionNonAlloc_Internal_Injected(ref Vector3 origin, ref Vector3 direction, float distance, int layerMask, [Out] RaycastHit2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D OverlapPoint_Internal_Injected(ref Vector2 point, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D[] OverlapPointAll_Internal_Injected(ref Vector2 point, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapPointNonAlloc_Internal_Injected(ref Vector2 point, ref ContactFilter2D contactFilter, [Out] Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D OverlapCircle_Internal_Injected(ref Vector2 point, float radius, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D[] OverlapCircleAll_Internal_Injected(ref Vector2 point, float radius, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapCircleNonAlloc_Internal_Injected(ref Vector2 point, float radius, ref ContactFilter2D contactFilter, [Out] Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D OverlapBox_Internal_Injected(ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D[] OverlapBoxAll_Internal_Injected(ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapBoxNonAlloc_Internal_Injected(ref Vector2 point, ref Vector2 size, float angle, ref ContactFilter2D contactFilter, [Out] Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D OverlapCapsule_Internal_Injected(ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Collider2D[] OverlapCapsuleAll_Internal_Injected(ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapCapsuleNonAlloc_Internal_Injected(ref Vector2 point, ref Vector2 size, CapsuleDirection2D direction, float angle, ref ContactFilter2D contactFilter, [Out] Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int OverlapCollider_Injected(Collider2D collider, ref ContactFilter2D contactFilter, [Out] Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetColliderContacts_Injected(Collider2D collider, ref ContactFilter2D contactFilter, [Out] ContactPoint2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetColliderColliderContacts_Injected(Collider2D collider1, Collider2D collider2, ref ContactFilter2D contactFilter, [Out] ContactPoint2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetRigidbodyContacts_Injected(Rigidbody2D rigidbody, ref ContactFilter2D contactFilter, [Out] ContactPoint2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetColliderContactsCollidersOnly_Injected(Collider2D collider, ref ContactFilter2D contactFilter, [Out] Collider2D[] results);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetRigidbodyContactsCollidersOnly_Injected(Rigidbody2D rigidbody, ref ContactFilter2D contactFilter, [Out] Collider2D[] results);
}
