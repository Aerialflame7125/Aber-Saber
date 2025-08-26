using System.Web.Services.Configuration;

namespace System.Web.Services.Description;

/// <summary>Represents a service description format extension applied to an <see cref="T:System.Web.Services.Description.FaultBinding" /> when an XML Web service supports the SOAP protocol version 1.2. This class cannot be inherited.</summary>
[XmlFormatExtension("fault", "http://schemas.xmlsoap.org/wsdl/soap12/", typeof(FaultBinding))]
public sealed class Soap12FaultBinding : SoapFaultBinding
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Soap12FaultBinding" /> class. </summary>
	public Soap12FaultBinding()
	{
	}
}
