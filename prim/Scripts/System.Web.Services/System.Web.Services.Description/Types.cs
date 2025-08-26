using System.Web.Services.Configuration;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Describes data type definitions relevant to exchanged messages. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class Types : DocumentableItem
{
	private XmlSchemas schemas;

	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the collection of <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> elements included in the XML Web service. This property is read-only.</summary>
	/// <returns>A collection of extension elements included in the XML Web service.</returns>
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

	/// <summary>Gets the collection of XML schemas included as data type definitions for the XML Web service. This property is read-only.</summary>
	/// <returns>An <see cref="T:System.Xml.Serialization.XmlSchemas" /> collection.</returns>
	[XmlElement("schema", typeof(XmlSchema), Namespace = "http://www.w3.org/2001/XMLSchema")]
	public XmlSchemas Schemas
	{
		get
		{
			if (schemas == null)
			{
				schemas = new XmlSchemas();
			}
			return schemas;
		}
	}

	internal bool HasItems()
	{
		if (schemas == null || schemas.Count <= 0)
		{
			if (extensions != null)
			{
				return extensions.Count > 0;
			}
			return false;
		}
		return true;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Types" /> class.</summary>
	public Types()
	{
	}
}
