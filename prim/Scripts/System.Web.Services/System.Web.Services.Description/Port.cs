using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Defines an individual endpoint contained in the XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class Port : NamedItem
{
	private ServiceDescriptionFormatExtensionCollection extensions;

	private XmlQualifiedName binding = XmlQualifiedName.Empty;

	private Service parent;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.Service" /> of which the <see cref="T:System.Web.Services.Description.Port" /> is a member.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.Service" /> of which the <see cref="T:System.Web.Services.Description.Port" /> is a member.</returns>
	public Service Service => parent;

	/// <summary>Gets the collection of extensibility elements associated with the <see cref="T:System.Web.Services.Description.Port" />.</summary>
	/// <returns>The collection of extensibility elements associated with the port.</returns>
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

	/// <summary>Gets or sets the value of the XML <see langword="&lt;binding&gt;" /> attribute of the <see cref="T:System.Web.Services.Description.Port" />.</summary>
	/// <returns>The value of the XML binding.</returns>
	[XmlAttribute("binding")]
	public XmlQualifiedName Binding
	{
		get
		{
			return binding;
		}
		set
		{
			binding = value;
		}
	}

	internal void SetParent(Service parent)
	{
		this.parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Port" /> class.</summary>
	public Port()
	{
	}
}
