using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Windows.Forms.Design;

/// <summary>Provides an editor for setting the <see cref="P:System.Windows.Forms.ToolStripStatusLabel.BorderSides" /> property.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class BorderSidesEditor : UITypeEditor
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.BorderSidesEditor" /> class.</summary>
	public BorderSidesEditor()
	{
	}

	/// <summary>Edits the given object value using the editor style provided by <see cref="M:System.Windows.Forms.Design.BorderSidesEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" />.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> providing information about the control or component.</param>
	/// <param name="provider">An <see cref="T:System.IServiceProvider" /> providing custom support to other objects.</param>
	/// <param name="value">The object value to edit.</param>
	/// <returns>The edited object.</returns>
	[System.MonoTODO]
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the editing style of the <see langword="EditValue" /> method.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> providing information about the control or component.</param>
	/// <returns>One of the <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> values. If the method is not supported, this method returns <see cref="F:System.Drawing.Design.UITypeEditorEditStyle.None" />.</returns>
	[System.MonoTODO]
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}
}
