namespace System.Web.Configuration;

/// <summary>Provides access to the mapping between configuration-file virtual and physical paths. </summary>
public interface IConfigMapPath
{
	/// <summary>Gets the machine-configuration file name.</summary>
	/// <returns>The machine-configuration file name.</returns>
	string GetMachineConfigFilename();

	/// <summary>Gets the name of the configuration file at the Web root.</summary>
	/// <returns>The name of the configuration file at the Web root.</returns>
	string GetRootWebConfigFilename();

	/// <summary>Populates the directory and name of the configuration file based on the site ID and site path.</summary>
	/// <param name="siteID">A unique identifier for the site.</param>
	/// <param name="path">The URL associated with the site.</param>
	/// <param name="directory">The physical directory of the configuration path.</param>
	/// <param name="baseName">The name of the configuration file.</param>
	void GetPathConfigFilename(string siteID, string path, out string directory, out string baseName);

	/// <summary>Populates the default site name and the site ID.</summary>
	/// <param name="siteName">The default site name.</param>
	/// <param name="siteID">A unique identifier for the site.</param>
	void GetDefaultSiteNameAndID(out string siteName, out string siteID);

	/// <summary>Populates the site name and site ID based on a site argument value.</summary>
	/// <param name="siteArgument">The site name or site identifier.</param>
	/// <param name="siteName">The default site name.</param>
	/// <param name="siteID">A unique identifier for the site.</param>
	void ResolveSiteArgument(string siteArgument, out string siteName, out string siteID);

	/// <summary>Gets the physical directory path based on the site ID and URL associated with the site.</summary>
	/// <param name="siteID">A unique identifier for the site.</param>
	/// <param name="path">The URL associated with the site.</param>
	/// <returns>The physical directory path.</returns>
	string MapPath(string siteID, string path);

	/// <summary>Gets the virtual-directory name associated with a specific site.</summary>
	/// <param name="siteID">A unique identifier for the site.</param>
	/// <param name="path">The URL associated with the site.</param>
	/// <returns>The <paramref name="siteID" /> must be unique. No two sites share the same id. The <paramref name="siteID" /> distinguishes sites that have the same name.</returns>
	string GetAppPathForPath(string siteID, string path);
}
