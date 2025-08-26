using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.XR;

[UsedByNativeCode]
[NativeHeader("Modules/XR/XRPrefix.h")]
[NativeType(Header = "Modules/XR/Subsystems/Camera/XRCameraSubsystemDescriptor.h")]
[NativeConditional("ENABLE_XR")]
public class XRCameraSubsystemDescriptor : SubsystemDescriptor<XRCameraSubsystem>
{
	public extern bool ProvidesAverageBrightness
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern bool ProvidesAverageColorTemperature
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern bool ProvidesProjectionMatrix
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern bool ProvidesDisplayMatrix
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern bool ProvidesTimestamp
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}
}
