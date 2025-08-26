namespace System.Web.UI.Design;

/// <summary>Provides a user interface for selecting an URL that indicates the location of an XSL file.</summary>
public class XslUrlEditor : UrlEditor
{
	/// <summary>Gets the caption to display on the selection dialog window.</summary>
	/// <returns>The caption to display on the selection dialog window.</returns>
	protected override string Caption => "Select XSL Transform File";

	/// <summary>Gets the file name filter string for the editor. This is used to determine the items that appear in the file list of the dialog box.</summary>
	/// <returns>A string that contains information about the file filtering options available in the dialog box.</returns>
	protected override string Filter => "XSL Files(*.xsl;*.xslt)|*.xsl;*.xslt|All Files(*.*)|*.*|";

	/// <summary>Gets the options for the URL builder to use.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> that indicates the options for the URL builder to use.</returns>
	protected override UrlBuilderOptions Options => UrlBuilderOptions.NoAbsolute;

	/// <summary>Initializes an instance of the <see cref="T:System.Web.UI.Design.XslUrlEditor" /> class.</summary>
	public XslUrlEditor()
	{
	}
}
