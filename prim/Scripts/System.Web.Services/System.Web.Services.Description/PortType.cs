using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents a named set of abstract operations and the corresponding abstract messages. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class PortType : NamedItem
{
	private OperationCollection operations;

	private ServiceDescription parent;

	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.PortType" />.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.PortType" />.</returns>
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

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.PortType" /> is a member.</summary>
	/// <returns>A service description of which the <see cref="T:System.Web.Services.Description.PortType" /> is a member.</returns>
	public ServiceDescription ServiceDescription => parent;

	/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.Operation" /> instances defined by the <see cref="T:System.Web.Services.Description.PortType" />.</summary>
	/// <returns>A collection of operation instances defined by the <see cref="T:System.Web.Services.Description.PortType" />.</returns>
	[XmlElement("operation")]
	public OperationCollection Operations
	{
		get
		{
			if (operations == null)
			{
				operations = new OperationCollection(this);
			}
			return operations;
		}
	}

	internal void SetParent(ServiceDescription parent)
	{
		this.parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.PortType" /> class.</summary>
	public PortType()
	{
	}
}
