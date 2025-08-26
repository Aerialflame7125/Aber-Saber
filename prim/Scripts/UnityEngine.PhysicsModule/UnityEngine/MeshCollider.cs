using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

[RequiredByNativeCode]
public sealed class MeshCollider : Collider
{
	public extern Mesh sharedMesh
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool convex
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern MeshColliderCookingOptions cookingOptions
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public bool inflateMesh
	{
		get
		{
			return (cookingOptions & MeshColliderCookingOptions.InflateConvexMesh) != 0;
		}
		set
		{
			MeshColliderCookingOptions meshColliderCookingOptions = cookingOptions & ~MeshColliderCookingOptions.InflateConvexMesh;
			if (value)
			{
				meshColliderCookingOptions |= MeshColliderCookingOptions.InflateConvexMesh;
			}
			cookingOptions = meshColliderCookingOptions;
		}
	}

	public extern float skinWidth
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("Configuring smooth sphere collisions is no longer needed. PhysX3 has a better behaviour in place.")]
	public bool smoothSphereCollisions
	{
		get
		{
			return true;
		}
		set
		{
		}
	}
}
