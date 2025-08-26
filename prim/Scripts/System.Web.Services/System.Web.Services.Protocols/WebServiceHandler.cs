using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Web.Services.Diagnostics;
using System.Web.Util;

namespace System.Web.Services.Protocols;

internal class WebServiceHandler
{
	private ServerProtocol protocol;

	private Exception exception;

	private AsyncCallback asyncCallback;

	private ManualResetEvent asyncBeginComplete;

	private int asyncCallbackCalls;

	private bool wroteException;

	private object[] parameters;

	internal WebServiceHandler(ServerProtocol protocol)
	{
		this.protocol = protocol;
	}

	private static void TraceFlush()
	{
	}

	private void PrepareContext()
	{
		exception = null;
		wroteException = false;
		asyncCallback = null;
		asyncBeginComplete = new ManualResetEvent(initialState: false);
		asyncCallbackCalls = 0;
		if (protocol.IsOneWay)
		{
			return;
		}
		HttpContext context = protocol.Context;
		if (context != null)
		{
			int cacheDuration = protocol.MethodAttribute.CacheDuration;
			if (cacheDuration > 0)
			{
				context.Response.Cache.SetCacheability(HttpCacheability.Server);
				context.Response.Cache.SetExpires(DateTime.Now.AddSeconds(cacheDuration));
				context.Response.Cache.SetSlidingExpiration(slide: false);
				context.Response.Cache.VaryByHeaders["Content-type"] = true;
				context.Response.Cache.VaryByHeaders["SOAPAction"] = true;
				context.Response.Cache.VaryByParams["*"] = true;
			}
			else
			{
				context.Response.Cache.SetNoServerCaching();
				context.Response.Cache.SetMaxAge(TimeSpan.Zero);
			}
			context.Response.BufferOutput = protocol.MethodAttribute.BufferResponse;
			context.Response.ContentType = null;
		}
	}

	private void WriteException(Exception e)
	{
		if (!wroteException)
		{
			_ = System.ComponentModel.CompModSwitches.Remote.TraceVerbose;
			if (e is TargetInvocationException)
			{
				_ = System.ComponentModel.CompModSwitches.Remote.TraceVerbose;
				e = e.InnerException;
			}
			wroteException = protocol.WriteException(e, protocol.Response.OutputStream);
			if (!wroteException)
			{
				throw e;
			}
		}
	}

