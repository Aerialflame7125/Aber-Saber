using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.XR;

[UsedByNativeCode]
[NativeHeader("Modules/XR/Subsystems/Raycast/XRRaycastSubsystem.h")]
public struct XRRaycastHit
{
	public TrackableId TrackableId { get; set; }

	public Pose Pose { get; set; }

	public float Distance { get; set; }

	public TrackableType HitType { get; set; }
}
