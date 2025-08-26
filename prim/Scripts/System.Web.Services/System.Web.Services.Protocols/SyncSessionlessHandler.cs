using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols;

internal class SyncSessionlessHandler : WebServiceHandler, IHttpHandler
{
	public bool IsReusable => false;

	internal SyncSessionlessHandler(ServerProtocol protocol)
		: base(protocol)
	{
	}

	public void ProcessRequest(HttpContext context)
	{
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "ProcessRequest") : null);
		if (Tracing.On)
		{
			Tracing.Enter("IHttpHandler.ProcessRequest", caller, Tracing.Details(context.Request));
		}
		CoreProcessRequest();
		if (Tracing.On)
		{
			Tracing.Exit("IHttpHandler.ProcessRequest", caller);
		}
	}
}
