namespace System.Web.Hosting;

/// <summary>Defines methods for objects that are managed by the hosting environment.</summary>
public interface IRegisteredObject
{
	/// <summary>Requests a registered object to unregister.</summary>
	/// <param name="immediate">
	///       <see langword="true" /> to indicate the registered object should unregister from the hosting environment before returning; otherwise, <see langword="false" />.</param>
	void Stop(bool immediate);
}
