namespace UnityEngine.AI;

public struct NavMeshLinkInstance
{
	private int m_Handle;

	public bool valid => m_Handle != 0 && NavMesh.IsValidLinkHandle(m_Handle);

	internal int id
	{
		get
		{
			return m_Handle;
		}
		set
		{
			m_Handle = value;
		}
	}

	public Object owner
	{
		get
		{
			return NavMesh.InternalGetLinkOwner(id);
		}
		set
		{
			int ownerID = ((value != null) ? value.GetInstanceID() : 0);
			if (!NavMesh.InternalSetLinkOwner(id, ownerID))
			{
				Debug.LogError("Cannot set 'owner' on an invalid NavMeshLinkInstance");
			}
		}
	}

	public void Remove()
	{
		NavMesh.RemoveLinkInternal(id);
	}
}
