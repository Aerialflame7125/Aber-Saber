using System.ComponentModel;

namespace System.Windows.Forms.Design;

internal interface IOleDragClient
{
	bool CanModifyComponents { get; }

	IComponent Component { get; }

	bool AddComponent(IComponent component, string name, bool firstAdd);

	Control GetControlForComponent(object component);

	Control GetDesignerControl();

	bool IsDropOk(IComponent component);
}
