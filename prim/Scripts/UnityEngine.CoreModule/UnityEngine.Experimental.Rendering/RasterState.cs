using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering;

public struct RasterState
{
	public static readonly RasterState Default = new RasterState(CullMode.Back, 0, 0f, depthClip: true);

	private CullMode m_CullingMode;

	private int m_OffsetUnits;

	private float m_OffsetFactor;

	private byte m_DepthClip;

	public CullMode cullingMode
	{
		get
		{
			return m_CullingMode;
		}
		set
		{
			m_CullingMode = value;
		}
	}

	public bool depthClip
	{
		get
		{
			return Convert.ToBoolean(m_DepthClip);
		}
		set
		{
			m_DepthClip = Convert.ToByte(value);
		}
	}

	public int offsetUnits
	{
		get
		{
			return m_OffsetUnits;
		}
		set
		{
			m_OffsetUnits = value;
		}
	}

	public float offsetFactor
	{
		get
		{
			return m_OffsetFactor;
		}
		set
		{
			m_OffsetFactor = value;
		}
	}

	public RasterState(CullMode cullingMode = CullMode.Back, int offsetUnits = 0, float offsetFactor = 0f, bool depthClip = true)
	{
		m_CullingMode = cullingMode;
		m_OffsetUnits = offsetUnits;
		m_OffsetFactor = offsetFactor;
		m_DepthClip = Convert.ToByte(depthClip);
	}
}
