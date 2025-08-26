using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeClass("Unity::Cloth")]
[RequireComponent(typeof(Transform), typeof(SkinnedMeshRenderer))]
[NativeHeader("Runtime/Cloth/Cloth.h")]
public sealed class Cloth : Component
{
	[Obsolete("Deprecated. Cloth.selfCollisions is no longer supported since Unity 5.0.", true)]
	public extern bool selfCollision
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern Vector3[] vertices
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern Vector3[] normals
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	[Obsolete("useContinuousCollision is no longer supported, use enableContinuousCollision instead")]
	public extern float useContinuousCollision
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern ClothSkinningCoefficient[] coefficients
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("Parameter solverFrequency is obsolete and no longer supported. Please use clothSolverFrequency instead.")]
	public bool solverFrequency
	{
		get
		{
			return clothSolverFrequency > 0f;
		}
		set
		{
			clothSolverFrequency = ((!value) ? 0f : 120f);
		}
	}

	public extern CapsuleCollider[] capsuleColliders
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern ClothSphereColliderPair[] sphereColliders
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float sleepThreshold
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float bendingStiffness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float stretchingStiffness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float damping
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public Vector3 externalAcceleration
	{
		get
		{
			get_externalAcceleration_Injected(out var ret);
			return ret;
		}
		set
		{
			set_externalAcceleration_Injected(ref value);
		}
	}

	public Vector3 randomAcceleration
	{
		get
		{
			get_randomAcceleration_Injected(out var ret);
			return ret;
		}
		set
		{
			set_randomAcceleration_Injected(ref value);
		}
	}

	public extern bool useGravity
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool enabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float friction
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float collisionMassScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool enableContinuousCollision
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float useVirtualParticles
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float worldVelocityScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float worldAccelerationScale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float clothSolverFrequency
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern bool useTethers
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float stiffnessFrequency
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float selfCollisionDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public extern float selfCollisionStiffness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		set;
	}

	public void ClearTransformMotion()
	{
		INTERNAL_CALL_ClearTransformMotion(this);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_ClearTransformMotion(Cloth self);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetEnabledFading(bool enabled, [DefaultValue("0.5f")] float interpolationTime);

	[ExcludeFromDocs]
	public void SetEnabledFading(bool enabled)
	{
		float interpolationTime = 0.5f;
		SetEnabledFading(enabled, interpolationTime);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern void GetVirtualParticleIndicesMono(object indicesOutList);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern void SetVirtualParticleIndicesMono(object indicesInList);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern void GetVirtualParticleWeightsMono(object weightsOutList);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern void SetVirtualParticleWeightsMono(object weightsInList);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern void GetSelfAndInterCollisionIndicesMono(object indicesOutList);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern void SetSelfAndInterCollisionIndicesMono(object indicesInList);

	public void GetVirtualParticleIndices(List<uint> indices)
	{
		if (indices == null)
		{
			throw new ArgumentNullException("indices");
		}
		GetVirtualParticleIndicesMono(indices);
	}

	public void SetVirtualParticleIndices(List<uint> indices)
	{
		if (indices == null)
		{
			throw new ArgumentNullException("indices");
		}
		SetVirtualParticleIndicesMono(indices);
	}

	public void GetVirtualParticleWeights(List<Vector3> weights)
	{
		if (weights == null)
		{
			throw new ArgumentNullException("weights");
		}
		GetVirtualParticleWeightsMono(weights);
	}

	public void SetVirtualParticleWeights(List<Vector3> weights)
	{
		if (weights == null)
		{
			throw new ArgumentNullException("weights");
		}
		SetVirtualParticleWeightsMono(weights);
	}

	public void GetSelfAndInterCollisionIndices(List<uint> indices)
	{
		if (indices == null)
		{
			throw new ArgumentNullException("indices");
		}
		GetSelfAndInterCollisionIndicesMono(indices);
	}

	public void SetSelfAndInterCollisionIndices(List<uint> indices)
	{
		if (indices == null)
		{
			throw new ArgumentNullException("indices");
		}
		SetSelfAndInterCollisionIndicesMono(indices);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_externalAcceleration_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_externalAcceleration_Injected(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void get_randomAcceleration_Injected(out Vector3 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[SpecialName]
	private extern void set_randomAcceleration_Injected(ref Vector3 value);
}
