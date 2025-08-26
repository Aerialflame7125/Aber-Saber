using System.ComponentModel.Design;

namespace System.Web.UI.Design;

/// <summary>Provides a data-binding handler for a data-bound control at design time.</summary>
public class TextDataBindingHandler : DataBindingHandler
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TextDataBindingHandler" /> class.</summary>
	public TextDataBindingHandler()
	{
	}

	/// <summary>Data-binds the specified control.</summary>
	/// <param name="designerHost">An object implementing <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the document that contains the control.</param>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to data-bind.</param>
	[System.MonoTODO]
	public override void DataBindControl(IDesignerHost designerHost, Control control)
	{
		throw new NotImplementedException();
	}
}
