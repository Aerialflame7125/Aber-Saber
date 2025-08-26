using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering;

[UsedByNativeCode]
public struct CameraProperties
{
	[StructLayout(LayoutKind.Sequential, Size = 96)]
	[UnsafeValueType]
	[CompilerGenerated]
	public struct _003C_shadowCullPlanes_003E__FixedBuffer1
	{
		public float FixedElementField;
	}

	[StructLayout(LayoutKind.Sequential, Size = 96)]
	[UnsafeValueType]
	[CompilerGenerated]
	public struct _003C_cameraCullPlanes_003E__FixedBuffer2
	{
		public float FixedElementField;
	}

	[StructLayout(LayoutKind.Sequential, Size = 128)]
	[UnsafeValueType]
	[CompilerGenerated]
	public struct _003ClayerCullDistances_003E__FixedBuffer3
	{
		public float FixedElementField;
	}

	private const int kNumLayers = 32;

	private Rect screenRect;

	private Vector3 viewDir;

	private float projectionNear;

	private float projectionFar;

	private float cameraNear;

	private float cameraFar;

	private float cameraAspect;

	private Matrix4x4 cameraToWorld;

	private Matrix4x4 actualWorldToClip;

	private Matrix4x4 cameraClipToWorld;

	private Matrix4x4 cameraWorldToClip;

	private Matrix4x4 implicitProjection;

	private Matrix4x4 stereoWorldToClipLeft;

	private Matrix4x4 stereoWorldToClipRight;

	private Matrix4x4 worldToCamera;

	private Vector3 up;

	private Vector3 right;

	private Vector3 transformDirection;

	private Vector3 cameraEuler;

	private Vector3 velocity;

	private float farPlaneWorldSpaceLength;

	private uint rendererCount;

	private _003C_shadowCullPlanes_003E__FixedBuffer1 _shadowCullPlanes;

	private _003C_cameraCullPlanes_003E__FixedBuffer2 _cameraCullPlanes;

	private float baseFarDistance;

	private Vector3 shadowCullCenter;

	private _003ClayerCullDistances_003E__FixedBuffer3 layerCullDistances;

	private int layerCullSpherical;

	private CoreCameraValues coreCameraValues;

	private uint cameraType;

	public unsafe Plane GetShadowCullingPlane(int index)
	{
		if (index < 0 || index >= 6)
		{
			throw new IndexOutOfRangeException("Invalid plane index");
		}
		fixed (float* shadowCullPlanes = &_shadowCullPlanes.FixedElementField)
		{
			return new Plane(new Vector3(System.Runtime.CompilerServices.Unsafe.Add(ref *shadowCullPlanes, index * 4), System.Runtime.CompilerServices.Unsafe.Add(ref *shadowCullPlanes, index * 4 + 1), System.Runtime.CompilerServices.Unsafe.Add(ref *shadowCullPlanes, index * 4 + 2)), System.Runtime.CompilerServices.Unsafe.Add(ref *shadowCullPlanes, index * 4 + 3));
		}
	}

	public unsafe void SetShadowCullingPlane(int index, Plane plane)
	{
		if (index < 0 || index >= 6)
		{
			throw new IndexOutOfRangeException("Invalid plane index");
		}
		fixed (float* shadowCullPlanes = &_shadowCullPlanes.FixedElementField)
		{
			System.Runtime.CompilerServices.Unsafe.Add(ref *shadowCullPlanes, index * 4) = plane.normal.x;
			System.Runtime.CompilerServices.Unsafe.Add(ref *shadowCullPlanes, index * 4 + 1) = plane.normal.y;
			System.Runtime.CompilerServices.Unsafe.Add(ref *shadowCullPlanes, index * 4 + 2) = plane.normal.z;
			System.Runtime.CompilerServices.Unsafe.Add(ref *shadowCullPlanes, index * 4 + 3) = plane.distance;
		}
	}

	public unsafe Plane GetCameraCullingPlane(int index)
	{
		if (index < 0 || index >= 6)
		{
			throw new IndexOutOfRangeException("Invalid plane index");
		}
		fixed (float* cameraCullPlanes = &_cameraCullPlanes.FixedElementField)
		{
			return new Plane(new Vector3(System.Runtime.CompilerServices.Unsafe.Add(ref *cameraCullPlanes, index * 4), System.Runtime.CompilerServices.Unsafe.Add(ref *cameraCullPlanes, index * 4 + 1), System.Runtime.CompilerServices.Unsafe.Add(ref *cameraCullPlanes, index * 4 + 2)), System.Runtime.CompilerServices.Unsafe.Add(ref *cameraCullPlanes, index * 4 + 3));
		}
	}

	public unsafe void SetCameraCullingPlane(int index, Plane plane)
	{
		if (index < 0 || index >= 6)
		{
			throw new IndexOutOfRangeException("Invalid plane index");
		}
		fixed (float* cameraCullPlanes = &_cameraCullPlanes.FixedElementField)
		{
			System.Runtime.CompilerServices.Unsafe.Add(ref *cameraCullPlanes, index * 4) = plane.normal.x;
			System.Runtime.CompilerServices.Unsafe.Add(ref *cameraCullPlanes, index * 4 + 1) = plane.normal.y;
			System.Runtime.CompilerServices.Unsafe.Add(ref *cameraCullPlanes, index * 4 + 2) = plane.normal.z;
			System.Runtime.CompilerServices.Unsafe.Add(ref *cameraCullPlanes, index * 4 + 3) = plane.distance;
		}
	}
}
