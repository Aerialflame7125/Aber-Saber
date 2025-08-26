using System.Web.Services.Configuration;

namespace System.Web.Services.Description;

/// <summary>Represents a binding in a Web Services Description Language (WSDL) document to the SOAP version 1.2 protocol. This class cannot be inherited.</summary>
[XmlFormatExtension("binding", "http://schemas.xmlsoap.org/wsdl/soap12/", typeof(Binding))]
[XmlFormatExtensionPrefix("soap12", "http://schemas.xmlsoap.org/wsdl/soap12/")]
public sealed class Soap12Binding : SoapBinding
{
	/// <summary>Represents the XML namespace of a binding to the SOAP protocol version 1.2. This field is constant.</summary>
	public new const string Namespace = "http://schemas.xmlsoap.org/wsdl/soap12/";

	/// <summary>Represents the transport protocol for the SOAP message is HTTP. This field is constant.</summary>
	public new const string HttpTransport = "http://schemas.xmlsoap.org/soap/http";

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Soap12Binding" /> class. </summary>
	public Soap12Binding()
	{
	}
}
