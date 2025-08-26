using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.XR;

[NativeHeader("Modules/XR/XRPrefix.h")]
[NativeConditional("ENABLE_XR")]
[NativeHeader("XRScriptingClasses.h")]
[UsedByNativeCode]
[NativeHeader("Modules/XR/Subsystems/Planes/XRBoundedPlane.h")]
public struct BoundedPlane
{
	private uint m_InstanceId;

	public TrackableId Id { get; set; }

	public TrackableId SubsumedById { get; set; }

	public Pose Pose { get; set; }

	public Vector3 Center { get; set; }

	public Vector2 Size { get; set; }

	public PlaneAlignment Alignment { get; set; }

	public float Width => Size.x;

	public float Height => Size.y;

	public Vector3 Normal => Pose.up;

	public Plane Plane => new Plane(Normal, Center);

	public void GetCorners(out Vector3 p0, out Vector3 p1, out Vector3 p2, out Vector3 p3)
	{
		Vector3 vector = Pose.right * (Width * 0.5f);
		Vector3 vector2 = Pose.forward * (Height * 0.5f);
		p0 = Center - vector - vector2;
		p1 = Center - vector + vector2;
		p2 = Center + vector + vector2;
		p3 = Center + vector - vector2;
	}

	public bool TryGetBoundary(List<Vector3> boundaryOut)
	{
		if (boundaryOut == null)
		{
			throw new ArgumentNullException("boundaryOut");
		}
		return Internal_GetBoundaryAsList(m_InstanceId, Id, boundaryOut);
	}

	private static Vector3[] Internal_GetBoundaryAsFixedArray(uint instanceId, TrackableId id)
	{
		return Internal_GetBoundaryAsFixedArray_Injected(instanceId, ref id);
	}

	private static bool Internal_GetBoundaryAsList(uint instanceId, TrackableId id, List<Vector3> boundaryOut)
	{
		return Internal_GetBoundaryAsList_Injected(instanceId, ref id, boundaryOut);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern Vector3[] Internal_GetBoundaryAsFixedArray_Injected(uint instanceId, ref TrackableId id);

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern bool Internal_GetBoundaryAsList_Injected(uint instanceId, ref TrackableId id, List<Vector3> boundaryOut);
}
