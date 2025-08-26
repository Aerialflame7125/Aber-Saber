namespace System.Web.Services.Description;

/// <summary>Specifies the type of transmission an endpoint of the XML Web service can support.</summary>
public enum OperationFlow
{
	/// <summary>Indicates that the endpoint of the XML Web service receives no transmissions.</summary>
	None,
	/// <summary>Indicates that the endpoint of the XML Web service receives a message.</summary>
	OneWay,
	/// <summary>Indicates that the endpoint of the XML Web service sends a message.</summary>
	Notification,
	/// <summary>Indicates that the endpoint of the XML Web service receives a message, then sends a correlated message.</summary>
	RequestResponse,
	/// <summary>Indicates that the endpoint of the XML Web service sends a message, then receives a correlated message.</summary>
	SolicitResponse
}
