using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Services.Diagnostics;

namespace System.Web.Services.Protocols;

/// <summary>Specifies the base class for all XML Web service client proxies created using ASP.NET.</summary>
[ComVisible(true)]
public abstract class WebClientProtocol : Component
{
	private static AsyncCallback getRequestStreamAsyncCallback;

	private static AsyncCallback getResponseAsyncCallback;

	private static volatile AsyncCallback readResponseAsyncCallback;

	private static ClientTypeCache cache;

	private static RequestCachePolicy bypassCache;

	private ICredentials credentials;

	private bool preAuthenticate;

	private Uri uri;

	private int timeout;

	private string connectionGroupName;

	private Encoding requestEncoding;

	private WebRequest pendingSyncRequest;

	private object nullToken = new object();

	private Hashtable asyncInvokes = Hashtable.Synchronized(new Hashtable());

	private static object s_InternalSyncObject;

	internal static object InternalSyncObject
	{
		get
		{
			if (s_InternalSyncObject == null)
			{
				object value = new object();
				Interlocked.CompareExchange(ref s_InternalSyncObject, value, null);
			}
			return s_InternalSyncObject;
		}
	}

	internal static RequestCachePolicy BypassCache
	{
		get
		{
			if (bypassCache == null)
			{
				bypassCache = new RequestCachePolicy(RequestCacheLevel.BypassCache);
			}
			return bypassCache;
		}
	}

