using System.IO;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Web.UI;

namespace System.Web.Services.Protocols;

internal sealed class DocumentationServerProtocol : ServerProtocol
{
	private DocumentationServerType serverType;

	private IHttpHandler handler;

	private object syncRoot = new object();

	private const int MAX_PATH_SIZE = 1024;

	internal override ServerType ServerType => serverType;

	internal override bool IsOneWay => false;

	internal override LogicalMethodInfo MethodInfo => serverType.MethodInfo;

	internal override bool Initialize()
	{
		if ((serverType = (DocumentationServerType)GetFromCache(typeof(DocumentationServerProtocol), base.Type)) == null && (serverType = (DocumentationServerType)GetFromCache(typeof(DocumentationServerProtocol), base.Type, excludeSchemeHostPort: true)) == null)
		{
			lock (ServerProtocol.InternalSyncObject)
			{
				if ((serverType = (DocumentationServerType)GetFromCache(typeof(DocumentationServerProtocol), base.Type)) == null && (serverType = (DocumentationServerType)GetFromCache(typeof(DocumentationServerProtocol), base.Type, excludeSchemeHostPort: true)) == null)
				{
					bool flag = IsCacheUnderPressure(typeof(DocumentationServerProtocol), base.Type);
					string uri = RuntimeUtils.EscapeUri(base.Request.Url);
					serverType = new DocumentationServerType(base.Type, uri, flag);
					AddToCache(typeof(DocumentationServerProtocol), base.Type, serverType, flag);
				}
			}
		}
		WebServicesSection current = WebServicesSection.Current;
		if (current.WsdlHelpGenerator.Href != null && current.WsdlHelpGenerator.Href.Length > 0)
		{
			TraceMethod caller = (Tracing.On ? new TraceMethod(this, "Initialize") : null);
			if (Tracing.On)
			{
				Tracing.Enter("ASP.NET", caller, new TraceMethod(typeof(PageParser), "GetCompiledPageInstance", current.WsdlHelpGenerator.HelpGeneratorVirtualPath, current.WsdlHelpGenerator.HelpGeneratorPath, base.Context));
			}
			handler = GetCompiledPageInstance(current.WsdlHelpGenerator.HelpGeneratorVirtualPath, current.WsdlHelpGenerator.HelpGeneratorPath, base.Context);
			if (Tracing.On)
			{
				Tracing.Exit("ASP.NET", caller);
			}
		}
		return true;
	}

	[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	private IHttpHandler GetCompiledPageInstance(string virtualPath, string inputFile, HttpContext context)
	{
		return PageParser.GetCompiledPageInstance(virtualPath, inputFile, context);
	}

	internal override object[] ReadParameters()
	{
		return new object[0];
	}

	internal override void WriteReturns(object[] returnValues, Stream outputStream)
	{
		try
		{
			if (handler == null)
			{
				return;
			}
			base.Context.Items.Add("wsdls", serverType.ServiceDescriptions);
			base.Context.Items.Add("schemas", serverType.Schemas);
			if (base.Context.Request.Url.IsLoopback || base.Context.Request.IsLocal)
			{
				base.Context.Items.Add("wsdlsWithPost", serverType.ServiceDescriptionsWithPost);
				base.Context.Items.Add("schemasWithPost", serverType.SchemasWithPost);
			}
			base.Context.Items.Add("conformanceWarnings", WebServicesSection.Current.EnabledConformanceWarnings);
			base.Response.ContentType = "text/html";
			if (serverType.UriFixups == null)
			{
				handler.ProcessRequest(base.Context);
				return;
			}
			lock (syncRoot)
			{
				RunUriFixups();
				handler.ProcessRequest(base.Context);
			}
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			throw new InvalidOperationException(Res.GetString("HelpGeneratorInternalError"), ex);
		}
	}

	internal override bool WriteException(Exception e, Stream outputStream)
	{
		return false;
	}

	internal void Documentation()
	{
	}

	private void RunUriFixups()
	{
		foreach (Action<Uri> uriFixup in serverType.UriFixups)
		{
			uriFixup(base.Context.Request.Url);
		}
	}
}
