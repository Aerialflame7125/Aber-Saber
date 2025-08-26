using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Specifies whether the import is made to the server or to the client computer.</summary>
public enum ServiceDescriptionImportStyle
{
	/// <summary>Specifies that the import should be made to the client computer.</summary>
	[XmlEnum("client")]
	Client,
	/// <summary>Specifies that the import should be made to the server.</summary>
	[XmlEnum("server")]
	Server,
	/// <summary>Specifies that the import should be made to a server interface.</summary>
	[XmlEnum("serverInterface")]
	ServerInterface
}
