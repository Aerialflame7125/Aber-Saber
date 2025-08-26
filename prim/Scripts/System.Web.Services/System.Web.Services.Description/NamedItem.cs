using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>Represents a named, item that can be documented.</summary>
public abstract class NamedItem : DocumentableItem
{
	private string name;

	/// <summary>Gets or sets the name of the item.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the name of the item.</returns>
	[XmlAttribute("name")]
	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	/// <summary>When called in a derived class, initializes a new instance of the <see cref="T:System.Web.Services.Description.NamedItem" /> class.</summary>
	protected NamedItem()
	{
	}
}
