using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Routing;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that enables ASP.NET to read the HTTP values that are sent by a client during a Web request.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HttpRequestWrapper : HttpRequestBase
{
	private HttpRequest w;

	/// <summary>Gets an array of client-supported MIME accept types.</summary>
	/// <returns>An array of client-supported MIME accept types.</returns>
	public override string[] AcceptTypes => w.AcceptTypes;

	/// <summary>Gets the anonymous identifier for the user, if it is available.</summary>
	/// <returns>The current anonymous user identifier.</returns>
	public override string AnonymousID => w.AnonymousID;

	/// <summary>Gets the virtual path of the root of the ASP.NET application on the server.</summary>
	/// <returns>The virtual root path of the current application.</returns>
	public override string ApplicationPath => w.ApplicationPath;

	/// <summary>Gets the virtual path of the application root and makes it relative by using the tilde (~) notation for the application root (as in "~/page.aspx").</summary>
	/// <returns>The virtual path of the application root for the current request with the tilde operator added.</returns>
	public override string AppRelativeCurrentExecutionFilePath => w.AppRelativeCurrentExecutionFilePath;

	/// <summary>Gets information about the requesting client's browser capabilities.</summary>
	/// <returns>An object that represents the capabilities of the client browser.</returns>
	public override HttpBrowserCapabilitiesBase Browser => new HttpBrowserCapabilitiesWrapper(w.Browser);

	/// <summary>Gets the current request's client security certificate.</summary>
	/// <returns>The client's security certificate.</returns>
	public override HttpClientCertificate ClientCertificate => w.ClientCertificate;

	/// <summary>Gets or sets the character set of the data that was provided by the client.</summary>
	/// <returns>The client's character set.</returns>
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

	/// <summary>Gets the length, in bytes, of content that was sent by the client.</summary>
	/// <returns>The length, in bytes, of content that was sent by the client.</returns>
	public override int ContentLength => w.ContentLength;

	/// <summary>Gets or sets the MIME content type of the request.</summary>
	/// <returns>The MIME content type of the request, such as "text/html".</returns>
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

	/// <summary>Gets the collection of cookies that were sent by the client.</summary>
	/// <returns>The client's cookies.</returns>
	public override HttpCookieCollection Cookies => w.Cookies;

	/// <summary>Gets the virtual path of the current request.</summary>
	/// <returns>The virtual path of the page handler that is currently executing.</returns>
	public override string CurrentExecutionFilePath => w.CurrentExecutionFilePath;

	/// <summary>Gets the virtual path of the current request.</summary>
	/// <returns>The virtual path of the current request.</returns>
	public override string FilePath => w.FilePath;

	/// <summary>Gets the collection of files that were uploaded by the client, in multipart MIME format.</summary>
	/// <returns>The files that were uploaded by the client. The items in the <see cref="T:System.Web.HttpFileCollection" /> object are of type <see cref="T:System.Web.HttpPostedFile" />.</returns>
	public override HttpFileCollectionBase Files => new HttpFileCollectionWrapper(w.Files);

	/// <summary>Gets or sets the filter to use when the current input stream is being read.</summary>
	/// <returns>An object to use as the filter.</returns>
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

	/// <summary>Gets the collection of form variables that were sent by the client.</summary>
	/// <returns>The form variables.</returns>
	public override NameValueCollection Form => w.Form;

	/// <summary>Gets the collection of HTTP headers that were sent by the client.</summary>
	/// <returns>The request headers.</returns>
	public override NameValueCollection Headers => w.Headers;

	/// <summary>Gets the HTTP data-transfer method (such as <see langword="GET" />, <see langword="POST" />, or <see langword="HEAD" />) that was used by the client.</summary>
	/// <returns>The HTTP data-transfer method that was used by the client.</returns>
	public override string HttpMethod => w.HttpMethod;

	/// <summary>Gets the <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> object of the current <see cref="T:System.Web.HttpWorkerRequest" /> instance.</summary>
	/// <returns>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" /> object of the current <see cref="T:System.Web.HttpWorkerRequest" /> instance.</returns>
	/// <exception cref="T:System.NotImplementedException">The current <see cref="T:System.Web.HttpWorkerRequest" /> object is not a <see langword="System.Web.Hosting.IIS7WorkerRequest" /> object or a <see langword="System.Web.Hosting.ISAPIWorkerRequestInProc" /> object.</exception>
	public override ChannelBinding HttpChannelBinding => w.HttpChannelBinding;

	/// <summary>Gets the contents of the incoming HTTP entity body.</summary>
	/// <returns>The contents of the incoming HTTP content body.</returns>
	public override Stream InputStream => w.InputStream;

	/// <summary>Gets a value that indicates whether the request has been authenticated.</summary>
	/// <returns>
	///     <see langword="true" /> if the request has been authenticated; otherwise, <see langword="false" />.</returns>
	public override bool IsAuthenticated => w.IsAuthenticated;

	/// <summary>Gets a value that indicates whether the request is from the local computer.</summary>
	/// <returns>
	///     <see langword="true" /> if the request is from the local computer; otherwise, <see langword="false" />.</returns>
	public override bool IsLocal => w.IsLocal;

	/// <summary>Gets a value that indicates whether the HTTP connection uses secure sockets (HTTPS protocol).</summary>
	/// <returns>
	///     <see langword="true" /> if the connection is an SSL connection that uses HTTPS protocol; otherwise, <see langword="false" />.</returns>
	public override bool IsSecureConnection => w.IsSecureConnection;

	/// <summary>Gets the specified object from the <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.QueryString" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collections.</summary>
	/// <param name="key">The name of the collection member to get.</param>
	/// <returns>The <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.Cookies" />, or <see cref="P:System.Web.HttpRequest.ServerVariables" /> collection member that is specified by <paramref name="key" />. If the specified <paramref name="key" /> value is not found, <see langword="null" /> is returned.</returns>
	public override string this[string key] => w[key];

	/// <summary>Gets the <see cref="T:System.Security.Principal.WindowsIdentity" /> type for the current user.</summary>
	/// <returns>The identity for the current user.</returns>
	public override WindowsIdentity LogonUserIdentity => w.LogonUserIdentity;

	/// <summary>Gets a combined collection of <see cref="P:System.Web.HttpRequest.QueryString" />, <see cref="P:System.Web.HttpRequest.Form" />, <see cref="P:System.Web.HttpRequest.ServerVariables" />, and <see cref="P:System.Web.HttpRequest.Cookies" /> items.</summary>
	/// <returns>The collection of combined values.</returns>
	public override NameValueCollection Params => w.Params;

	/// <summary>Gets the virtual path of the current request.</summary>
	/// <returns>The virtual path of the current request.</returns>
	public override string Path => w.Path;

	/// <summary>Gets additional path information for a resource that has a URL extension.</summary>
	/// <returns>The additional path information for the resource.</returns>
	public override string PathInfo => w.PathInfo;

	/// <summary>Gets the physical file-system path of the current application's root directory.</summary>
	/// <returns>The file-system path of the current application's root directory.</returns>
	public override string PhysicalApplicationPath => w.PhysicalApplicationPath;

	/// <summary>Gets the physical file-system path of the requested resource.</summary>
	/// <returns>The file-system path of the requested resource.</returns>
	public override string PhysicalPath => w.PhysicalPath;

	/// <summary>Gets the collection of HTTP query-string variables.</summary>
	/// <returns>The query-string variables that were sent by the client in the URL of the current request.</returns>
	public override NameValueCollection QueryString => w.QueryString;

	/// <summary>Gets the complete URL of the current request.</summary>
	/// <returns>The complete URL of the current request.</returns>
	public override string RawUrl => w.RawUrl;

	/// <summary>Gets or sets the HTTP data-transfer method (<see langword="GET" /> or <see langword="POST" />) that was used by the client.</summary>
	/// <returns>The HTTP data-transfer method type that was used by the client.</returns>
	public override string RequestType
	{
		get
		{
			return w.RequestType;
		}
		set
		{
			w.RequestType = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Routing.RequestContext" /> instance of the current request.</summary>
	/// <returns>The <see cref="T:System.Web.Routing.RequestContext" /> instance of the current request. For non-routed requests, the <see cref="T:System.Web.Routing.RequestContext" /> object that is returned is empty.</returns>
	public override RequestContext RequestContext
	{
		get
		{
			return w.RequestContext;
		}
		set
		{
			w.RequestContext = value;
		}
	}

	/// <summary>Gets a collection of Web server variables.</summary>
	/// <returns>The server variables.</returns>
	public override NameValueCollection ServerVariables => w.ServerVariables;

	/// <summary>Gets a <see cref="T:System.Threading.CancellationToken" /> object that is tripped when a request times out.</summary>
	/// <returns>The cancellation token.</returns>
	public override CancellationToken TimedOutToken => w.TimedOutToken;

	/// <summary>Gets the number of bytes in the current input stream.</summary>
	/// <returns>The number of bytes in the input stream.</returns>
	public override int TotalBytes => w.TotalBytes;

	/// <summary>Gets an access to HTTP request values without triggering request validation.</summary>
	/// <returns>The unvalidated request values.</returns>
	public override UnvalidatedRequestValuesBase Unvalidated => new UnvalidatedRequestValuesWrapper(w.Unvalidated);

	/// <summary>Gets a value that indicates whether the request entity body has been read, and if so, how it was read.</summary>
	/// <returns>The value that indicates how the request entity body was read, or that it has not been read.</returns>
	public override ReadEntityBodyMode ReadEntityBodyMode => ReadEntityBodyMode.Classic;

	/// <summary>Gets information about the URL of the current request.</summary>
	/// <returns>An object that contains information about the URL of the current request.</returns>
	public override Uri Url => w.Url;

	/// <summary>Gets information about the URL of the client request that linked to the current URL.</summary>
	/// <returns>The URL of the page that linked to the current request.</returns>
	public override Uri UrlReferrer => w.UrlReferrer;

	/// <summary>Gets the complete user-agent string of the client.</summary>
	/// <returns>The complete user-agent string of the client.</returns>
	public override string UserAgent => w.UserAgent;

	/// <summary>Gets the IP host address of the client.</summary>
	/// <returns>The IP address of the client.</returns>
	public override string UserHostAddress => w.UserHostAddress;

	/// <summary>Gets the DNS name of the client.</summary>
	/// <returns>The DNS name of the client.</returns>
	public override string UserHostName => w.UserHostName;

	/// <summary>Gets a sorted array of client language preferences.</summary>
	/// <returns>A sorted array of client language preferences, or <see langword="null" /> if the array is empty.</returns>
	public override string[] UserLanguages => w.UserLanguages;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpRequestWrapper" /> class by using the specified request object.</summary>
	/// <param name="httpRequest">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="httpRequest" /> is <see langword="null" />.</exception>
	public HttpRequestWrapper(HttpRequest httpRequest)
	{
		if (httpRequest == null)
		{
			throw new ArgumentNullException("httpRequest");
		}
		w = httpRequest;
	}

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
	/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following: The <see cref="P:System.Web.HttpRequest.Form" /> property.The <see cref="P:System.Web.HttpRequest.Files" /> property.The <see cref="P:System.Web.HttpRequest.InputStream" /> property.The <see cref="M:System.Web.HttpRequest.GetBufferlessInputStream" /> method.To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
	public override Stream GetBufferedInputStream()
	{
		return w.GetBufferedInputStream();
	}

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
	/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following:The <see cref="P:System.Web.HttpRequest.Form" /> property.The <see cref="P:System.Web.HttpRequest.InputStream" /> property.The <see cref="P:System.Web.HttpRequest.Files" /> property.The <see cref="M:System.Web.HttpRequest.GetBufferedInputStream" /> method.To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
	public override Stream GetBufferlessInputStream()
	{
		return w.GetBufferlessInputStream();
	}

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body, , optionally disabling the request length limit that is set in the <see cref="P:System.Web.Configuration.HttpRuntimeSection.MaxRequestLength" /> property.</summary>
	/// <param name="disableMaxRequestLength">
	///       <see langword="true" /> to disable the request length limit; otherwise, <see langword="false" />.</param>
	/// <returns>A <see cref="T:System.IO.Stream" /> object that can be used to read the incoming HTTP entity body.</returns>
	/// <exception cref="T:System.Web.HttpException">The request's entity body has already been loaded and parsed. Examples of properties that cause the entity body to be loaded and parsed include the following: The <see cref="P:System.Web.HttpRequest.Form" /> property.The <see cref="P:System.Web.HttpRequest.Files" /> property.The <see cref="P:System.Web.HttpRequest.InputStream" /> property.The <see cref="M:System.Web.HttpRequest.GetBufferedInputStream" /> method.To avoid this exception, call the <see cref="P:System.Web.HttpRequest.ReadEntityBodyMode" /> method first. This exception is also thrown if the client disconnects while the entity body is being read.</exception>
	public override Stream GetBufferlessInputStream(bool disableMaxRequestLength)
	{
		return w.GetBufferlessInputStream(disableMaxRequestLength);
	}

	/// <summary>Forcibly terminates the underlying TCP connection, causing any outstanding I/O to fail.</summary>
	public override void Abort()
	{
		w.WorkerRequest.CloseConnection();
	}

	/// <summary>Performs a binary read of a specified number of bytes from the current input stream.</summary>
	/// <param name="count">The number of bytes to read. </param>
	/// <returns>An array that contains the binary data.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="count" /> is less than 0.- or -
	///         <paramref name="count" /> is greater than the number of bytes available.</exception>
	public override byte[] BinaryRead(int count)
	{
		return w.BinaryRead(count);
	}

	/// <summary>Maps an incoming image-field form parameter to appropriate x-coordinate and y-coordinate values.</summary>
	/// <param name="imageFieldName">The name of the image map.</param>
	/// <returns>A two-dimensional array of integers.</returns>
	public override int[] MapImageCoordinates(string imageFieldName)
	{
		return w.MapImageCoordinates(imageFieldName);
	}

	/// <summary>Maps the specified virtual path to a physical path on the server.</summary>
	/// <param name="virtualPath">The virtual path (absolute or relative) to map to a physical path.</param>
	/// <returns>The physical path on the server that is specified by <paramref name="virtualPath" />.</returns>
	public override string MapPath(string virtualPath)
	{
		return w.MapPath(virtualPath);
	}

	/// <summary>Maps the specified virtual path to a physical path on the server.</summary>
	/// <param name="virtualPath">The virtual path (absolute or relative) to map to a physical path. </param>
	/// <param name="baseVirtualDir">The virtual base directory path that is used for relative resolution.</param>
	/// <param name="allowCrossAppMapping">
	///       <see langword="true" /> to indicate that <paramref name="virtualPath" /> can belong to another application; otherwise, <see langword="false" />.</param>
	/// <returns>The physical path on the server.</returns>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="allowCrossAppMapping" /> is <see langword="false" /> and <paramref name="virtualPath" /> belongs to another application.</exception>
	/// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.HttpContext" /> object is defined for the request. </exception>
	public override string MapPath(string virtualPath, string baseVirtualDir, bool allowCrossAppMapping)
	{
		return w.MapPath(virtualPath, baseVirtualDir, allowCrossAppMapping);
	}

	/// <summary>Maps an incoming image field form parameter into appropriate x and y coordinate values.</summary>
	/// <param name="imageFieldName">The name of the image field.</param>
	/// <returns>The x and y coordinate values.</returns>
	public override double[] MapRawImageCoordinates(string imageFieldName)
	{
		return w.MapRawImageCoordinates(imageFieldName);
	}

	/// <summary>Saves an HTTP request to disk.</summary>
	/// <param name="filename">The physical drive path.</param>
	/// <param name="includeHeaders">A value that specifies whether to save HTTP headers to disk. </param>
	public override void SaveAs(string filename, bool includeHeaders)
	{
		w.SaveAs(filename, includeHeaders);
	}

	/// <summary>Causes validation to occur for the collections that are accessed through the <see cref="P:System.Web.HttpRequest.Cookies" />, <see cref="P:System.Web.HttpRequest.Form" />, and <see cref="P:System.Web.HttpRequest.QueryString" /> properties.</summary>
	public override void ValidateInput()
	{
		w.ValidateInput();
	}
}
