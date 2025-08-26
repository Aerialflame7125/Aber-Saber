using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Provides a set of specifications for data formats and protocols used by the XML Web service for input messages. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class InputBinding : MessageBinding
{
	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the collection of extensibility elements associated with the current <see cref="T:System.Web.Services.Description.InputBinding" />.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.InputBinding" /> class. </summary>
	public InputBinding()
	{
	}
}
