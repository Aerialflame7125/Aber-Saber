using System;
using UnityEngine.Bindings;

namespace UnityEngine;

[Serializable]
public struct ContactFilter2D
{
	[NativeName("m_UseTriggers")]
	public bool useTriggers;

	[NativeName("m_UseLayerMask")]
	public bool useLayerMask;

	[NativeName("m_UseDepth")]
	public bool useDepth;

	[NativeName("m_UseOutsideDepth")]
	public bool useOutsideDepth;

	[NativeName("m_UseNormalAngle")]
	public bool useNormalAngle;

	[NativeName("m_UseOutsideNormalAngle")]
	public bool useOutsideNormalAngle;

	[NativeName("m_LayerMask")]
	public LayerMask layerMask;

	[NativeName("m_MinDepth")]
	public float minDepth;

	[NativeName("m_MaxDepth")]
	public float maxDepth;

	[NativeName("m_MinNormalAngle")]
	public float minNormalAngle;

	[NativeName("m_MaxNormalAngle")]
	public float maxNormalAngle;

	public const float NormalAngleUpperLimit = 359.9999f;

	public bool isFiltering => !useTriggers || useLayerMask || useDepth || useNormalAngle;

	public ContactFilter2D NoFilter()
	{
		useTriggers = true;
		useLayerMask = false;
		layerMask = -1;
		useDepth = false;
		useOutsideDepth = false;
		minDepth = float.NegativeInfinity;
		maxDepth = float.PositiveInfinity;
		useNormalAngle = false;
		useOutsideNormalAngle = false;
		minNormalAngle = 0f;
		maxNormalAngle = 359.9999f;
		return this;
	}

	private void CheckConsistency()
	{
		minDepth = ((minDepth != float.NegativeInfinity && minDepth != float.PositiveInfinity && !float.IsNaN(minDepth)) ? minDepth : float.MinValue);
		maxDepth = ((maxDepth != float.NegativeInfinity && maxDepth != float.PositiveInfinity && !float.IsNaN(maxDepth)) ? maxDepth : float.MaxValue);
		if (minDepth > maxDepth)
		{
			float num = minDepth;
			minDepth = maxDepth;
			maxDepth = num;
		}
		minNormalAngle = ((!float.IsNaN(minNormalAngle)) ? Mathf.Clamp(minNormalAngle, 0f, 359.9999f) : 0f);
		maxNormalAngle = ((!float.IsNaN(maxNormalAngle)) ? Mathf.Clamp(maxNormalAngle, 0f, 359.9999f) : 359.9999f);
		if (minNormalAngle > maxNormalAngle)
		{
			float num2 = minNormalAngle;
			minNormalAngle = maxNormalAngle;
			maxNormalAngle = num2;
		}
	}

	public void ClearLayerMask()
	{
		useLayerMask = false;
	}

	public void SetLayerMask(LayerMask layerMask)
	{
		this.layerMask = layerMask;
		useLayerMask = true;
	}

	public void ClearDepth()
	{
		useDepth = false;
	}

	public void SetDepth(float minDepth, float maxDepth)
	{
		this.minDepth = minDepth;
		this.maxDepth = maxDepth;
		useDepth = true;
		CheckConsistency();
	}

	public void ClearNormalAngle()
	{
		useNormalAngle = false;
	}

	public void SetNormalAngle(float minNormalAngle, float maxNormalAngle)
	{
		this.minNormalAngle = minNormalAngle;
		this.maxNormalAngle = maxNormalAngle;
		useNormalAngle = true;
		CheckConsistency();
	}

	public bool IsFilteringTrigger([Writable] Collider2D collider)
	{
		return !useTriggers && collider.isTrigger;
	}

	public bool IsFilteringLayerMask(GameObject obj)
	{
		return useLayerMask && ((int)layerMask & (1 << obj.layer)) == 0;
	}

	public bool IsFilteringDepth(GameObject obj)
	{
		if (!useDepth)
		{
			return false;
		}
		if (minDepth > maxDepth)
		{
			float num = minDepth;
			minDepth = maxDepth;
			maxDepth = num;
		}
		float z = obj.transform.position.z;
		bool flag = z < minDepth || z > maxDepth;
		if (useOutsideDepth)
		{
			return !flag;
		}
		return flag;
	}

	public bool IsFilteringNormalAngle(Vector2 normal)
	{
		float angle = Mathf.Atan2(normal.y, normal.x) * 57.29578f;
		return IsFilteringNormalAngle(angle);
	}

	public bool IsFilteringNormalAngle(float angle)
	{
		angle -= Mathf.Floor(angle / 359.9999f) * 359.9999f;
		float num = Mathf.Clamp(minNormalAngle, 0f, 359.9999f);
		float num2 = Mathf.Clamp(maxNormalAngle, 0f, 359.9999f);
		if (num > num2)
		{
			float num3 = num;
			num = num2;
			num2 = num3;
		}
		bool flag = angle < num || angle > num2;
		if (useOutsideNormalAngle)
		{
			return !flag;
		}
		return flag;
	}

	internal static ContactFilter2D CreateLegacyFilter(int layerMask, float minDepth, float maxDepth)
	{
		ContactFilter2D result = default(ContactFilter2D);
		result.useTriggers = Physics2D.queriesHitTriggers;
		result.SetLayerMask(layerMask);
		result.SetDepth(minDepth, maxDepth);
		return result;
	}
}
