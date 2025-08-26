using System.Net;
using System.Threading;
using System.Web.Services.Configuration;
using System.Web.Services.Description;
using System.Web.Services.Diagnostics;
using System.Xml;

namespace System.Web.Services.Protocols;

internal class Soap11ServerProtocolHelper : SoapServerProtocolHelper
{
	internal override SoapProtocolVersion Version => SoapProtocolVersion.Soap11;

	internal override WebServiceProtocols Protocol => WebServiceProtocols.HttpSoap;

	internal override string EnvelopeNs => "http://schemas.xmlsoap.org/soap/envelope/";

	internal override string EncodingNs => "http://schemas.xmlsoap.org/soap/encoding/";

	internal override string HttpContentType => "text/xml";

	internal Soap11ServerProtocolHelper(SoapServerProtocol protocol)
		: base(protocol)
	{
	}

	internal Soap11ServerProtocolHelper(SoapServerProtocol protocol, string requestNamespace)
		: base(protocol, requestNamespace)
	{
	}

	internal override SoapServerMethod RouteRequest()
	{
		string text = base.ServerProtocol.Request.Headers["SOAPAction"];
		if (text == null)
		{
			throw new SoapException(Res.GetString("UnableToHandleRequestActionRequired0"), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"));
		}
		object obj;
		if (base.ServerType.routingOnSoapAction)
		{
			if (text.StartsWith("\"", StringComparison.Ordinal) && text.EndsWith("\"", StringComparison.Ordinal))
			{
				text = text.Substring(1, text.Length - 2);
			}
			obj = HttpUtility.UrlDecode(text);
		}
		else
		{
			try
			{
				obj = GetRequestElement();
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
				throw new SoapException(Res.GetString("TheRootElementForTheRequestCouldNotBeDetermined0"), new XmlQualifiedName("Server", "http://schemas.xmlsoap.org/soap/envelope/"), ex2);
			}
		}
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "RouteRequest") : null);
		if (Tracing.On)
		{
			Tracing.Enter("RouteRequest", caller, new TraceMethod(base.ServerType, "GetMethod", obj), Tracing.Details(base.ServerProtocol.Request));
		}
		SoapServerMethod method = base.ServerType.GetMethod(obj);
		if (Tracing.On)
		{
			Tracing.Exit("RouteRequest", caller);
		}
		if (method == null)
		{
			if (base.ServerType.routingOnSoapAction)
			{
				throw new SoapException(Res.GetString("WebHttpHeader", "SOAPAction", (string)obj), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"));
			}
			throw new SoapException(Res.GetString("TheRequestElementXmlnsWasNotRecognized2", ((XmlQualifiedName)obj).Name, ((XmlQualifiedName)obj).Namespace), new XmlQualifiedName("Client", "http://schemas.xmlsoap.org/soap/envelope/"));
		}
		return method;
	}

	internal override void WriteFault(XmlWriter writer, SoapException soapException, HttpStatusCode statusCode)
	{
		if (statusCode != HttpStatusCode.InternalServerError || soapException == null)
		{
			return;
		}
		SoapServerMessage message = base.ServerProtocol.Message;
		writer.WriteStartDocument();
		writer.WriteStartElement("soap", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
		writer.WriteAttributeString("xmlns", "soap", null, "http://schemas.xmlsoap.org/soap/envelope/");
		writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
		writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
		if (base.ServerProtocol.ServerMethod != null)
		{
			SoapHeaderHandling.WriteHeaders(writer, base.ServerProtocol.ServerMethod.outHeaderSerializer, message.Headers, base.ServerProtocol.ServerMethod.outHeaderMappings, SoapHeaderDirection.Fault, base.ServerProtocol.ServerMethod.use == SoapBindingUse.Encoded, base.ServerType.serviceNamespace, base.ServerType.serviceDefaultIsEncoded, "http://schemas.xmlsoap.org/soap/envelope/");
		}
		else
		{
			SoapHeaderHandling.WriteUnknownHeaders(writer, message.Headers, "http://schemas.xmlsoap.org/soap/envelope/");
		}
		writer.WriteStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
		writer.WriteStartElement("Fault", "http://schemas.xmlsoap.org/soap/envelope/");
		writer.WriteStartElement("faultcode", "");
		XmlQualifiedName xmlQualifiedName = TranslateFaultCode(soapException.Code);
		if (xmlQualifiedName.Namespace != null && xmlQualifiedName.Namespace.Length > 0 && writer.LookupPrefix(xmlQualifiedName.Namespace) == null)
		{
			writer.WriteAttributeString("xmlns", "q0", null, xmlQualifiedName.Namespace);
		}
		writer.WriteQualifiedName(xmlQualifiedName.Name, xmlQualifiedName.Namespace);
		writer.WriteEndElement();
		writer.WriteStartElement("faultstring", "");
		if (soapException.Lang != null && soapException.Lang.Length != 0)
		{
			writer.WriteAttributeString("xml", "lang", "http://www.w3.org/XML/1998/namespace", soapException.Lang);
		}
		writer.WriteString(base.ServerProtocol.GenerateFaultString(soapException));
		writer.WriteEndElement();
		string actor = soapException.Actor;
		if (actor.Length > 0)
		{
			writer.WriteElementString("faultactor", "", actor);
		}
		if (!(soapException is SoapHeaderException))
		{
			if (soapException.Detail == null)
			{
				writer.WriteStartElement("detail", "");
				writer.WriteEndElement();
			}
			else
			{
				soapException.Detail.WriteTo(writer);
			}
		}
		writer.WriteEndElement();
		writer.WriteEndElement();
		writer.WriteEndElement();
		writer.Flush();
	}

	private static XmlQualifiedName TranslateFaultCode(XmlQualifiedName code)
	{
		if (code.Namespace == "http://schemas.xmlsoap.org/soap/envelope/")
		{
			return code;
		}
		if (code.Namespace == "http://www.w3.org/2003/05/soap-envelope")
		{
			if (code.Name == "Receiver")
			{
				return SoapException.ServerFaultCode;
			}
			if (code.Name == "Sender")
			{
				return SoapException.ClientFaultCode;
			}
			if (code.Name == "MustUnderstand")
			{
				return SoapException.MustUnderstandFaultCode;
			}
			if (code.Name == "VersionMismatch")
			{
				return SoapException.VersionMismatchFaultCode;
			}
		}
		return code;
	}
}
