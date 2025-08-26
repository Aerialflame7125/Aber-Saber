using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AI;

[NativeContainer]
[NativeHeader("Runtime/Math/Matrix4x4.h")]
[StaticAccessor("NavMeshQueryBindings", StaticAccessorType.DoubleColon)]
[NativeHeader("Modules/AI/NavMeshExperimental.bindings.h")]
public struct NavMeshQuery : IDisposable
{
	[NativeDisableUnsafePtrRestriction]
	internal IntPtr m_NavMeshQuery;

	private Allocator m_Allocator;

	private const string k_NoBufferAllocatedErrorMessage = "This query has no buffer allocated for pathfinding operations. Create a different NavMeshQuery with an explicit node pool size.";

	public NavMeshQuery(NavMeshWorld world, Allocator allocator, int pathNodePoolSize = 0)
	{
		m_Allocator = allocator;
		m_NavMeshQuery = Create(world, pathNodePoolSize);
	}

	public void Dispose()
	{
		Destroy(m_NavMeshQuery);
		m_NavMeshQuery = IntPtr.Zero;
	}

	private static IntPtr Create(NavMeshWorld world, int nodePoolSize)
	{
		return Create_Injected(ref world, nodePoolSize);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void Destroy(IntPtr navMeshQuery);

	public unsafe PathQueryStatus BeginFindPath(NavMeshLocation start, NavMeshLocation end, int areaMask = -1, NativeArray<float> costs = default(NativeArray<float>))
	{
		void* costs2 = ((costs.Length <= 0) ? null : costs.GetUnsafePtr());
		return BeginFindPath(m_NavMeshQuery, start, end, areaMask, costs2);
	}

	public PathQueryStatus UpdateFindPath(int iterations, out int iterationsPerformed)
	{
		return UpdateFindPath(m_NavMeshQuery, iterations, out iterationsPerformed);
	}

	public PathQueryStatus EndFindPath(out int pathSize)
	{
		return EndFindPath(m_NavMeshQuery, out pathSize);
	}

	public unsafe int GetPathResult(NativeSlice<PolygonId> path)
	{
		return GetPathResult(m_NavMeshQuery, path.GetUnsafePtr(), path.Length);
	}

	[ThreadSafe]
	private unsafe static PathQueryStatus BeginFindPath(IntPtr navMeshQuery, NavMeshLocation start, NavMeshLocation end, int areaMask, void* costs)
	{
		return BeginFindPath_Injected(navMeshQuery, ref start, ref end, areaMask, costs);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	private static extern PathQueryStatus UpdateFindPath(IntPtr navMeshQuery, int iterations, out int iterationsPerformed);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	private static extern PathQueryStatus EndFindPath(IntPtr navMeshQuery, out int pathSize);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	private unsafe static extern int GetPathResult(IntPtr navMeshQuery, void* path, int maxPath);

	[ThreadSafe]
	private static bool IsValidPolygon(IntPtr navMeshQuery, PolygonId polygon)
	{
		return IsValidPolygon_Injected(navMeshQuery, ref polygon);
	}

	public bool IsValid(PolygonId polygon)
	{
		return polygon.polyRef != 0 && IsValidPolygon(m_NavMeshQuery, polygon);
	}

	public bool IsValid(NavMeshLocation location)
	{
		return IsValid(location.polygon);
	}

	[ThreadSafe]
	private static int GetAgentTypeIdForPolygon(IntPtr navMeshQuery, PolygonId polygon)
	{
		return GetAgentTypeIdForPolygon_Injected(navMeshQuery, ref polygon);
	}

	public int GetAgentTypeIdForPolygon(PolygonId polygon)
	{
		return GetAgentTypeIdForPolygon(m_NavMeshQuery, polygon);
	}

	[ThreadSafe]
	private static bool IsPositionInPolygon(IntPtr navMeshQuery, Vector3 position, PolygonId polygon)
	{
		return IsPositionInPolygon_Injected(navMeshQuery, ref position, ref polygon);
	}

	[ThreadSafe]
	private static PathQueryStatus GetClosestPointOnPoly(IntPtr navMeshQuery, PolygonId polygon, Vector3 position, out Vector3 nearest)
	{
		return GetClosestPointOnPoly_Injected(navMeshQuery, ref polygon, ref position, out nearest);
	}

	public NavMeshLocation CreateLocation(Vector3 position, PolygonId polygon)
	{
		Vector3 nearest;
		PathQueryStatus closestPointOnPoly = GetClosestPointOnPoly(m_NavMeshQuery, polygon, position, out nearest);
		return ((closestPointOnPoly & PathQueryStatus.Success) == 0) ? default(NavMeshLocation) : new NavMeshLocation(nearest, polygon);
	}

	[ThreadSafe]
	private static NavMeshLocation MapLocation(IntPtr navMeshQuery, Vector3 position, Vector3 extents, int agentTypeID, int areaMask = -1)
	{
		MapLocation_Injected(navMeshQuery, ref position, ref extents, agentTypeID, areaMask, out var ret);
		return ret;
	}

	public NavMeshLocation MapLocation(Vector3 position, Vector3 extents, int agentTypeID, int areaMask = -1)
	{
		return MapLocation(m_NavMeshQuery, position, extents, agentTypeID, areaMask);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	private unsafe static extern void MoveLocations(IntPtr navMeshQuery, void* locations, void* targets, void* areaMasks, int count);

	public unsafe void MoveLocations(NativeSlice<NavMeshLocation> locations, NativeSlice<Vector3> targets, NativeSlice<int> areaMasks)
	{
		MoveLocations(m_NavMeshQuery, locations.GetUnsafePtr(), targets.GetUnsafeReadOnlyPtr(), areaMasks.GetUnsafeReadOnlyPtr(), locations.Length);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[ThreadSafe]
	private unsafe static extern void MoveLocationsInSameAreas(IntPtr navMeshQuery, void* locations, void* targets, int count, int areaMask);

	public unsafe void MoveLocationsInSameAreas(NativeSlice<NavMeshLocation> locations, NativeSlice<Vector3> targets, int areaMask = -1)
	{
		MoveLocationsInSameAreas(m_NavMeshQuery, locations.GetUnsafePtr(), targets.GetUnsafeReadOnlyPtr(), locations.Length, areaMask);
	}

	[ThreadSafe]
	private static NavMeshLocation MoveLocation(IntPtr navMeshQuery, NavMeshLocation location, Vector3 target, int areaMask)
	{
		MoveLocation_Injected(navMeshQuery, ref location, ref target, areaMask, out var ret);
		return ret;
	}

	public NavMeshLocation MoveLocation(NavMeshLocation location, Vector3 target, int areaMask = -1)
	{
		return MoveLocation(m_NavMeshQuery, location, target, areaMask);
	}

	[ThreadSafe]
	private static bool GetPortalPoints(IntPtr navMeshQuery, PolygonId polygon, PolygonId neighbourPolygon, out Vector3 left, out Vector3 right)
	{
		return GetPortalPoints_Injected(navMeshQuery, ref polygon, ref neighbourPolygon, out left, out right);
	}

	public bool GetPortalPoints(PolygonId polygon, PolygonId neighbourPolygon, out Vector3 left, out Vector3 right)
	{
		return GetPortalPoints(m_NavMeshQuery, polygon, neighbourPolygon, out left, out right);
	}

	[ThreadSafe]
	private static Matrix4x4 PolygonLocalToWorldMatrix(IntPtr navMeshQuery, PolygonId polygon)
	{
		PolygonLocalToWorldMatrix_Injected(navMeshQuery, ref polygon, out var ret);
		return ret;
	}

	public Matrix4x4 PolygonLocalToWorldMatrix(PolygonId polygon)
	{
		return PolygonLocalToWorldMatrix(m_NavMeshQuery, polygon);
	}

	[ThreadSafe]
	private static Matrix4x4 PolygonWorldToLocalMatrix(IntPtr navMeshQuery, PolygonId polygon)
	{
		PolygonWorldToLocalMatrix_Injected(navMeshQuery, ref polygon, out var ret);
		return ret;
	}

	public Matrix4x4 PolygonWorldToLocalMatrix(PolygonId polygon)
	{
		return PolygonWorldToLocalMatrix(m_NavMeshQuery, polygon);
	}

	[ThreadSafe]
	private static NavMeshPolyTypes GetPolygonType(IntPtr navMeshQuery, PolygonId polygon)
	{
		return GetPolygonType_Injected(navMeshQuery, ref polygon);
	}

	public NavMeshPolyTypes GetPolygonType(PolygonId polygon)
	{
		return GetPolygonType(m_NavMeshQuery, polygon);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern IntPtr Create_Injected(ref NavMeshWorld world, int nodePoolSize);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private unsafe static extern PathQueryStatus BeginFindPath_Injected(IntPtr navMeshQuery, ref NavMeshLocation start, ref NavMeshLocation end, int areaMask, void* costs);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsValidPolygon_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern int GetAgentTypeIdForPolygon_Injected(IntPtr navMeshQuery, ref PolygonId polygon);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool IsPositionInPolygon_Injected(IntPtr navMeshQuery, ref Vector3 position, ref PolygonId polygon);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern PathQueryStatus GetClosestPointOnPoly_Injected(IntPtr navMeshQuery, ref PolygonId polygon, ref Vector3 position, out Vector3 nearest);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void MapLocation_Injected(IntPtr navMeshQuery, ref Vector3 position, ref Vector3 extents, int agentTypeID, int areaMask = -1, out NavMeshLocation ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void MoveLocation_Injected(IntPtr navMeshQuery, ref NavMeshLocation location, ref Vector3 target, int areaMask, out NavMeshLocation ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool GetPortalPoints_Injected(IntPtr navMeshQuery, ref PolygonId polygon, ref PolygonId neighbourPolygon, out Vector3 left, out Vector3 right);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void PolygonLocalToWorldMatrix_Injected(IntPtr navMeshQuery, ref PolygonId polygon, out Matrix4x4 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern void PolygonWorldToLocalMatrix_Injected(IntPtr navMeshQuery, ref PolygonId polygon, out Matrix4x4 ret);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern NavMeshPolyTypes GetPolygonType_Injected(IntPtr navMeshQuery, ref PolygonId polygon);
}
