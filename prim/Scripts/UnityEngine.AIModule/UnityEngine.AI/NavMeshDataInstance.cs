namespace UnityEngine.AI;

public struct NavMeshDataInstance
{
	private int m_Handle;

	public bool valid => m_Handle != 0 && NavMesh.IsValidNavMeshDataHandle(m_Handle);

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
			return NavMesh.InternalGetOwner(id);
		}
		set
		{
			int ownerID = ((value != null) ? value.GetInstanceID() : 0);
			if (!NavMesh.InternalSetOwner(id, ownerID))
			{
				Debug.LogError("Cannot set 'owner' on an invalid NavMeshDataInstance");
			}
		}
	}

	public void Remove()
	{
		NavMesh.RemoveNavMeshDataInternal(id);
	}
}
