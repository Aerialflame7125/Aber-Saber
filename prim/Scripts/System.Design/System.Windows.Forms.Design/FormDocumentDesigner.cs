using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design;

internal class FormDocumentDesigner : DocumentDesigner
{
	public override void Initialize(IComponent component)
	{
		Form obj = (component as Form) ?? throw new NotSupportedException("FormDocumentDesigner can be initialized only with Forms");
		obj.TopLevel = false;
		obj.Visible = true;
		base.Initialize(component);
	}

	public override bool CanParent(Control control)
	{
		if (control is Form)
		{
			return false;
		}
		return base.CanParent(control);
	}

	protected override void WndProc(ref Message m)
	{
		switch ((Native.Msg)m.Msg)
		{
		case Native.Msg.WM_NCLBUTTONDOWN:
		case Native.Msg.WM_NCLBUTTONDBLCLK:
		case Native.Msg.WM_NCRBUTTONDOWN:
		case Native.Msg.WM_NCRBUTTONDBLCLK:
		case Native.Msg.WM_NCMBUTTONDOWN:
		case Native.Msg.WM_NCMBUTTONDBLCLK:
			if (GetService(typeof(ISelectionService)) is ISelectionService selectionService)
			{
				selectionService.SetSelectedComponents(new object[1] { base.Component });
			}
			break;
		default:
			base.WndProc(ref m);
			break;
		}
	}
}
