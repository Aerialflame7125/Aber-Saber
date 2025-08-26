using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI;

[MovedFrom("UnityEngine")]
public sealed class NavMeshAgent : Behaviour
{
	public Vector3 destination
	{
		get
		{
			INTERNAL_get_destination(out var value);
			return value;
		}
		set
		{
			INTERNAL_set_destination(ref value);
		}
	}

	public extern float stoppingDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public Vector3 velocity
	{
		get
		{
			INTERNAL_get_velocity(out var value);
			return value;
		}
		set
		{
			INTERNAL_set_velocity(ref value);
		}
	}

	public Vector3 nextPosition
	{
		get
		{
			INTERNAL_get_nextPosition(out var value);
			return value;
		}
		set
		{
			INTERNAL_set_nextPosition(ref value);
		}
	}

	public Vector3 steeringTarget
	{
		get
		{
			INTERNAL_get_steeringTarget(out var value);
			return value;
		}
	}

	public Vector3 desiredVelocity
	{
		get
		{
			INTERNAL_get_desiredVelocity(out var value);
			return value;
		}
	}

	public extern float remainingDistance
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern float baseOffset
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool isOnOffMeshLink
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public OffMeshLinkData currentOffMeshLinkData => GetCurrentOffMeshLinkDataInternal();

	public OffMeshLinkData nextOffMeshLinkData => GetNextOffMeshLinkDataInternal();

	public extern bool autoTraverseOffMeshLink
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool autoBraking
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool autoRepath
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool hasPath
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool pathPending
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern bool isPathStale
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public extern NavMeshPathStatus pathStatus
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public Vector3 pathEndPosition
	{
		get
		{
			INTERNAL_get_pathEndPosition(out var value);
			return value;
		}
	}

	public extern bool isStopped
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public NavMeshPath path
	{
		get
		{
			NavMeshPath result = new NavMeshPath();
			CopyPathTo(result);
			return result;
		}
		set
		{
			if (value == null)
			{
				throw new NullReferenceException();
			}
			SetPath(value);
		}
	}

	public Object navMeshOwner => GetOwnerInternal();

	public extern int agentTypeID
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[Obsolete("Use areaMask instead.")]
	public extern int walkableMask
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern int areaMask
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float speed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float angularSpeed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float acceleration
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool updatePosition
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool updateRotation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool updateUpAxis
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float radius
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern float height
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern ObstacleAvoidanceType obstacleAvoidanceType
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern int avoidancePriority
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public extern bool isOnNavMesh
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	public bool SetDestination(Vector3 target)
	{
		return INTERNAL_CALL_SetDestination(this, ref target);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool INTERNAL_CALL_SetDestination(NavMeshAgent self, ref Vector3 target);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_destination(out Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_set_destination(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_velocity(out Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_set_velocity(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_nextPosition(out Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_set_nextPosition(ref Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_steeringTarget(out Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_desiredVelocity(out Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void ActivateCurrentOffMeshLink(bool activated);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern OffMeshLinkData GetCurrentOffMeshLinkDataInternal();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern OffMeshLinkData GetNextOffMeshLinkDataInternal();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void CompleteOffMeshLink();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern void INTERNAL_get_pathEndPosition(out Vector3 value);

	public bool Warp(Vector3 newPosition)
	{
		return INTERNAL_CALL_Warp(this, ref newPosition);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool INTERNAL_CALL_Warp(NavMeshAgent self, ref Vector3 newPosition);

	public void Move(Vector3 offset)
	{
		INTERNAL_CALL_Move(this, ref offset);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Move(NavMeshAgent self, ref Vector3 offset);

	[Obsolete("Set isStopped to true instead")]
	public void Stop()
	{
		StopInternal();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern void StopInternal();

	[Obsolete("Set isStopped to true instead")]
	public void Stop(bool stopUpdates)
	{
		StopInternal();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[Obsolete("Set isStopped to false instead")]
	[GeneratedByOldBindingsGenerator]
	public extern void Resume();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void ResetPath();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern bool SetPath(NavMeshPath path);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	internal extern void CopyPathTo(NavMeshPath path);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern bool FindClosestEdge(out NavMeshHit hit);

	public bool Raycast(Vector3 targetPosition, out NavMeshHit hit)
	{
		return INTERNAL_CALL_Raycast(this, ref targetPosition, out hit);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool INTERNAL_CALL_Raycast(NavMeshAgent self, ref Vector3 targetPosition, out NavMeshHit hit);

	public bool CalculatePath(Vector3 targetPosition, NavMeshPath path)
	{
		path.ClearCorners();
		return CalculatePathInternal(targetPosition, path);
	}

	private bool CalculatePathInternal(Vector3 targetPosition, NavMeshPath path)
	{
		return INTERNAL_CALL_CalculatePathInternal(this, ref targetPosition, path);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern bool INTERNAL_CALL_CalculatePathInternal(NavMeshAgent self, ref Vector3 targetPosition, NavMeshPath path);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern bool SamplePathPosition(int areaMask, float maxDistance, out NavMeshHit hit);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[Obsolete("Use SetAreaCost instead.")]
	[GeneratedByOldBindingsGenerator]
	public extern void SetLayerCost(int layer, float cost);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	[Obsolete("Use GetAreaCost instead.")]
	public extern float GetLayerCost(int layer);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern void SetAreaCost(int areaIndex, float areaCost);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public extern float GetAreaCost(int areaIndex);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private extern Object GetOwnerInternal();
}