	private void Invoke()
	{
		PrepareContext();
		protocol.CreateServerInstance();
		try
		{
			TraceMethod caller = (Tracing.On ? new TraceMethod(this, "Invoke") : null);
			TraceMethod callDetails = (Tracing.On ? new TraceMethod(protocol.Target, protocol.MethodInfo.Name, parameters) : null);
			if (Tracing.On)
			{
				Tracing.Enter(protocol.MethodInfo.ToString(), caller, callDetails);
			}
			object[] returnValues = protocol.MethodInfo.Invoke(protocol.Target, parameters);
			if (Tracing.On)
			{
				Tracing.Exit(protocol.MethodInfo.ToString(), caller);
			}
			WriteReturns(returnValues);
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Error, this, "Invoke", ex);
			}
			if (!protocol.IsOneWay)
			{
				WriteException(ex);
				throw;
			}
		}
		finally
		{
			protocol.DisposeServerInstance();
		}
	}

	private void InvokeTransacted()
	{
		Transactions.InvokeTransacted(Invoke, protocol.MethodAttribute.TransactionOption);
	}

	private void ThrowInitException()
	{
		HandleOneWayException(new Exception(Res.GetString("WebConfigExtensionError"), protocol.OnewayInitException), "ThrowInitException");
	}

	private void HandleOneWayException(Exception e, string method)
	{
		if (Tracing.On)
		{
			Tracing.ExceptionCatch(TraceEventType.Error, this, string.IsNullOrEmpty(method) ? "HandleOneWayException" : method, e);
		}
	}

	protected void CoreProcessRequest()
	{
		try
		{
			bool transactionEnabled = protocol.MethodAttribute.TransactionEnabled;
			if (protocol.IsOneWay)
			{
				WorkItemCallback workItemCallback = null;
				TraceMethod traceMethod = null;
				if (protocol.OnewayInitException != null)
				{
					workItemCallback = ThrowInitException;
					traceMethod = (Tracing.On ? new TraceMethod(this, "ThrowInitException") : null);
				}
				else
				{
					parameters = protocol.ReadParameters();
					workItemCallback = (transactionEnabled ? new WorkItemCallback(OneWayInvokeTransacted) : new WorkItemCallback(OneWayInvoke));
					traceMethod = ((!Tracing.On) ? null : (transactionEnabled ? new TraceMethod(this, "OneWayInvokeTransacted") : new TraceMethod(this, "OneWayInvoke")));
				}
				if (Tracing.On)
				{
					Tracing.Information("TracePostWorkItemIn", traceMethod);
				}
				WorkItem.Post(workItemCallback);
				if (Tracing.On)
				{
					Tracing.Information("TracePostWorkItemOut", traceMethod);
				}
				protocol.WriteOneWayResponse();
			}
			else if (transactionEnabled)
			{
				parameters = protocol.ReadParameters();
				InvokeTransacted();
			}
			else
			{
				parameters = protocol.ReadParameters();
				Invoke();
			}
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Error, this, "CoreProcessRequest", ex);
			}
			if (!protocol.IsOneWay)
			{
				WriteException(ex);
			}
		}
		TraceFlush();
	}

	private HttpContext SwitchContext(HttpContext context)
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		HttpContext current = HttpContext.Current;
		HttpContext.Current = context;
		return current;
	}

	private void OneWayInvoke()
	{
		HttpContext httpContext = null;
		if (protocol.Context != null)
		{
			httpContext = SwitchContext(protocol.Context);
		}
		try
		{
			Invoke();
		}
		catch (Exception e)
		{
			HandleOneWayException(e, "OneWayInvoke");
		}
		finally
		{
			if (httpContext != null)
			{
				SwitchContext(httpContext);
			}
		}
	}

	private void OneWayInvokeTransacted()
	{
		HttpContext httpContext = null;
		if (protocol.Context != null)
		{
			httpContext = SwitchContext(protocol.Context);
		}
		try
		{
			InvokeTransacted();
		}
		catch (Exception e)
		{
			HandleOneWayException(e, "OneWayInvokeTransacted");
		}
		finally
		{
			if (httpContext != null)
			{
				SwitchContext(httpContext);
			}
		}
	}

	private void Callback(IAsyncResult result)
	{
		if (!result.CompletedSynchronously)
		{
			asyncBeginComplete.WaitOne();
		}
		DoCallback(result);
	}

	private void DoCallback(IAsyncResult result)
	{
		if (asyncCallback != null && Interlocked.Increment(ref asyncCallbackCalls) == 1)
		{
			asyncCallback(result);
		}
	}

	protected IAsyncResult BeginCoreProcessRequest(AsyncCallback callback, object asyncState)
	{
		if (protocol.MethodAttribute.TransactionEnabled)
		{
			throw new InvalidOperationException(Res.GetString("WebAsyncTransaction"));
		}
		parameters = protocol.ReadParameters();
		IAsyncResult asyncResult;
		if (protocol.IsOneWay)
		{
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "OneWayAsyncInvoke") : null);
			if (Tracing.On)
			{
				Tracing.Information("TracePostWorkItemIn", traceMethod);
			}
			WorkItem.Post(OneWayAsyncInvoke);
			if (Tracing.On)
			{
				Tracing.Information("TracePostWorkItemOut", traceMethod);
			}
			asyncResult = new CompletedAsyncResult(asyncState, completedSynchronously: true);
			callback?.Invoke(asyncResult);
		}
		else
		{
			asyncResult = BeginInvoke(callback, asyncState);
		}
		return asyncResult;
	}

	private void OneWayAsyncInvoke()
	{
		if (protocol.OnewayInitException != null)
		{
			HandleOneWayException(new Exception(Res.GetString("WebConfigExtensionError"), protocol.OnewayInitException), "OneWayAsyncInvoke");
			return;
		}
		HttpContext httpContext = null;
		if (protocol.Context != null)
		{
			httpContext = SwitchContext(protocol.Context);
		}
		try
		{
			BeginInvoke(OneWayCallback, null);
		}
		catch (Exception e)
		{
			HandleOneWayException(e, "OneWayAsyncInvoke");
		}
		finally
		{
			if (httpContext != null)
			{
				SwitchContext(httpContext);
			}
		}
	}

	private IAsyncResult BeginInvoke(AsyncCallback callback, object asyncState)
	{
		IAsyncResult asyncResult;
		try
		{
			PrepareContext();
			protocol.CreateServerInstance();
			asyncCallback = callback;
			TraceMethod caller = (Tracing.On ? new TraceMethod(this, "BeginInvoke") : null);
			TraceMethod callDetails = (Tracing.On ? new TraceMethod(protocol.Target, protocol.MethodInfo.Name, parameters) : null);
			if (Tracing.On)
			{
				Tracing.Enter(protocol.MethodInfo.ToString(), caller, callDetails);
			}
			asyncResult = protocol.MethodInfo.BeginInvoke(protocol.Target, parameters, Callback, asyncState);
			if (Tracing.On)
			{
				Tracing.Enter(protocol.MethodInfo.ToString(), caller);
			}
			if (asyncResult == null)
			{
				throw new InvalidOperationException(Res.GetString("WebNullAsyncResultInBegin"));
			}
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Error, this, "BeginInvoke", ex);
			}
			exception = ex;
			asyncResult = new CompletedAsyncResult(asyncState, completedSynchronously: true);
			asyncCallback = callback;
			DoCallback(asyncResult);
		}
		asyncBeginComplete.Set();
		TraceFlush();
		return asyncResult;
	}

	private void OneWayCallback(IAsyncResult asyncResult)
	{
		EndInvoke(asyncResult);
	}

	protected void EndCoreProcessRequest(IAsyncResult asyncResult)
	{
		if (asyncResult != null)
		{
			if (protocol.IsOneWay)
			{
				protocol.WriteOneWayResponse();
			}
			else
			{
				EndInvoke(asyncResult);
			}
		}
	}

	private void EndInvoke(IAsyncResult asyncResult)
	{
		try
		{
			if (exception != null)
			{
				throw exception;
			}
			object[] returnValues = protocol.MethodInfo.EndInvoke(protocol.Target, asyncResult);
			WriteReturns(returnValues);
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Error, this, "EndInvoke", ex);
			}
			if (!protocol.IsOneWay)
			{
				WriteException(ex);
			}
		}
		finally
		{
			protocol.DisposeServerInstance();
		}
		TraceFlush();
	}

	private void WriteReturns(object[] returnValues)
	{
		if (!protocol.IsOneWay)
		{
			bool bufferResponse = protocol.MethodAttribute.BufferResponse;
			Stream stream = protocol.Response.OutputStream;
			if (!bufferResponse)
			{
				stream = new BufferedResponseStream(stream, 16384);
				((BufferedResponseStream)stream).FlushEnabled = false;
			}
			protocol.WriteReturns(returnValues, stream);
			if (!bufferResponse)
			{
				((BufferedResponseStream)stream).FlushEnabled = true;
				stream.Flush();
			}
		}
	}
}
