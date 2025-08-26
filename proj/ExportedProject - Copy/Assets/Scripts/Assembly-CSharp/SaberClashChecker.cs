using UnityEngine;

public class SaberClashChecker : MonoBehaviour
{
	[SerializeField]
	private Saber _leftSaber;

	[SerializeField]
	private Saber _rightSaber;

	public bool sabersAreClashing { get; private set; }

	public Vector3 clashingPoint { get; private set; }

	private void Update()
	{
		Vector3 inbetweenPoint;
		float num = SegmentToSegmentDist(_leftSaber.saberBladeBottomPos, _leftSaber.saberBladeTopPos, _rightSaber.saberBladeBottomPos, _rightSaber.saberBladeTopPos, out inbetweenPoint);
		if (num < 0.04f && _leftSaber.isActiveAndEnabled && _rightSaber.isActiveAndEnabled)
		{
			clashingPoint = inbetweenPoint;
			sabersAreClashing = true;
		}
		else
		{
			sabersAreClashing = false;
		}
	}

	private void OnDisable()
	{
		sabersAreClashing = false;
	}

	private float SegmentToSegmentDist(Vector3 fromA, Vector3 toA, Vector3 fromB, Vector3 toB, out Vector3 inbetweenPoint)
	{
		float num = 1E-06f;
		Vector3 vector = toA - fromA;
		Vector3 vector2 = toB - fromB;
		Vector3 rhs = fromA - fromB;
		float num2 = Vector3.Dot(vector, vector);
		float num3 = Vector3.Dot(vector, vector2);
		float num4 = Vector3.Dot(vector2, vector2);
		float num5 = Vector3.Dot(vector, rhs);
		float num6 = Vector3.Dot(vector2, rhs);
		float num7 = num2 * num4 - num3 * num3;
		float num8 = num7;
		float num9 = num7;
		float num10;
		float num11;
		if (num7 < num)
		{
			num10 = 0f;
			num8 = 1f;
			num11 = num6;
			num9 = num4;
		}
		else
		{
			num10 = num3 * num6 - num4 * num5;
			num11 = num2 * num6 - num3 * num5;
			if (num10 < 0f)
			{
				num10 = 0f;
				num11 = num6;
				num9 = num4;
			}
			else if (num10 > num8)
			{
				num10 = num8;
				num11 = num6 + num3;
				num9 = num4;
			}
		}
		if (num11 < 0f)
		{
			num11 = 0f;
			if (0f - num5 < 0f)
			{
				num10 = 0f;
			}
			else if (0f - num5 > num2)
			{
				num10 = num8;
			}
			else
			{
				num10 = 0f - num5;
				num8 = num2;
			}
		}
		else if (num11 > num9)
		{
			num11 = num9;
			if (0f - num5 + num3 < 0f)
			{
				num10 = 0f;
			}
			else if (0f - num5 + num3 > num2)
			{
				num10 = num8;
			}
			else
			{
				num10 = 0f - num5 + num3;
				num8 = num2;
			}
		}
		float num12 = ((!(Mathf.Abs(num10) < num)) ? (num10 / num8) : 0f);
		float num13 = ((!(Mathf.Abs(num11) < num)) ? (num11 / num9) : 0f);
		Vector3 vector3 = fromA + num12 * vector;
		Vector3 vector4 = fromB + num13 * vector2;
		inbetweenPoint = (vector3 + vector4) * 0.5f;
		return (vector3 - vector4).magnitude;
	}
}
