using System.Web.Services.Configuration;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Provides an abstract definition of data passed by an XML Web service. This class cannot be inherited.</summary>
[XmlFormatExtensionPoint("Extensions")]
public sealed class Message : NamedItem
{
	private MessagePartCollection parts;

	private ServiceDescription parent;

	private ServiceDescriptionFormatExtensionCollection extensions;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Message" />.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Description.ServiceDescriptionFormatExtensionCollection" /> associated with this <see cref="T:System.Web.Services.Description.Message" />.</returns>
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

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.ServiceDescription" /> of which the current <see cref="T:System.Web.Services.Description.Message" /> is a member.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.ServiceDescription" />.</returns>
	public ServiceDescription ServiceDescription => parent;

	/// <summary>Gets the collection of the <see cref="T:System.Web.Services.Description.MessagePart" /> objects contained in the <see cref="T:System.Web.Services.Description.Message" />.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.MessagePartCollection" />.</returns>
	[XmlElement("part")]
	public MessagePartCollection Parts
	{
		get
		{
			if (parts == null)
			{
				parts = new MessagePartCollection(this);
			}
			return parts;
		}
	}

	internal void SetParent(ServiceDescription parent)
	{
		this.parent = parent;
	}

	/// <summary>Searches the <see cref="T:System.Web.Services.Description.MessagePartCollection" /> returned by the <see cref="P:System.Web.Services.Description.Message.Parts" /> property and returns an array of type <see cref="T:System.Web.Services.Description.MessagePart" /> that contains the named instances.</summary>
	/// <param name="partNames">An array of names of the <see cref="T:System.Web.Services.Description.MessagePart" /> instances to be returned. </param>
	/// <returns>An array of type <see cref="T:System.Web.Services.Description.MessagePart" />.</returns>
	/// <exception cref="T:System.ArgumentException">No <see cref="T:System.Web.Services.Description.MessagePart" /> instances with the specified names exist within the collection. </exception>
	public MessagePart[] FindPartsByName(string[] partNames)
	{
		MessagePart[] array = new MessagePart[partNames.Length];
		for (int i = 0; i < partNames.Length; i++)
		{
			array[i] = FindPartByName(partNames[i]);
		}
		return array;
	}

	/// <summary>Searches the <see cref="T:System.Web.Services.Description.MessagePartCollection" /> returned by the <see cref="P:System.Web.Services.Description.Message.Parts" /> property, and returns the named <see cref="T:System.Web.Services.Description.MessagePart" />.</summary>
	/// <param name="partName">A string that names the <see cref="T:System.Web.Services.Description.MessagePart" /> to be returned.</param>
	/// <returns>A <see cref="T:System.Web.Services.Description.MessagePart" />.</returns>
	/// <exception cref="T:System.ArgumentException">No <see cref="T:System.Web.Services.Description.MessagePart" /> with the specified name exists within the collection.</exception>
	public MessagePart FindPartByName(string partName)
	{
		for (int i = 0; i < parts.Count; i++)
		{
			MessagePart messagePart = parts[i];
			if (messagePart.Name == partName)
			{
				return messagePart;
			}
		}
		throw new ArgumentException(Res.GetString("MissingMessagePartForMessageFromNamespace3", partName, base.Name, ServiceDescription.TargetNamespace), "partName");
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.Message" /> class. </summary>
	public Message()
	{
	}
}
