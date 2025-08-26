namespace System.Web.Configuration;

/// <summary>Manages the path context for the current Web application. This class cannot be inherited.</summary>
public sealed class WebContext
{
	private WebApplicationLevel pathLevel;

	private string site;

	private string applicationPath;

	private string path;

	private string locationSubPath;

	/// <summary>Gets a <see cref="T:System.Web.Configuration.WebApplicationLevel" /> object that represents the path level of the current Web application.</summary>
	/// <returns>A <see cref="T:System.Web.Configuration.WebApplicationLevel" /> object.</returns>
	public WebApplicationLevel ApplicationLevel => pathLevel;

	/// <summary>Gets the application path of the current Web application.</summary>
	/// <returns>The application path of the current Web application.</returns>
	public string ApplicationPath => applicationPath;

	/// <summary>Gets the location subpath of the Web application.</summary>
	/// <returns>The location subpath of the current Web application.</returns>
	public string LocationSubPath => locationSubPath;

	/// <summary>Gets the current virtual path of the Web application.</summary>
	/// <returns>The current virtual path of the Web application.</returns>
	public string Path => path;

	/// <summary>Gets the name of the current Web application.</summary>
	/// <returns>The name of the current Web application.</returns>
	public string Site => site;

	public WebContext(WebApplicationLevel pathLevel, string site, string applicationPath, string path, string locationSubPath)
	{
		this.pathLevel = pathLevel;
		this.site = site;
		this.applicationPath = applicationPath;
		this.path = path;
		this.locationSubPath = locationSubPath;
	}
}
