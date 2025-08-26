using System.ComponentModel;
using System.ComponentModel.Design.Data;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Provides a base class for a user interface to select and edit a connection string property at design time.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class ConnectionStringEditor : UITypeEditor
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ConnectionStringEditor" /> class.</summary>
	public ConnectionStringEditor()
	{
	}

	/// <summary>Edits the value of the specified object by using the specified service provider and context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> to use to gain additional context information.</param>
	/// <param name="provider">A service provider object through which to obtain editing services.</param>
	/// <param name="value">An instance of the object being edited.</param>
	/// <returns>The selected connection expression, as a string object; otherwise, if a connection expression was not selected, the same <paramref name="value" /> that was passed in.</returns>
	[System.MonoTODO]
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the editing style that is associated with the connection string editor for the specified context.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> to use to gain additional context information.</param>
	/// <returns>An <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value that indicates the editing style for the provided user interface.</returns>
	[System.MonoTODO]
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the provider name for the provided instance of the <see cref="T:System.Web.UI.Design.ConnectionStringEditor" /> class.</summary>
	/// <param name="instance">A <see cref="T:System.Web.UI.Design.ConnectionStringEditor" /> or an instance of a derived class.</param>
	/// <returns>Always an empty string ("").</returns>
	[System.MonoTODO]
	protected virtual string GetProviderName(object instance)
	{
		throw new NotImplementedException();
	}

	/// <summary>Puts the provider name on the specified instance of the <see cref="T:System.Web.UI.Design.ConnectionStringEditor" /> class.</summary>
	/// <param name="instance">A <see cref="T:System.Web.UI.Design.ConnectionStringEditor" /> or an instance of a derived class.</param>
	/// <param name="connection">A <see cref="T:System.ComponentModel.Design.Data.DesignerDataConnection" />.</param>
	[System.MonoTODO]
	protected virtual void SetProviderName(object instance, DesignerDataConnection connection)
	{
		throw new NotImplementedException();
	}
}
