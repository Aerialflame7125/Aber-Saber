using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Caching;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;

namespace System.Web;

/// <summary>Encapsulates HTTP-response information from an ASP.NET operation.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpResponse
{
	internal HttpWorkerRequest WorkerRequest;

	internal HttpResponseStream output_stream;

	internal bool buffer = true;

	private ArrayList fileDependencies;

	private HttpContext context;

	private TextWriter writer;

	private HttpCachePolicy cache_policy;

	private Encoding encoding;

	private HttpCookieCollection cookies;

	private int status_code = 200;

	private string status_description = "OK";

	private string content_type = "text/html";

	private string charset;

	private bool charset_set;

	private CachedRawResponse cached_response;

	private string user_cache_control = "private";

	private string redirect_location;

	private string version_header;

	private bool version_header_checked;

	private long content_length = -1L;

	private HttpHeaderCollection headers;

	private bool headers_sent;

	private NameValueCollection cached_headers;

	private string transfer_encoding;

	internal bool use_chunked;

	private bool closed;

	private bool completed;

	internal bool suppress_content;

	private string app_path_mod;

	private bool is_request_being_redirected;

	private Encoding headerEncoding;

	private const int bufLen = 65535;

	internal string VersionHeader
	{
		get
		{
			if (!version_header_checked && version_header == null)
			{
				version_header_checked = true;
				HttpRuntimeSection section = HttpRuntime.Section;
				if (section != null && section.EnableVersionHeader)
				{
					version_header = Environment.Version.ToString(3);
				}
			}
			return version_header;
		}
	}

	internal HttpContext Context
	{
		get
		{
			return context;
		}
		set
		{
			context = value;
		}
	}

	internal string[] FileDependencies
	{
		get
		{
			if (fileDependencies == null || fileDependencies.Count == 0)
			{
				return new string[0];
			}
			return (string[])fileDependencies.ToArray(typeof(string));
		}
	}

	private ArrayList FileDependenciesArray
	{
		get
		{
			if (fileDependencies == null)
			{
				fileDependencies = new ArrayList();
			}
			return fileDependencies;
		}
	}

	/// <summary>Gets or sets a value indicating whether to buffer output and send it after the complete response is finished processing.</summary>
	/// <returns>
	///     <see langword="true" /> if the output to client is buffered; otherwise, <see langword="false" />.</returns>
	public bool Buffer
	{
		get
		{
			return buffer;
		}
		set
		{
			buffer = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether to buffer output and send it after the complete page is finished processing.</summary>
	/// <returns>
	///     <see langword="true" /> if the output to client is buffered; otherwise <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool BufferOutput
	{
		get
		{
			return buffer;
		}
		set
		{
			buffer = value;
		}
	}

	/// <summary>Gets or sets the HTTP character set of the output stream.</summary>
	/// <returns>A <see cref="T:System.Text.Encoding" /> object that contains information about the character set of the current response.</returns>
	/// <exception cref="T:System.ArgumentNullException">Attempted to set <see cref="P:System.Web.HttpResponse.ContentEncoding" /> to <see langword="null" />.</exception>
	public Encoding ContentEncoding
	{
		get
		{
			if (encoding == null)
			{
				if (context != null)
				{
					string parameter = HttpRequest.GetParameter(context.Request.ContentType, "; charset=");
					if (parameter != null)
					{
						try
						{
							encoding = Encoding.GetEncoding(parameter);
						}
						catch
						{
						}
					}
				}
				if (encoding == null)
				{
					encoding = WebEncoding.ResponseEncoding;
				}
			}
			return encoding;
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentException("ContentEncoding can not be null");
			}
			encoding = value;
			if (writer is HttpWriter httpWriter)
			{
				httpWriter.SetEncoding(encoding);
			}
		}
	}

	/// <summary>Gets or sets the HTTP MIME type of the output stream.</summary>
	/// <returns>The HTTP MIME type of the output stream. The default value is "<see langword="text/html" />".</returns>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.HttpResponse.ContentType" /> property is set to <see langword="null" />.</exception>
	public string ContentType
	{
		get
		{
			return content_type;
		}
		set
		{
			content_type = value;
		}
	}

	/// <summary>Gets or sets the HTTP character set of the output stream.</summary>
	/// <returns>The HTTP character set of the output stream.</returns>
	/// <exception cref="T:System.Web.HttpException">The <see langword="Charset" /> property was set after headers were sent.</exception>
	public string Charset
	{
		get
		{
			if (charset == null)
			{
				charset = ContentEncoding.WebName;
			}
			return charset;
		}
		set
		{
			charset_set = true;
			charset = value;
		}
	}

	/// <summary>Gets the response cookie collection.</summary>
	/// <returns>The response cookie collection.</returns>
	public HttpCookieCollection Cookies
	{
		get
		{
			if (cookies == null)
			{
				cookies = new HttpCookieCollection(auto_fill: true, read_only: false);
			}
			return cookies;
		}
	}

	/// <summary>Gets or sets the number of minutes before a page cached on a browser expires. If the user returns to the same page before it expires, the cached version is displayed. <see cref="P:System.Web.HttpResponse.Expires" /> is provided for compatibility with earlier versions of ASP.</summary>
	/// <returns>The number of minutes before the page expires.</returns>
	public int Expires
	{
		get
		{
			if (cache_policy == null)
			{
				return 0;
			}
			return cache_policy.ExpireMinutes();
		}
		set
		{
			Cache.SetExpires(DateTime.Now + new TimeSpan(0, value, 0));
		}
	}

	/// <summary>Gets or sets the absolute date and time at which to remove cached information from the cache. <see cref="P:System.Web.HttpResponse.ExpiresAbsolute" /> is provided for compatibility with earlier versions of ASP.</summary>
	/// <returns>The date and time at which the page expires.</returns>
	public DateTime ExpiresAbsolute
	{
		get
		{
			return Cache.Expires;
		}
		set
		{
			Cache.SetExpires(value);
		}
	}

	/// <summary>Gets or sets a wrapping filter object that is used to modify the HTTP entity body before transmission.</summary>
	/// <returns>The <see cref="T:System.IO.Stream" /> object that acts as the output filter.</returns>
	/// <exception cref="T:System.Web.HttpException">Filtering is not allowed with the entity.</exception>
	public Stream Filter
	{
		get
		{
			if (WorkerRequest == null)
			{
				return null;
			}
			return output_stream.Filter;
		}
		set
		{
			output_stream.Filter = value;
		}
	}

	/// <summary>Gets or sets an <see cref="T:System.Text.Encoding" /> object that represents the encoding for the current header output stream.</summary>
	/// <returns>An <see cref="T:System.Text.Encoding" /> that contains information about the character set for the current header.</returns>
	/// <exception cref="T:System.ArgumentNullException">The encoding value is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">The encoding value is <see cref="P:System.Text.Encoding.Unicode" />.- or -The headers have already been sent.</exception>
	public Encoding HeaderEncoding
	{
		get
		{
			if (headerEncoding == null)
			{
				if (!(WebConfigurationManager.SafeGetSection("system.web/globalization", typeof(GlobalizationSection)) is GlobalizationSection globalizationSection))
				{
					headerEncoding = Encoding.UTF8;
				}
				else
				{
					headerEncoding = globalizationSection.ResponseHeaderEncoding;
					if (headerEncoding == Encoding.Unicode)
					{
						throw new HttpException("HeaderEncoding must not be Unicode");
					}
				}
			}
			return headerEncoding;
		}
		set
		{
			if (headers_sent)
			{
				throw new HttpException("headers have already been sent");
			}
			if (value == null)
			{
				throw new ArgumentNullException("HeaderEncoding");
			}
			if (value == Encoding.Unicode)
			{
				throw new HttpException("HeaderEncoding must not be Unicode");
			}
			headerEncoding = value;
		}
	}

	/// <summary>Gets the collection of response headers.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of response headers.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">The operation requires the integrated pipeline mode in IIS 7.0 and at least the .NET Framework version 3.0.</exception>
	public NameValueCollection Headers
	{
		get
		{
			if (headers == null)
			{
				headers = new HttpHeaderCollection();
			}
			return headers;
		}
	}

	/// <summary>Gets a value indicating whether the client is still connected to the server.</summary>
	/// <returns>
	///     <see langword="true" /> if the client is currently connected; otherwise, <see langword="false" />.</returns>
	public bool IsClientConnected
	{
		get
		{
			if (WorkerRequest == null)
			{
				return true;
			}
			return WorkerRequest.IsClientConnected();
		}
	}

	/// <summary>Gets a Boolean value indicating whether the client is being transferred to a new location.</summary>
	/// <returns>
	///     <see langword="true" /> if the value of the location response header is different than the current location; otherwise, <see langword="false" />.</returns>
	public bool IsRequestBeingRedirected => is_request_being_redirected;

	/// <summary>Enables output of text to the outgoing HTTP response stream.</summary>
	/// <returns>A <see cref="T:System.IO.TextWriter" /> object that enables custom output to the client.</returns>
	public TextWriter Output
	{
		get
		{
			return writer;
		}
		set
		{
			writer = value;
		}
	}

	/// <summary>Enables binary output to the outgoing HTTP content body.</summary>
	/// <returns>An IO <see cref="T:System.IO.Stream" /> representing the raw contents of the outgoing HTTP content body.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="OutputStream" /> is not available.</exception>
	public Stream OutputStream => output_stream;

	/// <summary>Gets or sets the value of the HTTP <see langword="Location" /> header.</summary>
	/// <returns>The absolute URI that is transmitted to the client in the HTTP <see langword="Location" /> header.</returns>
	/// <exception cref="T:System.Web.HttpException">The HTTP headers have already been written.</exception>
	public string RedirectLocation
	{
		get
		{
			return redirect_location;
		}
		set
		{
			redirect_location = value;
		}
	}

	/// <summary>Sets the <see langword="Status" /> line that is returned to the client.</summary>
	/// <returns>Setting the status code causes a string describing the status of the HTTP output to be returned to the client. The default value is 200 (OK).</returns>
	/// <exception cref="T:System.Web.HttpException">Status is set to an invalid status code.</exception>
	public string Status
	{
		get
		{
			return status_code + " " + StatusDescription;
		}
		set
		{
			int num = value.IndexOf(' ');
			if (num == -1)
			{
				throw new HttpException("Invalid format for the Status property");
			}
			if (!int.TryParse(value.Substring(0, num), out status_code))
			{
				throw new HttpException("Invalid format for the Status property");
			}
			status_description = value.Substring(num + 1);
		}
	}

	/// <summary>Gets or sets a value qualifying the status code of the response.</summary>
	/// <returns>An integer value that represents the IIS 7.0 sub status code.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">The operation requires the integrated pipeline mode in IIS 7.0 and at least the .NET Framework version 3.0.</exception>
	/// <exception cref="T:System.Web.HttpException">The status code is set after all HTTP headers have been sent.</exception>
	public int SubStatusCode { get; set; }

	/// <summary>Gets or sets a value that specifies whether forms authentication redirection to the login page should be suppressed.</summary>
	/// <returns>
	///     <see langword="true" /> if forms authentication redirection should be suppressed; otherwise, <see langword="false" />.</returns>
	public bool SuppressFormsAuthenticationRedirect { get; set; }

	/// <summary>Gets or sets a value that specifies whether IIS 7.0 custom errors are disabled.</summary>
	/// <returns>
	///     <see langword="true" /> to disable IIS custom errors; otherwise, <see langword="false" />.</returns>
	public bool TrySkipIisCustomErrors { get; set; }

	/// <summary>Gets or sets the HTTP status code of the output returned to the client.</summary>
	/// <returns>An Integer representing the status of the HTTP output returned to the client. The default value is 200 (OK). For a listing of valid status codes, see Http Status Codes.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <see cref="P:System.Web.HttpResponse.StatusCode" /> is set after the HTTP headers have been sent.</exception>
	public int StatusCode
	{
		get
		{
			return status_code;
		}
		set
		{
			if (headers_sent)
			{
				throw new HttpException("headers have already been sent");
			}
			status_code = value;
			status_description = null;
		}
	}

	/// <summary>Gets or sets the HTTP status string of the output returned to the client.</summary>
	/// <returns>A string that describes the status of the HTTP output returned to the client. The default value is "OK". For a listing of valid status codes, see Http Status Codes.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="StatusDescription" /> is set after the HTTP headers have been sent.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value has a length greater than 512.</exception>
	public string StatusDescription
	{
		get
		{
			if (status_description == null)
			{
				status_description = HttpWorkerRequest.GetStatusDescription(status_code);
			}
			return status_description;
		}
		set
		{
			if (headers_sent)
			{
				throw new HttpException("headers have already been sent");
			}
			status_description = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether to send HTTP content to the client.</summary>
	/// <returns>
	///     <see langword="true" /> to suppress output; otherwise, <see langword="false" />.</returns>
	public bool SuppressContent
	{
		get
		{
			return suppress_content;
		}
		set
		{
			suppress_content = value;
		}
	}

	internal bool HeadersSent => headers_sent;

	internal bool IsCached
	{
		get
		{
			return cached_response != null;
		}
		set
		{
			if (value)
			{
				cached_response = new CachedRawResponse(cache_policy);
			}
			else
			{
				cached_response = null;
			}
		}
	}

	/// <summary>Gets the caching policy (such as expiration time, privacy settings, and vary clauses) of a Web page.</summary>
	/// <returns>An <see cref="T:System.Web.HttpCachePolicy" /> object that contains information about the caching policy of the current response.</returns>
	public HttpCachePolicy Cache
	{
		get
		{
			if (cache_policy == null)
			{
				cache_policy = new HttpCachePolicy();
			}
			return cache_policy;
		}
	}

	/// <summary>Gets or sets the <see langword="Cache-Control" /> HTTP header that matches one of the <see cref="T:System.Web.HttpCacheability" /> enumeration values.</summary>
	/// <returns>A string representation of the <see cref="T:System.Web.HttpCacheability" /> enumeration value.</returns>
	/// <exception cref="T:System.ArgumentException">The string value set does not match one of the <see cref="T:System.Web.HttpCacheability" /> enumeration values.</exception>
	public string CacheControl
	{
		get
		{
			if (user_cache_control == null)
			{
				return "private";
			}
			return user_cache_control;
		}
		set
		{
			if (value == null || value == "")
			{
				Cache.SetCacheability(HttpCacheability.NoCache);
				user_cache_control = null;
				return;
			}
			if (string.Compare(value, "public", ignoreCase: true, Helpers.InvariantCulture) == 0)
			{
				Cache.SetCacheability(HttpCacheability.Public);
				user_cache_control = "public";
				return;
			}
			if (string.Compare(value, "private", ignoreCase: true, Helpers.InvariantCulture) == 0)
			{
				Cache.SetCacheability(HttpCacheability.Private);
				user_cache_control = "private";
				return;
			}
			if (string.Compare(value, "no-cache", ignoreCase: true, Helpers.InvariantCulture) == 0)
			{
				Cache.SetCacheability(HttpCacheability.NoCache);
				user_cache_control = "no-cache";
				return;
			}
			throw new ArgumentException("CacheControl property only allows `public', `private' or no-cache, for different uses, use Response.AppendHeader");
		}
	}

	internal HttpResponse()
	{
		output_stream = new HttpResponseStream(this);
		writer = new HttpWriter(this);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpResponse" /> class.</summary>
	/// <param name="writer">A <see cref="T:System.IO.TextWriter" /> object that enables custom HTTP output.</param>
	public HttpResponse(TextWriter writer)
		: this()
	{
		this.writer = writer;
	}

	internal HttpResponse(HttpWorkerRequest worker_request, HttpContext context)
		: this()
	{
		WorkerRequest = worker_request;
		this.context = context;
		if (worker_request != null && worker_request.GetHttpVersion() == "HTTP/1.1")
		{
			string serverVariable = worker_request.GetServerVariable("GATEWAY_INTERFACE");
			use_chunked = string.IsNullOrEmpty(serverVariable) || !serverVariable.StartsWith("cgi", StringComparison.OrdinalIgnoreCase);
		}
		else
		{
			use_chunked = false;
		}
		writer = new HttpWriter(this);
	}

	internal TextWriter SetTextWriter(TextWriter writer)
	{
		TextWriter result = this.writer;
		this.writer = writer;
		return result;
	}

	/// <summary>Associates a set of cache dependencies with the response to facilitate invalidation of the response if it is stored in the output cache and the specified dependencies change.</summary>
	/// <param name="dependencies">A file, cache key, or <see cref="T:System.Web.Caching.CacheDependency" /> to add to the list of application dependencies.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="dependencies" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">This method was called too late in the cache processing pipeline, after the cached response was already created.</exception>
	[MonoTODO("Not implemented")]
	public void AddCacheDependency(params CacheDependency[] dependencies)
	{
		throw new NotImplementedException();
	}

	/// <summary>Makes the validity of a cached item dependent on another item in the cache.</summary>
	/// <param name="cacheKeys">An array of item keys that the cached response is dependent upon.</param>
	[MonoTODO("Not implemented")]
	public void AddCacheItemDependencies(string[] cacheKeys)
	{
		throw new NotImplementedException();
	}

	/// <summary>Makes the validity of a cached response dependent on other items in the cache.</summary>
	/// <param name="cacheKeys">The <see cref="T:System.Collections.ArrayList" /> that contains the keys of the items that the current cached response is dependent upon.</param>
	[MonoTODO("Currently does nothing")]
	public void AddCacheItemDependencies(ArrayList cacheKeys)
	{
	}

	/// <summary>Makes the validity of a cached response dependent on another item in the cache.</summary>
	/// <param name="cacheKey">The key of the item that the cached response is dependent upon.</param>
	[MonoTODO("Currently does nothing")]
	public void AddCacheItemDependency(string cacheKey)
	{
	}

	/// <summary>Adds a group of file names to the collection of file names on which the current response is dependent.</summary>
	/// <param name="filenames">The collection of files to add.</param>
	public void AddFileDependencies(ArrayList filenames)
	{
		if (filenames != null && filenames.Count != 0)
		{
			FileDependenciesArray.AddRange(filenames);
		}
	}

	/// <summary>Adds an array of file names to the collection of file names on which the current response is dependent.</summary>
	/// <param name="filenames">An array of files to add.</param>
	public void AddFileDependencies(string[] filenames)
	{
		if (filenames != null && filenames.Length != 0)
		{
			FileDependenciesArray.AddRange(filenames);
		}
	}

	/// <summary>Adds a single file name to the collection of file names on which the current response is dependent.</summary>
	/// <param name="filename">The name of the file to add.</param>
	public void AddFileDependency(string filename)
	{
		if (filename != null && !(filename == string.Empty))
		{
			FileDependenciesArray.Add(filename);
		}
	}

	/// <summary>Adds an HTTP header to the output stream. <see cref="M:System.Web.HttpResponse.AddHeader(System.String,System.String)" /> is provided for compatibility with earlier versions of ASP.</summary>
	/// <param name="name">The name of the HTTP header to add <paramref name="value" /> to.</param>
	/// <param name="value">The string to add to the header.</param>
	public void AddHeader(string name, string value)
	{
		AppendHeader(name, value);
	}

	/// <summary>Adds an HTTP cookie to the intrinsic cookie collection.</summary>
	/// <param name="cookie">The <see cref="T:System.Web.HttpCookie" /> to add to the output stream.</param>
	/// <exception cref="T:System.Web.HttpException">A cookie is appended after the HTTP headers have been sent.</exception>
	public void AppendCookie(HttpCookie cookie)
	{
		Cookies.Add(cookie);
	}

	/// <summary>Adds an HTTP header to the output stream.</summary>
	/// <param name="name">The name of the HTTP header to add to the output stream.</param>
	/// <param name="value">The string to append to the header.</param>
	/// <exception cref="T:System.Web.HttpException">The header is appended after the HTTP headers have been sent.</exception>
	public void AppendHeader(string name, string value)
	{
		if (headers_sent)
		{
			throw new HttpException("Headers have been already sent");
		}
		if (string.Compare(name, "content-length", StringComparison.OrdinalIgnoreCase) == 0)
		{
			content_length = (long)ulong.Parse(value);
			use_chunked = false;
		}
		else if (string.Compare(name, "content-type", StringComparison.OrdinalIgnoreCase) == 0)
		{
			ContentType = value;
		}
		else if (string.Compare(name, "transfer-encoding", StringComparison.OrdinalIgnoreCase) == 0)
		{
			transfer_encoding = value;
			use_chunked = false;
		}
		else if (string.Compare(name, "cache-control", StringComparison.OrdinalIgnoreCase) == 0)
		{
			user_cache_control = value;
		}
		else
		{
			Headers.Add(name, value);
		}
	}

	/// <summary>Adds custom log information to the Internet Information Services (IIS) log file.</summary>
	/// <param name="param">The text to add to the log file.</param>
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
	public void AppendToLog(string param)
	{
		Console.Write("System.Web: ");
		Console.WriteLine(param);
	}

	/// <summary>Adds a session ID to the virtual path if the session is using <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state and returns the combined path. If <see cref="P:System.Web.Configuration.SessionStateSection.Cookieless" /> session state is not used, <see cref="M:System.Web.HttpResponse.ApplyAppPathModifier(System.String)" /> returns the original virtual path.</summary>
	/// <param name="virtualPath">The virtual path to a resource. </param>
	/// <returns>The <paramref name="virtualPath" /> with the session ID inserted.</returns>
	public string ApplyAppPathModifier(string virtualPath)
	{
		if (virtualPath == null || context == null)
		{
			return null;
		}
		if (virtualPath.Length == 0)
		{
			return context.Request.RootVirtualDir;
		}
		if (UrlUtils.IsRelativeUrl(virtualPath))
		{
			virtualPath = UrlUtils.Combine(context.Request.RootVirtualDir, virtualPath);
		}
		else if (UrlUtils.IsRooted(virtualPath))
		{
			virtualPath = UrlUtils.Canonic(virtualPath);
		}
		SessionStateSection config = WebConfigurationManager.GetWebApplicationSection("system.web/sessionState") as SessionStateSection;
		if (!SessionStateModule.IsCookieLess(context, config))
		{
			return virtualPath;
		}
		if (app_path_mod != null && virtualPath.IndexOf(app_path_mod) < 0)
		{
			if (UrlUtils.HasSessionId(virtualPath))
			{
				virtualPath = UrlUtils.RemoveSessionId(VirtualPathUtility.GetDirectory(virtualPath), virtualPath);
			}
			return UrlUtils.InsertSessionId(app_path_mod, virtualPath);
		}
		return virtualPath;
	}

	/// <summary>Writes a string of binary characters to the HTTP output stream.</summary>
	/// <param name="buffer">The bytes to write to the output stream.</param>
	public void BinaryWrite(byte[] buffer)
	{
		output_stream.Write(buffer, 0, buffer.Length);
	}

	internal void BinaryWrite(byte[] buffer, int start, int len)
	{
		output_stream.Write(buffer, start, len);
	}

	/// <summary>Clears all content output from the buffer stream.</summary>
	public void Clear()
	{
		ClearContent();
	}

	/// <summary>Clears all content output from the buffer stream.</summary>
	public void ClearContent()
	{
		output_stream.Clear();
		content_length = -1L;
	}

	/// <summary>Clears all headers from the buffer stream.</summary>
	/// <exception cref="T:System.Web.HttpException">Headers are cleared after the HTTP headers have been sent.</exception>
	public void ClearHeaders()
	{
		if (headers_sent)
		{
			throw new HttpException("headers have been already sent");
		}
		content_length = -1L;
		content_type = "text/html";
		transfer_encoding = null;
		user_cache_control = "private";
		if (cache_policy != null)
		{
			cache_policy.Cacheability = HttpCacheability.Private;
		}
		if (headers != null)
		{
			headers.Clear();
		}
	}

	/// <summary>Closes the socket connection to a client.</summary>
	public void Close()
	{
		if (!closed)
		{
			if (WorkerRequest != null)
			{
				WorkerRequest.CloseConnection();
			}
			closed = true;
		}
	}

	/// <summary>Disables kernel caching for the current response.</summary>
	public void DisableKernelCache()
	{
	}

	/// <summary>Sends all currently buffered output to the client, stops execution of the page, and raises the <see cref="E:System.Web.HttpApplication.EndRequest" /> event.</summary>
	/// <exception cref="T:System.Threading.ThreadAbortException">The call to <see cref="M:System.Web.HttpResponse.End" /> has terminated the current request.</exception>
	public void End()
	{
		if (context != null)
		{
			if (context.TimeoutPossible)
			{
				Thread.CurrentThread.Abort(FlagEnd.Value);
			}
			else
			{
				context.ApplicationInstance?.CompleteRequest();
			}
		}
	}

	private void AddHeadersNoCache(NameValueCollection write_headers, bool final_flush)
	{
		if (use_chunked)
		{
			write_headers.Add("Transfer-Encoding", "chunked");
		}
		else if (transfer_encoding != null)
		{
			write_headers.Add("Transfer-Encoding", transfer_encoding);
		}
		if (redirect_location != null)
		{
			write_headers.Add("Location", redirect_location);
		}
		string versionHeader = VersionHeader;
		if (versionHeader != null)
		{
			write_headers.Add("X-AspNet-Version", versionHeader);
		}
		if (content_length >= 0)
		{
			write_headers.Add(HttpWorkerRequest.GetKnownResponseHeaderName(11), content_length.ToString(Helpers.InvariantCulture));
		}
		else if (BufferOutput)
		{
			if (final_flush)
			{
				content_length = output_stream.total;
				write_headers.Add(HttpWorkerRequest.GetKnownResponseHeaderName(11), content_length.ToString(Helpers.InvariantCulture));
			}
			else if (use_chunked)
			{
				write_headers.Add(HttpWorkerRequest.GetKnownResponseHeaderName(1), "close");
			}
		}
		else if (use_chunked)
		{
			write_headers.Add(HttpWorkerRequest.GetKnownResponseHeaderName(1), "close");
		}
		if (cache_policy != null)
		{
			cache_policy.SetHeaders(this, headers);
		}
		else
		{
			write_headers.Add("Cache-Control", CacheControl);
		}
		if (content_type != null)
		{
			string text = content_type;
			if ((charset_set || text == "text/plain" || text == "text/html") && text.IndexOf("charset=") == -1 && !string.IsNullOrEmpty(charset))
			{
				text = text + "; charset=" + charset;
			}
			write_headers.Add("Content-Type", text);
		}
		if (cookies != null && cookies.Count != 0)
		{
			int count = cookies.Count;
			for (int i = 0; i < count; i++)
			{
				write_headers.Add("Set-Cookie", cookies.Get(i).GetCookieHeaderValue());
			}
		}
	}

	internal void WriteHeaders(bool final_flush)
	{
		if (headers_sent)
		{
			return;
		}
		if (context != null)
		{
			context.ApplicationInstance?.TriggerPreSendRequestHeaders();
		}
		headers_sent = true;
		if (cached_response != null)
		{
			cached_response.SetHeaders(headers);
		}
		NameValueCollection nameValueCollection;
		if (cached_headers != null)
		{
			nameValueCollection = cached_headers;
		}
		else
		{
			nameValueCollection = Headers;
			AddHeadersNoCache(nameValueCollection, final_flush);
		}
		if (WorkerRequest != null)
		{
			WorkerRequest.SendStatus(status_code, StatusDescription);
		}
		if (WorkerRequest == null)
		{
			return;
		}
		for (int i = 0; i < nameValueCollection.Count; i++)
		{
			string key = nameValueCollection.GetKey(i);
			int knownResponseHeaderIndex = HttpWorkerRequest.GetKnownResponseHeaderIndex(key);
			string[] values = nameValueCollection.GetValues(i);
			if (values == null)
			{
				continue;
			}
			string[] array = values;
			foreach (string value in array)
			{
				if (knownResponseHeaderIndex > -1)
				{
					WorkerRequest.SendKnownResponseHeader(knownResponseHeaderIndex, value);
				}
				else
				{
					WorkerRequest.SendUnknownResponseHeader(key, value);
				}
			}
		}
	}

	internal void DoFilter(bool close)
	{
		if (output_stream.HaveFilter && context != null && context.Error == null)
		{
			output_stream.ApplyFilter(close);
		}
	}

	internal void Flush(bool final_flush)
	{
		if (completed)
		{
			throw new HttpException("Server cannot flush a completed response");
		}
		DoFilter(final_flush);
		if (!headers_sent && (final_flush || status_code != 200))
		{
			use_chunked = false;
		}
		bool flag = context != null && context.Request.HttpMethod == "HEAD";
		if (suppress_content || flag)
		{
			if (!headers_sent)
			{
				WriteHeaders(final_flush: true);
			}
			output_stream.Clear();
			if (WorkerRequest != null)
			{
				output_stream.Flush(WorkerRequest, final_flush: true);
			}
			completed = true;
			return;
		}
		completed = final_flush;
		if (!headers_sent)
		{
			WriteHeaders(final_flush);
		}
		if (context != null)
		{
			context.ApplicationInstance?.TriggerPreSendRequestContent();
		}
		if (IsCached)
		{
			cached_response.SetData(output_stream.GetData());
		}
		if (WorkerRequest != null)
		{
			output_stream.Flush(WorkerRequest, final_flush);
		}
	}

	/// <summary>Sends all currently buffered output to the client.</summary>
	/// <exception cref="T:System.Web.HttpException">The cache is flushed after the response has been sent.</exception>
	public void Flush()
	{
		Flush(final_flush: false);
	}

	/// <summary>Appends a HTTP <see langword="PICS-Label" /> header to the output stream.</summary>
	/// <param name="value">The string to add to the <see langword="PICS-Label" /> header.</param>
	public void Pics(string value)
	{
		AppendHeader("PICS-Label", value);
	}

	private void Redirect(string url, bool endResponse, int code)
	{
		if (url == null)
		{
			throw new ArgumentNullException("url");
		}
		if (headers_sent)
		{
			throw new HttpException("Headers have already been sent");
		}
		if (url.IndexOf('\n') != -1)
		{
			throw new ArgumentException("Redirect URI cannot contain newline characters.", "url");
		}
		is_request_being_redirected = true;
		ClearHeaders();
		ClearContent();
		StatusCode = code;
		url = ApplyAppPathModifier(url);
		if ((!StrUtils.StartsWith(url, "http:", ignore_case: true) && !StrUtils.StartsWith(url, "https:", ignore_case: true) && !StrUtils.StartsWith(url, "file:", ignore_case: true) && !StrUtils.StartsWith(url, "ftp:", ignore_case: true)) || 1 == 0)
		{
			HttpRuntimeSection section = HttpRuntime.Section;
			if (section != null && section.UseFullyQualifiedRedirectUrl)
			{
				UriBuilder uriBuilder = new UriBuilder(context.Request.Url);
				int num = url.IndexOf('?');
				if (num == -1)
				{
					uriBuilder.Path = url;
					uriBuilder.Query = null;
				}
				else
				{
					uriBuilder.Path = url.Substring(0, num);
					uriBuilder.Query = url.Substring(num + 1);
				}
				uriBuilder.Fragment = null;
				uriBuilder.Password = null;
				uriBuilder.UserName = null;
				url = uriBuilder.Uri.ToString();
			}
		}
		redirect_location = url;
		Write("<html><head><title>Object moved</title></head><body>\r\n");
		Write("<h2>Object moved to <a href=\"" + url + "\">here</a></h2>\r\n");
		Write("</body><html>\r\n");
		if (endResponse)
		{
			End();
		}
		is_request_being_redirected = false;
	}

	/// <summary>Redirects a request to a new URL and specifies the new URL.</summary>
	/// <param name="url">The target location. </param>
	/// <exception cref="T:System.Web.HttpException">A redirection is attempted after the HTTP headers have been sent.</exception>
	public void Redirect(string url)
	{
		Redirect(url, endResponse: true);
	}

	/// <summary>Redirects a client to a new URL. Specifies the new URL and whether execution of the current page should terminate.</summary>
	/// <param name="url">The location of the target. </param>
	/// <param name="endResponse">Indicates whether execution of the current page should terminate. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="url" /> contains a newline character.</exception>
	/// <exception cref="T:System.Web.HttpException">A redirection is attempted after the HTTP headers have been sent.</exception>
	/// <exception cref="T:System.ApplicationException">The page request is the result of a callback.</exception>
	public void Redirect(string url, bool endResponse)
	{
		Redirect(url, endResponse, 302);
	}

	/// <summary>Performs a permanent redirection from the requested URL to the specified URL.</summary>
	/// <param name="url">The location to redirect the request to. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="url" /> includes a newline character (\n).</exception>
	public void RedirectPermanent(string url)
	{
		RedirectPermanent(url, endResponse: true);
	}

	/// <summary>Performs a permanent redirection from the requested URL to the specified URL, and provides the option to complete the response. </summary>
	/// <param name="url">The location to redirect the request to. </param>
	/// <param name="endResponse">
	///       <see langword="true" /> to terminate the response; otherwise <see langword="false" />. The default is <see langword="false" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="url" /> includes a newline character (\n).</exception>
	public void RedirectPermanent(string url, bool endResponse)
	{
		Redirect(url, endResponse, 301);
	}

	/// <summary>Redirects a request to a new URL by using route parameter values.</summary>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
	public void RedirectToRoute(object routeValues)
	{
		RedirectToRoute("RedirectToRoute", null, new RouteValueDictionary(routeValues), 302, endResponse: true);
	}

	/// <summary>Redirects a request to a new URL by using route parameter values.</summary>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
	public void RedirectToRoute(RouteValueDictionary routeValues)
	{
		RedirectToRoute("RedirectToRoute", null, routeValues, 302, endResponse: true);
	}

	/// <summary>Redirects a request to a new URL by using a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
	public void RedirectToRoute(string routeName)
	{
		RedirectToRoute("RedirectToRoute", routeName, null, 302, endResponse: true);
	}

	/// <summary>Redirects a request to a new URL by using route parameter values and a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
	public void RedirectToRoute(string routeName, object routeValues)
	{
		RedirectToRoute("RedirectToRoute", routeName, new RouteValueDictionary(routeValues), 302, endResponse: true);
	}

	/// <summary>Redirects a request to a new URL by using route parameter values and a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
	public void RedirectToRoute(string routeName, RouteValueDictionary routeValues)
	{
		RedirectToRoute("RedirectToRoute", routeName, routeValues, 302, endResponse: true);
	}

	/// <summary>Performs a permanent redirection from a requested URL to a new URL by using route parameter values.</summary>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
	public void RedirectToRoutePermanent(object routeValues)
	{
		RedirectToRoute("RedirectToRoutePermanent", null, new RouteValueDictionary(routeValues), 301, endResponse: false);
	}

	/// <summary>Performs a permanent redirection from a requested URL to a new URL by using route parameter values.</summary>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
	public void RedirectToRoutePermanent(RouteValueDictionary routeValues)
	{
		RedirectToRoute("RedirectToRoutePermanent", null, routeValues, 301, endResponse: false);
	}

	/// <summary>Performs a permanent redirection from a requested URL to a new URL by using a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
	public void RedirectToRoutePermanent(string routeName)
	{
		RedirectToRoute("RedirectToRoutePermanent", routeName, null, 301, endResponse: false);
	}

	/// <summary>Performs a permanent redirection from a requested URL to a new URL by using the route parameter values and the name of the route that correspond to the new URL.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
	public void RedirectToRoutePermanent(string routeName, object routeValues)
	{
		RedirectToRoute("RedirectToRoutePermanent", routeName, new RouteValueDictionary(routeValues), 301, endResponse: false);
	}

	/// <summary>Performs a permanent redirection from a requested URL to a new URL by using route parameter values and a route name.</summary>
	/// <param name="routeName">The name of the route.</param>
	/// <param name="routeValues">The route parameter values.</param>
	/// <exception cref="T:System.InvalidOperationException">No route corresponds to the specified route parameters.</exception>
	/// <exception cref="T:System.Web.HttpException">Redirection was attempted after the HTTP headers had been sent.</exception>
	public void RedirectToRoutePermanent(string routeName, RouteValueDictionary routeValues)
	{
		RedirectToRoute("RedirectToRoutePermanent", routeName, routeValues, 301, endResponse: false);
	}

	private void RedirectToRoute(string callerName, string routeName, RouteValueDictionary routeValues, int redirectCode, bool endResponse)
	{
		HttpRequest httpRequest = (context ?? HttpContext.Current)?.Request;
		if (httpRequest == null)
		{
			throw new NullReferenceException();
		}
		string text = RouteTable.Routes.GetVirtualPath(httpRequest.RequestContext, routeName, routeValues)?.VirtualPath;
		if (string.IsNullOrEmpty(text))
		{
			throw new InvalidOperationException("No matching route found for RedirectToRoute");
		}
		Redirect(text, endResponse: true, redirectCode);
	}

	/// <summary>Uses the specified output-cache provider to remove all output-cache items that are associated with the specified path. </summary>
	/// <param name="path">The virtual absolute path of the items that are removed from the cache. </param>
	/// <param name="providerName">The provider that is used to remove the output-cache artifacts that are associated with the specified path.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is null.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="path" /> is an invalid path.</exception>
	public static void RemoveOutputCacheItem(string path, string providerName)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (path.Length > 0 && path[0] != '/')
		{
			throw new ArgumentException("Invalid path for HttpResponse.RemoveOutputCacheItem: '" + path + "'. An absolute virtual path is expected");
		}
		OutputCache.RemoveFromProvider(path, providerName);
	}

	/// <summary>Removes from the cache all cached items that are associated with the default output-cache provider. This method is static.</summary>
	/// <param name="path">The virtual absolute path to the items that are removed from the cache.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="path" /> is not an absolute virtual path.</exception>
	public static void RemoveOutputCacheItem(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (path.Length != 0)
		{
			if (path[0] != '/')
			{
				throw new ArgumentException("'" + path + "' is not an absolute virtual path.");
			}
			RemoveOutputCacheItem(path, OutputCache.DefaultProviderName);
		}
	}

	/// <summary>Updates an existing cookie in the cookie collection.</summary>
	/// <param name="cookie">The cookie in the collection to be updated.</param>
	/// <exception cref="T:System.Web.HttpException">The cookie is set after the HTTP headers have been sent.</exception>
	/// <exception cref="T:System.Web.HttpException">Attempted to set the cookie after the HTTP headers were sent.</exception>
	/// <exception cref="T:System.Web.HttpException">The cookie is set after the HTTP headers have been sent.</exception>
	/// <exception cref="T:System.Web.HttpException">Attempted to set the cookie after the HTTP headers were sent.</exception>
	public void SetCookie(HttpCookie cookie)
	{
		AppendCookie(cookie);
	}

	/// <summary>Writes a character to an HTTP response output stream.</summary>
	/// <param name="ch">The character to write to the HTTP output stream.</param>
	public void Write(char ch)
	{
		(Output ?? throw new NullReferenceException(".NET 4.0 emulation. A null value was found where an object was required.")).Write(ch);
	}

	/// <summary>Writes an <see cref="T:System.Object" /> to an HTTP response stream.</summary>
	/// <param name="obj">The <see cref="T:System.Object" /> to write to the HTTP output stream.</param>
	public void Write(object obj)
	{
		TextWriter output = Output;
		if (output == null)
		{
			throw new NullReferenceException(".NET 4.0 emulation. A null value was found where an object was required.");
		}
		if (obj != null)
		{
			output.Write(obj.ToString());
		}
	}

	/// <summary>Writes a string to an HTTP response output stream.</summary>
	/// <param name="s">The string to write to the HTTP output stream.</param>
	public void Write(string s)
	{
		(Output ?? throw new NullReferenceException(".NET 4.0 emulation. A null value was found where an object was required.")).Write(s);
	}

	/// <summary>Writes an array of characters to an HTTP response output stream.</summary>
	/// <param name="buffer">The character array to write.</param>
	/// <param name="index">The position in the character array where writing starts.</param>
	/// <param name="count">The number of characters to write, beginning at <paramref name="index" />.</param>
	public void Write(char[] buffer, int index, int count)
	{
		(Output ?? throw new NullReferenceException(".NET 4.0 emulation. A null value was found where an object was required.")).Write(buffer, index, count);
	}

	private bool IsFileSystemDirSeparator(char ch)
	{
		if (ch != '\\')
		{
			return ch == '/';
		}
		return true;
	}

	private string GetNormalizedFileName(string fn)
	{
		if (string.IsNullOrEmpty(fn))
		{
			return fn;
		}
		int length = fn.Length;
		if (length >= 3 && fn[1] == ':' && IsFileSystemDirSeparator(fn[2]))
		{
			return Path.GetFullPath(fn);
		}
		bool flag = IsFileSystemDirSeparator(fn[0]);
		if (length >= 2 && flag && IsFileSystemDirSeparator(fn[1]))
		{
			return Path.GetFullPath(fn);
		}
		if (!flag)
		{
			HttpRequest httpRequest = (context ?? HttpContext.Current)?.Request;
			if (httpRequest != null)
			{
				return httpRequest.MapPath(fn);
			}
		}
		return fn;
	}

	internal void WriteFile(FileStream fs, long offset, long size)
	{
		byte[] array = new byte[32768];
		if (offset != 0L)
		{
			fs.Position = offset;
		}
		long num = size;
		int num2;
		while (num > 0 && (num2 = fs.Read(array, 0, (int)Math.Min(num, 32768L))) != 0)
		{
			num -= num2;
			output_stream.Write(array, 0, num2);
		}
	}

	/// <summary>Writes the contents of the specified file directly to an HTTP response output stream as a file block.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is <see langword="null" />.</exception>
	public void WriteFile(string filename)
	{
		WriteFile(filename, readIntoMemory: false);
	}

	/// <summary>Writes the contents of the specified file directly to an HTTP response output stream as a memory block.</summary>
	/// <param name="filename">The name of the file to write into a memory block.</param>
	/// <param name="readIntoMemory">Indicates whether the file will be written into a memory block.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is <see langword="null" />.</exception>
	public void WriteFile(string filename, bool readIntoMemory)
	{
		if (filename == null)
		{
			throw new ArgumentNullException("filename");
		}
		string normalizedFileName = GetNormalizedFileName(filename);
		if (readIntoMemory)
		{
			using FileStream fileStream = File.OpenRead(normalizedFileName);
			WriteFile(fileStream, 0L, fileStream.Length);
		}
		else
		{
			FileInfo fileInfo = new FileInfo(normalizedFileName);
			output_stream.WriteFile(normalizedFileName, 0L, fileInfo.Length);
		}
		if (!buffer)
		{
			output_stream.ApplyFilter(close: false);
			Flush();
		}
	}

	/// <summary>Writes the specified file directly to an HTTP response output stream.</summary>
	/// <param name="fileHandle">The file handle of the file to write to the HTTP output stream.</param>
	/// <param name="offset">The byte position in the file where writing will start.</param>
	/// <param name="size">The number of bytes to write to the output stream.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="fileHandler" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="offset" /> is less than 0.- or -
	///         <paramref name="size" /> is greater than the file size minus <paramref name="offset" />.</exception>
	public void WriteFile(IntPtr fileHandle, long offset, long size)
	{
		if (offset < 0)
		{
			throw new ArgumentNullException("offset can not be negative");
		}
		if (size < 0)
		{
			throw new ArgumentNullException("size can not be negative");
		}
		if (size != 0L)
		{
			using (FileStream fs = new FileStream(fileHandle, FileAccess.Read))
			{
				WriteFile(fs, offset, size);
			}
			if (!buffer)
			{
				output_stream.ApplyFilter(close: false);
				Flush();
			}
		}
	}

	/// <summary>Writes the specified file directly to an HTTP response output stream.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output stream.</param>
	/// <param name="offset">The byte position in the file where writing will start.</param>
	/// <param name="size">The number of bytes to write to the output stream.</param>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="offset" /> is less than 0.- or -
	///         <paramref name="size" /> is greater than the file size minus <paramref name="offset" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is <see langword="null" />.</exception>
	public void WriteFile(string filename, long offset, long size)
	{
		if (filename == null)
		{
			throw new ArgumentNullException("filename");
		}
		if (offset < 0)
		{
			throw new ArgumentNullException("offset can not be negative");
		}
		if (size < 0)
		{
			throw new ArgumentNullException("size can not be negative");
		}
		if (size != 0L)
		{
			FileStream fs = File.OpenRead(filename);
			WriteFile(fs, offset, size);
			if (!buffer)
			{
				output_stream.ApplyFilter(close: false);
				Flush();
			}
		}
	}

	/// <summary>Allows insertion of response substitution blocks into the response, which allows dynamic generation of specified response regions for output cached responses.</summary>
	/// <param name="callback">The method, user control, or object to substitute.</param>
	/// <exception cref="T:System.ArgumentException">The target of the <paramref name="callback" /> parameter is of type <see cref="T:System.Web.UI.Control" />.</exception>
	public void WriteSubstitution(HttpResponseSubstitutionCallback callback)
	{
		if (callback == null)
		{
			throw new NullReferenceException();
		}
		object target = callback.Target;
		if (target != null && target.GetType() == typeof(Control))
		{
			throw new ArgumentException("callback");
		}
		string s = callback(context);
		if (!IsCached)
		{
			Write(s);
			return;
		}
		Cache.Cacheability = HttpCacheability.Server;
		Flush();
		if (WorkerRequest == null)
		{
			Write(s);
		}
		else
		{
			byte[] bytes = WebEncoding.ResponseEncoding.GetBytes(s);
			WorkerRequest.SendResponseFromMemory(bytes, bytes.Length);
		}
		cached_response.SetData(callback);
	}

	/// <summary>Writes the specified file directly to an HTTP response output stream, without buffering it in memory.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="filename" /> parameter is <see langword="null" /></exception>
	public void TransmitFile(string filename)
	{
		if (filename == null)
		{
			throw new ArgumentNullException("filename");
		}
		TransmitFile(filename, final_flush: false);
	}

	internal void TransmitFile(string filename, bool final_flush)
	{
		FileInfo fileInfo = new FileInfo(filename);
		using (fileInfo.OpenRead())
		{
		}
		output_stream.WriteFile(filename, 0L, fileInfo.Length);
		output_stream.ApplyFilter(final_flush);
		Flush(final_flush);
	}

	/// <summary>Writes the specified part of a file directly to an HTTP response output stream without buffering it in memory.</summary>
	/// <param name="filename">The name of the file to write to the HTTP output.</param>
	/// <param name="offset">The position in the file to begin to write to the HTTP output.</param>
	/// <param name="length">The number of bytes to be transmitted.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="offset" /> parameter is less than zero.- or -The <paramref name="length" /> parameter is less than -1.- or - The <paramref name="length" /> parameter specifies a number of bytes that is greater than the number of bytes the file contains minus the offset.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The out-of-process worker request is not supported.- or -The response is not using an <see cref="T:System.Web.HttpWriter" /> object.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="offset" /> parameter is less than zero or greater than the file size.- or -The <paramref name="length" /> parameter is less than -1 or greater than the value of the <paramref name="offset" /> parameter plus the file size.</exception>
	public void TransmitFile(string filename, long offset, long length)
	{
		output_stream.WriteFile(filename, offset, length);
		output_stream.ApplyFilter(close: false);
		Flush(final_flush: false);
	}

	internal void TransmitFile(VirtualFile vf)
	{
		TransmitFile(vf, final_flush: false);
	}

	internal void TransmitFile(VirtualFile vf, bool final_flush)
	{
		if (vf == null)
		{
			throw new ArgumentNullException("vf");
		}
		if (vf is DefaultVirtualFile)
		{
			TransmitFile(HostingEnvironment.MapPath(vf.VirtualPath), final_flush);
			return;
		}
		byte[] array = new byte[65535];
		using Stream stream = vf.Open();
		int count;
		while ((count = stream.Read(array, 0, 65535)) > 0)
		{
			output_stream.Write(array, 0, count);
			output_stream.ApplyFilter(final_flush);
			Flush(final_flush: false);
		}
		if (final_flush)
		{
			Flush(final_flush: true);
		}
	}

	internal void SetAppPathModifier(string app_modifier)
	{
		app_path_mod = app_modifier;
	}

	internal void SetCachedHeaders(NameValueCollection headers)
	{
		cached_headers = headers;
	}

	internal CachedRawResponse GetCachedResponse()
	{
		if (cached_response != null)
		{
			cached_response.StatusCode = StatusCode;
			cached_response.StatusDescription = StatusDescription;
		}
		return cached_response;
	}

	internal int GetOutputByteCount()
	{
		return output_stream.GetTotalLength();
	}

	internal void ReleaseResources()
	{
		if (output_stream != null)
		{
			output_stream.ReleaseResources(close_filter: true);
		}
		if (!completed)
		{
			Close();
			completed = true;
		}
	}
}
