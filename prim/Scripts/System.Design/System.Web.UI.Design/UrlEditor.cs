using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.Design;

/// <summary>Provides a user interface for selecting a URL.</summary>
public class UrlEditor : UITypeEditor
{
	/// <summary>Gets the caption to display on the selection dialog box.</summary>
	/// <returns>The caption to display on the selection dialog box.</returns>
	protected virtual string Caption => "Select URL";

	/// <summary>Gets the file name filter string for the editor. This is used to determine the items that appear in the file list of the dialog box.</summary>
	/// <returns>A string that contains information about the file filtering options available in the dialog box.</returns>
	protected virtual string Filter => "All Files(*.*)|*.*|";

	/// <summary>Gets the options for the URL builder to use.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> that indicates the options for the URL builder to use.</returns>
	protected virtual UrlBuilderOptions Options => UrlBuilderOptions.None;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.UrlEditor" /> class.</summary>
	public UrlEditor()
	{
	}

	/// <summary>Edits the value of the specified object using the editor style provided by the <see cref="M:System.Web.UI.Design.UrlEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" /> method.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information.</param>
	/// <param name="provider">A service provider object through which editing services may be obtained.</param>
	/// <param name="value">An instance of the value being edited.</param>
	/// <returns>The new value of the object. If the value of the object hasn't changed, this method should return the same object it was passed.</returns>
	[System.MonoTODO]
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the editing style of the <see cref="M:System.Web.UI.Design.UrlEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information.</param>
	/// <returns>One of the <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> values indicating the provided editing style. If the method is not supported, this method will return <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
	[System.MonoTODO]
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}
}
