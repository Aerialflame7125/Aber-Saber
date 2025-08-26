namespace System.Web.Services.Protocols;

/// <summary>Specifies how a SOAP message is routed to the Web server hosting the XML Web service.</summary>
public enum SoapServiceRoutingStyle
{
	/// <summary>The SOAP message is routed based on the <see langword="SOAPAction" /> HTTP header.</summary>
	SoapAction,
	/// <summary>The SOAP Message is routed based on the first child element following the <see langword="&lt;Body&gt;" /> XML element of the SOAP message.</summary>
	RequestElement
}
