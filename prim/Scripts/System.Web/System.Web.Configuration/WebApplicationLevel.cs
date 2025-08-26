namespace System.Web.Configuration;

/// <summary>Specifies the path level of a Web-application configuration file.</summary>
public enum WebApplicationLevel
{
	/// <summary>Specifies that the configuration file is in a global directory in relation to the current ASP.NET Web application.</summary>
	AboveApplication = 10,
	/// <summary>Specifies that the configuration file is in the root directory of the current ASP.NET Web application.</summary>
	AtApplication = 20,
	/// <summary>Specifies that the configuration file is in a sub-directory of the current ASP.NET Web application.</summary>
	BelowApplication = 30
}
