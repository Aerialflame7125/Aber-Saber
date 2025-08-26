using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Associates an XML namespace with a document location. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class Import : DocumentableItem
{
	private string ns;

	private string location;

	private ServiceDescription parent;

	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Import" /> class.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Import" /> class.</returns>
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

	/// <summary>Gets a reference to the <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Import" /> is a member.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the <see cref="T:System.Web.Services.Description.Import" /> is a member.</returns>
	public ServiceDescription ServiceDescription => parent;

	/// <summary>Gets or sets the value of the XML <see langword="namespace" /> attribute of the <see langword="import" /> element.</summary>
	/// <returns>The value of the XML <see langword="namespace" /> attribute of the <see langword="import" /> element.</returns>
	[XmlAttribute("namespace")]
	public string Namespace
	{
		get
		{
			if (ns != null)
			{
				return ns;
			}
			return string.Empty;
		}
		set
		{
			ns = value;
		}
	}

	/// <summary>Gets or sets the value of the XML <see langword="location" /> attribute of the <see langword="import" /> element.</summary>
	/// <returns>The value of the XML <see langword="location" /> attribute of the <see langword="import" /> element. This value also specifies the URL of the imported document.</returns>
	[XmlAttribute("location")]
	public string Location
	{
		get
		{
			if (location != null)
			{
				return location;
			}
			return string.Empty;
		}
		set
		{
			location = value;
		}
	}

	internal void SetParent(ServiceDescription parent)
	{
		this.parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Import" /> class.</summary>
	public Import()
	{
	}
}