	/// <summary>Gets or sets security credentials for XML Web service client authentication.</summary>
	/// <returns>The <see cref="T:System.Net.ICredentials" /> for the XML Web service client.</returns>
	public ICredentials Credentials
	{
		get
		{
			return credentials;
		}
		set
		{
			credentials = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether to set the <see cref="P:System.Web.Services.Protocols.WebClientProtocol.Credentials" /> property to the value of the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> property.</summary>
	/// <returns>
	///     <see langword="true" /> if the Credentials property is set to the value of the <see cref="P:System.Net.CredentialCache.DefaultCredentials" /> property; otherwise, <see langword="false" />.</returns>
	public bool UseDefaultCredentials
	{
		get
		{
			if (credentials != CredentialCache.DefaultCredentials)
			{
				return false;
			}
			return true;
		}
		set
		{
			credentials = (value ? CredentialCache.DefaultCredentials : null);
		}
	}

	/// <summary>Gets or sets the name of the connection group for the request.</summary>
	/// <returns>The name of the connection group. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	public string ConnectionGroupName
	{
		get
		{
			if (connectionGroupName != null)
			{
				return connectionGroupName;
			}
			return string.Empty;
		}
		set
		{
			connectionGroupName = value;
		}
	}

	internal WebRequest PendingSyncRequest
	{
		get
		{
			return pendingSyncRequest;
		}
		set
		{
			pendingSyncRequest = value;
		}
	}

	/// <summary>Gets or sets whether pre-authentication is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> to pre-authenticate the request; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebServicesDescription("ClientProtocolPreAuthenticate")]
	public bool PreAuthenticate
	{
		get
		{
			return preAuthenticate;
		}
		set
		{
			preAuthenticate = value;
		}
	}

	/// <summary>Gets or sets the base URL of the XML Web service the client is requesting.</summary>
	/// <returns>The base URL of the XML Web service the client is requesting. The default is <see cref="F:System.String.Empty" />.</returns>
	[DefaultValue("")]
	[SettingsBindable(true)]
	[WebServicesDescription("ClientProtocolUrl")]
	public string Url
	{
		get
		{
			if (!(uri == null))
			{
				return uri.ToString();
			}
			return string.Empty;
		}
		set
		{
			uri = new Uri(value);
		}
	}

	internal Hashtable AsyncInvokes => asyncInvokes;

	internal object NullToken => nullToken;

	internal Uri Uri
	{
		get
		{
			return uri;
		}
		set
		{
			uri = value;
		}
	}

	/// <summary>The <see cref="T:System.Text.Encoding" /> used to make the client request to the XML Web service.</summary>
	/// <returns>The character encoding for the client request. The default is <see langword="null" />, which uses the default encoding for the underlying transport and protocol.</returns>
	[DefaultValue(null)]
	[SettingsBindable(true)]
	[WebServicesDescription("ClientProtocolEncoding")]
	public Encoding RequestEncoding
	{
		get
		{
			return requestEncoding;
		}
		set
		{
			requestEncoding = value;
		}
	}

	/// <summary>Indicates the time an XML Web service client waits for the reply to a synchronous XML Web service request to arrive (in milliseconds).</summary>
	/// <returns>The time out, in milliseconds, for synchronous calls to the XML Web service. The default is 100000 milliseconds.</returns>
	[DefaultValue(100000)]
	[SettingsBindable(true)]
	[WebServicesDescription("ClientProtocolTimeout")]
	public int Timeout
	{
		get
		{
			return timeout;
		}
		set
		{
			timeout = ((value < -1) ? (-1) : value);
		}
	}

	static WebClientProtocol()
	{
		cache = new ClientTypeCache();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.WebClientProtocol" /> class.</summary>
	protected WebClientProtocol()
	{
		timeout = 100000;
	}

	internal WebClientProtocol(WebClientProtocol protocol)
	{
		credentials = protocol.credentials;
		uri = protocol.uri;
		timeout = protocol.timeout;
		connectionGroupName = protocol.connectionGroupName;
		requestEncoding = protocol.requestEncoding;
	}

	/// <summary>Cancels a request to an XML Web service method.</summary>
	public virtual void Abort()
	{
		PendingSyncRequest?.Abort();
	}

	internal IAsyncResult BeginSend(Uri requestUri, WebClientAsyncResult asyncResult, bool callWriteAsyncRequest)
	{
		if (readResponseAsyncCallback == null)
		{
			lock (InternalSyncObject)
			{
				if (readResponseAsyncCallback == null)
				{
					getRequestStreamAsyncCallback = GetRequestStreamAsyncCallback;
					getResponseAsyncCallback = GetResponseAsyncCallback;
					readResponseAsyncCallback = ReadResponseAsyncCallback;
				}
			}
		}
		WebRequest webRequest = (asyncResult.Request = GetWebRequest(requestUri));
		InitializeAsyncRequest(webRequest, asyncResult.InternalAsyncState);
		if (callWriteAsyncRequest)
		{
			webRequest.BeginGetRequestStream(getRequestStreamAsyncCallback, asyncResult);
		}
		else
		{
			webRequest.BeginGetResponse(getResponseAsyncCallback, asyncResult);
		}
		if (!asyncResult.IsCompleted)
		{
			asyncResult.CombineCompletedSynchronously(innerCompletedSynchronously: false);
		}
		return asyncResult;
	}

	private static void ProcessAsyncException(WebClientAsyncResult client, Exception e, string method)
	{
		if (Tracing.On)
		{
			Tracing.ExceptionCatch(TraceEventType.Error, typeof(WebClientProtocol), method, e);
		}
		if (e is WebException { Response: not null } ex)
		{
			client.Response = ex.Response;
			return;
		}
		if (client.IsCompleted)
		{
			throw new InvalidOperationException(Res.GetString("ThereWasAnErrorDuringAsyncProcessing"), e);
		}
		client.Complete(e);
	}

	private static void GetRequestStreamAsyncCallback(IAsyncResult asyncResult)
	{
		WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)asyncResult.AsyncState;
		webClientAsyncResult.CombineCompletedSynchronously(asyncResult.CompletedSynchronously);
		bool flag = true;
		try
		{
			Stream stream = webClientAsyncResult.Request.EndGetRequestStream(asyncResult);
			flag = false;
			try
			{
				webClientAsyncResult.ClientProtocol.AsyncBufferedSerialize(webClientAsyncResult.Request, stream, webClientAsyncResult.InternalAsyncState);
			}
			finally
			{
				stream.Close();
			}
			webClientAsyncResult.Request.BeginGetResponse(getResponseAsyncCallback, webClientAsyncResult);
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			ProcessAsyncException(webClientAsyncResult, ex, "GetRequestStreamAsyncCallback");
			if (flag && ex is WebException { Response: not null })
			{
				webClientAsyncResult.Complete(ex);
			}
		}
	}

	private static void GetResponseAsyncCallback(IAsyncResult asyncResult)
	{
		WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)asyncResult.AsyncState;
		webClientAsyncResult.CombineCompletedSynchronously(asyncResult.CompletedSynchronously);
		try
		{
			webClientAsyncResult.Response = webClientAsyncResult.ClientProtocol.GetWebResponse(webClientAsyncResult.Request, asyncResult);
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			ProcessAsyncException(webClientAsyncResult, ex, "GetResponseAsyncCallback");
			if (webClientAsyncResult.Response == null)
			{
				return;
			}
		}
		ReadAsyncResponse(webClientAsyncResult);
	}

	private static void ReadAsyncResponse(WebClientAsyncResult client)
	{
		if (client.Response.ContentLength == 0L)
		{
			client.Complete();
			return;
		}
		try
		{
			client.ResponseStream = client.Response.GetResponseStream();
			ReadAsyncResponseStream(client);
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			ProcessAsyncException(client, ex, "ReadAsyncResponse");
		}
	}

	private static void ReadAsyncResponseStream(WebClientAsyncResult client)
	{
		IAsyncResult asyncResult;
		do
		{
			byte[] array = client.Buffer;
			long contentLength = client.Response.ContentLength;
			if (array == null)
			{
				array = (client.Buffer = new byte[(contentLength == -1) ? 1024 : contentLength]);
			}
			else if (contentLength != -1 && contentLength > array.Length)
			{
				array = (client.Buffer = new byte[contentLength]);
			}
			asyncResult = client.ResponseStream.BeginRead(array, 0, array.Length, readResponseAsyncCallback, client);
		}
		while (asyncResult.CompletedSynchronously && !ProcessAsyncResponseStreamResult(client, asyncResult));
	}

	private static bool ProcessAsyncResponseStreamResult(WebClientAsyncResult client, IAsyncResult asyncResult)
	{
		int num = client.ResponseStream.EndRead(asyncResult);
		long contentLength = client.Response.ContentLength;
		bool flag;
		if (contentLength > 0 && num == contentLength)
		{
			client.ResponseBufferedStream = new MemoryStream(client.Buffer);
			flag = true;
		}
		else if (num > 0)
		{
			if (client.ResponseBufferedStream == null)
			{
				int capacity = (int)((contentLength == -1) ? client.Buffer.Length : contentLength);
				client.ResponseBufferedStream = new MemoryStream(capacity);
			}
			client.ResponseBufferedStream.Write(client.Buffer, 0, num);
			flag = false;
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			client.Complete();
		}
		return flag;
	}

	private static void ReadResponseAsyncCallback(IAsyncResult asyncResult)
	{
		WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)asyncResult.AsyncState;
		webClientAsyncResult.CombineCompletedSynchronously(asyncResult.CompletedSynchronously);
		if (asyncResult.CompletedSynchronously)
		{
			return;
		}
		try
		{
			if (!ProcessAsyncResponseStreamResult(webClientAsyncResult, asyncResult))
			{
				ReadAsyncResponseStream(webClientAsyncResult);
			}
		}
		catch (Exception ex)
		{
			if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
			{
				throw;
			}
			ProcessAsyncException(webClientAsyncResult, ex, "ReadResponseAsyncCallback");
		}
	}

