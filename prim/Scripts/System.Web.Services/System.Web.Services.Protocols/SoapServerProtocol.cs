using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Description;
using System.Web.Services.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

/// <summary>The .NET Framework creates an instance of the <see cref="T:System.Web.Services.Protocols.SoapServerProtocol" /> class to process XML Web service requests.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public class SoapServerProtocol : ServerProtocol
{
	internal class SoapEnvelopeReader : XmlTextReader
	{
		private long readerTimedout;

		internal SoapEnvelopeReader(TextReader input, long timeout)
			: base(input)
		{
			readerTimedout = timeout;
		}

		internal SoapEnvelopeReader(Stream input, long timeout)
			: base(input)
		{
			readerTimedout = timeout;
		}

		public override bool Read()
		{
			CheckTimeout();
			return base.Read();
		}

		public override bool MoveToNextAttribute()
		{
			CheckTimeout();
			return base.MoveToNextAttribute();
		}

		public override XmlNodeType MoveToContent()
		{
			CheckTimeout();
			return base.MoveToContent();
		}

		private void CheckTimeout()
		{
			if (DateTime.UtcNow.Ticks > readerTimedout)
			{
				throw new InvalidOperationException(Res.GetString("WebTimeout"));
			}
		}
	}

	private SoapServerType serverType;

	private SoapServerMethod serverMethod;

	private SoapServerMessage message;

	private bool isOneWay;

	private Exception onewayInitException;

	private SoapProtocolVersion version;

	private WebServiceProtocols protocolsSupported;

	private SoapServerProtocolHelper helper;

	internal override ServerType ServerType => serverType;

	internal override LogicalMethodInfo MethodInfo => serverMethod.methodInfo;

	internal SoapServerMethod ServerMethod => serverMethod;

	internal SoapServerMessage Message => message;

	internal override bool IsOneWay => isOneWay;

	internal override Exception OnewayInitException => onewayInitException;

	internal SoapProtocolVersion Version => version;

	/// <summary>Creates a new <see cref="T:System.Web.Services.Protocols.SoapServerProtocol" />.</summary>
	protected internal SoapServerProtocol()
	{
		protocolsSupported = WebServicesSection.Current.EnabledProtocols;
	}

	/// <summary>Returns an <see cref="T:System.Xml.XmlTextWriter" /> initialized with the specified <see cref="T:System.Web.Services.Protocols.SoapServerMessage" /> and buffer size.</summary>
	/// <param name="message">The <see cref="T:System.Web.Services.Protocols.SoapServerMessage" /> with which to initialize the <see cref="T:System.Xml.XmlTextWriter" />.</param>
	/// <param name="bufferSize">The buffer size with which to initialize the <see cref="T:System.Xml.XmlTextWriter" />.</param>
	/// <returns>A <see cref="T:System.Xml.XmlTextWriter" /> initialized with the <see cref="T:System.Web.Services.Protocols.SoapServerMessage" /> specified by <paramref name="message" /> and the buffer size specified by <paramref name="bufferSize" />.</returns>
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	protected virtual XmlWriter GetWriterForMessage(SoapServerMessage message, int bufferSize)
	{
		if (bufferSize < 512)
		{
			bufferSize = 512;
		}
		return new XmlTextWriter(new StreamWriter(message.Stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), bufferSize));
	}

	/// <summary>Returns an <see cref="T:System.Xml.XmlTextReader" /> initialized with the specified <see cref="T:System.Web.Services.Protocols.SoapServerMessage" /> and buffer size.</summary>
	/// <param name="message">The <see cref="T:System.Web.Services.Protocols.SoapServerMessage" /> with which to initialize the <see cref="T:System.Xml.XmlTextReader" />.</param>
	/// <param name="bufferSize">The buffer size with which to initialize the <see cref="T:System.Xml.XmlTextReader" />.</param>
	/// <returns>A <see cref="T:System.Xml.XmlTextReader" /> initialized with the <see cref="T:System.Web.Services.Protocols.SoapServerMessage" /> specified by <paramref name="message" /> and the buffer size specified by <paramref name="bufferSize" />.</returns>
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	protected virtual XmlReader GetReaderForMessage(SoapServerMessage message, int bufferSize)
	{
		Encoding encoding = RequestResponseUtils.GetEncoding2(message.ContentType);
		if (bufferSize < 512)
		{
			bufferSize = 512;
		}
		int readTimeout = WebServicesSection.Current.SoapEnvelopeProcessing.ReadTimeout;
		long num = ((readTimeout < 0) ? 0 : ((long)readTimeout * 10000000L));
		long ticks = DateTime.UtcNow.Ticks;
		long num2 = ((long.MaxValue - num <= ticks) ? long.MaxValue : (ticks + num));
		XmlTextReader xmlTextReader = ((encoding != null) ? ((num2 != long.MaxValue) ? new SoapEnvelopeReader(new StreamReader(message.Stream, encoding, detectEncodingFromByteOrderMarks: true, bufferSize), num2) : new XmlTextReader(new StreamReader(message.Stream, encoding, detectEncodingFromByteOrderMarks: true, bufferSize))) : ((num2 != long.MaxValue) ? new SoapEnvelopeReader(message.Stream, num2) : new XmlTextReader(message.Stream)));
		xmlTextReader.DtdProcessing = DtdProcessing.Prohibit;
		xmlTextReader.Normalization = true;
		xmlTextReader.XmlResolver = null;
		return xmlTextReader;
	}

	internal override bool Initialize()
	{
		GuessVersion();
		message = new SoapServerMessage(this);
		onewayInitException = null;
		if ((serverType = (SoapServerType)GetFromCache(typeof(SoapServerProtocol), base.Type)) == null && (serverType = (SoapServerType)GetFromCache(typeof(SoapServerProtocol), base.Type, excludeSchemeHostPort: true)) == null)
		{
			lock (ServerProtocol.InternalSyncObject)
			{
				if ((serverType = (SoapServerType)GetFromCache(typeof(SoapServerProtocol), base.Type)) == null && (serverType = (SoapServerType)GetFromCache(typeof(SoapServerProtocol), base.Type, excludeSchemeHostPort: true)) == null)
				{
					bool excludeSchemeHostPort = IsCacheUnderPressure(typeof(SoapServerProtocol), base.Type);
					serverType = new SoapServerType(base.Type, protocolsSupported);
					AddToCache(typeof(SoapServerProtocol), base.Type, serverType, excludeSchemeHostPort);
				}
			}
		}
		Exception ex = null;
		try
		{
			message.highPriConfigExtensions = SoapMessage.InitializeExtensions(serverType.HighPriExtensions, serverType.HighPriExtensionInitializers);
			message.highPriConfigExtensions = ModifyInitializedExtensions(PriorityGroup.High, message.highPriConfigExtensions);
			message.SetStream(base.Request.InputStream);
			message.InitExtensionStreamChain(message.highPriConfigExtensions);
			message.SetStage(SoapMessageStage.BeforeDeserialize);
			message.ContentType = base.Request.ContentType;
			message.ContentEncoding = base.Request.Headers["Content-Encoding"];
			message.RunExtensions(message.highPriConfigExtensions, throwOnException: false);
			ex = message.Exception;
		}
		catch (Exception ex2)
		{
			if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Warning, this, "Initialize", ex2);
			}
			ex = ex2;
		}
		message.allExtensions = message.highPriConfigExtensions;
		GuessVersion();
		try
		{
			serverMethod = RouteRequest(message);
			if (serverMethod == null)
			{
				throw new SoapException(Res.GetString("UnableToHandleRequest0"), new XmlQualifiedName("Server", "http://schemas.xmlsoap.org/soap/envelope/"));
			}
		}
		catch (Exception ex3)
		{
			if (ex3 is ThreadAbortException || ex3 is StackOverflowException || ex3 is OutOfMemoryException)
			{
				throw;
			}
			if (helper.RequestNamespace != null)
			{
				SetHelper(SoapServerProtocolHelper.GetHelper(this, helper.RequestNamespace));
			}
			CheckHelperVersion();
			throw;
		}
		isOneWay = serverMethod.oneWay;
		if (ex == null)
		{
			try
			{
				SoapReflectedExtension[] reflectedExtensions = (SoapReflectedExtension[])CombineExtensionsHelper(serverMethod.extensions, serverType.LowPriExtensions, typeof(SoapReflectedExtension));
				object[] extensionInitializers = (object[])CombineExtensionsHelper(serverMethod.extensionInitializers, serverType.LowPriExtensionInitializers, typeof(object));
				message.otherExtensions = SoapMessage.InitializeExtensions(reflectedExtensions, extensionInitializers);
				message.otherExtensions = ModifyInitializedExtensions(PriorityGroup.Low, message.otherExtensions);
				message.allExtensions = (SoapExtension[])CombineExtensionsHelper(message.highPriConfigExtensions, message.otherExtensions, typeof(SoapExtension));
			}
			catch (Exception ex4)
			{
				if (ex4 is ThreadAbortException || ex4 is StackOverflowException || ex4 is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "Initialize", ex4);
				}
				ex = ex4;
			}
		}
		if (ex != null)
		{
			if (!isOneWay)
			{
				if (ex is SoapException)
				{
					throw ex;
				}
				throw SoapException.Create(Version, Res.GetString("WebConfigExtensionError"), new XmlQualifiedName("Server", "http://schemas.xmlsoap.org/soap/envelope/"), ex);
			}
			onewayInitException = ex;
		}
		return true;
	}

	/// <summary>Applies the specified priority and group attributes to the SOAP extensions contained in the specified array of type <see cref="T:System.Web.Services.Protocols.SoapExtension" />.</summary>
	/// <param name="group">A <see cref="T:System.Web.Services.Configuration.PriorityGroup" /> that specifies the priority and group attributes to be applied to the SOAP extensions contained in <paramref name="extensions" />.</param>
	/// <param name="extensions">An array of type <see cref="T:System.Web.Services.Protocols.SoapExtension" /> to which to apply the priority and group attributes specified by <paramref name="group" />.</param>
	/// <returns>An array of type <see cref="T:System.Web.Services.Protocols.SoapExtension" /> with the priority and group attributes specified by <paramref name="group" /> applied.</returns>
	protected virtual SoapExtension[] ModifyInitializedExtensions(PriorityGroup group, SoapExtension[] extensions)
	{
		return extensions;
	}

	/// <summary>Returns the <see cref="T:System.Web.Services.Protocols.SoapServerMethod" /> to which the specified <see cref="T:System.Web.Services.Protocols.SoapServerMessage" /> should be routed.</summary>
	/// <param name="message">The <see cref="T:System.Web.Services.Protocols.SoapServerMessage" /> sent to the XML Web service.</param>
	/// <returns>The <see cref="T:System.Web.Services.Protocols.SoapServerMethod" /> to which the <see cref="T:System.Web.Services.Protocols.SoapServerMessage" /> specified by <paramref name="message" /> should be routed.</returns>
	protected virtual SoapServerMethod RouteRequest(SoapServerMessage message)
	{
		return helper.RouteRequest();
	}

	private void GuessVersion()
	{
		if (IsSupported(WebServiceProtocols.AnyHttpSoap))
		{
			if (base.Request.Headers["SOAPAction"] == null || ContentType.MatchesBase(base.Request.ContentType, "application/soap+xml"))
			{
				SetHelper(new Soap12ServerProtocolHelper(this));
			}
			else
			{
				SetHelper(new Soap11ServerProtocolHelper(this));
			}
		}
		else if (IsSupported(WebServiceProtocols.HttpSoap))
		{
			SetHelper(new Soap11ServerProtocolHelper(this));
		}
		else if (IsSupported(WebServiceProtocols.HttpSoap12))
		{
			SetHelper(new Soap12ServerProtocolHelper(this));
		}
	}

	internal bool IsSupported(WebServiceProtocols protocol)
	{
		return (protocolsSupported & protocol) == protocol;
	}

	internal override void CreateServerInstance()
	{
		base.CreateServerInstance();
		message.SetStage(SoapMessageStage.AfterDeserialize);
		message.RunExtensions(message.allExtensions, throwOnException: true);
		SoapHeaderHandling.SetHeaderMembers(message.Headers, Target, serverMethod.inHeaderMappings, SoapHeaderDirection.In, client: false);
	}

	private void SetHelper(SoapServerProtocolHelper helper)
	{
		this.helper = helper;
		version = helper.Version;
		base.Context.Items[WebService.SoapVersionContextSlot] = helper.Version;
	}

	private static Array CombineExtensionsHelper(Array array1, Array array2, Type elementType)
	{
		if (array1 == null)
		{
			return array2;
		}
		if (array2 == null)
		{
			return array1;
		}
		int num = array1.Length + array2.Length;
		if (num == 0)
		{
			return null;
		}
		Array array3 = null;
		if (elementType == typeof(SoapReflectedExtension))
		{
			array3 = new SoapReflectedExtension[num];
		}
		else if (elementType == typeof(SoapExtension))
		{
			array3 = new SoapExtension[num];
		}
		else
		{
			if (!(elementType == typeof(object)))
			{
				throw new ArgumentException(Res.GetString("ElementTypeMustBeObjectOrSoapExtensionOrSoapReflectedException"), "elementType");
			}
			array3 = new object[num];
		}
		Array.Copy(array1, 0, array3, 0, array1.Length);
		Array.Copy(array2, 0, array3, array1.Length, array2.Length);
		return array3;
	}

	private void CheckHelperVersion()
	{
		if (helper.RequestNamespace == null)
		{
			return;
		}
		if (helper.RequestNamespace != helper.EnvelopeNs)
		{
			string requestNamespace = helper.RequestNamespace;
			if (IsSupported(WebServiceProtocols.HttpSoap))
			{
				SetHelper(new Soap11ServerProtocolHelper(this));
			}
			else
			{
				SetHelper(new Soap12ServerProtocolHelper(this));
			}
			throw new SoapException(Res.GetString("WebInvalidEnvelopeNamespace", requestNamespace, helper.EnvelopeNs), SoapException.VersionMismatchFaultCode);
		}
		if (IsSupported(helper.Protocol))
		{
			return;
		}
		string requestNamespace2 = helper.RequestNamespace;
		string text = (IsSupported(WebServiceProtocols.HttpSoap) ? "http://schemas.xmlsoap.org/soap/envelope/" : "http://www.w3.org/2003/05/soap-envelope");
		SetHelper(new Soap11ServerProtocolHelper(this));
		throw new SoapException(Res.GetString("WebInvalidEnvelopeNamespace", requestNamespace2, text), SoapException.VersionMismatchFaultCode);
	}

	internal override object[] ReadParameters()
	{
		message.InitExtensionStreamChain(message.otherExtensions);
		message.RunExtensions(message.otherExtensions, throwOnException: true);
		if (!ContentType.IsSoap(message.ContentType))
		{
			throw new SoapException(Res.GetString("WebRequestContent", message.ContentType, helper.HttpContentType), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"), new SoapFaultSubCode(Soap12FaultCodes.UnsupportedMediaTypeFaultCode));
		}
		XmlReader xmlReader = null;
		try
		{
			xmlReader = GetXmlReader();
			xmlReader.MoveToContent();
			SetHelper(SoapServerProtocolHelper.GetHelper(this, xmlReader.NamespaceURI));
		}
		catch (XmlException innerException)
		{
			throw new SoapException(Res.GetString("WebRequestUnableToRead"), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"), innerException);
		}
		CheckHelperVersion();
		if (version == SoapProtocolVersion.Soap11 && !ContentType.MatchesBase(message.ContentType, helper.HttpContentType))
		{
			throw new SoapException(Res.GetString("WebRequestContent", message.ContentType, helper.HttpContentType), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"), new SoapFaultSubCode(Soap12FaultCodes.UnsupportedMediaTypeFaultCode));
		}
		if (message.Exception != null)
		{
			throw message.Exception;
		}
		try
		{
			if (!xmlReader.IsStartElement("Envelope", helper.EnvelopeNs))
			{
				throw new InvalidOperationException(Res.GetString("WebMissingEnvelopeElement"));
			}
			if (xmlReader.IsEmptyElement)
			{
				throw new InvalidOperationException(Res.GetString("WebMissingBodyElement"));
			}
			int depth = xmlReader.Depth;
			xmlReader.ReadStartElement("Envelope", helper.EnvelopeNs);
			xmlReader.MoveToContent();
			bool checkRequiredHeaders = (serverMethod.wsiClaims & WsiProfiles.BasicProfile1_1) != 0 && version != SoapProtocolVersion.Soap12;
			string text = new SoapHeaderHandling().ReadHeaders(xmlReader, serverMethod.inHeaderSerializer, message.Headers, serverMethod.inHeaderMappings, SoapHeaderDirection.In, helper.EnvelopeNs, (serverMethod.use == SoapBindingUse.Encoded) ? helper.EncodingNs : null, checkRequiredHeaders);
			if (text != null)
			{
				throw new SoapHeaderException(Res.GetString("WebMissingHeader", text), new XmlQualifiedName("MustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/"));
			}
			if (!xmlReader.IsStartElement("Body", helper.EnvelopeNs))
			{
				throw new InvalidOperationException(Res.GetString("WebMissingBodyElement"));
			}
			xmlReader.ReadStartElement("Body", helper.EnvelopeNs);
			xmlReader.MoveToContent();
			bool flag = serverMethod.use == SoapBindingUse.Encoded;
			TraceMethod caller = (Tracing.On ? new TraceMethod(this, "ReadParameters") : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceReadRequest"), caller, new TraceMethod(serverMethod.parameterSerializer, "Deserialize", xmlReader, (serverMethod.use == SoapBindingUse.Encoded) ? helper.EncodingNs : null));
			}
			object[] array;
			if (!flag && (WebServicesSection.Current.SoapEnvelopeProcessing.IsStrict || Tracing.On))
			{
				XmlDeserializationEvents events = (Tracing.On ? Tracing.GetDeserializationEvents() : RuntimeUtils.GetDeserializationEvents());
				array = (object[])serverMethod.parameterSerializer.Deserialize(xmlReader, null, events);
			}
			else
			{
				array = (object[])serverMethod.parameterSerializer.Deserialize(xmlReader, flag ? helper.EncodingNs : null);
			}
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceReadRequest"), caller);
			}
			while (depth < xmlReader.Depth && xmlReader.Read())
			{
			}
			if (xmlReader.NodeType == XmlNodeType.EndElement)
			{
				xmlReader.Read();
			}
			message.SetParameterValues(array);
			return array;
		}
		catch (SoapException)
		{
			throw;
		}
		catch (Exception ex2)
		{
			if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
			{
				throw;
			}
			throw new SoapException(Res.GetString("WebRequestUnableToRead"), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"), ex2);
		}
	}

	internal override void WriteReturns(object[] returnValues, Stream outputStream)
	{
		if (!serverMethod.oneWay)
		{
			bool flag = serverMethod.use == SoapBindingUse.Encoded;
			SoapHeaderHandling.EnsureHeadersUnderstood(message.Headers);
			message.Headers.Clear();
			SoapHeaderHandling.GetHeaderMembers(message.Headers, Target, serverMethod.outHeaderMappings, SoapHeaderDirection.Out, client: false);
			if (message.allExtensions != null)
			{
				message.SetExtensionStream(new SoapExtensionStream());
			}
			message.InitExtensionStreamChain(message.allExtensions);
			message.SetStage(SoapMessageStage.BeforeSerialize);
			message.ContentType = ContentType.Compose(helper.HttpContentType, Encoding.UTF8);
			message.SetParameterValues(returnValues);
			message.RunExtensions(message.allExtensions, throwOnException: true);
			message.SetStream(outputStream);
			base.Response.ContentType = message.ContentType;
			if (message.ContentEncoding != null && message.ContentEncoding.Length > 0)
			{
				base.Response.AppendHeader("Content-Encoding", message.ContentEncoding);
			}
			XmlWriter writerForMessage = GetWriterForMessage(message, 1024);
			if (writerForMessage == null)
			{
				throw new InvalidOperationException(Res.GetString("WebNullWriterForMessage"));
			}
			writerForMessage.WriteStartDocument();
			writerForMessage.WriteStartElement("soap", "Envelope", helper.EnvelopeNs);
			writerForMessage.WriteAttributeString("xmlns", "soap", null, helper.EnvelopeNs);
			if (flag)
			{
				writerForMessage.WriteAttributeString("xmlns", "soapenc", null, helper.EncodingNs);
				writerForMessage.WriteAttributeString("xmlns", "tns", null, serverType.serviceNamespace);
				writerForMessage.WriteAttributeString("xmlns", "types", null, SoapReflector.GetEncodedNamespace(serverType.serviceNamespace, serverType.serviceDefaultIsEncoded));
			}
			if (serverMethod.rpc && version == SoapProtocolVersion.Soap12)
			{
				writerForMessage.WriteAttributeString("xmlns", "rpc", null, "http://www.w3.org/2003/05/soap-rpc");
			}
			writerForMessage.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
			writerForMessage.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
			SoapHeaderHandling.WriteHeaders(writerForMessage, serverMethod.outHeaderSerializer, message.Headers, serverMethod.outHeaderMappings, SoapHeaderDirection.Out, flag, serverType.serviceNamespace, serverType.serviceDefaultIsEncoded, helper.EnvelopeNs);
			writerForMessage.WriteStartElement("Body", helper.EnvelopeNs);
			if (flag && version != SoapProtocolVersion.Soap12)
			{
				writerForMessage.WriteAttributeString("soap", "encodingStyle", null, helper.EncodingNs);
			}
			TraceMethod caller = (Tracing.On ? new TraceMethod(this, "WriteReturns") : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceWriteResponse"), caller, new TraceMethod(serverMethod.returnSerializer, "Serialize", writerForMessage, returnValues, null, flag ? helper.EncodingNs : null));
			}
			serverMethod.returnSerializer.Serialize(writerForMessage, returnValues, null, flag ? helper.EncodingNs : null);
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceWriteResponse"), caller);
			}
			writerForMessage.WriteEndElement();
			writerForMessage.WriteEndElement();
			writerForMessage.Flush();
			message.SetStage(SoapMessageStage.AfterSerialize);
			message.RunExtensions(message.allExtensions, throwOnException: true);
		}
	}

	internal override bool WriteException(Exception e, Stream outputStream)
	{
		if (message == null)
		{
			return false;
		}
		message.Headers.Clear();
		if (serverMethod != null && Target != null)
		{
			SoapHeaderHandling.GetHeaderMembers(message.Headers, Target, serverMethod.outHeaderMappings, SoapHeaderDirection.Fault, client: false);
		}
		SoapException ex = ((e is SoapException) ? ((SoapException)e) : ((serverMethod == null || !serverMethod.rpc || helper.Version != SoapProtocolVersion.Soap12 || !(e is ArgumentException)) ? SoapException.Create(Version, Res.GetString("WebRequestUnableToProcess"), new XmlQualifiedName("Server", "http://schemas.xmlsoap.org/soap/envelope/"), e) : SoapException.Create(Version, Res.GetString("WebRequestUnableToProcess"), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"), null, null, null, new SoapFaultSubCode(Soap12FaultCodes.RpcBadArgumentsFaultCode), e)));
		if (SoapException.IsVersionMismatchFaultCode(ex.Code) && IsSupported(WebServiceProtocols.HttpSoap12))
		{
			SoapUnknownHeader soapUnknownHeader = CreateUpgradeHeader();
			if (soapUnknownHeader != null)
			{
				Message.Headers.Add(soapUnknownHeader);
			}
		}
		base.Response.ClearHeaders();
		base.Response.Clear();
		HttpStatusCode statusCode = helper.SetResponseErrorCode(base.Response, ex);
		bool flag = false;
		SoapExtensionStream soapExtensionStream = new SoapExtensionStream();
		if (message.allExtensions != null)
		{
			message.SetExtensionStream(soapExtensionStream);
		}
		try
		{
			message.InitExtensionStreamChain(message.allExtensions);
		}
		catch (Exception ex2)
		{
			if (ex2 is ThreadAbortException || ex2 is StackOverflowException || ex2 is OutOfMemoryException)
			{
				throw;
			}
			if (Tracing.On)
			{
				Tracing.ExceptionCatch(TraceEventType.Warning, this, "WriteException", ex2);
			}
			flag = true;
		}
		message.SetStage(SoapMessageStage.BeforeSerialize);
		message.ContentType = ContentType.Compose(helper.HttpContentType, Encoding.UTF8);
		message.Exception = ex;
		if (!flag)
		{
			try
			{
				message.RunExtensions(message.allExtensions, throwOnException: false);
			}
			catch (Exception ex3)
			{
				if (ex3 is ThreadAbortException || ex3 is StackOverflowException || ex3 is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "WriteException", ex3);
				}
				flag = true;
			}
		}
		message.SetStream(outputStream);
		base.Response.ContentType = message.ContentType;
		if (message.ContentEncoding != null && message.ContentEncoding.Length > 0)
		{
			base.Response.AppendHeader("Content-Encoding", message.ContentEncoding);
		}
		XmlWriter writerForMessage = GetWriterForMessage(message, 512);
		if (writerForMessage == null)
		{
			throw new InvalidOperationException(Res.GetString("WebNullWriterForMessage"));
		}
		helper.WriteFault(writerForMessage, message.Exception, statusCode);
		if (!flag)
		{
			SoapException ex4 = null;
			try
			{
				message.SetStage(SoapMessageStage.AfterSerialize);
				message.RunExtensions(message.allExtensions, throwOnException: false);
			}
			catch (Exception ex5)
			{
				if (ex5 is ThreadAbortException || ex5 is StackOverflowException || ex5 is OutOfMemoryException)
				{
					throw;
				}
				if (Tracing.On)
				{
					Tracing.ExceptionCatch(TraceEventType.Warning, this, "WriteException", ex5);
				}
				if (!soapExtensionStream.HasWritten)
				{
					ex4 = SoapException.Create(Version, Res.GetString("WebExtensionError"), new XmlQualifiedName("Server", "http://schemas.xmlsoap.org/soap/envelope/"), ex5);
				}
			}
			if (ex4 != null)
			{
				base.Response.ContentType = ContentType.Compose("text/plain", Encoding.UTF8);
				StreamWriter streamWriter = new StreamWriter(outputStream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
				streamWriter.WriteLine(GenerateFaultString(message.Exception));
				streamWriter.Flush();
			}
		}
		return true;
	}

	private bool WriteException_TryWriteFault(SoapServerMessage message, Stream outputStream, HttpStatusCode statusCode, bool disableExtensions)
	{
		return true;
	}

	internal SoapUnknownHeader CreateUpgradeHeader()
	{
		XmlDocument xmlDocument = new XmlDocument();
		XmlElement xmlElement = xmlDocument.CreateElement("soap12", "Upgrade", "http://www.w3.org/2003/05/soap-envelope");
		if (IsSupported(WebServiceProtocols.HttpSoap))
		{
			xmlElement.AppendChild(CreateUpgradeEnvelope(xmlDocument, "soap", "http://schemas.xmlsoap.org/soap/envelope/"));
		}
		if (IsSupported(WebServiceProtocols.HttpSoap12))
		{
			xmlElement.AppendChild(CreateUpgradeEnvelope(xmlDocument, "soap12", "http://www.w3.org/2003/05/soap-envelope"));
		}
		return new SoapUnknownHeader
		{
			Element = xmlElement
		};
	}

	private static XmlElement CreateUpgradeEnvelope(XmlDocument doc, string prefix, string envelopeNs)
	{
		XmlElement xmlElement = doc.CreateElement("soap12", "SupportedEnvelope", "http://www.w3.org/2003/05/soap-envelope");
		XmlAttribute xmlAttribute = doc.CreateAttribute("xmlns", prefix, "http://www.w3.org/2000/xmlns/");
		xmlAttribute.Value = envelopeNs;
		XmlAttribute xmlAttribute2 = doc.CreateAttribute("qname");
		xmlAttribute2.Value = prefix + ":Envelope";
		xmlElement.Attributes.Append(xmlAttribute2);
		xmlElement.Attributes.Append(xmlAttribute);
		return xmlElement;
	}

	internal XmlReader GetXmlReader()
	{
		Encoding encoding = RequestResponseUtils.GetEncoding2(Message.ContentType);
		if (serverMethod != null && (serverMethod.wsiClaims & WsiProfiles.BasicProfile1_1) != 0 && Version != SoapProtocolVersion.Soap12 && encoding != null && !(encoding is UTF8Encoding) && !(encoding is UnicodeEncoding))
		{
			throw new InvalidOperationException(Res.GetString("WebWsiContentTypeEncoding"));
		}
		return GetReaderForMessage(Message, RequestResponseUtils.GetBufferSize(base.Request.ContentLength)) ?? throw new InvalidOperationException(Res.GetString("WebNullReaderForMessage"));
	}
}
