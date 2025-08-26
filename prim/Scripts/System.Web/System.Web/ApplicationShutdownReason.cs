namespace System.Web;

/// <summary>Specifies why the <see cref="T:System.AppDomain" /> class shut down.</summary>
public enum ApplicationShutdownReason
{
	/// <summary>No shutdown reason was provided. </summary>
	None,
	/// <summary>The hosting environment shut down the application domain.</summary>
	HostingEnvironment,
	/// <summary>A change was made to the Global.asax file. </summary>
	ChangeInGlobalAsax,
	/// <summary>A change was made to the application-level configuration file.</summary>
	ConfigurationChange,
	/// <summary>A call was made to <see cref="M:System.Web.HttpRuntime.UnloadAppDomain" />. </summary>
	UnloadAppDomainCalled,
	/// <summary>A change was made in the code access security policy file. </summary>
	ChangeInSecurityPolicyFile,
	/// <summary>A change was made to the Bin folder or to files in it. </summary>
	BinDirChangeOrDirectoryRename,
	/// <summary>A change was made to the App_Browsers folder or to files in it. </summary>
	BrowsersDirChangeOrDirectoryRename,
	/// <summary>A change was made to the App_Code folder or to files in it. </summary>
	CodeDirChangeOrDirectoryRename,
	/// <summary>A change was made to the App_GlobalResources folder or to files in it. </summary>
	ResourcesDirChangeOrDirectoryRename,
	/// <summary>The maximum idle time limit was reached. </summary>
	IdleTimeout,
	/// <summary>A change was made to the physical path of the application. </summary>
	PhysicalApplicationPathChanged,
	/// <summary>A call was made to <see cref="M:System.Web.HttpRuntime.Close" />. </summary>
	HttpRuntimeClose,
	/// <summary>An <see cref="T:System.AppDomain" /> initialization error occurred. </summary>
	InitializationError,
	/// <summary>The maximum number of dynamic recompiles of resources was reached.</summary>
	MaxRecompilationsReached,
	/// <summary>The compilation system shut the application domain. The <see cref="F:System.Web.ApplicationShutdownReason.BuildManagerChange" /> member is introduced in the .NET Framework version 3.5.  For more information, see .NET Framework Versions and Dependencies.</summary>
	BuildManagerChange
}
