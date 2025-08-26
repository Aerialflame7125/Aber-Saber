using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents a message type passed by the action of an XML Web service.</summary>
public abstract class OperationMessage : NamedItem
{
	private XmlQualifiedName message = XmlQualifiedName.Empty;

	private Operation parent;

	/// <summary>Gets the <see cref="T:System.Web.Services.Description.Operation" /> of which the <see cref="T:System.Web.Services.Description.OperationMessage" /> is a member.</summary>
	/// <returns>The operation of which the <see cref="T:System.Web.Services.Description.OperationMessage" /> is a member.</returns>
	public Operation Operation => parent;

	/// <summary>Gets or sets an abstract, typed definition of the data being communicated.</summary>
	/// <returns>An XML qualified name.</returns>
	[XmlAttribute("message")]
	public XmlQualifiedName Message
	{
		get
		{
			return message;
		}
		set
		{
			message = value;
		}
	}

	internal void SetParent(Operation parent)
	{
		this.parent = parent;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.OperationMessage" /> class.</summary>
	protected OperationMessage()
	{
	}
}
