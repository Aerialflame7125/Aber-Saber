using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that provides helper methods for processing Web requests.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HttpServerUtilityWrapper : HttpServerUtilityBase
{
	private HttpServerUtility w;

	/// <summary>Gets the server's computer name.</summary>
	/// <returns>The name of the server computer.</returns>
	/// <exception cref="T:System.Web.HttpException">The computer name cannot be found.</exception>
	public override string MachineName => w.MachineName;

	/// <summary>Gets or sets the request time-out value in seconds.</summary>
	/// <returns>The time-out value for requests.</returns>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> object is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out period is <see langword="null" /> or otherwise cannot be set.</exception>
	public override int ScriptTimeout
	{
		get
		{
			return w.ScriptTimeout;
		}
		set
		{
			w.ScriptTimeout = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpServerUtilityWrapper" /> class.</summary>
	/// <param name="httpServerUtility">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="httpServerUtility" /> parameter is <see langword="null" />.</exception>
	public HttpServerUtilityWrapper(HttpServerUtility httpServerUtility)
	{
		if (httpServerUtility == null)
		{
			throw new ArgumentNullException("httpServerUtility");
		}
		w = httpServerUtility;
	}

	/// <summary>Clears the most recent exception.</summary>
	public override void ClearError()
	{
		w.ClearError();
	}

	/// <summary>Creates a server instance of a COM object that is identified by the object's programmatic identifier (ProgID).</summary>
	/// <param name="progID">The class or type of object to create an instance of.</param>
	/// <returns>The new object.</returns>
	/// <exception cref="T:System.Web.HttpException">An instance of the object could not be created.</exception>
	public override object CreateObject(string progID)
	{
		return w.CreateObject(progID);
	}

	/// <summary>Creates a server instance of a COM object that is identified by the object's type.</summary>
	/// <param name="type">A type that represents the object to create.</param>
	/// <returns>The new object.</returns>
	public override object CreateObject(Type type)
	{
		return w.CreateObject(type);
	}

	/// <summary>Creates a server instance of a COM object that is identified by the object's class identifier (CLSID).</summary>
	/// <param name="clsid">The class identifier of the object to create an instance of.</param>
	/// <returns>The new object.</returns>
	/// <exception cref="T:System.Web.HttpException">An instance of the object cannot be created.</exception>
	public override object CreateObjectFromClsid(string clsid)
	{
		return w.CreateObjectFromClsid(clsid);
	}

	/// <summary>Executes the handler for the specified virtual path in the context of the current process.</summary>
	/// <param name="path">The URL of the handler to execute.</param>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> object is <see langword="null" />.- or -An error occurred when the handler specified by <paramref name="path" /> was executed.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />. - or -
	///         <paramref name="path" /> is not a virtual path.</exception>
	public override void Execute(string path)
	{
		w.Execute(path);
	}

	/// <summary>Executes the handler for the specified virtual path in the context of the current process and specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
	/// <param name="path">The URL of the handler to execute. </param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> object is <see langword="null" />.- or -An error occurred when the handler specified by <paramref name="path" /> was executed.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />. - or -
	///         <paramref name="path" /> is not a virtual path. </exception>
	public override void Execute(string path, bool preserveForm)
	{
		w.Execute(path, preserveForm);
	}

	/// <summary>Executes the handler for the specified virtual path in the context of the current process, using a <see cref="T:System.IO.TextWriter" /> instance to capture output from the executed handler.</summary>
	/// <param name="path">The URL of the handler to execute. </param>
	/// <param name="writer">An object to capture the output. </param>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is <see langword="null" />. - or -An error occurred when the handler specified by <paramref name="path" /> was executed.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />. - or -
	///         <paramref name="path" /> is not a virtual path. </exception>
	public override void Execute(string path, TextWriter writer)
	{
		w.Execute(path, writer);
	}

	/// <summary>Executes the handler for the specified virtual path in the context of the current request, using a <see cref="T:System.IO.TextWriter" /> instance to capture output from the page and a value that indicates whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
	/// <param name="path">The URL of the handler to execute.</param>
	/// <param name="writer">The object to capture the output.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> instance is <see langword="null" />. - or -
	///         <paramref name="path" /> ends with a period (.).- or -An error occurred when the handler specified by <paramref name="path" /> was executed.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="path" /> is not a virtual path.</exception>
	public override void Execute(string path, TextWriter writer, bool preserveForm)
	{
		w.Execute(path, writer, preserveForm);
	}

	/// <summary>Executes the specified handler in the context of the current process, using a <see cref="T:System.IO.TextWriter" /> instance to capture output from the executed handler and a value that specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
	/// <param name="handler">The HTTP handler that implements the interface to transfer the current request to.</param>
	/// <param name="writer">The object to capture the output.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.Web.HttpException">An error occurred when the handler specified by <paramref name="handler" /> was executed.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="handler" /> parameter is <see langword="null" />.</exception>
	public override void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
	{
		w.Execute(handler, writer, preserveForm);
	}

	/// <summary>Returns the most recent exception.</summary>
	/// <returns>The previous exception that was thrown.</returns>
	public override Exception GetLastError()
	{
		return w.GetLastError();
	}

	/// <summary>Decodes an HTML-encoded string and returns the decoded string.</summary>
	/// <param name="s">The HTML string to decode.</param>
	/// <returns>The decoded text.</returns>
	public override string HtmlDecode(string s)
	{
		return w.HtmlDecode(s);
	}

	/// <summary>Decodes an HTML-encoded string and returns the results in a stream.</summary>
	/// <param name="s">The HTML string to decode.</param>
	/// <param name="output">The stream to contain the decoded string.</param>
	public override void HtmlDecode(string s, TextWriter output)
	{
		w.HtmlDecode(s, output);
	}

	/// <summary>HTML-encodes a string and returns the encoded string.</summary>
	/// <param name="s">The string to encode.</param>
	/// <returns>The HTML-encoded text.</returns>
	public override string HtmlEncode(string s)
	{
		return w.HtmlEncode(s);
	}

	/// <summary>HTML-encodes a string and sends the resulting output to an output stream.</summary>
	/// <param name="s">The string to encode. </param>
	/// <param name="output">The stream to contain the encoded string.</param>
	public override void HtmlEncode(string s, TextWriter output)
	{
		w.HtmlEncode(s, output);
	}

	/// <summary>Returns the physical file path that corresponds to the specified virtual path on the Web server.</summary>
	/// <param name="path">The virtual path to get the physical path for.</param>
	/// <returns>The physical file path that corresponds to <paramref name="path" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> object is <see langword="null" />.</exception>
	public override string MapPath(string path)
	{
		return w.MapPath(path);
	}

	/// <summary>Terminates execution of the current process and starts execution of a page or handler that is specified with a URL.</summary>
	/// <param name="path">The URL of the page or handler to execute.</param>
	public override void Transfer(string path)
	{
		w.Transfer(path);
	}

	/// <summary>Terminates execution of the current page and starts execution of a different page or handler by using the specified URL and a value that specifies whether to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" /> collections.</summary>
	/// <param name="path">The URL of the page or handler to execute.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.ApplicationException">The current page request is a callback.</exception>
	public override void Transfer(string path, bool preserveForm)
	{
		w.Transfer(path, preserveForm);
	}

	/// <summary>Terminates execution of the current process and starts execution of a new request, using a custom HTTP handler and a value that specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
	/// <param name="handler">The HTTP handler to transfer the current request to.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.ApplicationException">The current page request is a callback.</exception>
	public override void Transfer(IHttpHandler handler, bool preserveForm)
	{
		w.Transfer(handler, preserveForm);
	}

	/// <summary>Asynchronously executes the end point at the specified URL.</summary>
	/// <param name="path">The URL of the page or handler to execute.</param>
	/// <exception cref="T:System.PlatformNotSupportedException">The request requires the integrated pipeline mode of IIS 7.0.</exception>
	/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
	[MonoTODO]
	public override void TransferRequest(string path)
	{
		throw new NotImplementedException();
	}

	/// <summary>Asynchronously executes the endpoint at the specified URL and specifies whether to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" /> collections.</summary>
	/// <param name="path">The URL of the page to execute.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.PlatformNotSupportedException">The request requires the integrated pipeline mode of IIS 7.0.</exception>
	/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
	[MonoTODO]
	public override void TransferRequest(string path, bool preserveForm)
	{
		throw new NotImplementedException();
	}

	/// <summary>Asynchronously executes the endpoint at the specified URL by using the specified HTTP method and headers.</summary>
	/// <param name="path">The URL of the page or handler to execute.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <param name="method">The HTTP method (<see langword="GET" />, <see langword="POST" />, and so on) to use for the new request. If <see langword="null" />, the HTTP method of the original request is used.</param>
	/// <param name="headers">A collection of request headers for the new request.</param>
	/// <exception cref="T:System.PlatformNotSupportedException">The request requires IIS 7.0 running in integrated mode.</exception>
	/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
	[MonoTODO]
	public override void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
	{
		throw new NotImplementedException();
	}

	/// <summary>Decodes a URL-encoded string and returns the decoded string.</summary>
	/// <param name="s">The string to decode.</param>
	/// <returns>The decoded text.</returns>
	public override string UrlDecode(string s)
	{
		return w.UrlDecode(s);
	}

	/// <summary>Decodes a URL-encoded string and sends the resulting output to a stream.</summary>
	/// <param name="s">The HTML string to decode.</param>
	/// <param name="output">The stream to contain the decoded string.</param>
	public override void UrlDecode(string s, TextWriter output)
	{
		w.UrlDecode(s, output);
	}

	/// <summary>URL-encodes a string and returns the encoded string.</summary>
	/// <param name="s">The text to URL-encode.</param>
	/// <returns>The URL-encoded text.</returns>
	public override string UrlEncode(string s)
	{
		return w.UrlEncode(s);
	}

	/// <summary>URL-encodes a string and sends the resulting output to a stream.</summary>
	/// <param name="s">The string to encode.</param>
	/// <param name="output">The stream to contain the encoded string.</param>
	public override void UrlEncode(string s, TextWriter output)
	{
		w.UrlEncode(s, output);
	}

	/// <summary>URL-encodes the path section of a URL string.</summary>
	/// <param name="s">The string to URL-encode.</param>
	/// <returns>The URL-encoded text.</returns>
	public override string UrlPathEncode(string s)
	{
		return w.UrlPathEncode(s);
	}

	/// <summary>Decodes a URL string token into an equivalent byte array by using base64 digits.</summary>
	/// <param name="input">The URL string token to decode.</param>
	/// <returns>The byte array that contains the decoded URL string token.</returns>
	/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="input" /> parameter is <see langword="null" />.</exception>
	[MonoTODO]
	public override byte[] UrlTokenDecode(string input)
	{
		throw new NotImplementedException();
	}

	/// <summary>Encodes a byte array into an equivalent string representation by using base64 digits, which makes it usable for transmission on the URL.</summary>
	/// <param name="input">The byte array to encode.</param>
	/// <returns>The string that contains the encoded array if the length of <paramref name="input" /> is greater than 1; otherwise, an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="input" /> parameter is <see langword="null" />.</exception>
	[MonoTODO]
	public override string UrlTokenEncode(byte[] input)
	{
		throw new NotImplementedException();
	}
}
