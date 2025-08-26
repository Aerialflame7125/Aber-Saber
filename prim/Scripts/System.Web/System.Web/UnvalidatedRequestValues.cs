using System.Collections.Specialized;

namespace System.Web;

/// <summary>Provides access to HTTP request values without triggering ASP.NET request validation.</summary>
public sealed class UnvalidatedRequestValues
{
	/// <summary>Gets the collection of cookies that the client sent, without triggering ASP.NET request validation.</summary>
	/// <returns>The cookies from the HTTP request.</returns>
	public HttpCookieCollection Cookies { get; internal set; }

	/// <summary>Gets the collection of files that the client uploaded, without triggering ASP.NET request validation.</summary>
	/// <returns>The files from the HTTP request.</returns>
	public HttpFileCollection Files { get; internal set; }

	/// <summary>Gets the collection of form variables that the client submitted, without triggering ASP.NET request validation.</summary>
	/// <returns>The form variables from the HTTP request.</returns>
	public NameValueCollection Form { get; internal set; }

	/// <summary>Gets the collection of HTTP headers that the client sent, without triggering request validation.</summary>
	/// <returns>The headers from the HTTP request.</returns>
	public NameValueCollection Headers { get; internal set; }

	/// <summary>Gets the virtual path of the requested resource without triggering ASP.NET request validation.</summary>
	/// <returns>The virtual path.</returns>
	public string Path { get; internal set; }

	/// <summary>Gets additional path information for a resource that has a URL extension, without triggering ASP.NET request validation.</summary>
	/// <returns>A string that contains additional path information for a resource.</returns>
	public string PathInfo { get; internal set; }

	/// <summary>Gets the collection of HTTP query string variables that the client submitted, without triggering ASP.NET request validation.</summary>
	/// <returns>The collection of query string variables sent by the client.</returns>
	public NameValueCollection QueryString { get; internal set; }

	/// <summary>Gets the part of the requested URL that follows the website name, without triggering ASP.NET request validation.</summary>
	/// <returns>The part of the URL that follows the website name. </returns>
	public string RawUrl { get; internal set; }

	/// <summary>Gets the URL data for the request without triggering ASP.NET request validation.</summary>
	/// <returns>An object that contains the URL data for the request. </returns>
	public Uri Url { get; internal set; }

	/// <summary>Gets the specified object from the <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.QueryString" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection, without triggering ASP.NET request validation.</summary>
	/// <param name="field">The key of the object to retrieve.</param>
	/// <returns>The requested object, or <see langword="null" /> if the object is not found.</returns>
	public string this[string field]
	{
		get
		{
			if (Form != null && Form[field] != null)
			{
				return Form[field];
			}
			if (Cookies != null && Cookies[field] != null)
			{
				return Cookies[field].Value;
			}
			if (QueryString != null && QueryString[field] != null)
			{
				return QueryString[field];
			}
			return null;
		}
	}
}
