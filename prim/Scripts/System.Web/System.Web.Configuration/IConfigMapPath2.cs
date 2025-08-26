namespace System.Web.Configuration;

internal interface IConfigMapPath2
{
	void GetPathConfigFilename(string siteID, VirtualPath path, out string directory, out string baseName);

	string MapPath(string siteID, VirtualPath path);

	VirtualPath GetAppPathForPath(string siteID, VirtualPath path);
}
