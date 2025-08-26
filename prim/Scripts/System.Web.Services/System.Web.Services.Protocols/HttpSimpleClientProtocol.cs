using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols;

/// <summary>Represents the base class for communicating with an XML Web service using the simple HTTP-GET and HTTP-POST protocols bindings.</summary>
[ComVisible(true)]
public abstract class HttpSimpleClientProtocol : HttpWebClientProtocol
{
	private class InvokeAsyncState
	{
		internal object[] Parameters;

		internal MimeParameterWriter ParamWriter;

		internal HttpClientMethod Method;

		internal InvokeAsyncState(HttpClientMethod method, MimeParameterWriter paramWriter, object[] parameters)
		{
			Method = method;
			ParamWriter = paramWriter;
			Parameters = parameters;
		}
	}

	private HttpClientType clientType;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HttpSimpleClientProtocol" /> class.</summary>
	protected HttpSimpleClientProtocol()
	{
		Type type = GetType();
		clientType = (HttpClientType)WebClientProtocol.GetFromCache(type);
		if (clientType != null)
		{
			return;
		}
		lock (WebClientProtocol.InternalSyncObject)
		{
			clientType = (HttpClientType)WebClientProtocol.GetFromCache(type);
			if (clientType == null)
			{
				clientType = new HttpClientType(type);
				WebClientProtocol.AddToCache(type, clientType);
			}
		}
	}

	/// <summary>Invokes an XML Web service method using HTTP.</summary>
	/// <param name="methodName">The name of the XML Web service method in the derived class that is invoking the <see cref="M:System.Web.Services.Protocols.HttpSimpleClientProtocol.Invoke(System.String,System.String,System.Object[])" /> method. </param>
	/// <param name="requestUrl">The URL of the XML Web service method that the client is requesting. </param>
	/// <param name="parameters">An array of objects containing the parameters to pass to the remote XML Web service. The order of the values in the array corresponds to the order of the parameters in the calling method of the derived class. </param>
	/// <returns>An array of objects containing the return value and any by-reference or <paramref name="out" /> parameters of the derived class method.</returns>
	/// <exception cref="T:System.Exception">The request reached the server computer, but was not processed successfully. </exception>
	protected object Invoke(string methodName, string requestUrl, object[] parameters)
	{
		WebResponse webResponse = null;
		HttpClientMethod clientMethod = GetClientMethod(methodName);
		MimeParameterWriter parameterWriter = GetParameterWriter(clientMethod);
		Uri uri = new Uri(requestUrl);
		if (parameterWriter != null)
		{
			parameterWriter.RequestEncoding = base.RequestEncoding;
			requestUrl = parameterWriter.GetRequestUrl(uri.AbsoluteUri, parameters);
			uri = new Uri(requestUrl, dontEscape: true);
		}
		WebRequest webRequest = null;
		try
		{
			webRequest = GetWebRequest(uri);
			NotifyClientCallOut(webRequest);
			base.PendingSyncRequest = webRequest;
			if (parameterWriter != null)
			{
				parameterWriter.InitializeRequest(webRequest, parameters);
				if (parameterWriter.UsesWriteRequest)
				{
					if (parameters.Length == 0)
					{
						webRequest.ContentLength = 0L;
					}
					else
					{
						Stream stream = null;
						try
						{
							stream = webRequest.GetRequestStream();
							parameterWriter.WriteRequest(stream, parameters);
						}
						finally
						{
							stream?.Close();
						}
					}
				}
			}
			webResponse = GetWebResponse(webRequest);
			Stream responseStream = null;
			if (webResponse.ContentLength != 0L)
			{
				responseStream = webResponse.GetResponseStream();
			}
			return ReadResponse(clientMethod, webResponse, responseStream);
		}
		finally
		{
			if (webRequest == base.PendingSyncRequest)
			{
				base.PendingSyncRequest = null;
			}
		}
	}

