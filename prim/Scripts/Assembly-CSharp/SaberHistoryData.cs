using UnityEngine;

public class SaberHistoryData
{
	public struct TimeAndPos
	{
		public Vector3 topPos;

		public Vector3 bottomPos;

		public float time;
	}

	private TimeAndPos[] _data;

	private int _nextAddIndex;

	private int _validCount;

	public TimeAndPos lastAddedTimeAndPos
	{
		get
		{
			int num = _nextAddIndex - 1;
			if (num < 0)
			{
				num += _data.Length;
			}
			return _data[num];
		}
	}

	public SaberHistoryData()
	{
		_data = new TimeAndPos[300];
	}

	public void AddNewData(Vector3 topPos, Vector3 bottomPos, float time)
	{
		_data[_nextAddIndex].topPos = topPos;
		_data[_nextAddIndex].bottomPos = bottomPos;
		_data[_nextAddIndex].time = time;
		_nextAddIndex = (_nextAddIndex + 1) % _data.Length;
		if (_validCount < _data.Length)
		{
			_validCount++;
		}
	}

	public Vector3 ComputeCutPlaneNormal()
	{
		int num = _data.Length;
		int num2 = _nextAddIndex - 1;
		if (num2 < 0)
		{
			num2 += num;
		}
		int num3 = num2 - 1;
		if (num3 < 0)
		{
			num3 += num;
		}
		Vector3 topPos = _data[num2].topPos;
		Vector3 bottomPos = _data[num2].bottomPos;
		Vector3 topPos2 = _data[num3].topPos;
		Vector3 bottomPos2 = _data[num3].bottomPos;
		return Vector3.Cross(topPos - bottomPos, (topPos2 + bottomPos2) * 0.5f - bottomPos).normalized;
	}

	public float ComputeSwingRating()
	{
		if (_validCount < 2)
		{
			return 0f;
		}
		int num = _data.Length;
		int num2 = 0;
		int num3 = _nextAddIndex - 1;
		if (num3 < 0)
		{
			num3 += num;
		}
		int num4 = num3 - 1;
		if (num4 < 0)
		{
			num4 += num;
		}
		Vector3 to = ComputeCutPlaneNormal();
		float time = _data[num3].time;
		float num5 = time;
		float num6 = 0f;
		while (time - num5 < 0.4f && num2 < _validCount)
		{
			num3 = num4--;
			if (num4 < 0)
			{
				num4 += num;
			}
			Vector3 topPos = _data[num3].topPos;
			Vector3 bottomPos = _data[num3].bottomPos;
			Vector3 topPos2 = _data[num4].topPos;
			Vector3 bottomPos2 = _data[num4].bottomPos;
			Vector3 normalized = Vector3.Cross(topPos - bottomPos, (topPos2 + bottomPos2) * 0.5f - bottomPos).normalized;
			Vector3 vector = topPos2 - bottomPos2 + bottomPos;
			float angleDiff = Vector3.Angle(vector - bottomPos, topPos - bottomPos);
			float num7 = Vector3.Angle(normalized, to);
			if (num7 > 90f)
			{
				break;
			}
			num6 += SaberSwingRating.BeforeCutStepRating(angleDiff, num7);
			num5 = _data[num3].time;
			num2++;
		}
		if (num6 > 1f)
		{
			num6 = 1f;
		}
		return num6;
	}
}
