using UnityEngine.Scripting;

namespace UnityEngine;

[UsedByNativeCode]
public struct ContactPoint
{
	internal Vector3 m_Point;

	internal Vector3 m_Normal;

	internal int m_ThisColliderInstanceID;

	internal int m_OtherColliderInstanceID;

	internal float m_Separation;

	public Vector3 point => m_Point;

	public Vector3 normal => m_Normal;

	public Collider thisCollider => Object.FindObjectFromInstanceID(m_ThisColliderInstanceID) as Collider;

	public Collider otherCollider => Object.FindObjectFromInstanceID(m_OtherColliderInstanceID) as Collider;

	public float separation => m_Separation;
}
