using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering;

[UsedByNativeCode]
public struct ShadowSplitData
{
	[StructLayout(LayoutKind.Sequential, Size = 160)]
	[UnsafeValueType]
	[CompilerGenerated]
	public struct _003C_cullingPlanes_003E__FixedBuffer6
	{
		public float FixedElementField;
	}

	public int cullingPlaneCount;

	private _003C_cullingPlanes_003E__FixedBuffer6 _cullingPlanes;

	public Vector4 cullingSphere;

	public unsafe Plane GetCullingPlane(int index)
	{
		if (index < 0 || index >= cullingPlaneCount || index >= 10)
		{
			throw new IndexOutOfRangeException("Invalid plane index");
		}
		fixed (float* cullingPlanes = &_cullingPlanes.FixedElementField)
		{
			return new Plane(new Vector3(System.Runtime.CompilerServices.Unsafe.Add(ref *cullingPlanes, index * 4), System.Runtime.CompilerServices.Unsafe.Add(ref *cullingPlanes, index * 4 + 1), System.Runtime.CompilerServices.Unsafe.Add(ref *cullingPlanes, index * 4 + 2)), System.Runtime.CompilerServices.Unsafe.Add(ref *cullingPlanes, index * 4 + 3));
		}
	}

	public unsafe void SetCullingPlane(int index, Plane plane)
	{
		if (index < 0 || index >= cullingPlaneCount || index >= 10)
		{
			throw new IndexOutOfRangeException("Invalid plane index");
		}
		fixed (float* cullingPlanes = &_cullingPlanes.FixedElementField)
		{
			System.Runtime.CompilerServices.Unsafe.Add(ref *cullingPlanes, index * 4) = plane.normal.x;
			System.Runtime.CompilerServices.Unsafe.Add(ref *cullingPlanes, index * 4 + 1) = plane.normal.y;
			System.Runtime.CompilerServices.Unsafe.Add(ref *cullingPlanes, index * 4 + 2) = plane.normal.z;
			System.Runtime.CompilerServices.Unsafe.Add(ref *cullingPlanes, index * 4 + 3) = plane.distance;
		}
	}
}
