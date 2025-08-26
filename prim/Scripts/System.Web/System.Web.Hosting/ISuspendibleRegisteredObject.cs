namespace System.Web.Hosting;

/// <summary>[Supported in the .NET Framework 4.5.1 and later versions] Listens for suspend and resume notifications.</summary>
public interface ISuspendibleRegisteredObject : IRegisteredObject
{
	/// <summary>[Supported in the .NET Framework 4.5.1 and later versions] Called when ASP.NET notifies an application that a process is being suspended.</summary>
	Action Suspend();
}
