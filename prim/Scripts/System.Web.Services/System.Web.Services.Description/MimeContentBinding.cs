using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to an <see cref="T:System.Web.Services.Description.InputBinding" /> or an <see cref="T:System.Web.Services.Description.OutputBinding" /> within an XML Web service, specifying the MIME format for the body of the HTTP transmission. This class cannot be inherited.</summary>
[XmlFormatExtension("content", "http://schemas.xmlsoap.org/wsdl/mime/", typeof(MimePart), typeof(InputBinding), typeof(OutputBinding))]
[XmlFormatExtensionPrefix("mime", "http://schemas.xmlsoap.org/wsdl/mime/")]
public sealed class MimeContentBinding : ServiceDescriptionFormatExtension
{
	private string type;

	private string part;

	/// <summary>Specifies the URI for the XML namespace of the <see cref="T:System.Web.Services.Description.MimeContentBinding" /> class. This field is constant.</summary>
	public const string Namespace = "http://schemas.xmlsoap.org/wsdl/mime/";

	/// <summary>Gets or sets the name of the <see cref="T:System.Web.Services.Description.MessagePart" /> to which the <see cref="T:System.Web.Services.Description.MimeContentBinding" /> applies.</summary>
	/// <returns>A string representing the name of the <see cref="T:System.Web.Services.Description.MessagePart" /> with which the current <see cref="T:System.Web.Services.Description.MimeContentBinding" /> is associated. The default value is an empty string ("").</returns>
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

	/// <summary>Gets or sets a value indicating the format of the body of the HTTP transmission.</summary>
	/// <returns>A string indicating the format of the body of the HTTP transmission. The default value is an empty string ("").</returns>
	[XmlAttribute("type")]
	public string Type
	{
		get
		{
			if (type != null)
			{
				return type;
			}
			return string.Empty;
		}
		set
		{
			type = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.MimeContentBinding" /> class. </summary>
	public MimeContentBinding()
	{
	}
}
