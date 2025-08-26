using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.XR;

[NativeType(Header = "Modules/XR/Subsystems/ReferencePoints/XRReferencePointSubsystemDescriptor.h")]
[NativeHeader("Modules/XR/XRPrefix.h")]
[UsedByNativeCode]
public class XRReferencePointSubsystemDescriptor : SubsystemDescriptor<XRReferencePointSubsystem>
{
}
