using System.Net;
using System.Web.Services.Configuration;
using System.Web.Services.Description;
using System.Web.Services.Diagnostics;
using System.Xml;

namespace System.Web.Services.Protocols;

internal class Soap12ServerProtocolHelper : SoapServerProtocolHelper
{
	internal override SoapProtocolVersion Version => SoapProtocolVersion.Soap12;

	internal override WebServiceProtocols Protocol => WebServiceProtocols.HttpSoap12;

	internal override string EnvelopeNs => "http://www.w3.org/2003/05/soap-envelope";

	internal override string EncodingNs => "http://www.w3.org/2003/05/soap-encoding";

	internal override string HttpContentType => "application/soap+xml";

	internal Soap12ServerProtocolHelper(SoapServerProtocol protocol)
		: base(protocol)
	{
	}

	internal Soap12ServerProtocolHelper(SoapServerProtocol protocol, string requestNamespace)
		: base(protocol, requestNamespace)
	{
	}

	internal override SoapServerMethod RouteRequest()
	{
		string text = ContentType.GetAction(base.ServerProtocol.Request.ContentType);
		SoapServerMethod soapServerMethod = null;
		bool flag = false;
		bool flag2 = false;
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "RouteRequest") : null);
		if (text != null && text.Length > 0)
		{
			text = HttpUtility.UrlDecode(text);
			if (Tracing.On)
			{
				Tracing.Enter("RouteRequest", caller, new TraceMethod(base.ServerType, "GetMethod", text), Tracing.Details(base.ServerProtocol.Request));
			}
			soapServerMethod = base.ServerType.GetMethod(text);
			if (Tracing.On)
			{
				Tracing.Exit("RouteRequest", caller);
			}
			if (soapServerMethod != null && base.ServerType.GetDuplicateMethod(text) != null)
			{
				soapServerMethod = null;
				flag = true;
			}
		}
		XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Empty;
		if (soapServerMethod == null)
		{
			xmlQualifiedName = GetRequestElement();
			if (Tracing.On)
			{
				Tracing.Enter("RouteRequest", caller, new TraceMethod(base.ServerType, "GetMethod", xmlQualifiedName), Tracing.Details(base.ServerProtocol.Request));
			}
			soapServerMethod = base.ServerType.GetMethod(xmlQualifiedName);
			if (Tracing.On)
			{
				Tracing.Exit("RouteRequest", caller);
			}
			if (soapServerMethod != null && base.ServerType.GetDuplicateMethod(xmlQualifiedName) != null)
			{
				soapServerMethod = null;
				flag2 = true;
			}
		}
		if (soapServerMethod == null)
		{
			if (text == null || text.Length == 0)
			{
				throw new SoapException(Res.GetString("UnableToHandleRequestActionRequired0"), Soap12FaultCodes.SenderFaultCode);
			}
			if (flag)
			{
				if (flag2)
				{
					throw new SoapException(Res.GetString("UnableToHandleRequest0"), Soap12FaultCodes.ReceiverFaultCode);
				}
				throw new SoapException(Res.GetString("TheRequestElementXmlnsWasNotRecognized2", xmlQualifiedName.Name, xmlQualifiedName.Namespace), Soap12FaultCodes.SenderFaultCode);
			}
			throw new SoapException(Res.GetString("UnableToHandleRequestActionNotRecognized1", text), Soap12FaultCodes.SenderFaultCode);
		}
		return soapServerMethod;
	}

	internal override void WriteFault(XmlWriter writer, SoapException soapException, HttpStatusCode statusCode)
	{
		if (statusCode != HttpStatusCode.InternalServerError || soapException == null)
		{
			return;
		}
		writer.WriteStartDocument();
		writer.WriteStartElement("soap", "Envelope", "http://www.w3.org/2003/05/soap-envelope");
		writer.WriteAttributeString("xmlns", "soap", null, "http://www.w3.org/2003/05/soap-envelope");
		writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
		writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
		if (base.ServerProtocol.ServerMethod != null)
		{
			SoapHeaderHandling.WriteHeaders(writer, base.ServerProtocol.ServerMethod.outHeaderSerializer, base.ServerProtocol.Message.Headers, base.ServerProtocol.ServerMethod.outHeaderMappings, SoapHeaderDirection.Fault, base.ServerProtocol.ServerMethod.use == SoapBindingUse.Encoded, base.ServerType.serviceNamespace, base.ServerType.serviceDefaultIsEncoded, "http://www.w3.org/2003/05/soap-envelope");
		}
		else
		{
			SoapHeaderHandling.WriteUnknownHeaders(writer, base.ServerProtocol.Message.Headers, "http://www.w3.org/2003/05/soap-envelope");
		}
		writer.WriteStartElement("Body", "http://www.w3.org/2003/05/soap-envelope");
		writer.WriteStartElement("Fault", "http://www.w3.org/2003/05/soap-envelope");
		writer.WriteStartElement("Code", "http://www.w3.org/2003/05/soap-envelope");
		WriteFaultCodeValue(writer, TranslateFaultCode(soapException.Code), soapException.SubCode);
		writer.WriteEndElement();
		writer.WriteStartElement("Reason", "http://www.w3.org/2003/05/soap-envelope");
		writer.WriteStartElement("Text", "http://www.w3.org/2003/05/soap-envelope");
		writer.WriteAttributeString("xml", "lang", "http://www.w3.org/XML/1998/namespace", Res.GetString("XmlLang"));
		writer.WriteString(base.ServerProtocol.GenerateFaultString(soapException));
		writer.WriteEndElement();
		writer.WriteEndElement();
		string actor = soapException.Actor;
		if (actor.Length > 0)
		{
			writer.WriteElementString("Node", "http://www.w3.org/2003/05/soap-envelope", actor);
		}
		string role = soapException.Role;
		if (role.Length > 0)
		{
			writer.WriteElementString("Role", "http://www.w3.org/2003/05/soap-envelope", role);
		}
		if (!(soapException is SoapHeaderException))
		{
			if (soapException.Detail == null)
			{
				writer.WriteStartElement("Detail", "http://www.w3.org/2003/05/soap-envelope");
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

	private static void WriteFaultCodeValue(XmlWriter writer, XmlQualifiedName code, SoapFaultSubCode subcode)
	{
		if (!(code == null))
		{
			writer.WriteStartElement("Value", "http://www.w3.org/2003/05/soap-envelope");
			if (code.Namespace != null && code.Namespace.Length > 0 && writer.LookupPrefix(code.Namespace) == null)
			{
				writer.WriteAttributeString("xmlns", "q0", null, code.Namespace);
			}
			writer.WriteQualifiedName(code.Name, code.Namespace);
			writer.WriteEndElement();
			if (subcode != null)
			{
				writer.WriteStartElement("Subcode", "http://www.w3.org/2003/05/soap-envelope");
				WriteFaultCodeValue(writer, subcode.Code, subcode.SubCode);
				writer.WriteEndElement();
			}
		}
	}

	private static XmlQualifiedName TranslateFaultCode(XmlQualifiedName code)
	{
		if (code.Namespace == "http://schemas.xmlsoap.org/soap/envelope/")
		{
			if (code.Name == "Server")
			{
				return Soap12FaultCodes.ReceiverFaultCode;
			}
			if (code.Name == "Client")
			{
				return Soap12FaultCodes.SenderFaultCode;
			}
			if (code.Name == "MustUnderstand")
			{
				return Soap12FaultCodes.MustUnderstandFaultCode;
			}
			if (code.Name == "VersionMismatch")
			{
				return Soap12FaultCodes.VersionMismatchFaultCode;
			}
		}
		return code;
	}
}
