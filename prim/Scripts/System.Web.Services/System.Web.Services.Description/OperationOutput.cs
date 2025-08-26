using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Defines the specifications for output messages returned by the XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class OperationOutput : OperationMessage
{
	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.OperationOutput" />.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.OperationOutput" />.</returns>
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.OperationOutput" /> class. </summary>
	public OperationOutput()
	{
	}
}
