using System.Net;
using System.Web.Services.Configuration;
using System.Xml;

namespace System.Web.Services.Protocols;

internal abstract class SoapServerProtocolHelper
{
	private SoapServerProtocol protocol;

	private string requestNamespace;

	internal abstract SoapProtocolVersion Version { get; }

	internal abstract WebServiceProtocols Protocol { get; }

	internal abstract string EnvelopeNs { get; }

	internal abstract string EncodingNs { get; }

	internal abstract string HttpContentType { get; }

	internal string RequestNamespace => requestNamespace;

	protected SoapServerProtocol ServerProtocol => protocol;

	protected SoapServerType ServerType => (SoapServerType)protocol.ServerType;

	protected SoapServerProtocolHelper(SoapServerProtocol protocol)
	{
		this.protocol = protocol;
	}

	protected SoapServerProtocolHelper(SoapServerProtocol protocol, string requestNamespace)
	{
		this.protocol = protocol;
		this.requestNamespace = requestNamespace;
	}

	internal static SoapServerProtocolHelper GetHelper(SoapServerProtocol protocol, string envelopeNs)
	{
		if (envelopeNs == "http://schemas.xmlsoap.org/soap/envelope/")
		{
			return new Soap11ServerProtocolHelper(protocol, envelopeNs);
		}
		if (envelopeNs == "http://www.w3.org/2003/05/soap-envelope")
		{
			return new Soap12ServerProtocolHelper(protocol, envelopeNs);
		}
		return new Soap11ServerProtocolHelper(protocol, envelopeNs);
	}

	internal HttpStatusCode SetResponseErrorCode(HttpResponse response, SoapException soapException)
	{
		if (soapException.SubCode != null && soapException.SubCode.Code == Soap12FaultCodes.UnsupportedMediaTypeFaultCode)
		{
			response.StatusCode = 415;
			soapException.ClearSubCode();
		}
		else if (SoapException.IsClientFaultCode(soapException.Code))
		{
			System.Web.Services.Protocols.ServerProtocol.SetHttpResponseStatusCode(response, 500);
			for (Exception ex = soapException; ex != null; ex = ex.InnerException)
			{
				if (ex is XmlException)
				{
					response.StatusCode = 400;
				}
			}
		}
		else
		{
			System.Web.Services.Protocols.ServerProtocol.SetHttpResponseStatusCode(response, 500);
		}
		response.StatusDescription = HttpWorkerRequest.GetStatusDescription(response.StatusCode);
		return (HttpStatusCode)response.StatusCode;
	}

	internal abstract void WriteFault(XmlWriter writer, SoapException soapException, HttpStatusCode statusCode);

	internal abstract SoapServerMethod RouteRequest();

	protected XmlQualifiedName GetRequestElement()
	{
		SoapServerMessage message = ServerProtocol.Message;
		long position = message.Stream.Position;
		XmlReader xmlReader = protocol.GetXmlReader();
		xmlReader.MoveToContent();
		requestNamespace = xmlReader.NamespaceURI;
		if (!xmlReader.IsStartElement("Envelope", requestNamespace))
		{
			throw new InvalidOperationException(Res.GetString("WebMissingEnvelopeElement"));
		}
		if (xmlReader.IsEmptyElement)
		{
			throw new InvalidOperationException(Res.GetString("WebMissingBodyElement"));
		}
		xmlReader.ReadStartElement("Envelope", requestNamespace);
		xmlReader.MoveToContent();
		while (!xmlReader.EOF && !xmlReader.IsStartElement("Body", requestNamespace))
		{
			xmlReader.Skip();
		}
		if (xmlReader.EOF)
		{
			throw new InvalidOperationException(Res.GetString("WebMissingBodyElement"));
		}
		XmlQualifiedName result;
		if (xmlReader.IsEmptyElement)
		{
			result = XmlQualifiedName.Empty;
		}
		else
		{
			xmlReader.ReadStartElement("Body", requestNamespace);
			xmlReader.MoveToContent();
			result = new XmlQualifiedName(xmlReader.LocalName, xmlReader.NamespaceURI);
		}
		message.Stream.Position = position;
		return result;
	}
}
