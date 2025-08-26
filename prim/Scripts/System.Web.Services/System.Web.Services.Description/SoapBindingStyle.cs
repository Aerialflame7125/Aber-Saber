using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Specifies the type of action that occurs in the XML Web service at the level of the class hierarchy to which this enumeration is applied.</summary>
public enum SoapBindingStyle
{
	/// <summary>The default type of action for the current hierarchical level of the Web Services Description Language (WSDL) file.</summary>
	[XmlIgnore]
	Default,
	/// <summary>The message being transmitted is document-oriented.</summary>
	[XmlEnum("document")]
	Document,
	/// <summary>The message being transmitted contains the parameters to call a procedure or the return values from that procedure. RPC is an acronym for "remote procedure call." </summary>
	[XmlEnum("rpc")]
	Rpc
}
