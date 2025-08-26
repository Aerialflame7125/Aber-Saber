using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine;

[StructLayout(LayoutKind.Sequential)]
[UsedByNativeCode]
public class Collision2D
{
	internal int m_Collider;

	internal int m_OtherCollider;

	internal int m_Rigidbody;

	internal int m_OtherRigidbody;

	internal Vector2 m_RelativeVelocity;

	internal int m_Enabled;

	internal int m_ContactCount;

	internal CachedContactPoints2D m_CachedContactPoints;

	internal ContactPoint2D[] m_LegacyContactArray;

	public Collider2D collider => Object.FindObjectFromInstanceID(m_Collider) as Collider2D;

	public Collider2D otherCollider => Object.FindObjectFromInstanceID(m_OtherCollider) as Collider2D;

	public Rigidbody2D rigidbody => Object.FindObjectFromInstanceID(m_Rigidbody) as Rigidbody2D;

	public Rigidbody2D otherRigidbody => Object.FindObjectFromInstanceID(m_OtherRigidbody) as Rigidbody2D;

	public Transform transform => (!(rigidbody != null)) ? collider.transform : rigidbody.transform;

	public GameObject gameObject => (!(rigidbody != null)) ? collider.gameObject : rigidbody.gameObject;

	public Vector2 relativeVelocity => m_RelativeVelocity;

	public bool enabled => m_Enabled == 1;

	public ContactPoint2D[] contacts
	{
		get
		{
			if (m_LegacyContactArray == null)
			{
				m_LegacyContactArray = new ContactPoint2D[m_ContactCount];
				if (m_ContactCount > 0)
				{
					for (int i = 0; i < m_ContactCount; i++)
					{
						ref ContactPoint2D reference = ref m_LegacyContactArray[i];
						reference = m_CachedContactPoints[i];
					}
				}
			}
			return m_LegacyContactArray;
		}
	}

	public int GetContacts(ContactPoint2D[] contacts)
	{
		if (contacts == null)
		{
			throw new ArgumentNullException("Cannot get contacts into a NULL array.");
		}
		int num = Mathf.Min(contacts.Length, m_ContactCount);
		if (num == 0)
		{
			return 0;
		}
		if (m_LegacyContactArray != null)
		{
			Array.Copy(m_LegacyContactArray, contacts, num);
			return num;
		}
		if (m_ContactCount > 0)
		{
			for (int i = 0; i < num; i++)
			{
				ref ContactPoint2D reference = ref contacts[i];
				reference = m_CachedContactPoints[i];
			}
		}
		return num;
	}
}
