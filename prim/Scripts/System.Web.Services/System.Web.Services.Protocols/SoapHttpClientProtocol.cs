using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Description;
using System.Web.Services.Diagnostics;
using System.Web.Services.Discovery;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

/// <summary>Specifies the class client that proxies derive from when using SOAP.</summary>
[ComVisible(true)]
public class SoapHttpClientProtocol : HttpWebClientProtocol
{
	private class InvokeAsyncState
	{
		public string MethodName;

		public object[] Parameters;

		public SoapClientMessage Message;

		public InvokeAsyncState(string methodName, object[] parameters)
		{
			MethodName = methodName;
			Parameters = parameters;
		}
	}

	private SoapClientType clientType;

	private SoapProtocolVersion version;

	/// <summary>Gets or sets the version of the SOAP protocol used to make the SOAP request to the XML Web service.</summary>
	/// <returns>One of the <see cref="T:System.Web.Services.Protocols.SoapProtocolVersion" /> values. The default is <see cref="F:System.Web.Services.Protocols.SoapProtocolVersion.Soap11" />.</returns>
	[DefaultValue(SoapProtocolVersion.Default)]
	[WebServicesDescription("ClientProtocolSoapVersion")]
	[ComVisible(false)]
	public SoapProtocolVersion SoapVersion
	{
		get
		{
			return version;
		}
		set
		{
			version = value;
		}
	}

	private string EnvelopeNs
	{
		get
		{
			if (version != SoapProtocolVersion.Soap12)
			{
				return "http://schemas.xmlsoap.org/soap/envelope/";
			}
			return "http://www.w3.org/2003/05/soap-envelope";
		}
	}

	private string EncodingNs
	{
		get
		{
			if (version != SoapProtocolVersion.Soap12)
			{
				return "http://schemas.xmlsoap.org/soap/encoding/";
			}
			return "http://www.w3.org/2003/05/soap-encoding";
		}
	}

