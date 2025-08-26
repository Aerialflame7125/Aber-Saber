using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Represents an item to embed in an e-mail message constructed using the <see cref="T:System.Web.UI.WebControls.MailDefinition" /> class.</summary>
public sealed class EmbeddedMailObject
{
	/// <summary>Gets or sets the name that is used as the identifier of the item to be embedded in a mail message constructed with the <see cref="T:System.Web.UI.WebControls.MailDefinition" /> class.</summary>
	/// <returns>Returns the identifier of the item to embed in a mail message.</returns>
	[NotifyParentProperty(true)]
	[DefaultValue("")]
	public string Name { get; set; }

	/// <summary>Gets or sets the path that is used to retrieve an item to embed in a mail message constructed with the <see cref="T:System.Web.UI.WebControls.MailDefinition" /> class.</summary>
	/// <returns>Returns the path to the item to embed in a mail message.</returns>
	[DefaultValue("")]
	[NotifyParentProperty(true)]
	[UrlProperty]
	[Editor("System.Web.UI.Design.MailFileEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string Path { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> class.</summary>
	public EmbeddedMailObject()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.EmbeddedMailObject" /> class, using the specified identifier name and path to populate the object.</summary>
	/// <param name="name">The name used as the identifier of the item to embed in the mail message. For more information, see <see cref="P:System.Web.UI.WebControls.EmbeddedMailObject.Name" />.</param>
	/// <param name="path">The path used to retrieve an item to embed in the mail message. For more information, see <see cref="P:System.Web.UI.WebControls.EmbeddedMailObject.Path" />.</param>
	public EmbeddedMailObject(string name, string path)
	{
		Name = name;
		Path = path;
	}
}
