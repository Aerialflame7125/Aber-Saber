namespace System.Web.Services.Protocols;

/// <summary>Specifies how parameters are formatted in a SOAP message.</summary>
public enum SoapParameterStyle
{
	/// <summary>Specifies using the default <see cref="T:System.Web.Services.Protocols.SoapParameterStyle" /> for the XML Web service. The default for an XML Web service can be set by applying a <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> to the class implementing the XML Web service. If a <see cref="T:System.Web.Services.Protocols.SoapDocumentServiceAttribute" /> is not applied to the class implementing the XML Web service, the default is <see cref="F:System.Web.Services.Protocols.SoapParameterStyle.Wrapped" />.</summary>
	Default,
	/// <summary>Parameters sent to and from an XML Web service method are placed in XML elements directly following the <see langword="Body" /> element of a SOAP request or SOAP response.</summary>
	Bare,
	/// <summary>Parameters sent to and from an XML Web service method are encapsulated within a single XML element followig the <see langword="Body" /> element of the XML portion of a SOAP request or SOAP response.</summary>
	Wrapped
}
