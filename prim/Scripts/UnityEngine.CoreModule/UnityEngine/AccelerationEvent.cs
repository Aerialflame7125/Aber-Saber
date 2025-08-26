namespace UnityEngine;

public struct AccelerationEvent
{
	private float x;

	private float y;

	private float z;

	private float m_TimeDelta;

	public Vector3 acceleration => new Vector3(x, y, z);

	public float deltaTime => m_TimeDelta;
}
