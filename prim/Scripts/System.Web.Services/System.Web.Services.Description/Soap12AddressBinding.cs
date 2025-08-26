using System.Web.Services.Configuration;

namespace System.Web.Services.Description;

/// <summary>Represents a service description format extension applied to a <see cref="T:System.Web.Services.Description.Port" /> when an XML Web service supports the SOAP protocol version 1.2. This class cannot be inherited.</summary>
[XmlFormatExtension("address", "http://schemas.xmlsoap.org/wsdl/soap12/", typeof(Port))]
public sealed class Soap12AddressBinding : SoapAddressBinding
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Soap12AddressBinding" /> class. </summary>
	public Soap12AddressBinding()
	{
	}
}
