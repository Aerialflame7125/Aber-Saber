using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Specifies the format for any error messages that might be output as a result of the operation. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class FaultBinding : MessageBinding
{
	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the collection of extensibility elements associated with the current <see cref="T:System.Web.Services.Description.FaultBinding" />.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" />. The default value is a collection with a <see cref="P:System.Collections.CollectionBase.Count" /> of zero.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.FaultBinding" /> class. </summary>
	public FaultBinding()
	{
	}
}
