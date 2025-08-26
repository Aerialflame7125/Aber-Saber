using System.Web.Services.Configuration;

namespace System.Web.Services.Description;

/// <summary>Represents a service description format extension applied to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" /> when an XML Web service supports the SOAP protocol version 1.2. This class cannot be inherited.</summary>
[XmlFormatExtension("header", "http://schemas.xmlsoap.org/wsdl/soap12/", typeof(InputBinding), typeof(OutputBinding))]
public sealed class Soap12HeaderBinding : SoapHeaderBinding
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Soap12HeaderBinding" /> class. </summary>
	public Soap12HeaderBinding()
	{
	}
}
