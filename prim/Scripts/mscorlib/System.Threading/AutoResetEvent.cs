using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Threading;

/// <summary>Notifies a waiting thread that an event has occurred. This class cannot be inherited.</summary>
[ComVisible(true)]
[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
public sealed class AutoResetEvent : EventWaitHandle
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AutoResetEvent" /> class with a Boolean value indicating whether to set the initial state to signaled.</summary>
	/// <param name="initialState">
	///   <see langword="true" /> to set the initial state to signaled; <see langword="false" /> to set the initial state to non-signaled.</param>
	public AutoResetEvent(bool initialState)
		: base(initialState, EventResetMode.AutoReset)
	{
	}
}
