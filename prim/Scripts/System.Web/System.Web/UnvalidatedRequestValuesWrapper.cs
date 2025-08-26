using System.Collections.Specialized;

namespace System.Web;

/// <summary>Provides a wrapper class for the <see cref="T:System.Web.UnvalidatedRequestValuesBase" /> class, and provides access to HTTP request values without triggering ASP.NET request validation.</summary>
public class UnvalidatedRequestValuesWrapper : UnvalidatedRequestValuesBase
{
	private readonly UnvalidatedRequestValues _requestValues;

	/// <summary>Gets the collection of form variables that the client submitted, without triggering ASP.NET request validation.</summary>
	/// <returns>The form variables from the HTTP request.</returns>
	public override NameValueCollection Form => _requestValues.Form;

	/// <summary>Gets the collection of HTTP query string variables that the client submitted, without triggering ASP.NET request validation.</summary>
	/// <returns>The collection of query string variables sent by the client.</returns>
	public override NameValueCollection QueryString => _requestValues.QueryString;

	/// <summary>Gets the collection of HTTP headers that the client sent, without triggering ASP.NET request validation.</summary>
	/// <returns>The headers from the HTTP request.</returns>
	public override NameValueCollection Headers => _requestValues.Headers;

	/// <summary>Gets the collection of cookies that the client sent, without triggering ASP.NET request validation.</summary>
	/// <returns>The cookies from the HTTP request.</returns>
	public override HttpCookieCollection Cookies => _requestValues.Cookies;

	/// <summary>Gets the collection of files that the client uploaded, without triggering ASP.NET request validation.</summary>
	/// <returns>The files from the HTTP request.</returns>
	public override HttpFileCollectionBase Files => new HttpFileCollectionWrapper(_requestValues.Files);

	/// <summary>Gets the part of the requested URL that follows the website name, without triggering ASP.NET request validation.</summary>
	/// <returns>The part of the URL that follows the website name.</returns>
	public override string RawUrl => _requestValues.RawUrl;

	/// <summary>Gets the virtual path of the requested resource without triggering ASP.NET request validation.</summary>
	/// <returns>The virtual path.</returns>
	public override string Path => _requestValues.Path;

	/// <summary>Gets additional path information for a resource that has a URL extension, without triggering ASP.NET request validation.</summary>
	/// <returns>A string that contains additional path information for a resource.</returns>
	public override string PathInfo => _requestValues.PathInfo;

	/// <summary>Gets the specified object from the <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.QueryString" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection, without triggering ASP.NET request validation.</summary>
	/// <param name="field">The key of the object to retrieve.</param>
	/// <returns>The requested object, or <see langword="null" /> if the object is not found.</returns>
	public override string this[string field] => _requestValues[field];

	/// <summary>Gets the URL data for the request without triggering ASP.NET request validation.</summary>
	/// <returns>An object that contains the URL data for the request.</returns>
	public override Uri Url => _requestValues.Url;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UnvalidatedRequestValuesWrapper" /> class.</summary>
	/// <param name="requestValues">The object that is passed to the constructor to initialize the class.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="requestValues" /> parameter is <see langword="null" />.</exception>
	public UnvalidatedRequestValuesWrapper(UnvalidatedRequestValues requestValues)
	{
		if (requestValues == null)
		{
			throw new ArgumentNullException("requestValues");
		}
		_requestValues = requestValues;
	}
}
