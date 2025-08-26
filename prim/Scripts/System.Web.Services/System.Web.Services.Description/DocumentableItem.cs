using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents the abstract base class from which several classes in the <see cref="N:System.Web.Services.Description" /> namespace are derived.</summary>
public abstract class DocumentableItem
{
	private XmlDocument parent;

	private string documentation;

	private XmlElement documentationElement;

	private XmlAttribute[] anyAttribute;

	private XmlSerializerNamespaces namespaces;

	/// <summary>Gets or sets the text documentation for the instance of the <see cref="T:System.Web.Services.Description.DocumentableItem" />.</summary>
	/// <returns>A string that represents the documentation for the <see cref="T:System.Web.Services.Description.DocumentableItem" />.</returns>
	[XmlIgnore]
	public string Documentation
	{
		get
		{
			if (documentation != null)
			{
				return documentation;
			}
			if (documentationElement == null)
			{
				return string.Empty;
			}
			return documentationElement.InnerXml;
		}
		set
		{
			documentation = value;
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			xmlTextWriter.WriteElementString("wsdl", "documentation", "http://schemas.xmlsoap.org/wsdl/", value);
			Parent.LoadXml(stringWriter.ToString());
			documentationElement = parent.DocumentElement;
			xmlTextWriter.Close();
		}
	}

	/// <summary>Gets or sets the documentation element for the <see cref="T:System.Web.Services.Description.DocumentableItem" />.</summary>
	/// <returns>A <see cref="T:System.Xml.XmlElement" /> that represents the documentation for the <see cref="T:System.Web.Services.Description.DocumentableItem" />.</returns>
	[XmlAnyElement("documentation", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
	[ComVisible(false)]
	public XmlElement DocumentationElement
	{
		get
		{
			return documentationElement;
		}
		set
		{
			documentationElement = value;
			documentation = null;
		}
	}

	/// <summary>Gets or sets an array of type <see cref="T:System.Xml.XmlAttribute" /> that represents attribute extensions of WSDL to comply with Web Services Interoperability (WS-I) Basic Profile 1.1.</summary>
	/// <returns>An array of type <see cref="T:System.Xml.XmlAttribute" /> that represents attribute extensions of WSDL to comply with Web Services Interoperability (WS-I) Basic Profile 1.1.</returns>
	[XmlAnyAttribute]
	public XmlAttribute[] ExtensibleAttributes
	{
		get
		{
			return anyAttribute;
		}
		set
		{
			anyAttribute = value;
		}
	}

	/// <summary>Gets or sets the dictionary of namespace prefixes and namespaces used to preserve namespace prefixes and namespaces when a <see cref="T:System.Web.Services.Description.ServiceDescription" /> object is constructed.</summary>
	/// <returns>A <see cref="T:System.Xml.Serialization.XmlSerializerNamespaces" /> dictionary containing prefix/namespace pairs.</returns>
	[XmlNamespaceDeclarations]
	public XmlSerializerNamespaces Namespaces
	{
		get
		{
			if (namespaces == null)
			{
				namespaces = new XmlSerializerNamespaces();
			}
			return namespaces;
		}
		set
		{
			namespaces = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.DocumentableItem" />.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.DocumentableItem" />.</returns>
	[XmlIgnore]
	public abstract ServiceDescriptionFormatExtensionCollection Extensions { get; }

	internal XmlDocument Parent
	{
		get
		{
			if (parent == null)
			{
				parent = new XmlDocument();
			}
			return parent;
		}
	}

	internal XmlElement GetDocumentationElement()
	{
		if (documentationElement == null)
		{
			documentationElement = Parent.CreateElement("wsdl", "documentation", "http://schemas.xmlsoap.org/wsdl/");
			Parent.InsertBefore(documentationElement, null);
		}
		return documentationElement;
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Description.DocumentableItem" /> class.</summary>
	protected DocumentableItem()
	{
	}
}
