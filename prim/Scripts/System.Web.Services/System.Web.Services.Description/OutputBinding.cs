using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Provides a set of specifications for data formats and protocols used by the XML Web service for output messages. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class OutputBinding : MessageBinding
{
	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the collection of extensibility elements associated with the current <see cref="T:System.Web.Services.Description.OutputBinding" />.</summary>
	/// <returns>A collection of service description format extension.</returns>
	[XmlIgnore]
	public override ServiceDescriptionFormatExtensionCollection Extensions
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.OutputBinding" /> class.</summary>
	public OutputBinding()
	{
	}
}