	/// <summary>Starts an asynchronous invocation of a method of an XML Web service.</summary>
	/// <param name="methodName">The name of the XML Web service method. </param>
	/// <param name="requestUrl">The URL to use when creating the <see cref="T:System.Net.WebRequest" />. </param>
	/// <param name="parameters">An array of objects containing the parameters to pass to the XML Web service method. The order of the values in the array corresponds to the order of the parameters in the calling method of the derived class. </param>
	/// <param name="callback">The delegate to call when the asynchronous method call is complete. If <paramref name="callback" /> is <see langword="null" />, the delegate is not called. </param>
	/// <param name="asyncState">The additional information supplied by a client. </param>
	/// <returns>An <see cref="T:System.IAsyncResult" /> that can be passed to the <see cref="M:System.Web.Services.Protocols.HttpSimpleClientProtocol.EndInvoke(System.IAsyncResult)" /> method to obtain the return values from the XML Web service method.</returns>
	/// <exception cref="T:System.Exception">The request reached the server computer, but was not processed successfully. </exception>
	protected IAsyncResult BeginInvoke(string methodName, string requestUrl, object[] parameters, AsyncCallback callback, object asyncState)
	{
		HttpClientMethod clientMethod = GetClientMethod(methodName);
		MimeParameterWriter parameterWriter = GetParameterWriter(clientMethod);
		Uri uri = new Uri(requestUrl);
		if (parameterWriter != null)
		{
			parameterWriter.RequestEncoding = base.RequestEncoding;
			requestUrl = parameterWriter.GetRequestUrl(uri.AbsoluteUri, parameters);
			uri = new Uri(requestUrl, dontEscape: true);
		}
		InvokeAsyncState internalAsyncState = new InvokeAsyncState(clientMethod, parameterWriter, parameters);
		WebClientAsyncResult asyncResult = new WebClientAsyncResult(this, internalAsyncState, null, callback, asyncState);
		return BeginSend(uri, asyncResult, parameterWriter.UsesWriteRequest);
	}

	internal override void InitializeAsyncRequest(WebRequest request, object internalAsyncState)
	{
		InvokeAsyncState invokeAsyncState = (InvokeAsyncState)internalAsyncState;
		if (invokeAsyncState.ParamWriter.UsesWriteRequest && invokeAsyncState.Parameters.Length == 0)
		{
			request.ContentLength = 0L;
		}
	}

	internal override void AsyncBufferedSerialize(WebRequest request, Stream requestStream, object internalAsyncState)
	{
		InvokeAsyncState invokeAsyncState = (InvokeAsyncState)internalAsyncState;
		if (invokeAsyncState.ParamWriter != null)
		{
			invokeAsyncState.ParamWriter.InitializeRequest(request, invokeAsyncState.Parameters);
			if (invokeAsyncState.ParamWriter.UsesWriteRequest && invokeAsyncState.Parameters.Length != 0)
			{
				invokeAsyncState.ParamWriter.WriteRequest(requestStream, invokeAsyncState.Parameters);
			}
		}
	}

	/// <summary>Completes asynchronous invocation of an XML Web service method using HTTP.</summary>
	/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned from the <see cref="M:System.Web.Services.Protocols.HttpSimpleClientProtocol.BeginInvoke(System.String,System.String,System.Object[],System.AsyncCallback,System.Object)" /> method. </param>
	/// <returns>An array of objects containing the return value and any by reference or <paramref name="out" /> parameters for the XML Web service method.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="asyncResult" /> is not the return value from the <see cref="M:System.Web.Services.Protocols.HttpSimpleClientProtocol.BeginInvoke(System.String,System.String,System.Object[],System.AsyncCallback,System.Object)" /> method. </exception>
	protected object EndInvoke(IAsyncResult asyncResult)
	{
		object internalAsyncState = null;
		Stream responseStream = null;
		WebResponse response = EndSend(asyncResult, ref internalAsyncState, ref responseStream);
		InvokeAsyncState invokeAsyncState = (InvokeAsyncState)internalAsyncState;
		return ReadResponse(invokeAsyncState.Method, response, responseStream);
	}

