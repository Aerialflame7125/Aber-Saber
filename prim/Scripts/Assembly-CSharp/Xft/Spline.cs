using System.Collections.Generic;
using UnityEngine;

namespace Xft;

public class Spline
{
	private List<SplineControlPoint> mControlPoints = new List<SplineControlPoint>();

	private List<SplineControlPoint> mSegments = new List<SplineControlPoint>();

	public int Granularity = 20;

	public SplineControlPoint this[int index]
	{
		get
		{
			if (index > -1 && index < mSegments.Count)
			{
				return mSegments[index];
			}
			return null;
		}
	}

	public List<SplineControlPoint> Segments => mSegments;

	public List<SplineControlPoint> ControlPoints => mControlPoints;

	public SplineControlPoint NextControlPoint(SplineControlPoint controlpoint)
	{
		if (mControlPoints.Count == 0)
		{
			return null;
		}
		int num = controlpoint.ControlPointIndex + 1;
		if (num >= mControlPoints.Count)
		{
			return null;
		}
		return mControlPoints[num];
	}

	public SplineControlPoint PreviousControlPoint(SplineControlPoint controlpoint)
	{
		if (mControlPoints.Count == 0)
		{
			return null;
		}
		int num = controlpoint.ControlPointIndex - 1;
		if (num < 0)
		{
			return null;
		}
		return mControlPoints[num];
	}

	public Vector3 NextPosition(SplineControlPoint controlpoint)
	{
		return NextControlPoint(controlpoint)?.Position ?? controlpoint.Position;
	}

	public Vector3 PreviousPosition(SplineControlPoint controlpoint)
	{
		return PreviousControlPoint(controlpoint)?.Position ?? controlpoint.Position;
	}

	public Vector3 PreviousNormal(SplineControlPoint controlpoint)
	{
		return PreviousControlPoint(controlpoint)?.Normal ?? controlpoint.Normal;
	}

	public Vector3 NextNormal(SplineControlPoint controlpoint)
	{
		return NextControlPoint(controlpoint)?.Normal ?? controlpoint.Normal;
	}

	public SplineControlPoint LenToSegment(float t, out float localF)
	{
		SplineControlPoint splineControlPoint = null;
		t = Mathf.Clamp01(t);
		float num = t * mSegments[mSegments.Count - 1].Dist;
		int num2 = 0;
		for (num2 = 0; num2 < mSegments.Count; num2++)
		{
			if (mSegments[num2].Dist >= num)
			{
				splineControlPoint = mSegments[num2];
				break;
			}
		}
		if (num2 == 0)
		{
			localF = 0f;
			return splineControlPoint;
		}
		float num3 = 0f;
		int index = splineControlPoint.SegmentIndex - 1;
		SplineControlPoint splineControlPoint2 = mSegments[index];
		num3 = splineControlPoint.Dist - splineControlPoint2.Dist;
		localF = (num - splineControlPoint2.Dist) / num3;
		return splineControlPoint2;
	}

	public static Vector3 CatmulRom(Vector3 T0, Vector3 P0, Vector3 P1, Vector3 T1, float f)
	{
		float num = -0.5f;
		float num2 = 1.5f;
		float num3 = -1.5f;
		float num4 = 0.5f;
		float num5 = -2.5f;
		float num6 = 2f;
		float num7 = -0.5f;
		float num8 = -0.5f;
		float num9 = 0.5f;
		float num10 = num * T0.x + num2 * P0.x + num3 * P1.x + num4 * T1.x;
		float num11 = T0.x + num5 * P0.x + num6 * P1.x + num7 * T1.x;
		float num12 = num8 * T0.x + num9 * P1.x;
		float x = P0.x;
		float num13 = num * T0.y + num2 * P0.y + num3 * P1.y + num4 * T1.y;
		float num14 = T0.y + num5 * P0.y + num6 * P1.y + num7 * T1.y;
		float num15 = num8 * T0.y + num9 * P1.y;
		float y = P0.y;
		float num16 = num * T0.z + num2 * P0.z + num3 * P1.z + num4 * T1.z;
		float num17 = T0.z + num5 * P0.z + num6 * P1.z + num7 * T1.z;
		float num18 = num8 * T0.z + num9 * P1.z;
		float z = P0.z;
		float x2 = ((num10 * f + num11) * f + num12) * f + x;
		float y2 = ((num13 * f + num14) * f + num15) * f + y;
		float z2 = ((num16 * f + num17) * f + num18) * f + z;
		return new Vector3(x2, y2, z2);
	}

	public Vector3 InterpolateByLen(float tl)
	{
		float localF;
		SplineControlPoint splineControlPoint = LenToSegment(tl, out localF);
		return splineControlPoint.Interpolate(localF);
	}

	public Vector3 InterpolateNormalByLen(float tl)
	{
		float localF;
		SplineControlPoint splineControlPoint = LenToSegment(tl, out localF);
		return splineControlPoint.InterpolateNormal(localF);
	}

	public SplineControlPoint AddControlPoint(Vector3 pos, Vector3 up)
	{
		SplineControlPoint splineControlPoint = new SplineControlPoint();
		splineControlPoint.Init(this);
		splineControlPoint.Position = pos;
		splineControlPoint.Normal = up;
		mControlPoints.Add(splineControlPoint);
		splineControlPoint.ControlPointIndex = mControlPoints.Count - 1;
		return splineControlPoint;
	}

	public void Clear()
	{
		mControlPoints.Clear();
	}

	private void RefreshDistance()
	{
		if (mSegments.Count >= 1)
		{
			mSegments[0].Dist = 0f;
			for (int i = 1; i < mSegments.Count; i++)
			{
				float magnitude = (mSegments[i].Position - mSegments[i - 1].Position).magnitude;
				mSegments[i].Dist = mSegments[i - 1].Dist + magnitude;
			}
		}
	}

	public void RefreshSpline()
	{
		mSegments.Clear();
		for (int i = 0; i < mControlPoints.Count; i++)
		{
			if (mControlPoints[i].IsValid)
			{
				mSegments.Add(mControlPoints[i]);
				mControlPoints[i].SegmentIndex = mSegments.Count - 1;
			}
		}
		RefreshDistance();
	}
}
