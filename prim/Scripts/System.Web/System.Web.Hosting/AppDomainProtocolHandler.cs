using System.Security.Permissions;

namespace System.Web.Hosting;

/// <summary>Provides support for programmatic access to application domain protocols.</summary>
public abstract class AppDomainProtocolHandler : MarshalByRefObject, IRegisteredObject
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.AppDomainProtocolHandler" /> class.</summary>
	protected AppDomainProtocolHandler()
	{
		HostingEnvironment.RegisterObject(this);
	}

	/// <summary>Gives the protocol handler an infinite lifetime by preventing a lease from being created.</summary>
	/// <returns>
	///     <see langword="true" /> if the service is initiated; otherwise, <see langword="false" />.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public override object InitializeLifetimeService()
	{
		return null;
	}

	/// <summary>Starts a protocol listener channel.</summary>
	/// <param name="listenerChannelCallback">The callback for the listener channel.</param>
	public abstract void StartListenerChannel(IListenerChannelCallback listenerChannelCallback);

	/// <summary>Stops the specified process protocol handler.</summary>
	/// <param name="listenerChannelId">The callback for the listener channel.</param>
	/// <param name="immediate">
	///       <see langword="true" /> to stop the protocol immediately; otherwise, <see langword="false" />.</param>
	public abstract void StopListenerChannel(int listenerChannelId, bool immediate);

	/// <summary>Stops a protocol.</summary>
	/// <param name="immediate">
	///       <see langword="true" /> to stop the protocol immediately.</param>
	public abstract void StopProtocol(bool immediate);

	/// <summary>Stops a queue.</summary>
	/// <param name="immediate">
	///       <see langword="true" /> to stop the queue immediately.</param>
	public virtual void Stop(bool immediate)
	{
		StopProtocol(immediate: true);
		HostingEnvironment.UnregisterObject(this);
	}
}
