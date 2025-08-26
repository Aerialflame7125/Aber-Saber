using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.XR;

[NativeType(Header = "Modules/XR/Subsystems/Depth/XRDepthSubsystemDescriptor.h")]
[NativeHeader("Modules/XR/XRPrefix.h")]
[NativeConditional("ENABLE_XR")]
[UsedByNativeCode]
public class XRDepthSubsystemDescriptor : SubsystemDescriptor<XRDepthSubsystem>
{
	public extern bool SupportsFeaturePoints
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}
}