	private void InvokeAsyncCallback(IAsyncResult result)
	{
		object obj = null;
		Exception e = null;
		WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)result;
		if (webClientAsyncResult.Request != null)
		{
			try
			{
				object internalAsyncState = null;
				Stream responseStream = null;
				WebResponse response = EndSend(webClientAsyncResult, ref internalAsyncState, ref responseStream);
				InvokeAsyncState invokeAsyncState = (InvokeAsyncState)internalAsyncState;
				obj = ReadResponse(invokeAsyncState.Method, response, responseStream);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				e = ex;
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Error, this, "InvokeAsyncCallback", ex);
				}
			}
		}
		UserToken userToken = (UserToken)((AsyncOperation)result.AsyncState).UserSuppliedState;
		OperationCompleted(userToken.UserState, new object[1] { obj }, e, canceled: false);
	}

	/// <summary>Invokes the specified method asynchronously.</summary>
	/// <param name="methodName">The name of the method to invoke.</param>
	/// <param name="requestUrl">The request URL of the invoked web service.</param>
	/// <param name="parameters">The parameters to pass to the method.</param>
	/// <param name="callback">The delegate called when the method invocation has completed.</param>
	protected void InvokeAsync(string methodName, string requestUrl, object[] parameters, SendOrPostCallback callback)
	{
		InvokeAsync(methodName, requestUrl, parameters, callback, null);
	}

	/// <summary>Invokes the specified method asynchronously while maintaining an associated state.</summary>
	/// <param name="methodName">The name of the method to invoke.</param>
	/// <param name="requestUrl">The request URL of the invoked web service.</param>
	/// <param name="parameters">The parameters to pass to the method.</param>
	/// <param name="callback">The delegate called when the method invocation has completed.</param>
	/// <param name="userState">An object containing associated state information that is passed to the <paramref name="callback" /> delegate when the method has completed.</param>
	protected void InvokeAsync(string methodName, string requestUrl, object[] parameters, SendOrPostCallback callback, object userState)
	{
		if (userState == null)
		{
			userState = base.NullToken;
		}
		AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(new UserToken(callback, userState));
		WebClientAsyncResult webClientAsyncResult = new WebClientAsyncResult(this, null, null, InvokeAsyncCallback, asyncOperation);
		try
		{
			base.AsyncInvokes.Add(userState, webClientAsyncResult);
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Error, this, "InvokeAsync", ex);
			}
			Exception exception = new ArgumentException(Res.GetString("AsyncDuplicateUserState"), ex);
			InvokeCompletedEventArgs arg = new InvokeCompletedEventArgs(new object[1], exception, cancelled: false, userState);
			asyncOperation.PostOperationCompleted(callback, arg);
			return;
		}
		try
		{
			HttpClientMethod clientMethod = GetClientMethod(methodName);
			MimeParameterWriter parameterWriter = GetParameterWriter(clientMethod);
			Uri uri = new Uri(requestUrl);
			if (parameterWriter != null)
			{
				parameterWriter.RequestEncoding = base.RequestEncoding;
				requestUrl = parameterWriter.GetRequestUrl(uri.AbsoluteUri, parameters);
				uri = new Uri(requestUrl, dontEscape: true);
			}
			webClientAsyncResult.InternalAsyncState = new InvokeAsyncState(clientMethod, parameterWriter, parameters);
			BeginSend(uri, webClientAsyncResult, parameterWriter.UsesWriteRequest);
		}
		catch (Exception ex2)
		{
			if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Error, this, "InvokeAsync", ex2);
			}
			OperationCompleted(userState, new object[1], ex2, canceled: false);
		}
	}

	private MimeParameterWriter GetParameterWriter(HttpClientMethod method)
	{
		if (method.writerType == null)
		{
			return null;
		}
		return (MimeParameterWriter)MimeFormatter.CreateInstance(method.writerType, method.writerInitializer);
	}

	private HttpClientMethod GetClientMethod(string methodName)
	{
		HttpClientMethod method = clientType.GetMethod(methodName);
		if (method == null)
		{
			throw new ArgumentException(Res.GetString("WebInvalidMethodName", methodName), "methodName");
		}
		return method;
	}

	private object ReadResponse(HttpClientMethod method, WebResponse response, Stream responseStream)
	{
		if (response is HttpWebResponse { StatusCode: >=HttpStatusCode.MultipleChoices } httpWebResponse)
		{
			throw new WebException(RequestResponseUtils.CreateResponseExceptionString(httpWebResponse, responseStream), null, WebExceptionStatus.ProtocolError, httpWebResponse);
		}
		if (method.readerType == null)
		{
			return null;
		}
		if (responseStream != null)
		{
			return ((MimeReturnReader)MimeFormatter.CreateInstance(method.readerType, method.readerInitializer)).Read(response, responseStream);
		}
		return null;
	}
}
