using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Specifies the concrete data format and protocols used in the XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class Binding : NamedItem
{
	private ServiceDescriptionFormatExtensionCollection extensions;

	private OperationBindingCollection operations;

	private XmlQualifiedName type = XmlQualifiedName.Empty;

	private ServiceDescription parent;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Binding" /> is a member.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Binding" /> is a member.</returns>
	public ServiceDescription ServiceDescription => parent;

	/// <summary>Gets the collection of extensibility elements used in the XML Web service.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> object that contains the collection of extensibility elements used in the XML Web service.</returns>
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

	/// <summary>Gets the collection of specifications for data formats and message protocols used in the action supported by the XML Web service.</summary>
	/// <returns>An <see cref="T:System.Web.Services.Description.OperationBindingCollection" /> object that contains the collection of specifications for data formats and message protocols used in the action supported by the XML Web service.</returns>
	[XmlElement("operation")]
	public OperationBindingCollection Operations
	{
		get
		{
			if (operations == null)
			{
				operations = new OperationBindingCollection(this);
			}
			return operations;
		}
	}

	/// <summary>Gets or sets a value representing the namespace-qualified name of the <see cref="T:System.Web.Services.Description.PortType" /> with which the <see langword="Binding" /> is associated.</summary>
	/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" />  of the <see cref="T:System.Web.Services.Description.PortType" /> with which the <see langword="Binding" /> is associated.</returns>
	[XmlAttribute("type")]
	public XmlQualifiedName Type
	{
		get
		{
			if ((object)type == null)
			{
				return XmlQualifiedName.Empty;
			}
			return type;
		}
		set
		{
			type = value;
		}
	}

	internal void SetParent(ServiceDescription parent)
	{
		this.parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Binding" /> class.</summary>
	public Binding()
	{
	}
}
