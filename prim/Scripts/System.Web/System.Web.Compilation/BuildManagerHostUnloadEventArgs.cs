namespace System.Web.Compilation;

/// <summary>Contains event data for the <see cref="E:System.Web.Compilation.ClientBuildManager.AppDomainShutdown" /> event and the <see cref="E:System.Web.Compilation.ClientBuildManager.AppDomainUnloaded" /> event. </summary>
public class BuildManagerHostUnloadEventArgs : EventArgs
{
	private ApplicationShutdownReason reason;

	/// <summary>Gets the reason the hosted application domain was shut down.</summary>
	/// <returns>One of the <see cref="T:System.Web.ApplicationShutdownReason" /> enumerated values.</returns>
	public ApplicationShutdownReason Reason => reason;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.BuildManagerHostUnloadEventArgs" /> class. </summary>
	/// <param name="reason">The reason for the hosted application domain shutdown.</param>
	public BuildManagerHostUnloadEventArgs(ApplicationShutdownReason reason)
	{
		this.reason = reason;
	}
}
