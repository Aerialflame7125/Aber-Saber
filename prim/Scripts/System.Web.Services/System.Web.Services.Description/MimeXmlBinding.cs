using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.MimePart" />, an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" />. It specifies the schema for XML messages that are not SOAP compliant. This class cannot be inherited.</summary>
[XmlFormatExtension("mimeXml", "http://schemas.xmlsoap.org/wsdl/mime/", typeof(MimePart), typeof(InputBinding), typeof(OutputBinding))]
public sealed class MimeXmlBinding : ServiceDescriptionFormatExtension
{
	private string part;

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.Services.Description.MessagePart" /> to which the <see cref="T:System.Web.Services.Description.MimeXmlBinding" /> applies.</summary>
	/// <returns>The name of the corresponding <see cref="T:System.Web.Services.Description.MessagePart" />. The default value is an empty string ("").</returns>
	[XmlAttribute("part")]
	public string Part
	{
		get
		{
			return part;
		}
		set
		{
			part = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.MimeXmlBinding" /> class. </summary>
	public MimeXmlBinding()
	{
	}
}
