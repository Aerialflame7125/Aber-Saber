namespace UnityEngine.Experimental.XR;

public struct PointCloudUpdatedEventArgs
{
	internal XRDepthSubsystem m_DepthSubsystem;

	public XRDepthSubsystem DepthSubsystem => m_DepthSubsystem;
}
