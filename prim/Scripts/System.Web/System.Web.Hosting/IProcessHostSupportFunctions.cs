using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Web.Hosting;

/// <summary>Provides helper functions for the process host.</summary>
[ComImport]
[Guid("35f9c4c1-3800-4d17-99bc-018a62243687")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
[SuppressUnmanagedCodeSecurity]
public interface IProcessHostSupportFunctions
{
	/// <summary>Gets the properties from the application's metabase.</summary>
	/// <param name="appId">The ID of the application.</param>
	/// <param name="virtualPath">The root virtual path of the application.</param>
	/// <param name="physicalPath">The root physical path of the application.</param>
	/// <param name="siteName">The display name of the application.</param>
	/// <param name="siteId">The site ID.</param>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	void GetApplicationProperties([In][MarshalAs(UnmanagedType.LPWStr)] string appId, out string virtualPath, out string physicalPath, out string siteName, out string siteId);

	/// <summary>Gets the physical path of a relative URL.</summary>
	/// <param name="appId">The application ID.</param>
	/// <param name="virtualPath">The relative URL to map.</param>
	/// <param name="physicalPath">The physical path of the relative URL.</param>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	void MapPath([In][MarshalAs(UnmanagedType.LPWStr)] string appId, [In][MarshalAs(UnmanagedType.LPWStr)] string virtualPath, out string physicalPath);

	/// <summary>Gets a Windows security token for the specified application's root directory.</summary>
	/// <param name="appId">The unique identifier of the application.</param>
	/// <returns>A Windows handle that contains a Windows security token for the specified application's root directory.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[return: MarshalAs(UnmanagedType.SysInt)]
	IntPtr GetConfigToken([In][MarshalAs(UnmanagedType.LPWStr)] string appId);

	/// <summary>Gets the application host configuration (.config) file path.</summary>
	/// <returns>The physical path (including the file name) to the application host configuration file.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[return: MarshalAs(UnmanagedType.BStr)]
	string GetAppHostConfigFilename();

	/// <summary>Gets the physical path for the ApplicationHost.config file.</summary>
	/// <returns>The physical path for the ApplicationHost.config file.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[return: MarshalAs(UnmanagedType.BStr)]
	string GetRootWebConfigFilename();

	/// <summary>Retrieves the INativeConfigurationSystem interface.</summary>
	/// <returns>A pointer to the INativeConfigurationSystem interface.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[return: MarshalAs(UnmanagedType.SysInt)]
	IntPtr GetNativeConfigurationSystem();
}
