using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Specifies whether the message parts are encoded as abstract type definitions or concrete schema definitions.</summary>
public enum SoapBindingUse
{
	/// <summary>Specifies an empty string ("") value for the corresponding XML <see langword="use" /> attribute.</summary>
	[XmlIgnore]
	Default,
	/// <summary>The message parts are encoded using given encoding rules.</summary>
	[XmlEnum("encoded")]
	Encoded,
	/// <summary>The message parts represent a concrete schema.</summary>
	[XmlEnum("literal")]
	Literal
}
