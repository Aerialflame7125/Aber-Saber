namespace System.Web.Hosting;

/// <summary>[Supported in the .NET Framework 4.5.1 and later versions] Listens for GL_STOP_LISTENING notifications from IIS.</summary>
public interface IStopListeningRegisteredObject : IRegisteredObject
{
	/// <summary>[Supported in the .NET Framework 4.5.1 and later versions] Stops listening for new requests.</summary>
	void StopListening();
}
