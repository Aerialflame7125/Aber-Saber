using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental;

[StructLayout(LayoutKind.Sequential)]
[UsedByNativeCode]
[NativeType(Header = "Modules/XR/XRSubsystem.h")]
public class Subsystem
{
	internal IntPtr m_Ptr;

	internal ISubsystemDescriptor m_subsystemDescriptor;

	[MethodImpl(MethodImplOptions.InternalCall)]
	internal extern void SetHandle(Subsystem inst);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void Start();

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void Stop();

	public void Destroy()
	{
		IntPtr ptr = m_Ptr;
		Internal_SubsystemInstances.Internal_RemoveInstanceByPtr(m_Ptr);
		SubsystemManager.DestroyInstance_Internal(ptr);
	}
}
[UsedByNativeCode("XRSubsystem_TXRSubsystemDescriptor")]
public class Subsystem<TSubsystemDescriptor> : Subsystem where TSubsystemDescriptor : ISubsystemDescriptor
{
	public TSubsystemDescriptor SubsystemDescriptor => (TSubsystemDescriptor)m_subsystemDescriptor;
}
