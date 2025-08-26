using System.Security.Permissions;

namespace System.Web.Hosting;

/// <summary>Manages <see cref="T:System.Web.HttpWorkerRequest" /> objects in the .NET Framework. This class cannot be inherited.</summary>
public sealed class ISAPIRuntime : MarshalByRefObject, IISAPIRuntime, IRegisteredObject
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.ISAPIRuntime" /> class. </summary>
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public ISAPIRuntime()
	{
	}

	/// <summary>Forces garbage collection.</summary>
	public void DoGCCollect()
	{
	}

	/// <summary>Creates a new <see cref="T:System.Web.HttpWorkerRequest" /> object to process the current request.</summary>
	/// <param name="ecb">An ISAPI extension control block.</param>
	/// <param name="iWRType">0 to create an out-of-process request; otherwise, an in-process request is created.</param>
	/// <returns>0 if <see cref="T:System.Web.HttpWorkerRequest" /> was created successfully; otherwise, 1.</returns>
	[MonoTODO("Not implemented")]
	public int ProcessRequest(IntPtr ecb, int iWRType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Starts processing all items in the worker process pipeline.</summary>
	[MonoTODO("Not implemented")]
	public void StartProcessing()
	{
		throw new NotImplementedException();
	}

	/// <summary>Stops processing the items in the worker process pipeline.</summary>
	[MonoTODO("Not implemented")]
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public void StopProcessing()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gives the <see cref="T:System.Web.Hosting.ISAPIRuntime" /> object an infinite lifetime by preventing a lease from being created. </summary>
	/// <returns>
	///     <see langword="null" /> to prevent a lease from being created.</returns>
	[MonoTODO("Not implemented")]
	public override object InitializeLifetimeService()
	{
		throw new NotImplementedException();
	}

	/// <summary>Requests a registered object to unregister.</summary>
	/// <param name="immediate">
	///       <see langword="true" /> to indicate that the registered object should unregister from the hosting environment before returning; otherwise, <see langword="false" />.</param>
	[MonoTODO("Not implemented")]
	void IRegisteredObject.Stop(bool immediate)
	{
		throw new NotImplementedException();
	}
}
