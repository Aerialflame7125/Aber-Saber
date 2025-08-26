using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design;

internal class SplitContainerDesigner : ParentControlDesigner
{
	public override void Initialize(IComponent component)
	{
		base.Initialize(component);
		SplitContainer splitContainer = (SplitContainer)component;
		EnableDesignMode(splitContainer.Panel1, "Panel1");
		EnableDesignMode(splitContainer.Panel2, "Panel2");
	}

	public override ControlDesigner InternalControlDesigner(int internalControlIndex)
	{
		return internalControlIndex switch
		{
			0 => GetDesigner(((SplitContainer)Control).Panel1), 
			1 => GetDesigner(((SplitContainer)Control).Panel2), 
			_ => null, 
		};
	}

	private ControlDesigner GetDesigner(IComponent component)
	{
		if (GetService(typeof(IDesignerHost)) is IDesignerHost designerHost)
		{
			return designerHost.GetDesigner(component) as ControlDesigner;
		}
		return null;
	}

	public override int NumberOfInternalControlDesigners()
	{
		return 2;
	}
}
