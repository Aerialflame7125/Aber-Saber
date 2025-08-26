namespace System.Web.Hosting;

/// <summary>Creates and stops application domains for a Web-application manager. This class cannot be inherited.</summary>
public sealed class AppManagerAppDomainFactory : IAppManagerAppDomainFactory
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.AppManagerAppDomainFactory" /> class.</summary>
	public AppManagerAppDomainFactory()
	{
	}

	/// <summary>Creates a new application domain for the specified Web application.</summary>
	/// <param name="appId">The unique identifier for the new Web application.</param>
	/// <param name="appPath">The path to the new Web application's files.</param>
	/// <returns>A new application domain for the specified Web application.</returns>
	[MonoTODO("Not implemented")]
	public object Create(string appId, string appPath)
	{
		throw new NotImplementedException();
	}

	/// <summary>Stops all application domains associated with this application manager. </summary>
	public void Stop()
	{
	}
}
