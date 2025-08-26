namespace System.Web.Configuration;

/// <summary>Supplies current context information to configuration-section handlers in ASP.NET applications.</summary>
public class HttpConfigurationContext
{
	private string virtualPath;

	/// <summary>Gets the virtual path to the Web.config configuration file.</summary>
	/// <returns>The virtual path to the Web.config file. <see langword="Null" /> when evaluating Machine.config; an empty string ("") when evaluating the root Web.config file for the site.</returns>
	public string VirtualPath => virtualPath;

	internal HttpConfigurationContext(string virtualPath)
	{
		this.virtualPath = virtualPath;
	}
}
