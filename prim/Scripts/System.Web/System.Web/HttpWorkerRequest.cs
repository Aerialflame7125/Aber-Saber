using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web;

/// <summary>
///     This abstract class defines the base worker methods and enumerations used by ASP.NET managed code to process requests. </summary>
[ComVisible(false)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class HttpWorkerRequest
{
	/// <summary>Represents the method that Notifies callers when sending of the response is complete.</summary>
	/// <param name="wr">The current <see cref="T:System.Web.HttpWorkerRequest" />. </param>
	/// <param name="extraData">Any additional data needed to process the request. </param>
	public delegate void EndOfSendNotification(HttpWorkerRequest wr, object extraData);

	/// <summary>The index that represents the HTTP <see langword="Cache-Control" /> HTTP header.</summary>
	public const int HeaderCacheControl = 0;

	/// <summary>Specifies the index number for the <see langword="Connection" /> HTTP header.</summary>
	public const int HeaderConnection = 1;

	/// <summary>Specifies the index number for the <see langword="Date" /> HTTP header.</summary>
	public const int HeaderDate = 2;

	/// <summary>Specifies the index number for the <see langword="Keep-Alive" /> HTTP header.</summary>
	public const int HeaderKeepAlive = 3;

	/// <summary>Specifies the index number for the <see langword="Pragma" /> HTTP header.</summary>
	public const int HeaderPragma = 4;

	/// <summary>Specifies the index number for the <see langword="Trailer" /> HTTP header.</summary>
	public const int HeaderTrailer = 5;

	/// <summary>Specifies the index number for the <see langword="Transfer-Encoding" /> HTTP header.</summary>
	public const int HeaderTransferEncoding = 6;

	/// <summary>Specifies the index number for the <see langword="Upgrade" /> HTTP header.</summary>
	public const int HeaderUpgrade = 7;

	/// <summary>Specifies the index number for the <see langword="Via" /> HTTP header.</summary>
	public const int HeaderVia = 8;

	/// <summary>Specifies the index number for the <see langword="Warning" /> HTTP header.</summary>
	public const int HeaderWarning = 9;

	/// <summary>Specifies the index number for the <see langword="Allow" /> HTTP header.</summary>
	public const int HeaderAllow = 10;

	/// <summary>Specifies the index number for the <see langword="Content-Length" /> HTTP header.</summary>
	public const int HeaderContentLength = 11;

	/// <summary>Specifies the index number for the <see langword="Content-Type" /> HTTP header.</summary>
	public const int HeaderContentType = 12;

	/// <summary>Specifies the index number for the <see langword="Content-Encoding" /> HTTP header.</summary>
	public const int HeaderContentEncoding = 13;

	/// <summary>Specifies the index number for the <see langword="Content-Language" /> HTTP header.</summary>
	public const int HeaderContentLanguage = 14;

	/// <summary>Specifies the index number for the <see langword="Content-Location" /> HTTP header.</summary>
	public const int HeaderContentLocation = 15;

	/// <summary>Specifies the index number for the <see langword="Content-MD5" /> HTTP header.</summary>
	public const int HeaderContentMd5 = 16;

	/// <summary>Specifies the index number for the <see langword="Content-Range" /> HTTP header.</summary>
	public const int HeaderContentRange = 17;

	/// <summary>Specifies the index number for the <see langword="Expires" /> HTTP header.</summary>
	public const int HeaderExpires = 18;

	/// <summary>Specifies the index number for the <see langword="Last-Modified" /> HTTP header.</summary>
	public const int HeaderLastModified = 19;

	/// <summary>Specifies the index number for the <see langword="Accept" /> HTTP header.</summary>
	public const int HeaderAccept = 20;

	/// <summary>Specifies the index number for the <see langword="Accept-Charset" /> HTTP header.</summary>
	public const int HeaderAcceptCharset = 21;

	/// <summary>Specifies the index number for the <see langword="Accept-Encoding" /> HTTP header.</summary>
	public const int HeaderAcceptEncoding = 22;

	/// <summary>Specifies the index number for the <see langword="Accept-Language" /> HTTP header.</summary>
	public const int HeaderAcceptLanguage = 23;

	/// <summary>Specifies the index number for the <see langword="Authorization" /> HTTP header.</summary>
	public const int HeaderAuthorization = 24;

	/// <summary>Specifies the index number for the <see langword="Cookie" /> HTTP header.</summary>
	public const int HeaderCookie = 25;

	/// <summary>Specifies the index number for the <see langword="Except" /> HTTP header.</summary>
	public const int HeaderExpect = 26;

	/// <summary>Specifies the index number for the <see langword="From" /> HTTP header.</summary>
	public const int HeaderFrom = 27;

	/// <summary>Specifies the index number for the <see langword="Host" /> HTTP header.</summary>
	public const int HeaderHost = 28;

	/// <summary>Specifies the index number for the <see langword="If-Match" /> HTTP header.</summary>
	public const int HeaderIfMatch = 29;

	/// <summary>Specifies the index number for the <see langword="If-Modified-Since" /> HTTP header.</summary>
	public const int HeaderIfModifiedSince = 30;

	/// <summary>Specifies the index number for the <see langword="If-None-Match" /> HTTP header.</summary>
	public const int HeaderIfNoneMatch = 31;

	/// <summary>Specifies the index number for the <see langword="If-Range" /> HTTP header.</summary>
	public const int HeaderIfRange = 32;

	/// <summary>Specifies the index number for the <see langword="If-Unmodified-Since" /> HTTP header.</summary>
	public const int HeaderIfUnmodifiedSince = 33;

	/// <summary>Specifies the index number for the <see langword="Max-Forwards" /> HTTP header.</summary>
	public const int HeaderMaxForwards = 34;

	/// <summary>Specifies the index number for the <see langword="Proxy-Authorization" /> HTTP header.</summary>
	public const int HeaderProxyAuthorization = 35;

	/// <summary>Specifies the index number for the <see langword="Referer" /> HTTP header.</summary>
	public const int HeaderReferer = 36;

	/// <summary>Specifies the index number for the <see langword="Range" /> HTTP header.</summary>
	public const int HeaderRange = 37;

	/// <summary>Specifies the index number for the <see langword="TE" /> HTTP header.</summary>
	public const int HeaderTe = 38;

	/// <summary>Specifies the index number for the <see langword="User-Agent" /> HTTP header.</summary>
	public const int HeaderUserAgent = 39;

	/// <summary>Specifies the index number for the <see langword="Maximum" /> HTTP request header.</summary>
	public const int RequestHeaderMaximum = 40;

	/// <summary>Specifies the index number for the <see langword="Accept-Ranges" /> HTTP header.</summary>
	public const int HeaderAcceptRanges = 20;

	/// <summary>Specifies the index number for the <see langword="Age" /> HTTP header.</summary>
	public const int HeaderAge = 21;

	/// <summary>Specifies the index number for the <see langword="ETag" /> HTTP header.</summary>
	public const int HeaderEtag = 22;

	/// <summary>Specifies the index number for the <see langword="Location" /> HTTP header.</summary>
	public const int HeaderLocation = 23;

	/// <summary>Specifies the index number for the <see langword="Proxy-Authenticate" /> HTTP header.</summary>
	public const int HeaderProxyAuthenticate = 24;

	/// <summary>Specifies the index number for the <see langword="Retry-After" /> HTTP header.</summary>
	public const int HeaderRetryAfter = 25;

	/// <summary>Specifies the index number for the <see langword="Server" /> HTTP header.</summary>
	public const int HeaderServer = 26;

	/// <summary>Specifies the index number for the <see langword="Set-Cookie" /> HTTP header.</summary>
	public const int HeaderSetCookie = 27;

	/// <summary>Specifies the index number for the <see langword="Vary" /> HTTP header.</summary>
	public const int HeaderVary = 28;

	/// <summary>Specifies the index number for the <see langword="WWW-Authenticate" /> HTTP header.</summary>
	public const int HeaderWwwAuthenticate = 29;

	/// <summary>Specifies the index number for the <see langword="Maximum" /> HTTP response header.</summary>
	public const int ResponseHeaderMaximum = 30;

	/// <summary>Specifies a reason for the request.</summary>
	public const int ReasonResponseCacheMiss = 0;

	/// <summary>Specifies a reason for the request.</summary>
	public const int ReasonFileHandleCacheMiss = 1;

	/// <summary>Specifies a reason for the request.</summary>
	public const int ReasonCachePolicy = 2;

	/// <summary>Specifies a reason for the request.</summary>
	public const int ReasonCacheSecurity = 3;

	/// <summary>Specifies a reason for the request.</summary>
	public const int ReasonClientDisconnect = 4;

	/// <summary>Specifies a reason for the request. The default value is <see cref="F:System.Web.HttpWorkerRequest.ReasonResponseCacheMiss" />.</summary>
	public const int ReasonDefault = 0;

	private static readonly Dictionary<string, int> RequestHeaderIndexer;

	private static readonly Dictionary<string, int> ResponseHeaderIndexer;

	private bool started_internally;

	internal bool StartedInternally
	{
		get
		{
			return started_internally;
		}
		set
		{
			started_internally = value;
		}
	}

	/// <summary>Gets the full physical path to the Machine.config file.</summary>
	/// <returns>The physical path to the Machine.config file.</returns>
	public virtual string MachineConfigPath => null;

	/// <summary>Gets the physical path to the directory where the ASP.NET binaries are installed.</summary>
	/// <returns>The physical directory to the ASP.NET binary files.</returns>
	public virtual string MachineInstallDirectory => null;

	/// <summary>Gets the corresponding Event Tracking for Windows trace ID for the current request.</summary>
	/// <returns>A trace ID for the current ASP.NET request.</returns>
	public virtual Guid RequestTraceIdentifier => Guid.Empty;

	/// <summary>Gets the full physical path to the root Web.config file.</summary>
	/// <returns>The physical path to the root Web.config file.</returns>
	public virtual string RootWebConfigPath => null;

	static HttpWorkerRequest()
	{
		RequestHeaderIndexer = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
		for (int i = 0; i < 40; i++)
		{
			RequestHeaderIndexer.Add(GetKnownRequestHeaderName(i), i);
		}
		ResponseHeaderIndexer = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
		for (int j = 0; j < 30; j++)
		{
			ResponseHeaderIndexer.Add(GetKnownResponseHeaderName(j), j);
		}
	}

	/// <summary>Terminates the connection with the client.</summary>
	public virtual void CloseConnection()
	{
	}

	/// <summary>Returns the virtual path to the currently executing server application.</summary>
	/// <returns>The virtual path of the current application.</returns>
	public virtual string GetAppPath()
	{
		return null;
	}

	/// <summary>Returns the physical path to the currently executing server application.</summary>
	/// <returns>The physical path of the current application.</returns>
	public virtual string GetAppPathTranslated()
	{
		return null;
	}

	/// <summary>When overridden in a derived class, returns the application pool ID for the current URL.</summary>
	/// <returns>Always returns null.</returns>
	public virtual string GetAppPoolID()
	{
		return null;
	}

	/// <summary>Gets the number of bytes read in from the client.</summary>
	/// <returns>A <see langword="Long" /> containing the number of bytes read.</returns>
	public virtual long GetBytesRead()
	{
		return 0L;
	}

	/// <summary>When overridden in a derived class, returns the virtual path to the requested URI.</summary>
	/// <returns>The path to the requested URI.</returns>
	public virtual string GetFilePath()
	{
		return null;
	}

	/// <summary>Returns the physical file path to the requested URI (and translates it from virtual path to physical path: for example, "/proj1/page.aspx" to "c:\dir\page.aspx") </summary>
	/// <returns>The translated physical file path to the requested URI.</returns>
	public virtual string GetFilePathTranslated()
	{
		return null;
	}

	/// <summary>Returns the standard HTTP request header that corresponds to the specified index.</summary>
	/// <param name="index">The index of the header. For example, the <see cref="F:System.Web.HttpWorkerRequest.HeaderAllow" /> field. </param>
	/// <returns>The HTTP request header.</returns>
	public virtual string GetKnownRequestHeader(int index)
	{
		return null;
	}

	/// <summary>Returns additional path information for a resource with a URL extension. That is, for the path /virdir/page.html/tail, the <see langword="GetPathInfo" /> value is /tail.</summary>
	/// <returns>Additional path information for a resource.</returns>
	public virtual string GetPathInfo()
	{
		return "";
	}

	/// <summary>Returns the portion of the HTTP request body that has already been read.</summary>
	/// <returns>The portion of the HTTP request body that has been read.</returns>
	public virtual byte[] GetPreloadedEntityBody()
	{
		return null;
	}

	/// <summary>Gets the portion of the HTTP request body that has currently been read by using the specified buffer data and byte offset.</summary>
	/// <param name="buffer">The data to read.</param>
	/// <param name="offset">The byte offset at which to begin reading.</param>
	/// <returns>The portion of the HTTP request body that has been read.</returns>
	public virtual int GetPreloadedEntityBody(byte[] buffer, int offset)
	{
		return 0;
	}

	/// <summary>Gets the length of the portion of the HTTP request body that has currently been read.</summary>
	/// <returns>An integer containing the length of the currently read HTTP request body.</returns>
	public virtual int GetPreloadedEntityBodyLength()
	{
		return 0;
	}

	/// <summary>When overridden in a derived class, returns the HTTP protocol (HTTP or HTTPS).</summary>
	/// <returns>HTTPS if the <see cref="M:System.Web.HttpWorkerRequest.IsSecure" /> method is <see langword="true" />, otherwise HTTP.</returns>
	public virtual string GetProtocol()
	{
		if (IsSecure())
		{
			return "https";
		}
		return "http";
	}

	/// <summary>When overridden in a derived class, returns the response query string as an array of bytes.</summary>
	/// <returns>An array of bytes containing the response.</returns>
	public virtual byte[] GetQueryStringRawBytes()
	{
		return null;
	}

	/// <summary>When overridden in a derived class, returns the name of the client computer.</summary>
	/// <returns>The name of the client computer.</returns>
	public virtual string GetRemoteName()
	{
		return GetRemoteAddress();
	}

	/// <summary>When overridden in a derived class, returns the reason for the request.</summary>
	/// <returns>Reason code. The default is <see langword="ReasonResponseCacheMiss" />.</returns>
	public virtual int GetRequestReason()
	{
		return 0;
	}

	/// <summary>When overridden in a derived class, returns the name of the local server.</summary>
	/// <returns>The name of the local server.</returns>
	public virtual string GetServerName()
	{
		return GetLocalAddress();
	}

	/// <summary>Returns a single server variable from a dictionary of server variables associated with the request.</summary>
	/// <param name="name">The name of the requested server variable. </param>
	/// <returns>The requested server variable.</returns>
	public virtual string GetServerVariable(string name)
	{
		return null;
	}

	/// <summary>Gets the length of the entire HTTP request body.</summary>
	/// <returns>An integer containing the length of the entire HTTP request body.</returns>
	public virtual int GetTotalEntityBodyLength()
	{
		return 0;
	}

	/// <summary>Returns a nonstandard HTTP request header value.</summary>
	/// <param name="name">The header name. </param>
	/// <returns>The header value.</returns>
	public virtual string GetUnknownRequestHeader(string name)
	{
		return null;
	}

	/// <summary>Get all nonstandard HTTP header name-value pairs.</summary>
	/// <returns>An array of header name-value pairs.</returns>
	[CLSCompliant(false)]
	public virtual string[][] GetUnknownRequestHeaders()
	{
		return null;
	}

	/// <summary>When overridden in a derived class, returns the client's impersonation token.</summary>
	/// <returns>A value representing the client's impersonation token. The default is 0.</returns>
	public virtual IntPtr GetUserToken()
	{
		return IntPtr.Zero;
	}

	/// <summary>Returns a value indicating whether the request contains body data.</summary>
	/// <returns>
	///     <see langword="true" /> if the request contains body data; otherwise, <see langword="false" />.</returns>
	public bool HasEntityBody()
	{
		return false;
	}

	/// <summary>Returns a value indicating whether HTTP response headers have been sent to the client for the current request.</summary>
	/// <returns>
	///     <see langword="true" /> if HTTP response headers have been sent to the client; otherwise, <see langword="false" />.</returns>
	public virtual bool HeadersSent()
	{
		return true;
	}

	/// <summary>Returns a value indicating whether the client connection is still active.</summary>
	/// <returns>
	///     <see langword="true" /> if the client connection is still active; otherwise, <see langword="false" />.</returns>
	public virtual bool IsClientConnected()
	{
		return true;
	}

	/// <summary>Returns a value indicating whether all request data is available and no further reads from the client are required.</summary>
	/// <returns>
	///     <see langword="true" /> if all request data is available; otherwise, <see langword="false" />.</returns>
	public virtual bool IsEntireEntityBodyIsPreloaded()
	{
		return false;
	}

	/// <summary>Returns a value indicating whether the connection uses SSL.</summary>
	/// <returns>
	///     <see langword="true" /> if the connection is an SSL connection; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public virtual bool IsSecure()
	{
		return false;
	}

	/// <summary>Returns the physical path corresponding to the specified virtual path.</summary>
	/// <param name="virtualPath">The virtual path. </param>
	/// <returns>The physical path that corresponds to the virtual path specified in the <paramref name="virtualPath" /> parameter.</returns>
	public virtual string MapPath(string virtualPath)
	{
		return null;
	}

	/// <summary>Reads request data from the client (when not preloaded).</summary>
	/// <param name="buffer">The byte array to read data into. </param>
	/// <param name="size">The maximum number of bytes to read. </param>
	/// <returns>The number of bytes read.</returns>
	public virtual int ReadEntityBody(byte[] buffer, int size)
	{
		return 0;
	}

	/// <summary>Reads request data from the client (when not preloaded) by using the specified buffer to read from, byte offset, and maximum bytes.</summary>
	/// <param name="buffer">The byte array to read data into.</param>
	/// <param name="offset">The byte offset at which to begin reading.</param>
	/// <param name="size">The maximum number of bytes to read.</param>
	/// <returns>The number of bytes read.</returns>
	public virtual int ReadEntityBody(byte[] buffer, int offset, int size)
	{
		byte[] array = new byte[size];
		int num = ReadEntityBody(array, size);
		if (num > 0)
		{
			Array.Copy(array, 0, buffer, offset, num);
		}
		return num;
	}

	/// <summary>Adds a <see langword="Content-Length" /> HTTP header to the response for message bodies that are greater than 2 GB.</summary>
	/// <param name="contentLength">The length of the response, in bytes.</param>
	public virtual void SendCalculatedContentLength(long contentLength)
	{
		SendCalculatedContentLength((int)contentLength);
	}

	/// <summary>Adds a <see langword="Content-Length" /> HTTP header to the response for message bodies that are less than or equal to 2 GB.</summary>
	/// <param name="contentLength">The length of the response, in bytes.</param>
	public virtual void SendCalculatedContentLength(int contentLength)
	{
	}

	/// <summary>Adds the specified number of bytes from a block of memory to the response.</summary>
	/// <param name="data">An unmanaged pointer to the block of memory. </param>
	/// <param name="length">The number of bytes to send. </param>
	public virtual void SendResponseFromMemory(IntPtr data, int length)
	{
		if (data != IntPtr.Zero)
		{
			byte[] array = new byte[length];
			Marshal.Copy(data, array, 0, length);
			SendResponseFromMemory(array, length);
		}
	}

	/// <summary>Registers for an optional notification when all the response data is sent.</summary>
	/// <param name="callback">The notification callback that is called when all data is sent (out-of-band). </param>
	/// <param name="extraData">An additional parameter to the callback. </param>
	public virtual void SetEndOfSendNotification(EndOfSendNotification callback, object extraData)
	{
	}

	/// <summary>Used by the runtime to notify the <see cref="T:System.Web.HttpWorkerRequest" /> that request processing for the current request is complete.</summary>
	public abstract void EndOfRequest();

	/// <summary>Sends all pending response data to the client.</summary>
	/// <param name="finalFlush">
	///       <see langword="true" /> if this is the last time response data will be flushed; otherwise, <see langword="false" />. </param>
	public abstract void FlushResponse(bool finalFlush);

	/// <summary>Returns the specified member of the request header.</summary>
	/// <returns>The HTTP verb returned in the request header.</returns>
	public abstract string GetHttpVerbName();

	/// <summary>Provides access to the HTTP version of the request (for example, "HTTP/1.1").</summary>
	/// <returns>The HTTP version returned in the request header.</returns>
	public abstract string GetHttpVersion();

	/// <summary>Provides access to the specified member of the request header.</summary>
	/// <returns>The server IP address returned in the request header.</returns>
	public abstract string GetLocalAddress();

	/// <summary>Provides access to the specified member of the request header.</summary>
	/// <returns>The server port number returned in the request header.</returns>
	public abstract int GetLocalPort();

	/// <summary>Returns the query string specified in the request URL.</summary>
	/// <returns>The request query string.</returns>
	public abstract string GetQueryString();

	/// <summary>Returns the URL path contained in the request header with the query string appended.</summary>
	/// <returns>The raw URL path of the request header.</returns>
	public abstract string GetRawUrl();

	/// <summary>Provides access to the specified member of the request header.</summary>
	/// <returns>The client's IP address.</returns>
	public abstract string GetRemoteAddress();

	/// <summary>Provides access to the specified member of the request header.</summary>
	/// <returns>The client's HTTP port number.</returns>
	public abstract int GetRemotePort();

	/// <summary>Returns the virtual path to the requested URI.</summary>
	/// <returns>The path to the requested URI.</returns>
	public abstract string GetUriPath();

	/// <summary>Adds a standard HTTP header to the response.</summary>
	/// <param name="index">The header index. For example, <see cref="F:System.Web.HttpWorkerRequest.HeaderContentLength" />. </param>
	/// <param name="value">The value of the header. </param>
	public abstract void SendKnownResponseHeader(int index, string value);

	/// <summary>Adds the contents of the specified file to the response and specifies the starting position in the file and the number of bytes to send.</summary>
	/// <param name="handle">The handle of the file to send. </param>
	/// <param name="offset">The starting position in the file. </param>
	/// <param name="length">The number of bytes to send. </param>
	public abstract void SendResponseFromFile(IntPtr handle, long offset, long length);

	/// <summary>Adds the contents of the specified file to the response and specifies the starting position in the file and the number of bytes to send.</summary>
	/// <param name="filename">The name of the file to send. </param>
	/// <param name="offset">The starting position in the file. </param>
	/// <param name="length">The number of bytes to send. </param>
	public abstract void SendResponseFromFile(string filename, long offset, long length);

	/// <summary>Adds the specified number of bytes from a byte array to the response.</summary>
	/// <param name="data">The byte array to send. </param>
	/// <param name="length">The number of bytes to send, starting at the first byte. </param>
	public abstract void SendResponseFromMemory(byte[] data, int length);

	/// <summary>Specifies the HTTP status code and status description of the response, such as SendStatus(200, "Ok").</summary>
	/// <param name="statusCode">The status code to send </param>
	/// <param name="statusDescription">The status description to send. </param>
	public abstract void SendStatus(int statusCode, string statusDescription);

	/// <summary>Adds a nonstandard HTTP header to the response.</summary>
	/// <param name="name">The name of the header to send. </param>
	/// <param name="value">The value of the header. </param>
	public abstract void SendUnknownResponseHeader(string name, string value);

	/// <summary>Returns the index number of the specified HTTP request header.</summary>
	/// <param name="header">The name of the header. </param>
	/// <returns>The index number of the HTTP request header specified in the <paramref name="header" /> parameter.</returns>
	public static int GetKnownRequestHeaderIndex(string header)
	{
		if (RequestHeaderIndexer.TryGetValue(header, out var value))
		{
			return value;
		}
		return -1;
	}

	/// <summary>Returns the name of the specified HTTP request header.</summary>
	/// <param name="index">The index number of the header. </param>
	/// <returns>The name of the HTTP request header specified in the <paramref name="index" /> parameter.</returns>
	public static string GetKnownRequestHeaderName(int index)
	{
		return index switch
		{
			0 => "Cache-Control", 
			1 => "Connection", 
			2 => "Date", 
			3 => "Keep-Alive", 
			4 => "Pragma", 
			5 => "Trailer", 
			6 => "Transfer-Encoding", 
			7 => "Upgrade", 
			8 => "Via", 
			9 => "Warning", 
			10 => "Allow", 
			11 => "Content-Length", 
			12 => "Content-Type", 
			13 => "Content-Encoding", 
			14 => "Content-Language", 
			15 => "Content-Location", 
			16 => "Content-MD5", 
			17 => "Content-Range", 
			18 => "Expires", 
			19 => "Last-Modified", 
			20 => "Accept", 
			21 => "Accept-Charset", 
			22 => "Accept-Encoding", 
			23 => "Accept-Language", 
			24 => "Authorization", 
			25 => "Cookie", 
			26 => "Expect", 
			27 => "From", 
			28 => "Host", 
			29 => "If-Match", 
			30 => "If-Modified-Since", 
			31 => "If-None-Match", 
			32 => "If-Range", 
			33 => "If-Unmodified-Since", 
			34 => "Max-Forwards", 
			35 => "Proxy-Authorization", 
			36 => "Referer", 
			37 => "Range", 
			38 => "TE", 
			39 => "User-Agent", 
			_ => throw new IndexOutOfRangeException("index"), 
		};
	}

	/// <summary>Returns the index number of the specified HTTP response header.</summary>
	/// <param name="header">The name of the HTTP header. </param>
	/// <returns>The index number of the HTTP response header specified in the <paramref name="header" /> parameter.</returns>
	public static int GetKnownResponseHeaderIndex(string header)
	{
		if (ResponseHeaderIndexer.TryGetValue(header, out var value))
		{
			return value;
		}
		return -1;
	}

	/// <summary>Returns the name of the specified HTTP response header.</summary>
	/// <param name="index">The index number of the header. </param>
	/// <returns>The name of the HTTP response header specified in the <paramref name="index" /> parameter.</returns>
	public static string GetKnownResponseHeaderName(int index)
	{
		return index switch
		{
			0 => "Cache-Control", 
			1 => "Connection", 
			2 => "Date", 
			3 => "Keep-Alive", 
			4 => "Pragma", 
			5 => "Trailer", 
			6 => "Transfer-Encoding", 
			7 => "Upgrade", 
			8 => "Via", 
			9 => "Warning", 
			10 => "Allow", 
			11 => "Content-Length", 
			12 => "Content-Type", 
			13 => "Content-Encoding", 
			14 => "Content-Language", 
			15 => "Content-Location", 
			16 => "Content-MD5", 
			17 => "Content-Range", 
			18 => "Expires", 
			19 => "Last-Modified", 
			20 => "Accept-Ranges", 
			21 => "Age", 
			22 => "ETag", 
			23 => "Location", 
			24 => "Proxy-Authenticate", 
			25 => "Retry-After", 
			26 => "Server", 
			27 => "Set-Cookie", 
			28 => "Vary", 
			29 => "WWW-Authenticate", 
			_ => throw new IndexOutOfRangeException("index"), 
		};
	}

	/// <summary>Returns a string that describes the name of the specified HTTP status code.</summary>
	/// <param name="code">The HTTP status code. </param>
	/// <returns>The status description. For example, <see cref="M:System.Web.HttpWorkerRequest.GetStatusDescription(System.Int32)" /> (404) returns "Not Found".</returns>
	public static string GetStatusDescription(int code)
	{
		return code switch
		{
			100 => "Continue", 
			101 => "Switching Protocols", 
			102 => "Processing", 
			200 => "OK", 
			201 => "Created", 
			202 => "Accepted", 
			203 => "Non-Authoritative Information", 
			204 => "No Content", 
			205 => "Reset Content", 
			206 => "Partial Content", 
			207 => "Multi-Status", 
			300 => "Multiple Choices", 
			301 => "Moved Permanently", 
			302 => "Found", 
			303 => "See Other", 
			304 => "Not Modified", 
			305 => "Use Proxy", 
			307 => "Temporary Redirect", 
			400 => "Bad Request", 
			401 => "Unauthorized", 
			402 => "Payment Required", 
			403 => "Forbidden", 
			404 => "Not Found", 
			405 => "Method Not Allowed", 
			406 => "Not Acceptable", 
			407 => "Proxy Authentication Required", 
			408 => "Request Timeout", 
			409 => "Conflict", 
			410 => "Gone", 
			411 => "Length Required", 
			412 => "Precondition Failed", 
			413 => "Request Entity Too Large", 
			414 => "Request-Uri Too Long", 
			415 => "Unsupported Media Type", 
			416 => "Requested Range Not Satisfiable", 
			417 => "Expectation Failed", 
			422 => "Unprocessable Entity", 
			423 => "Locked", 
			424 => "Failed Dependency", 
			500 => "Internal Server Error", 
			501 => "Not Implemented", 
			502 => "Bad Gateway", 
			503 => "Service Unavailable", 
			504 => "Gateway Timeout", 
			505 => "Http Version Not Supported", 
			507 => "Insufficient Storage", 
			_ => "", 
		};
	}

	/// <summary>When overridden in a derived class, gets the certification fields (specified in the X.509 standard) from a request issued by the client.</summary>
	/// <returns>A byte array containing the stream of the entire certificate content.</returns>
	public virtual byte[] GetClientCertificate()
	{
		return new byte[0];
	}

	/// <summary>Gets the certificate issuer, in binary format.</summary>
	/// <returns>A byte array containing the certificate issuer expressed in binary format.</returns>
	public virtual byte[] GetClientCertificateBinaryIssuer()
	{
		return new byte[0];
	}

	/// <summary>When overridden in a derived class, returns the <see cref="T:System.Text.Encoding" /> object in which the client certificate was encoded. </summary>
	/// <returns>The certificate encoding, expressed as an integer.</returns>
	public virtual int GetClientCertificateEncoding()
	{
		return 0;
	}

	/// <summary>When overridden in a derived class, gets a <see langword="PublicKey" /> object associated with the client certificate.</summary>
	/// <returns>A <see langword="PublicKey" /> object.</returns>
	public virtual byte[] GetClientCertificatePublicKey()
	{
		return new byte[0];
	}

	/// <summary>When overridden in a derived class, gets the date when the certificate becomes valid. The date varies with international settings. </summary>
	/// <returns>A <see cref="T:System.DateTime" /> object representing when the certificate becomes valid.</returns>
	public virtual DateTime GetClientCertificateValidFrom()
	{
		return DateTime.Now;
	}

	/// <summary>Gets the certificate expiration date.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> object representing the date that the certificate expires.</returns>
	public virtual DateTime GetClientCertificateValidUntil()
	{
		return DateTime.Now;
	}

	/// <summary>When overridden in a derived class, returns the ID of the current connection.</summary>
	/// <returns>Always returns 0.</returns>
	public virtual long GetConnectionID()
	{
		return 0L;
	}

	/// <summary>When overridden in a derived class, returns the context ID of the current connection.</summary>
	/// <returns>Always returns 0.</returns>
	public virtual long GetUrlContextID()
	{
		return 0L;
	}

	/// <summary>Gets the impersonation token for the request virtual path.</summary>
	/// <returns>An unmanaged memory pointer for the token for the request virtual path.</returns>
	public virtual IntPtr GetVirtualPathToken()
	{
		return IntPtr.Zero;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpWorkerRequest" /> class.</summary>
	protected HttpWorkerRequest()
	{
	}
}
