using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Diagnostics;
using System.Web.UI;

namespace System.Web.Services.Protocols;

/// <summary>Dynamically manufactures Web service handler instances, whose type or types implement the <see cref="T:System.Web.IHttpHandler" /> interface.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
public class WebServiceHandlerFactory : IHttpHandlerFactory
{
	/// <summary>Returns an <see cref="T:System.Web.IHttpHandler" /> instance.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that provides references to intrinsic server objects (For example, <see cref="P:System.Web.HttpContext.Request" />, <see cref="P:System.Web.HttpContext.Response" />, <see cref="P:System.Web.HttpContext.Session" />, and <see cref="P:System.Web.HttpContext.Server" />) used to service HTTP requests.</param>
	/// <param name="verb">The HTTP data transfer method (GET or POST) that the client uses.</param>
	/// <param name="url">The raw URL of the requested resource.</param>
	/// <param name="filePath">The file-system path of the requested resource.</param>
	/// <returns>An <see cref="T:System.Web.IHttpHandler" /> instance.</returns>
	public IHttpHandler GetHandler(HttpContext context, string verb, string url, string filePath)
	{
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "GetHandler") : null);
		if (Tracing.On)
		{
			Tracing.Enter("IHttpHandlerFactory.GetHandler", caller, Tracing.Details(context.Request));
		}
		new AspNetHostingPermission(AspNetHostingPermissionLevel.Minimal).Demand();
		Type compiledType = GetCompiledType(url, context);
		IHttpHandler result = CoreGetHandler(compiledType, context, context.Request, context.Response);
		if (Tracing.On)
		{
			Tracing.Exit("IHttpHandlerFactory.GetHandler", caller);
		}
		return result;
	}

	[SecurityPermission(SecurityAction.Assert, Unrestricted = true)]
	private Type GetCompiledType(string url, HttpContext context)
	{
		return WebServiceParser.GetCompiledType(url, context);
	}

	internal IHttpHandler CoreGetHandler(Type type, HttpContext context, HttpRequest request, HttpResponse response)
	{
		TraceMethod method = (Tracing.On ? new TraceMethod(this, "CoreGetHandler") : null);
		ServerProtocolFactory[] serverProtocolFactories = GetServerProtocolFactories();
		ServerProtocol serverProtocol = null;
		bool abortProcessing = false;
		for (int i = 0; i < serverProtocolFactories.Length; i++)
		{
			try
			{
				serverProtocol = serverProtocolFactories[i].Create(type, context, request, response, out abortProcessing);
				if ((serverProtocol != null && serverProtocol.GetType() != typeof(UnsupportedRequestProtocol)) || abortProcessing)
				{
					break;
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw Tracing.ExceptionThrow(method, new InvalidOperationException(Res.GetString("FailedToHandleRequest0"), ex));
			}
		}
		if (abortProcessing)
		{
			return new NopHandler();
		}
		if (serverProtocol == null)
		{
			if (request.PathInfo != null && request.PathInfo.Length != 0)
			{
				throw Tracing.ExceptionThrow(method, new InvalidOperationException(Res.GetString("WebUnrecognizedRequestFormatUrl", request.PathInfo)));
			}
			throw Tracing.ExceptionThrow(method, new InvalidOperationException(Res.GetString("WebUnrecognizedRequestFormat")));
		}
		if (serverProtocol is UnsupportedRequestProtocol)
		{
			throw Tracing.ExceptionThrow(method, new HttpException(((UnsupportedRequestProtocol)serverProtocol).HttpCode, Res.GetString("WebUnrecognizedRequestFormat")));
		}
		bool isAsync = serverProtocol.MethodInfo.IsAsync;
		bool enableSession = serverProtocol.MethodAttribute.EnableSession;
		if (isAsync)
		{
			if (enableSession)
			{
				return new AsyncSessionHandler(serverProtocol);
			}
			return new AsyncSessionlessHandler(serverProtocol);
		}
		if (enableSession)
		{
			return new SyncSessionHandler(serverProtocol);
		}
		return new SyncSessionlessHandler(serverProtocol);
	}

	[PermissionSet(SecurityAction.Assert, Name = "FullTrust")]
	private ServerProtocolFactory[] GetServerProtocolFactories()
	{
		return WebServicesSection.Current.ServerProtocolFactories;
	}

	/// <summary>Releases the <see cref="T:System.Web.IHttpHandler" /> instance.</summary>
	/// <param name="handler">The <see cref="T:System.Web.IHttpHandler" /> instance to release.</param>
	public void ReleaseHandler(IHttpHandler handler)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.WebServiceHandlerFactory" /> class. </summary>
	public WebServiceHandlerFactory()
	{
	}
}
