using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Configuration;
using System.Web.Routing;
using System.Web.Util;

namespace System.Web;

/// <summary>Enables ASP.NET to read the HTTP values sent by a client during a Web request. </summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpRequest
{
	private class BufferlessInputStream : Stream
	{
		private HttpRequest request;

		private int content_length;

		private byte[] preloadedBuffer;

		private bool preloaded_served;

		private bool checked_maxRequestLength;

		private long position;

		public override bool CanRead => true;

		public override bool CanSeek => false;

		public override bool CanWrite => false;

		public override long Length => content_length;

		public override long Position
		{
			get
			{
				return position;
			}
			set
			{
				throw new NotSupportedException("This is a readonly stream");
			}
		}

		public BufferlessInputStream(HttpRequest request)
		{
			this.request = request;
			content_length = request.ContentLength;
		}

		public override void Flush()
		{
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("offset or count less than zero.");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("offset+count", "The size of the buffer is less than offset + count.");
			}
			if (count == 0 || request.worker_request == null)
			{
				return 0;
			}
			if (!checked_maxRequestLength)
			{
				int num = content_length / 1024;
				HttpRuntimeSection section = HttpRuntime.Section;
				if (num > section.MaxRequestLength)
				{
					throw HttpException.NewWithCode(400, "Upload size exceeds httpRuntime limit.", 3004);
				}
				checked_maxRequestLength = true;
			}
			if (!preloaded_served)
			{
				if (preloadedBuffer == null)
				{
					preloadedBuffer = request.worker_request.GetPreloadedEntityBody();
				}
				if (preloadedBuffer != null)
				{
					long num2 = preloadedBuffer.Length - position;
					int num3 = (int)Math.Min(count, num2);
					Array.Copy(preloadedBuffer, position, buffer, offset, num3);
					position += num3;
					if (num3 == num2)
					{
						preloaded_served = true;
					}
					return num3;
				}
				preloaded_served = true;
			}
			if (position < content_length)
			{
				long num4 = content_length - position;
				int size = count;
				if (num4 < count)
				{
					size = (int)num4;
				}
				int num5 = request.worker_request.ReadEntityBody(buffer, offset, size);
				position += num5;
				return num5;
			}
			return 0;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("Can not seek on the HttpRequest.BufferlessInputStream");
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException("Can not set length on the HttpRequest.BufferlessInputStream");
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("Can not write on the HttpRequest.BufferlessInputStream");
		}
	}

	private HttpWorkerRequest worker_request;

	private HttpContext context;

	private WebROCollection query_string_nvc;

	private string orig_url;

	private UriBuilder url_components;

	private string client_target;

	private HttpBrowserCapabilities browser_capabilities;

	private string file_path;

	private string base_virtual_dir;

	private string root_virtual_dir;

	private string client_file_path;

	private string content_type;

	private int content_length = -1;

	private Encoding encoding;

	private string current_exe_path;

	private string physical_path;

	private string unescaped_path;

	private string original_path;

	private string path_info;

	private string path_info_unvalidated;

	private string raw_url;

	private string raw_url_unvalidated;

	private WebROCollection all_params;

	private NameValueCollection headers;

	private WebROCollection headers_unvalidated;

	private Stream input_stream;

	private InputFilterStream input_filter;

	private Stream filter;

	private HttpCookieCollection cookies;

	private HttpCookieCollection cookies_unvalidated;

	private string http_method;

	private WebROCollection form;

	private HttpFileCollection files;

	private ServerVariablesCollection server_variables;

	private HttpClientCertificate client_cert;

	private string request_type;

	private string[] accept_types;

	private string[] user_languages;

	private Uri cached_url;

	private TempFileStream request_file;

	private static readonly IPAddress[] host_addresses;

	private bool validate_cookies;

	private bool validate_query_string;

	private bool validate_form;

	private bool checked_cookies;

	private bool checked_query_string;

	private bool checked_form;

	private static readonly UrlMappingCollection urlMappings;

	private static readonly char[] queryTrimChars;

	private bool lazyFormValidation;

	private bool lazyQueryStringValidation;

	private bool inputValidationEnabled;

	private RequestContext requestContext;

	private BufferlessInputStream bufferlessInputStream;

	private static bool validateRequestNewMode;

	private string anonymous_id;

	private const int INPUT_BUFFER_SIZE = 32768;

	internal static bool ValidateRequestNewMode => validateRequestNewMode;

	internal bool InputValidationEnabled => inputValidationEnabled;

	private static char[] RequestPathInvalidCharacters { get; set; }

	internal UriBuilder UrlComponents
	{
		get
		{
			if (url_components == null)
			{
				byte[] queryStringRawBytes = worker_request.GetQueryStringRawBytes();
				BuildUrlComponents(query: (queryStringRawBytes == null) ? worker_request.GetQueryString() : ContentEncoding.GetString(queryStringRawBytes), path: ApplyUrlMapping(worker_request.GetUriPath()));
			}
			return url_components;
		}
	}

	/// <summary>Gets a string array of client-supported MIME accept types.</summary>
	/// <returns>A string array of client-supported MIME accept types.</returns>
	public string[] AcceptTypes
	{
		get
		{
			if (worker_request == null)
			{
				return null;
			}
			if (accept_types == null)
			{
				accept_types = SplitHeader(20);
			}
			return accept_types;
		}
	}

	/// <summary>Gets the <see cref="T:System.Security.Principal.WindowsIdentity" /> type for the current user.</summary>
	/// <returns>A <see cref="T:System.Security.Principal.WindowsIdentity" /> object for the current Microsoft Internet Information Services (IIS) authentication settings.</returns>
	/// <exception cref="T:System.InvalidOperationException">The Web application is running in IIS 7 integrated mode and the <see cref="E:System.Web.HttpApplication.PostAuthenticateRequest" /> event has not yet been raised.</exception>
	public WindowsIdentity LogonUserIdentity
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the anonymous identifier for the user, if present.</summary>
	/// <returns>A string representing the current anonymous user identifier.</returns>
	public string AnonymousID
	{
		get
		{
			return anonymous_id;
		}
		internal set
		{
			anonymous_id = value;
		}
	}

	/// <summary>Gets the ASP.NET application's virtual application root path on the server.</summary>
	/// <returns>The virtual path of the current application.</returns>
	public string ApplicationPath
	{
		get
		{
			if (worker_request == null)
			{
				return null;
			}
			return worker_request.GetAppPath();
		}
	}

	/// <summary>Gets or sets information about the requesting client's browser capabilities.</summary>
	/// <returns>An <see cref="T:System.Web.HttpBrowserCapabilities" /> object listing the capabilities of the client's browser.</returns>
	public HttpBrowserCapabilities Browser
	{
		get
		{
			if (browser_capabilities == null)
			{
				browser_capabilities = HttpCapabilitiesBase.BrowserCapabilitiesProvider.GetBrowserCapabilities(this);
			}
			return browser_capabilities;
		}
		set
		{
			browser_capabilities = value;
		}
	}

	internal bool BrowserMightHaveSpecialWriter
	{
		get
		{
			if (browser_capabilities == null)
			{
				return HttpApplicationFactory.AppBrowsersFiles.Length != 0;
			}
			return true;
		}
	}

	internal bool BrowserMightHaveAdapters
	{
		get
		{
			if (browser_capabilities == null)
			{
				return HttpApplicationFactory.AppBrowsersFiles.Length != 0;
			}
			return true;
		}
	}

	/// <summary>Gets the current request's client security certificate.</summary>
	/// <returns>An <see cref="T:System.Web.HttpClientCertificate" /> object containing information about the client's security certificate settings.</returns>
	public HttpClientCertificate ClientCertificate
	{
		get
		{
			if (client_cert == null)
			{
				client_cert = new HttpClientCertificate(worker_request);
			}
			return client_cert;
		}
	}

	/// <summary>Gets or sets the character set of the entity-body.</summary>
	/// <returns>An <see cref="T:System.Text.Encoding" /> object representing the client's character set.</returns>
	public Encoding ContentEncoding
	{
		get
		{
			if (encoding == null)
			{
				if (worker_request == null)
				{
					throw HttpException.NewWithCode("No HttpWorkerRequest", 3001);
				}
				string parameter = GetParameter(ContentType, "; charset=");
				if (parameter == null)
				{
					encoding = WebEncoding.RequestEncoding;
				}
				else
				{
					try
					{
						encoding = Encoding.GetEncoding(parameter);
					}
					catch
					{
						encoding = WebEncoding.RequestEncoding;
					}
				}
			}
			return encoding;
		}
		set
		{
			encoding = value;
		}
	}

	/// <summary>Specifies the length, in bytes, of content sent by the client.</summary>
	/// <returns>The length, in bytes, of content sent by the client.</returns>
	public int ContentLength
	{
		get
		{
			if (content_length == -1)
			{
				if (worker_request == null)
				{
					return 0;
				}
				string knownRequestHeader = worker_request.GetKnownRequestHeader(11);
				if (knownRequestHeader != null)
				{
					try
					{
						content_length = int.Parse(knownRequestHeader);
					}
					catch
					{
					}
				}
			}
			if (content_length < 0)
			{
				return 0;
			}
			return content_length;
		}
	}

	/// <summary>Gets or sets the MIME content type of the incoming request.</summary>
	/// <returns>A string representing the MIME content type of the incoming request, for example, "text/html". Additional common MIME types include "audio.wav", "image/gif", and "application/pdf".</returns>
	public string ContentType
	{
		get
		{
			if (content_type == null)
			{
				if (worker_request != null)
				{
					content_type = worker_request.GetKnownRequestHeader(12);
				}
				if (content_type == null)
				{
					content_type = string.Empty;
				}
			}
			return content_type;
		}
		set
		{
			content_type = value;
		}
	}

	internal HttpCookieCollection CookiesNoValidation
	{
		get
		{
			if (cookies_unvalidated == null)
			{
				if (worker_request == null)
				{
					cookies_unvalidated = new HttpCookieCollection();
				}
				else
				{
					string knownRequestHeader = worker_request.GetKnownRequestHeader(25);
					cookies_unvalidated = new HttpCookieCollection(knownRequestHeader);
				}
			}
			return cookies_unvalidated;
		}
	}

	/// <summary>Gets a collection of cookies sent by the client.</summary>
	/// <returns>An <see cref="T:System.Web.HttpCookieCollection" /> object representing the client's cookie variables.</returns>
	public HttpCookieCollection Cookies
	{
		get
		{
			if (cookies == null)
			{
				cookies = CookiesNoValidation;
			}
			if ((validate_cookies | validateRequestNewMode) && !checked_cookies)
			{
				checked_cookies = true;
				ValidateCookieCollection(cookies);
			}
			return cookies;
		}
	}

	/// <summary>Gets the virtual path of the current request.</summary>
	/// <returns>The virtual path of the current request.</returns>
	public string CurrentExecutionFilePath
	{
		get
		{
			if (current_exe_path != null)
			{
				return current_exe_path;
			}
			return FilePath;
		}
	}

	/// <summary>Gets the extension of the file name that is specified in the <see cref="P:System.Web.HttpRequest.CurrentExecutionFilePath" /> property.</summary>
	/// <returns>The extension of the file name that is specified in the <see cref="P:System.Web.HttpRequest.CurrentExecutionFilePath" /> property.</returns>
	public string CurrentExecutionFilePathExtension => System.IO.Path.GetExtension(CurrentExecutionFilePath);

	/// <summary>Gets the virtual path of the application root and makes it relative by using the tilde (~) notation for the application root (as in "~/page.aspx").</summary>
	/// <returns>The virtual path of the application root for the current request.</returns>
	public string AppRelativeCurrentExecutionFilePath => VirtualPathUtility.ToAppRelative(CurrentExecutionFilePath);

	/// <summary>Gets the virtual path of the current request.</summary>
	/// <returns>The virtual path of the current request.</returns>
	public string FilePath
	{
		get
		{
			if (worker_request == null)
			{
				return "/";
			}
			if (file_path == null)
			{
				file_path = UrlUtils.Canonic(ApplyUrlMapping(worker_request.GetFilePath()));
			}
			return file_path;
		}
	}

	internal string ClientFilePath
	{
		get
		{
			if (client_file_path == null)
			{
				if (worker_request == null)
				{
					return "/";
				}
				return UrlUtils.Canonic(ApplyUrlMapping(worker_request.GetFilePath()));
			}
			return client_file_path;
		}
		set
		{
			if (value == null || value.Length == 0)
			{
				client_file_path = null;
			}
			else
			{
				client_file_path = value;
			}
		}
	}

	internal string BaseVirtualDir
	{
		get
		{
			if (base_virtual_dir == null)
			{
				base_virtual_dir = FilePath;
				if (UrlUtils.HasSessionId(base_virtual_dir))
				{
					base_virtual_dir = UrlUtils.RemoveSessionId(VirtualPathUtility.GetDirectory(base_virtual_dir), base_virtual_dir);
				}
				int num = base_virtual_dir.LastIndexOf('/');
				if (num != -1)
				{
					if (num == 0)
					{
						num = 1;
					}
					base_virtual_dir = base_virtual_dir.Substring(0, num);
				}
				else
				{
					base_virtual_dir = "/";
				}
			}
			return base_virtual_dir;
		}
	}

	/// <summary>Gets the collection of files uploaded by the client, in multipart MIME format.</summary>
	/// <returns>An <see cref="T:System.Web.HttpFileCollection" /> object representing a collection of files uploaded by the client. The items of the <see cref="T:System.Web.HttpFileCollection" /> object are of type <see cref="T:System.Web.HttpPostedFile" />.</returns>
	public HttpFileCollection Files
	{
		get
		{
			if (files == null)
			{
				files = new HttpFileCollection();
				if (worker_request != null && IsContentType("multipart/form-data", starts_with: true))
				{
					form = new WebROCollection();
					LoadMultiPart();
					form.Protect();
				}
			}
			return files;
		}
	}

	/// <summary>Gets or sets the filter to use when reading the current input stream.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> object to be used as the filter.</returns>
	/// <exception cref="T:System.Web.HttpException">The specified <see cref="T:System.IO.Stream" /> is invalid.</exception>
	public Stream Filter
	{
		get
		{
			if (filter != null)
			{
				return filter;
			}
			if (input_filter == null)
			{
				input_filter = new InputFilterStream();
			}
			return input_filter;
		}
		set
		{
			if (input_filter == null)
			{
				throw new HttpException("Invalid filter");
			}
			filter = value;
		}
	}

	internal WebROCollection FormUnvalidated
	{
		get
		{
			if (form == null)
			{
				form = new WebROCollection();
				files = new HttpFileCollection();
				if (IsContentType("multipart/form-data", starts_with: true))
				{
					LoadMultiPart();
				}
				else if (IsContentType("application/x-www-form-urlencoded", starts_with: true))
				{
					LoadWwwForm();
				}
				form.Protect();
			}
			return form;
		}
	}

	/// <summary>Gets a collection of form variables.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> representing a collection of form variables.</returns>
	public NameValueCollection Form
	{
		get
		{
			NameValueCollection formUnvalidated = FormUnvalidated;
			if (validateRequestNewMode && !checked_form)
			{
				if (!lazyFormValidation)
				{
					checked_form = true;
					ValidateNameValueCollection("Form", formUnvalidated, RequestValidationSource.Form);
				}
			}
			else if (validate_form && !checked_form)
			{
				checked_form = true;
				ValidateNameValueCollection("Form", formUnvalidated);
			}
			return formUnvalidated;
		}
	}

	internal NameValueCollection HeadersNoValidation
	{
		get
		{
			if (headers_unvalidated == null)
			{
				headers_unvalidated = new HeadersCollection(this);
			}
			return headers_unvalidated;
		}
	}

	/// <summary>Gets a collection of HTTP headers.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of headers.</returns>
	public NameValueCollection Headers
	{
		get
		{
			if (headers == null)
			{
				headers = HeadersNoValidation;
				if (validateRequestNewMode)
				{
					RequestValidator current = RequestValidator.Current;
					string[] allKeys = headers.AllKeys;
					foreach (string text in allKeys)
					{
						string value = headers[text];
						if (!current.IsValidRequestString(HttpContext.Current, value, RequestValidationSource.Headers, text, out var _))
						{
							ThrowValidationException("Headers", text, value);
						}
					}
				}
			}
			return headers;
		}
	}

	/// <summary>Gets the HTTP data transfer method (such as <see langword="GET" />, <see langword="POST" />, or <see langword="HEAD" />) used by the client.</summary>
	/// <returns>The HTTP data transfer method used by the client.</returns>
	public string HttpMethod
	{
		get
		{
			if (http_method == null)
			{
				if (worker_request != null)
				{
					http_method = worker_request.GetHttpVerbName();
				}
				else
				{
					http_method = "GET";
				}
			}
			return http_method;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Routing.RequestContext" /> instance of the current request.</summary>
	/// <returns>The <see cref="T:System.Web.Routing.RequestContext" /> instance of the current request. For non-routed requests, the <see cref="T:System.Web.Routing.RequestContext" /> object that is returned is empty.</returns>
	public RequestContext RequestContext
	{
		get
		{
			if (requestContext == null)
			{
				requestContext = new RequestContext(new HttpContextWrapper(context ?? HttpContext.Current), new RouteData());
			}
			return requestContext;
		}
		internal set
		{
			requestContext = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> object of the current <see cref="T:System.Web.HttpWorkerRequest" /> instance.</summary>
	/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> object of the current <see cref="T:System.Web.HttpWorkerRequest" /> instance.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">The current <see cref="T:System.Web.HttpWorkerRequest" /> object is not a <see langword="System.Web.Hosting.IIS7WorkerRequest" /> object or a <see langword="System.Web.Hosting.ISAPIWorkerRequestInProc" /> object.</exception>
	public ChannelBinding HttpChannelBinding
	{
		get
		{
			throw new PlatformNotSupportedException("This property is not supported.");
		}
	}

	/// <summary>Gets the contents of the incoming HTTP entity body.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> object representing the contents of the incoming HTTP content body.</returns>
	public Stream InputStream
	{
		get
		{
			if (input_stream == null)
			{
				MakeInputStream();
			}
			return input_stream;
		}
	}

	/// <summary>Gets a value indicating whether the request has been authenticated.</summary>
	/// <returns>
	///     <see langword="true" /> if the request is authenticated; otherwise, <see langword="false" />.</returns>
	public bool IsAuthenticated
	{
		get
		{
			if (context.User == null || context.User.Identity == null)
			{
				return false;
			}
			return context.User.Identity.IsAuthenticated;
		}
	}

	/// <summary>Gets a value indicating whether the HTTP connection uses secure sockets (that is, HTTPS).</summary>
	/// <returns>
	///     <see langword="true" /> if the connection is an SSL connection; otherwise, <see langword="false" />.</returns>
	public bool IsSecureConnection
	{
		get
		{
			if (worker_request == null)
			{
				return false;
			}
			return worker_request.IsSecure();
		}
	}

	/// <summary>Gets the specified object from the <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collections.</summary>
	/// <param name="key">The name of the collection member to get. </param>
	/// <returns>The <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection member specified in the <paramref name="key" /> parameter. If the specified <paramref name="key" /> is not found, then <see langword="null" /> is returned.</returns>
	public string this[string key]
	{
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
		get
		{
			string text = QueryString[key];
			if (text == null)
			{
				text = Form[key];
			}
			if (text == null)
			{
				HttpCookie httpCookie = Cookies[key];
				if (httpCookie != null)
				{
					text = httpCookie.Value;
				}
			}
			if (text == null)
			{
				text = ServerVariables[key];
			}
			return text;
		}
	}

	/// <summary>Gets a combined collection of <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, and <see cref="P:System.Web.HttpRequest.ServerVariables" /> items.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object. </returns>
	public NameValueCollection Params
	{
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
		get
		{
			if (all_params == null)
			{
				all_params = new HttpParamsCollection(QueryString, Form, ServerVariables, Cookies);
			}
			return all_params;
		}
	}

	internal string PathNoValidation
	{
		get
		{
			if (original_path == null)
			{
				if (url_components != null)
				{
					original_path = UrlComponents.Path;
				}
				else
				{
					original_path = ApplyUrlMapping(worker_request.GetUriPath());
				}
			}
			return original_path;
		}
	}

	/// <summary>Gets the virtual path of the current request.</summary>
	/// <returns>The virtual path of the current request.</returns>
	public string Path
	{
		get
		{
			if (unescaped_path == null)
			{
				unescaped_path = PathNoValidation;
				if (validateRequestNewMode && !RequestValidator.Current.IsValidRequestString(HttpContext.Current, unescaped_path, RequestValidationSource.Path, null, out var _))
				{
					ThrowValidationException("Path", "Path", unescaped_path);
				}
			}
			return unescaped_path;
		}
	}

	internal string PathInfoNoValidation
	{
		get
		{
			if (path_info_unvalidated == null)
			{
				if (worker_request == null)
				{
					return string.Empty;
				}
				path_info_unvalidated = worker_request.GetPathInfo() ?? string.Empty;
			}
			return path_info_unvalidated;
		}
	}

	/// <summary>Gets the additional path information for a resource with a URL extension.</summary>
	/// <returns>The additional path information for a resource.</returns>
	public string PathInfo
	{
		get
		{
			if (path_info == null)
			{
				path_info = PathInfoNoValidation;
				if (validateRequestNewMode && !RequestValidator.Current.IsValidRequestString(HttpContext.Current, path_info, RequestValidationSource.PathInfo, null, out var _))
				{
					ThrowValidationException("PathInfo", "PathInfo", path_info);
				}
			}
			return path_info;
		}
	}

	/// <summary>Gets the physical file system path of the currently executing server application's root directory.</summary>
	/// <returns>The file system path of the current application's root directory.</returns>
	public string PhysicalApplicationPath
	{
		get
		{
			if (worker_request == null)
			{
				throw new ArgumentNullException();
			}
			string appDomainAppPath = HttpRuntime.AppDomainAppPath;
			if (SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, appDomainAppPath).Demand();
			}
			return appDomainAppPath;
		}
	}

	/// <summary>Gets the physical file system path corresponding to the requested URL.</summary>
	/// <returns>The file system path of the current request.</returns>
	public string PhysicalPath
	{
		get
		{
			if (worker_request == null)
			{
				return string.Empty;
			}
			if (physical_path == null)
			{
				physical_path = worker_request.MapPath(FilePath);
			}
			if (SecurityManager.SecurityEnabled)
			{
				new FileIOPermission(FileIOPermissionAccess.PathDiscovery, physical_path).Demand();
			}
			return physical_path;
		}
	}

	internal string RootVirtualDir
	{
		get
		{
			if (root_virtual_dir == null)
			{
				string filePath = FilePath;
				int num = filePath.LastIndexOf('/');
				if (num < 1)
				{
					root_virtual_dir = "/";
				}
				else
				{
					root_virtual_dir = filePath.Substring(0, num);
				}
			}
			return root_virtual_dir;
		}
	}

	internal WebROCollection QueryStringUnvalidated
	{
		get
		{
			if (query_string_nvc == null)
			{
				query_string_nvc = new WebROCollection();
				string text = UrlComponents.Query;
				if (text != null)
				{
					if (text.Length != 0)
					{
						text = text.Remove(0, 1);
					}
					HttpUtility.ParseQueryString(text, ContentEncoding, query_string_nvc);
				}
				query_string_nvc.Protect();
			}
			return query_string_nvc;
		}
	}

	/// <summary>Gets the collection of HTTP query string variables.</summary>
	/// <returns>The query string variables sent by the client. Keys and values are URL-decoded. </returns>
	public NameValueCollection QueryString
	{
		get
		{
			NameValueCollection queryStringUnvalidated = QueryStringUnvalidated;
			if (validateRequestNewMode && !checked_query_string)
			{
				if (!lazyQueryStringValidation)
				{
					checked_query_string = true;
					ValidateNameValueCollection("QueryString", queryStringUnvalidated, RequestValidationSource.QueryString);
				}
			}
			else if (validate_query_string && !checked_query_string)
			{
				checked_query_string = true;
				ValidateNameValueCollection("QueryString", queryStringUnvalidated);
			}
			return queryStringUnvalidated;
		}
	}

	internal string RawUrlUnvalidated
	{
		get
		{
			if (raw_url_unvalidated == null)
			{
				if (worker_request != null)
				{
					raw_url_unvalidated = worker_request.GetRawUrl();
				}
				else
				{
					raw_url_unvalidated = UrlComponents.Path + UrlComponents.Query;
				}
				if (raw_url_unvalidated == null)
				{
					raw_url_unvalidated = string.Empty;
				}
			}
			return raw_url_unvalidated;
		}
	}

	/// <summary>Gets the raw URL of the current request.</summary>
	/// <returns>The raw URL of the current request.</returns>
	public string RawUrl
	{
		get
		{
			if (raw_url == null)
			{
				raw_url = RawUrlUnvalidated;
				if (validateRequestNewMode && !RequestValidator.Current.IsValidRequestString(HttpContext.Current, raw_url, RequestValidationSource.RawUrl, null, out var _))
				{
					ThrowValidationException("RawUrl", "RawUrl", raw_url);
				}
			}
			return raw_url;
		}
	}

	/// <summary>Gets or sets the HTTP data transfer method (<see langword="GET" /> or <see langword="POST" />) used by the client.</summary>
	/// <returns>A string representing the HTTP invocation type sent by the client.</returns>
	public string RequestType
	{
		get
		{
			if (request_type == null)
			{
				if (worker_request != null)
				{
					request_type = worker_request.GetHttpVerbName();
					http_method = request_type;
				}
				else
				{
					request_type = "GET";
				}
			}
			return request_type;
		}
		set
		{
			request_type = value;
		}
	}

	/// <summary>Gets a collection of Web server variables.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of server variables.</returns>
	public NameValueCollection ServerVariables
	{
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Low)]
		get
		{
			if (server_variables == null)
			{
				server_variables = new ServerVariablesCollection(this);
			}
			return server_variables;
		}
	}

	/// <summary>Gets a <see cref="T:System.Threading.CancellationToken" /> object that is tripped when a request times out.</summary>
	/// <returns>The cancellation token.</returns>
	public CancellationToken TimedOutToken
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the number of bytes in the current input stream.</summary>
	/// <returns>The number of bytes in the input stream.</returns>
	public int TotalBytes => (int)InputStream.Length;

	/// <summary>Gets the HTTP request values without triggering request validation.</summary>
	/// <returns>The HTTP request values that have not been checked using request validation.</returns>
	public UnvalidatedRequestValues Unvalidated => new UnvalidatedRequestValues
	{
		Cookies = CookiesNoValidation,
		Files = Files,
		Form = FormUnvalidated,
		Headers = HeadersNoValidation,
		Path = PathNoValidation,
		PathInfo = PathInfoNoValidation,
		QueryString = QueryStringUnvalidated,
		RawUrl = RawUrlUnvalidated,
		Url = Url
	};

	/// <summary>Gets information about the URL of the current request.</summary>
	/// <returns>A <see cref="T:System.Uri" /> object that contains the URL of the current request.</returns>
	public Uri Url
	{
		get
		{
			if (cached_url == null)
			{
				if (orig_url == null)
				{
					cached_url = UrlComponents.Uri;
				}
				else
				{
					cached_url = new Uri(orig_url);
				}
			}
			return cached_url;
		}
	}

	/// <summary>Gets information about the URL of the client's previous request that linked to the current URL.</summary>
	/// <returns>A <see cref="T:System.Uri" /> object.</returns>
	/// <exception cref="T:System.UriFormatException">The HTTP <see langword="Referer" /> request header is malformed and cannot be converted to a <see cref="T:System.Uri" /> object. </exception>
	public Uri UrlReferrer
	{
		get
		{
			if (worker_request == null)
			{
				return null;
			}
			string knownRequestHeader = worker_request.GetKnownRequestHeader(36);
			if (knownRequestHeader == null)
			{
				return null;
			}
			Uri result = null;
			try
			{
				result = new Uri(knownRequestHeader);
			}
			catch (UriFormatException)
			{
			}
			return result;
		}
	}

	/// <summary>Gets the raw user agent string of the client browser.</summary>
	/// <returns>The raw user agent string of the client browser.</returns>
	public string UserAgent
	{
		get
		{
			if (worker_request == null)
			{
				return null;
			}
			return worker_request.GetKnownRequestHeader(39);
		}
	}

	/// <summary>Gets the IP host address of the remote client.</summary>
	/// <returns>The IP address of the remote client.</returns>
	public string UserHostAddress
	{
		get
		{
			if (worker_request == null)
			{
				return null;
			}
			return worker_request.GetRemoteAddress();
		}
	}

	/// <summary>Gets the DNS name of the remote client.</summary>
	/// <returns>The DNS name of the remote client.</returns>
	public string UserHostName
	{
		get
		{
			if (worker_request == null)
			{
				return null;
			}
			return worker_request.GetRemoteName();
		}
	}

	/// <summary>Gets a sorted string array of client language preferences.</summary>
	/// <returns>A sorted string array of client language preferences, or <see langword="null" /> if empty.</returns>
	public string[] UserLanguages
	{
		get
		{
			if (worker_request == null)
			{
				return null;
			}
			if (user_languages == null)
			{
				user_languages = SplitHeader(23);
			}
			return user_languages;
		}
	}

	internal string ClientTarget
	{
		get
		{
			return client_target;
		}
		set
		{
			client_target = value;
		}
	}

	/// <summary>Gets a value indicating whether the request is from the local computer.</summary>
	/// <returns>
	///     <see langword="true" /> if the request is from the local computer; otherwise, <see langword="false" />.</returns>
	public bool IsLocal
	{
		get
		{
			string remoteAddress = worker_request.GetRemoteAddress();
			if (StrUtils.IsNullOrEmpty(remoteAddress))
			{
				return false;
			}
			if (remoteAddress == "127.0.0.1")
			{
				return true;
			}
			IPAddress iPAddress = IPAddress.Parse(remoteAddress);
			if (IPAddress.IsLoopback(iPAddress))
			{
				return true;
			}
			for (int i = 0; i < host_addresses.Length; i++)
			{
				if (iPAddress.Equals(host_addresses[i]))
				{
					return true;
				}
			}
			return false;
		}
	}

	internal string QueryStringRaw
	{
		get
		{
			if (UrlComponents == null)
			{
				string queryString = worker_request.GetQueryString();
				if (queryString == null || queryString.Length == 0)
				{
					return string.Empty;
				}
				if (queryString[0] == '?')
				{
					return queryString;
				}
				return "?" + queryString;
			}
			return UrlComponents.Query;
		}
		set
		{
			UrlComponents.Query = value;
			cached_url = null;
			query_string_nvc = null;
		}
	}

	internal HttpWorkerRequest WorkerRequest => worker_request;

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

	private static char[] CharsFromList(string list)
	{
		string[] array = list.Split(',');
		char[] array2 = new char[array.Length];
		for (int i = 0; i < array2.Length; i++)
		{
			string text = array[i].Trim();
			if (text.Length != 1)
			{
				throw new ConfigurationErrorsException();
			}
			array2[i] = text[0];
		}
		return array2;
	}

	static HttpRequest()
	{
		queryTrimChars = new char[1] { '?' };
		try
		{
			if (WebConfigurationManager.GetWebApplicationSection("system.web/urlMappings") is UrlMappingsSection { IsEnabled: not false } urlMappingsSection)
			{
				urlMappings = urlMappingsSection.UrlMappings;
				if (urlMappings.Count == 0)
				{
					urlMappings = null;
				}
			}
			if (HttpRuntime.Section.RequestValidationMode >= new Version(4, 0))
			{
				validateRequestNewMode = true;
				string requestPathInvalidCharacters = HttpRuntime.Section.RequestPathInvalidCharacters;
				if (!string.IsNullOrEmpty(requestPathInvalidCharacters))
				{
					RequestPathInvalidCharacters = CharsFromList(requestPathInvalidCharacters);
				}
			}
		}
		catch
		{
		}
		host_addresses = GetLocalHostAddresses();
	}

	/// <summary>Initializes an <see cref="T:System.Web.HttpRequest" /> object.</summary>
	/// <param name="filename">The name of the file associated with the request. </param>
	/// <param name="url">The information regarding the URL of the current request. </param>
	/// <param name="queryString">The entire query string sent with the request (everything after the'?'). </param>
	public HttpRequest(string filename, string url, string queryString)
	{
		orig_url = url;
		url_components = new UriBuilder(url);
		url_components.Query = queryString;
		query_string_nvc = new WebROCollection();
		if (queryString != null)
		{
			HttpUtility.ParseQueryString(queryString, Encoding.Default, query_string_nvc);
		}
		query_string_nvc.Protect();
	}

	internal HttpRequest(HttpWorkerRequest worker_request, HttpContext context)
	{
		this.worker_request = worker_request;
		this.context = context;
	}

	private void BuildUrlComponents(string path, string query)
	{
		if (url_components == null)
		{
			url_components = new UriBuilder();
			url_components.Scheme = worker_request.GetProtocol();
			url_components.Host = worker_request.GetServerName();
			url_components.Port = worker_request.GetLocalPort();
			url_components.Path = path;
			if (query != null && query.Length > 0)
			{
				url_components.Query = query.TrimStart(queryTrimChars);
			}
		}
	}

	internal string ApplyUrlMapping(string url)
	{
		if (urlMappings == null)
		{
			return url;
		}
		string strA = VirtualPathUtility.ToAppRelative(url);
		UrlMapping urlMapping = null;
		foreach (UrlMapping urlMapping2 in urlMappings)
		{
			if (urlMapping2 != null && string.Compare(strA, urlMapping2.Url, StringComparison.Ordinal) == 0)
			{
				urlMapping = urlMapping2;
				break;
			}
		}
		if (urlMapping == null)
		{
			return url;
		}
		string text = VirtualPathUtility.ToAbsolute(urlMapping.MappedUrl.Trim());
		Uri uri = new Uri("http://host.com" + text);
		if (url_components != null)
		{
			url_components.Path = uri.AbsolutePath;
			url_components.Query = uri.Query.TrimStart(queryTrimChars);
			query_string_nvc = new WebROCollection();
			HttpUtility.ParseQueryString(uri.Query, Encoding.Default, query_string_nvc);
			query_string_nvc.Protect();
		}
		else
		{
			BuildUrlComponents(uri.AbsolutePath, uri.Query);
		}
		return url_components.Path;
	}

	private string[] SplitHeader(int header_index)
	{
		string[] array = null;
		string knownRequestHeader = worker_request.GetKnownRequestHeader(header_index);
		if (knownRequestHeader != null && knownRequestHeader != "" && knownRequestHeader.Trim() != "")
		{
			array = knownRequestHeader.Split(',');
			for (int num = array.Length - 1; num >= 0; num--)
			{
				array[num] = array[num].Trim();
			}
		}
		return array;
	}

	internal static string GetParameter(string header, string attr)
	{
		int num = header.IndexOf(attr);
		if (num == -1)
		{
			return null;
		}
		num += attr.Length;
		if (num >= header.Length)
		{
			return null;
		}
		char c = header[num];
		if (c != '"')
		{
			c = ' ';
		}
		int num2 = header.IndexOf(c, num + 1);
		if (num2 == -1)
		{
			if (c != '"')
			{
				return header.Substring(num);
			}
			return null;
		}
		return header.Substring(num + 1, num2 - num - 1);
	}

	private static Stream GetSubStream(Stream stream)
	{
		if (stream is IntPtrStream)
		{
			return new IntPtrStream(stream);
		}
		if (stream is MemoryStream)
		{
			MemoryStream memoryStream = (MemoryStream)stream;
			return new MemoryStream(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, writable: false, publiclyVisible: true);
		}
		if (stream is TempFileStream)
		{
			((TempFileStream)stream).SavePosition();
			return stream;
		}
		throw new NotSupportedException("The stream is " + stream.GetType());
	}

	private static void EndSubStream(Stream stream)
	{
		if (stream is TempFileStream)
		{
			((TempFileStream)stream).RestorePosition();
		}
	}

	private void LoadMultiPart()
	{
		string parameter = GetParameter(ContentType, "; boundary=");
		if (parameter == null)
		{
			return;
		}
		Stream subStream = GetSubStream(InputStream);
		HttpMultipart httpMultipart = new HttpMultipart(subStream, parameter, ContentEncoding);
		HttpMultipart.Element element;
		while ((element = httpMultipart.ReadNextElement()) != null)
		{
			if (element.Filename == null)
			{
				byte[] array = new byte[element.Length];
				subStream.Position = element.Start;
				subStream.Read(array, 0, (int)element.Length);
				form.Add(element.Name, ContentEncoding.GetString(array));
			}
			else
			{
				HttpPostedFile file = new HttpPostedFile(element.Filename, element.ContentType, subStream, element.Start, element.Length);
				files.AddFile(element.Name, file);
			}
		}
		EndSubStream(subStream);
	}

	private void AddRawKeyValue(StringBuilder key, StringBuilder value)
	{
		string name = HttpUtility.UrlDecode(key.ToString(), ContentEncoding);
		form.Add(name, HttpUtility.UrlDecode(value.ToString(), ContentEncoding));
		key.Length = 0;
		value.Length = 0;
	}

	private void LoadWwwForm()
	{
		using Stream stream = GetSubStream(InputStream);
		using StreamReader streamReader = new StreamReader(stream, ContentEncoding);
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		int num;
		while ((num = streamReader.Read()) != -1)
		{
			switch (num)
			{
			case 61:
				stringBuilder2.Length = 0;
				while ((num = streamReader.Read()) != -1)
				{
					if (num == 38)
					{
						AddRawKeyValue(stringBuilder, stringBuilder2);
						break;
					}
					stringBuilder2.Append((char)num);
				}
				if (num == -1)
				{
					AddRawKeyValue(stringBuilder, stringBuilder2);
					return;
				}
				break;
			case 38:
				AddRawKeyValue(stringBuilder, stringBuilder2);
				break;
			default:
				stringBuilder.Append((char)num);
				break;
			}
		}
		if (num == -1)
		{
			AddRawKeyValue(stringBuilder, stringBuilder2);
		}
		EndSubStream(stream);
	}

	private bool IsContentType(string ct, bool starts_with)
	{
		if (starts_with)
		{
			return StrUtils.StartsWith(ContentType, ct, ignore_case: true);
		}
		return string.Compare(ContentType, ct, ignoreCase: true, Helpers.InvariantCulture) == 0;
	}

	private void DoFilter(byte[] buffer)
	{
		if (input_filter == null || filter == null)
		{
			return;
		}
		if (buffer.Length < 1024)
		{
			buffer = new byte[1024];
		}
		input_filter.BaseStream = input_stream;
		MemoryStream memoryStream = new MemoryStream();
		while (true)
		{
			int num = filter.Read(buffer, 0, buffer.Length);
			if (num <= 0)
			{
				break;
			}
			memoryStream.Write(buffer, 0, num);
		}
		input_stream = new MemoryStream(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, writable: false, publiclyVisible: true);
	}

	private TempFileStream GetTempStream()
	{
		string dynamicBase = AppDomain.CurrentDomain.SetupInformation.DynamicBase;
		TempFileStream tempFileStream = null;
		Random random = new Random();
		do
		{
			int num = random.Next();
			string name = System.IO.Path.Combine(dynamicBase, "tmp" + (num + 1).ToString("x") + ".req");
			try
			{
				tempFileStream = new TempFileStream(name);
			}
			catch (SecurityException)
			{
				throw;
			}
			catch
			{
			}
		}
		while (tempFileStream == null);
		return tempFileStream;
	}

	private void MakeInputStream()
	{
		if (input_stream != null)
		{
			return;
		}
		if (worker_request == null)
		{
			input_stream = new MemoryStream(new byte[0], 0, 0, writable: false, publiclyVisible: true);
			DoFilter(new byte[1024]);
			return;
		}
		int contentLength = ContentLength;
		int num = contentLength / 1024;
		HttpRuntimeSection section = HttpRuntime.Section;
		if (num > section.MaxRequestLength)
		{
			throw HttpException.NewWithCode(400, "Upload size exceeds httpRuntime limit.", 3004);
		}
		int num2 = 0;
		byte[] array = worker_request.GetPreloadedEntityBody();
		if (content_length <= 0 || worker_request.IsEntireEntityBodyIsPreloaded())
		{
			if (array == null || contentLength == 0)
			{
				input_stream = new MemoryStream(new byte[0], 0, 0, writable: false, publiclyVisible: true);
			}
			else
			{
				input_stream = new MemoryStream(array, 0, array.Length, writable: false, publiclyVisible: true);
			}
			DoFilter(new byte[1024]);
			return;
		}
		if (array != null)
		{
			num2 = array.Length;
		}
		if (contentLength > 0 && num >= section.RequestLengthDiskThreshold)
		{
			num2 = Math.Min(contentLength, num2);
			request_file = GetTempStream();
			Stream stream = request_file;
			if (num2 > 0)
			{
				stream.Write(array, 0, num2);
			}
			if (num2 < contentLength)
			{
				array = new byte[Math.Min(contentLength, 32768)];
				do
				{
					int size = Math.Min(contentLength - num2, 32768);
					int num3 = worker_request.ReadEntityBody(array, size);
					if (num3 <= 0)
					{
						break;
					}
					stream.Write(array, 0, num3);
					num2 += num3;
				}
				while (num2 < contentLength);
			}
			request_file.SetReadOnly();
			input_stream = request_file;
		}
		else if (contentLength > 0)
		{
			num2 = Math.Min(contentLength, num2);
			IntPtr intPtr = Marshal.AllocHGlobal(contentLength);
			if (intPtr == (IntPtr)0)
			{
				throw HttpException.NewWithCode($"Not enough memory to allocate {contentLength} bytes.", 3009);
			}
			if (num2 > 0)
			{
				Marshal.Copy(array, 0, intPtr, num2);
			}
			if (num2 < contentLength)
			{
				array = new byte[Math.Min(contentLength, 32768)];
				do
				{
					int size2 = Math.Min(contentLength - num2, 32768);
					int num4 = worker_request.ReadEntityBody(array, size2);
					if (num4 <= 0)
					{
						break;
					}
					Marshal.Copy(array, 0, (IntPtr)((long)intPtr + num2), num4);
					num2 += num4;
				}
				while (num2 < contentLength);
			}
			input_stream = new IntPtrStream(intPtr, num2);
		}
		else
		{
			MemoryStream memoryStream = new MemoryStream();
			Stream stream2 = memoryStream;
			if (num2 > 0)
			{
				memoryStream.Write(array, 0, num2);
			}
			array = new byte[32768];
			long num5 = (long)section.MaxRequestLength * 1024L;
			long num6 = (long)section.RequestLengthDiskThreshold * 1024L;
			while (true)
			{
				int num7 = worker_request.ReadEntityBody(array, 32768);
				if (num7 <= 0)
				{
					break;
				}
				num2 += num7;
				if (num2 < 0 || num2 > num5)
				{
					throw HttpException.NewWithCode(400, "Upload size exceeds httpRuntime limit.", 3004);
				}
				if (memoryStream != null && num2 > num6)
				{
					request_file = GetTempStream();
					memoryStream.WriteTo(request_file);
					memoryStream = null;
					stream2 = request_file;
				}
				stream2.Write(array, 0, num7);
			}
			if (memoryStream != null)
			{
				input_stream = new MemoryStream(memoryStream.GetBuffer(), 0, (int)memoryStream.Length, writable: false, publiclyVisible: true);
			}
			else
			{
				request_file.SetReadOnly();
				input_stream = request_file;
			}
		}
		DoFilter(array);
		if (num2 >= contentLength)
		{
			return;
		}
		throw HttpException.NewWithCode(411, "The request body is incomplete.", 3009);
	}

	internal void ReleaseResources()
	{
		if (input_stream != null)
		{
			Stream stream = input_stream;
			input_stream = null;
			try
			{
				stream.Close();
			}
			catch
			{
			}
		}
		if (request_file != null)
		{
			Stream stream = request_file;
			request_file = null;
			try
			{
				stream.Close();
			}
			catch
			{
			}
		}
	}

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
	/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following: The <see cref="P:System.Web.HttpRequest.Form" /> property.The <see cref="P:System.Web.HttpRequest.Files" /> property.The <see cref="P:System.Web.HttpRequest.InputStream" /> property.The <see cref="M:System.Web.HttpRequest.GetBufferlessInputStream" /> method.To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
	public Stream GetBufferedInputStream()
	{
		return input_stream;
	}

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
	/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following:
	///             <see cref="P:System.Web.HttpRequest.Form" />
	///
	///             <see cref="P:System.Web.HttpRequest.InputStream" />
	///
	///             <see cref="P:System.Web.HttpRequest.Files" />
	///
	///             <see cref="M:System.Web.HttpRequest.GetBufferedInputStream" />
	///           To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
	public Stream GetBufferlessInputStream()
	{
		if (bufferlessInputStream == null)
		{
			if (input_stream != null)
			{
				throw new HttpException("Input stream has already been created");
			}
			bufferlessInputStream = new BufferlessInputStream(this);
		}
		return bufferlessInputStream;
	}

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body, optionally disabling the request-length limit that is set in the <see cref="P:System.Web.Configuration.HttpRuntimeSection.MaxRequestLength" /> property.</summary>
	/// <param name="disableMaxRequestLength">
	///       <see langword="true" /> to disable the request-length limit; otherwise, <see langword="false" />.</param>
	/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
	/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following: The <see cref="P:System.Web.HttpRequest.Form" /> property.The <see cref="P:System.Web.HttpRequest.Files" /> property.The <see cref="P:System.Web.HttpRequest.InputStream" /> property.The <see cref="M:System.Web.HttpRequest.GetBufferedInputStream" /> method.To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
	public Stream GetBufferlessInputStream(bool disableMaxRequestLength)
	{
		return GetBufferlessInputStream();
	}

	/// <summary>Performs a binary read of a specified number of bytes from the current input stream.</summary>
	/// <param name="count">The number of bytes to read. </param>
	/// <returns>A byte array.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="count" /> is 0.- or -
	///         <paramref name="count" /> is greater than the number of bytes available. </exception>
	public byte[] BinaryRead(int count)
	{
		if (count < 0)
		{
			throw new ArgumentException("count is < 0");
		}
		Stream inputStream = InputStream;
		byte[] array = new byte[count];
		if (inputStream.Read(array, 0, count) != count)
		{
			throw new ArgumentException($"count {count} exceeds length of available input {inputStream.Length - inputStream.Position}");
		}
		return array;
	}

	/// <summary>Maps an incoming image-field form parameter to appropriate x-coordinate and y-coordinate values.</summary>
	/// <param name="imageFieldName">The name of the form image map. </param>
	/// <returns>A two-dimensional array of integers.</returns>
	public int[] MapImageCoordinates(string imageFieldName)
	{
		string[] imageCoordinatesParameters = GetImageCoordinatesParameters(imageFieldName);
		if (imageCoordinatesParameters == null)
		{
			return null;
		}
		int[] array = new int[2];
		try
		{
			array[0] = int.Parse(imageCoordinatesParameters[0]);
			array[1] = int.Parse(imageCoordinatesParameters[1]);
			return array;
		}
		catch
		{
			return null;
		}
	}

	/// <summary>Maps an incoming image field form parameter into appropriate x and y coordinate values.</summary>
	/// <param name="imageFieldName">The name of the image field.</param>
	/// <returns>The x and y coordinate values.</returns>
	public double[] MapRawImageCoordinates(string imageFieldName)
	{
		string[] imageCoordinatesParameters = GetImageCoordinatesParameters(imageFieldName);
		if (imageCoordinatesParameters == null)
		{
			return null;
		}
		double[] array = new double[2];
		try
		{
			array[0] = double.Parse(imageCoordinatesParameters[0]);
			array[1] = double.Parse(imageCoordinatesParameters[1]);
			return array;
		}
		catch
		{
			return null;
		}
	}

	private string[] GetImageCoordinatesParameters(string imageFieldName)
	{
		string httpMethod = HttpMethod;
		NameValueCollection nameValueCollection = null;
		switch (httpMethod)
		{
		case "HEAD":
		case "GET":
			nameValueCollection = QueryString;
			break;
		case "POST":
			nameValueCollection = Form;
			break;
		}
		if (nameValueCollection == null)
		{
			return null;
		}
		string text = nameValueCollection[imageFieldName + ".x"];
		if (text == null || text == "")
		{
			return null;
		}
		string text2 = nameValueCollection[imageFieldName + ".y"];
		if (text2 == null || text2 == "")
		{
			return null;
		}
		return new string[2] { text, text2 };
	}

	/// <summary>Maps the specified virtual path to a physical path.</summary>
	/// <param name="virtualPath">The virtual path (absolute or relative) for the current request. </param>
	/// <returns>The physical path on the server specified by <paramref name="virtualPath" />.</returns>
	/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.HttpContext" /> object is defined for the request. </exception>
	public string MapPath(string virtualPath)
	{
		if (worker_request == null)
		{
			return null;
		}
		return MapPath(virtualPath, BaseVirtualDir, allowCrossAppMapping: true);
	}

	/// <summary>Maps the specified virtual path to a physical path.</summary>
	/// <param name="virtualPath">The virtual path (absolute or relative) for the current request. </param>
	/// <param name="baseVirtualDir">The virtual base directory path used for relative resolution. </param>
	/// <param name="allowCrossAppMapping">
	///       <see langword="true" /> to indicate that <paramref name="virtualPath" /> may belong to another application; otherwise, <see langword="false" />. </param>
	/// <returns>The physical path on the server.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="allowCrossMapping" /> is <see langword="false" /> and <paramref name="virtualPath" /> belongs to another application. </exception>
	/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.HttpContext" /> object is defined for the request. </exception>
	public string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
	{
		if (worker_request == null)
		{
			throw HttpException.NewWithCode("No HttpWorkerRequest", 3001);
		}
		if (virtualPath == null)
		{
			virtualPath = "~";
		}
		else
		{
			virtualPath = virtualPath.Trim();
			if (virtualPath.Length == 0)
			{
				virtualPath = "~";
			}
		}
		if (!VirtualPathUtility.IsValidVirtualPath(virtualPath))
		{
			throw HttpException.NewWithCode($"'{virtualPath}' is not a valid virtual path.", 3001);
		}
		string appDomainAppVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
		if (!VirtualPathUtility.IsRooted(virtualPath))
		{
			if (StrUtils.IsNullOrEmpty(baseVirtualDir))
			{
				baseVirtualDir = appDomainAppVirtualPath;
			}
			virtualPath = VirtualPathUtility.Combine(VirtualPathUtility.AppendTrailingSlash(baseVirtualDir), virtualPath);
			if (!VirtualPathUtility.IsAbsolute(virtualPath))
			{
				virtualPath = VirtualPathUtility.ToAbsolute(virtualPath, normalize: false);
			}
		}
		else if (!VirtualPathUtility.IsAbsolute(virtualPath))
		{
			virtualPath = VirtualPathUtility.ToAbsolute(virtualPath, normalize: false);
		}
		bool num = string.Compare(virtualPath, appDomainAppVirtualPath, RuntimeHelpers.StringComparison) == 0;
		appDomainAppVirtualPath = VirtualPathUtility.AppendTrailingSlash(appDomainAppVirtualPath);
		if (!allowCrossAppMapping)
		{
			if (!StrUtils.StartsWith(virtualPath, appDomainAppVirtualPath, ignore_case: true))
			{
				throw new ArgumentException("MapPath: Mapping across applications not allowed");
			}
			if (appDomainAppVirtualPath.Length > 1 && virtualPath.Length > 1 && virtualPath[0] != '/')
			{
				throw HttpException.NewWithCode("MapPath: Mapping across applications not allowed", 3001);
			}
		}
		if (!num && !virtualPath.StartsWith(appDomainAppVirtualPath, RuntimeHelpers.StringComparison))
		{
			throw new InvalidOperationException($"Failed to map path '{virtualPath}'");
		}
		string text = worker_request.MapPath(virtualPath);
		if (virtualPath[virtualPath.Length - 1] != '/' && text[text.Length - 1] == System.IO.Path.DirectorySeparatorChar)
		{
			text = text.TrimEnd(System.IO.Path.DirectorySeparatorChar);
		}
		return text;
	}

	/// <summary>Saves an HTTP request to disk.</summary>
	/// <param name="filename">The physical drive path. </param>
	/// <param name="includeHeaders">A Boolean value specifying whether an HTTP header should be saved to disk. </param>
	/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.Configuration.HttpRuntimeSection.RequireRootedSaveAsPath" /> property of the <see cref="T:System.Web.Configuration.HttpRuntimeSection" /> is set to <see langword="true" /> but <paramref name="filename" /> is not an absolute path.</exception>
	public void SaveAs(string filename, bool includeHeaders)
	{
		Stream stream = new FileStream(filename, FileMode.Create);
		if (includeHeaders)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = string.Empty;
			string text2 = "/";
			if (worker_request != null)
			{
				text = worker_request.GetHttpVersion();
				text2 = UrlComponents.Path;
			}
			string query = UrlComponents.Query;
			stringBuilder.AppendFormat("{0} {1}{2} {3}\r\n", HttpMethod, text2, query, text);
			NameValueCollection nameValueCollection = Headers;
			string[] allKeys = nameValueCollection.AllKeys;
			foreach (string text3 in allKeys)
			{
				stringBuilder.Append(text3);
				stringBuilder.Append(':');
				stringBuilder.Append(nameValueCollection[text3]);
				stringBuilder.Append("\r\n");
			}
			stringBuilder.Append("\r\n");
			byte[] bytes = Encoding.GetEncoding(28591).GetBytes(stringBuilder.ToString());
			stream.Write(bytes, 0, bytes.Length);
		}
		Stream subStream = GetSubStream(InputStream);
		try
		{
			long num = subStream.Length;
			int num2 = (int)Math.Min((num < 0) ? 0 : num, 8192L);
			byte[] buffer = new byte[num2];
			int num3 = 0;
			while (num > 0 && (num3 = subStream.Read(buffer, 0, num2)) > 0)
			{
				stream.Write(buffer, 0, num3);
				num -= num3;
			}
		}
		finally
		{
			stream.Flush();
			stream.Close();
			EndSubStream(subStream);
		}
	}

	/// <summary>Causes validation to occur for the collections accessed through the <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.Form" />, and <see cref="P:System.Web.HttpRequest.QueryString" /> properties.</summary>
	/// <exception cref="T:System.Web.HttpRequestValidationException">Potentially dangerous data was received from the client. </exception>
	public void ValidateInput()
	{
		validate_cookies = true;
		validate_query_string = true;
		validate_form = true;
		inputValidationEnabled = true;
	}

	internal void Validate()
	{
		HttpRuntimeSection section = HttpRuntime.Section;
		string query = UrlComponents.Query;
		if (query != null && query.Length > section.MaxQueryStringLength)
		{
			throw new HttpException(400, "The length of the query string for this request exceeds the configured maxQueryStringLength value.");
		}
		string pathNoValidation = PathNoValidation;
		if (pathNoValidation != null)
		{
			if (pathNoValidation.Length > section.MaxUrlLength)
			{
				throw new HttpException(400, "The length of the URL for this request exceeds the configured maxUrlLength value.");
			}
			char[] requestPathInvalidCharacters = RequestPathInvalidCharacters;
			if (requestPathInvalidCharacters != null)
			{
				int num = pathNoValidation.IndexOfAny(requestPathInvalidCharacters);
				if (num != -1)
				{
					throw HttpException.NewWithCode($"A potentially dangerous Request.Path value was detected from the client ({pathNoValidation[num]}).", 3003);
				}
			}
		}
		if (validateRequestNewMode)
		{
			ValidateInput();
		}
	}

	internal void SetFilePath(string path)
	{
		file_path = path;
		physical_path = null;
		original_path = null;
	}

	internal void SetCurrentExePath(string path)
	{
		cached_url = null;
		current_exe_path = path;
		UrlComponents.Path = path + PathInfo;
		root_virtual_dir = null;
		base_virtual_dir = null;
		physical_path = null;
		unescaped_path = null;
		original_path = null;
	}

	internal void SetPathInfo(string pi)
	{
		cached_url = null;
		path_info = pi;
		original_path = null;
		string path = UrlComponents.Path;
		UrlComponents.Path = path + PathInfo;
	}

	internal void SetFormCollection(WebROCollection coll, bool lazyValidation)
	{
		if (coll != null)
		{
			form = coll;
			lazyFormValidation = lazyValidation;
		}
	}

	internal void SetQueryStringCollection(WebROCollection coll, bool lazyValidation)
	{
		if (coll != null)
		{
			query_string_nvc = coll;
			lazyQueryStringValidation = lazyValidation;
		}
	}

	internal void SetHeader(string name, string value)
	{
		WebROCollection obj = (WebROCollection)Headers;
		obj.Unprotect();
		obj[name] = value;
		obj.Protect();
	}

	internal void SetForm(WebROCollection coll)
	{
		form = coll;
	}

	private static void ValidateNameValueCollection(string name, NameValueCollection coll)
	{
		if (coll == null)
		{
			return;
		}
		foreach (string key in coll.Keys)
		{
			string text2 = coll[key];
			if (text2 != null && text2.Length > 0 && IsInvalidString(text2))
			{
				ThrowValidationException(name, key, text2);
			}
		}
	}

	private static void ValidateNameValueCollection(string name, NameValueCollection coll, RequestValidationSource source)
	{
		if (coll == null)
		{
			return;
		}
		RequestValidator current = RequestValidator.Current;
		HttpContext current2 = HttpContext.Current;
		foreach (string key in coll.Keys)
		{
			string text2 = coll[key];
			if (text2 != null && text2.Length > 0 && !current.IsValidRequestString(current2, text2, source, key, out var _))
			{
				ThrowValidationException(name, key, text2);
			}
		}
	}

	/// <summary>Provides IIS with a copy of the HTTP request entity body.</summary>
	/// <exception cref="T:System.PlatformNotSupportedException">The method was invoked on a version of IIS earlier than IIS 7.0. </exception>
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
	public void InsertEntityBody()
	{
		throw new PlatformNotSupportedException("This method is not supported.");
	}

	/// <summary>Provides IIS with a copy of the HTTP request entity body and with information about the request entity object.</summary>
	/// <param name="buffer">An array that contains the request entity data.</param>
	/// <param name="offset">The zero-based position in <paramref name="buffer" /> at which to begin storing the request entity data.</param>
	/// <param name="count">The number of bytes to read into the <paramref name="buffer" /> array.</param>
	/// <exception cref="T:System.PlatformNotSupportedException">The method was invoked on a version of IIS earlier than IIS 7.0. </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="buffer" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="count" /> is a negative value. </exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="offset" /> is a negative value.</exception>
	/// <exception cref="T:System.ArgumentException">The number of items in <paramref name="count" /> is larger than the available space in <paramref name="buffer" />, given the <paramref name="offset" /> value.</exception>
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.High)]
	public void InsertEntityBody(byte[] buffer, int offset, int count)
	{
		throw new PlatformNotSupportedException("This method is not supported.");
	}

	private static void ValidateCookieCollection(HttpCookieCollection cookies)
	{
		if (cookies == null)
		{
			return;
		}
		int count = cookies.Count;
		RequestValidator current = RequestValidator.Current;
		HttpContext current2 = HttpContext.Current;
		for (int i = 0; i < count; i++)
		{
			HttpCookie httpCookie = cookies[i];
			if (httpCookie != null)
			{
				string value = httpCookie.Value;
				string name = httpCookie.Name;
				if (!string.IsNullOrEmpty(value) && ((!validateRequestNewMode) ? IsInvalidString(value) : (!current.IsValidRequestString(current2, value, RequestValidationSource.Cookies, name, out var _))))
				{
					ThrowValidationException("Cookies", name, value);
				}
			}
		}
	}

	private static void ThrowValidationException(string name, string key, string value)
	{
		string text = "\"" + value + "\"";
		if (text.Length > 20)
		{
			text = text.Substring(0, 16) + "...\"";
		}
		throw new HttpRequestValidationException($"A potentially dangerous Request.{name} value was detected from the client ({key}={text}).");
	}

	internal static void ValidateString(string key, string value, RequestValidationSource source)
	{
		if (!string.IsNullOrEmpty(value) && IsInvalidString(value, out var _))
		{
			ThrowValidationException(source.ToString(), key, value);
		}
	}

	internal static bool IsInvalidString(string val)
	{
		int validationFailureIndex;
		return IsInvalidString(val, out validationFailureIndex);
	}

	internal static bool IsInvalidString(string val, out int validationFailureIndex)
	{
		validationFailureIndex = 0;
		int length = val.Length;
		if (length < 2)
		{
			return false;
		}
		char c = val[0];
		for (int i = 1; i < length; i++)
		{
			char c2 = val[i];
			switch (c)
			{
			case '<':
			case '＜':
				if (c2 == '!' || c2 < ' ' || (c2 >= 'a' && c2 <= 'z') || (c2 >= 'A' && c2 <= 'Z'))
				{
					validationFailureIndex = i - 1;
					return true;
				}
				break;
			case '&':
				if (c2 == '#')
				{
					validationFailureIndex = i - 1;
					return true;
				}
				break;
			}
			c = c2;
		}
		return false;
	}

	private static IPAddress[] GetLocalHostAddresses()
	{
		try
		{
			return Dns.GetHostAddresses(Dns.GetHostName());
		}
		catch
		{
			return new IPAddress[0];
		}
	}
}
