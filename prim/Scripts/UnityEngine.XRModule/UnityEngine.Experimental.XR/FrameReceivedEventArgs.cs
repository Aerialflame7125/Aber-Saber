namespace UnityEngine.Experimental.XR;

public struct FrameReceivedEventArgs
{
	internal XRCameraSubsystem m_CameraSubsystem;

	public XRCameraSubsystem CameraSubsystem => m_CameraSubsystem;
}
