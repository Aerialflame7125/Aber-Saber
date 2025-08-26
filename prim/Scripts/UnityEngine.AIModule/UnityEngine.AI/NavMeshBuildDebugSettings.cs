namespace UnityEngine.AI;

public struct NavMeshBuildDebugSettings
{
	private byte m_Flags;

	public NavMeshBuildDebugFlags flags
	{
		get
		{
			return (NavMeshBuildDebugFlags)m_Flags;
		}
		set
		{
			m_Flags = (byte)value;
		}
	}
}
