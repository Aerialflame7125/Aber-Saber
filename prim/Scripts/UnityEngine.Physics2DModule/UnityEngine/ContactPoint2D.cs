using UnityEngine.Scripting;

namespace UnityEngine;

[UsedByNativeCode]
public struct ContactPoint2D
{
	private Vector2 m_Point;

	private Vector2 m_Normal;

	private Vector2 m_RelativeVelocity;

	private float m_Separation;

	private float m_NormalImpulse;

	private float m_TangentImpulse;

	private int m_Collider;

	private int m_OtherCollider;

	private int m_Rigidbody;

	private int m_OtherRigidbody;

	private int m_Enabled;

	public Vector2 point => m_Point;

	public Vector2 normal => m_Normal;

	public float separation => m_Separation;

	public float normalImpulse => m_NormalImpulse;

	public float tangentImpulse => m_TangentImpulse;

	public Vector2 relativeVelocity => m_RelativeVelocity;

	public Collider2D collider => Object.FindObjectFromInstanceID(m_Collider) as Collider2D;

	public Collider2D otherCollider => Object.FindObjectFromInstanceID(m_OtherCollider) as Collider2D;

	public Rigidbody2D rigidbody => Object.FindObjectFromInstanceID(m_Rigidbody) as Rigidbody2D;

	public Rigidbody2D otherRigidbody => Object.FindObjectFromInstanceID(m_OtherRigidbody) as Rigidbody2D;

	public bool enabled => m_Enabled == 1;
}
