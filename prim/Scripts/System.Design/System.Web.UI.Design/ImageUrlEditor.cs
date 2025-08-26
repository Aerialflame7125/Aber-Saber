namespace System.Web.UI.Design;

/// <summary>Provides a user interface for selecting a URL that references an image.</summary>
public class ImageUrlEditor : UrlEditor
{
	/// <summary>Gets the caption to display on the selection dialog window.</summary>
	/// <returns>The caption to display on the selection dialog window.</returns>
	protected override string Caption => "Select Image";

	/// <summary>Gets the file name filter string for the editor. This string is used to determine the items that appear in the file list of the dialog box.</summary>
	/// <returns>The filter string that can be used to filter the file list of the dialog box.</returns>
	protected override string Filter => "Image Files(*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png|All Files(*.*)|*.*|";

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ImageUrlEditor" /> class.</summary>
	public ImageUrlEditor()
	{
	}
}
