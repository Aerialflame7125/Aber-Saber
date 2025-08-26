using System.ComponentModel;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Provides a user interface for selecting and editing an expressions binding collection at design time.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class ExpressionsCollectionEditor : UITypeEditor
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ExpressionsCollectionEditor" /> class.</summary>
	public ExpressionsCollectionEditor()
	{
	}

	/// <summary>Edits the value of the specified object with the specified service provider and context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information such as the associated control.</param>
	/// <param name="provider">A service provider object through which editing services can be obtained.</param>
	/// <param name="value">An instance of the object being edited.</param>
	/// <returns>An <see cref="T:System.Web.UI.ExpressionBindingCollection" /> object containing the selected expressions; otherwise, if no expressions are selected, the <paramref name="value" /> object.</returns>
	[System.MonoTODO]
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the editing style that is associated with this editor for the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that can be used to gain additional context information.</param>
	/// <returns>An <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value indicating the editing style for the provided user interface.</returns>
	[System.MonoTODO]
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}
}
