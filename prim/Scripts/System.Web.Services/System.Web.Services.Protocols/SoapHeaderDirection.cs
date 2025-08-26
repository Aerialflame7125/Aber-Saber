namespace System.Web.Services.Protocols;

/// <summary>Specifies whether the recipient of the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> is the XML Web service, the XML Web service client, or both.</summary>
[Flags]
public enum SoapHeaderDirection
{
	/// <summary>Specifies the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> is sent to the XML Web service.</summary>
	In = 1,
	/// <summary>Specifies the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> is sent to the XML Web service client.</summary>
	Out = 2,
	/// <summary>Specifies the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> is sent to both the XML Web service and the XML Web service client.</summary>
	InOut = 3,
	/// <summary>Specifies the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> is sent to the XML Web service client when an exception is thrown by the XML Web service method.</summary>
	Fault = 4
}
