using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine;

[RequiredByNativeCode(Optional = true)]
public struct ParticleCollisionEvent
{
	private Vector3 m_Intersection;

	private Vector3 m_Normal;

	private Vector3 m_Velocity;

	private int m_ColliderInstanceID;

	public Vector3 intersection => m_Intersection;

	public Vector3 normal => m_Normal;

	public Vector3 velocity => m_Velocity;

	public Component colliderComponent => InstanceIDToColliderComponent(m_ColliderInstanceID);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern Component InstanceIDToColliderComponent(int instanceID);
}
