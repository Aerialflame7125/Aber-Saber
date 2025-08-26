namespace System.Web.Util;

/// <summary>Specifies what kind of HTTP request data to validate.</summary>
public enum RequestValidationSource
{
	/// <summary>The query string.</summary>
	QueryString,
	/// <summary>The form values.</summary>
	Form,
	/// <summary>The request cookies.</summary>
	Cookies,
	/// <summary>The uploaded file.</summary>
	Files,
	/// <summary>The raw URL. (The part of a URL after the domain.)</summary>
	RawUrl,
	/// <summary>The virtual path.</summary>
	Path,
	/// <summary>An HTTP <see cref="P:System.Web.HttpRequest.PathInfo" /> string, which is an extension to a URL path. </summary>
	PathInfo,
	/// <summary>The request headers.</summary>
	Headers
}
