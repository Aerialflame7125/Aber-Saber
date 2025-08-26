using System.Runtime.InteropServices;

namespace System.Web.Configuration.Internal;

/// <summary>Defines interfaces used by internal .NET Framework infrastructure for accessing and manipulating Host configuration files.</summary>
[ComVisible(false)]
public interface IInternalConfigWebHost
{
	/// <summary>Sets the current site identifier of a configuration object based on the application virtual path and the application configuration path.</summary>
	/// <param name="configPath">A string representing the path to the application's configuration file.</param>
	/// <param name="siteID">The application's site Identifier. For more information, see ASP.NET Configuration Overview.</param>
	/// <param name="vpath">The application's virtual path as a string. For more information, see ASP.NET Web Site Paths.</param>
	void GetSiteIDAndVPathFromConfigPath(string configPath, out string siteID, out string vpath);

	/// <summary>Returns a value representing the path to a configuration file associated with the provided site identifier and application's virtual path.</summary>
	/// <param name="siteID">The application's site identifier. For more information, see ASP.NET Configuration Overview.</param>
	/// <param name="vpath">The application's virtual path as a string. For more information, see ASP.NET Web Site Paths.</param>
	/// <returns>A string representing the path to a configuration file.</returns>
	string GetConfigPathFromSiteIDAndVPath(string siteID, string vpath);
}
