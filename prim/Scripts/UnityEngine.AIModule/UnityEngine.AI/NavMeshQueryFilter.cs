using System;

namespace UnityEngine.AI;

public struct NavMeshQueryFilter
{
	private const int AREA_COST_ELEMENT_COUNT = 32;

	private int m_AreaMask;

	private int m_AgentTypeID;

	private float[] m_AreaCost;

	internal float[] costs => m_AreaCost;

	public int areaMask
	{
		get
		{
			return m_AreaMask;
		}
		set
		{
			m_AreaMask = value;
		}
	}

	public int agentTypeID
	{
		get
		{
			return m_AgentTypeID;
		}
		set
		{
			m_AgentTypeID = value;
		}
	}

	public float GetAreaCost(int areaIndex)
	{
		if (m_AreaCost == null)
		{
			if (areaIndex < 0 || areaIndex >= 32)
			{
				string message = $"The valid range is [0:{31}]";
				throw new IndexOutOfRangeException(message);
			}
			return 1f;
		}
		return m_AreaCost[areaIndex];
	}

	public void SetAreaCost(int areaIndex, float cost)
	{
		if (m_AreaCost == null)
		{
			m_AreaCost = new float[32];
			for (int i = 0; i < 32; i++)
			{
				m_AreaCost[i] = 1f;
			}
		}
		m_AreaCost[areaIndex] = cost;
	}
}
