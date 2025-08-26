using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Routing;

namespace System.Web;

/// <summary>Represents the base class for classes that provides HTTP-response information from an ASP.NET operation.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public abstract class HttpResponseBase
{
	/// <summary>Gets or sets a value that indicates whether to buffer output and send it after the complete response has finished processing.</summary>
	/// <returns>
	///     <see langword="true" /> if the output is buffered; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool Buffer
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value that indicates whether to buffer output and send it after the complete page has finished processing.</summary>
	/// <returns>
	///     <see langword="true" /> if the output is buffered; otherwise <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool BufferOutput
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the caching policy (such as expiration time, privacy settings, and vary clauses) of the current Web page.</summary>
	/// <returns>The caching policy of the current response.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpCachePolicyBase Cache
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the <see langword="Cache-Control" /> HTTP header that matches one of the <see cref="T:System.Web.HttpCacheability" /> enumeration values.</summary>
	/// <returns>The caching policy of the current response.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string CacheControl
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the HTTP character set of the current response.</summary>
	/// <returns>The HTTP character set of the current response.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string Charset
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a <see cref="T:System.Threading.CancellationToken" /> object that is tripped when the client disconnects.</summary>
	/// <returns>The <see cref="T:System.Threading.CancellationToken" /> object that is tripped when the client disconnects.</returns>
	public virtual CancellationToken ClientDisconnectedToken
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the content encoding of the current response.</summary>
	/// <returns>The content encoding of the current response.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Encoding ContentEncoding
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the HTTP MIME type of the current response.</summary>
	/// <returns>The HTTP MIME type of the current response.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string ContentType
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the response cookie collection.</summary>
	/// <returns>The response cookie collection.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpCookieCollection Cookies
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the number of minutes before a page that is cached on the client or proxy expires. If the user returns to the same page before it expires, the cached version is displayed. <see cref="P:System.Web.HttpResponseBase.Expires" /> is provided for compatibility with earlier versions of Active Server Pages (ASP).</summary>
	/// <returns>The number of minutes before the page expires.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int Expires
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the absolute date and time at which cached information expires in the cache. <see cref="P:System.Web.HttpResponseBase.ExpiresAbsolute" /> is provided for compatibility with earlier versions of Active Server Pages (ASP).</summary>
	/// <returns>The date and time at which the page expires.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual DateTime ExpiresAbsolute
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a filter object that is used to modify the HTTP entity body before transmission.</summary>
	/// <returns>An object that acts as the output filter.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Stream Filter
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the collection of response headers.</summary>
	/// <returns>The response headers.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual NameValueCollection Headers
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>[Supported in the .NET Framework 4.5.2 and later versions]Gets a value indicating whether the response headers have been written.</summary>
	/// <returns>
	///     <see langword="true" /> if the response headers have been written; otherwise, <see langword="false" />.</returns>
	public virtual bool HeadersWritten
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the encoding for the header of the current response.</summary>
	/// <returns>The encoding for the current header.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Encoding HeaderEncoding
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the client is connected to the server.</summary>
	/// <returns>
	///     <see langword="true" /> if the client is currently connected; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsClientConnected
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the client is being redirected to a new location.</summary>
	/// <returns>
	///     <see langword="true" /> if the value of the location response header differs from the current location; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsRequestBeingRedirected
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the object that enables text output to the HTTP response stream.</summary>
	/// <returns>An object that enables output to the client.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual TextWriter Output
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the object that enables binary output to the outgoing HTTP content body.</summary>
	/// <returns>An object that represents the raw contents of the outgoing HTTP content body.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual Stream OutputStream
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the value of the HTTP <see langword="Location" /> header.</summary>
	/// <returns>The absolute URL of the HTTP <see langword="Location" /> header.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string RedirectLocation
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the <see langword="Status" /> value that is returned to the client.</summary>
	/// <returns>The status of the HTTP output. For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string Status
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the HTTP status code of the output that is returned to the client.</summary>
	/// <returns>The status code of the HTTP output that is returned to the client. For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int StatusCode
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the HTTP status message of the output that is returned to the client.</summary>
	/// <returns>The status message of the HTTP output that is returned to the client. For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string StatusDescription
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value that qualifies the status code of the response.</summary>
	/// <returns>The IIS 7.0 substatus code.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int SubStatusCode
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the connection supports asynchronous flush operation.</summary>
	/// <returns>
	///     <see langword="true" /> if the connection supports asynchronous flush operations; otherwise, <see langword="false" />.</returns>
	public virtual bool SupportsAsyncFlush
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value that indicates whether to send HTTP content to the client.</summary>
	/// <returns>
	///     <see langword="true" /> if output is suppressed; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool SuppressContent
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>[Supported in the .NET Framework 4.5.2 and later versions]Gets or sets a value indicating whether to suppress the default Cache Control: private header for the current HTTP response.</summary>
	/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
	public virtual bool SuppressDefaultCacheControlHeader
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value that specifies whether forms authentication redirection to the login page should be suppressed.</summary>
	/// <returns>
	///     <see langword="true" /> if forms authentication redirection should be suppressed; otherwise, <see langword="false" />.</returns>
	public virtual bool SuppressFormsAuthenticationRedirect
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value that specifies whether IIS 7.0 custom errors are disabled.</summary>
	/// <returns>
	///     <see langword="true" /> if IIS custom errors are disabled; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool TrySkipIisCustomErrors
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Makes the validity of a cached response dependent on the specified item in the cache.</summary>
	/// <param name="cacheKey">The key of the item that the cached response is dependent on.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AddCacheItemDependency(string cacheKey)
	{
		throw new NotImplementedException();
	}

	/// <summary>Makes the validity of a cached response dependent on the specified items in the cache.</summary>
	/// <param name="cacheKeys">A collection that contains the keys of the items that the cached response is dependent on.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AddCacheItemDependencies(ArrayList cacheKeys)
	{
		throw new NotImplementedException();
	}

	/// <summary>Makes the validity of a cached item dependent on the specified items in the cache.</summary>
	/// <param name="cacheKeys">An array that contains the keys of the items that the cached response is dependent on.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AddCacheItemDependencies(string[] cacheKeys)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds an associates cache dependencies with the response that enable the response to be invalidated if it is cached and if the specified dependencies change.</summary>
	/// <param name="dependencies">A file, cache key, or <see cref="T:System.Web.Caching.CacheDependency" /> object to add to the list of application dependencies.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AddCacheDependency(params CacheDependency[] dependencies)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds a single file name to the collection of file names on which the current response is dependent.</summary>
	/// <param name="filename">The name of the file to add.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AddFileDependency(string filename)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds file names to the collection of file names on which the current response is dependent.</summary>
	/// <param name="filenames">The names of the files to add.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AddFileDependencies(ArrayList filenames)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds an array of file names to the collection of file names on which the current response is dependent.</summary>
	/// <param name="filenames">An array of file names to add.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AddFileDependencies(string[] filenames)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds an HTTP header to the current response. This method is provided for compatibility with earlier versions of ASP.</summary>
	/// <param name="name">The name of the HTTP header to add <paramref name="value" /> to.</param>
	/// <param name="value">The string to add to the header.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AddHeader(string name, string value)
	{
		throw new NotImplementedException();
	}

	/// <summary>[Supported in the .NET Framework 4.5.2 and later versions]Registers a callback that the ASP.NET runtime will invoke immediately before response headers are sent for this request.</summary>
	/// <param name="callback">The callback method.</param>
	/// <returns>Returns  an<see cref="T:System.Web.ISubscriptionToken" /> object that represents the subscription to the OnSendingHeaders pseudo event..</returns>
	public virtual ISubscriptionToken AddOnSendingHeaders(Action<HttpContextBase> callback)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds an HTTP cookie to the HTTP response cookie collection.</summary>
	/// <param name="cookie">The cookie to add to the response.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AppendCookie(HttpCookie cookie)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds an HTTP header to the current response.</summary>
	/// <param name="name">The name of the HTTP header to add to the current response.</param>
	/// <param name="value">The value of the header.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AppendHeader(string name, string value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds custom log information to the Internet Information Services (IIS) log file.</summary>
	/// <param name="param">The text to add to the log file.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void AppendToLog(string param)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds a session ID to the virtual path if the session is using <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state, and returns the combined path.</summary>
	/// <param name="virtualPath">The virtual path of a resource.</param>
	/// <returns>The virtual path, with the session ID inserted.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string ApplyAppPathModifier(string virtualPath)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sends the currently buffered response to the client.</summary>
	/// <param name="callback">The callback object.</param>
	/// <param name="state">The response state.</param>
	/// <returns>The asynchronous result object.</returns>
	public virtual IAsyncResult BeginFlush(AsyncCallback callback, object state)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes a string of binary characters to the HTTP output stream.</summary>
	/// <param name="buffer">The binary characters to write to the current response.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void BinaryWrite(byte[] buffer)
	{
		throw new NotImplementedException();
	}

	/// <summary>Clears all headers and content output from the current response.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Clear()
	{
		throw new NotImplementedException();
	}

	/// <summary>Clears all content from the current response.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void ClearContent()
	{
		throw new NotImplementedException();
	}

	/// <summary>Clears all headers from the current response.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void ClearHeaders()
	{
		throw new NotImplementedException();
	}

	/// <summary>Closes the socket connection to a client.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Close()
	{
		throw new NotImplementedException();
	}

	/// <summary>Disables kernel caching for the current response.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void DisableKernelCache()
	{
		throw new NotImplementedException();
	}

	/// <summary>Disables IIS user-mode caching for this response.</summary>
	public virtual void DisableUserCache()
	{
		throw new NotImplementedException();
	}

	/// <summary>Sends all currently buffered output to the client, stops execution of the requested process, and raises the <see cref="E:System.Web.HttpApplication.EndRequest" /> event.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void End()
	{
		throw new NotImplementedException();
	}

	/// <summary>Completes an asynchronous flush operation.</summary>
	/// <param name="asyncResult">The asynchronous result object.</param>
	public virtual void EndFlush(IAsyncResult asyncResult)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sends all currently buffered output to the client.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Flush()
	{
		throw new NotImplementedException();
	}

	/// <summary>Asynchronously sends all currently buffered output to the client.</summary>
	/// <returns>The task the represents the asynchronous operation.</returns>
	public virtual Task FlushAsync()
	{
		throw new NotImplementedException();
	}

	/// <summary>Appends an HTTP <see langword="PICS-Label" /> header to the current response.</summary>
	/// <param name="value">The string to add to the <see langword="PICS-Label" /> header.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Pics(string value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Redirects a request to the specified URL.</summary>
	/// <param name="url">The target location.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Redirect(string url)
	{
		throw new NotImplementedException();
	}

	/// <summary>Redirects a request to the specified URL and specifies whether execution of the current process should terminate.</summary>
	/// <param name="url">The target location.</param>
	/// <param name="endResponse">
	///       <see langword="true" /> to terminate the current process.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Redirect(string url, bool endResponse)
	{
		throw new NotImplementedException();
	}

	/// <summary>Redirects the request to a new URL by using route parameter values.</summary>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectToRoute(object routeValues)
	{
		throw new NotImplementedException();
	}

	/// <summary>Redirects the request to a new URL by using a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectToRoute(string routeName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Redirects the request to a new URL by using route parameter values.</summary>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectToRoute(RouteValueDictionary routeValues)
	{
		throw new NotImplementedException();
	}

	/// <summary>Redirects the request to a new URL by using route parameter values and a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectToRoute(string routeName, object routeValues)
	{
		throw new NotImplementedException();
	}

	/// <summary>Redirects the request to a new URL by using route parameter values and a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectToRoute(string routeName, RouteValueDictionary routeValues)
	{
		throw new NotImplementedException();
	}

	/// <summary>Performs a permanent redirection from the requested URL to a new URL by using route parameter values.</summary>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectToRoutePermanent(object routeValues)
	{
		throw new NotImplementedException();
	}

	/// <summary>Performs a permanent redirection from the requested URL to a new URL by using a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectToRoutePermanent(string routeName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Performs a permanent redirection from the requested URL to a new URL by using route parameter values.</summary>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectToRoutePermanent(RouteValueDictionary routeValues)
	{
		throw new NotImplementedException();
	}

	/// <summary>Performs a permanent redirection from the requested URL to a new URL by using the route parameter values and the name of the route that correspond to the new URL.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectToRoutePermanent(string routeName, object routeValues)
	{
		throw new NotImplementedException();
	}

	/// <summary>Performs a permanent redirection from the requested URL to a new URL by using route parameter values and a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
	{
		throw new NotImplementedException();
	}

	/// <summary>Performs a permanent redirect from the requested URL to the specified URL.</summary>
	/// <param name="url">The location to which the request is redirected.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectPermanent(string url)
	{
		throw new NotImplementedException();
	}

	/// <summary>Performs a permanent redirect from the requested URL to the specified URL, and provides the option to complete the response.</summary>
	/// <param name="url">The location to which the request is redirected.</param>
	/// <param name="endResponse">
	///       <see langword="true" /> to terminate the response; otherwise <see langword="false" />. The default is <see langword="false" />.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RedirectPermanent(string url, bool endResponse)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes from the cache all cached items that are associated with the specified path.</summary>
	/// <param name="path">The virtual absolute path to the items to be removed from the cache.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RemoveOutputCacheItem(string path)
	{
		throw new NotImplementedException();
	}

	/// <summary>Uses the specified output-cache provider to remove all output-cache artifacts that are associated with the specified path.</summary>
	/// <param name="path">The virtual absolute path of the items that are removed from the cache.</param>
	/// <param name="providerName">The provider that is used to remove the output-cache artifacts that are associated with the specified path.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RemoveOutputCacheItem(string path, string providerName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates an existing cookie in the cookie collection.</summary>
	/// <param name="cookie">The cookie in the collection to be updated.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void SetCookie(HttpCookie cookie)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the specified file to the HTTP response output stream, without buffering it in memory.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void TransmitFile(string filename)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the specified part of a file to the HTTP response output stream, without buffering it in memory.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
	/// <param name="offset">The position in the file where writing starts.</param>
	/// <param name="length">The number of bytes to write, starting at <paramref name="offset" />.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void TransmitFile(string filename, long offset, long length)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes a character to an HTTP response output stream.</summary>
	/// <param name="ch">The character to write to the HTTP output stream.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Write(char ch)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the specified array of characters to the HTTP response output stream.</summary>
	/// <param name="buffer">The character array to write.</param>
	/// <param name="index">The position in the character array where writing starts.</param>
	/// <param name="count">The number of characters to write, starting at <paramref name="index" />.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Write(char[] buffer, int index, int count)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the specified object to the HTTP response stream.</summary>
	/// <param name="obj">The object to write to the HTTP output stream.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Write(object obj)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the specified string to the HTTP response output stream.</summary>
	/// <param name="s">The string to write to the HTTP output stream.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Write(string s)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the contents of the specified file to the HTTP response output stream as a file block.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void WriteFile(string filename)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the contents of the specified file to the HTTP response output stream and specifies whether the content is written as a memory block.</summary>
	/// <param name="filename">The name of the file to write to the current response.</param>
	/// <param name="readIntoMemory">
	///       <see langword="true" /> to write the file into a memory block.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void WriteFile(string filename, bool readIntoMemory)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the specified file to the HTTP response output stream.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
	/// <param name="offset">The position in the file where writing starts.</param>
	/// <param name="size">The number of bytes to write, starting at <paramref name="offset" />.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void WriteFile(string filename, long offset, long size)
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the specified file to the HTTP response output stream.</summary>
	/// <param name="fileHandle">The file handle of the file to write to the HTTP output stream.</param>
	/// <param name="offset">The position in the file where writing starts.</param>
	/// <param name="size">The number of bytes to write, starting at <paramref name="offset" />.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void WriteFile(IntPtr fileHandle, long offset, long size)
	{
		throw new NotImplementedException();
	}

	/// <summary>Inserts substitution blocks into the response, which enables dynamic generation of regions for cached output responses.</summary>
	/// <param name="callback">The method, user control, or object to substitute.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void WriteSubstitution(HttpResponseSubstitutionCallback callback)
	{
		throw new NotImplementedException();
	}

	/// <summary>This API is to support applications sending push promises to HTTP 2.0 clients. You can find more details regarding to Http2 server push at HTTP/2 Specification Section 8.2: Server Push.</summary>
	/// <param name="path">The URL of the push request. It should be the virtual path of the relative resource that the server wants to push to the client.</param>
	public virtual void PushPromise(string path)
	{
		throw new NotImplementedException();
	}

	/// <summary>This API is to support applications sending push promises to HTTP 2.0 clients. You can find more details regarding to Http2 server push at HTTP/2 Specification Section 8.2: Server Push.</summary>
	/// <param name="path">The URL of the push request. It should be the virtual path of the relative resource that the server wants to push to the client.</param>
	/// <param name="method">Http request method that would be used by the push request.</param>
	/// <param name="headers">Http request header that would be used by the push request.</param>
	public virtual void PushPromise(string path, string method, NameValueCollection headers)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpResponseBase" /> class.</summary>
	protected HttpResponseBase()
	{
	}
}
