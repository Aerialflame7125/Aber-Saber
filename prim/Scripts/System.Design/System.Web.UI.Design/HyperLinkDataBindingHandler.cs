using System.ComponentModel.Design;

namespace System.Web.UI.Design;

/// <summary>Provides a data-binding handler for a hyperlink property.</summary>
public class HyperLinkDataBindingHandler : DataBindingHandler
{
	/// <summary>Initializes an instance of the <see cref="T:System.Web.UI.Design.HyperLinkDataBindingHandler" /> class.</summary>
	public HyperLinkDataBindingHandler()
	{
	}

	/// <summary>Resolves design-time data-binding for the specified control.</summary>
	/// <param name="designerHost">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the document that contains the control.</param>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to data bind.</param>
	[System.MonoTODO]
	public override void DataBindControl(IDesignerHost designerHost, Control control)
	{
		throw new NotImplementedException();
	}
}
