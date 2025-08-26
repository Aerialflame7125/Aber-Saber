using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Groups together a set of related instances of the <see cref="T:System.Web.Services.Description.Port" /> class that are associated with an XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class Service : NamedItem
{
	private ServiceDescriptionFormatExtensionCollection extensions;

	private PortCollection ports;

	private ServiceDescription parent;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Service" /> is a member.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Service" /> is a member.</returns>
	public ServiceDescription ServiceDescription => parent;

	/// <summary>Gets the collection of extensibility elements associated with the <see cref="T:System.Web.Services.Description.Service" />.</summary>
	/// <returns>The collection of extensibility elements associated with the <see cref="T:System.Web.Services.Description.Service" />.</returns>
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

	/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Port" /> instances contained in the <see cref="T:System.Web.Services.Description.Service" />.</summary>
	/// <returns>A collection of port instances contained in the <see cref="T:System.Web.Services.Description.Service" />.</returns>
	[XmlElement("port")]
	public PortCollection Ports
	{
		get
		{
			if (ports == null)
			{
				ports = new PortCollection(this);
			}
			return ports;
		}
	}

	internal void SetParent(ServiceDescription parent)
	{
		this.parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Service" /> class.</summary>
	public Service()
	{
	}
}