	private string HttpContentType
	{
		get
		{
			if (version != SoapProtocolVersion.Soap12)
			{
				return "text/xml";
			}
			return "application/soap+xml";
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHttpClientProtocol" /> class.</summary>
	public SoapHttpClientProtocol()
	{
		Type type = GetType();
		clientType = (SoapClientType)WebClientProtocol.GetFromCache(type);
		if (clientType != null)
		{
			return;
		}
		lock (WebClientProtocol.InternalSyncObject)
		{
			clientType = (SoapClientType)WebClientProtocol.GetFromCache(type);
			if (clientType == null)
			{
				clientType = new SoapClientType(type);
				WebClientProtocol.AddToCache(type, clientType);
			}
		}
	}

	/// <summary>Dynamically binds to an XML Web service described in the discovery document at <see cref="P:System.Web.Services.Protocols.WebClientProtocol.Url" />.</summary>
	/// <exception cref="T:System.Exception">The binding defined in the proxy class could not be found in the discovery document at <see cref="P:System.Web.Services.Protocols.WebClientProtocol.Url" />. </exception>
	/// <exception cref="T:System.Exception">The proxy class does not have a binding defined. </exception>
	public void Discover()
	{
		if (clientType.Binding == null)
		{
			throw new InvalidOperationException(Res.GetString("DiscoveryIsNotPossibleBecauseTypeIsMissing1", GetType().FullName));
		}
		foreach (object reference in new DiscoveryClientProtocol(this).Discover(base.Url).References)
		{
			if (reference is System.Web.Services.Discovery.SoapBinding soapBinding && clientType.Binding.Name == soapBinding.Binding.Name && clientType.Binding.Namespace == soapBinding.Binding.Namespace)
			{
				base.Url = soapBinding.Address;
				return;
			}
		}
		throw new InvalidOperationException(Res.GetString("TheBindingNamedFromNamespaceWasNotFoundIn3", clientType.Binding.Name, clientType.Binding.Namespace, base.Url));
	}

	/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> for the specified <paramref name="uri" />.</summary>
	/// <param name="uri">The <see cref="T:System.Uri" /> to use when creating the <see cref="T:System.Net.WebRequest" />. </param>
	/// <returns>The <see cref="T:System.Net.WebRequest" /> for the specified URI.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <paramref name="uri" /> parameter is <see langword="null" />. </exception>
	protected override WebRequest GetWebRequest(Uri uri)
	{
		return base.GetWebRequest(uri);
	}

	/// <summary>Returns a <see cref="T:System.Xml.XmlWriter" /> initialized with the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> property of the <see cref="T:System.Web.Services.Protocols.SoapClientMessage" /> parameter.</summary>
	/// <param name="message">A <see cref="T:System.Web.Services.Protocols.SoapClientMessage" /> that provides the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> to initialize the <see cref="T:System.Xml.XmlWriter" />.</param>
	/// <param name="bufferSize">The initial buffer size of the <see cref="T:System.IO.StreamWriter" /> used by the <see cref="T:System.Xml.XmlWriter" />.</param>
	/// <returns>A <see cref="T:System.Xml.XmlWriter" /> initialized with the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> property of the <paramref name="message" /> parameter.</returns>
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	protected virtual XmlWriter GetWriterForMessage(SoapClientMessage message, int bufferSize)
	{
		if (bufferSize < 512)
		{
			bufferSize = 512;
		}
		return new XmlTextWriter(new StreamWriter(message.Stream, (base.RequestEncoding != null) ? base.RequestEncoding : new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), bufferSize));
	}

	/// <summary>Returns an <see cref="T:System.Xml.XmlReader" /> initialized with the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> property of the <see cref="T:System.Web.Services.Protocols.SoapClientMessage" /> parameter.</summary>
	/// <param name="message">A <see cref="T:System.Web.Services.Protocols.SoapClientMessage" /> that provides the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> to initialize the <see cref="T:System.Xml.XmlReader" />.</param>
	/// <param name="bufferSize">The initial buffer size of the <see cref="T:System.IO.StreamReader" /> used by the <see cref="T:System.Xml.XmlReader" />.</param>
	/// <returns>A <see cref="T:System.Xml.XmlReader" /> initialized with the <see cref="P:System.Web.Services.Protocols.SoapMessage.Stream" /> property of the <paramref name="message" /> parameter.</returns>
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	protected virtual XmlReader GetReaderForMessage(SoapClientMessage message, int bufferSize)
	{
		Encoding encoding = ((message.SoapVersion == SoapProtocolVersion.Soap12) ? RequestResponseUtils.GetEncoding2(message.ContentType) : RequestResponseUtils.GetEncoding(message.ContentType));
		if (bufferSize < 512)
		{
			bufferSize = 512;
		}
		XmlTextReader xmlTextReader = ((encoding == null) ? new XmlTextReader(message.Stream) : new XmlTextReader(new StreamReader(message.Stream, encoding, detectEncodingFromByteOrderMarks: true, bufferSize)));
		xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
		xmlTextReader.Normalization = true;
		xmlTextReader.XmlResolver = null;
		return xmlTextReader;
	}

	/// <summary>Invokes an XML Web service method synchronously using SOAP.</summary>
	/// <param name="methodName">The name of the XML Web service method. </param>
	/// <param name="parameters">An array of objects that contains the parameters to pass to the XML Web service. The order of the values in the array corresponds to the order of the parameters in the calling method of the derived class. </param>
	/// <returns>An array of objects that contains the return value and any <see langword="reference" /> or <see langword="out" /> parameters of the derived class method.</returns>
	/// <exception cref="T:System.Web.Services.Protocols.SoapException">The request reached the server computer, but was not processed successfully. </exception>
	/// <exception cref="T:System.InvalidOperationException">The request was not valid for the object's current state.</exception>
	/// <exception cref="T:System.Net.WebException">An error occurred while accessing the network.</exception>
	protected object[] Invoke(string methodName, object[] parameters)
	{
		WebResponse webResponse = null;
		WebRequest webRequest = null;
		try
		{
			webRequest = GetWebRequest(base.Uri);
			NotifyClientCallOut(webRequest);
			base.PendingSyncRequest = webRequest;
			SoapClientMessage soapClientMessage = BeforeSerialize(webRequest, methodName, parameters);
			Stream requestStream = webRequest.GetRequestStream();
			try
			{
				soapClientMessage.SetStream(requestStream);
				Serialize(soapClientMessage);
			}
			finally
			{
				requestStream.Close();
			}
			webResponse = GetWebResponse(webRequest);
			Stream stream = null;
			try
			{
				stream = webResponse.GetResponseStream();
				return ReadResponse(soapClientMessage, webResponse, stream, asyncCall: false);
			}
			catch (XmlException innerException)
			{
				throw new InvalidOperationException(Res.GetString("WebResponseBadXml"), innerException);
			}
			finally
			{
				stream?.Close();
			}
		}
		finally
		{
			if (webRequest == base.PendingSyncRequest)
			{
				base.PendingSyncRequest = null;
			}
		}
	}

	/// <summary>Starts an asynchronous invocation of an XML Web service method using SOAP.</summary>
	/// <param name="methodName">The name of the XML Web service method in the derived class that is invoking the <see cref="M:System.Web.Services.Protocols.SoapHttpClientProtocol.BeginInvoke(System.String,System.Object[],System.AsyncCallback,System.Object)" /> method. </param>
	/// <param name="parameters">An array of objects containing the parameters to pass to the XML Web service. The order of the values in the array correspond to the order of the parameters in the calling method of the derived class. </param>
	/// <param name="callback">The delegate to call when the asynchronous invoke is complete. If <paramref name="callback" /> is <see langword="null" />, the delegate is not called. </param>
	/// <param name="asyncState">Extra information supplied by the caller. </param>
	/// <returns>An <see cref="T:System.IAsyncResult" /> that is passed to the <see cref="M:System.Web.Services.Protocols.SoapHttpClientProtocol.EndInvoke(System.IAsyncResult)" /> method to obtain the return values from the remote method call.</returns>
	/// <exception cref="T:System.Web.Services.Protocols.SoapException">The request reached the server computer, but was not processed successfully. </exception>
	/// <exception cref="T:System.InvalidOperationException">The request was not valid for the object's current state.</exception>
	/// <exception cref="T:System.Net.WebException">An error occurred while accessing the network.</exception>
	protected IAsyncResult BeginInvoke(string methodName, object[] parameters, AsyncCallback callback, object asyncState)
	{
		InvokeAsyncState internalAsyncState = new InvokeAsyncState(methodName, parameters);
		WebClientAsyncResult asyncResult = new WebClientAsyncResult(this, internalAsyncState, null, callback, asyncState);
		return BeginSend(base.Uri, asyncResult, callWriteAsyncRequest: true);
	}

	internal override void InitializeAsyncRequest(WebRequest request, object internalAsyncState)
	{
		InvokeAsyncState invokeAsyncState = (InvokeAsyncState)internalAsyncState;
		invokeAsyncState.Message = BeforeSerialize(request, invokeAsyncState.MethodName, invokeAsyncState.Parameters);
	}

	internal override void AsyncBufferedSerialize(WebRequest request, Stream requestStream, object internalAsyncState)
	{
		InvokeAsyncState invokeAsyncState = (InvokeAsyncState)internalAsyncState;
		invokeAsyncState.Message.SetStream(requestStream);
		Serialize(invokeAsyncState.Message);
	}

	/// <summary>Ends an asynchronous invocation of an XML Web service method using SOAP.</summary>
	/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned from the <see cref="M:System.Web.Services.Protocols.SoapHttpClientProtocol.BeginInvoke(System.String,System.Object[],System.AsyncCallback,System.Object)" /> method. </param>
	/// <returns>An array of objects that contains the return value and any by-reference or <see langword="out" /> parameters of the derived class method.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="asyncResult" /> is not the return value from the <see cref="M:System.Web.Services.Protocols.SoapHttpClientProtocol.BeginInvoke(System.String,System.Object[],System.AsyncCallback,System.Object)" /> method. </exception>
	/// <exception cref="T:System.Web.Services.Protocols.SoapException">The request reached the server computer, but was not processed successfully. </exception>
	/// <exception cref="T:System.InvalidOperationException">The request was not valid for the object's current state.</exception>
	/// <exception cref="T:System.Net.WebException">An error occurred while accessing the network.</exception>
	protected object[] EndInvoke(IAsyncResult asyncResult)
	{
		object internalAsyncState = null;
		Stream responseStream = null;
		try
		{
			WebResponse response = EndSend(asyncResult, ref internalAsyncState, ref responseStream);
			InvokeAsyncState invokeAsyncState = (InvokeAsyncState)internalAsyncState;
			return ReadResponse(invokeAsyncState.Message, response, responseStream, asyncCall: true);
		}
		catch (XmlException innerException)
		{
			throw new InvalidOperationException(Res.GetString("WebResponseBadXml"), innerException);
		}
		finally
		{
			responseStream?.Close();
		}
	}

	private void InvokeAsyncCallback(IAsyncResult result)
	{
		object[] parameters = null;
		Exception e = null;
		WebClientAsyncResult webClientAsyncResult = (WebClientAsyncResult)result;
		if (webClientAsyncResult.Request != null)
		{
			object internalAsyncState = null;
			Stream responseStream = null;
			try
			{
				WebResponse response = EndSend(webClientAsyncResult, ref internalAsyncState, ref responseStream);
				InvokeAsyncState invokeAsyncState = (InvokeAsyncState)internalAsyncState;
				parameters = ReadResponse(invokeAsyncState.Message, response, responseStream, asyncCall: true);
			}
			catch (XmlException ex)
			{
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "InvokeAsyncCallback", ex);
				}
				e = new InvalidOperationException(Res.GetString("WebResponseBadXml"), ex);
			}
			catch (Exception ex2)
			{
				if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "InvokeAsyncCallback", ex2);
				}
				e = ex2;
			}
			finally
			{
				responseStream?.Close();
			}
		}
		UserToken userToken = (UserToken)((AsyncOperation)result.AsyncState).UserSuppliedState;
		OperationCompleted(userToken.UserState, parameters, e, canceled: false);
	}

	/// <summary>Invokes the specified method asynchronously.</summary>
	/// <param name="methodName">The name of the method to invoke.</param>
	/// <param name="parameters">The parameters to pass to the method.</param>
	/// <param name="callback">The delegate called when the method invocation has completed.</param>
	protected void InvokeAsync(string methodName, object[] parameters, SendOrPostCallback callback)
	{
		InvokeAsync(methodName, parameters, callback, null);
	}

	/// <summary>Invokes the specified method asynchronously.</summary>
	/// <param name="methodName">The name of the method to invoke.</param>
	/// <param name="parameters">The parameters to pass to the method.</param>
	/// <param name="callback">The delegate called when the method invocation has completed.</param>
	/// <param name="userState">An object used to pass state information into the <paramref name="callback" /> delegate.</param>
	protected void InvokeAsync(string methodName, object[] parameters, SendOrPostCallback callback, object userState)
	{
		if (userState == null)
		{
			userState = base.NullToken;
		}
		InvokeAsyncState internalAsyncState = new InvokeAsyncState(methodName, parameters);
		AsyncOperation asyncOperation = AsyncOperationManager.CreateOperation(new UserToken(callback, userState));
		WebClientAsyncResult webClientAsyncResult = new WebClientAsyncResult(this, internalAsyncState, null, InvokeAsyncCallback, asyncOperation);
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
				Tracing.ExceptionCatch(TraceEventType.Warning, this, "InvokeAsync", ex);
			}
			Exception exception = new ArgumentException(Res.GetString("AsyncDuplicateUserState"), ex);
			InvokeCompletedEventArgs arg = new InvokeCompletedEventArgs(new object[1], exception, cancelled: false, userState);
			asyncOperation.PostOperationCompleted(callback, arg);
			return;
		}
		try
		{
			BeginSend(base.Uri, webClientAsyncResult, callWriteAsyncRequest: true);
		}
		catch (Exception ex2)
		{
			if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Warning, this, "InvokeAsync", ex2);
			}
			OperationCompleted(userState, new object[1], ex2, canceled: false);
		}
	}

	private static Array CombineExtensionsHelper(Array array1, Array array2, Array array3, Type elementType)
	{
		int num = array1.Length + array2.Length + array3.Length;
		if (num == 0)
		{
			return null;
		}
		Array array4 = null;
		if (elementType == typeof(SoapReflectedExtension))
		{
			array4 = new SoapReflectedExtension[num];
		}
		else
		{
			if (!(elementType == typeof(object)))
			{
				throw new ArgumentException(Res.GetString("ElementTypeMustBeObjectOrSoapReflectedException"), "elementType");
			}
			array4 = new object[num];
		}
		int num2 = 0;
		Array.Copy(array1, 0, array4, num2, array1.Length);
		num2 += array1.Length;
		Array.Copy(array2, 0, array4, num2, array2.Length);
		num2 += array2.Length;
		Array.Copy(array3, 0, array4, num2, array3.Length);
		return array4;
	}

	private SoapClientMessage BeforeSerialize(WebRequest request, string methodName, object[] parameters)
	{
		if (parameters == null)
		{
			throw new ArgumentNullException("parameters");
		}
		SoapClientMethod method = clientType.GetMethod(methodName);
		if (method == null)
		{
			throw new ArgumentException(Res.GetString("WebInvalidMethodName", methodName));
		}
		SoapReflectedExtension[] reflectedExtensions = (SoapReflectedExtension[])CombineExtensionsHelper(clientType.HighPriExtensions, method.extensions, clientType.LowPriExtensions, typeof(SoapReflectedExtension));
		object[] extensionInitializers = (object[])CombineExtensionsHelper(clientType.HighPriExtensionInitializers, method.extensionInitializers, clientType.LowPriExtensionInitializers, typeof(object));
		SoapExtension[] array = SoapMessage.InitializeExtensions(reflectedExtensions, extensionInitializers);
		SoapClientMessage soapClientMessage = new SoapClientMessage(this, method, base.Url);
		soapClientMessage.initializedExtensions = array;
		if (array != null)
		{
			soapClientMessage.SetExtensionStream(new SoapExtensionStream());
		}
		soapClientMessage.InitExtensionStreamChain(soapClientMessage.initializedExtensions);
		string text = UrlEncoder.EscapeString(method.action, Encoding.UTF8);
		soapClientMessage.SetStage(SoapMessageStage.BeforeSerialize);
		if (version == SoapProtocolVersion.Soap12)
		{
			soapClientMessage.ContentType = ContentType.Compose("application/soap+xml", (base.RequestEncoding != null) ? base.RequestEncoding : Encoding.UTF8, text);
		}
		else
		{
			soapClientMessage.ContentType = ContentType.Compose("text/xml", (base.RequestEncoding != null) ? base.RequestEncoding : Encoding.UTF8);
		}
		soapClientMessage.SetParameterValues(parameters);
		SoapHeaderHandling.GetHeaderMembers(soapClientMessage.Headers, this, method.inHeaderMappings, SoapHeaderDirection.In, client: true);
		soapClientMessage.RunExtensions(soapClientMessage.initializedExtensions, throwOnException: true);
		request.ContentType = soapClientMessage.ContentType;
		if (soapClientMessage.ContentEncoding != null && soapClientMessage.ContentEncoding.Length > 0)
		{
			request.Headers["Content-Encoding"] = soapClientMessage.ContentEncoding;
		}
		request.Method = "POST";
		if (version != SoapProtocolVersion.Soap12 && request.Headers["SOAPAction"] == null)
		{
			StringBuilder stringBuilder = new StringBuilder(text.Length + 2);
			stringBuilder.Append('"');
			stringBuilder.Append(text);
			stringBuilder.Append('"');
			request.Headers.Add("SOAPAction", stringBuilder.ToString());
		}
		return soapClientMessage;
	}

	private void Serialize(SoapClientMessage message)
	{
		_ = message.Stream;
		SoapClientMethod method = message.Method;
		bool flag = method.use == SoapBindingUse.Encoded;
		string envelopeNs = EnvelopeNs;
		string encodingNs = EncodingNs;
		XmlWriter writerForMessage = GetWriterForMessage(message, 1024);
		if (writerForMessage == null)
		{
			throw new InvalidOperationException(Res.GetString("WebNullWriterForMessage"));
		}
		writerForMessage.WriteStartDocument();
		writerForMessage.WriteStartElement("soap", "Envelope", envelopeNs);
		writerForMessage.WriteAttributeString("xmlns", "soap", null, envelopeNs);
		if (flag)
		{
			writerForMessage.WriteAttributeString("xmlns", "soapenc", null, encodingNs);
			writerForMessage.WriteAttributeString("xmlns", "tns", null, clientType.serviceNamespace);
			writerForMessage.WriteAttributeString("xmlns", "types", null, SoapReflector.GetEncodedNamespace(clientType.serviceNamespace, clientType.serviceDefaultIsEncoded));
		}
		writerForMessage.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
		writerForMessage.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
		SoapHeaderHandling.WriteHeaders(writerForMessage, method.inHeaderSerializer, message.Headers, method.inHeaderMappings, SoapHeaderDirection.In, flag, clientType.serviceNamespace, clientType.serviceDefaultIsEncoded, envelopeNs);
		writerForMessage.WriteStartElement("Body", envelopeNs);
		if (flag && version != SoapProtocolVersion.Soap12)
		{
			writerForMessage.WriteAttributeString("soap", "encodingStyle", null, encodingNs);
		}
		object[] parameterValues = message.GetParameterValues();
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "Serialize") : null);
		if (Tracing.On)
		{
			Tracing.Enter(Tracing.TraceId("TraceWriteRequest"), caller, new TraceMethod(method.parameterSerializer, "Serialize", writerForMessage, parameterValues, null, flag ? encodingNs : null));
		}
		method.parameterSerializer.Serialize(writerForMessage, parameterValues, null, flag ? encodingNs : null);
		if (Tracing.On)
		{
			Tracing.Exit(Tracing.TraceId("TraceWriteRequest"), caller);
		}
		writerForMessage.WriteEndElement();
		writerForMessage.WriteEndElement();
		writerForMessage.Flush();
		message.SetStage(SoapMessageStage.AfterSerialize);
		message.RunExtensions(message.initializedExtensions, throwOnException: true);
	}

	private object[] ReadResponse(SoapClientMessage message, WebResponse response, Stream responseStream, bool asyncCall)
	{
		SoapClientMethod method = message.Method;
		HttpWebResponse httpWebResponse = response as HttpWebResponse;
		int num = (int)(httpWebResponse?.StatusCode ?? ((HttpStatusCode)(-1)));
		if (num >= 300 && num != 500 && num != 400)
		{
			throw new WebException(RequestResponseUtils.CreateResponseExceptionString(httpWebResponse, responseStream), null, WebExceptionStatus.ProtocolError, httpWebResponse);
		}
		message.Headers.Clear();
		message.SetStream(responseStream);
		message.InitExtensionStreamChain(message.initializedExtensions);
		message.SetStage(SoapMessageStage.BeforeDeserialize);
		message.ContentType = response.ContentType;
		message.ContentEncoding = response.Headers["Content-Encoding"];
		message.RunExtensions(message.initializedExtensions, throwOnException: false);
		if (method.oneWay && (httpWebResponse == null || httpWebResponse.StatusCode != HttpStatusCode.InternalServerError))
		{
			return new object[0];
		}
		bool flag = ContentType.IsSoap(message.ContentType);
		if (!flag || (flag && httpWebResponse != null && httpWebResponse.ContentLength == 0L))
		{
			if (num == 400)
			{
				throw new WebException(RequestResponseUtils.CreateResponseExceptionString(httpWebResponse, responseStream), null, WebExceptionStatus.ProtocolError, httpWebResponse);
			}
			throw new InvalidOperationException(Res.GetString("WebResponseContent", message.ContentType, HttpContentType) + Environment.NewLine + RequestResponseUtils.CreateResponseExceptionString(response, responseStream));
		}
		if (message.Exception != null)
		{
			throw message.Exception;
		}
		int bufferSize = ((!asyncCall && httpWebResponse != null) ? RequestResponseUtils.GetBufferSize((int)httpWebResponse.ContentLength) : 512);
		XmlReader readerForMessage = GetReaderForMessage(message, bufferSize);
		if (readerForMessage == null)
		{
			throw new InvalidOperationException(Res.GetString("WebNullReaderForMessage"));
		}
		readerForMessage.MoveToContent();
		int depth = readerForMessage.Depth;
		string encodingNs = EncodingNs;
		string namespaceURI = readerForMessage.NamespaceURI;
		if (namespaceURI == null || namespaceURI.Length == 0)
		{
			readerForMessage.ReadStartElement("Envelope");
		}
		else if (readerForMessage.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/")
		{
			readerForMessage.ReadStartElement("Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
		}
		else
		{
			if (!(readerForMessage.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope"))
			{
				throw new SoapException(Res.GetString("WebInvalidEnvelopeNamespace", namespaceURI, EnvelopeNs), SoapException.VersionMismatchFaultCode);
			}
			readerForMessage.ReadStartElement("Envelope", "http://www.w3.org/2003/05/soap-envelope");
		}
		readerForMessage.MoveToContent();
		new SoapHeaderHandling().ReadHeaders(readerForMessage, method.outHeaderSerializer, message.Headers, method.outHeaderMappings, SoapHeaderDirection.Out | SoapHeaderDirection.Fault, namespaceURI, (method.use == SoapBindingUse.Encoded) ? encodingNs : null, checkRequiredHeaders: false);
		readerForMessage.MoveToContent();
		readerForMessage.ReadStartElement("Body", namespaceURI);
		readerForMessage.MoveToContent();
		if (readerForMessage.IsStartElement("Fault", namespaceURI))
		{
			message.Exception = ReadSoapException(readerForMessage);
		}
		else if (method.oneWay)
		{
			readerForMessage.Skip();
			message.SetParameterValues(new object[0]);
		}
		else
		{
			TraceMethod caller = (Tracing.On ? new TraceMethod(this, "ReadResponse") : null);
			bool flag2 = method.use == SoapBindingUse.Encoded;
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceReadResponse"), caller, new TraceMethod(method.returnSerializer, "Deserialize", readerForMessage, flag2 ? encodingNs : null));
			}
			if (!flag2 && (WebServicesSection.Current.SoapEnvelopeProcessing.IsStrict || Tracing.On))
			{
				XmlDeserializationEvents xmlDeserializationEvents = (Tracing.On ? Tracing.GetDeserializationEvents() : RuntimeUtils.GetDeserializationEvents());
				message.SetParameterValues((object[])method.returnSerializer.Deserialize(readerForMessage, null, xmlDeserializationEvents));
			}
			else
			{
				message.SetParameterValues((object[])method.returnSerializer.Deserialize(readerForMessage, flag2 ? encodingNs : null));
			}
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceReadResponse"), caller);
			}
		}
		while (depth < readerForMessage.Depth && readerForMessage.Read())
		{
		}
		if (readerForMessage.NodeType == XmlNodeType.EndElement)
		{
			readerForMessage.Read();
		}
		message.SetStage(SoapMessageStage.AfterDeserialize);
		message.RunExtensions(message.initializedExtensions, throwOnException: false);
		SoapHeaderHandling.SetHeaderMembers(message.Headers, this, method.outHeaderMappings, SoapHeaderDirection.Out | SoapHeaderDirection.Fault, client: true);
		if (message.Exception != null)
		{
			throw message.Exception;
		}
		return message.GetParameterValues();
	}

	private SoapException ReadSoapException(XmlReader reader)
	{
		XmlQualifiedName code = XmlQualifiedName.Empty;
		string message = null;
		string actor = null;
		string role = null;
		XmlNode xmlNode = null;
		SoapFaultSubCode subcode = null;
		string lang = null;
		bool flag = reader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope";
		if (reader.IsEmptyElement)
		{
			reader.Skip();
		}
		else
		{
			reader.ReadStartElement();
			reader.MoveToContent();
			int depth = reader.Depth;
			while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != 0)
			{
				if (reader.NamespaceURI == "http://schemas.xmlsoap.org/soap/envelope/" || reader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope" || reader.NamespaceURI == null || reader.NamespaceURI.Length == 0)
				{
					if (reader.LocalName == "faultcode" || reader.LocalName == "Code")
					{
						code = ((!flag) ? ReadFaultCode(reader) : ReadSoap12FaultCode(reader, out subcode));
					}
					else if (reader.LocalName == "faultstring")
					{
						lang = reader.GetAttribute("lang", "http://www.w3.org/XML/1998/namespace");
						reader.MoveToElement();
						message = reader.ReadElementString();
					}
					else if (reader.LocalName == "Reason")
					{
						if (reader.IsEmptyElement)
						{
							reader.Skip();
						}
						else
						{
							reader.ReadStartElement();
							reader.MoveToContent();
							while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != 0)
							{
								if (reader.LocalName == "Text" && reader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope")
								{
									message = reader.ReadElementString();
								}
								else
								{
									reader.Skip();
								}
								reader.MoveToContent();
							}
							while (reader.NodeType == XmlNodeType.Whitespace)
							{
								reader.Skip();
							}
							if (reader.NodeType == XmlNodeType.None)
							{
								reader.Skip();
							}
							else
							{
								reader.ReadEndElement();
							}
						}
					}
					else if (reader.LocalName == "faultactor" || reader.LocalName == "Node")
					{
						actor = reader.ReadElementString();
					}
					else if (reader.LocalName == "detail" || reader.LocalName == "Detail")
					{
						xmlNode = new XmlDocument().ReadNode(reader);
					}
					else if (reader.LocalName == "Role")
					{
						role = reader.ReadElementString();
					}
					else
					{
						reader.Skip();
					}
				}
				else
				{
					reader.Skip();
				}
				reader.MoveToContent();
			}
			while (reader.Read() && depth < reader.Depth)
			{
			}
			if (reader.NodeType == XmlNodeType.EndElement)
			{
				reader.Read();
			}
		}
		if (xmlNode != null || flag)
		{
			return new SoapException(message, code, actor, role, lang, xmlNode, subcode, null);
		}
		return new SoapHeaderException(message, code, actor, role, lang, subcode, null);
	}

	private XmlQualifiedName ReadSoap12FaultCode(XmlReader reader, out SoapFaultSubCode subcode)
	{
		SoapFaultSubCode soapFaultSubCode = ReadSoap12FaultCodesRecursive(reader, 0);
		if (soapFaultSubCode == null)
		{
			subcode = null;
			return null;
		}
		subcode = soapFaultSubCode.SubCode;
		return soapFaultSubCode.Code;
	}

	private SoapFaultSubCode ReadSoap12FaultCodesRecursive(XmlReader reader, int depth)
	{
		if (depth > 100)
		{
			return null;
		}
		if (reader.IsEmptyElement)
		{
			reader.Skip();
			return null;
		}
		XmlQualifiedName code = null;
		SoapFaultSubCode subCode = null;
		int depth2 = reader.Depth;
		reader.ReadStartElement();
		reader.MoveToContent();
		while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != 0)
		{
			if (reader.NamespaceURI == "http://www.w3.org/2003/05/soap-envelope" || reader.NamespaceURI == null || reader.NamespaceURI.Length == 0)
			{
				if (reader.LocalName == "Value")
				{
					code = ReadFaultCode(reader);
				}
				else if (reader.LocalName == "Subcode")
				{
					subCode = ReadSoap12FaultCodesRecursive(reader, depth + 1);
				}
				else
				{
					reader.Skip();
				}
			}
			else
			{
				reader.Skip();
			}
			reader.MoveToContent();
		}
		while (depth2 < reader.Depth && reader.Read())
		{
		}
		if (reader.NodeType == XmlNodeType.EndElement)
		{
			reader.Read();
		}
		return new SoapFaultSubCode(code, subCode);
	}

	private XmlQualifiedName ReadFaultCode(XmlReader reader)
	{
		if (reader.IsEmptyElement)
		{
			reader.Skip();
			return null;
		}
		reader.ReadStartElement();
		string text = reader.ReadString();
		int num = text.IndexOf(":", StringComparison.Ordinal);
		string text2 = reader.NamespaceURI;
		if (num >= 0)
		{
			string text3 = text.Substring(0, num);
			text2 = reader.LookupNamespace(text3);
			if (text2 == null)
			{
				throw new InvalidOperationException(Res.GetString("WebQNamePrefixUndefined", text3));
			}
		}
		reader.ReadEndElement();
		return new XmlQualifiedName(text.Substring(num + 1), text2);
	}
}
