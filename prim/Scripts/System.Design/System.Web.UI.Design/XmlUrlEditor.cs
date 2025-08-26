namespace System.Web.UI.Design;

/// <summary>Provides a user interface for selecting a URL that indicates the location of an XML file.</summary>
public class XmlUrlEditor : UrlEditor
{
	/// <summary>Gets the caption to display on the selection dialog window.</summary>
	/// <returns>The caption to display on the selection dialog window.</returns>
	protected override string Caption => "Select XML File";

	/// <summary>Gets the file name filter string for the editor. This is used to determine the items that appear in the file list of the dialog box.</summary>
	/// <returns>A string that contains information about the file filtering options available in the dialog box.</returns>
	protected override string Filter => "XML Files(*.xml)|*.xml|All Files(*.*)|*.*|";

	/// <summary>Gets the options for the URL builder to use.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> that indicates the options for the URL builder to use.</returns>
	protected override UrlBuilderOptions Options => UrlBuilderOptions.NoAbsolute;

	/// <summary>Initializes an instance of the <see cref="T:System.Web.UI.Design.XmlUrlEditor" /> class.</summary>
	public XmlUrlEditor()
	{
	}
}
