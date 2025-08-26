using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Caching;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that provides HTTP-response information from an ASP.NET operation.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HttpResponseWrapper : HttpResponseBase
{
	private HttpResponse w;

	/// <summary>Gets or sets a value that indicates whether to buffer output and send it after the complete response has finished processing.</summary>
	/// <returns>
	///     <see langword="true" /> if the output is buffered; otherwise, <see langword="false" />.</returns>
	public override bool Buffer
	{
		get
		{
			return w.Buffer;
		}
		set
		{
			w.Buffer = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether to buffer output and send it after the complete page has finished processing.</summary>
	/// <returns>
	///     <see langword="true" /> if the output is buffered; otherwise <see langword="false" />. The default is <see langword="true" />.</returns>
	public override bool BufferOutput
	{
		get
		{
			return w.BufferOutput;
		}
		set
		{
			w.BufferOutput = value;
		}
	}

	/// <summary>Gets the caching policy (such as expiration time, privacy settings, and vary clauses) of the current Web page.</summary>
	/// <returns>The caching policy of the current response.</returns>
	public override HttpCachePolicyBase Cache => new HttpCachePolicyWrapper(w.Cache);

	/// <summary>Gets or sets the <see langword="Cache-Control" /> HTTP header that matches one of the <see cref="T:System.Web.HttpCacheability" /> enumeration values.</summary>
	/// <returns>The caching policy of the current response.</returns>
	public override string CacheControl
	{
		get
		{
			return w.CacheControl;
		}
		set
		{
			w.CacheControl = value;
		}
	}

	/// <summary>Gets or sets the HTTP character set of the current response.</summary>
	/// <returns>The HTTP character set of the current response.</returns>
	public override string Charset
	{
		get
		{
			return w.Charset;
		}
		set
		{
			w.Charset = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Threading.CancellationToken" /> object that is tripped when the client disconnects.</summary>
	/// <returns>The cancellation token that is tripped when the client disconnects.</returns>
	public override CancellationToken ClientDisconnectedToken => CancellationToken.None;

	/// <summary>Gets or sets the content encoding of the current response.</summary>
	/// <returns>The information about the content encoding of the current response.</returns>
	/// <exception cref="T:System.ArgumentNullException">Attempted to set <see cref="P:System.Web.HttpResponse.ContentEncoding" /> to <see langword="null" />.</exception>
	public override Encoding ContentEncoding
	{
		get
		{
			return w.ContentEncoding;
		}
		set
		{
			w.ContentEncoding = value;
		}
	}

	/// <summary>Gets or sets the HTTP MIME type of the current response.</summary>
	/// <returns>The HTTP MIME type of the current response. The default value is "<see langword="text/html" />".</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.HttpResponse.ContentType" /> property is set to <see langword="null" />.</exception>
	public override string ContentType
	{
		get
		{
			return w.ContentType;
		}
		set
		{
			w.ContentType = value;
		}
	}

	/// <summary>Gets the response cookie collection.</summary>
	/// <returns>The response cookie collection.</returns>
	public override HttpCookieCollection Cookies => w.Cookies;

	/// <summary>Gets or sets the number of minutes before a page that is cached on the client or proxy expires. If the user returns to the same page before it expires, the cached version is displayed. <see cref="P:System.Web.HttpResponseWrapper.Expires" /> is provided for compatibility with earlier versions of ASP.</summary>
	/// <returns>The number of minutes before the page expires.</returns>
	public override int Expires
	{
		get
		{
			return w.Expires;
		}
		set
		{
			w.Expires = value;
		}
	}

	/// <summary>Gets or sets the absolute date and time at which cached information expires in the cache. <see cref="P:System.Web.HttpResponseWrapper.ExpiresAbsolute" /> is provided for compatibility with earlier versions of ASP.</summary>
	/// <returns>The date and time at which the page expires.</returns>
	public override DateTime ExpiresAbsolute
	{
		get
		{
			return w.ExpiresAbsolute;
		}
		set
		{
			w.ExpiresAbsolute = value;
		}
	}

	/// <summary>Gets or sets a filter object that is used to modify the HTTP entity body before transmission.</summary>
	/// <returns>An object that acts as the output filter.</returns>
	public override Stream Filter
	{
		get
		{
			return w.Filter;
		}
		set
		{
			w.Filter = value;
		}
	}

	/// <summary>Gets or sets the encoding for the header of the current response.</summary>
	/// <returns>The information about the encoding for the current header.</returns>
	/// <exception cref="T:System.ArgumentNullException">The encoding value is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">The encoding value is <see cref="P:System.Text.Encoding.Unicode" />.- or -The headers have already been sent.</exception>
	public override Encoding HeaderEncoding
	{
		get
		{
			return w.HeaderEncoding;
		}
		set
		{
			w.HeaderEncoding = value;
		}
	}

	/// <summary>Gets the collection of response headers.</summary>
	/// <returns>The response headers.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">The operation requires the integrated pipeline mode in IIS 7.0 and at least the .NET Framework version 3.0.</exception>
	public override NameValueCollection Headers => w.Headers;

	/// <summary>Gets a value that indicates whether the client is connected to the server.</summary>
	/// <returns>
	///     <see langword="true" /> if the client is currently connected; otherwise, <see langword="false" />.</returns>
	public override bool IsClientConnected => w.IsClientConnected;

	/// <summary>Gets a value that indicates whether the client is being redirected to a new location.</summary>
	/// <returns>
	///     <see langword="true" /> if the value of the location response header differs from the current location; otherwise, <see langword="false" />.</returns>
	public override bool IsRequestBeingRedirected => w.IsRequestBeingRedirected;

	/// <summary>Gets the object that enables output of text to the outgoing HTTP response stream.</summary>
	/// <returns>An object that enables custom output to the client.</returns>
	public override TextWriter Output
	{
		get
		{
			return w.Output;
		}
		set
		{
			w.Output = value;
		}
	}

	/// <summary>Provides binary output to the outgoing HTTP content body.</summary>
	/// <returns>An object that represents the raw contents of the outgoing HTTP content body.</returns>
	public override Stream OutputStream => w.OutputStream;

	/// <summary>Gets or sets the value of the HTTP <see langword="Location" /> header.</summary>
	/// <returns>The absolute URL of the HTTP <see langword="Location" /> header.</returns>
	public override string RedirectLocation
	{
		get
		{
			return w.RedirectLocation;
		}
		set
		{
			w.RedirectLocation = value;
		}
	}

	/// <summary>Sets the <see langword="Status" /> value that is returned to the client.</summary>
	/// <returns>The status of the HTTP output. The default value is "200 (OK)". For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <see langword="Status" /> is set to an invalid status code.</exception>
	public override string Status
	{
		get
		{
			return w.Status;
		}
		set
		{
			w.Status = value;
		}
	}

	/// <summary>Gets or sets the HTTP status code of the output that is returned to the client.</summary>
	/// <returns>The status code of the HTTP output that is returned to the client. The default value is 200. For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <see cref="P:System.Web.HttpResponse.StatusCode" /> was set after the HTTP headers were sent.</exception>
	public override int StatusCode
	{
		get
		{
			return w.StatusCode;
		}
		set
		{
			w.StatusCode = value;
		}
	}

	/// <summary>Gets or sets the HTTP status message of the output that is returned to the client.</summary>
	/// <returns>The status message of the HTTP output that is returned to the client. The default value is "OK". For information about valid status codes, see HTTP Status Codes on the MSDN Web site.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <see langword="StatusDescription" /> was set after the HTTP headers were sent.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The status value is longer than 512 characters.</exception>
	public override string StatusDescription
	{
		get
		{
			return w.StatusDescription;
		}
		set
		{
			w.StatusDescription = value;
		}
	}

	/// <summary>Gets or sets a value that qualifies the status code of the response.</summary>
	/// <returns>The IIS 7.0 substatus code.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">The operation requires the integrated pipeline mode in IIS 7.0 and at least the .NET Framework version 3.0.</exception>
	/// <exception cref="T:System.Web.HttpException">The status code was set after all HTTP headers were sent.</exception>
	public override int SubStatusCode
	{
		get
		{
			return w.SubStatusCode;
		}
		set
		{
			w.SubStatusCode = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether to send HTTP content to the client.</summary>
	/// <returns>
	///     <see langword="true" /> if output is suppressed; otherwise, <see langword="false" />.</returns>
	public override bool SuppressContent
	{
		get
		{
			return w.SuppressContent;
		}
		set
		{
			w.SuppressContent = value;
		}
	}

	/// <summary>Gets or sets a value that specifies whether forms authentication redirection to the login page should be suppressed.</summary>
	/// <returns>
	///     <see langword="true" /> if forms authentication redirection should be suppressed; otherwise, <see langword="false" />.</returns>
	public override bool SuppressFormsAuthenticationRedirect
	{
		get
		{
			return w.SuppressFormsAuthenticationRedirect;
		}
		set
		{
			w.SuppressFormsAuthenticationRedirect = value;
		}
	}

	/// <summary>Gets or sets a value that specifies whether IIS 7.0 custom errors are disabled.</summary>
	/// <returns>
	///     <see langword="true" /> if IIS 7.0 custom errors are disabled; otherwise, <see langword="false" />.</returns>
	public override bool TrySkipIisCustomErrors
	{
		get
		{
			return w.TrySkipIisCustomErrors;
		}
		set
		{
			w.TrySkipIisCustomErrors = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpResponseWrapper" /> class.</summary>
	/// <param name="httpResponse">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="httpResponse" /> parameter is <see langword="null" />.</exception>
	public HttpResponseWrapper(HttpResponse httpResponse)
	{
		if (httpResponse == null)
		{
			throw new ArgumentNullException("httpResponse");
		}
		w = httpResponse;
	}

	/// <summary>When overridden in a derived class, associates cache dependencies with the response that enable the response to be invalidated if it is cached and if the specified dependencies change.</summary>
	/// <param name="dependencies">A file, cache key, or <see cref="T:System.Web.Caching.CacheDependency" /> object to add to the list of application dependencies.</param>
	public override void AddCacheDependency(params CacheDependency[] dependencies)
	{
		w.AddCacheDependency(dependencies);
	}

	/// <summary>Makes the validity of a cached response dependent on the specified items in the cache.</summary>
	/// <param name="cacheKeys">A collection that contains the keys of the items that the cached response is dependent on.</param>
	public override void AddCacheItemDependencies(ArrayList cacheKeys)
	{
		w.AddCacheItemDependencies(cacheKeys);
	}

	/// <summary>Makes the validity of a cached item dependent on the specified items in the cache.</summary>
	/// <param name="cacheKeys">An array that contains the keys of the items that the cached response is dependent on.</param>
	public override void AddCacheItemDependencies(string[] cacheKeys)
	{
		w.AddCacheItemDependencies(cacheKeys);
	}

	/// <summary>Makes the validity of a cached response dependent on the specified item in the cache.</summary>
	/// <param name="cacheKey">The key of the item that the cached response is dependent on.</param>
	public override void AddCacheItemDependency(string cacheKey)
	{
		w.AddCacheItemDependency(cacheKey);
	}

	/// <summary>Adds file names to the collection of file names on which the current response is dependent.</summary>
	/// <param name="filenames">The names of the files to add.</param>
	public override void AddFileDependencies(ArrayList filenames)
	{
		w.AddFileDependencies(filenames);
	}

	/// <summary>Adds an array of file names to the collection of file names on which the current response is dependent.</summary>
	/// <param name="filenames">An array of files names to add.</param>
	public override void AddFileDependencies(string[] filenames)
	{
		w.AddFileDependencies(filenames);
	}

	/// <summary>Adds a single file name to the collection of file names on which the current response is dependent.</summary>
	/// <param name="filename">The name of the file to add.</param>
	public override void AddFileDependency(string filename)
	{
		w.AddFileDependency(filename);
	}

	/// <summary>Adds an HTTP header to the current response. This method is provided for compatibility with earlier versions of ASP.</summary>
	/// <param name="name">The name of the HTTP header to add <paramref name="value" /> to.</param>
	/// <param name="value">The string to add to the header.</param>
	public override void AddHeader(string name, string value)
	{
		w.AddHeader(name, value);
	}

	/// <summary>Adds an HTTP cookie to the HTTP response cookie collection.</summary>
	/// <param name="cookie">The cookie to add to the response.</param>
	/// <exception cref="T:System.Web.HttpException">The cookie was added after the HTTP headers were sent.</exception>
	public override void AppendCookie(HttpCookie cookie)
	{
		w.AppendCookie(cookie);
	}

	/// <summary>Adds an HTTP header to the current response.</summary>
	/// <param name="name">The name of the HTTP header to add to the current response.</param>
	/// <param name="value">The value of the header.</param>
	/// <exception cref="T:System.Web.HttpException">The header was appended after the HTTP headers were sent.</exception>
	public override void AppendHeader(string name, string value)
	{
		w.AppendHeader(name, value);
	}

	/// <summary>Adds custom log information to the Internet Information Services (IIS) log file.</summary>
	/// <param name="param">The text to add to the log file.</param>
	public override void AppendToLog(string param)
	{
		w.AppendToLog(param);
	}

	/// <summary>Adds a session ID to the virtual path if the session is using <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state, and returns the combined path. </summary>
	/// <param name="virtualPath">The virtual path of a resource.</param>
	/// <returns>The virtual path with the session ID inserted. If <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state is not used, returns the original <paramref name="virtualpath" /> value.</returns>
	public override string ApplyAppPathModifier(string virtualPath)
	{
		return w.ApplyAppPathModifier(virtualPath);
	}

	/// <summary>Writes a string of binary characters to the HTTP output stream.</summary>
	/// <param name="buffer">The binary characters to write to the current response.</param>
	public override void BinaryWrite(byte[] buffer)
	{
		w.BinaryWrite(buffer);
	}

	/// <summary>Clears all headers and content output from the current response.</summary>
	public override void Clear()
	{
		w.Clear();
	}

	/// <summary>Clears all content output from the current response.</summary>
	public override void ClearContent()
	{
		w.ClearContent();
	}

	/// <summary>Clears all headers from the current response.</summary>
	public override void ClearHeaders()
	{
		w.ClearHeaders();
	}

	/// <summary>Closes the socket connection to a client.</summary>
	public override void Close()
	{
		w.Close();
	}

	/// <summary>Disables kernel caching for the current response.</summary>
	public override void DisableKernelCache()
	{
		w.DisableKernelCache();
	}

	/// <summary>Sends all currently buffered output to the client, stops execution of the requested process, and raises the <see cref="E:System.Web.HttpApplication.EndRequest" /> event.</summary>
	/// <exception cref="T:System.Threading.ThreadAbortException">The call to <see cref="M:System.Web.HttpResponse.End" /> has terminated the current request.</exception>
	public override void End()
	{
		w.End();
	}

	/// <summary>Sends all currently buffered output to the client.</summary>
	/// <exception cref="T:System.Web.HttpException">The method was called after the response was finished.</exception>
	public override void Flush()
	{
		w.Flush();
	}

	/// <summary>Appends an HTTP <see langword="PICS-Label" /> header to the current response.</summary>
	/// <param name="value">The string to add to the <see langword="PICS-Label" /> header.</param>
	public override void Pics(string value)
	{
		w.Pics(value);
	}

	/// <summary>Redirects a request to the specified URL.</summary>
	/// <param name="url">The target location.</param>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers were sent.</exception>
	public override void Redirect(string url)
	{
		w.Redirect(url);
	}

	/// <summary>Redirects a request to the specified URL and specifies whether execution of the current process should terminate.</summary>
	/// <param name="url">The target location. </param>
	/// <param name="endResponse">
	///       <see langword="true" /> to terminate the current process.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="url" /> contains a newline character.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers were sent.</exception>
	/// <exception cref="T:System.ApplicationException">The request is the result of a callback.</exception>
	public override void Redirect(string url, bool endResponse)
	{
		w.Redirect(url, endResponse);
	}

	/// <summary>Performs a permanent redirect from the requested URL to the specified URL.</summary>
	/// <param name="url">The URL to redirect the request to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="url" /> includes a newline character (\n).</exception>
	public override void RedirectPermanent(string url)
	{
		w.RedirectPermanent(url);
	}

	/// <summary>Performs a permanent redirect from the requested URL to the specified URL, and provides the option to complete the response.</summary>
	/// <param name="url">The URL to redirect the request to.</param>
	/// <param name="endResponse">
	///       <see langword="true" /> to terminate the response; otherwise <see langword="false" />. The default is <see langword="false" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="url" /> includes a newline character (\n).</exception>
	public override void RedirectPermanent(string url, bool endResponse)
	{
		w.RedirectPermanent(url, endResponse);
	}

	/// <summary>Uses the specified output-cache provider to remove all output-cache artifacts that are associated with the specified path.</summary>
	/// <param name="path">The virtual absolute path of the items that are removed from the cache. </param>
	/// <param name="providerName">The provider that is used to remove the output-cache artifacts that are associated with the specified path.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="path" /> is an invalid path.</exception>
	public override void RemoveOutputCacheItem(string path, string providerName)
	{
		HttpResponse.RemoveOutputCacheItem(path, providerName);
	}

	/// <summary>Removes from the cache all cached items that are associated with the specified path.</summary>
	/// <param name="path">The virtual absolute path to the items to be removed from the cache.</param>
	public override void RemoveOutputCacheItem(string path)
	{
		HttpResponse.RemoveOutputCacheItem(path);
	}

	/// <summary>Updates an existing cookie in the cookie collection.</summary>
	/// <param name="cookie">The cookie in the collection to be updated.</param>
	/// <exception cref="T:System.Web.HttpException">The cookie was set after the HTTP headers were sent.</exception>
	public override void SetCookie(HttpCookie cookie)
	{
		w.SetCookie(cookie);
	}

	/// <summary>Writes the specified file to the HTTP response output stream, without buffering it in memory.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="filename" /> is <see langword="null" /></exception>
	public override void TransmitFile(string filename)
	{
		w.TransmitFile(filename);
	}

	/// <summary>Writes the specified part of a file to the HTTP response output stream, without buffering it in memory.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
	/// <param name="offset">The position in the file where writing starts.</param>
	/// <param name="length">The number of bytes to write, starting at <paramref name="offset" />.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="offset" /> parameter is less than zero.- or -The <paramref name="length" /> parameter is less than -1.- or - The <paramref name="length" /> parameter is greater than the file size minus <paramref name="offset" />.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The out-of-process worker request is not supported.- or -The response is not using a <see cref="T:System.Web.HttpWriter" /> object.</exception>
	public override void TransmitFile(string filename, long offset, long length)
	{
		w.TransmitFile(filename, offset, length);
	}

	/// <summary>Writes a character to an HTTP response output stream.</summary>
	/// <param name="ch">The character to write to the HTTP output stream.</param>
	public override void Write(char ch)
	{
		w.Write(ch);
	}

	/// <summary>Writes the specified object to the HTTP response stream.</summary>
	/// <param name="obj">The object to write to the HTTP output stream.</param>
	public override void Write(object obj)
	{
		w.Write(obj);
	}

	/// <summary>Writes the specified string to the HTTP response output stream.</summary>
	/// <param name="s">The string to write to the HTTP output stream.</param>
	public override void Write(string s)
	{
		w.Write(s);
	}

	/// <summary>Writes the specified array of characters to the HTTP response output stream.</summary>
	/// <param name="buffer">The character array to write.</param>
	/// <param name="index">The position in the character array where writing starts.</param>
	/// <param name="count">The number of characters to write, starting at <paramref name="index" />.</param>
	public override void Write(char[] buffer, int index, int count)
	{
		w.Write(buffer, index, count);
	}

	/// <summary>Writes the contents of the specified file to the HTTP response output stream as a file block.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is <see langword="null" />.</exception>
	public override void WriteFile(string filename)
	{
		w.WriteFile(filename);
	}

	/// <summary>Writes the contents of the specified file to the HTTP response output stream and specifies whether the content is written as a memory block.</summary>
	/// <param name="filename">The name of the file to write to the current response.</param>
	/// <param name="readIntoMemory">
	///       <see langword="true" /> to write the file into a memory block.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is <see langword="null" />.</exception>
	public override void WriteFile(string filename, bool readIntoMemory)
	{
		w.WriteFile(filename, readIntoMemory);
	}

	/// <summary>Writes the specified file to the HTTP response output stream.</summary>
	/// <param name="fileHandle">The file handle of the file to write to the HTTP output stream.</param>
	/// <param name="offset">The position in the file where writing starts.</param>
	/// <param name="size">The number of bytes to write, starting at <paramref name="offset" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="fileHandle" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="offset" /> is less than 0.- or -
	///         <paramref name="size" /> is greater than the file size minus <paramref name="offset" />.</exception>
	public override void WriteFile(IntPtr fileHandle, long offset, long size)
	{
		w.WriteFile(fileHandle, offset, size);
	}

	/// <summary>Writes the specified file to the HTTP response output stream.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
	/// <param name="offset">The position in the file where writing starts.</param>
	/// <param name="size">The number of bytes to write, starting at <paramref name="offset" />.</param>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="offset" /> is less than 0.- or -
	///         <paramref name="size" /> is greater than the file size minus <paramref name="offset" />.</exception>
	public override void WriteFile(string filename, long offset, long size)
	{
		w.WriteFile(filename, offset, size);
	}

	/// <summary>Inserts substitution blocks into the response, which enables dynamic generation of regions for cached output responses.</summary>
	/// <param name="callback">The method, user control, or object to substitute.</param>
	/// <exception cref="T:System.ArgumentException">The target of the <paramref name="callback" /> parameter is of type <see cref="T:System.Web.UI.Control" />.</exception>
	public override void WriteSubstitution(HttpResponseSubstitutionCallback callback)
	{
		w.WriteSubstitution(callback);
	}
}
