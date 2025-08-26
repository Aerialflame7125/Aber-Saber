namespace System.Web.UI.Design;

/// <summary>Represents a client script element in a Web Form or user control at design time. This class cannot be inherited.</summary>
public sealed class ClientScriptItem
{
	private string text;

	private string source;

	private string language;

	private string type;

	private string id;

	/// <summary>Gets the <see langword="ID" /> attribute value for the client script element.</summary>
	/// <returns>The <see langword="ID" /> value of the <see langword="script" /> element.</returns>
	public string Id => id;

	/// <summary>Gets the <see langword="language" /> attribute value for the client script element.</summary>
	/// <returns>The language name specified for the <see langword="language" /> attribute in the <see langword="script" /> element.</returns>
	public string Language => language;

	/// <summary>Gets the <see langword="src" /> attribute value for the client script element.</summary>
	/// <returns>The path to the source file specified for the <see langword="src" /> attribute in the <see langword="script" /> element.</returns>
	public string Source => source;

	/// <summary>Gets the script statements contained in the client script element.</summary>
	/// <returns>The script statements contained in the <see langword="script" /> element.</returns>
	public string Text => text;

	/// <summary>Gets the <see langword="type" /> attribute value for the client script element.</summary>
	/// <returns>The name of the MIME type associated with the <see langword="script" /> element.</returns>
	public string Type => type;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ClientScriptItem" /> class using the provided parameters.</summary>
	/// <param name="text">The contents for the <see langword="script" /> element; a string of script statements to run on the client.</param>
	/// <param name="source">The <see langword="src" /> attribute value for the <see langword="script" /> element, specifying an external source location for the client script contents.</param>
	/// <param name="language">The <see langword="language" /> attribute value for the <see langword="script" /> element, specifying the language of the script statements.</param>
	/// <param name="type">The type attribute value for the <see langword="script" /> element, indicating the MIME type for the associated scripting engine.</param>
	/// <param name="id">The ID for the <see langword="script" /> element. This argument is required by the design host (for example, Visual Studio 2005).</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="id" /> is null (thrown by the design host).</exception>
	public ClientScriptItem(string text, string source, string language, string type, string id)
	{
		this.text = text;
		this.source = source;
		this.language = language;
		this.type = type;
		this.id = id;
	}
}
