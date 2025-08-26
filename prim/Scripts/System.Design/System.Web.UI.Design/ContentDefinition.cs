namespace System.Web.UI.Design;

/// <summary>Provides a property structure that defines Web content at design time.</summary>
public class ContentDefinition
{
	private string id;

	private string content;

	private string html;

	/// <summary>Gets the ID of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> control that is associated with the current content.</summary>
	/// <returns>The ID of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> associated with the current content.</returns>
	public string ContentPlaceHolderID => id;

	/// <summary>Gets the default HTML markup for the content.</summary>
	/// <returns>A string containing HTML markup.</returns>
	public string DefaultContent => content;

	/// <summary>Gets the HTML markup to represent the content at design time.</summary>
	/// <returns>A string containing HTML markup.</returns>
	public string DefaultDesignTimeHtml => html;

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.Design.ContentDefinition" /> class.</summary>
	/// <param name="id">A string identifier for the content.</param>
	/// <param name="content">The default HTML markup content.</param>
	/// <param name="designTimeHtml">The design-time HTML markup content.</param>
	public ContentDefinition(string id, string content, string designTimeHtml)
	{
		this.id = id;
		this.content = content;
		html = designTimeHtml;
	}
}
