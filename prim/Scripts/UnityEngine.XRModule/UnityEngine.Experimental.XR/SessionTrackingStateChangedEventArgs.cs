namespace UnityEngine.Experimental.XR;

public struct SessionTrackingStateChangedEventArgs
{
	internal XRSessionSubsystem m_Session;

	public XRSessionSubsystem SessionSubsystem => m_Session;

	public TrackingState NewState { get; set; }
}
