using System.ComponentModel;
using System.Drawing.Design;

namespace System.Web.UI.Design;

/// <summary>Provides a user interface for editing a collection of data bindings.</summary>
[Obsolete("This class is not supposed to be in use anymore as DesignerActionList is supposed to be used for editing DataBinding")]
public class DataBindingCollectionEditor : UITypeEditor
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DataBindingCollectionEditor" /> class.</summary>
	public DataBindingCollectionEditor()
	{
	}

	/// <summary>Edits the value of the specified data-binding collection using the specified service provider and context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that identifies the component or control the collection belongs to.</param>
	/// <param name="provider">The <see cref="T:System.IServiceProvider" /> to use.</param>
	/// <param name="value">The collection to edit.</param>
	/// <returns>The new collection.</returns>
	[System.MonoTODO]
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the editor style used by the <see cref="M:System.Web.UI.Design.DataBindingCollectionEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that identifies the component or control to retrieve the edit style for.</param>
	/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> object that specifies the editor style of the component or control.</returns>
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		return UITypeEditorEditStyle.Modal;
	}
}
