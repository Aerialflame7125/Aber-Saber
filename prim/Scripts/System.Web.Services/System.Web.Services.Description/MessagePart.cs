using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents the messages to be broken up into their logical units with specific abstract information for each part. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class MessagePart : NamedItem
{
	private XmlQualifiedName type = XmlQualifiedName.Empty;

	private XmlQualifiedName element = XmlQualifiedName.Empty;

	private Message parent;

	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.MessagePart" />.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.MessagePart" />.</returns>
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

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.Message" /> of which the <see cref="T:System.Web.Services.Description.MessagePart" /> is a member.</summary>
	/// <returns>The message of which the <see cref="T:System.Web.Services.Description.MessagePart" /> is a member.</returns>
	public Message Message => parent;

	/// <summary>Gets or sets the name of the XML element that corresponds to the current <see cref="T:System.Web.Services.Description.MessagePart" />.</summary>
	/// <returns>The name of the XML element that corresponds to the current <see cref="T:System.Web.Services.Description.MessagePart" />.</returns>
	[XmlAttribute("element")]
	public XmlQualifiedName Element
	{
		get
		{
			return element;
		}
		set
		{
			element = value;
		}
	}

	/// <summary>Gets or sets the XML data type of the <see cref="T:System.Web.Services.Description.MessagePart" />.</summary>
	/// <returns>An <see cref="T:System.Xml.XmlQualifiedName" />.</returns>
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

	internal void SetParent(Message parent)
	{
		this.parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.MessagePart" /> class.</summary>
	public MessagePart()
	{
	}
}
