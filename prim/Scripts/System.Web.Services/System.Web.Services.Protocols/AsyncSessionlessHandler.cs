using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols;

internal class AsyncSessionlessHandler : SyncSessionlessHandler, IHttpAsyncHandler, IHttpHandler
{
	internal AsyncSessionlessHandler(ServerProtocol protocol)
		: base(protocol)
	{
	}

	public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object asyncState)
	{
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "BeginProcessRequest") : null);
		if (Tracing.On)
		{
			Tracing.Enter("IHttpAsyncHandler.BeginProcessRequest", caller, Tracing.Details(context.Request));
		}
		IAsyncResult result = BeginCoreProcessRequest(callback, asyncState);
		if (Tracing.On)
		{
			Tracing.Exit("IHttpAsyncHandler.BeginProcessRequest", caller);
		}
		return result;
	}

	public void EndProcessRequest(IAsyncResult asyncResult)
	{
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "EndProcessRequest") : null);
		if (Tracing.On)
		{
			Tracing.Enter("IHttpAsyncHandler.EndProcessRequest", caller);
		}
		EndCoreProcessRequest(asyncResult);
		if (Tracing.On)
		{
			Tracing.Exit("IHttpAsyncHandler.EndProcessRequest", caller);
		}
	}
}
