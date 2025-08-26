using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.XR;

[NativeHeader("Modules/XR/XRPrefix.h")]
[NativeType(Header = "Modules/XR/Subsystems/Planes/XRPlaneSubsystemDescriptor.h")]
[UsedByNativeCode]
public class XRPlaneSubsystemDescriptor : SubsystemDescriptor<XRPlaneSubsystem>
{
}
