using System.ComponentModel.Design;

namespace System.Web.UI.Design;

/// <summary>Provides a base class for a data-binding handler.</summary>
public abstract class DataBindingHandler
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.DataBindingHandler" /> class.</summary>
	protected DataBindingHandler()
	{
	}

	/// <summary>Binds the specified control.</summary>
	/// <param name="designerHost">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the document.</param>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to bind.</param>
	public abstract void DataBindControl(IDesignerHost designerHost, Control control);
}
