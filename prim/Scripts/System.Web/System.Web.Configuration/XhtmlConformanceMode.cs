namespace System.Web.Configuration;

/// <summary>Specifies the authentication mode to use in a Web application.</summary>
public enum XhtmlConformanceMode
{
	/// <summary>Reverts a number of rendering changes made for conformance to the v1.1 rendering behavior. </summary>
	Transitional,
	/// <summary>XHTML 1.0 Transitional </summary>
	Legacy,
	/// <summary>XHTML 1.0 Strict conformance </summary>
	Strict
}
