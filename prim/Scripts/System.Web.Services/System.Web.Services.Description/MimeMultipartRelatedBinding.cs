using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" />, specifying the individual MIME formats for the parts of the HTTP transmission. This class cannot be inherited.</summary>
[XmlFormatExtension("multipartRelated", "http://schemas.xmlsoap.org/wsdl/mime/", typeof(InputBinding), typeof(OutputBinding))]
public sealed class MimeMultipartRelatedBinding : ServiceDescriptionFormatExtension
{
	private MimePartCollection parts = new MimePartCollection();

	/// <summary>Gets the collection of extensibility elements added to the <see cref="T:System.Web.Services.Description.MimeMultipartRelatedBinding" /> to specify the MIME format for the parts of the MIME message.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.MimePartCollection" /> representing extensibility elements added to the <see cref="T:System.Web.Services.Description.MimeMultipartRelatedBinding" />.</returns>
	[XmlElement("part")]
	public MimePartCollection Parts => parts;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.MimeMultipartRelatedBinding" /> class. </summary>
	public MimeMultipartRelatedBinding()
	{
	}
}
