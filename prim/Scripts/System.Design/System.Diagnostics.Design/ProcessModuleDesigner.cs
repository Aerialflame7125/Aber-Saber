using System.Collections;
using System.ComponentModel.Design;

namespace System.Diagnostics.Design;

/// <summary>Base designer class for extending the design-mode behavior of a process module.</summary>
public class ProcessModuleDesigner : ComponentDesigner
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Design.ProcessModuleDesigner" /> class.</summary>
	public ProcessModuleDesigner()
	{
	}

	/// <summary>Adjusts the set of properties the process module exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	/// <param name="properties">An <see cref="T:System.Collections.IDictionary" /> containing the properties for the class of the component.</param>
	[System.MonoTODO]
	protected override void PreFilterProperties(IDictionary properties)
	{
		throw new NotImplementedException();
	}
}
