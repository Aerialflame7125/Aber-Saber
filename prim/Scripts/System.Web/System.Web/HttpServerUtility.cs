using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.Util;

namespace System.Web;

/// <summary>Provides helper methods for processing Web requests.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpServerUtility
{
	private HttpContext context;

	/// <summary>Gets the server's computer name.</summary>
	/// <returns>The name of the local computer.</returns>
	/// <exception cref="T:System.Web.HttpException">The computer name cannot be found.</exception>
	public string MachineName
	{
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		[EnvironmentPermission(SecurityAction.Assert, Read = "COMPUTERNAME")]
		get
		{
			return Environment.MachineName;
		}
	}

	/// <summary>Gets and sets the request time-out value in seconds.</summary>
	/// <returns>The time-out value setting for requests.</returns>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out period is <see langword="null" /> or otherwise could not be set.</exception>
	public int ScriptTimeout
	{
		get
		{
			return (int)context.ConfigTimeout.TotalSeconds;
		}
		[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Medium)]
		set
		{
			context.ConfigTimeout = TimeSpan.FromSeconds(value);
		}
	}

	internal HttpServerUtility(HttpContext context)
	{
		this.context = context;
	}

	/// <summary>Clears the previous exception.</summary>
	public void ClearError()
	{
		context.ClearError();
	}

	/// <summary>Creates a server instance of a COM object identified by the object's programmatic identifier (ProgID).</summary>
	/// <param name="progID">The class or type of object to create an instance of.</param>
	/// <returns>The new object.</returns>
	/// <exception cref="T:System.Web.HttpException">An instance of the object could not be created.</exception>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public object CreateObject(string progID)
	{
		throw new HttpException(500, "COM is not supported");
	}

	/// <summary>Creates a server instance of a COM object identified by the object's type.</summary>
	/// <param name="type">A <see cref="T:System.Type" /> representing the object to create.</param>
	/// <returns>The new object.</returns>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public object CreateObject(Type type)
	{
		throw new HttpException(500, "COM is not supported");
	}

	/// <summary>Creates a server instance of a COM object identified by the object's class identifier (CLSID).</summary>
	/// <param name="clsid">The class identifier of the object to create an instance of.</param>
	/// <returns>The new object.</returns>
	/// <exception cref="T:System.Web.HttpException">An instance of the object could not be created.</exception>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public object CreateObjectFromClsid(string clsid)
	{
		throw new HttpException(500, "COM is not supported");
	}

	/// <summary>Executes the handler for the specified virtual path in the context of the current request. </summary>
	/// <param name="path">The URL path to execute.</param>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is <see langword="null" />.- or -An error occurred while executing the handler specified by <paramref name="path" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />. - or -
	///         <paramref name="path" /> is not a virtual path.</exception>
	public void Execute(string path)
	{
		Execute(path, null, preserveForm: true);
	}

	/// <summary>Executes the handler for the specified virtual path in the context of the current request. A <see cref="T:System.IO.TextWriter" /> captures output from the executed handler.</summary>
	/// <param name="path">The URL path to execute. </param>
	/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to capture the output. </param>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is <see langword="null" />. - or -An error occurred while executing the handler specified by <paramref name="path" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />. - or -
	///         <paramref name="path" /> is not a virtual path. </exception>
	public void Execute(string path, TextWriter writer)
	{
		Execute(path, writer, preserveForm: true);
	}

	/// <summary>Executes the handler for the specified virtual path in the context of the current request and specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
	/// <param name="path">The URL path to execute. </param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is <see langword="null" />.- or -An error occurred while executing the handler specified by <paramref name="path" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />. - or -
	///         <paramref name="path" /> is not a virtual path. </exception>
	public void Execute(string path, bool preserveForm)
	{
		Execute(path, null, preserveForm);
	}

	/// <summary>Executes the handler for the specified virtual path in the context of the current request. A <see cref="T:System.IO.TextWriter" /> captures output from the page and a Boolean parameter specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
	/// <param name="path">The URL path to execute.</param>
	/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to capture the output.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is a null reference (<see langword="Nothing" /> in Visual Basic). - or -
	///         <paramref name="path" /> ends with a period (.).- or -An error occurred while executing the handler specified by <paramref name="path" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="path" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="path" /> is not a virtual path.</exception>
	public void Execute(string path, TextWriter writer, bool preserveForm)
	{
		Execute(path, writer, preserveForm, isTransfer: false);
	}

	private void Execute(string path, TextWriter writer, bool preserveForm, bool isTransfer)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		if (path.IndexOf(':') != -1)
		{
			throw new ArgumentException("Invalid path.");
		}
		string queryString = null;
		int num = path.IndexOf('?');
		if (num != -1)
		{
			queryString = path.Substring(num + 1);
			path = path.Substring(0, num);
		}
		string text = UrlUtils.Combine(context.Request.BaseVirtualDir, path);
		SessionStateSection config = WebConfigurationManager.GetWebApplicationSection("system.web/sessionState") as SessionStateSection;
		if (SessionStateModule.IsCookieLess(context, config))
		{
			text = UrlUtils.RemoveSessionId(VirtualPathUtility.GetDirectory(text), text);
		}
		IHttpHandler handler = context.ApplicationInstance.GetHandler(context, text, ignoreContextHandler: true);
		Execute(handler, writer, preserveForm, text, queryString, isTransfer, isInclude: true);
	}

	internal void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm, string exePath, string queryString, bool isTransfer, bool isInclude)
	{
		bool flag = handler is StaticFileHandler;
		if (isTransfer && !(handler is Page) && !flag)
		{
			throw new HttpException("Transfer is only allowed to .aspx and static files");
		}
		HttpRequest request = context.Request;
		string queryStringRaw = request.QueryStringRaw;
		if (queryString != null)
		{
			request.QueryStringRaw = queryString;
		}
		else if (!preserveForm)
		{
			request.QueryStringRaw = string.Empty;
		}
		HttpResponse response = context.Response;
		WebROCollection form = request.Form as WebROCollection;
		if (!preserveForm)
		{
			request.SetForm(new WebROCollection());
		}
		TextWriter textWriter = writer;
		if (textWriter == null)
		{
			textWriter = response.Output;
		}
		TextWriter textWriter2 = response.SetTextWriter(textWriter);
		string currentExecutionFilePath = request.CurrentExecutionFilePath;
		bool isProcessingInclude = context.IsProcessingInclude;
		try
		{
			context.PushHandler(handler);
			if (flag)
			{
				request.SetFilePath(exePath);
			}
			request.SetCurrentExePath(exePath);
			context.IsProcessingInclude = isInclude;
			if (!(handler is IHttpAsyncHandler))
			{
				handler.ProcessRequest(context);
				return;
			}
			IHttpAsyncHandler obj = (IHttpAsyncHandler)handler;
			IAsyncResult asyncResult = obj.BeginProcessRequest(context, null, null);
			(asyncResult?.AsyncWaitHandle)?.WaitOne();
			obj.EndProcessRequest(asyncResult);
		}
		finally
		{
			if (queryStringRaw != request.QueryStringRaw)
			{
				if (queryStringRaw != null && queryStringRaw.Length > 0)
				{
					queryStringRaw = queryStringRaw.Substring(1);
					request.QueryStringRaw = queryStringRaw;
				}
				else
				{
					request.QueryStringRaw = string.Empty;
				}
			}
			response.SetTextWriter(textWriter2);
			if (!preserveForm)
			{
				request.SetForm(form);
			}
			context.PopHandler();
			request.SetCurrentExePath(currentExecutionFilePath);
			context.IsProcessingInclude = isProcessingInclude;
		}
	}

	/// <summary>Returns the previous exception.</summary>
	/// <returns>The previous exception that was thrown.</returns>
	public Exception GetLastError()
	{
		if (context == null)
		{
			return HttpContext.Current.Error;
		}
		return context.Error;
	}

	/// <summary>Decodes an HTML-encoded string and returns the decoded string.</summary>
	/// <param name="s">The HTML string to decode.</param>
	/// <returns>The decoded text.</returns>
	public string HtmlDecode(string s)
	{
		return HttpUtility.HtmlDecode(s);
	}

	/// <summary>Decodes an HTML-encoded string and sends the resulting output to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
	/// <param name="s">The HTML string to decode.</param>
	/// <param name="output">The <see cref="T:System.IO.TextWriter" /> output stream that contains the decoded string.</param>
	public void HtmlDecode(string s, TextWriter output)
	{
		HttpUtility.HtmlDecode(s, output);
	}

	/// <summary>HTML-encodes a string and returns the encoded string.</summary>
	/// <param name="s">The text string to encode.</param>
	/// <returns>The HTML-encoded text.</returns>
	public string HtmlEncode(string s)
	{
		return HttpUtility.HtmlEncode(s);
	}

	/// <summary>HTML-encodes a string and sends the resulting output to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
	/// <param name="s">The string to encode. </param>
	/// <param name="output">The <see cref="T:System.IO.TextWriter" /> output stream that contains the encoded string.</param>
	public void HtmlEncode(string s, TextWriter output)
	{
		HttpUtility.HtmlEncode(s, output);
	}

	/// <summary>Returns the physical file path that corresponds to the specified virtual path.</summary>
	/// <param name="path">The virtual path in the Web application.</param>
	/// <returns>The physical file path on the Web server that corresponds to <paramref name="path" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is <see langword="null" />.</exception>
	public string MapPath(string path)
	{
		return context.Request.MapPath(path);
	}

	/// <summary>Performs an asynchronous execution of the specified URL.</summary>
	/// <param name="path">The URL path of the new page on the server to execute.</param>
	/// <exception cref="T:System.PlatformNotSupportedException">The request requires the integrated pipeline mode of IIS 7.0.</exception>
	/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
	public void TransferRequest(string path)
	{
		TransferRequest(path, preserveForm: false, null, null);
	}

	/// <summary>Performs an asynchronous execution of the specified URL and preserves query string parameters.</summary>
	/// <param name="path">The URL path of the new page on the server to execute.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.Form" /> collection; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.Form" /> collection.</param>
	/// <exception cref="T:System.PlatformNotSupportedException">The request requires the integrated pipeline mode of IIS 7.0.</exception>
	/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
	public void TransferRequest(string path, bool preserveForm)
	{
		TransferRequest(path, preserveForm, null, null);
	}

	/// <summary>Performs an asynchronous execution of the specified URL using the specified HTTP method and headers.</summary>
	/// <param name="path">The URL path of the new page on the server to execute.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.Form" /> collection; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.Form" /> collection.</param>
	/// <param name="method">The HTTP method to use in the execution of the new request.</param>
	/// <param name="headers">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of request headers for the new request.</param>
	/// <exception cref="T:System.PlatformNotSupportedException">The request requires IIS 7.0 running in integrated mode.</exception>
	/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
	[MonoTODO("Always throws PlatformNotSupportedException.")]
	public void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
	{
		throw new PlatformNotSupportedException();
	}

	/// <summary>For the current request, terminates execution of the current page and starts execution of a new page by using the specified URL path of the page.</summary>
	/// <param name="path">The URL path of the new page on the server to execute.</param>
	public void Transfer(string path)
	{
		Transfer(path, preserveForm: true);
	}

	/// <summary>Terminates execution of the current page and starts execution of a new page by using the specified URL path of the page. Specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
	/// <param name="path">The URL path of the new page on the server to execute.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.ApplicationException">The current page request is a callback.</exception>
	public void Transfer(string path, bool preserveForm)
	{
		Execute(path, null, preserveForm, isTransfer: true);
		context.Response.End();
	}

	/// <summary>Terminates execution of the current page and starts execution of a new request by using a custom HTTP handler that implements the <see cref="T:System.Web.IHttpHandler" /> interface and specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
	/// <param name="handler">The HTTP handler that implements the <see cref="T:System.Web.IHttpHandler" /> to transfer the current request to.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.ApplicationException">The current page request is a callback.</exception>
	public void Transfer(IHttpHandler handler, bool preserveForm)
	{
		if (handler == null)
		{
			throw new ArgumentNullException("handler");
		}
		Execute(handler, null, preserveForm, context.Request.CurrentExecutionFilePath, null, isTransfer: true, isInclude: true);
		context.Response.End();
	}

	/// <summary>Executes the handler for the specified virtual path in the context of the current request. A <see cref="T:System.IO.TextWriter" /> captures output from the executed handler and a Boolean parameter specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
	/// <param name="handler">The HTTP handler that implements the <see cref="T:System.Web.IHttpHandler" /> to transfer the current request to.</param>
	/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to capture the output.</param>
	/// <param name="preserveForm">
	///       <see langword="true" /> to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; <see langword="false" /> to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
	/// <exception cref="T:System.Web.HttpException">An error occurred while executing the handler specified by <paramref name="handler" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="handler" /> parameter is <see langword="null" />.</exception>
	public void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
	{
		if (handler == null)
		{
			throw new ArgumentNullException("handler");
		}
		Execute(handler, writer, preserveForm, context.Request.CurrentExecutionFilePath, null, isTransfer: false, isInclude: true);
	}

	/// <summary>Decodes a URL string token to its equivalent byte array using base 64 digits.</summary>
	/// <param name="input">The URL string token to decode.</param>
	/// <returns>The byte array containing the decoded URL string token.</returns>
	/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="input" /> parameter is <see langword="null" />.</exception>
	public static byte[] UrlTokenDecode(string input)
	{
		if (input == null)
		{
			throw new ArgumentNullException("input");
		}
		if (input.Length < 1)
		{
			return new byte[0];
		}
		byte[] bytes = Encoding.ASCII.GetBytes(input);
		int num = input.Length - 1;
		int num2 = bytes[num] - 48;
		char[] array = new char[num + num2];
		int i;
		for (i = 0; i < num; i++)
		{
			switch ((char)bytes[i])
			{
			case '-':
				array[i] = '+';
				break;
			case '_':
				array[i] = '/';
				break;
			default:
				array[i] = (char)bytes[i];
				break;
			}
		}
		while (num2 > 0)
		{
			array[i++] = '=';
			num2--;
		}
		return Convert.FromBase64CharArray(array, 0, array.Length);
	}

	/// <summary>Encodes a byte array into its equivalent string representation using base 64 digits, which is usable for transmission on the URL.</summary>
	/// <param name="input">The byte array to encode.</param>
	/// <returns>The string containing the encoded token if the byte array length is greater than one; otherwise, an empty string ("").</returns>
	/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="input" /> parameter is <see langword="null" />.</exception>
	public static string UrlTokenEncode(byte[] input)
	{
		if (input == null)
		{
			throw new ArgumentNullException("input");
		}
		if (input.Length < 1)
		{
			return string.Empty;
		}
		string text = Convert.ToBase64String(input);
		int num;
		if (text == null || (num = text.Length) == 0)
		{
			return string.Empty;
		}
		int num2 = 48;
		while (num > 0 && text[num - 1] == '=')
		{
			num2++;
			num--;
		}
		char[] array = new char[num + 1];
		array[num] = (char)num2;
		for (int i = 0; i < num; i++)
		{
			switch (text[i])
			{
			case '+':
				array[i] = '-';
				break;
			case '/':
				array[i] = '_';
				break;
			default:
				array[i] = text[i];
				break;
			}
		}
		return new string(array);
	}

	/// <summary>URL-decodes a string and returns the decoded string.</summary>
	/// <param name="s">The text string to decode.</param>
	/// <returns>The decoded text.</returns>
	public string UrlDecode(string s)
	{
		HttpRequest request = context.Request;
		if (request != null)
		{
			return HttpUtility.UrlDecode(s, request.ContentEncoding);
		}
		return HttpUtility.UrlDecode(s);
	}

	/// <summary>Decodes an HTML string received in a URL and sends the resulting output to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
	/// <param name="s">The HTML string to decode.</param>
	/// <param name="output">The <see cref="T:System.IO.TextWriter" /> output stream that contains the decoded string.</param>
	public void UrlDecode(string s, TextWriter output)
	{
		if (s != null)
		{
			output.Write(UrlDecode(s));
		}
	}

	/// <summary>URL-encodes a string and returns the encoded string.</summary>
	/// <param name="s">The text to URL-encode.</param>
	/// <returns>The URL-encoded text.</returns>
	public string UrlEncode(string s)
	{
		HttpResponse response = context.Response;
		if (response != null)
		{
			return HttpUtility.UrlEncode(s, response.ContentEncoding);
		}
		return HttpUtility.UrlEncode(s);
	}

	/// <summary>URL-encodes a string and sends the resulting output to a <see cref="T:System.IO.TextWriter" /> output stream.</summary>
	/// <param name="s">The text string to encode.</param>
	/// <param name="output">The <see cref="T:System.IO.TextWriter" /> output stream that contains the encoded string.</param>
	public void UrlEncode(string s, TextWriter output)
	{
		if (s != null)
		{
			output.Write(UrlEncode(s));
		}
	}

	/// <summary>Do not use; intended only for browser compatibility. Use <see cref="M:System.Web.HttpServerUtility.UrlEncode(System.String)" />.</summary>
	/// <param name="s">The text to URL-encode.</param>
	/// <returns>The URL encoded text.</returns>
	public string UrlPathEncode(string s)
	{
		if (s == null)
		{
			return null;
		}
		int num = s.IndexOf('?');
		string text = null;
		if (num != -1)
		{
			text = s.Substring(0, num);
			return HttpUtility.UrlEncode(text) + s.Substring(num);
		}
		return HttpUtility.UrlEncode(s);
	}
}
