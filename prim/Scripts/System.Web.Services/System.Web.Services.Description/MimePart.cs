using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to a <see cref="T:System.Web.Services.Description.MimeMultipartRelatedBinding" />, specifying the concrete MIME type for the <see cref="T:System.Web.Services.Description.MessagePart" /> to which the <see langword="MimePart" /> applies. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class MimePart : ServiceDescriptionFormatExtension
{
	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the collection of MIME extensibility elements for the part of the <see cref="T:System.Web.Services.Description.MimeMultipartRelatedBinding" /> of which the <see cref="T:System.Web.Services.Description.MimePart" /> is a member.</summary>
	/// <returns>A collection of service description format extension.</returns>
	[XmlIgnore]
	public ServiceDescriptionFormatExtensionCollection Extensions
	{
		get
		{
			if (extensions == null)
			{
				extensions = new ServiceDescriptionFormatExtensionCollection(this);
			}
			return extensions;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.MimePart" /> class.</summary>
	public MimePart()
	{
	}
}
