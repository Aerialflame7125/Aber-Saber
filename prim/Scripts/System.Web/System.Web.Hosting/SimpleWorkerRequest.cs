using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Hosting;

/// <summary>Provides a simple implementation of the <see cref="T:System.Web.HttpWorkerRequest" /> abstract class that can be used to host ASP.NET applications outside an Internet Information Services (IIS) application. You can employ <see langword="SimpleWorkerRequest" /> directly or extend it.</summary>
[ComVisible(false)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class SimpleWorkerRequest : HttpWorkerRequest
{
	private string page;

	private string query;

	private string app_virtual_dir;

	private string app_physical_dir;

	private string path_info;

	private TextWriter output;

	private bool hosted;

	private string raw_url;

	/// <summary>Gets the full physical path to the Machine.config file.</summary>
	/// <returns>The physical path to the Machine.config file.</returns>
	public override string MachineConfigPath
	{
		get
		{
			if (hosted)
			{
				string machineConfigPath = ICalls.GetMachineConfigPath();
				if (SecurityManager.SecurityEnabled && machineConfigPath != null && machineConfigPath.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, machineConfigPath).Demand();
				}
				return machineConfigPath;
			}
			return null;
		}
	}

	/// <summary>Gets the physical path to the directory where the ASP.NET binaries are installed.</summary>
	/// <returns>The physical directory to the ASP.NET binary files.</returns>
	public override string MachineInstallDirectory
	{
		get
		{
			if (hosted)
			{
				string machineInstallDirectory = ICalls.GetMachineInstallDirectory();
				if (SecurityManager.SecurityEnabled && machineInstallDirectory != null && machineInstallDirectory.Length > 0)
				{
					new FileIOPermission(FileIOPermissionAccess.PathDiscovery, machineInstallDirectory).Demand();
				}
				return machineInstallDirectory;
			}
			return null;
		}
	}

	/// <summary>Gets the full physical path to the root Web.config file.</summary>
	/// <returns>The physical path to the root Web.config file.</returns>
	public override string RootWebConfigPath => WebConfigurationManager.OpenWebConfiguration("~").FilePath;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.SimpleWorkerRequest" /> class when the target application domain has been created using the <see cref="M:System.Web.Hosting.ApplicationHost.CreateApplicationHost(System.Type,System.String,System.String)" /> method.</summary>
	/// <param name="page">The page to be requested (or the virtual path to the page, relative to the application directory). </param>
	/// <param name="query">The text of the query string. </param>
	/// <param name="output">A <see cref="T:System.IO.TextWriter" /> that captures output from the response </param>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public SimpleWorkerRequest(string page, string query, TextWriter output)
	{
		this.page = page;
		this.query = query;
		this.output = output;
		app_virtual_dir = HttpRuntime.AppDomainAppVirtualPath;
		app_physical_dir = HttpRuntime.AppDomainAppPath;
		hosted = true;
		InitializePaths();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Hosting.SimpleWorkerRequest" /> class for use in an arbitrary application domain, when the user code creates an <see cref="T:System.Web.HttpContext" /> (passing the <see langword="SimpleWorkerRequest" /> as an argument to the <see langword="HttpContext" /> constructor).</summary>
	/// <param name="appVirtualDir">The virtual path to the application directory; for example, "/app". </param>
	/// <param name="appPhysicalDir">The physical path to the application directory; for example, "c:\app". </param>
	/// <param name="page">The virtual path for the request (relative to the application directory). </param>
	/// <param name="query">The text of the query string. </param>
	/// <param name="output">A <see cref="T:System.IO.TextWriter" /> that captures the output from the response. </param>
	/// <exception cref="T:System.Web.HttpException">The <paramref name="appVirtualDir" /> parameter cannot be overridden in this context.</exception>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public SimpleWorkerRequest(string appVirtualDir, string appPhysicalDir, string page, string query, TextWriter output)
	{
		this.page = page;
		this.query = query;
		this.output = output;
		app_virtual_dir = appVirtualDir;
		app_physical_dir = appPhysicalDir;
		InitializePaths();
	}

	private void InitializePaths()
	{
		int num = page.IndexOf('/');
		if (num >= 0)
		{
			path_info = page.Substring(num);
			page = page.Substring(0, num);
		}
		else
		{
			path_info = "";
		}
	}

	/// <summary>Notifies the <see cref="T:System.Web.HttpWorkerRequest" /> that request processing for the current request is complete.</summary>
	public override void EndOfRequest()
	{
	}

	/// <summary>Sends all pending response data to the client.</summary>
	/// <param name="finalFlush">
	///       <see langword="true" /> if this is the last time response data will be flushed; otherwise, <see langword="false" />. </param>
	public override void FlushResponse(bool finalFlush)
	{
	}

	/// <summary>Returns the virtual path to the currently executing server application.</summary>
	/// <returns>The virtual path of the current application.</returns>
	public override string GetAppPath()
	{
		return app_virtual_dir;
	}

	/// <summary>Returns the UNC-translated path to the currently executing server application.</summary>
	/// <returns>The physical path of the current application.</returns>
	public override string GetAppPathTranslated()
	{
		if (SecurityManager.SecurityEnabled && app_physical_dir != null && app_physical_dir.Length > 0)
		{
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, app_physical_dir).Demand();
		}
		return app_physical_dir;
	}

	/// <summary>Returns the physical path to the requested URI.</summary>
	/// <returns>The physical path to the requested URI.</returns>
	public override string GetFilePath()
	{
		string text = UrlUtils.Combine(app_virtual_dir, page);
		if (text == "")
		{
			if (!(app_virtual_dir == "/"))
			{
				return app_virtual_dir + "/";
			}
			return app_virtual_dir;
		}
		return text;
	}

	/// <summary>Returns the physical file path to the requested URI (and translates it from virtual path to physical path: for example, "/proj1/page.aspx" to "c:\dir\page.aspx") </summary>
	/// <returns>The translated physical file path to the requested URI.</returns>
	public override string GetFilePathTranslated()
	{
		string text = Path.Combine(path2: (Path.DirectorySeparatorChar != '\\') ? page : page.Replace('/', '\\'), path1: app_physical_dir);
		if (SecurityManager.SecurityEnabled && text != null && text.Length > 0)
		{
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, text).Demand();
		}
		return text;
	}

	/// <summary>Returns the HTTP request verb.</summary>
	/// <returns>The HTTP verb for this request.</returns>
	public override string GetHttpVerbName()
	{
		return "GET";
	}

	/// <summary>Returns the HTTP version string of the request (for example, "HTTP/1.1").</summary>
	/// <returns>The HTTP version string returned in the request header.</returns>
	public override string GetHttpVersion()
	{
		return "HTTP/1.0";
	}

	/// <summary>Returns the server IP address of the interface on which the request was received.</summary>
	/// <returns>The server IP address of the interface on which the request was received.</returns>
	public override string GetLocalAddress()
	{
		return "127.0.0.1";
	}

	/// <summary>Returns the port number on which the request was received.</summary>
	/// <returns>The server port number on which the request was received.</returns>
	public override int GetLocalPort()
	{
		return 80;
	}

	/// <summary>Returns additional path information for a resource with a URL extension. That is, for the path /virdir/page.html/tail, the return value is /tail.</summary>
	/// <returns>Additional path information for a resource.</returns>
	public override string GetPathInfo()
	{
		return path_info;
	}

	/// <summary>Returns the query string specified in the request URL.</summary>
	/// <returns>The request query string.</returns>
	public override string GetQueryString()
	{
		return query;
	}

	/// <summary>Returns the URL path contained in the header with the query string appended.</summary>
	/// <returns>The raw URL path of the request header.The returned URL is not normalized. Using the URL for access control, or security-sensitive decisions can expose your application to canonicalization security vulnerabilities.</returns>
	public override string GetRawUrl()
	{
		if (raw_url == null)
		{
			string text = ((query == null || query == "") ? "" : ("?" + query));
			raw_url = UrlUtils.Combine(app_virtual_dir, page);
			if (path_info != "")
			{
				raw_url = raw_url + "/" + path_info + text;
			}
			else
			{
				raw_url += text;
			}
		}
		return raw_url;
	}

	/// <summary>Returns the IP address of the client.</summary>
	/// <returns>The client's IP address.</returns>
	public override string GetRemoteAddress()
	{
		return "127.0.0.1";
	}

	/// <summary>Returns the client's port number.</summary>
	/// <returns>The client's port number.</returns>
	public override int GetRemotePort()
	{
		return 0;
	}

	/// <summary>Returns a single server variable from a dictionary of server variables associated with the request.</summary>
	/// <param name="name">The name of the requested server variable. </param>
	/// <returns>The requested server variable.</returns>
	public override string GetServerVariable(string name)
	{
		return "";
	}

	/// <summary>Returns the virtual path to the requested URI.</summary>
	/// <returns>The path to the requested URI.</returns>
	public override string GetUriPath()
	{
		if (app_virtual_dir == "/")
		{
			return app_virtual_dir + page + path_info;
		}
		return app_virtual_dir + "/" + page + path_info;
	}

	/// <summary>Returns the client's impersonation token.</summary>
	/// <returns>A value representing the client's impersonation token. The default is <see cref="F:System.IntPtr.Zero" />.</returns>
	public override IntPtr GetUserToken()
	{
		return IntPtr.Zero;
	}

	/// <summary>Returns the physical path corresponding to the specified virtual path.</summary>
	/// <param name="path">The virtual path. </param>
	/// <returns>The physical path that corresponds to the virtual path specified in the <paramref name="path" /> parameter.</returns>
	public override string MapPath(string path)
	{
		if (!hosted)
		{
			return null;
		}
		if (path != null && path.Length == 0)
		{
			return app_physical_dir;
		}
		if (!path.StartsWith(app_virtual_dir))
		{
			throw new ArgumentNullException("path is not rooted in the virtual directory");
		}
		string text = path.Substring(app_virtual_dir.Length);
		if (text.Length > 0 && text[0] == '/')
		{
			text = text.Substring(1);
		}
		if (Path.DirectorySeparatorChar != '/')
		{
			text = text.Replace('/', Path.DirectorySeparatorChar);
		}
		return Path.Combine(app_physical_dir, text);
	}

	/// <summary>Adds a standard HTTP header to the response.</summary>
	/// <param name="index">The header index. For example, <see cref="F:System.Web.HttpWorkerRequest.HeaderContentLength" />. </param>
	/// <param name="value">The header value. </param>
	public override void SendKnownResponseHeader(int index, string value)
	{
	}

	/// <summary>Adds the contents of the file with the specified handle to the response and specifies the starting position in the file and the number of bytes to send.</summary>
	/// <param name="handle">The handle of the file to send. </param>
	/// <param name="offset">The starting position in the file. </param>
	/// <param name="length">The number of bytes to send. </param>
	public override void SendResponseFromFile(IntPtr handle, long offset, long length)
	{
	}

	/// <summary>Adds the contents of the file with the specified name to the response and specifies the starting position in the file and the number of bytes to send.</summary>
	/// <param name="filename">The name of the file to send. </param>
	/// <param name="offset">The starting position in the file. </param>
	/// <param name="length">The number of bytes to send. </param>
	public override void SendResponseFromFile(string filename, long offset, long length)
	{
	}

	/// <summary>Adds the contents of a byte array to the response and specifies the number of bytes to send.</summary>
	/// <param name="data">The byte array to send. </param>
	/// <param name="length">The number of bytes to send. </param>
	public override void SendResponseFromMemory(byte[] data, int length)
	{
		output.Write(Encoding.Default.GetChars(data, 0, length));
	}

	/// <summary>Specifies the HTTP status code and status description of the response; for example, SendStatus(200, "Ok").</summary>
	/// <param name="statusCode">The status code to send </param>
	/// <param name="statusDescription">The status description to send. </param>
	public override void SendStatus(int statusCode, string statusDescription)
	{
	}

	/// <summary>Adds a nonstandard HTTP header to the response.</summary>
	/// <param name="name">The name of the header to send.</param>
	/// <param name="value">The value of the header.</param>
	public override void SendUnknownResponseHeader(string name, string value)
	{
	}
}