	internal void NotifyClientCallOut(WebRequest request)
	{
	}

	/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> instance for the specified <paramref name="uri" />. This protected method is called by the XML Web service client infrastructure to get a new <see cref="T:System.Net.WebRequest" /> transport object to transmit the XML Web service request.</summary>
	/// <param name="uri">The <see cref="T:System.Uri" /> to use when creating the <see cref="T:System.Net.WebRequest" />. </param>
	/// <returns>The <see cref="T:System.Net.WebRequest" /> instance.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <paramref name="uri" /> parameter is <see langword="null" />. </exception>
	protected virtual WebRequest GetWebRequest(Uri uri)
	{
		if (uri == null)
		{
			throw new InvalidOperationException(Res.GetString("WebMissingPath"));
		}
		WebRequest webRequest2 = (PendingSyncRequest = WebRequest.Create(uri));
		webRequest2.Timeout = timeout;
		webRequest2.ConnectionGroupName = connectionGroupName;
		webRequest2.Credentials = Credentials;
		webRequest2.PreAuthenticate = PreAuthenticate;
		webRequest2.CachePolicy = BypassCache;
		return webRequest2;
	}

	/// <summary>Returns a response from a synchronous request to an XML Web service method.</summary>
	/// <param name="request">The <see cref="T:System.Net.WebRequest" /> to get the response from. </param>
	/// <returns>A response from a synchronous request to an XML Web service method.</returns>
	/// <exception cref="T:System.Net.WebException">If <see cref="M:System.Web.Services.Protocols.WebClientProtocol.Abort" /> is invoked prior to calling <see cref="M:System.Web.Services.Protocols.WebClientProtocol.GetWebResponse(System.Net.WebRequest)" />. </exception>
	protected virtual WebResponse GetWebResponse(WebRequest request)
	{
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "GetWebResponse") : null);
		WebResponse webResponse = null;
		try
		{
			if (Tracing.On)
			{
				Tracing.Enter("WebRequest.GetResponse", caller, new TraceMethod(request, "GetResponse"));
			}
			webResponse = request.GetResponse();
			if (Tracing.On)
			{
				Tracing.Exit("WebRequest.GetResponse", caller);
			}
		}
		catch (WebException ex)
		{
			if (ex.Response == null)
			{
				throw ex;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Error, this, "GetWebResponse", ex);
			}
			webResponse = ex.Response;
		}
		return webResponse;
	}

	/// <summary>Returns a response from an asynchronous request to an XML Web service method. This protected method is called by the XML Web service client infrastructure to get the response from an asynchronous XML Web service request.</summary>
	/// <param name="request">The <see cref="T:System.Net.WebRequest" /> to get the response from. </param>
	/// <param name="result">The <see cref="T:System.IAsyncResult" /> to pass to <see cref="M:System.Net.HttpWebRequest.EndGetResponse(System.IAsyncResult)" /> when the response has completed. </param>
	/// <returns>A response from an asynchronous request to an XML Web service method.</returns>
	/// <exception cref="T:System.Net.WebException">If <see cref="M:System.Web.Services.Protocols.WebClientProtocol.Abort" /> is invoked prior to calling <see cref="M:System.Web.Services.Protocols.WebClientProtocol.GetWebResponse(System.Net.WebRequest)" />. </exception>
	protected virtual WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
	{
		return request.EndGetResponse(result);
	}

	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	internal virtual void InitializeAsyncRequest(WebRequest request, object internalAsyncState)
	{
	}

	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	internal virtual void AsyncBufferedSerialize(WebRequest request, Stream requestStream, object internalAsyncState)
	{
		throw new NotSupportedException(Res.GetString("ProtocolDoesNotAsyncSerialize"));
	}

	internal WebResponse EndSend(IAsyncResult asyncResult, ref object internalAsyncState, ref Stream responseStream)
	{
		if (asyncResult == null)
		{
			throw new ArgumentNullException(Res.GetString("WebNullAsyncResultInEnd"));
		}
		WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)asyncResult;
		if (webClientAsyncResult.EndSendCalled)
		{
			throw new InvalidOperationException(Res.GetString("CanTCallTheEndMethodOfAnAsyncCallMoreThan"));
		}
		webClientAsyncResult.EndSendCalled = true;
		WebResponse result = webClientAsyncResult.WaitForResponse();
		internalAsyncState = webClientAsyncResult.InternalAsyncState;
		responseStream = webClientAsyncResult.ResponseBufferedStream;
		return result;
	}

	/// <summary>Gets an instance of a client protocol handler from the cache.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> of the client protocol handler to be returned from the cache.</param>
	/// <returns>An instance of a client protocol handler from the cache.</returns>
	protected static object GetFromCache(Type type)
	{
		return cache[type];
	}

	/// <summary>Add an instance of the client protocol handler to the cache.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> of the client protocol handler..</param>
	/// <param name="value">The client protocol handler to be added to the cache.</param>
	protected static void AddToCache(Type type, object value)
	{
		cache.Add(type, value);
	}
}
