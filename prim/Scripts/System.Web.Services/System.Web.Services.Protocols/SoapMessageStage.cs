namespace System.Web.Services.Protocols;

/// <summary>Specifies the processing stage of a SOAP message.</summary>
public enum SoapMessageStage
{
	/// <summary>The stage just prior to a <see cref="T:System.Web.Services.Protocols.SoapMessage" /> being serialized.</summary>
	BeforeSerialize = 1,
	/// <summary>The stage just after a <see cref="T:System.Web.Services.Protocols.SoapMessage" /> is serialized, but before the SOAP message is sent over the wire.</summary>
	AfterSerialize = 2,
	/// <summary>The stage just before a <see cref="T:System.Web.Services.Protocols.SoapMessage" /> is deserialized from the SOAP message sent across the network into an object.</summary>
	BeforeDeserialize = 4,
	/// <summary>The stage just after a <see cref="T:System.Web.Services.Protocols.SoapMessage" /> is deserialized from a SOAP message into an object.</summary>
	AfterDeserialize = 8
}
