using System.ComponentModel;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Web.Services.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;

namespace System.Web.Services.Discovery;

/// <summary>An ASP.NET HTTP handler that processes a request for a Web services discovery document.</summary>
public sealed class DiscoveryRequestHandler : IHttpHandler
{
	/// <summary>Gets a value of <see langword="true" />, indicates whether the instance of <see cref="T:System.Web.Services.Discovery.DiscoveryRequestHandler" /> (or a derived class) is reusable. </summary>
	/// <returns>This property always returns <see langword="true" />.</returns>
	public bool IsReusable => true;

	/// <summary>Handles an HTTP request for a discovery document, which is serialized to the HTTP response.</summary>
	/// <param name="context">The <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties of the <see cref="T:System.Web.HttpContext" /> class are used for input and output, respectively.</param>
	public void ProcessRequest(HttpContext context)
	{
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "ProcessRequest") : null);
		if (Tracing.On)
		{
			Tracing.Enter("IHttpHandler.ProcessRequest", caller, Tracing.Details(context.Request));
		}
		new PermissionSet(PermissionState.Unrestricted).Demand();
		string physicalPath = context.Request.PhysicalPath;
		_ = System.ComponentModel.CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
		if (File.Exists(physicalPath))
		{
			DynamicDiscoveryDocument dynamicDiscoveryDocument = null;
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(physicalPath, FileMode.Open, FileAccess.Read);
				if (new XmlTextReader(fileStream)
				{
					WhitespaceHandling = WhitespaceHandling.Significant,
					XmlResolver = null,
					DtdProcessing = DtdProcessing.Prohibit
				}.IsStartElement("dynamicDiscovery", "urn:schemas-dynamicdiscovery:disco.2000-03-17"))
				{
					fileStream.Position = 0L;
					dynamicDiscoveryDocument = DynamicDiscoveryDocument.Load(fileStream);
				}
			}
			finally
			{
				fileStream?.Close();
			}
			if (dynamicDiscoveryDocument != null)
			{
				string[] array = new string[dynamicDiscoveryDocument.ExcludePaths.Length];
				string directoryName = Path.GetDirectoryName(physicalPath);
				string fileToSkipAtBegin = Path.GetFileName(physicalPath);
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = dynamicDiscoveryDocument.ExcludePaths[i].Path;
				}
				Uri url = context.Request.Url;
				string str = RuntimeUtils.EscapeUri(url);
				string dirPartOfPath = GetDirPartOfPath(str);
				DynamicDiscoSearcher dynamicDiscoSearcher;
				if (GetDirPartOfPath(url.LocalPath).Length == 0 || System.ComponentModel.CompModSwitches.DynamicDiscoveryVirtualSearch.Enabled)
				{
					fileToSkipAtBegin = GetFilePartOfPath(str);
					dynamicDiscoSearcher = new DynamicVirtualDiscoSearcher(directoryName, array, dirPartOfPath);
				}
				else
				{
					dynamicDiscoSearcher = new DynamicPhysicalDiscoSearcher(directoryName, array, dirPartOfPath);
				}
				_ = System.ComponentModel.CompModSwitches.DynamicDiscoverySearcher.TraceVerbose;
				dynamicDiscoSearcher.Search(fileToSkipAtBegin);
				DiscoveryDocument discoveryDocument = dynamicDiscoSearcher.DiscoveryDocument;
				MemoryStream memoryStream = new MemoryStream(1024);
				StreamWriter writer = new StreamWriter(memoryStream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
				discoveryDocument.Write(writer);
				memoryStream.Position = 0L;
				byte[] array2 = new byte[(int)memoryStream.Length];
				int count = memoryStream.Read(array2, 0, array2.Length);
				context.Response.ContentType = ContentType.Compose("text/xml", Encoding.UTF8);
				context.Response.OutputStream.Write(array2, 0, count);
			}
			else
			{
				context.Response.ContentType = "text/xml";
				context.Response.WriteFile(physicalPath);
			}
			if (Tracing.On)
			{
				Tracing.Exit("IHttpHandler.ProcessRequest", caller);
			}
			return;
		}
		if (Tracing.On)
		{
			Tracing.Exit("IHttpHandler.ProcessRequest", caller);
		}
		throw new HttpException(404, Res.GetString("WebPathNotFound", context.Request.Path));
	}

	private static string GetDirPartOfPath(string str)
	{
		int num = str.LastIndexOf('/');
		if (num <= 0)
		{
			return "";
		}
		return str.Substring(0, num);
	}

	private static string GetFilePartOfPath(string str)
	{
		int num = str.LastIndexOf('/');
		if (num < 0)
		{
			return str;
		}
		if (num == str.Length - 1)
		{
			return "";
		}
		return str.Substring(num + 1);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryRequestHandler" /> class. </summary>
	public DiscoveryRequestHandler()
	{
	}
}
