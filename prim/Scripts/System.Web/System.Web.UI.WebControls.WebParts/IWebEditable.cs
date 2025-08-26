namespace System.Web.UI.WebControls.WebParts;

/// <summary>Provides an interface for developers to specify custom editing controls that are associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
public interface IWebEditable
{
	/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, user control, or custom control that will be edited by <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
	/// <returns>An object reference to the control associated with an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</returns>
	object WebBrowsableObject { get; }

	/// <summary>Returns a collection of custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls associated with a server control that implements the <see cref="T:System.Web.UI.WebControls.WebParts.IWebEditable" /> interface.</summary>
	/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains the collection of custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls associated with a server control. </returns>
	EditorPartCollection CreateEditorParts();
}
