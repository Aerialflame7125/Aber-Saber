using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" />, an <see cref="T:System.Web.Services.Description.OutputBinding" />, or a <see cref="T:System.Web.Services.Description.MimePart" />, specifying the text patterns for which to search the HTTP transmission. This class cannot be inherited.</summary>
[XmlFormatExtension("text", "http://microsoft.com/wsdl/mime/textMatching/", typeof(InputBinding), typeof(OutputBinding), typeof(MimePart))]
[XmlFormatExtensionPrefix("tm", "http://microsoft.com/wsdl/mime/textMatching/")]
public sealed class MimeTextBinding : ServiceDescriptionFormatExtension
{
	private MimeTextMatchCollection matches = new MimeTextMatchCollection();

	/// <summary>Specifies the URI for the XML namespace of the <see cref="T:System.Web.Services.Description.MimeTextBinding" /> class. This field is constant.</summary>
	public const string Namespace = "http://microsoft.com/wsdl/mime/textMatching/";

	/// <summary>Gets the collection of MIME text patterns for which the HTTP transmission is searched.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.MimeTextMatchCollection" /> representing the MIME text patterns to search for.</returns>
	[XmlElement("match", typeof(MimeTextMatch))]
	public MimeTextMatchCollection Matches => matches;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.MimeTextBinding" /> class. </summary>
	public MimeTextBinding()
	{
	}
}
