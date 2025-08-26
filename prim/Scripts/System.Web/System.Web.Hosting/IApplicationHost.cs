using System.Security.Permissions;
using System.Web.Configuration;

namespace System.Web.Hosting;

/// <summary>Retrieves information about the application host.</summary>
public interface IApplicationHost
{
	/// <summary>Gets the application's root virtual path.</summary>
	/// <returns>The application's root virtual path.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	string GetVirtualPath();

	/// <summary>Gets the application's root physical path.</summary>
	/// <returns>The physical path of the application root.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	string GetPhysicalPath();

	/// <summary>Enables creation of an <see cref="T:System.Web.Configuration.IConfigMapPath" /> interface in the target application domain.</summary>
	/// <returns>An object that is used to map virtual and physical paths of the configuration file.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	IConfigMapPathFactory GetConfigMapPathFactory();

	/// <summary>Gets the token for the application host configuration (.config) file.</summary>
	/// <returns>A Windows handle that contains the Windows security token for the application's root. The token can be used to open and read the application configuration file.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	IntPtr GetConfigToken();

	/// <summary>Gets the site name.</summary>
	/// <returns>The site name.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	string GetSiteName();

	/// <summary>Gets the site ID.</summary>
	/// <returns>The site ID.</returns>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	string GetSiteID();

	/// <summary>Indicates that a message was received.</summary>
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	void MessageReceived();
}
