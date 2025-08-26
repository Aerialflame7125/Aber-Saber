namespace System.Web.Configuration;

/// <summary>Specifies values for how the URL of the original request is handled when a custom error page is displayed.</summary>
public enum CustomErrorsRedirectMode
{
	/// <summary>Display the error page and change the URL of the original request.</summary>
	ResponseRedirect,
	/// <summary>Display the error page without changing the original URL.</summary>
	ResponseRewrite
}
