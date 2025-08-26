using System.Runtime.CompilerServices;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that contains methods for setting cache-specific HTTP headers and for controlling the ASP.NET page output cache.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class HttpCachePolicyWrapper : HttpCachePolicyBase
{
	private HttpCachePolicy _httpCachePolicy;

	/// <summary>Gets the list of <see langword="Content-Encoding" /> headers that will be used to vary the output cache.</summary>
	/// <returns>An object that specifies which <see langword="Content-Encoding" /> headers are used to select the cached response.</returns>
	public override HttpCacheVaryByContentEncodings VaryByContentEncodings => _httpCachePolicy.VaryByContentEncodings;

	/// <summary>Gets the list of all HTTP headers that will be used to vary cache output.</summary>
	/// <returns>An object that specifies which HTTP headers are used to select the cached response.</returns>
	public override HttpCacheVaryByHeaders VaryByHeaders => _httpCachePolicy.VaryByHeaders;

	/// <summary>Gets the list of parameters received by an HTTP <see langword="GET" /> or HTTP <see langword="POST" /> that affect caching.</summary>
	/// <returns>An object that specifies which cache-control parameters are used to select the cached response.</returns>
	public override HttpCacheVaryByParams VaryByParams => _httpCachePolicy.VaryByParams;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCachePolicyWrapper" /> class. </summary>
	/// <param name="httpCachePolicy">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="httpCachePolicy" /> is <see langword="null" />.</exception>
	public HttpCachePolicyWrapper(HttpCachePolicy httpCachePolicy)
	{
		if (httpCachePolicy == null)
		{
			throw new ArgumentNullException("httpCachePolicy");
		}
		_httpCachePolicy = httpCachePolicy;
	}

	/// <summary>Registers a validation callback for the current response.</summary>
	/// <param name="handler">The object that will handle the request.</param>
	/// <param name="data">The user-supplied data that is passed to the <see cref="M:System.Web.HttpCachePolicyWrapper.AddValidationCallback(System.Web.HttpCacheValidateHandler,System.Object)" /> delegate.</param>
	/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="handler" /> is <see langword="null" />. </exception>
	public override void AddValidationCallback(HttpCacheValidateHandler handler, object data)
	{
		_httpCachePolicy.AddValidationCallback(handler, data);
	}

	/// <summary>Appends the specified text to the <see langword="Cache-Control" /> HTTP header.</summary>
	/// <param name="extension">The text to append to the <see langword="Cache-Control" /> header.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="extension" /> is <see langword="null" />. </exception>
	public override void AppendCacheExtension(string extension)
	{
		_httpCachePolicy.AppendCacheExtension(extension);
	}

	/// <summary>Makes the response available in the browser history cache, regardless of the <see cref="T:System.Web.HttpCacheability" /> setting made on the server.</summary>
	/// <param name="allow">
	///       <see langword="true" /> to direct the client browser to store responses in the browser history cache; otherwise <see langword="false" />. The default is <see langword="false" />.</param>
	public override void SetAllowResponseInBrowserHistory(bool allow)
	{
		_httpCachePolicy.SetAllowResponseInBrowserHistory(allow);
	}

	/// <summary>Sets the <see langword="Cache-Control" /> header to the specified <see cref="T:System.Web.HttpCacheability" /> value.</summary>
	/// <param name="cacheability">The <see cref="T:System.Web.HttpCacheability" /> enumeration value to set the header to.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="cacheability" /> is not one of the enumeration values. </exception>
	public override void SetCacheability(HttpCacheability cacheability)
	{
		_httpCachePolicy.SetCacheability(cacheability);
	}

	/// <summary>Sets the <see langword="Cache-Control" /> header to the specified <see cref="T:System.Web.HttpCacheability" /> value and appends an extension to the directive.</summary>
	/// <param name="cacheability">The <see cref="T:System.Web.HttpCacheability" /> enumeration value to set the header to.</param>
	/// <param name="field">The cache-control extension to add to the header.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="field" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="cacheability" /> is not <see cref="F:System.Web.HttpCacheability.Private" /> or <see cref="F:System.Web.HttpCacheability.NoCache" />. </exception>
	public override void SetCacheability(HttpCacheability cacheability, string field)
	{
		_httpCachePolicy.SetCacheability(cacheability, field);
	}

	/// <summary>Sets the <see langword="ETag" /> HTTP header to the specified string.</summary>
	/// <param name="etag">The text to use for the <see langword="ETag" /> header.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="etag" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <see langword="ETag" /> header has already been set. - or -The <see cref="M:System.Web.HttpCachePolicy.SetETagFromFileDependencies" /> method has already been called.</exception>
	public override void SetETag(string etag)
	{
		_httpCachePolicy.SetETag(etag);
	}

	/// <summary>Sets the <see langword="ETag" /> HTTP header based on the time stamps of the handler's file dependencies.</summary>
	/// <exception cref="T:System.InvalidOperationException">The <see langword="ETag" /> header has already been set. </exception>
	public override void SetETagFromFileDependencies()
	{
		_httpCachePolicy.SetETagFromFileDependencies();
	}

	/// <summary>Sets the <see langword="Expires" /> HTTP header to an absolute date and time.</summary>
	/// <param name="date">The absolute expiration time.</param>
	public override void SetExpires(DateTime date)
	{
		_httpCachePolicy.SetExpires(date);
	}

	/// <summary>Sets the <see langword="Last-Modified" /> HTTP header to the specified date and time.</summary>
	/// <param name="date">The date-time value to set the <see langword="Last-Modified" /> header to.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="date" /> is later than the current <see langword="DateTime" />. </exception>
	public override void SetLastModified(DateTime date)
	{
		_httpCachePolicy.SetLastModified(date);
	}

	/// <summary>Sets the <see langword="Last-Modified" /> HTTP header based on the time stamps of the handler's file dependencies.</summary>
	public override void SetLastModifiedFromFileDependencies()
	{
		_httpCachePolicy.SetLastModifiedFromFileDependencies();
	}

	/// <summary>Sets the <see langword="Cache-Control: max-age" /> HTTP header to the specified time span.</summary>
	/// <param name="delta">The time span to set the <see langword="Cache" />-<see langword="Control: max-age" /> header to.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="delta" /> is less than 0 or greater than one year. </exception>
	public override void SetMaxAge(TimeSpan delta)
	{
		_httpCachePolicy.SetMaxAge(delta);
	}

	/// <summary>Stops all origin-server caching for the current response.</summary>
	public override void SetNoServerCaching()
	{
		_httpCachePolicy.SetNoServerCaching();
	}

	/// <summary>Sets the <see langword="Cache-Control: no-store" /> HTTP header.</summary>
	public override void SetNoStore()
	{
		_httpCachePolicy.SetNoStore();
	}

	/// <summary>Sets the <see langword="Cache-Control: no-transform" /> HTTP header.</summary>
	public override void SetNoTransforms()
	{
		_httpCachePolicy.SetNoTransforms();
	}

	/// <summary>Specifies whether the response contains the <see langword="vary:*" /> header when varying by parameters.</summary>
	/// <param name="omit">
	///       <see langword="true" /> to direct the <see cref="T:System.Web.HttpCachePolicy" /> object to not use the * value for its <see cref="P:System.Web.HttpCachePolicy.VaryByHeaders" /> property; otherwise, <see langword="false" />.</param>
	public override void SetOmitVaryStar(bool omit)
	{
		_httpCachePolicy.SetOmitVaryStar(omit);
	}

	/// <summary>Sets the <see langword="Cache-Control: s-maxage" /> HTTP header to the specified time span.</summary>
	/// <param name="delta">The time span to set the <see langword="Cache-Control: s-maxage" /> header to.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="delta" /> is less than 0. </exception>
	public override void SetProxyMaxAge(TimeSpan delta)
	{
		_httpCachePolicy.SetProxyMaxAge(delta);
	}

	/// <summary>Sets the <see langword="Cache-Control" /> HTTP header to either the <see langword="must-revalidate" /> or the <see langword="proxy-revalidate" /> directives, based on the specified enumeration value.</summary>
	/// <param name="revalidation">The <see cref="T:System.Web.HttpCacheRevalidation" /> enumeration value to set the <see langword="Cache-Control" /> header to.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="revalidation" /> is not one of the enumeration values. </exception>
	public override void SetRevalidation(HttpCacheRevalidation revalidation)
	{
		_httpCachePolicy.SetRevalidation(revalidation);
	}

	/// <summary>Sets cache expiration to absolute or sliding.</summary>
	/// <param name="slide">
	///       <see langword="true" /> to set a sliding cache expiration, and false to set an absolute cache expiration.</param>
	public override void SetSlidingExpiration(bool slide)
	{
		_httpCachePolicy.SetSlidingExpiration(slide);
	}

	/// <summary>Specifies whether the ASP.NET cache should ignore HTTP <see langword="Cache-Control" /> headers sent by the client that invalidate the cache.</summary>
	/// <param name="validUntilExpires">
	///       <see langword="true" /> to specify that ASP.NET should ignore <see langword="Cache-Control" /> invalidation headers; otherwise, <see langword="false" />.</param>
	public override void SetValidUntilExpires(bool validUntilExpires)
	{
		_httpCachePolicy.SetValidUntilExpires(validUntilExpires);
	}

	/// <summary>Specifies a text string to vary cached output responses by.</summary>
	/// <param name="custom">The text string to vary cached output by.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="custom" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.HttpCachePolicy.SetVaryByCustom(System.String)" /> method has already been called. </exception>
	public override void SetVaryByCustom(string custom)
	{
		_httpCachePolicy.SetVaryByCustom(custom);
	}
}
