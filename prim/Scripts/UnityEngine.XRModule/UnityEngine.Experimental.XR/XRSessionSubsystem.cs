using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.XR;

[NativeHeader("Modules/XR/Subsystems/Session/XRSessionSubsystem.h")]
[UsedByNativeCode]
[NativeConditional("ENABLE_XR")]
[NativeHeader("Modules/XR/XRPrefix.h")]
public class XRSessionSubsystem : Subsystem<XRSessionSubsystemDescriptor>
{
	[NativeConditional("ENABLE_XR", StubReturnStatement = "kUnityXRTrackingStateUnknown")]
	public extern TrackingState TrackingState
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public extern int LastUpdatedFrame
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		get;
	}

	public event Action<SessionTrackingStateChangedEventArgs> TrackingStateChanged;

	[RequiredByNativeCode]
	private void InvokeTrackingStateChangedEvent(TrackingState newState)
	{
		if (this.TrackingStateChanged != null)
		{
			this.TrackingStateChanged(new SessionTrackingStateChangedEventArgs
			{
				m_Session = this,
				NewState = newState
			});
		}
	}
}
