using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents an extensibility element added to an XML Web service.</summary>
public abstract class ServiceDescriptionFormatExtension
{
	private object parent;

	private bool required;

	private bool handled;

	/// <summary>Gets the parent of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />.</summary>
	/// <returns>The parent of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" />.</returns>
	public object Parent => parent;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is necessary for the action to which it refers.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is required; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[XmlAttribute("required", Namespace = "http://schemas.xmlsoap.org/wsdl/")]
	[DefaultValue(false)]
	public bool Required
	{
		get
		{
			return required;
		}
		set
		{
			required = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is used by the import process when the extensibility element is imported.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> is used by the import process; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[XmlIgnore]
	public bool Handled
	{
		get
		{
			return handled;
		}
		set
		{
			handled = value;
		}
	}

	internal void SetParent(object parent)
	{
		this.parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtension" /> class.</summary>
	protected ServiceDescriptionFormatExtension()
	{
	}
}
