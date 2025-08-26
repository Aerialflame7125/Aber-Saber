using System.Drawing.Design;

namespace System.ComponentModel.Design;

/// <summary>Provides a user interface for editing binary data.</summary>
public sealed class BinaryEditor : UITypeEditor
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.BinaryEditor" /> class.</summary>
	public BinaryEditor()
	{
	}

	/// <summary>Edits the value of the specified object using the specified service provider and context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <param name="provider">A service provider object through which editing services may be obtained.</param>
	/// <param name="value">The object to edit the value of.</param>
	/// <returns>The new value of the object. If the value of the object hasn't changed, this should return the same object it was passed.</returns>
	[System.MonoTODO]
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the editor style used by the <see cref="M:System.ComponentModel.Design.BinaryEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <returns>An <see langword="enum" /> value indicating the provided editing style.</returns>
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		return UITypeEditorEditStyle.Modal;
	}
}
